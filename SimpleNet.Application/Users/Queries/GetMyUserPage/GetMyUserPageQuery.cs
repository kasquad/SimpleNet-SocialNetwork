using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Users.Queries.GetMyUserPage;

public sealed class GetMyUserPageQuery : IAppRequest<MyUserPageDto>
{
    public Guid UserId { get; private set; }

    public GetMyUserPageQuery(Guid userId)
    {
        UserId = userId;
    }
    public GetMyUserPageQuery(string userId)
    {
        UserId = Guid.Parse(userId);
    }
}