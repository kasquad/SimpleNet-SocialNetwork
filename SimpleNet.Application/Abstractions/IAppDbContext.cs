using Microsoft.EntityFrameworkCore;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Abstractions;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}