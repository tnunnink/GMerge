using System.Collections.Generic;
using GServer.Archestra.Abstractions;

namespace GServer.Services.Abstractions
{
    public interface IGalaxyRegistry
    {
        bool IsRegistered(string galaxyName, string userName);
        IGalaxyRepository GetGalaxy(string galaxyName, string userName);
        IEnumerable<IGalaxyRepository> GetByName(string galaxyName);
        IEnumerable<IGalaxyRepository> GetByUser(string userName);
        IEnumerable<IGalaxyRepository> GetByCurrentIdentity();
        IGalaxyRepository GetByCurrentIdentity(string galaxyName);
        IEnumerable<IGalaxyRepository> GetAll();
        void Register(string galaxyName);
        void Register(string galaxyName, string userName);
        void RegisterAll();
        void RegisterAll(string userName);
        void Unregister(string galaxyName, string userName);
        void Unregister(IEnumerable<string> galaxies, string userName);
    }
}