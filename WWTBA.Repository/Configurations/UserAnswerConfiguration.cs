using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WWTBA.Core.Models;

namespace WWTBA.Repository.Configurations
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.AnswerId).IsRequired();
            builder.Property(x => x.UserTestId).IsRequired();

            builder.ToTable("UserAnswers");

            builder.HasOne(x => x.User).WithMany(x => x.UserAnswers).HasForeignKey(x => x.UserId);
        }
    }
}