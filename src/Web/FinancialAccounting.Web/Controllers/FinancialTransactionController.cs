using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionById;
using FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinancialAccounting.Web.Controllers;

/// <summary>
/// Контроллер для финансовых транзакция пользователя
/// </summary>
public class FinancialTransactionController : ApiControllerBase
{
    /// <summary>
    /// Получить финансовую транзакцию
    /// </summary>
    /// <param name="id">Идентификатор финансовой транзакции</param>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Объект перевода</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetFinancialTransactionByIdResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<GetFinancialTransactionByIdResponse> GetFinancialTransactionByIdAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken) =>
        await mediator.Send(
            new GetFinancialTransactionByIdQuery()
            {
                Id = id
            },
            cancellationToken);
}