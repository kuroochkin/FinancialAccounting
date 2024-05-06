using FinancialAccounting.Domain.Enums;

namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PutFinancialTransaction;

/// <summary>
/// Запрос на обновление финансовой транзакции
/// </summary>
public class PutFinancialTransactionRequest
{
    /// <summary>
    /// Идентификатор финансовой транзакции
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Сумма операции
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Тип операции (доход/расход)
    /// </summary>
    public TransactionType Type { get; set; }
    
    /// <summary>
    /// Дата, установленная пользователем
    /// </summary>
    public DateTime ActualDate { get; set; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }
    
    /// <summary>
    /// Идентификатор банковского счета
    /// </summary>
    public Guid? BankAccountId { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}