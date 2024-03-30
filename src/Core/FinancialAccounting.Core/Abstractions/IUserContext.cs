namespace FinancialAccounting.Core.Abstractions;

/// <summary>
/// Контекст текущего пользователя
/// </summary>
public interface IUserContext
{
    /// <summary>
    /// ИД текущего пользователя
    /// </summary>
    Guid CurrentUserId { get; }

    /// <summary>
    /// Название роли текущего пользователя
    /// </summary>
    string CurrentUserRoleName { get; }
}