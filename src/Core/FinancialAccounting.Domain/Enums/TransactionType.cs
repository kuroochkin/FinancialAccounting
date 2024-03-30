namespace FinancialAccounting.Domain.Enums;

/// <summary>
/// Тип финансовой операции (расход/доход)
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Неопределенный тип
    /// </summary>
    Unspecified = 0,
    
    /// <summary>
    /// Расход
    /// </summary>
    Consumption = 1,

    /// <summary>
    /// Доход
    /// </summary>
    Income = 2,
}