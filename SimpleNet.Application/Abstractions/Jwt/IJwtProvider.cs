using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Abstractions.Jwt;

public interface IJwtProvider
{
    public string Generate(User user);
}