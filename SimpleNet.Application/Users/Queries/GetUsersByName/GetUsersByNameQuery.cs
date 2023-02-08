using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Users.Queries.GetUsersByName;

public record GetUsersByNameQuery(Guid UserId,string Name) : IAppRequest<SearchedUsersVm>;