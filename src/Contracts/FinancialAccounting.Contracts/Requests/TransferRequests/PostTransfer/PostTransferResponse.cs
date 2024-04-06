namespace FinancialAccounting.Contracts.Requests.TransferRequests.PostTransfer;

/// <summary>
/// Ответ на запрос создания перевода
/// </summary>
public class PostTransferResponse
{
    /// <summary>
    /// Идентификатор перевода
    /// </summary>
    public Guid Id { get; set; }
}