using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = FinancialAccounting.Domain.Entities.File;

namespace FinancialAccounting.Core.Abstractions;

/// <summary>
/// Контекст EF Core для приложения
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Банковский счет
    /// </summary>
    public DbSet<BankAccount> BankAccounts { get; set; }
    
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Финансовые операции
    /// </summary>
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    
    /// <summary>
    /// Переводы
    /// </summary>
    public DbSet<Transfer> Transfers { get; set; }
    
    /// <summary>
    /// Фотографии
    /// </summary>
    public DbSet<Photo> Photos { get; set; }
    
    /// <summary>
    /// Файлы
    /// </summary>
    public DbSet<File> Files { get; set; }

    /// <summary>
    /// Категории
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// БД в памяти
    /// </summary>
    bool IsInMemory { get; }

    /// <summary>
    /// Сохранить изменения в БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество обновленных записей</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}