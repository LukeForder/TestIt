using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.ComponentModel;
using Validation;
using Domain;
using Domain.Db;

namespace UI.Admin.ViewModels
{
    #region Question eventing decls

    public class QuestionEventArgs : EventArgs
    {
        public QuestionEventArgs(Question question)
        {
            _question = question;
        }

        private readonly Question _question;

        public Question Question
        {
            get
            {
                return _question;
            }
        }
    }

    public delegate void QuestionEventHandler(object sender, QuestionEventArgs e);

    #endregion

    public class QuestionsViewModel : ViewModelExtended
    {
        public event QuestionEventHandler QuestionAddedEvent;
        public event QuestionEventHandler QuestionRemovedEvent;
        public event EventHandler QuestionChangedEvent;

        public QuestionsViewModel(
            IEnumerable<Domain.Question> questions,
            IValidator<Question> questionValidator,
            IValidator<Answer> answerValidator,
            IValidator<AssociatedSubject> subjectValidator,
            ISubjectQuery subjectQuery,
            Guid busId
        )
            : base()
        {
            _busId = busId;

            _questions = 
                new ObservableCollection<QuestionViewModel>();

            _questionValidator = questionValidator;
            _answerValidator = answerValidator;
            _subjectValidator = subjectValidator;
            _subjectQuery = subjectQuery;

            BuildViewModels(questions);
            InitializeCommands();
            InitializeView();
        }

        private ObservableCollection<QuestionViewModel> _questions;
        private ICollectionView _view;
        private RelayCommand<QuestionViewModel> _deleteQuestionCommand;
        private ICommand _newQuestionCommand;

        private readonly Guid _busId;
        
        private ISubjectQuery _subjectQuery;
        private IValidator<AssociatedSubject> _subjectValidator;
        private IValidator<Answer> _answerValidator;
        private IValidator<Question> _questionValidator;
        
        public ICollectionView Questions
        {
            get
            {
                return _view;
            }
        }

        public QuestionViewModel Question
        {
            get
            {
                return _view.CurrentItem as QuestionViewModel;
            }
        }

        public ICommand DeleteQuestionCommand
        {
            get
            {
                return _deleteQuestionCommand;
            }
        }

        public ICommand NewQuestionCommand
        {
            get
            {
                return _newQuestionCommand;
            }
        }
        
        private void AddToQuestions(List<QuestionViewModel> questions)
        {
            foreach (QuestionViewModel question in questions)
            {
                AttachEvents(question);
                this._questions.Add(question);                
            }
        }

        private void AddQuestion(QuestionViewModel question)
        {
            AttachEvents(question);
            this._questions.Add(question);

            RaiseQuestionAdded(question.Question);
        }

        private void AttachEvents(QuestionViewModel question)
        {
            question.IsDirtyChangedEvent += QuestionIsDirtyHandler;
            question.PropertyChanged += QuestionChangedHandler;
        }

        private void DettachEvents(QuestionViewModel question)
        {
            question.IsDirtyChangedEvent -= QuestionIsDirtyHandler;
            question.PropertyChanged -= QuestionChangedHandler;
        }

        private void BuildViewModels(IEnumerable<Domain.Question> questions)
        {
            var viewModels = 
                questions
                    .Select(q => 
                        new QuestionViewModel(
                        q, 
                        _questionValidator,
                        _answerValidator,
                        _subjectValidator,
                        _subjectQuery, 
                        _busId
                        )
                    )
                    .ToList();
            AddToQuestions(viewModels);
              
        }
        
        void QuestionIsDirtyHandler(object sender, EventArgs e)
        {
            ViewModelExtended vm = sender as ViewModelExtended;
            if (vm != null)
            {
                if (vm.IsDirty && !this.IsDirty)
                {
                    IncreaseDirtyLevel();
                }
            }
        }

        private void InitializeCommands()
        {
            _deleteQuestionCommand =
                new RelayCommand<QuestionViewModel>(
                    q => RemoveQuestion(q),
                    q => CanRemoveQuestion(q)
                );

            _newQuestionCommand =
                new RelayCommand(
                    () => NewQuestion()
                );
        }

        private bool CanRemoveQuestion(QuestionViewModel question)
        {
            return (question != null && _questions.Contains(question));
        }

        private void RemoveQuestion(QuestionViewModel question)
        {
            NotifyIsDirty();

            DettachEvents(question);
            _questions.Remove(question);

            RaiseQuestionRemoved(question.Question);
        }

        private void NewQuestion()
        {
            NotifyIsDirty();

            var vm = new QuestionViewModel(
                new Domain.Question(), 
                _questionValidator,
                _answerValidator,
                _subjectValidator,
                _subjectQuery, 
                _busId
            );
            AddQuestion(vm);
            _view.MoveCurrentTo(vm);
        }
        
        private void NotifyIsDirty()
        {
            MessengerInstance.Send<Messages.IsDirty>(Messages.IsDirty.DirtyMessage);
        }

        void QuestionChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Question");
            _deleteQuestionCommand.RaiseCanExecuteChanged();
        }

        private void InitializeView()
        {
            _view = CollectionViewSource.GetDefaultView(_questions);
            _view.CurrentChanged += new EventHandler(QuestionChanged);
        }

        private void QuestionChangedHandler(object sender, EventArgs e)
        {
            RaiseQuestionChanged();
        }

        private void RaiseQuestionAdded(Question question)
        {
            var handler = QuestionAddedEvent;
            if (handler != null)
                handler(this, new QuestionEventArgs(question));            
        }

        private void RaiseQuestionRemoved(Question question)
        {
            var handler = QuestionRemovedEvent;
            if (handler != null)
                handler(this, new QuestionEventArgs(question));
        }

        private void RaiseQuestionChanged()
        {
            var handler = QuestionChangedEvent;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
