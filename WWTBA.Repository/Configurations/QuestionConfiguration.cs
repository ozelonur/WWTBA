using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WWTBA.Core.Models;

namespace WWTBA.Repository.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.QuestionText).IsRequired();
            builder.Property(x => x.Explanation).IsRequired();

            builder.ToTable("Questions");

            builder.HasOne(x => x.Subject).WithMany(x => x.Questions).HasForeignKey(x => x.SubjectId);
        }
    }
}