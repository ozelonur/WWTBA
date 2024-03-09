using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WWTBA.Core.Models;

namespace WWTBA.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();

            builder.ToTable("Users");
        }
    }
}

