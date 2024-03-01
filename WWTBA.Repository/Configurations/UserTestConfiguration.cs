using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WWTBA.Core.Models;

namespace WWTBA.Repository.Configurations
{
    public class UserTestConfiguration : IEntityTypeConfiguration<UserTest>
    {
        public void Configure(EntityTypeBuilder<UserTest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.UserId).IsRequired();

            builder.ToTable("UserTests");

            builder.HasOne(x => x.User).WithMany(x => x.UserTests).HasForeignKey(x => x.UserId);
        }
    }
}

