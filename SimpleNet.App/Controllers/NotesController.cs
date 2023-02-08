using Microsoft.AspNetCore.Mvc;
using SimpleNet.AppConstants;
using SimpleNet.Application.Notes.Commands.Create;
using SimpleNet.Application.Notes.Queries.GetFriendsNotes;
using SimpleNet.ViewModels.Notes;

namespace SimpleNet.Controllers;

[Route("notes")]
public class NotesController : SNetController
{
    [HttpGet("")]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken
        )
    {
        var query = new GetFriendsNotesQuery(UserId);
        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return BadRequest(result.Errors);
    }
    

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] CreateNoteVm vm,
        CancellationToken cancellationToken
        )
    {
        var command = new CreateNoteCommand(UserId, vm.Body, vm.IsPersonal);
        var result = await Mediator.Send(command, cancellationToken);

        if(!result.IsSuccess)
        {
            ViewBag.AddNewsErrorMessage = "Ошибка добавления новости";
        }
        return Redirect(RoutesConstants.MyPageRoutePath);
    }
}