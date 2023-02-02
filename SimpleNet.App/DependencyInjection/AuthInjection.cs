using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleNet.AppConstants;
using SimpleNet.Services.Authentication;

namespace SimpleNet.DependencyInjection;

public static class AuthInjection
{
    public static IServiceCollection AddCookieBasedJwtAuth(this IServiceCollection
        services,IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                var jwtOptions = new JwtOptions();
                configuration.GetSection(SettingsConstants.JwtSectionName).Bind(jwtOptions);
        
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
                    )
                };

                o.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[SettingsConstants.AuthCookieName];
                        return Task.CompletedTask;
                    }
                };
            });
        
        return services;
    }
}