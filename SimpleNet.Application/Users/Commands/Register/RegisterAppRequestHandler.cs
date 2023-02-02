using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Hash;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Users.Commands.Register;

public class RegisterAppRequestHandler : IAppRequestHandler<RegisterAppRequest>
{
    private readonly IAppDbContext _context;
    private readonly IHashProvider _hashProvider;

    public RegisterAppRequestHandler(
            IAppDbContext context,
            IHashProvider hashProvider)
    {
        _context = context;
        _hashProvider = hashProvider;
    }

    public async Task<Result> Handle(RegisterAppRequest appRequest, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(
                u => u.Email == appRequest.Email,
                cancellationToken: cancellationToken))
        {
            return Result.Failure("Invalid email");
        }

        try
        {
            var hashedPassword = _hashProvider.ComputeHash(appRequest.Password);
            
            _context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = appRequest.Email,
                FullName = appRequest.FullName,
                HashedPassword = hashedPassword
            });

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure("Exception");
        }
        
        return Result.Success();
    }
}
