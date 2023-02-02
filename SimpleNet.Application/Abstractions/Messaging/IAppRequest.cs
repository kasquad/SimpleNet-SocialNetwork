using MediatR;
using SimpleNet.Application.Helpers;

namespace SimpleNet.Application.Abstractions.Messaging;

public interface IAppRequest : IRequest<Result>
{
}

public interface IAppRequest<TResponse> : IRequest<Result<TResponse>>
{
}
