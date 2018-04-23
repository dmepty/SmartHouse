using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System;
using PropertyChanged;
using SmartHouse.Commands;
using SmartHouse.Models;


namespace SmartHouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class SensorViewModel
    {
        public Sensor SelectedSensor { get; set; }
        public ObservableCollection<Sensor> Sensors { get; set; }

        public  SensorParameter SelectedSensorParameter { get; set; }
        public List<SensorParameter> SensorParameters { get; set; }

        public Parameter SelectedParameter { get; set; }
        public List<Parameter> Parameters { get; set; }

        public ParameterType SelectedParameterType { get; set; }
        public List<ParameterType> ParameterTypes { get; set; }


        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _toggleAddParameterCommand;
        private ICommand _deleteCommand;
        private ICommand _deleteParameterCommand;
        private ICommand _changeCommand;
        private ICommand _addParameterCommand;

        public SensorViewModel()
        {
            try
            {
                Sensors = BackClient.GetEntities<ObservableCollection<Sensor>>("json_sensors.php");
                SensorParameters = BackClient.GetEntities<List<SensorParameter>>("json_sensor_parameters.php");
                ParameterTypes = BackClient.GetEntities<List<ParameterType>>("json_parameter_type.php");
                Parameters = BackClient.GetEntities<List<Parameter>>("json_parameters.php");

                foreach (var sensor in Sensors)
                {
                    for (int i = 0; i < SensorParameters.Count; i++)
                    {
                        if (sensor.Id == SensorParameters[i].IdSensor)
                            sensor.SensorParameters.Add(SensorParameters[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand AddCommand => _addCommand ?? (_addCommand = new AddCommand(this));
        public ICommand ToggleAddParameterCommand => _toggleAddParameterCommand ?? (_toggleAddParameterCommand = new ToggleAddButtonCommand(this));
        public ICommand EditCommand => _editCommand ?? (_editCommand = new EditCommand(this));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DeleteCommand(this));

        public ICommand DeleteParameterCommand =>
            _deleteParameterCommand ?? (_deleteParameterCommand = new DeleteFromTableCommand(this));
        public ICommand ChangeCommand => _changeCommand ?? (_changeCommand = new ChangeCommand(this));
        public ICommand AddParameterCommand =>
            _addParameterCommand ?? (_addParameterCommand = new AddInTableCommand(this));
    }
}
