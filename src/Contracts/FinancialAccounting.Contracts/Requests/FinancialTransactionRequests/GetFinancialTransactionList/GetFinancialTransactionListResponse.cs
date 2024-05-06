namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;

/// <summary>
/// Ответ на запрос списка финансовых транзакций
/// </summary>
public class GetFinancialTransactionListResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="financialTransactions">Список финансовых транзакций</param>
    public GetFinancialTransactionListResponse(
        Guid userId,
        List<GetFinancialTransactionListResponseItem> financialTransactions)
    {
        UserId = userId;
        FinancialTransactions = financialTransactions;
    }
    
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Список финансовых транзакций
    /// </summary>
    public List<GetFinancialTransactionListResponseItem>? FinancialTransactions { get; set; }
}