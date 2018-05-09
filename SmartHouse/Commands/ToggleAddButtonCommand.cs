using SmartHouse.ViewModels;
using SmartHouse.Models;
using System.Linq;
using System.Windows;

namespace SmartHouse.Commands
{
    class ToggleAddButtonCommand : Command
    {
        public ToggleAddButtonCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public ToggleAddButtonCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
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
                _templateDeviceViewModel.SelectedTemplate.IsNewSensor = !_templateDeviceViewModel.SelectedTemplate.IsNewSensor;
            }

            if(_sensorViewModel != null)
            {
                _sensorViewModel.SelectedSensor.IsNewParameter = !_sensorViewModel.SelectedSensor.IsNewParameter;
            }
        }
    }
}
