namespace FinancialAccounting.Core.Abstractions;

/// <summary>
/// Сервис добавления данных в БД
/// </summary>
public interface IDbSeeder
{
    /// <summary>
    /// Добавить данные в БД
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>-</returns>
    public Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default);
}