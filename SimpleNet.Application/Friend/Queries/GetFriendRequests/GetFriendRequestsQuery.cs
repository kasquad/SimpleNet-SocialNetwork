using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Friend.Queries.GetFriendRequests;

public record GetFriendRequestsQuery(Guid UserId) : IAppRequest<FriendsListVm>;