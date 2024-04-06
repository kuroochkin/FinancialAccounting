namespace FinancialAccounting.Contracts.Requests.TransferRequests.PostTransfer;

/// <summary>
/// Запрос на создание перевода
/// </summary>
public class PostTransferRequest
{
    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Сумма перевода
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Идентификатор счета, с которого совершается перевод
    /// </summary>
    public Guid FromBankAccountId { get; set; }

    /// <summary>
    /// Идентификатор счета, на который совершается перевод
    /// </summary>
    public Guid ToBankAccountId { get; set; }
}