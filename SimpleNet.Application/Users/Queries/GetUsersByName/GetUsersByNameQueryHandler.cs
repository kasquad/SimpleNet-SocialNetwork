using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Extensions;
using SimpleNet.Application.Helpers;

namespace SimpleNet.Application.Users.Queries.GetUsersByName;

public class GetUsersByNameQueryHandler : IAppRequestHandler<GetUsersByNameQuery, SearchedUsersVm>
{
    private IAppDbContext _context;

    public GetUsersByNameQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<SearchedUsersVm>> Handle(GetUsersByNameQuery query, CancellationToken cancellationToken)
    {
        var searchedUsersDto = new SearchedUsersVm();

        try
        {
            // Todo: Make normalized name field 
            searchedUsersDto.Users = await _context.Users
                .Select(u => new UserMinVm()
                {
                    Id = u.Id,
                    Name = u.FullName
                })
                .Where(u => u.Id != query.UserId &&
                            !_context.Friends.Any(f =>
                                f.ProposingUserId == query.UserId &&
                                f.AcceptingUserId == u.Id ||
                                f.ProposingUserId == u.Id &&
                                f.AcceptingUserId == query.UserId
                            )
                            && u.Name.Contains(query.Name)
                )
                .ToListAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<SearchedUsersVm>.Failure("Exception");
        }
        
        return Result<SearchedUsersVm>.Success(searchedUsersDto);
    }
}