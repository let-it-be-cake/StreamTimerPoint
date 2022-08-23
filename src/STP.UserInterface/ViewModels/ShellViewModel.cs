using Caliburn.Micro;
using STP.DataLayer.Interfaces;
using System.Windows;

namespace STP.UserInterface.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IStreamService _authorizeService;
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

        public ShellViewModel(IStreamService authorizeService)
        {
            _authorizeService = authorizeService;
        }
    }
}
