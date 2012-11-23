using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace NewsStand.UI.Behaviors
{
    public class HyperlinkNavigationBehavior : Behavior<Hyperlink>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += new System.Windows.RoutedEventHandler(AssociatedObject_Click);
        }

        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(AssociatedObject.NavigateUri.ToString());
        }
    }
}
