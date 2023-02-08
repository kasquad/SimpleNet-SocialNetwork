using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Queries.GetUsersByName;

public class UserMinVm
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static explicit operator UserMinVm(User user)
    {
        return new UserMinVm
        {
            Id = user.Id,
            Name = user.FullName
        };
    }
}