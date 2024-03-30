using FinancialAccounting.Core.Abstractions;

namespace FinancialAccounting.Web.Authentication;

/// <summary>
/// Точка входа сервисов аутентификации для приложения
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавить контекст пользователя в службы приложения
    /// </summary>
    /// <param name="services">Службы приложения</param>
    /// <returns>Службы приложения с контекстом текущего пользователя</returns>
    public static IServiceCollection AddUserContext(this IServiceCollection services) =>
        services
            .AddScoped<IUserContext, UserContext>();
}