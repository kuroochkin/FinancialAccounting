using FinancialAccounting.Contracts.Requests.TransferRequests.PostTransfer;
using MediatR;

namespace FinancialAccounting.Core.Requests.TransferRequests.PostTransfer;

/// <summary>
/// Команда на создание перевода
/// </summary>
public class PostTransferCommand : PostTransferRequest, IRequest<PostTransferResponse>
{
}