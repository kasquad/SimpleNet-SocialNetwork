using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Users.Commands.Login;

public record LoginAppRequest(string Email, string Password) : IAppRequest<string>;