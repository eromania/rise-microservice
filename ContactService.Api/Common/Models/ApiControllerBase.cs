using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Api.Common.Models;

//abstract class for api controllers
[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
}