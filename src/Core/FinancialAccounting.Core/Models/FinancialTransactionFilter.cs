using FinancialAccounting.Domain.Enums;

namespace FinancialAccounting.Core.Models;

/// <summary>
/// Фильтр для запроса <see cref="GetFinancialTransactionListQuery"/>
/// </summary>
public class FinancialTransactionFilter
{
    /// <summary>
    /// Тип трназакции
    /// </summary>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Идентификатор счета
    /// </summary>
    public Guid? BankAccountId { get; set; }
    
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Временной промежуток
    /// </summary>
    public DateSpan? DateSpan { get; set; }
}