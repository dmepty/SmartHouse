using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using System;
using PropertyChanged;
using SmartHouse.Commands;
using SmartHouse.Models;


namespace SmartHouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class DeviceViewModel
    {
        public Device SelectedDevice { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public List<TemplateFirmware> TemplatesFirmwares { get; set; }
        public List<TemplateDevice> TemplateDevices { get; set; }

        private ICommand _addCommand;
        private ICommand _changeCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;

        public DeviceViewModel()
        {
            try
            {
                Devices = BackClient.GetEntities<ObservableCollection<Device>>("json_devices.php");

                TemplatesFirmwares = BackClient.GetEntities<List<TemplateFirmware>>("json_firmwares.php");
                TemplateDevices = BackClient.GetEntities<List<TemplateDevice>>("json_template_device.php");
                var sensorsOfDevices = BackClient.GetEntities<List<SensorsOfDevice>>("json_sensors_of_device.php");


                foreach (var device in Devices)
                {
                    device.TemplatesFirmwares = TemplatesFirmwares[device.IdTemplateFirmware - 1];
                    device.TemplateDevice = TemplateDevices[device.IdTemplateDevice - 1];

                    foreach (var sensor in sensorsOfDevices)
                    {
                        if (device.IdTemplateDevice == sensor.IdTemplateDevice && !device.SensorsOfDevices.Any(item => item.IdInDevice == sensor.IdInDevice))
                            device.SensorsOfDevices.Add(sensor);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public ICommand AddDevice => _addCommand ?? (_addCommand = new AddCommand(this));
        public ICommand ChangeCommand => _changeCommand ?? (_changeCommand = new ChangeCommand(this));
        public ICommand EditCommand => _editCommand ?? (_editCommand = new EditCommand(this));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DeleteCommand(this));
    }
}
