using Microsoft.AspNetCore.Mvc;
using SimpleNet.AppConstants;
using SimpleNet.Application.Notes.Create;
using SimpleNet.ViewModels.Notes;

namespace SimpleNet.Controllers;

[Route("notes")]
public class NotesController : SNetController
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] CreateNoteVm vm)
    {
        var command = new CreateNoteCommand(UserId, vm.Body, vm.IsPersonal);

        var result = await Mediator.Send(command);

        if(!result.IsSuccess)
        {
            ViewBag.AddNewsErrorMessage = "Ошибка добавления новости";
        }
        return Redirect(RoutesConstants.MyPageRoutePath);
    }
}