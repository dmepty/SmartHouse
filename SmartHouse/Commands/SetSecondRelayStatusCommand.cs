using SmartHouse.ViewModels;

namespace SmartHouse.Commands
{
    class SetSecondRelayStatusCommand : Command
    {
        public SetSecondRelayStatusCommand(MainViewModel mainViewModel) : base(mainViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_mainViewModel != null)
            {
                _mainViewModel.SecondRelayStatus = !_mainViewModel.SecondRelayStatus;

                var firstRelayStatus = _mainViewModel.FirstRelayStatus ? 1 : 0;
                var secondRelayStatus = _mainViewModel.SecondRelayStatus ? 1 : 0;

                string url = "http://h92761ae.beget.tech/set_rele_status.php?releone=" + firstRelayStatus +
                             "&reletwo=" + secondRelayStatus;

                BackClient.SendRequest(url);
            }
        }
    }
}
