using System.Windows;
using SmartHouse.ViewModels;

namespace SmartHouse.Commands
{
    class DeleteFromTableCommand : Command
    {
        public DeleteFromTableCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public DeleteFromTableCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_templateDeviceViewModel != null)
            {
                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                    MessageBoxResult.Yes)
                {
                    if (_templateDeviceViewModel.SelectedSensorOfDevice != null)
                    {
                        string url = "http://h92761ae.beget.tech/delete_sensor_of_device.php?id=" +
                                     _templateDeviceViewModel.SelectedSensorOfDevice.Id;

                        if (!BackClient.SendRequest(url))
                            return;

                        _templateDeviceViewModel.SelectedTemplate.SensorsOfDevices.Remove(_templateDeviceViewModel
                            .SelectedSensorOfDevice);

                        MessageBox.Show("Удаление успешно!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
            }

            if (_sensorViewModel != null)
            {
                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                    MessageBoxResult.Yes)
                {
                    if (_sensorViewModel.SelectedSensorParameter != null)
                    {
                        string url = "http://h92761ae.beget.tech/delete_sensor_parameter.php?id=" +
                                     _sensorViewModel.SelectedSensorParameter.Id;

                        if (!BackClient.SendRequest(url))
                            return;

                        _sensorViewModel.SelectedSensor.SensorParameters.Remove(_sensorViewModel.SelectedSensorParameter);

                        MessageBox.Show("Удаление успешно!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
            }
        }
    }
}
