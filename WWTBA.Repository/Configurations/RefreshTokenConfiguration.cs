using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WWTBA.Core.Models;

namespace WWTBA.Repository.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Id).UseIdentityColumn();
            builder.Property(rt => rt.Token).IsRequired().HasMaxLength(256);
            builder.Property(rt => rt.UserId).IsRequired();
            builder.Property(rt => rt.Created).IsRequired();
            builder.Property(rt => rt.Expires).IsRequired();
            builder.Property(rt => rt.IsRevoked);
            builder.Property(rt => rt.Revoked);

            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.ToTable("RefreshTokens");
        }
    }
}