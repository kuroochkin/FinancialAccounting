using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PutFinancialTransaction;
using MediatR;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.PutFinancialTransaction;

/// <summary>
/// Команда на обновление участия в мероприятии
/// </summary>
public class PutFinancialTransactionCommand : PutFinancialTransactionRequest, IRequest<PutFinancialTransactionResponse>
{
}