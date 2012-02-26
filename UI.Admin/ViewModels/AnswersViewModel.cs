using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using System.Collections.ObjectModel;
using Domain;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Windows.Data;
using Validation;
using Domain.Db;

namespace UI.Admin.ViewModels
{
    public class AnswersEventArgs : EventArgs
    {
        private  Answer _answer;
        
        public AnswersEventArgs (Answer answer)
	    {
            _answer = answer;
	    }

        public Answer Answer
        {
            get
            {
                return _answer;
            }
        }
    }

    public delegate void AnswersEventHandler(object sender, AnswersEventArgs e);

    public class AnswersViewModel : ViewModelExtended
    {
        public event AnswersEventHandler AnswerAddedEvent;
        public event AnswersEventHandler AnswerRemovedEvent;
        public event EventHandler AnswerChanged;

        public AnswersViewModel(
            IEnumerable<Answer> answers,
            IValidator<Answer> answerValidator,
            IValidator<AssociatedSubject> subjectValidator,
            ISubjectQuery subjectQuery,
            Guid busId)
        {
            if (answerValidator == null)
                throw new ArgumentNullException("answerValidator");

            if (subjectValidator == null)
                throw new ArgumentNullException("subjectValidator");

            if (subjectQuery == null)
                throw new ArgumentNullException("subjectQuery");

            _busId = busId;

            _answerValidator = answerValidator;
            _subjectValidator = subjectValidator;
            _subjectQuery = subjectQuery;

            InitializeAnswers(answers);
            InitializeCommands();
        }

        private readonly Guid _busId;

        private IValidator<Answer> _answerValidator;
        private IValidator<AssociatedSubject> _subjectValidator;
        private ISubjectQuery _subjectQuery;

        private ObservableCollection<AnswerViewModel> _answers; //initialization happens in a task
        private RelayCommand _addNewAnswerCommand;

        public ObservableCollection<AnswerViewModel> Answers
        {
            get
            {
                return _answers;
            }
            private set
            {
                _answers = value;
                RaisePropertyChanged("Answers");
            }
        }

        public ICommand AddNewAnswerCommand
        {
            get
            {
                return _addNewAnswerCommand;
            }
        }

        private void AddAnswer(AnswerViewModel answer)
        {
            AttachEvents(answer);
            _answers.Add(answer);

            RaiseAnswerAdded(answer.Answer);

            _logger.Log(Level.Trace, "Added a new answer to the question.");
        }

        
        private void RemoveAnswer(AnswerViewModel answer)
        {
            DettachEvents(answer);
            _answers.Remove(answer);

            RaiseAnswerRemoved(answer.Answer);

            _logger.Log(Level.Trace, "Removed an answer from the question.");
        }


        private void AttachEvents(AnswerViewModel answer)
        {
            answer.PropertyChanged += AnswerPropertyChanged;
            answer.IsDirtyChangedEvent += IsDirtyHandler;
            answer.RequestsRemovalEvent += RequestAnswerRemoval;
        }

        private void DettachEvents(AnswerViewModel answer)
        {
            answer.PropertyChanged -= AnswerPropertyChanged;
            answer.IsDirtyChangedEvent -= IsDirtyHandler;
            answer.RequestsRemovalEvent -= RequestAnswerRemoval;
        }

        private void RequestAnswerAddition()
        {
            AnswerViewModel vm = new AnswerViewModel(
                new Answer(),
                _answerValidator,
                _subjectValidator,
                 _subjectQuery,
                _busId
            );

            AddAnswer(vm);

            CollectionViewSource.GetDefaultView(_answers).MoveCurrentTo(vm);
        }

        private void RequestAnswerRemoval(object sender, EventArgs e)
        {
            AnswerViewModel vm = sender as AnswerViewModel;
            if (vm != null)
            {
                RemoveAnswer(vm);
            }
        }

        private void InitializeAnswers(IEnumerable<Answer> answers)
        {
            var viewModels =
                answers
                    .Select
                    (
                        answer => 
                            new AnswerViewModel
                            (
                                answer,
                                _answerValidator,
                                _subjectValidator,
                                _subjectQuery, 
                                _busId
                            )
                    )
                    .ToList();
                
            foreach (AnswerViewModel  vm in viewModels)
            {
                AttachEvents(vm);
            }

            _answers = new ObservableCollection<AnswerViewModel>(viewModels);

            _logger.Log(Level.Trace, "Set the answers for a question.");
        }

        private void InitializeCommands()
        {
            _addNewAnswerCommand =
                new RelayCommand(() => RequestAnswerAddition());

            _logger.Log(Level.Trace, "Allowed creation of new answers for a question");
        }

        private void IsDirtyHandler(object sender, EventArgs e)
        {
            MessengerInstance.Send<Messages.IsDirty>(Messages.IsDirty.DirtyMessage, _busId);

            _logger.Log(Level.Trace, "An answer became dirty");
        }

        public override void Cleanup()
        {
            base.Cleanup();

            foreach (var answer in _answers)
            {
                RemoveAnswer(answer); //ensure the events are released
            }
        }

        private void RaiseAnswerAdded(Answer answer)
        {
            var handler = AnswerAddedEvent;
            if (handler != null)
            {
                handler(this, new AnswersEventArgs(answer));
            }
        }

        private void RaiseAnswerRemoved(Answer answer)
        {
            var handler = AnswerRemovedEvent;
            if (handler != null)
            {
                handler(this, new AnswersEventArgs(answer));
            }
        }

        private void AnswerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var handler = AnswerChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
