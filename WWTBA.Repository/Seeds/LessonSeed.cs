using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WWTBA.Core.Models;

namespace WWTBA.Repository.Seeds
{
    public class LessonSeed : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasData(
                new Lesson { Id = 1, Name = "Tarih" },
                new Lesson { Id = 2, Name = "Coğrafya" },
                new Lesson { Id = 3, Name = "Vatandaşlık" }
            );
        }
    }
}