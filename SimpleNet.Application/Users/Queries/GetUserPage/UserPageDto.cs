using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetUserPage;

public record UserPageDto(Guid UserId, string UserName, ICollection<Note> Notes) : IAppRequest
{
    public static explicit operator UserPageDto(User user)
    {
        return new UserPageDto(
            user.Id,
            user.FullName,
            user.Notes
        );
    }
}