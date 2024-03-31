using MediatR;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Core.Services;
using FinancialAccounting.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialAccounting.Core;

/// <summary>
/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавить службы проекта с логикой
    /// </summary>
    /// <param name="services">Коллекция служб</param>
    /// <returns>Обновленная коллекция служб</returns>
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Entry));
        services.AddMediatR(typeof(EntityBase));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDbSeeder, DbSeeder>();
        services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
        services.AddScoped<IClaimsIdentityFactory, ClaimsIdentityFactory>();

        return services;
    }
}