using System.Security.Claims;
using FinancialAccounting.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace FinancialAccounting.Core.Abstractions;

/// <summary>
/// Сервис работы с токенами
/// </summary>
public interface ITokenAuthenticationService
{
    /// <summary>
    /// Создать токен для пользователя с клаймами
    /// </summary>
    /// <param name="identity">Пользователь с клаймами.</param>
    /// <param name="tokenType">Тип токена.</param>
    /// <returns>Токен для пользователя с клаймами.</returns>
    string CreateToken(ClaimsIdentity identity, TokenTypes tokenType);

    /// <summary>
    /// Получить параметры валидации токенов
    /// </summary>
    /// <param name="tokenType">Тип токенов.</param>
    /// <returns>Параметры валидации токенов.</returns>
    TokenValidationParameters GetTokenValidationParameters(TokenTypes tokenType);

    /// <summary>
    /// Вид авторизации по токену (Bearer)
    /// </summary>
    string AuthTokenType { get; }

    /// <summary>
    /// УЦ токенов
    /// </summary>
    string Authority { get; }

    /// <summary>
    /// Аудитория токенов. Для каждого типа токенов задаем разную аудиторию.
    /// </summary>
    string Audience { get; }

    /// <summary>
    /// Выпускатель токенов
    /// </summary>
    string ClaimsIssuer { get; }
}