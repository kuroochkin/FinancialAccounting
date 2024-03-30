using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Abstractions;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;
using File = FinancialAccounting.Domain.Entities.File;

namespace FinancialAccounting.Dara.PostgreSql;

/// <summary>
/// Контекст EF Core для приложения
/// </summary>
public class EfContext : DbContext, IDbContext
{
    private const string DefaultSchema = "public";
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContext _userContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options">Параметры подключения к БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    /// <param name="dateTimeProvider">Провайдер даты и времени</param>
    public EfContext(
        DbContextOptions<EfContext> options,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : base(options)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }
    
    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

    public DbSet<Transfer> Transfers { get; set; }
    
    public DbSet<Photo> Photos { get; set; }

    public DbSet<File> Files { get; set; }

    public DbSet<Category> Categories { get; set; }
    public bool IsInMemory => Database.IsInMemory();
    
    /// <inheritdoc/>
	public override async Task<int> SaveChangesAsync(
	    bool acceptAllChangesOnSuccess, 
	    CancellationToken cancellationToken = default)
	{
		var entityEntries = ChangeTracker.Entries().ToArray();
		if (entityEntries.Length > 10)
			entityEntries.AsParallel().ForAll(OnSave);
		else
			foreach (var entityEntry in entityEntries)
				OnSave(entityEntry);

		try
		{
			var isNewTransaction = Database.CurrentTransaction is null;

			if (isNewTransaction)
				await Database.BeginTransactionAsync(cancellationToken);
			
			var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

			if (isNewTransaction)
			{
				await Database.CommitTransactionAsync(cancellationToken);
			}

			return result;
		}
		catch (DbUpdateException ex)
		{
			if (Database.CurrentTransaction is not null)
				await Database.RollbackTransactionAsync(cancellationToken);

			return HandleDbUpdateException(ex, cancellationToken);
		}
	}

	/// <inheritdoc cref="IDbContext.SaveChangesAsync(CancellationToken)" />
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
		await SaveChangesAsync(true, cancellationToken);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.HasDefaultSchema(DefaultSchema);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
	}

	protected virtual int HandleDbUpdateException(
		DbUpdateException ex, 
		CancellationToken cancellationToken = default)
	{
		if (ex?.InnerException is PostgresException postgresEx)
			throw postgresEx.SqlState switch
			{
				PostgresErrorCodes.ForeignKeyViolation => new ApplicationExceptionBase(
					$"Заданы некорректные идентификаторы для внешних ключей сущности: {postgresEx.Detail}", ex),
				PostgresErrorCodes.UniqueViolation => new DuplicateUniqueKeyException(ex),
				_ => ex,
			};
		throw ex ?? throw new ArgumentNullException(nameof(ex));
	}

	private static void SoftDeleted(EntityEntry entityEntry)
	{
		if (entityEntry?.Entity is not null
			&& entityEntry.Entity is ISoftDeletable softDeletable)
		{
			if (softDeletable.IsDeleted)
			{
				entityEntry.State = EntityState.Deleted;
				return;
			}

			softDeletable.IsDeleted = true;
			entityEntry.State = EntityState.Modified;
		}
	}

	private void OnSave(EntityEntry entityEntry)
	{
		if (entityEntry.State != EntityState.Unchanged)
		{
			UpdateTimestamp(entityEntry);
			SetModifiedUser(entityEntry);
		}

		if (entityEntry.State == EntityState.Deleted)
			SoftDeleted(entityEntry);
	}

	private void UpdateTimestamp(EntityEntry entityEntry)
	{
		var entity = entityEntry.Entity;

		if (entity is not EntityBase table) 
			return;
		
		table.ModifiedOn = _dateTimeProvider.UtcNow;

		if (entityEntry.State == EntityState.Added && table.CreatedOn == DateTime.MinValue)
			table.CreatedOn = _dateTimeProvider.UtcNow;
	}
	
	private void SetModifiedUser(EntityEntry entityEntry)
	{
		if (entityEntry?.Entity == null
		    || entityEntry.State == EntityState.Unchanged
		    || entityEntry.Entity is not IUserTrackable userTrackable) return;
		
		userTrackable.ModifiedByUserId = _userContext.CurrentUserId;

		if (entityEntry.State != EntityState.Added) return;
		
		if (IsInMemory && userTrackable.CreatedByUserId != default)
			return;
		
		userTrackable.CreatedByUserId = _userContext.CurrentUserId;
	}
}