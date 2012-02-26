using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System.Windows;

namespace UI.Admin.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            MessengerInstance.Register<Messages.IsDirty>(this, msg => DirtyMessageHandler(msg));
        }

        private string _currentView;
        private bool _isDirty;

        public string CurrentView
        {
            get
            {
                return _currentView;
            }
        }

        public ICommand GoToSubjects
        {
            get
            {
                return new RelayCommand(() => RequestNavigateTo("SubjectsView"));
            }
        }

        public ICommand GoToTests
        {
            get
            {
                return new RelayCommand(() => RequestNavigateTo("TestsView"));
            }
        }

        private void RequestNavigateTo(string view, bool checkDirty = true)
        {
            if (checkDirty && _isDirty)
            {
                var navigationConfirmationDialog = new DialogMessage(
                    "The current page has unsaved changes, if you continue\nthese changes will be lost. Do you want to continue?",
                    result =>
                    {
                        if (result == MessageBoxResult.Yes)
                        {
                            RequestNavigateTo(view, false);
                        }
                    }
                )
                {
                    Button = MessageBoxButton.YesNo,
                    Caption = "Are you sure?"
                };

                MessengerInstance.Send<DialogMessage>(navigationConfirmationDialog);
            }
            else
            {
                MessengerInstance.Send<Messages.NavigateTo>(new Messages.NavigateTo(view));

                _isDirty = false;
                MessengerInstance.Send<Messages.Navigating>(new Messages.Navigating(), CurrentView);
                
                _currentView = view;
                RaisePropertyChanged("CurrentView");
            }

        }

        private void  DirtyMessageHandler(Messages.IsDirty msg)
        {
            if (msg.Dirty != _isDirty)
            {
                _isDirty = msg.Dirty;
                RaisePropertyChanged("NavigationRequiresConfirmation");
            }
        }
    }
}
