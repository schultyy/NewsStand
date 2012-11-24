using Caliburn.Micro;

namespace NewsStand.UI.Shell.ViewModels
{
    public sealed class UsernameViewModel : Screen
    {
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value)
                    return;
                username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanConfirm);
            }
        }

        public bool CanConfirm
        {
            get { return !string.IsNullOrEmpty(Username); }
        }

        public delegate void ClosedHandler(object sender, ClosedEventArgs args);

        public event ClosedHandler Closed;

        public UsernameViewModel()
        {
            DisplayName = string.Empty;
        }

        public void Confirm()
        {
            Closed(this, new ClosedEventArgs { Username = Username });
        }
    }
}
