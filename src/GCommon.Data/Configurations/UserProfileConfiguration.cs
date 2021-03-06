using GCommon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCommon.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            //Configuring as view because EF core will treat this table as read-only, which we want.
            builder.ToView("user_profile").HasKey(x => x.UserId);

            builder.Property(x => x.UserId).HasColumnName("user_profile_id");
            builder.Property(x => x.UserGuid).HasColumnName("user_guid");
            builder.Property(x => x.UserName).HasColumnName("user_profile_name");
            builder.Property(x => x.UserFullName).HasColumnName("user_full_name");
        }
    }
}