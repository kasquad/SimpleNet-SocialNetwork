using SimpleNet.Application.Users.Queries.GetUsersByName;

namespace SimpleNet.Application.Friend.Queries;

public class FriendsListVm
{
    public ICollection<UserMinVm> List { get; set; } = new List<UserMinVm>();
    public int FriendRequestsCount { get; set; }
}