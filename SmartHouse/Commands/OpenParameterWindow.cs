using SmartHouse.Views;

namespace SmartHouse.Commands
{
    class OpenParameterWindow : Command
    {
        public OpenParameterWindow()
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            ParameterView parameterView = new ParameterView();
            parameterView.Show();
        }
    }
}
