using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Db;
using Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using Validation;
using Domain;
using GalaSoft.MvvmLight.Messaging;

namespace UI.Admin.ViewModels
{
    public class TestsViewModel : ViewModelExtended
    {
        public TestsViewModel(
            ITestQuery queries,
            ITests commands,
            ISubjectQuery subjectQuery,
            IValidator<Test> testValidator,
            IValidator<Question> questionValidator,
            IValidator<Answer> answerValidator,
            IValidator<AssociatedSubject> subjectValidator
        )
            : base()
        {
            _queries = queries;
            _commands = commands;

            _subjectQuery = subjectQuery;

            _testValidator = testValidator;
            _questionValidator = questionValidator;
            _answerValidator = answerValidator;
            _subjectValidator = subjectValidator;


            LoadAllTests();

            _addTestCommand = new RelayCommand(
                () => AddNewTest(),
                () => CanAddNewTest()
            );
        }

        #region Fields

        private ITestQuery _queries;
        private ITests _commands;
        private ObservableCollection<TestViewModel> _tests; //assigned through a task

        private object _context;
        private RelayCommand _addTestCommand;
        private bool _editing;

        private IValidator<AssociatedSubject> _subjectValidator;
        private IValidator<Answer> _answerValidator;
        private IValidator<Test> _testValidator;
        private ISubjectQuery _subjectQuery;
        private IValidator<Question> _questionValidator;

        #endregion

        #region Properties

        public bool IsEditing
        {
            get
            {
                return _editing;
            }
            set
            {
                if (_editing != value)
                {
                    _editing = value;
                    _addTestCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged("IsEditing");
                    RaisePropertyChanged("Heading");
                }
            }
        }

        public object DataContext
        {
            get
            {
                return _context;
            }
            private set
            {
                _context = value;
                RaisePropertyChanged("DataContext");
            }
        }
        
        public ICommand NewTestCommand
        {
            get
            {
                return _addTestCommand;
            }
        }

        public string Heading
        {
            get
            {
                var test = DataContext as EditingTestViewModel;
                StringBuilder heading = new StringBuilder();

                if (IsEditing)
                {
                    heading.Append("Editing ");

                    if (test != null && !string.IsNullOrEmpty(test.Name))
                    {
                        heading.Append(test.Name);
                    }
                    else
                    {
                        heading.Append("an unnamed test");
                    }

                    heading.Append(".");
                }
                else
                {
                    heading.Append("Tests");
                }

                return heading.ToString();
            }
        }

        #endregion

        #region TestViewModel Eventing

        private void AttachEvents(TestViewModel vm)
        {
            vm.RequestDeleteEvent += RequestDelete;
            vm.RequestEditEvent += RequestEdit;
        }

        private void DettachEvents(TestViewModel vm)
        {
            vm.RequestDeleteEvent -= RequestDelete;
            vm.RequestEditEvent -= RequestEdit;
        }

        void RequestEdit(object sender, EventArgs e)
        {
            TestViewModel vm = sender as TestViewModel;
            if (vm != null)
            {
                BeginEditingTest(vm);
            }
        }

