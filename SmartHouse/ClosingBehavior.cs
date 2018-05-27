using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Forms; 
using System.Drawing;


namespace SmartHouse
{
    class ClosingBehavior : Behavior<Window>
    {
        public static NotifyIcon NotifyIcon { get; private set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            NotifyIcon = new NotifyIcon();
            NotifyIcon.Icon = new Icon("icon.ico");
            NotifyIcon.DoubleClick += (sender, args) => AssociatedObject.Show();

            AssociatedObject.Closing += AssociatedObjectOnClosing;
        }

        private void AssociatedObjectOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            if (System.Windows.MessageBox.Show("Свернуть в трей?", "Закрытие", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                AssociatedObject.Hide();
                NotifyIcon.Visible = true;
                cancelEventArgs.Cancel = true;
            }

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Closing -= AssociatedObjectOnClosing;
        }
    }
}
