using FinancialAccounting.Domain.Enums;
using FinancialAccounting.Domain.Exceptions;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Сущность финансовой операции
/// </summary>
public class FinancialTransaction : EntityBase
{
    /// <summary>
    /// Поле для <see cref="_bankAccount"/>
    /// </summary>
    public const string BankAccountField = nameof(_bankAccount);
    
    /// <summary>
    /// Поле для <see cref="_category"/>
    /// </summary>
    public const string CategoryField = nameof(_category);
    
    /// <summary>
    /// Поле для <see cref="_photos"/>
    /// </summary>
    public const string PhotosField = nameof(_photos);
    
    private const int MAXCOUNTPHOTOS = 3;

    private decimal _amount;
    private DateTime _actualDate;
    private TransactionType _type;

    private BankAccount? _bankAccount;
    private Category? _category;
    private List<Photo> _photos;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="amount">Сумма операции</param>
    /// <param name="type">Тип операции</param>
    /// <param name="category">Категория</param>
    /// <param name="bankAccount">Счет, на котором произвелась операция</param>
    /// <param name="userId">Id пользователя</param>
    /// <param name="comment">Комментарий</param>
    /// <param name="actualDate">Фактическая дата операции</param>
    public FinancialTransaction(
        decimal amount,
        TransactionType type,
        Category? category,
        BankAccount? bankAccount,
        Guid userId,
        string? comment = default,
        DateTime actualDate = default)
    {
        Amount = amount;
        Type = type;
        Category = category;
        BankAccount = bankAccount;
        Comment = comment;
        ActualDate = actualDate;
        UserId = userId;
        
        _photos = new List<Photo>();
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected FinancialTransaction()
    {
    }

    /// <summary>
    /// Сумма операции
    /// </summary>
    public decimal Amount
    {
        get => _amount;
        private set => _amount = value is >= 0
            ? value
            : throw new ApplicationExceptionBase("Введена некорректная сумма");
    }

    /// <summary>
    /// Тип операции
    /// </summary>
    public TransactionType Type
    {
        get => _type;
        private set => _type = value == default
            ? throw new RequiredFieldNotSpecifiedException("Тип операции")
            : value;
    }

    /// <summary>
    /// Счет, на котором произвелась операция
    /// </summary>
    public BankAccount? BankAccount
    {
        get => _bankAccount;
        private set
        {
            _bankAccount = value
                ?? throw new RequiredFieldNotSpecifiedException("Счет");
            BankAccountId = value.Id;
        }
    }

    /// <summary>
    /// Категория, под которую попадает операция
    /// </summary>
    public Category? Category
    {
        get => _category;
        private set
        {
            _category = value
                ?? throw new RequiredFieldNotSpecifiedException("Категория");
            CategoryId = value.Id;
        }
    }

    /// <summary>
    /// Фактическая дата совершения операции
    /// </summary>
    public DateTime ActualDate
    {
        get => _actualDate;
        private set => _actualDate = value == default
            ? throw new RequiredFieldNotSpecifiedException("Дата совершения операции")
            : value.ToUniversalTime();
    }

    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; private set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; private set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    public Guid? CategoryId { get; private set; }

    /// <summary>
    /// Счет
    /// </summary>
    public Guid? BankAccountId { get; private set; }

    /// <summary>
    /// Список фото, прикрепленных к операции
    /// </summary>
    public IReadOnlyList<Photo> Photos => _photos;

    public void Upsert(
        decimal amount = default,
        DateTime actualDate = default,
        TransactionType type = default,
        string? comment = default,
        BankAccount? bankAccount = default,
        Category? category = default)
    {
        if (amount != default && Amount != amount)
            Amount = amount;
        if (ActualDate != actualDate.ToUniversalTime())
            ActualDate = actualDate;
        if (Type != type)
            Type = type;
        if (comment != null && Comment != comment)
            Comment = comment;
        if (bankAccount != null && BankAccount != bankAccount)
            BankAccount = bankAccount;
        if (category != null && Category != category)
            Category = category;
    }

    public void AddPhotos(List<Photo> photos)
    {
        ArgumentNullException.ThrowIfNull(photos);
        
        if (_photos.Count + photos.Count >= MAXCOUNTPHOTOS)
            throw new ApplicationExceptionBase("У вас не может быть более 3 фотографий");
        
        _photos.AddRange(photos);
    }
}