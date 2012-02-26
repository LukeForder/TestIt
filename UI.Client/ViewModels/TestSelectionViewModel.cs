using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using UI.Client.Models;
using Domain.Db;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Domain;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.Client.ViewModels
{
    public class TestSelectionViewModel : ViewModelBase
    {
        public TestSelectionViewModel(IUserModel user, ITestQuery testsDb)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (testsDb == null)
                throw new ArgumentNullException("tests");

            _user = user;
            _testsDb = testsDb;

            LoadTests();
            InitializeCommands();
        }

        private IUserModel _user;
        private ITestQuery _testsDb;
        private RelayCommand<Test> _beginTestCommand;
        private ObservableCollection<Test> _tests;

        public ICommand BeginTestCommand
        {
            get
            {
                return _beginTestCommand;
            }
        }

        public ObservableCollection<Test> Tests
        {
            get
            {
                return _tests;
            }
        }

        private void LoadTests()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    return _testsDb.All;
                }
            )
            .ContinueWith(
                task =>
                {
                    _tests = new ObservableCollection<Test>(task.Result);

                    CollectionViewSource.GetDefaultView(_tests).CurrentChanged += TestChanged;

                    RaisePropertyChanged("Tests");
                },
                scheduler
            );
        }

        private void InitializeCommands()
        {
            _beginTestCommand =
                new RelayCommand<Test>(
                    test =>
                    {
                        BeginTest(test);
                    },
                    test =>
                    {
                        return test != null;
                    }
                );
        }

        private void BeginTest(Test test)
        {
            _user.BeginTest(test);
            MessengerInstance.Send<Messages.NavigateTo>(
                new Messages.NavigateTo("TestView")
            );
        }

        private void TestChanged(object sender, EventArgs e)
        {
            _beginTestCommand.RaiseCanExecuteChanged();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            CollectionViewSource.GetDefaultView(_tests).CurrentChanged -= TestChanged;
        }
    }
}
