using System.Collections.ObjectModel;
using System.Windows.Input;
using PropertyChanged;
using SmartHouse.Commands;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class ParameterViewModel
    {
        public ObservableCollection<Parameter> Parameters { get; set; }

        public bool NewParameter { get; set; }

        public ICommand _toggleAddparameterCommand;

        public ParameterViewModel()
        {
            Parameters = BackClient.GetEntities<ObservableCollection<Parameter>>("json_parameters.php");
        }

        public ICommand ToggleAddParameterCommand =>
            _toggleAddparameterCommand ?? (_toggleAddparameterCommand = new ToggleAddButtonCommand(this));
    }
}
