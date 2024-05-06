namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionById;

/// <summary>
/// Ответ для категории для <see cref="GetFinancialTransactionByIdResponse"/>
/// </summary>
public class GetFinancialTransactionByIdCategory
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