using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Domain;
using GalaSoft.MvvmLight.Command;
using Validation;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace UI.Admin.ViewModels
{
    public class EditingSubjectViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler ChangesSavedEvent;
        public event EventHandler ChangesCanceledEvent;

        private readonly string _nameSnapShot;
        private readonly IValidator<Subject> _validator;
        private readonly Subject _subject;

        private readonly RelayCommand _saveChangesCommand;
                
        public EditingSubjectViewModel(Subject instance, IValidator<Subject> validator)
        {
            _subject = instance;
            _nameSnapShot = instance.Name;
            _validator = validator;
            _saveChangesCommand =
                    new RelayCommand(
                            () => RaiseEvent(ChangesSavedEvent),
                            () => CanSaveChanges()
                        );
        }

        public ICommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand;
            }
        }

        private bool CanSaveChanges()
        {
            return _validator.Validate(_subject).IsValid;
        }

        public ICommand CancelChangesCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _subject.Name = _nameSnapShot;
                    RaiseEvent(ChangesCanceledEvent);
                });
            }
        }

        private void RaiseEvent(EventHandler @event)
        {
            var handler = @event;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public Subject Subject
        {
            get
            {
                return _subject;
            }
        }

        public string Name
        {
            get
                {
                return _subject.Name;
            }
            set
            {
                if (_subject.Name != value)
                {
                    _subject.Name = value;

                    RaisePropertyChanged("Name");
                    _saveChangesCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                var error = _validator.ValidateProperty(_subject, columnName).Errors.FirstOrDefault();

                return (error == null) ? null : error.Message;
            }
        }
    }
}
