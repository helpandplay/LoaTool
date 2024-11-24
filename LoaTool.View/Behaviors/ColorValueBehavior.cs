using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LoaTool.Define.Enums;
using LoaTool.Define.Structs;
using Microsoft.Xaml.Behaviors;

namespace LoaTool.View.Behaviors
{
    public class ColorValueBehavior : Behavior<Slider>
    {
        private static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(RGB), typeof(ColorValueBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty ChangeColorValueCommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ColorValueBehavior), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(ChangeColorValueCommandProperty); }
            set { SetValue(ChangeColorValueCommandProperty, value); }
        }

        public RGB Color
        {
            get { return (RGB)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ValueChanged += AssociatedObject_ValueChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ValueChanged -= AssociatedObject_ValueChanged;
        }

        private void AssociatedObject_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            //문 성윤
            if(Command is not null)
            {
                var rGBValueStruct = new RGBValueStruct(Color, (int)e.NewValue);
                Command.Execute(rGBValueStruct);
            }
        }
    }
}
