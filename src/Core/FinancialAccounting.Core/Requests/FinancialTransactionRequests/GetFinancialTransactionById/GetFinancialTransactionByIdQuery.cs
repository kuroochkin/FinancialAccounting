using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionById;
using MediatR;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionById;

/// <summary>
/// Запрос на получение финансовой транзакции
/// </summary>
public class GetFinancialTransactionByIdQuery : IRequest<GetFinancialTransactionByIdResponse>
{
    /// <summary>
    /// Идентификатор финансовой транзакции
    /// </summary>
    public Guid Id { get; set; }
}