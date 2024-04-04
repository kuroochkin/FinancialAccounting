using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAccounting.Web.Controllers;

/// <summary>
/// Базовый API-контроллер
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
}