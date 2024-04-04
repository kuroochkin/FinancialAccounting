using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;
using MediatR;

namespace FinancialAccounting.Core.Requests.AuthenticationRequests.Register;

/// <summary>
/// Команда на регистрацию пользователя
/// </summary>
public class RegisterCommand : RegisterRequest, IRequest<RegisterResponse>
{
}