namespace FinancialAccounting.Contracts.Requests.BankAccountRequests.PostBankAccount;

/// <summary>
/// Ответ на запрос создания банковского счета
/// </summary>
public class PostBankAccountResponse
{
    /// <summary>
    /// Идентификатор банковского счета
    /// </summary>
    public Guid Id { get; set; }
}