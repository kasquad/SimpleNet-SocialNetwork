using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Extensions;
using SimpleNet.Application.Helpers;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetUserPage;

public class GetUserPageQueryHandler : IAppRequestHandler<GetUserPageQuery,UserPageVm>
{
    private IAppDbContext _context;

    public GetUserPageQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<UserPageVm>> Handle(
        GetUserPageQuery query,
        CancellationToken cancellationToken)
    {
        var user = default(User);
        var friendState = await _context
            .GetFriendState(query.VisitorId,query.UserId , cancellationToken);
        
        try
        {
            var isFriends = friendState == FriendState.Accept;
            
            user = await _context.Users
                .Include(
                    u => u.Notes
                        .Where(n => n.IsPersonal && isFriends || !n.IsPersonal)
                        .OrderByDescending(n => n.DateTime)
                )
                .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<UserPageVm>.Failure("Exception");
        }

        if (user is null)
        {
            return Result<UserPageVm>.Failure("User not founded");
        }

        var vm = new UserPageVm(query.UserId, user.FullName, user.Notes, friendState);
        return Result<UserPageVm>.Success(vm);
    }
}