namespace FinancialAccounting.Contracts.Requests.BankAccountRequests.PostBankAccount;

/// <summary>
/// Запрос на создание банковского счета
/// </summary>
public class PostBankAccountRequest
{
    /// <summary>
    /// Баланс счета
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Название счета
    /// </summary>
    public string? Title { get; set; }
}