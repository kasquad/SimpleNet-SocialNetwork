using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Extensions;
using SimpleNet.Application.Helpers;
using SimpleNet.Application.Users.Queries.GetUsersByName;

namespace SimpleNet.Application.Friend.Queries.GetFriendsList;

public sealed class GetFriendsQueryHandler : IAppRequestHandler<GetFriendsQuery,FriendsListVm>
{
    private readonly IAppDbContext _context;

    public GetFriendsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<FriendsListVm>> Handle(
        GetFriendsQuery query,
        CancellationToken cancellationToken)
    {
        var friendsList = new FriendsListVm();

        try
        {
            friendsList.List = await _context
                .Friends
                .GetFriendsQuery(query.UserId,u => u.Notes)
                .Select(u => new UserMinVm()
                {
                    Id = u.Id,
                    Name = u.FullName
                })
                .ToListAsync(cancellationToken: cancellationToken);

            friendsList.FriendRequestsCount = await _context
                .Friends
                .CountAsync(f => 
                        f.AcceptingUserId == query.UserId &&
                        f.IsAccept == false,
                    cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<FriendsListVm>.Failure("Exception");
        }
        
        return Result<FriendsListVm>.Success(friendsList);
    }
}