using Caliburn.Micro;
using STP.DataLayer.API;
using System.Threading.Tasks;
using System.Windows;

namespace STP.UserInterface.ViewModels
{
    internal class ShellViewModel : Conductor<object>
    {
        private readonly StreamListViewModel _streamListViewModel;

        public ShellViewModel(StreamListViewModel streamListViewModel)
        {
            _streamListViewModel = streamListViewModel;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await StreamListAsync();
        }

        public Task StreamListAsync()
        {
            return ActivateItemAsync(_streamListViewModel, default);
        }
    }
}
