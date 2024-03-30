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
    
    private User? _user;
    private List<FinancialTransaction>? _financialTransactions;
    private List<Transfer>? _outTransfers;
    private List<Transfer>? _inTransfers;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="balance">Баланс счета</param>
    /// <param name="user">Пользователь-владелец счета</param>
    public BankAccount(
        decimal balance,
        User? user)
    {
        Balance = balance;
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
}