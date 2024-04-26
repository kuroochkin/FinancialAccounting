namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;

/// <summary>
/// Ответ категории для <see cref="GetFinancialTransactionListResponseItem"/>
/// </summary>
public class GetFinancialTransactionListResponseItemCategory
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string? Title { get; set; }
}