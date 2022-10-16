using Caliburn.Micro;
using STP.DataLayer.API;
using STP.DataLayer.Models;
using System.Threading.Tasks;

namespace STP.UserInterface.ViewModels
{
    internal class StreamListViewModel : Screen
    {
        private readonly IStreamsService _streamsService;
        private readonly IEventAggregator _eventAggregator;
        private readonly StreamTimelineViewModel _streamTimelineViewModel;

        private StreamInfo _selectedInfo;

        public BindableCollection<StreamInfo> StreamInfos { get; set; } = new();

        public StreamInfo SelectedStream
        {
            get => _selectedInfo;
            set
            {
                _selectedInfo = value;
                NotifyOfPropertyChange(nameof(CanSelect));
            }
        }

        public bool CanSelect => !_selectedInfo.Equals(default(StreamInfo));

        public StreamListViewModel(IStreamsService streamsService, IEventAggregator eventAggregator, StreamTimelineViewModel streamTimeLineViewModel)
        {
            _streamsService = streamsService;
            _eventAggregator = eventAggregator;
            _streamTimelineViewModel = streamTimeLineViewModel;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            StreamInfos.AddRange(await _streamsService.GetMineStreamsAsync());
        }

        public async Task Select()
        {
            _streamsService.StartCheckNewStream(_selectedInfo.Id);
            _streamsService.StreamsStatusChanged += _streamTimelineViewModel.StreamChanged;
            _streamTimelineViewModel.StreamInfo = await _streamsService.GetStreamInfoAsync(_selectedInfo.Id);

            await _eventAggregator.PublishOnUIThreadAsync(_streamTimelineViewModel);
        }
    }
}
