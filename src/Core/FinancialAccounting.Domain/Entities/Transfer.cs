using FinancialAccounting.Domain.Exceptions;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Сущность финансового перевода
/// </summary>
public class Transfer : EntityBase
{
    /// <summary>
    /// Поле для <see cref="_fromBankAccount"/>
    /// </summary>
    public const string FromBankAccountField = nameof(_fromBankAccount);

    /// <summary>
    /// Поле для <see cref="_toBankAccount"/>
    /// </summary>
    public const string ToBankAccountField = nameof(_toBankAccount);
    
    private decimal _amount;

    private BankAccount? _fromBankAccount;
    private BankAccount? _toBankAccount;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="fromBankAccount">Счет, с которого совершен перевод</param>
    /// <param name="toBankAccount">Счет, на который совершен перевод</param>
    public Transfer(
        decimal amount,
        BankAccount? fromBankAccount,
        BankAccount? toBankAccount)
    {
        Amount = amount;
        FromBankAccount = fromBankAccount;
        ToBankAccount = toBankAccount;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected Transfer()
    {
    }
    
    /// <summary>
    /// Сумма перевода
    /// </summary>
    public decimal Amount
    {
        get => _amount;
        private set => _amount = value is >= 0
            ? value
            : throw new ApplicationExceptionBase("Введена некорректная сумма");
    }
    
    /// <summary>
    /// Идентификатор счета, с которого был отправлен перевод
    /// </summary>
    public Guid FromBankAccountId { get; private set; }

    /// <summary>
    /// Идентификатор счета, на который был отправлен перевод
    /// </summary>
    public Guid ToBankAccountId { get; private set; }

    /// <summary>
    /// Счет, с которого был совершен перевод
    /// </summary>
    public BankAccount? FromBankAccount
    {
        get => _fromBankAccount;
        set
        {
            _fromBankAccount = value
                ?? throw new RequiredFieldNotSpecifiedException("Счет");
            FromBankAccountId = value.Id;
        }
    }
    
    /// <summary>
    /// Счет, на который был отправлен перевод
    /// </summary>
    public BankAccount? ToBankAccount
    {
        get => _toBankAccount;
        set
        {
            _toBankAccount = value
                ?? throw new RequiredFieldNotSpecifiedException("Счет");
            ToBankAccountId = value.Id;
        }
    }
}