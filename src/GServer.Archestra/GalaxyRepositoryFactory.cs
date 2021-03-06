using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArchestrA.GRAccess;
using GServer.Archestra.Extensions;
using GServer.Archestra.Abstractions;
using NLog;

namespace GServer.Archestra
{
    public class GalaxyRepositoryFactory : IGalaxyRepositoryFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IGalaxyRepository Create(string galaxyName)
        {
            return new GalaxyRepository(galaxyName);
        }

        public IEnumerable<IGalaxyRepository> CreateAll()
        {
            var host = Environment.MachineName;
            var grAccess = new GRAccessAppClass();
            
            Logger.Debug("Querying for galaxies on {Host}", host);
            var galaxies = grAccess.QueryGalaxies(host);
            grAccess.CommandResult.Process();

            foreach (IGalaxy galaxy in galaxies)
            {
                Logger.Debug("Creating repository instance for {Galaxy}", galaxy.Name);
                yield return new GalaxyRepository(grAccess, galaxy);
            }
        }
    }
}