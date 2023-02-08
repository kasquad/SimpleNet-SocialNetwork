using Microsoft.EntityFrameworkCore;
using SimpleNet.Application.Abstractions;
using SimpleNet.Domain.Models;
using SimpleNet.Persistence.EntityTypeConfiguration;

namespace SimpleNet.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Friends> Friends { get; set; }
    public DbSet<Note> Notes { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfiguration(new UserConfiguration());
        // builder.Entity<Friends>()
        //     .HasOne(f => f.ProposingUser)
        //     .WithMany(u => u.IncomingFriends)
        //     .HasForeignKey(f => f.ProposingUserId)
        //     .OnDelete(DeleteBehavior.Restrict);
        //
        // builder.Entity<Friends>()
        //     .HasOne(f => f.AcceptingUser)
        //     .WithMany(u => u.OutgoingFriends)
        //     .HasForeignKey(f => f.AcceptingUserId)
        //     .OnDelete(DeleteBehavior.Restrict);
        //
        //
        // builder.Entity<Friends>().HasKey(f => new { f.AcceptingUserId, f.ProposingUserId });

        // builder.Entity<Friends>()
        //     .HasRequired(a => a.RequestedTo)
        //     .WithMany(b => b.ReceievedFriendRequests)
        //     .HasForeignKey(c => c.RequestedToId);
        //
        builder.Entity<User>()
            .HasMany(u => u.OutgoingFriends)
            .WithMany(u => u.IncomingFriends)
            .UsingEntity<Friends>(
                j => j
                    .HasOne(f => f.AcceptingUser)
                    .WithMany()
                    .HasForeignKey(f => f.AcceptingUserId),
                j => j
                    .HasOne(f => f.ProposingUser)
                    .WithMany()
                    .HasForeignKey(f => f.ProposingUserId),
                j => j
                    .HasKey(f => new { f.AcceptingUserId, f.ProposingUserId })
            );
        // builder.Entity<Friends>()
        //     .HasOne(f => f.AcceptingUser)
        //     .WithMany()
        //     .OnDelete(DeleteBehavior.Restrict);
        //
        // builder.Entity<Friends>()
        //     .HasOne(f => f.ProposingUser)
        //     .WithMany()
        //     .OnDelete(DeleteBehavior.Restrict);
        
            // builder.Entity<Friends>().HasKey(f => new { f.AcceptingUserId, f.ProposingUserId });

        base.OnModelCreating(builder);
    }
}   