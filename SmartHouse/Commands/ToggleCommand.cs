using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Commands
{
    public class ToggleCommand
    {
        protected readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public ToggleCommand(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public void Execute(object parameter) => _execute(parameter);

        public bool CanExecute(object parameter) => IsOn;

        bool isOn = false;
        public bool IsOn
        {
            get => isOn;
            set
            {
                isOn = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
