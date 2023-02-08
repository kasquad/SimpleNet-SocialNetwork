using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNet.AppConstants;
using SimpleNet.Application.Helpers;
using SimpleNet.Application.Users.Commands.Login;
using SimpleNet.Application.Users.Commands.Register;
using SimpleNet.ViewModels.Auth;

namespace SimpleNet.Controllers;

[Route("auth")]
public class AuthController : SNetController
{
    [HttpGet("register")]
    public Task<IActionResult> Register()
    {
        return Task.FromResult<IActionResult>(View());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromForm] RegisterVm vm,
        CancellationToken cancellationToken
    )
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var command = (RegisterAppRequest)vm;

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return RedirectToAction("Login");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpGet("login")]
    public  Task<IActionResult> Login()
    {
        return Task.FromResult<IActionResult>(View());
    }
    
    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromForm] LoginVm vm,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var command = (LoginAppRequest)vm;
        
        Result<string> result = await Mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            Response.Cookies.Append(SettingsConstants.AuthCookieName, result.Value);
            
            return Redirect(RoutesConstants.MyPageRoutePath);
        }
        return BadRequest(result.Errors);
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            Response.Cookies.Delete(SettingsConstants.AuthCookieName);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
        return RedirectToAction("Login");
    }
}