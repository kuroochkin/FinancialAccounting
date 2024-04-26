using FinancialAccounting.Domain.Enums;
using FinancialAccounting.Domain.Exceptions;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Сущность счета пользователя
/// </summary>
public class BankAccount : EntityBase
{
    /// <summary>
    /// Поле для <see cref="_user"/>
    /// </summary>
    public const string UserField = nameof(_user);

    /// <summary>
    /// Поле для <see cref="_financialTransactions"/>
    /// </summary>
    public const string FinancialTransactionsField = nameof(_financialTransactions);

    /// <summary>
    /// Поле для <see cref="_outTransfers"/>
    /// </summary>
    public const string OutTransfersField = nameof(_outTransfers);

    /// <summary>
    /// Поле для <see cref="_inTransfers"/>
    /// </summary>
    public const string InTransfersField = nameof(_inTransfers);
    
    private decimal _balance;
    private string _title;
    
    private User? _user;
    private List<FinancialTransaction>? _financialTransactions;
    private List<Transfer>? _outTransfers;
    private List<Transfer>? _inTransfers;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="balance">Баланс счета</param>
    /// <param name="title">Название счета</param>
    /// <param name="user">Пользователь-владелец счета</param>
    public BankAccount(
        decimal balance,
        string title,
        User? user)
    {
        Balance = balance;
        Title = title;
        User = user;

        _financialTransactions = new List<FinancialTransaction>();
        _outTransfers = new List<Transfer>();
        _inTransfers = new List<Transfer>();
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected BankAccount()
    {
    }

    /// <summary>
    /// Идентификатор пользователя-владельца счета
    /// </summary>
    public Guid UserId { get; private set; }
    
    /// <summary>
    /// Текущий баланс счета
    /// </summary>
    public decimal Balance
    {
        get => _balance;
        private set => _balance = value is >= 0
            ? value
            : throw new ApplicationExceptionBase("Введена некорректная сумма");
    }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string? Title
    {
        get => _title;
        private set => _title = value
            ?? throw new RequiredFieldNotSpecifiedException("Логин");
    }

    /// <summary>
    /// Пользователь, владелец счета
    /// </summary>
    /// <exception cref="RequiredFieldNotSpecifiedException"></exception>
    public User? User
    {
        get => _user;
        private set
        {
            _user = value
                ?? throw new RequiredFieldNotSpecifiedException("Пользователь");
            UserId = value.Id;
        }
    }
    
    /// <summary>
    /// Доходы/расходы на счете
    /// </summary>
    public IReadOnlyList<FinancialTransaction>? FinancialTransactions => _financialTransactions;

    /// <summary>
    /// Переводы со счета
    /// </summary>
    public IReadOnlyList<Transfer>? OutTransfers => _outTransfers;

    /// <summary>
    /// Переводы на счет
    /// </summary>
    public IReadOnlyList<Transfer>? InTransfers => _inTransfers;

    /// <summary>
    /// Добавление сущности перевода в список
    /// </summary>
    /// <param name="transfer">Перевод</param>
    public void AddTransfer(Transfer transfer)
    {
        if (transfer.FromBankAccountId == Id || transfer.ToBankAccountId == Id)
        {
            if (transfer.FromBankAccountId == Id)
                _outTransfers?.Add(transfer);
            else if (transfer.ToBankAccountId == Id)
                _inTransfers?.Add(transfer);
            
            ChangeBalance(transfer);
        }
        else
            throw new ApplicationExceptionBase($"Перевод c Id {transfer.Id} никак не связан с со счетом с Id {Id}");
    }

    private void ChangeBalance(Transfer transfer)
    {
        if (transfer.FromBankAccountId == Id)
        {
            if (_balance < transfer.Amount)
                throw new ApplicationExceptionBase(
                    $"Недостаточно средств на счету с Id {Id} для совершения перевода");

            _balance -= transfer.Amount;
        }
        else if (transfer.ToBankAccountId == Id)
        {
            _balance += transfer.Amount;
        }
        else
        {
            throw new ApplicationExceptionBase($"Перевод с Id {transfer.Id} не связан со счетом с Id {Id}");
        }
    }

    /// <summary>
    /// Добавление финансовой транзакции в список
    /// </summary>
    /// <param name="financialTransaction">Финансовая транзакция</param>
    public void AddFinancialTransaction(FinancialTransaction financialTransaction)
    {
        switch (financialTransaction.Type)
        {
            case TransactionType.Consumption:
                Balance -= financialTransaction.Amount;
                break;
            case TransactionType.Income:
                Balance += financialTransaction.Amount;
                break;
            default:
                throw new ArgumentException("Невалидное значение типа финансовой транзакции");
        }

        _financialTransactions?.Add(financialTransaction);
    }

    /// <summary>
    /// Проверка на принадлежность банковского счета пользователю
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public void CheckUserInDesiredBankAccount(Guid userId)
    {
        if (UserId != userId)
            throw new ApplicationException(
                $"Банковский счет с Id {Id} не принадлежит пользователю с Id {userId}");
    }
}