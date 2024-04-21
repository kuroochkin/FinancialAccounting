namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionById;

/// <summary>
/// Ответ банковского счета для <see cref="GetFinancialTransactionByIdResponse"/>
/// </summary>
public class GetFinancialTransactionByIdResponseBankAccount
{
    /// <summary>
    /// Идентификатор банковского счета
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название банковского счета
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Текущий баланс
    /// </summary>
    public decimal Balance { get; set; }
}