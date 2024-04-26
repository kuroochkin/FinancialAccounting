using FinancialAccounting.Domain.Enums;

namespace FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;

/// <summary>
/// Элемент списка для <see cref="GetFinancialTransactionListResponse"/>
/// </summary>
public class GetFinancialTransactionListResponseItem
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
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }
    
    /// <summary>
    /// Дата, установленная пользователем
    /// </summary>
    public DateTime ActualDate { get; set; }
    
    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedOn { get; set; }
    
    /// <summary>
    /// Банковский счет, на которм была совершена транзакция
    /// </summary>
    public GetFinancialTransactionListResponseItemBankAccount? BankAccount { get; set; }
    
    /// <summary>
    /// Категория, в которой совершена транзакция
    /// </summary>
    public GetFinancialTransactionListResponseItemCategory? Category { get; set; }
}