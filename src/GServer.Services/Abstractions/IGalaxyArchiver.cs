using GCommon.Core;

namespace GServer.Services.Abstractions
{
    public interface IGalaxyArchiver
    {
        void Archive(ArchiveObject archiveObject);
    }
}