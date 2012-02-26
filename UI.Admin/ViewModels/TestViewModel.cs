using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace UI.Admin.ViewModels
{
    public class TestViewModel : ViewModelExtended
    {
        public event EventHandler RequestEditEvent;
        public event EventHandler RequestDeleteEvent;

        public TestViewModel(Test test)
            : base()
        {
            _test = test;
        }

        private Test _test;

        internal Test Test
        {
            get
            {
                return _test;
            }
        }

        public string Name
        {
            get
            {
                return _test.Name;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand
                (
                    () => RaiseEvent(RequestDeleteEvent)
                );
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand
                (
                    () => RaiseEvent(RequestEditEvent)
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
