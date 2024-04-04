namespace FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;

/// <summary>
/// Запрос на регистрацию пользователя
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string Phone { get; set; } = default!;
}