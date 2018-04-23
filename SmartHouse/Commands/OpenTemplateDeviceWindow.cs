using SmartHouse.Views;

namespace SmartHouse.Commands
{
    class OpenTemplateDeviceWindow : Command
    {
        public OpenTemplateDeviceWindow()
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            TemplateDeviceView templateDeviceView = new TemplateDeviceView();
            templateDeviceView.Show();
        }
    }
}
