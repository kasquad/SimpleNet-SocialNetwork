using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Friend.Commands.AddFriend;
using SimpleNet.Application.Helpers;

namespace SimpleNet.Application.Friend.Commands.AcceptFriend;

public class AcceptFriendCommandHandler : IAppRequestHandler<AcceptFriendCommand>
{
    private IAppDbContext _context;

    public AcceptFriendCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AcceptFriendCommand command, CancellationToken cancellationToken)
    {
        Guid ProposingUserId;

        if (command.UserId.ToString()
            .Equals(command.ProposingUserId))
        {
            return Result.Failure("Self adding exception");
        }

        if (!Guid.TryParse(command.ProposingUserId, out ProposingUserId))
        {
            return Result.Failure("Exception");
        }

        try
        {
            var friendsRequest = await _context.Friends
                .FirstOrDefaultAsync(f => f.AcceptingUserId == command.UserId
                                          && f.ProposingUserId == ProposingUserId
                                          && f.IsAccept == false,
                    cancellationToken: cancellationToken);

            if (friendsRequest is null)
            {
                return Result.Failure("Not found request exception");
            }

            friendsRequest.IsAccept = true;
            _context.Friends.Update(friendsRequest);
            
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure("Exception");
        }
        
        return Result.Success();
    }
}