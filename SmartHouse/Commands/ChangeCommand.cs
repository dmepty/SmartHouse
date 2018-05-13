using System.Linq;
using SmartHouse.ViewModels;
using System.Windows;
using Newtonsoft.Json;

namespace SmartHouse.Commands
{
    class ChangeCommand : Command
    {
        public ChangeCommand(DeviceViewModel deviceView) : base(deviceView)
        {
        }

        public ChangeCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public ChangeCommand(TemplateFirmwareViewModel templateFirmwareViewModel) : base(templateFirmwareViewModel)
        {
        }

        public ChangeCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
        {
        }

        public ChangeCommand(ParameterViewModel parameterViewModel) : base(parameterViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            //Изменение устройства
            if (_deviceViewModel != null)
            {
                var url = "http://h92761ae.beget.tech/change_device.php?id=" + _deviceViewModel.SelectedDevice.Id +
                          "&id_temp_device=" + _deviceViewModel.SelectedDevice.TemplateDevice.Id +
                          "&id_temp_firmware=" + _deviceViewModel.SelectedDevice.TemplatesFirmwares.Id +
                          "&name_temp=" + _deviceViewModel.SelectedDevice.TemplateDeviceName +
                          "&code_device=" + _deviceViewModel.SelectedDevice.CodeDevice +
                          "&description=" + _deviceViewModel.SelectedDevice.Description;

                if (_deviceViewModel.SelectedDevice.CodeDevice == "")
                {
                    MessageBox.Show("Поле 'Код устройства' обязательно для заполнения!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Сохранение успешно!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);


                _deviceViewModel.SelectedDevice.IsEdit = false;
            }

            //Изменение шаблона устройства
            if (_templateDeviceViewModel != null)
            {
                var selected = _templateDeviceViewModel.SelectedTemplate;

                var jsonObject = JsonConvert.SerializeObject(selected.SensorsOfDevices);


                var url = "http://h92761ae.beget.tech/change_template_device.php?id_dev=" + selected.Id +
                             "&name_dev=" + selected.Name +
                             "&id_controllers=" + selected.Controllers.Id +
                             "&id_template_firmware=" + selected.TemplatesFirmwares.Id +
                             "&name_firm=" + selected.TemplatesFirmwares.Name +
                             "&version=" + selected.TemplatesFirmwares.Version +
                             "&text_template=" + selected.TemplatesFirmwares.TextTemplate;

                var urlPost = "http://h92761ae.beget.tech/rewrite_sensors.php";


                if (selected.Name == "")
                {
                    MessageBox.Show("Поле 'Код устройства' обязательно для заполнения!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (selected.TemplatesFirmwares.Name == "")
                {
                    MessageBox.Show("Поле 'Название прошивки' обязательно для заполнения!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (selected.TemplatesFirmwares.Version == "")
                {
                    MessageBox.Show("Поле 'Версия' обязательно для заполнения!", "Ошибка", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }

                if (!BackClient.SendRequest(url))
                    return;

                if (!BackClient.SendPostRequest(urlPost, jsonObject))
                    return;


                MessageBox.Show("Сохранение успешно!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);


                selected.IsEdit = false;
            }

            //Изменение шаблона прошивки
            if (_templateFirmwareViewModel != null)
            {
                var selected = _templateFirmwareViewModel.SelectedTemplate;

                var url = "http://h92761ae.beget.tech/change_template_firmware.php?id=" + selected.Id +
                             "&name=" + selected.Name +
                             "&version=" + selected.Version +
                             "&text_template=" + selected.TextTemplate;

                if (selected.Name == "")
                {
                    MessageBox.Show("Поле 'Название прошивки' обязательно для заполнения!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (selected.Version == "")
                {
                    MessageBox.Show("Поле 'Версия' обязательно для заполнения!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (selected.TextTemplate == "")
                {
                    MessageBox.Show("Поле 'Текст прошивки' обязательно для заполнения!", "Ошибка", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Сохранение успешно!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);


                selected.IsEdit = false;
            }

            //Изменение датчика
            if (_sensorViewModel != null)
            {
                var selected = _sensorViewModel.SelectedSensor;

                var url = "http://h92761ae.beget.tech/change_sensor.php?id=" + selected.Id +
                             "&name=" + selected.Name +
                             "&description=" + selected.Description;


                if (selected.Name == "")
                {
                    MessageBox.Show("Поле 'Название' обязательно для заполнения!", "Ошибка", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }

                if (!BackClient.SendRequest(url))
                    return;

                MessageBox.Show("Сохранение успешно!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
        }
    }
}
