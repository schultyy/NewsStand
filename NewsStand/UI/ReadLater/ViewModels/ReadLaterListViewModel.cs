using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using NewsStand.Model;

namespace NewsStand.UI.ReadLater.ViewModels
{
    public class ReadLaterListViewModel : Screen
    {
        public override string DisplayName
        {
            get
            {
                return "Recommendations saved for later reading";
            }
            set
            {
                base.DisplayName = value;
            }
        }

        private ObservableCollection<Recommendation> field;

        public ObservableCollection<Recommendation> Field
        {
            get { return field; }
            set
            {
                if (field == value)
                    return;
                field = value;
                NotifyOfPropertyChange(() => Field);
            }
        }
    }
}
