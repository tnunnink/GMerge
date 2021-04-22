using System;
using System.Threading;
using Autofac;
using GalaxyMerge.Archestra;
using GalaxyMerge.Archestra.Abstractions;
using GalaxyMerge.Core.Utilities;
using GalaxyMerge.Services;

namespace GalaxyMerge.Host
{
    public class Bootstrapper
    {
        private IContainer _container;
        
        public void Bootstrap()
        {
            ConfigureContainer();
            RegisterGalaxies();
            ValidateArchives();
        }

        public IContainer GetContainer()
        {
            return _container;
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GalaxyRepositoryFactory>().As<IGalaxyRepositoryFactory>();
            builder.RegisterType<GalaxyRegistry>().AsSelf().SingleInstance();
            builder.RegisterType<GalaxyManager>();
            _container = builder.Build();
        }

        private void RegisterGalaxies()
        {
            if (_container == null)
                throw new InvalidOperationException("Container not yet initialized");

            var registrant = _container.Resolve<GalaxyRegistry>();
            
            registrant.RegisterAllAsync(CancellationToken.None); //todo should I be passing in a cancellation here? perhaps after x seconds stop service.
        }

        private void ValidateArchives()
        {
            if (_container == null)
                throw new InvalidOperationException("Container not yet initialized");

            var registry = _container.Resolve<GalaxyRegistry>();
            var galaxies = registry.GetAll();

            foreach (var galaxy in galaxies)
            {
                var connectionString = ConnectionStringBuilder.BuildArchiveConnection(galaxy.Name);
                //TODO I guess figure out if the DBs exists and create them if not.
            }
        }
    }
}