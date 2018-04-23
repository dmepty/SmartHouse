using SmartHouse.Views;

namespace SmartHouse.Commands
{
    class OpenTemplateFirmwareWindow : Command
    {
        public OpenTemplateFirmwareWindow()
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            TemplateFirmwareView templateFirmwareView = new TemplateFirmwareView();
            templateFirmwareView.Show();
        }
    }
}
