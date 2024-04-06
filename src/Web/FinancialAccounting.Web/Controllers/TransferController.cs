using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Login;
using FinancialAccounting.Contracts.Requests.TransferRequests.GetTransferById;
using FinancialAccounting.Contracts.Requests.TransferRequests.PostTransfer;
using FinancialAccounting.Core.Requests.TransferRequests.GetTransferById;
using FinancialAccounting.Core.Requests.TransferRequests.PostTransfer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinancialAccounting.Web.Controllers;

/// <summary>
/// Контроллер для банковских переводов
/// </summary>
public class TransferController : ApiControllerBase
{
    /// <summary>
    /// Получить перевод
    /// </summary>
    /// <param name="id">Идентификатор перевода</param>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Объект перевода</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetTransferByIdResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<GetTransferByIdResponse> GetTransferByIdAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(
            new GetTransferByIdQuery()
            {
                Id = id
            },
            cancellationToken);
    
    /// <summary>
    /// Создать перевод
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Объект запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostTransferResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<PostTransferResponse> CreateTransferAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostTransferRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new PostTransferCommand
            {
                Comment = request.Comment,
                Amount = request.Amount,
                FromBankAccountId = request.FromBankAccountId,
                ToBankAccountId = request.ToBankAccountId
            },
            cancellationToken);
}