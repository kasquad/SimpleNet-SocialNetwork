using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Application.Users.Queries.GetUsersByName;

namespace SimpleNet.Application.Notes.Queries.GetFriendsNotes;

public class GetFriendsNotesQueryHandler : IAppRequestHandler<GetFriendsNotesQuery, FriendsNotesVm>
{
    private readonly IAppDbContext _context;

    public GetFriendsNotesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<FriendsNotesVm>> Handle(
        GetFriendsNotesQuery request,
        CancellationToken cancellationToken)
    {
        var friendsNotesVm = new FriendsNotesVm();
        
        try
        {
            // Todo: refactor this query and reduce the amount of data requested in the database
            var t = await _context
                .Friends
                .Include(f => f.AcceptingUser)
                    .ThenInclude(u => u.Notes)
                .Include(f => f.ProposingUser)
                  .ThenInclude(u => u.Notes)
                .Where(f =>
                    f.IsAccept &&
                    (f.AcceptingUserId == request.UserId || f.ProposingUserId == request.UserId))
                .Select(f =>
                    f.AcceptingUserId == request.UserId ? 
                        f.ProposingUser : 
                        f.AcceptingUser
                )
                .ToListAsync(cancellationToken: cancellationToken);

            friendsNotesVm.Notes = t.Select(u =>
                    u.Notes
                        .Select(n => new NoteVm()
                        {
                            User = (UserMinVm)u,
                            Body = n.Body,
                            DateTime = n.DateTime,
                            IsPersonal = n.IsPersonal
                        }))
                .SelectMany(n => n);
            
            return Result<FriendsNotesVm>.Success(friendsNotesVm);
        }
        catch (Exception ex)
        {
            return Result<FriendsNotesVm>.Failure("Exception");         
        }
    }
}