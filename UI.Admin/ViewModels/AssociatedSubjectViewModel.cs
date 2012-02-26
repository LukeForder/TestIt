using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Domain;
using Domain.Db;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Validation;
using System.ComponentModel;

namespace UI.Admin.ViewModels
{
    public class AssociatedSubjectViewModel : ViewModelExtended, IDataErrorInfo
    {
        public event EventHandler RequestRemovalEvent;

        public AssociatedSubjectViewModel(
            AssociatedSubject associatedSubject,
            IValidator<AssociatedSubject> subjectValidator,
            Subject subject,
            Guid busId)
        {
            if (subject == null)
                throw new ArgumentNullException("subject");

            if (associatedSubject == null)
                throw new ArgumentNullException("associatedSubject");

            this._subjectValidator = subjectValidator;
            this._associatedSubject = associatedSubject;
            this._subject = subject;
            this._busId = busId;

            InitializeCommands();
        }
                
        private readonly Guid _busId;

        private IValidator<Domain.AssociatedSubject> _subjectValidator;
        private AssociatedSubject _associatedSubject;
        private Subject _subject;

        private RelayCommand _requestRemovalCommand;

        public AssociatedSubject AssociatedSubject
        {
            get
            {
                return _associatedSubject;
            }
        }

        public string Subject
        {
            get
            {
                return _subject.Name;
            }
        }

        public int Marks
        {
            get
            {
                return _associatedSubject.Points;
            }
            set
            {
                if (_associatedSubject.Points != value)
                {
                    _associatedSubject.Points = value;
                    RaisePropertyChanged("Marks");
                }
            }
        }

        public ICommand RequestRemovalCommand
        {
            get
            {
                return _requestRemovalCommand;
            }
        }

        private void InitializeCommands()
        {
            _requestRemovalCommand =
                new RelayCommand(
                    () =>
                    {
                        RaiseRequestRemoval();
                    }
                );
        }

        private void RaiseRequestRemoval()
        {
            var handler = RequestRemovalEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
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
                if (columnName == "Points")
                    return _subjectValidator.ValidateProperty(_associatedSubject, "Points")
                        .Errors
                            .Select(e => e.Message)
                            .FirstOrDefault();

                return null;
            }
        }
    }
}
