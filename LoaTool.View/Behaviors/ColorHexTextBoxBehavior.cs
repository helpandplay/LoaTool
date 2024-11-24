using System.Text.RegularExpressions;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace LoaTool.View.Behaviors
{
    public class ColorHexTextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox textBox)
            {
                if(IsHexValue(textBox.Text))
                {

                }
            }
        }

        private bool IsHexValue(string text)
        {
            if(string.IsNullOrEmpty(text)) return false;

            if(!text.StartsWith("#"))
            {
                text = text.Replace("", "#");
            }

            if(text.Length >= 8)
            {
                return false;
            }

            if(text.Length <= 7)
            {
                text = text.PadRight(7, '0');
            }

            return Regex.IsMatch(text, @"#^(?:[0-9a-fA-F]{3}){1,2}$");
        }
    }
}
