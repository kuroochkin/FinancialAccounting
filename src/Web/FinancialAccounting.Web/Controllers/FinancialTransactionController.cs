using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionById;
using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;
using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PostFinancialTransaction;
using FinancialAccounting.Core.Models;
using FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionById;
using FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionList;
using FinancialAccounting.Core.Requests.FinancialTransactionRequests.PostFinancialTransaction;
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

    [HttpPost("list")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetFinancialTransactionListResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<GetFinancialTransactionListResponse> GetFinancialTransactionsAsync(
        [FromBody] FinancialTransactionFilter filter,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new GetFinancialTransactionListQuery
            {
                Filter = filter
            }, 
            cancellationToken);
    
    /// <summary>
    /// Создать финансовую транзакцию
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Объект запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostFinancialTransactionResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<PostFinancialTransactionResponse> CreateFinancialTransactionAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostFinancialTransactionRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new PostFinancialTransactionCommand
            {
                Amount = request.Amount,
                Type = request.Type,
                ActualDate = request.ActualDate,
                Comment = request.Comment,
                BankAccountId = request.BankAccountId,
                CategoryId = request.CategoryId
            },
            cancellationToken);
}