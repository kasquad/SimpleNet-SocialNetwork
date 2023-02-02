using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleNet.Application.Abstractions.Hash;
using SimpleNet.Application.Abstractions.Jwt;
using SimpleNet.Services.Authentication;

namespace SimpleNet.Services;

public static class DipendencyInjection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<IHashProvider, Md5HashProvider>();
        
        return services;
    }
}