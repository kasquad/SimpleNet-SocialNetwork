using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Users.Queries;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Extensions;

public static class AppDbContextExtensions
{
    public static async Task<FriendState> GetFriendState(
        this IAppDbContext context,
        Guid visitorId,
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        var friends =  await context.Friends
            .FirstOrDefaultAsync(f => 
                    (f.ProposingUserId == visitorId && f.AcceptingUserId == userId||
                     f.ProposingUserId == userId && f.AcceptingUserId == visitorId), 
                cancellationToken: cancellationToken);

        if (friends is null)
        {
            return FriendState.None;
        }

        if (friends.IsAccept)
        {
            return FriendState.Accept;
        }

        if (friends.AcceptingUserId == visitorId)
        {
            return FriendState.IncomingRequest;
        }
        
        return FriendState.OutgoingRequest;
    }

    public static IQueryable<User> GetFriendsQuery<TPropery>(
        this DbSet<Friends> friends,
        Guid userId,
        Expression<Func<User,TPropery>> includeExpression
    )
    {
        return friends
            .Include(f => f.AcceptingUser)
            .Include(f => f.ProposingUser)
            .Where(f => (f.ProposingUserId == userId ||
                         f.AcceptingUserId == userId) && 
                        f.IsAccept == true)
            .Select(f =>
                f.AcceptingUserId == userId
                    ? f.ProposingUser
                    : f.AcceptingUser
            ).AsQueryable();
    }
}