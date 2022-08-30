using Caliburn.Micro;
using STP.DataLayer.API;
using System.Windows;

namespace STP.UserInterface.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IStreamsService _streamsService;
        private string? _name;

        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", Name)); //Don't do this in real life :)
        }

        public ShellViewModel(IStreamsService streamsService)
        {
            _streamsService = streamsService;
        }
    }
}
