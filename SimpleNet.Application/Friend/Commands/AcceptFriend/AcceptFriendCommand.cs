using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Friend.Commands.AcceptFriend;

public record AcceptFriendCommand(Guid UserId, string ProposingUserId) : IAppRequest
{
};
