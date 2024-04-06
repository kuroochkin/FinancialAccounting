using FinancialAccounting.Contracts.Requests.TransferRequests.GetTransferById;
using MediatR;

namespace FinancialAccounting.Core.Requests.TransferRequests.GetTransferById;

/// <summary>
/// Запрос на получение перевода
/// </summary>
public class GetTransferByIdQuery : IRequest<GetTransferByIdResponse>
{
    /// <summary>
    /// Идентификатор перевода
    /// </summary>
    public Guid Id { get; set; }
}