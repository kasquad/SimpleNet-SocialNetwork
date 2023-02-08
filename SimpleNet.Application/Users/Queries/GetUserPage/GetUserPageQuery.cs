using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Users.Queries.GetMyUserPage;

namespace SimpleNet.Application.Users.Queries.GetUserPage;

public sealed class GetUserPageQuery : IAppRequest<UserPageVm>
{
    public Guid VisitorId { get; private set; }
    public Guid UserId { get; private set; }

    public GetUserPageQuery(Guid visitorId,string userId)
    {
        VisitorId = visitorId;
        UserId = Guid.Parse(userId);
    }
} 