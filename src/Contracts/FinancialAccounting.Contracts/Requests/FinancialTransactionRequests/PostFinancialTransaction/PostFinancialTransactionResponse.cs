namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PostFinancialTransaction;

/// <summary>
/// Ответ на запрос создания финансовой транзакции
/// </summary>
public class PostFinancialTransactionResponse
{
    /// <summary>
    /// Идентификатор финансовой транзакции
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Остаток на балансе
    /// </summary>
    public decimal CurrentBalance { get; set; }
}