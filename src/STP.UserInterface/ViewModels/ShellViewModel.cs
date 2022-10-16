using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace STP.UserInterface.ViewModels
{
    internal class ShellViewModel : Conductor<object>, IHandle<StreamTimelineViewModel>
    {
        private readonly StreamListViewModel _streamListViewModel;

        public ShellViewModel(StreamListViewModel streamListViewModel, IEventAggregator eventAggregator)
        {
            _streamListViewModel = streamListViewModel;
            eventAggregator.SubscribeOnUIThread(this);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await StreamListAsync();
        }

        public Task StreamListAsync()
        {
            return ActivateItemAsync(_streamListViewModel);
        }

        public Task HandleAsync(StreamTimelineViewModel message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(message);
        }
    }
}
