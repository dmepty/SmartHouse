using SmartHouse.ViewModels;
using SmartHouse.Models;
using System.Linq;
using System.Windows;

namespace SmartHouse.Commands
{
    class AddCommand : Command
    {
        public AddCommand(DeviceViewModel deviceView) : base(deviceView)
        {
        }

        public AddCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public AddCommand(TemplateFirmwareViewModel templateFirmwareViewModel) : base(templateFirmwareViewModel)
        {
        }

        public AddCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            //Добавление устройства
            if (_deviceViewModel != null)
            {
                var device = new Device
                {
                    IsEdit = true,
                    IsNew = true,
                    Id = _deviceViewModel.Devices.Last().Id + 1,
                    CodeDevice = "Новое устройство",
                    IdTemplateDevice = 1
                };

                _deviceViewModel.Devices.Add(device);
                _deviceViewModel.SelectedDevice = device;

                var url = "http://h92761ae.beget.tech/add_device.php?id=" + device.Id +
                          "&id_temp_device=" + device.IdTemplateDevice +
                          "&code_device=" + _deviceViewModel.SelectedDevice.CodeDevice;

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Новое устройство добавленно", "Добавление", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
            }

            //Добавление шаблона устройства
            if(_templateDeviceViewModel != null)
            {
                TemplateDevice template = new TemplateDevice
                {
                    IsEdit = true,
                    Id = _templateDeviceViewModel.TemplatesDevices.Last().Id + 1,
                    Name = "Новый шаблон",
                    IdControllers = 1,
                    IdTemplateFirmware = 1
                };

                _templateDeviceViewModel.TemplatesDevices.Add(template);
                _templateDeviceViewModel.SelectedTemplate = template;

                string url = "http://h92761ae.beget.tech/add_template_device.php?id=" + template.Id + 
                             "&name_temp_device=" + template.Name +
                             "&id_controller=" + template.IdControllers +
                             "&id_temp_firmware=" + template.IdTemplateFirmware;

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Новый шаблон устройства добавлен", "Добавление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            //Доабвление шаблона прошивки
            if (_templateFirmwareViewModel != null)
            {
                TemplateFirmware templateFirmware = new TemplateFirmware
                {
                    IsEdit = true,
                    Id = _templateFirmwareViewModel.TemplatesFirmware.Last().Id + 1,
                    Name = "Новый шаблон",
                    Version = "1.0.0",
                    TextTemplate = "*TEXT*"
                };

                _templateFirmwareViewModel.TemplatesFirmware.Add(templateFirmware);
                _templateFirmwareViewModel.SelectedTemplate = templateFirmware;

                string url = "http://h92761ae.beget.tech/add_template_firmware.php?id=" + templateFirmware.Id +
                             "&name=" + templateFirmware.Name +
                             "&version=" + templateFirmware.Version +
                             "&text_template=" + templateFirmware.TextTemplate;

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Новый шаблон добавлен", "Добавление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            //Доабвление датчика
            if (_sensorViewModel != null)
            {
                Sensor sensor = new Sensor
                {
                    IsEdit = true,
                    IsNew = true,
                    Id = _sensorViewModel.Sensors.Last().Id + 1,
                    Name = "Новый датчик"
                };

                _sensorViewModel.Sensors.Add(sensor);
                _sensorViewModel.SelectedSensor = sensor;

                string url = "http://h92761ae.beget.tech/add_sensor.php?id=" + sensor.Id +
                             "&name=" + sensor.Name;

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Новый датчик добавлен", "Добавление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }


        }
    }
}