using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleNet.Domain.Models;

namespace SimpleNet.Persistence.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id).IsUnique();
        builder.Property(user => user.FullName).HasMaxLength(120);
        builder.Property(user => user.HashedPassword).HasMaxLength(32);
    }
}