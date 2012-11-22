using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace NewsStand.UI.Shell.ViewModels
{
    public class ShellViewModel : Screen
    {
        public override string DisplayName
        {
            get
            {
                return "Newsstand";
            }
            set
            {
                base.DisplayName = value;
            }
        }
    }
}
