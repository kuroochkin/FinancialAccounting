namespace FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;

/// <summary>
/// Ответ на команду <see cref="RegisterRequest"/>
/// </summary>
public class RegisterResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <param name="token">Токен авторизации</param>
    public RegisterResponse(
        Guid userId,
        string token)
    {
        UserId = userId;
        Token = token;
    }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string Token { get; }
}