namespace SimpleNet.Application.Users.Queries.GetUsersByName;

public class SearchedUsersVm
{
    public ICollection<UserMinVm> Users { get; set; } = new List<UserMinVm>();
}