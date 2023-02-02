using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Users.Queries.GetUserPage;

public record GetUserPageQuery(Guid UserId) : IAppRequest<UserPageDto>{ }