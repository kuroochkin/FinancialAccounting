using System.Security.Claims;

namespace FinancialAccounting.Core.Common;

/// <summary>
/// Названия клеймов
/// </summary>
public static class ClaimNames
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public const string UserId = ClaimTypes.NameIdentifier;

    /// <summary>
    /// Логин
    /// </summary>
    public const string Login = "Login";
}