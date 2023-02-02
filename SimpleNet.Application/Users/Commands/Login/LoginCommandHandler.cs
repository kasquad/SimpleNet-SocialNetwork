using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Hash;
using SimpleNet.Application.Abstractions.Jwt;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;

namespace SimpleNet.Application.Users.Commands.Login;

internal sealed class LoginAppRequestHandler : IAppRequestHandler<LoginAppRequest,string>
{
    private readonly IAppDbContext _appContext;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashProvider _hashProvider;

    public LoginAppRequestHandler(
        IAppDbContext appContext, 
        IJwtProvider jwtProvider,
        IHashProvider hashProvider)
    {
        _appContext = appContext;
        _jwtProvider = jwtProvider;
        _hashProvider = hashProvider;
    }

    public async Task<Result<string>> Handle(
        LoginAppRequest appRequest,
        CancellationToken cancellationToken)
    {
        //Get User
        var hashedPassword = _hashProvider.ComputeHash(appRequest.Password);
        
        var user = await _appContext.Users
            .FirstOrDefaultAsync(
                u => u.Email == appRequest.Email && u.HashedPassword == hashedPassword,
                cancellationToken: cancellationToken);
    
        if (user is null)
        {
            return Result<string>.Failure("Invalid credentials");
        }
        
        // Generate jwt
        string token = _jwtProvider.Generate(user);
        
        // Return jwt
        return Result<string>.Success(token);
    }
}