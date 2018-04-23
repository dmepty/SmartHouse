using SmartHouse.Views;

namespace SmartHouse.Commands
{
    class OpenSensorWindow : Command
    {
        public OpenSensorWindow()
        {

        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            SensorView sensorView = new SensorView();
            sensorView.Show();
        }
    }
}
