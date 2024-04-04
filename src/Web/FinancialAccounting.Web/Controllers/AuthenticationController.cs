using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Login;
using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;
using FinancialAccounting.Core.Requests.AuthenticationRequests.Login;
using FinancialAccounting.Core.Requests.AuthenticationRequests.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinancialAccounting.Web.Controllers;

/// <summary>
/// Контроллер для аутентификации
/// </summary>
[AllowAnonymous]
public class AuthenticationController : ApiControllerBase
{
    /// <summary>
    /// Логин
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Объект логина</returns>
    [HttpPost("login")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(LoginResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<LoginResponse> LoginAsync(
        [FromServices] IMediator mediator,
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await mediator.Send(
            new LoginQuery
            {
                Login = request.Login,
                Password = request.Password,
            },
            cancellationToken);
    }

    /// <summary>
    /// Зарегистрироваться студентом
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного пользователя</returns>
    [HttpPost("register")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(RegisterResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<RegisterResponse> RegisterAsync(
        [FromServices] IMediator mediator,
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await mediator.Send(
            new RegisterCommand
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Phone = request.Phone,
            },
            cancellationToken);
    }
}