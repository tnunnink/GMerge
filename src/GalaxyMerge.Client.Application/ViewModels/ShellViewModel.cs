using GalaxyMerge.Client.Application.Views;
using GalaxyMerge.Client.Core.Naming;
using GalaxyMerge.Client.Events;
using NLog;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace GalaxyMerge.Client.Application.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private bool _showEventLog;
        
        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Logger.Trace("Initializing Shell ViewModel");

            regionManager.RegisterViewWithRegion<ShellHeaderView>(RegionName.ShellHeaderRegion);
            regionManager.RegisterViewWithRegion<ShellFooterView>(RegionName.ShellFooterRegion);
            regionManager.RegisterViewWithRegion<EventLogView>(RegionName.EventLogRegion);

            eventAggregator.GetEvent<ShowEventLogChangedEvent>().Subscribe(OnShowEventLogChanged);
        }

        private void OnShowEventLogChanged(bool showEventLog)
        {
            ShowEventLog = showEventLog;
        }

        public bool ShowEventLog
        {
            get => _showEventLog;
            set => SetProperty(ref _showEventLog, value);
        }
    }
}