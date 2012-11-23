using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace NewsStand.UI.Behaviors
{
    public class LabelClickBehavior : Behavior<Label>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(AssociatedObject.Content.ToString());
        }
    }
}
