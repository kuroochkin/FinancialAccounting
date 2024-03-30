using FinancialAccounting.Domain.Exceptions;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Класс пользователя
/// </summary>
public class User : EntityBase
{
    /// <summary>
    /// Поле для <see cref="_bankAccounts"/>
    /// </summary>
    public const string BankAccountsField = nameof(_bankAccounts);
    
    private string _login;
    private string _email;
    private string _passwordHash;

    private List<BankAccount> _bankAccounts;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="login">Логин</param>
    /// <param name="passwordHash">Пароль</param>
    /// <param name="email">E-mail</param>
    /// <param name="phone">Номер телефона</param>
    public User(
        string login,
        string passwordHash,
        string email,
        string? phone = default)
    {
        Login = login;
        PasswordHash = passwordHash;
        Email = email;
        Phone = phone;

        _bankAccounts = new List<BankAccount>();
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    protected User()
    {
    }

    /// <summary>
    /// Логин
    /// </summary>
    public string? Login
    {
        get => _login;
        private set => _login = value
                ?? throw new RequiredFieldNotSpecifiedException("Логин");
    }

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string? PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = value
                ?? throw new RequiredFieldNotSpecifiedException("Хеш пароля");
    }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email
    {
        get => _email;
        private set => _email = value
                ?? throw new RequiredFieldNotSpecifiedException("Электронная почта");
    }

    /// <summary>
    /// Телефон
    /// </summary>
    public string? Phone { get; set; }

    public IReadOnlyList<BankAccount> BankAccounts => _bankAccounts;
    
    /// <summary>
    /// Обновить контактную информацию пользователя
    /// </summary>
    /// <param name="login">Логин</param>
    /// <param name="email">E-mail</param>
    /// <param name="phone">Телефон</param>
    public void UpsertContactInformation(
        string? login = default,
        string? email = default,
        string? phone = default)
    {
        if (login != null && Login != login)
            Login = login;
        if (email != null && Email != email)
            Email = email;
        if (phone != null && Phone != phone)
            Phone = phone;
    }
}