        void RequestDelete(object sender, EventArgs e)
        {
            TestViewModel vm = sender as TestViewModel;
            if (vm == null)
                return;

            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    _commands.Remove(vm.Test);
                }
            )
            .ContinueWith(
                task =>
                {
                    if (task.Exception != null)
                    {
                        HandleDatabaseException(task.Exception);
                    }
                    else
                    {
                        RemoveTest(vm);
                    }
                },
                scheduler
            );
        }

        #endregion

        #region Editing methods and eventing

        private void BeginEditingTest(TestViewModel vm)
        {
            EditingTestViewModel editingViewModel =
                new EditingTestViewModel(
                    vm.Test, _testValidator,
                    _questionValidator,
                    _answerValidator,
                    _subjectValidator,
                    _subjectQuery
                );

            AttachEditingEvents(editingViewModel);

            DataContext = editingViewModel;

            IsEditing = true;
        }

        private void AttachEditingEvents(EditingTestViewModel editingViewModel)
        {
            editingViewModel.ChangesCancelledEvent += RequestedChangesCancelled;
            editingViewModel.ChangesSavedEvent += RequestedChangesSaved;
        }

        private void DettachEditingEvents(EditingTestViewModel editingViewModel)
        {
            editingViewModel.ChangesCancelledEvent -= RequestedChangesCancelled;
            editingViewModel.ChangesSavedEvent -= RequestedChangesSaved;
        }

        void RequestedChangesSaved(object sender, EventArgs e)
        {
            EditingTestViewModel viewModel = sender as EditingTestViewModel;
            if (viewModel != null)
            {
                DettachEditingEvents(viewModel);

                TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Factory.StartNew(
                    () =>
                    {
                        _commands.Update(viewModel.Test);
                    }
                )
                .ContinueWith(
                    task =>
                    {
                        if (task.Exception != null)
                        {
                            HandleDatabaseException(task.Exception);
                            AttachEditingEvents(viewModel);
                        }
                        else
                        {
                            SwitchToTests();
                            MessengerInstance.Send<Messages.IsDirty>(Messages.IsDirty.CleanMessage);
                        }
                    },
                    scheduler
                );
            }
        }

        void RequestedChangesCancelled(object sender, EventArgs e)
        {
            EditingTestViewModel viewModel = sender as EditingTestViewModel;
            if (viewModel != null)
            {
                DettachEditingEvents(viewModel);

                TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Factory.StartNew(
                    () =>
                    {
                        return _queries.ById(viewModel.Test.Id);
                    }
                )
                .ContinueWith(
                    task =>
                    {
                        if (task.Exception != null)
                        {
                            HandleDatabaseException(task.Exception);
                            LoadAllTests();
                        }
                        else if (task.Result != null)
                        {
                            ReloadTest(task.Result);
                        }
                    },
                    scheduler
                );
            }
        }

        private void ReloadTest(Test test)
        {
            var vm = _tests.Where(t => t.Test.Id == test.Id).FirstOrDefault();

            int idx = 0;
            if (vm != null)
            {
                idx = _tests.IndexOf(vm);
                _tests.Remove(vm);
            }

            AddTest(
                new TestViewModel(test)
            );

            SwitchToTests();
        }

        #endregion

        #region Initialization Methods

        private void LoadAllTests()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(
                () => _queries.All
            ).ContinueWith(
                task => 
                {
                    _tests = new ObservableCollection<TestViewModel>();

                    AddTests(task.Result);

                    SwitchToTests();
                },
                scheduler
            );
        }

        #endregion

        #region Tests Collection helpers

        private void SwitchToTests()
        {
            IsEditing = false;
            DataContext = _tests;
        }


        private void AddTests(IEnumerable<Domain.Test> tests)
        {
            foreach (var vm in tests.Select(t => new TestViewModel(t)))
            {
                AddTest(vm);
            }
        }

        private void AddTest(TestViewModel vm)
        {
            AttachEvents(vm);
            _tests.Add(vm);
        }

        private void RemoveTest(TestViewModel vm)
        {
            DettachEvents(vm);
            _tests.Remove(vm);
        }

        #endregion

        private void HandleDatabaseException(AggregateException exception)
        {
            exception.Handle(e =>
                {
                    if (!(e is Domain.Exceptions.DomainValidationException))
                    {
                        _logger.Log(Level.Error, "Data operation failed to successfully complete, exception: {0}.", e.GetType().Name);
                        MessengerInstance.Send<DialogMessage>(new DialogMessage("An error occured and the test was not saved", msg => { }));
                    }
                    return true;
                });
        }

        #region New Test Methods and events

        private void AddNewTest()
        {
            EditingTestViewModel viewModel
                = new EditingTestViewModel(
                    new Domain.Test(),
                    _testValidator,
                    _questionValidator,
                    _answerValidator,
                    _subjectValidator,
                    _subjectQuery
                );

            AttachNewTestEvents(viewModel);

            DataContext = viewModel;

            IsEditing = true;
        }

        private bool CanAddNewTest()
        {
            return IsEditing == false;
        }

        private void AttachNewTestEvents(EditingTestViewModel viewModel)
        {
            viewModel.ChangesCancelledEvent += CancelNewTest;
            viewModel.ChangesSavedEvent += SaveNewTest;
        }

        private void DettachNewTestEvents(EditingTestViewModel viewModel)
        {
            viewModel.ChangesCancelledEvent -= CancelNewTest;
            viewModel.ChangesSavedEvent -= SaveNewTest;
        }

        void CancelNewTest(object sender, EventArgs e)
        {
            EditingTestViewModel viewModel = sender as EditingTestViewModel;
            if (viewModel != null)
            {
                DettachNewTestEvents(viewModel);
                IsEditing = false;
                DataContext = _tests;
            }
        }

        void SaveNewTest(object sender, EventArgs e)
        {
            EditingTestViewModel viewModel = sender as EditingTestViewModel;
            if (viewModel != null)
            {
                TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Factory.StartNew(
                    () =>
                    {
                        _commands.Add(viewModel.Test);
                    }
                ).ContinueWith(
                    task =>
                    {
                        if (task.Exception != null)
                        {
                            HandleDatabaseException(task.Exception);
                        }
                        else
                        {
                            DettachNewTestEvents(viewModel);
                            AddTest(new TestViewModel(viewModel.Test));
                            SwitchToTests();
                        }
                    },
                    scheduler
                );
            }
        }

        #endregion
    }
}
