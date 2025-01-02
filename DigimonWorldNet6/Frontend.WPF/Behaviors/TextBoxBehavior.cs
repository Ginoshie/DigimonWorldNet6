using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace DigimonWorld.Frontend.WPF.Behaviors
{
    public class TextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += OnGotFocus;
            AssociatedObject.LostFocus += OnLostFocus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= OnGotFocus;
            AssociatedObject.LostFocus -= OnLostFocus;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.Text == "0")
            {
                AssociatedObject.Text = string.Empty;
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AssociatedObject.Text))
            {
                AssociatedObject.Text = "0";
            }
        }
    }
}