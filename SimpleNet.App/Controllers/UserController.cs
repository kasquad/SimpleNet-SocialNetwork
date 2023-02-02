using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNet.Application.Users.Queries.GetUserPage;
using SimpleNet.DataModels.User;
using SimpleNet.ViewModels.User;


namespace SimpleNet.Controllers;

[Route ("user")]
public class UserController : SNetController
{

    [HttpGet("mypage")]
    public async Task<IActionResult> MyPage()
    {
        var query = new GetUserPageQuery(UserId);
        var result = await Mediator.Send(query);

        var dto = (MyUserPageVm)result.Value;

        if (result.IsSuccess)
        {
            return View(dto);
        }
        return NotFound(result.Errors);
    }
}