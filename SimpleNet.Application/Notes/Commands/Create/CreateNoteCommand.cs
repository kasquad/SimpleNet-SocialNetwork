using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Notes.Commands.Create;

public record CreateNoteCommand(Guid UserId,string Body,bool IsPersonal) : IAppRequest
{
}
