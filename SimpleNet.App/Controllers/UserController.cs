using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleNet.Application.Users.Queries.GetMyUserPage;
using SimpleNet.Application.Users.Queries.GetUserPage;
using SimpleNet.Application.Users.Queries.GetUsersByName;
using SimpleNet.ViewModels.User;


namespace SimpleNet.Controllers;

[Route ("user")]
public class UserController : SNetController
{
    [HttpGet("mypage")]
    public async Task<IActionResult> MyPage(   
        CancellationToken cancellationToken
    )
    {
        var query = new GetMyUserPageQuery(UserId);
        var result = await Mediator.Send(query, cancellationToken);

        var vm = (MyUserPageVm)result.Value;

        if (result.IsSuccess)
        {
            return View(vm);
        }
        return NotFound(result.Errors);
    }

    [HttpGet("{visitUserId}")]
    public async Task<IActionResult> UserPage(
        string visitUserId
        )
    {
        // Todo: Make another view and vm
        var query = new GetUserPageQuery(UserId,visitUserId);
        var result = await Mediator.Send(query);
        
        var vm = result.Value;

        if (result.IsSuccess)
        {
            return View(vm);
        }

        return BadRequest(result.Errors);
    }
}