using FinancialAccounting.Contracts.Requests.BankAccountRequests.PostBankAccount;
using FinancialAccounting.Core.Requests.BankAccountRequests.PostBankAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinancialAccounting.Web.Controllers;

/// <summary>
/// Контроллер для банковских счетов пользователя
/// </summary>
public class BankAccountController : ApiControllerBase
{
    /// <summary>
    /// Создать банковский счет
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Объект запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostBankAccountResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<PostBankAccountResponse> CreateTransferAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostBankAccountRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new PostBankAccountCommand
            {
                Balance = request.Balance,
                Title = request.Title
            },
            cancellationToken);
}