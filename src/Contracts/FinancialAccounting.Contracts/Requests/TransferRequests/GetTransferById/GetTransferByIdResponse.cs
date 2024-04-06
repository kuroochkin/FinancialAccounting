namespace FinancialAccounting.Contracts.Requests.TransferRequests.GetTransferById;

/// <summary>
/// Ответ на запрос получения банковского перевода по Id
/// </summary>
public class GetTransferByIdResponse
{
    /// <summary>
    /// Идентификатор банковского перевода
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Сумма перевода
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Дата перевода
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Счет, с которого был совершен перевод
    /// </summary>
    public BankAccountResponse? FromBankAccount { get; set; }

    /// <summary>
    /// Счет, на который был совершен перевод
    /// </summary>
    public BankAccountResponse? ToBankAccount { get; set; }
}