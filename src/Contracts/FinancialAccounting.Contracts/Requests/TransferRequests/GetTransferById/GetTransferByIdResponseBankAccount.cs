namespace FinancialAccounting.Contracts.Requests.TransferRequests.GetTransferById;

/// <summary>
/// Модель банковского счета для получения перевода
/// </summary>
public class GetTransferByIdResponseBankAccount
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