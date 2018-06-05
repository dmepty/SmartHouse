using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace SmartHouse
{
    class WarningBehavior : Behavior<TextBlock>
    {

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.TargetUpdated += AssociatedObject_TargetUpdated;
        }

        
        private void AssociatedObject_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if (!String.IsNullOrEmpty(AssociatedObject.Text) && AssociatedObject.IsVisible)
            {
                string value = AssociatedObject.Text.TrimEnd('°');

                if (Int32.Parse(value) > 35)
                {
                    MessageBox.Show("Температура превысила допустимы значения! Температура: " + AssociatedObject.Text,
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    AssociatedObject.Foreground = Brushes.Crimson;
                }
                else
                {
                    AssociatedObject.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2195f2"));

                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.TargetUpdated -= AssociatedObject_TargetUpdated;
        }
    }
}
