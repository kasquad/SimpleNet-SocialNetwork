using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Friend.Queries.GetFriendsList;

public record GetFriendsQuery(Guid UserId) : IAppRequest<FriendsListVm>;