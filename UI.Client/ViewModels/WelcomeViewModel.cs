using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace UI.Client.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            InitializeCommands();
        }

        private RelayCommand _gotoUserRegistrationCommand;

        public ICommand GoToUserRegistrationCommand
        {
            get
            {
                return _gotoUserRegistrationCommand;
            }
        }

        #region Initialization

        private void InitializeCommands()
        {
            _gotoUserRegistrationCommand = new RelayCommand(
                GoToUserRegistration
            );
        }

        #endregion

        #region Command Callbacks

        private void GoToUserRegistration()
        {
            MessengerInstance.Send<Messages.NavigateTo>(
                new Messages.NavigateTo("UserRegistrationView")
            );
        }

        #endregion
    }
}
