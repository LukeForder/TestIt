using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Domain;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace UI.Admin.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        public event EventHandler RequestDeletionEvent;
        public event EventHandler RequestEditCommand;

        public SubjectViewModel(Subject subject)
        {
            _subject = subject;
        }

        private readonly Subject _subject;

        public Subject Subject
        {
            get
            {
                return _subject;
            }
        }

        public ICommand EditSubjectCommand
        {
            get
            {
                return new RelayCommand(
                    () => RaiseEvent(RequestEditCommand)
                );
            }
        }

        public ICommand DeleteSubjectCommand
        {
            get
            {
                return new RelayCommand(
                    () => RaiseEvent(RequestDeletionEvent)
                );
            }
        }

        private void RaiseEvent(EventHandler eventDelegate)
        {
            var handler = eventDelegate;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
