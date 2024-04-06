using FinancialAccounting.Contracts.Requests.BankAccountRequests.PostBankAccount;
using MediatR;

namespace FinancialAccounting.Core.Requests.BankAccountRequests.PostBankAccount;

/// <summary>
/// Команда на создание банковского счета
/// </summary>
public class PostBankAccountCommand : PostBankAccountRequest, IRequest<PostBankAccountResponse>
{
}