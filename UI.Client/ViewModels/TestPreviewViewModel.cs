using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using UI.Client.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace UI.Client.ViewModels
{
    public class TestPreviewViewModel : ViewModelBase
    {
        public TestPreviewViewModel(IUserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _user = user;

            InitializeCommands();
        }

        private IUserModel _user;
        private RelayCommand _goToTestSelectionCommand;

        public string Name
        {
            get
            {
                return _user.Name;
            }
        }

        public ICommand GoToTestSelectionCommand
        {
            get
            {
                return _goToTestSelectionCommand;
            }
        }

        private void InitializeCommands()
        {
            _goToTestSelectionCommand = new RelayCommand(
                () => GoToTestSelection()
            );
        }

        private void GoToTestSelection()
        {
            MessengerInstance.Send<Messages.NavigateTo>(
                new Messages.NavigateTo("TestSelectionView")
            );
        }
    }
}
