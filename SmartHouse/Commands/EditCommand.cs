using SmartHouse.ViewModels;

namespace SmartHouse.Commands
{
    class EditCommand : Command
    {
        public EditCommand(DeviceViewModel deviceView) : base(deviceView)
        {
        }

        public EditCommand(TemplateDeviceViewModel templateDeviceViewModel) : base(templateDeviceViewModel)
        {
        }

        public EditCommand(SensorViewModel sensorViewModel) : base(sensorViewModel)
        {
        }

        public EditCommand(TemplateFirmwareViewModel templateFirmwareViewModel) : base(templateFirmwareViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if(_deviceViewModel?.SelectedDevice != null)
                _deviceViewModel.SelectedDevice.IsEdit = true;

            if(_templateDeviceViewModel?.SelectedTemplate != null)
                _templateDeviceViewModel.SelectedTemplate.IsEdit = true;

            if (_sensorViewModel?.SelectedSensor != null)
                _sensorViewModel.SelectedSensor.IsEdit = true;

            if (_templateFirmwareViewModel?.SelectedTemplate != null)
                _templateFirmwareViewModel.SelectedTemplate.IsEdit = true;
        }
    }
}
