using SimpleNet.Application.Abstractions;
using SimpleNet.Application.Abstractions.Messaging;
using SimpleNet.Application.Helpers;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Notes.Commands.Create;

public class CreateNoteCommandHandler : IAppRequestHandler<CreateNoteCommand>
{
    private readonly IAppDbContext _context;

    public CreateNoteCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _context.Notes.Add(new Note
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Body = command.Body,
                DateTime = DateTime.Now,
                IsPersonal = command.IsPersonal
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