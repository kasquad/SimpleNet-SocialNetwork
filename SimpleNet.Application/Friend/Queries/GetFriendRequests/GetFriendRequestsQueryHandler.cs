using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Application.Users.Queries.GetUsersByName;

namespace SimpleNet.Application.Friend.Queries.GetFriendRequests;

public class GetFriendRequestsQueryHandler : IAppRequestHandler<GetFriendRequestsQuery,FriendsListVm>
{
    private IAppDbContext _context;

    public GetFriendRequestsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<FriendsListVm>> Handle(
        GetFriendRequestsQuery query, 
        CancellationToken cancellationToken)
    {
        var friendRequestsList = new FriendsListVm();

        try
        {
            friendRequestsList.List = await _context.Friends
                .Where(f => f.AcceptingUserId == query.UserId && f.IsAccept == false)
                .Include(f => f.ProposingUser)
                .Select(f => new UserMinVm
                {
                    Id = f.ProposingUserId,
                    Name = f.ProposingUser.FullName
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<FriendsListVm>.Failure("Exception");
        }
        
        return Result<FriendsListVm>.Success(friendRequestsList);
    }
}