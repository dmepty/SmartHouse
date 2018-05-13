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
        
        public string Name { get; set; }
        public string Description { get; set; }

        private ICommand _addParameterCommand;


        public ParameterViewModel()
        {
            Parameters = BackClient.GetEntities<ObservableCollection<Parameter>>("json_parameters.php");
        }

        public ICommand AddParameterCommand =>
            _addParameterCommand ?? (_addParameterCommand = new AddInTableCommand(this));

    }
}
