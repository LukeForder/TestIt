using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Domain.Db;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using Logging;
using UI.Admin.Messages;
using Validation;
using Domain;

namespace UI.Admin.ViewModels
{
    public class SubjectsViewModel : ViewModelExtended
    {
        public SubjectsViewModel(ISubjectQuery subjectQueries, ISubjects subjectCommands, IValidator<Subject> subjectValidator)
            : base()
        {
            _commands = subjectCommands;
            _queries = subjectQueries;
            _subjects = new ObservableCollection<ViewModelBase>();
            _subjectValidator = subjectValidator;

            LoadSubjects();

            this.IsDirtyChangedEvent += new EventHandler(IsDirtyChangedHandler);

            MessengerInstance.Register<Messages.Navigating>(this, "SubjectsView", msg => Cleanup());
        }

        private ISubjectQuery _queries;
        private ISubjects _commands;
        private ObservableCollection<ViewModelBase> _subjects;
        private IValidator<Subject> _subjectValidator;

        public ObservableCollection<ViewModelBase> Subjects
        {
            get
            {
                return _subjects;
            }
        }

        public ICommand NewSubjectCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        var subjectVM = new EditingSubjectViewModel(new Domain.Subject(), _subjectValidator);
                        subjectVM.ChangesCanceledEvent += new EventHandler(DiscardNewSubject);
                        subjectVM.ChangesSavedEvent += new EventHandler(SaveNewSubject);
                        _subjects.Add(subjectVM);
                        IncreaseDirtyLevel();
                    }
                );
            }
        }

        void DiscardNewSubject(object sender, EventArgs e)
        {
            var subjectViewModel = sender as EditingSubjectViewModel;
            if (subjectViewModel != null)
            {
                UnhookCreationHandlers(subjectViewModel);
                _subjects.Remove(subjectViewModel);
                DecreaseDirtyLevel();
            }
        }

        private void UnhookCreationHandlers(EditingSubjectViewModel subjectViewModel)
        {
            subjectViewModel.ChangesCanceledEvent -= DiscardNewSubject;
            subjectViewModel.ChangesSavedEvent -= EditCompleted;
        }

        void SaveNewSubject(object sender, EventArgs e)
        {
            var subjectViewModel = sender as EditingSubjectViewModel;
            if (subjectViewModel != null)
            {
                var context = TaskScheduler.FromCurrentSynchronizationContext();

                Task.Factory.StartNew(() => _commands.Add(subjectViewModel.Subject))
                    .ContinueWith(task =>
                        {
                            if (task.Exception == null)
                            {
                                UnhookCreationHandlers(subjectViewModel);
                                EditCompleted(subjectViewModel, EventArgs.Empty);
                            }
                        }, context
                    );
            }
        }

        private void DeleteSubject(object sender, EventArgs e)
        {
            var subjectVM = sender as SubjectViewModel;
            if (subjectVM != null)
            {
                var syncContext = TaskScheduler.FromCurrentSynchronizationContext();

                Task.Factory
                    .StartNew(() => _commands.Remove(subjectVM.Subject))
                    .ContinueWith(task =>
                        {
                            if (task.Exception == null)
                            {
                                RemoveFromSubjects(subjectVM);
                            }
                            else
                            {
                                //handle the exception
                            }
                        }
                    , syncContext
                    );
            }
        }

        private void EditSubject(object sender, EventArgs e)
        {
            var subjectVM = sender as SubjectViewModel;
            if (subjectVM != null)
            {
                int idx = _subjects.IndexOf(subjectVM);
                if (idx >= 0)
                {
                    var editingVM =  new EditingSubjectViewModel(subjectVM.Subject, _subjectValidator);
                    RemoveFromSubjects(subjectVM); 
                    InsertIntoSubjects(idx, editingVM);                   

                    IncreaseDirtyLevel();
                }
            }
        }


        void EditCompleted(object sender, EventArgs e)
        {
            var editingVM = sender as EditingSubjectViewModel;
            if (editingVM != null)
            {
                int idx = _subjects.IndexOf(editingVM);
                RemoveFromSubjects(editingVM);
                
                var subjectVM = new SubjectViewModel(editingVM.Subject);        

                if (idx >= 0)
                {
                    InsertIntoSubjects(idx, subjectVM);
                }
                else
                {
                    AddToSubjects(subjectVM);
                }

                DecreaseDirtyLevel();
            }
        }


        private void AddSubjects(ObservableCollection<SubjectViewModel> subjects)
        {
            foreach (SubjectViewModel subject in subjects)
            {
                AddToSubjects(subject);
            }
        }

        private void AttachEvents(SubjectViewModel subject)
        {
            subject.RequestDeletionEvent += new EventHandler(DeleteSubject);
            subject.RequestEditCommand += new EventHandler(EditSubject);
        }

        private void AttachEvents(EditingSubjectViewModel editingVM)
        {
            editingVM.ChangesCanceledEvent += new EventHandler(EditCompleted);
            editingVM.ChangesSavedEvent += new EventHandler(EditCompleted);
        }

        private void UnhookEvents(EditingSubjectViewModel alterSubjectViewModel)
        {
            if (alterSubjectViewModel != null)
            {
                alterSubjectViewModel.ChangesCanceledEvent -= EditCompleted;
            }
        }

        private void UnhookEvents(SubjectViewModel svm)
        {
            svm.RequestDeletionEvent -= DeleteSubject;
            svm.RequestEditCommand -= EditSubject;
        }

        private void LoadSubjects()
        {
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () => _queries.All.Select(s => new ViewModels.SubjectViewModel(s))
            ).
            ContinueWith(task =>
                {
                    AddSubjects(
                        new ObservableCollection<SubjectViewModel>(task.Result)
                    );
                }, context
            );
        }


        private void InsertIntoSubjects(int idx, SubjectViewModel subjectVM)
        {
            AttachEvents(subjectVM);
            _subjects.Insert(idx, subjectVM);
        }

        private void InsertIntoSubjects(int idx, EditingSubjectViewModel editingVM)
        {
            AttachEvents(editingVM);
            _subjects.Insert(idx, editingVM);
        }

        private void AddToSubjects(SubjectViewModel vm)
        {
            AttachEvents(vm);
            _subjects.Add(vm);
        }

        private void AddToSubjects(EditingSubjectViewModel vm)
        {
            AttachEvents(vm);
            _subjects.Add(vm);
        }

        private void RemoveFromSubjects(SubjectViewModel subjectVM)
        {
            UnhookEvents(subjectVM);
            _subjects.Remove(subjectVM);
        }

        private void RemoveFromSubjects(EditingSubjectViewModel subjectVM)
        {
            UnhookEvents(subjectVM);
            _subjects.Remove(subjectVM);
        }

        void IsDirtyChangedHandler(object sender, EventArgs e)
        {
            IsDirtyChanged();
        }

        private void IsDirtyChanged()
        {
            MessengerInstance.Send<IsDirty>(
                (IsDirty == true) ?  Messages.IsDirty.DirtyMessage : Messages.IsDirty.CleanMessage
            );
        }

        public override void Cleanup()
        {
            //stop listening for the dirty event
            IsDirtyChangedEvent -= IsDirtyChangedHandler;

            //release singletons
            _queries = null;
            _commands = null;
            _subjectValidator = null;

            //clean the list
            var editingViewModels = _subjects.OfType<EditingSubjectViewModel>().ToList();
            var subjectViewModels = _subjects.OfType<SubjectViewModel>().ToList();

            for (int i = 0; i < editingViewModels.Count; i++)
            {
                var vm = editingViewModels[0];
                UnhookEvents(vm);
                vm.Cleanup();
            }

            for (int i = 0; i < subjectViewModels.Count; i++)
            {
                var vm = subjectViewModels[0];
                UnhookEvents(vm);
                vm.Cleanup();
            }

            for (int i = 0; i < _subjects.Count; i++)
            {
                var vm = _subjects[0];
                vm.Cleanup();
            }
            
            //call base cleanup
            base.Cleanup();
        }
    }
}
