using GCommon.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCommon.Archiving.Configurations
{
    internal class ArchiveEntryConfiguration : IEntityTypeConfiguration<ArchiveEntry>
    {
        public void Configure(EntityTypeBuilder<ArchiveEntry> builder)
        {
            builder.ToTable(nameof(ArchiveEntry)).HasKey(x => x.EntryId);
            
            builder.Property(g => g.ObjectId).IsRequired();
            builder.Property(g => g.Version).IsRequired();
            builder.Property(g => g.ArchivedOn).IsRequired();
            builder.Property(g => g.OriginalSize).IsRequired();
            builder.Property(g => g.CompressedSize).IsRequired();
            builder.Property(g => g.CompressedData).IsRequired();
        }
    }
}