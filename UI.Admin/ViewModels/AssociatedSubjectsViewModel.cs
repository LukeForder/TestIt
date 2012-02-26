using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Logging;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Domain.Db;
using Validation;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;

namespace UI.Admin.ViewModels
{
    public class SubjectEventArgs : EventArgs
    {
        public SubjectEventArgs(AssociatedSubject subject)
        {
            _subject = subject;
        }

        private AssociatedSubject _subject;

        public AssociatedSubject Subject
        {
            get
            {
                return _subject;
            }
        }
    }

    public delegate void SubjectEventHandler(object sender, SubjectEventArgs e);

    public class AssociatedSubjectsViewModel : ViewModelExtended
    {
        public event SubjectEventHandler SubjectAddedEvent;
        public event SubjectEventHandler SubjectRemovedEvent;

        public AssociatedSubjectsViewModel(
            IEnumerable<AssociatedSubject> subjects, 
            IValidator<AssociatedSubject> subjectValidator,
            ISubjectQuery subjectsQuery,
            Guid busId)
        {
            if (subjectsQuery == null)
                throw new ArgumentNullException("subjectsQuery");

            if (subjectValidator == null)
                throw new ArgumentNullException("subjectValidator");

            if (subjects == null)
                throw new ArgumentNullException("subjects");

            _busId = busId;
            _subjectQuery = subjectsQuery;

            _subjectValidator = subjectValidator;
            _subjectQuery = subjectsQuery;

            InitializeSubjects(subjects);
            InitializeCommands();
        }

        private readonly Guid _busId;

        private ObservableCollection<AssociatedSubjectViewModel> _associatedSubjects;
        private ObservableCollection<Subject> _availableSubjects;
        private ISubjectQuery _subjectQuery;
        private IValidator<AssociatedSubject> _subjectValidator;

        private RelayCommand<Subject> _addAssociatedSubjectCommand;

        public ObservableCollection<AssociatedSubjectViewModel> AssociatedSubjects
        {
            get
            {
                return _associatedSubjects;
            }
            private set
            {
                _associatedSubjects = value;
                RaisePropertyChanged("AssociatedSubjects");
            }
        }

        public ObservableCollection<Subject> AvailableSubjects
        {
            get
            {
                return _availableSubjects;
            }
        }

        public ICommand AddAssociatedSubjectCommand
        {
            get
            {
                return _addAssociatedSubjectCommand;
            }
        }

        private void InitializeCommands()
        {
            _addAssociatedSubjectCommand = new RelayCommand<Subject>(
                subject =>
                {
                    AddAssociatedSubject(subject);
                },
                subject => 
                    subject != null && 
                    _availableSubjects.Contains(subject)
            );
        }

        private void AddAssociatedSubject(Subject subject)
        {
            var viewModel =
                new AssociatedSubjectViewModel(
                    new AssociatedSubject() { SubjectId = subject.Id, Points = 0 },
                    _subjectValidator, 
                    subject, 
                    _busId
                );

            RemoveAvailableSubject(subject);

            AttachEvents(viewModel);
            _associatedSubjects.Add(viewModel);

            RaiseSubjectAdded(viewModel.AssociatedSubject);

            NotifyDirtyHasChanged();
        }

        private void RemoveAvailableSubject(Subject subject)
        {

            _availableSubjects.Remove(subject);
            _addAssociatedSubjectCommand.RaiseCanExecuteChanged();
        }

        private void RemoveAssociatedSubject(AssociatedSubjectViewModel viewModel)
        {
            DettachEvents(viewModel);
            _associatedSubjects.Remove(viewModel);

            AddAvailableSubject(viewModel.AssociatedSubject.SubjectId);

            RaiseSubjectRemoved(viewModel.AssociatedSubject);
            NotifyDirtyHasChanged();
        }
        
        private void AddAvailableSubject(Guid id)
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                ()  => _subjectQuery.ById(id)
            )
            .ContinueWith(
                task =>
                {
                    if (task.Exception == null)
                    {
                        if (task.Result != null)
                        {
                            _availableSubjects.Add(task.Result);
                        }
                    }
                    else
                    {
                        _logger.Log(Level.Error, "Error occured reading subject from database");
                    }
                }, scheduler
            );            
        }

        private void AttachEvents(AssociatedSubjectViewModel viewModel)
        {
            viewModel.IsDirtyChangedEvent += IsDirtyChangedHandler;
            viewModel.RequestRemovalEvent += RequestRemovalHandler;
        }

        private void DettachEvents(AssociatedSubjectViewModel viewModel)
        {
            viewModel.IsDirtyChangedEvent -= IsDirtyChangedHandler;
            viewModel.RequestRemovalEvent -= RequestRemovalHandler;
        }

        void RequestRemovalHandler(object sender, EventArgs e)
        {
            AssociatedSubjectViewModel viewModel = sender as AssociatedSubjectViewModel;
            if (viewModel != null)
            {
                RemoveAssociatedSubject(viewModel);
            }
        }
        
        void IsDirtyChangedHandler(object sender, EventArgs e)
        {
            NotifyDirtyHasChanged();
        }

        private void NotifyDirtyHasChanged()
        {
            MessengerInstance.Send<Messages.IsDirty>(Messages.IsDirty.DirtyMessage, _busId);
        }

        private void InitializeSubjects(IEnumerable<AssociatedSubject> subjects)
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    var domainSubjects = _subjectQuery.All.Select(s => s).ToList();
                    List<AssociatedSubjectViewModel> viewModels = new List<AssociatedSubjectViewModel>();

                    foreach (var subject in subjects)
	                {
                        var domainSubject = domainSubjects.Where(s => s.Id == subject.SubjectId).FirstOrDefault();

                        if (domainSubject != null)
                        {
                            var viewModel = new AssociatedSubjectViewModel(
                                    subject, _subjectValidator, domainSubject, _busId
                                );

                            AttachEvents(viewModel);

                            viewModels.Add(
                                viewModel
                            );
                        }
	                }

                    return viewModels;
                }
            )
            .ContinueWith(
                task =>
                {
                    AssociatedSubjects = new ObservableCollection<AssociatedSubjectViewModel>(task.Result);
                    InitializeAvailableSubjects();
                },
                scheduler
            );
        }

        private void InitializeAvailableSubjects()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                   return
                    _subjectQuery
                        .All
                        .Where(s => !_associatedSubjects.Select(x => x.AssociatedSubject.SubjectId).Contains(s.Id))
                        .ToList();
                }
            )
            .ContinueWith(
                task =>
                {
                    if (task.Exception != null)
                    {
                        _logger.Log(Level.Error, "Failed to load subjects from database");
                    }
                    else
                    {
                        _availableSubjects = new ObservableCollection<Subject>(task.Result);
                       
                        CollectionViewSource.GetDefaultView(_availableSubjects).CurrentChanged += SelectedSubjectChanged;
                        
                        RaisePropertyChanged("AvailableSubjects");
                    }
                },
                scheduler
            );
        }

        void SelectedSubjectChanged(object sender, EventArgs e)
        {
            _addAssociatedSubjectCommand.RaiseCanExecuteChanged();
        }

        private void RaiseSubjectRemoved(AssociatedSubject subject)
        {
            var handler = SubjectRemovedEvent;
            if (handler != null)
                handler(this, new SubjectEventArgs(subject));
        }

        private void RaiseSubjectAdded(AssociatedSubject subject)
        {
            var handler = SubjectAddedEvent;
            if (handler != null)
                handler(this, new SubjectEventArgs(subject));
        }
    }
}
