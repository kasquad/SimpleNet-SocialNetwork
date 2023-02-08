using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleNet.Application.Friend.Commands.AcceptFriend;
using SimpleNet.Application.Friend.Commands.AddFriend;
using SimpleNet.Application.Friend.Queries.GetFriendRequests;
using SimpleNet.Application.Friend.Queries.GetFriendsList;
using SimpleNet.Application.Users.Queries.GetUsersByName;
using SimpleNet.ViewModels.Friends;

namespace SimpleNet.Controllers;

[Route("friends")]
public class FriendsController : SNetController
{
    [HttpGet("")]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var query = new GetFriendsQuery(UserId);
        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("requests")]
    public async Task<IActionResult> Requests(
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var query = new GetFriendRequestsQuery(UserId);
        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return View(result.Value);
        }
        return BadRequest(result.Errors);
    }
    
    
    [HttpGet("search/{name?}")]
    public async Task<IActionResult> SearchFriends(
        string name,
        CancellationToken cancellationToken)
    {
        if (name.IsNullOrEmpty())
        {
            return View(new SearchedUsersVm());
        }

        var query = new GetUsersByNameQuery(UserId, name);
        var result = await Mediator.Send(query, cancellationToken);
        
        return View(result.Value);
    }

    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddFriend(
        [FromForm] AddFriendVm vm,
        CancellationToken cancellationToken
    )
    {
        var command = new AddFriendCommand(UserId,vm.AcceptingUserId);

        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        return BadRequest(result.Errors);
    }
    
    [HttpPost("accept")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptFriend(
        [FromForm] string acceptingUserId,
        CancellationToken cancellationToken
    )
    {
        var command = new AcceptFriendCommand(UserId,acceptingUserId);
        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        return BadRequest(result.Errors);
    }
}