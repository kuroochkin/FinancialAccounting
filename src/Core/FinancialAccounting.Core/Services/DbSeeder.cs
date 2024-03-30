using FinancialAccounting.Core.Abstractions;

namespace FinancialAccounting.Core.Services;

/// <summary>
/// Сервис добавления данных в БД
/// </summary>
public class DbSeeder : IDbSeeder
{
    /// <inheritdoc/>
    public async Task SeedAsync(
        IDbContext dbContext, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}