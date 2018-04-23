using System;
using System.Windows.Input;
using SmartHouse.ViewModels;

namespace SmartHouse.Commands
{
    abstract class Command : ICommand
    {
        protected DeviceViewModel _deviceViewModel;
        protected TemplateDeviceViewModel _templateDeviceViewModel;
        protected TemplateFirmwareViewModel _templateFirmwareViewModel;
        protected SensorViewModel _sensorViewModel;
        protected ParameterViewModel _parameterViewModel;

        public Command()
        {
        }

        public Command(DeviceViewModel deviceViewModel)
        {
            _deviceViewModel = deviceViewModel;
        }

        public Command(TemplateDeviceViewModel templateDeviceViewModel)
        {
            _templateDeviceViewModel = templateDeviceViewModel;
        }

        public Command(TemplateFirmwareViewModel templateFirmwareViewModel)
        {
            _templateFirmwareViewModel = templateFirmwareViewModel;
        }

        public Command(SensorViewModel sensorViewModel)
        {
            _sensorViewModel = sensorViewModel;
        }

        public Command(ParameterViewModel parameterViewModel)
        {
            _parameterViewModel = parameterViewModel;
        }


        public event EventHandler CanExecuteChanged;
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }
}