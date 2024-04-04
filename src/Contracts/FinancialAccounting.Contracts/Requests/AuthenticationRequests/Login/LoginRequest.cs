namespace FinancialAccounting.Contracts.Requests.AuthenticationRequests.Login;

/// <summary>
/// Запрос на логин
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;
}