using System.Windows;
using SmartHouse.ViewModels;
using SmartHouse.Models;

namespace SmartHouse.Commands
{
    class AddInTableCommand : Command
    {

        public AddInTableCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public AddInTableCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            //Добавление датчиков в шаблон
            if (_templateDeviceViewModel != null)
            {

                if (_templateDeviceViewModel.SelectedSensor == null ||
                    _templateDeviceViewModel.SelectedParameter == null ||
                    _templateDeviceViewModel.CodeParameter == null || _templateDeviceViewModel.CodeParameter == "")
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string url = "http://h92761ae.beget.tech/add_sensor_of_device.php?id_temp_device=" + _templateDeviceViewModel.SelectedTemplate.Id + 
                             "&id_sensor=" + _templateDeviceViewModel.SelectedSensor.Id +
                             "&id_parameter=" + _templateDeviceViewModel.SelectedParameter.IdParameter +
                             "&code_parameter=" + _templateDeviceViewModel.CodeParameter + 
                             "&id_in_device=" + _templateDeviceViewModel.IdInDevice;

                if (!BackClient.SendRequest(url))
                    return;

                SensorsOfDevice sensorsOfDevice = new SensorsOfDevice
                {
                    NameSensor = _templateDeviceViewModel.SelectedSensor.Name,
                    NameParameter = _templateDeviceViewModel.SelectedParameter.NameParameter,
                    CodeParameter = _templateDeviceViewModel.CodeParameter,
                    IdInDevice = _templateDeviceViewModel.IdInDevice,
                    IdSensor = _templateDeviceViewModel.SelectedSensor.Id,
                    IdParameter = _templateDeviceViewModel.SelectedParameter.IdParameter,
                    IdTemplateDevice = _templateDeviceViewModel.SelectedTemplate.Id
                };

                _templateDeviceViewModel.SelectedTemplate.SensorsOfDevices.Add(sensorsOfDevice);
            }

            //Добавление параметров датчику
            if (_sensorViewModel != null)
            {
                if (_sensorViewModel.SelectedSensor == null ||
                    _sensorViewModel.SelectedParameterType == null)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string url = "http://h92761ae.beget.tech/add_sensor_parameter.php?id_parameter=" + _sensorViewModel.SelectedParameter.Id +
                              "&id_sensor=" + _sensorViewModel.SelectedSensor.Id + 
                              "&id_type_parameter=" + _sensorViewModel.SelectedParameterType.Id;

                if (!BackClient.SendRequest(url))
                    return;

                SensorParameter sensorParameter = new SensorParameter
                {
                    NameParameter = _sensorViewModel.SelectedParameter.Name,
                    NameTypeParameter = _sensorViewModel.SelectedParameterType.Name,
                    IdParameter = _sensorViewModel.SelectedParameter.Id,
                    IdTypeParameter = _sensorViewModel.SelectedParameterType.Id,
                    IdSensor = _sensorViewModel.SelectedSensor.Id
                };

                _sensorViewModel.SelectedSensor.SensorParameters.Add(sensorParameter);
            }
        }
    }
}
