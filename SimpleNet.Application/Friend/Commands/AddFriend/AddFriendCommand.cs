using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Friend.Commands.AddFriend;

public record AddFriendCommand(Guid UserId, string AcceptingUserId) : IAppRequest;
