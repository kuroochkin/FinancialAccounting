namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;

/// <summary>
/// Ответ банковского счета для <see cref="GetFinancialTransactionListResponseItem"/>
/// </summary>
public class GetFinancialTransactionListResponseItemBankAccount
{
    /// <summary>
    /// Идентификатор банковского счета
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название банковского счета
    /// </summary>
    public string? Title { get; set; }
}