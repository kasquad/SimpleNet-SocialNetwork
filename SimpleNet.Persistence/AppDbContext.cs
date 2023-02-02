using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Domain.Models;
using SimpleNet.Persistence.EntityTypeConfiguration;

namespace SimpleNet.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(builder);
    }
}   