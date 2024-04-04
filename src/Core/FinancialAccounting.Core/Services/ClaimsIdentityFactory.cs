using System.Security.Claims;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Core.Common;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Core.Services;

/// <summary>
/// Фабрика ClaimsPrincipal для пользователей
/// </summary>
public class ClaimsIdentityFactory : IClaimsIdentityFactory
{
    /// <inheritdoc/>
    public ClaimsIdentity CreateClaimsIdentity(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>
        {
            new(ClaimNames.UserId, user.Id.ToString(), ClaimValueTypes.String),
            new(ClaimNames.Login, user.Login ?? throw new InvalidOperationException(), ClaimValueTypes.String),
        };

        return new ClaimsIdentity(claims);
    }
}