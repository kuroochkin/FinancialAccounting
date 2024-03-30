namespace FinancialAccounting.Dara.PostgreSql;

/// <summary>
/// Конфигурация проекта
/// </summary>
public class PostgresDbOptions
{
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    public string? ConnectionString { get; set; } = default!;
}