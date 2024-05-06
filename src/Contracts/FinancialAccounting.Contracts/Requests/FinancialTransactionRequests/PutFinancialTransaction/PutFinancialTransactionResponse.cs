using FinancialAccounting.Domain.Enums;

namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PutFinancialTransaction;

/// <summary>
/// Ответ на запрос обновления транзакции
/// </summary>
public class PutFinancialTransactionResponse
{
    /// <summary>
    /// Идентификатор финансовой транзакции
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Тип операции
    /// </summary>
    public TransactionType Type { get; set; }
}