using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Notes.Queries.GetFriendsNotes;

public record GetFriendsNotesQuery(Guid UserId) : IAppRequest<FriendsNotesVm>
{
    
}
