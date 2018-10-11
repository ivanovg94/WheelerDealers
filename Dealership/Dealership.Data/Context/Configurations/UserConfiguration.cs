using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dealership.Data.Context.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Username)
                    .IsUnique();

            builder.HasIndex(u => u.Email)
                    .IsUnique();
        }
    }
}
