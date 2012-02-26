using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using UI.Client.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Domain;
using System.ComponentModel;
using System.Windows.Data;

namespace UI.Client.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        public TestViewModel(IUserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (user.CurrentTest == null)
                throw new ArgumentException("You must have selected a test.");

            _user = user;

            InitializeCommands();
            InitializeQuestions();
        }
        
        private IUserModel _user;
        
        private ICollectionView _questions;

        private RelayCommand<QuestionViewModel> _moveToQuestionCommand;
        private RelayCommand _goToTestEvaluationCommand;
        private RelayCommand _goToNextQuestionCommand;
        private RelayCommand _goToPreviousQuestionCommand;
        private RelayCommand _goToQuestionsReviewCommand;

        private bool _isReviewing;
        private bool _isFinished;
        

        public object Context
        {
            get
            {
                if (_questions == null) return null;

                return (IsReviewing) ? _questions : _questions.CurrentItem;
            }
        }

        public bool IsFinished
        {
            get
            {
                return _isFinished;
            }
        }


        public bool IsReviewing
        {
            get
            {
                return _isReviewing;
            }
            set
            {
                if (_isReviewing != value)
                {
                    _isReviewing = value;
                    RaisePropertyChanged("IsReviewing");
                }
            }
        }

        public ICommand MoveToQuestionCommand
        {
            get
            {
                return _moveToQuestionCommand;
            }
        }
  
        public ICommand GoToTestEvaluationCommand
        {
            get
            {
                return _goToTestEvaluationCommand;
            }
        }

        public ICommand GoToNextQuestionCommand
        {
            get
            {
                return _goToNextQuestionCommand;
            }
        }

        public ICommand GoToPreviousQuestionCommand
        {
            get
            {
                return _goToPreviousQuestionCommand;
            }
        }

        public ICommand GoToQuestionsReviewCommand
        {
            get
            {
                return _goToQuestionsReviewCommand;
            }
        }

        public string Name
        {
            get
            {
                return _user.CurrentTest.Name;
            }
        }

        private void InitializeQuestions()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    return _user.CurrentTest.Questions.Select(q => new QuestionViewModel(q)).ToList();
                }
            )
            .ContinueWith(
                task =>
                {
                    _questions = CollectionViewSource.GetDefaultView(task.Result);
                    RaisePropertyChanged("Context");
                    
                    _questions.MoveCurrentToFirst();
                    RaisePropertyChanged("Context");
                    Refresh();

                    RaisePropertyChanged("IsReviewing");
                },
                scheduler
            );
        }
        
        private void InitializeCommands()
        {
            _goToTestEvaluationCommand =
                new RelayCommand(
                    () => GoToTestEvaluation()
                );

            _goToQuestionsReviewCommand =
                new RelayCommand(
                    () => MoveToQuestionsReview(),
                    () => !IsReviewing
                );

            _goToNextQuestionCommand =
                new RelayCommand(
                    () =>
                    {
                        _questions.MoveCurrentToNext();

                        if (_questions.IsCurrentAfterLast)
                            _isFinished = true;

                        Refresh();
                    },
                    () =>
                    {
                        return _questions != null && !_questions.IsCurrentAfterLast;
                    }
                );

            _goToPreviousQuestionCommand =
                new RelayCommand(
                    () =>
                    {
                        if (_questions.MoveCurrentToPrevious())
                            _isFinished = false;
                        Refresh();
                    },
                    () =>
                    {
                        return _questions != null && !(_questions.IsCurrentBeforeFirst || _questions.CurrentPosition == 0);
                    }
                );

            _moveToQuestionCommand =
                new RelayCommand<QuestionViewModel>(
                    question =>
                    {
                        MoveToQuestion(question);
                    },
                    question =>
                    {
                        return question != null;
                    }
                );
                    
        }

        private void Refresh()
        {
            RaisePropertyChanged(string.Empty);

            _goToPreviousQuestionCommand.RaiseCanExecuteChanged();
            _goToNextQuestionCommand.RaiseCanExecuteChanged();
            _goToQuestionsReviewCommand.RaiseCanExecuteChanged();
        }

        private void MoveToQuestionsReview()
        {
            IsReviewing = true;
            _isFinished = false;

            Refresh();
        }
        
        private void MoveToQuestion(QuestionViewModel question)
        {
           if( _questions.MoveCurrentTo(question))
           {
               IsReviewing = false;
               Refresh();
           }
        }

        private void GoToTestEvaluation()
        {
            MessengerInstance.Send<Messages.NavigateTo>(new Messages.NavigateTo("TestEvaluationView"));
        }
    }
}

