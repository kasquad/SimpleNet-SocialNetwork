using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetMyUserPage;

public class GetMyUserPageQueryHandler : IAppRequestHandler<GetMyUserPageQuery,MyUserPageDto>
{
    private IAppDbContext _context;

    public GetMyUserPageQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<MyUserPageDto>> Handle(GetMyUserPageQuery query, CancellationToken cancellationToken)
    {
        User? user = default;
        try
        {
            user = await _context.Users
                .Include(
                    u => u.Notes
                    .OrderByDescending(n => n.DateTime)
                )
                .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<MyUserPageDto>.Failure("Exception");
        }

        if (user is null)
        {
            return Result<MyUserPageDto>.Failure("User not founded");
        }

        var vm = (MyUserPageDto)user;
        return Result<MyUserPageDto>.Success(vm);
    }
}