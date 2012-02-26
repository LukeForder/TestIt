using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using UI.Client.Models;

namespace UI.Client.ViewModels
{
    public class UserRegistrationViewModel : ViewModelBase
    {
        public UserRegistrationViewModel(IUserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _user = user;

            InitializeCommands();
        }

        private RelayCommand _goToTestPreviewCommand;
        private IUserModel _user;

        public ICommand GoToTestPreviewCommand
        {
            get
            {
                return _goToTestPreviewCommand;
            }
        }

        public string Name
        {
            get
            {
                return _user.Name;
            }
            set
            {
                if (_user.Name != value)
                {
                    _user.Name = value;

                    RaisePropertyChanged("Name");
                    _goToTestPreviewCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #region Initialization

        private void InitializeCommands()
        {
            _goToTestPreviewCommand = new RelayCommand(
                () => GoToTestPreview(),
                () => CanGoToTestPreview()
            );
        }

        #endregion

        #region Command Callbacks

        private bool CanGoToTestPreview()
        {
            return !string.IsNullOrEmpty(_user.Name);
        }

        private void GoToTestPreview()
        {
            Messages.NavigateTo navigationMessage 
                = new Messages.NavigateTo("TestPreviewView");

            MessengerInstance.Send<Messages.NavigateTo>(navigationMessage);
        }

        #endregion
    }
}
