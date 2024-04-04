using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Login;
using MediatR;

namespace FinancialAccounting.Core.Requests.AuthenticationRequests.Login;

/// <summary>
/// Запрос на логин
/// </summary>
public class LoginQuery : LoginRequest, IRequest<LoginResponse>
{
}