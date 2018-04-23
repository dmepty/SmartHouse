using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using PropertyChanged;
using SmartHouse.Commands;
using SmartHouse.Models;
using System.Windows;
using System;

namespace SmartHouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class TemplateDeviceViewModel
    {
        public TemplateDevice SelectedTemplate { get; set; }
        public ObservableCollection<TemplateDevice> TemplatesDevices { get; set; }

        public List<TemplateFirmware> TemplatesFirmwares { get; set; }
        public List<Controller> Controllers { get; set; }

        public Sensor SelectedSensor { get; set; }
        public List<Sensor> Sensors { get; set; }

        public SensorParameter SelectedParameter { get; set; }

        public List<Parameter> Parameters { get; set; }

        public List<SensorParameter> SensorParameters { get; set; }

        public SensorsOfDevice SelectedSensorOfDevice { get; set; }

        public string CodeParameter { get; set; }
        public int IdInDevice { get; set; }

        private ICommand _editCommand;
        private ICommand _addCommand;
        private ICommand _changeCommand;
        private ICommand _deleteCommand;
        private ICommand _toggleAddSensorCommand;
        private ICommand _addSensorCommand;
        private ICommand _deleteSensorCommand;

        public TemplateDeviceViewModel()
        {
            try
            {
                TemplatesDevices = BackClient.GetEntities<ObservableCollection<TemplateDevice>>("json_template_device.php");

                TemplatesFirmwares = BackClient.GetEntities<List<TemplateFirmware>>("json_firmwares.php");
                Controllers = BackClient.GetEntities<List<Controller>>("json_controllers.php");
                Sensors = BackClient.GetEntities<List<Sensor>>("json_sensors.php");
                Parameters = BackClient.GetEntities<List<Parameter>>("json_parameters.php");
                SensorParameters = BackClient.GetEntities<List<SensorParameter>>("json_sensor_parameters.php");
                var sensorsOfDevices = BackClient.GetEntities<List<SensorsOfDevice>>("json_sensors_of_device.php");


                foreach (var sensor in Sensors)
                {
                    foreach (var SensorParameter in SensorParameters)
                    {
                        if (sensor.Id == SensorParameter.IdSensor)
                        {
                            sensor.SensorParameters.Add(SensorParameter);
                        }
                    }
                }

                foreach (var templateDevice in TemplatesDevices)
                {
                    templateDevice.TemplatesFirmwares = TemplatesFirmwares[templateDevice.IdTemplateFirmware - 1];
                    templateDevice.Controllers = Controllers[templateDevice.IdControllers - 1];



                    foreach (var sensor in sensorsOfDevices)
                    {
                        if (templateDevice.Id == sensor.IdTemplateDevice)
                            templateDevice.SensorsOfDevices?.Add(sensor);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public ICommand EditCommand => _editCommand ?? (_editCommand = new EditCommand(this));
        public ICommand AddCommand => _addCommand ?? (_addCommand = new AddCommand(this));
        public ICommand ChangeCommand => _changeCommand ?? (_changeCommand = new ChangeCommand(this));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DeleteCommand(this));
        public ICommand ToggleAddSensorCommand => _toggleAddSensorCommand ?? (_toggleAddSensorCommand = new ToggleAddButtonCommand(this));
        public ICommand AddSensorCommand => _addSensorCommand ?? (_addSensorCommand = new AddInTableCommand(this));
        public ICommand DeleteSensorCommand => _deleteSensorCommand ?? (_deleteSensorCommand = new DeleteFromTableCommand(this));
    }
}
