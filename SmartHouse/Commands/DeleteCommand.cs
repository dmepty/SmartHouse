using System.Net;
using System.Text;
using System.Windows;
using SmartHouse.ViewModels;

namespace SmartHouse.Commands
{
    class DeleteCommand : Command
    {
        public DeleteCommand(DeviceViewModel deviceView) : base(deviceView)
        {
        }

        public DeleteCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public DeleteCommand(TemplateFirmwareViewModel templateFirmwareViewModel) : base(templateFirmwareViewModel)
        {
        }

        public DeleteCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Удаление устройства
                if (_deviceViewModel?.SelectedDevice != null)
                {
                    string url = "http://h92761ae.beget.tech/delete_device.php?id_device=" + _deviceViewModel.SelectedDevice.Id;



                    if (!BackClient.SendRequest(url))
                        return;

                    _deviceViewModel.Devices.Remove(_deviceViewModel.SelectedDevice);

                    MessageBox.Show("Удаление успешно!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                //Удаление шаблона устройства
                if (_templateDeviceViewModel?.SelectedTemplate != null)
                {
                    string url = "http://h92761ae.beget.tech/delete_template_device.php?id_device=" + _templateDeviceViewModel.SelectedTemplate.Id;



                    if (!BackClient.SendRequest(url))
                        return;

                    _templateDeviceViewModel.TemplatesDevices.Remove(_templateDeviceViewModel.SelectedTemplate);

                    MessageBox.Show("Удаление успешно!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                //Удаление шаблона прошивки
                if (_templateFirmwareViewModel?.SelectedTemplate != null)
                {
                    string url = "http://h92761ae.beget.tech/delete_template_firmware.php?id=" + _templateFirmwareViewModel.SelectedTemplate.Id;



                    if (!BackClient.SendRequest(url))
                        return;

                    _templateFirmwareViewModel.TemplatesFirmware.Remove(_templateFirmwareViewModel.SelectedTemplate);

                    MessageBox.Show("Удаление успешно!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                //Удаление датчика
                if (_sensorViewModel?.SelectedSensor != null)
                {
                    string url = "http://h92761ae.beget.tech/delete_sensor.php?id=" + _sensorViewModel.SelectedSensor.Id;

                    if (!BackClient.SendRequest(url))
                        return;

                    _sensorViewModel.Sensors.Remove(_sensorViewModel.SelectedSensor);

                    MessageBox.Show("Удаление успешно!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

    }
}
