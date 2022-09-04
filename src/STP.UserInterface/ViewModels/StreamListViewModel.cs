using Caliburn.Micro;
using STP.DataLayer.API;
using STP.DataLayer.Models;

namespace STP.UserInterface.ViewModels
{
    internal class StreamListViewModel : Screen
    {
        public BindableCollection<StreamInfo> StreamInfos { get; set; } = new();

        private readonly IStreamsService _streamsService;

        public StreamListViewModel(IStreamsService streamsService)
        {
            _streamsService = streamsService;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            StreamInfos.AddRange(await _streamsService.GetMineStreamsAsync());
        }
    }
}
