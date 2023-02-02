using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleNet.Controllers;

public abstract class SNetController : Controller
{
    private IMediator _mediator { get; set; }
    
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    internal Guid UserId => !User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}