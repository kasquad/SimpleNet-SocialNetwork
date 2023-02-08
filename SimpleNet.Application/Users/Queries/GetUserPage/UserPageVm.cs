using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetUserPage;

public record UserPageVm(Guid visitUserId, string FullName,ICollection<Note> Notes,FriendState friendState)
{
    
}