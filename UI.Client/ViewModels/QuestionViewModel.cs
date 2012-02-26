using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using UI.Client.Model;

namespace UI.Client.ViewModels
{
    public class QuestionViewModel : ViewModelBase
    {
        private QuestionModel _question;
        private IEnumerable<AnswerViewModel> _answers;
        private Guid _qId;

        public QuestionViewModel(QuestionModel question)
        {
            this._question = question;
            InitializeAnswers();
            _qId = Guid.NewGuid();
        }

     
        public IEnumerable<AnswerViewModel> Answers
        {
            get
            {
                return _answers;
            }
        }

        internal QuestionModel Question
        {
            get
            {
                return _question;
            }
        }

        public string Title
        {
            get
            {
                return _question.Title;
            }
        }

        public string Text
        {
            get
            {
                return _question.Text;
            }
        }

        public BitmapImage Image
        {
            get
            {
                return _question.Image;
            }
        }

        public string Prompt
        {
            get
            {
                return _question.Prompt;
            }
        }

        private void InitializeAnswers()
        {
            TaskScheduler scheduler = TaskScheduler.Current;
            Task.Factory.StartNew(
                () =>
                {
                    return (_question.SelectMany) ?
                        _question.Answers
                            .Select(answer => new ViewModels.SetMemberAnswerViewModel(answer))
                            .ToList<ViewModels.AnswerViewModel>() :

                        _question.Answers
                            .Select(answer => new ViewModels.StandAloneAnswerViewModel(answer, _qId))
                            .ToList<ViewModels.AnswerViewModel>();
                }
            )
            .ContinueWith(
                task =>
                {
                    _answers = task.Result;
                }
            );

        }
     
    }
}

