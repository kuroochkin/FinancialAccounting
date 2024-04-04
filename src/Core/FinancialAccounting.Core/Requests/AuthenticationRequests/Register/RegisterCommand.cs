using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;
using MediatR;

namespace FinancialAccounting.Core.Requests.AuthenticationRequests.Register;

/// <summary>
/// Команда на регистрацию студента
/// </summary>
public class RegisterCommand : RegisterRequest, IRequest<RegisterResponse>
{
}