using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PostFinancialTransaction;
using MediatR;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.PostFinancialTransaction;

/// <summary>
/// Команда на создание финансовой транзакции
/// </summary>
public class PostFinancialTransactionCommand : PostFinancialTransactionRequest, IRequest<PostFinancialTransactionResponse>
{
}