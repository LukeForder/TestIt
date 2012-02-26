using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Logging;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Validation;
using Domain.Db;
using System.ComponentModel;

namespace UI.Admin.ViewModels
{
    public class EditingTestViewModel : ViewModelExtended, IDataErrorInfo
    {
        public event EventHandler ChangesSavedEvent;
        public event EventHandler ChangesCancelledEvent;

        public EditingTestViewModel(
            Test test,
            IValidator<Test> testValidator,
            IValidator<Question> questionValidator,
            IValidator<Answer> answerValidator,
            IValidator<AssociatedSubject> subjectValidator,
            ISubjectQuery subjectQuery
        )
        {
            if (test == null)
                throw new ArgumentNullException("test");

            _busId = test.Id;
            _test = test;
            _testValidator = testValidator;

            InitializeQuestions(test, questionValidator, answerValidator, subjectValidator, subjectQuery);

            this.PropertyChanged += PropertyHasChanged;
           
            RegisterMessages();
            IntializeCommands();
        }


        private readonly Guid _busId;
        private QuestionsViewModel _questions;
        private Test _test;

        private RelayCommand _saveChangesCommand;
        private RelayCommand _discardChangesCommand;
        private IValidator<Test> _testValidator;

        public string Name
        {
            get
            {
                 return _test.Name;
            }
            set
            {
                if (_test.Name != value)
                {
                    _test.Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        internal Test Test
        {
            get
            {
                return _test;
            }
        }

        public QuestionsViewModel Questions
        {
            get
            {
                return _questions;
            }
        }

        public ICommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand;
            }
        }

        public ICommand CancelChangesCommand
        {
            get
            {
                return _discardChangesCommand;
            }
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<Messages.IsDirty>(this, _busId, msg => MakeDirty());
        }

        private void MakeDirty()
        {
            if (!IsDirty)
                IncreaseDirtyLevel();
        }

        private void IntializeCommands()
        {
            _discardChangesCommand =
                new RelayCommand(
                    () =>
                    {
                        RaiseChangesCancelled();
                    }
                );

            _saveChangesCommand =
                new RelayCommand(
                    () =>
                    {
                        RaiseChangesSavedEvent();
                    }
                    ,
                    () => _testValidator.Validate(_test).IsValid
                );
        }

        private void RaiseChangesSavedEvent()
        {
            var handler = ChangesSavedEvent;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void RaiseChangesCancelled()
        {
            var handler = ChangesCancelledEvent;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get 
            {
                if (columnName == "Name")
                    return _testValidator.ValidateProperty(_test, "Name").Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "Questions")
                    return _testValidator.ValidateProperty(_test, "Questions").Errors.Select(x => x.Message).FirstOrDefault();

                _logger.Log(Level.Warning, "Trying to validate {0} of test.", columnName);
                return null;
            }
        }

        private void InitializeQuestions(
            Test test, 
            IValidator<Question> questionValidator,
            IValidator<Answer> answerValidator, 
            IValidator<AssociatedSubject> subjectValidator, 
            ISubjectQuery subjectQuery
        )
        {
            _questions = new QuestionsViewModel(
                test.Questions,
                questionValidator,
                answerValidator,
                subjectValidator,
                subjectQuery,
                _busId
            );

            AttachQuestionEvents(_questions);
        }

        private void AttachQuestionEvents(QuestionsViewModel viewModel)
        {
            viewModel.QuestionAddedEvent += QuestionAdded;
            viewModel.QuestionChangedEvent += QuestionChanged;
            viewModel.QuestionRemovedEvent += QuestionRemoved;
        }

        private void DettachQuestionEvents(QuestionsViewModel viewModel)
        {
            viewModel.QuestionAddedEvent -= QuestionAdded;
            viewModel.QuestionChangedEvent -= QuestionChanged;
            viewModel.QuestionRemovedEvent -= QuestionRemoved;
        }
        
        void QuestionRemoved(object sender, QuestionEventArgs e)
        {
            if (e.Question != null)
            {
                _test.Questions.Remove(e.Question);
                RaisePropertyChanged("Questions");
            }
        }

        void QuestionChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Questions");
        }

        void QuestionAdded(object sender, QuestionEventArgs e)
        {
            if (e.Question != null)
            {
                _test.Questions.Add(e.Question);
                RaisePropertyChanged("Questions");
            }
        }

        void PropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {
            _saveChangesCommand.RaiseCanExecuteChanged();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            this.PropertyChanged -= PropertyHasChanged;
            
            this._testValidator = null;
            this._test = null;

            _questions.Cleanup();
        }
    }
}
