using System.Collections.ObjectModel;
using System.Windows.Input;
using PropertyChanged;
using SmartHouse.Commands;
using SmartHouse.Models;
using System.Windows;
using System;

namespace SmartHouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class TemplateFirmwareViewModel
    {
        public TemplateFirmware SelectedTemplate { get; set; }
        public ObservableCollection<TemplateFirmware> TemplatesFirmware { get; set; }

        private ICommand _editCommand;
        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _changeCommand;

        public TemplateFirmwareViewModel()
        {
            try
            {
                TemplatesFirmware = BackClient.GetEntities<ObservableCollection<TemplateFirmware>>("json_firmwares.php");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand EditCommand => _editCommand ?? (_editCommand = new EditCommand(this));
        public ICommand AddCommand => _addCommand ?? (_addCommand = new AddCommand(this));
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DeleteCommand(this));
        public ICommand ChangeCommand => _changeCommand ?? (_changeCommand = new ChangeCommand(this));
    }
}
