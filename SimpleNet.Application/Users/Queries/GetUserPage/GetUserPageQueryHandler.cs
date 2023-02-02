using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetUserPage;

public class GetUserPageQueryHandler : IAppRequestHandler<GetUserPageQuery,UserPageDto>
{
    private IAppDbContext _context;

    public GetUserPageQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    

    public async Task<Result<UserPageDto>> Handle(GetUserPageQuery query, CancellationToken cancellationToken)
    {
        User? user = default;
        try
        {
            user = await _context.Users
                .Include(u => u.Notes.OrderByDescending(n => n.DateTime))
                .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<UserPageDto>.Failure("Exception");
        }

        if (user is null)
        {
            return Result<UserPageDto>.Failure("User not founded");
        }

        var vm = (UserPageDto)user;
        return Result<UserPageDto>.Success(vm);
    }
}