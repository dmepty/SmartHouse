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

        public int CountParamsAtStart { get; set; }

        private ICommand _changeCommand;


        public ParameterViewModel()
        {
            Parameters = BackClient.GetEntities<ObservableCollection<Parameter>>("json_parameters.php");

            CountParamsAtStart = Parameters.Count;
        }

        public ICommand ChangeCommand => _changeCommand ?? (_changeCommand = new ChangeCommand(this));
    }
}
