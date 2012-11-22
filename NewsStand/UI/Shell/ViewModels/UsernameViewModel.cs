using System;
using Caliburn.Micro;

namespace NewsStand.UI.Shell.ViewModels
{
    public class UsernameViewModel : Screen
    {
        public override string DisplayName
        {
            get
            {
                return "Quote.fm username";
            }
            set
            {
                base.DisplayName = value;
            }
        }

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
            }
        }

        public delegate void ClosedHandler(object sender, ClosedEventArgs args);

        public event ClosedHandler Closed;

        public void Ok()
        {
            Closed(this, new ClosedEventArgs { Username = Username });
        }
    }

    public class ClosedEventArgs : EventArgs
    {
        public string Username { get; set; }
    }
}
