using GCommon.Archiving.Abstractions;

namespace GCommon.Archiving
{
    public class ArchiveRepositoryFactory : IArchiveRepositoryFactory
    {
        public IArchiveRepository Create(string connectionString)
        {
            return new ArchiveRepository(connectionString);
        }
    }
}