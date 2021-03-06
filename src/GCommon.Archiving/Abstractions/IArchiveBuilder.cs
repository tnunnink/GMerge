using GCommon.Core;

namespace GCommon.Archiving.Abstractions
{
    /// <summary>
    /// Interface that defines the api for building a new archive repository database. 
    /// </summary>
    public interface IArchiveBuilder
    {
        /// <summary>
        /// Builds an archive database using the provided archive configuration
        /// </summary>
        /// <param name="archiveConfig">The archive used to build the database</param>
        /// <param name="connectionString">Optionally provide the connection string for the database</param>
        void Build(ArchiveConfig archiveConfig, string connectionString = null);
    }
}