
namespace SmartHouse.Commands
{
    class OpenDeviceWindow : Command
    {
        public OpenDeviceWindow()
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            DeviceView deviceView = new DeviceView();
            deviceView.Show();
        }
    }
}
