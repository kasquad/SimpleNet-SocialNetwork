using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetMyUserPage;

public record MyUserPageDto(Guid UserId, string UserName, ICollection<Note> Notes) : IAppRequest
{
    public static explicit operator MyUserPageDto(User user)
    {
        return new MyUserPageDto(
            user.Id,
            user.FullName,
            user.Notes
        );
    }
}