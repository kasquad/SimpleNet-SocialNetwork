using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Friend.Commands.AddFriend;

public class AddFriendCommandHandler : IAppRequestHandler<AddFriendCommand>
{
    private IAppDbContext _context;

    public AddFriendCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AddFriendCommand command, CancellationToken cancellationToken)
    {
        Guid acceptingUserId;

        if (command.UserId.ToString()
            .Equals(command.AcceptingUserId))
        {
            return Result.Failure("Self adding exception");
        }
        
        if (!Guid.TryParse(command.AcceptingUserId, out acceptingUserId))
        {
            return Result.Failure("Exception");
        }

        try
        {
            if (await _context.Friends.AnyAsync(f =>
                    f.ProposingUserId == acceptingUserId
                    && f.AcceptingUserId == command.UserId,
                    cancellationToken: cancellationToken))
            {
                return Result.Failure("Exception");
            }
            
            _context.Friends.Add(
            new Friends()
            {
                    ProposingUserId = command.UserId,
                    AcceptingUserId = acceptingUserId,
                    IsAccept = false,
                });

            await _context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            return Result.Failure("Exception");
        }


        return Result.Success();
    }
}