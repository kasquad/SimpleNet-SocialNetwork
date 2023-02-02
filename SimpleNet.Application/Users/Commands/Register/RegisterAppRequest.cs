using SimpleNet.Application.Abstractions.Messaging;

namespace SimpleNet.Application.Users.Commands.Register;

public record RegisterAppRequest(string FullName,string Email,string Password) : IAppRequest;
