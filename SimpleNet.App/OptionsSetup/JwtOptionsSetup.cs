using Microsoft.Extensions.Options;
using SimpleNet.AppConstants;
using SimpleNet.Services.Authentication;

namespace SimpleNet.OptionsSetup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SettingsConstants.JwtSectionName).Bind(options);
    }
}