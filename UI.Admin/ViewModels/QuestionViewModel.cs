using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using Domain;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Validation;
using Domain.Db;
using System.ComponentModel;

namespace UI.Admin.ViewModels
{
    public class QuestionViewModel : ViewModelExtended, IDataErrorInfo
    {
        public QuestionViewModel(
            Question question, 
            IValidator<Question> questionValidator,
            IValidator<Answer> answerValidator,
            IValidator<AssociatedSubject> subjectValidator,
            ISubjectQuery subjectQuery,
            Guid busId
        )
            : base()
        {
            _busId = busId;
            _question = question;
            _questionValidator = questionValidator;
            _answerValidator = answerValidator;
            _subjectValidator = subjectValidator;
            _subjectQuery = subjectQuery;

            InitializeCommands();
            InitializeAnswers();
        }
        
        private Question _question;
        private RelayCommand _selectImageCommand;

        private IValidator<Question> _questionValidator;
        private IValidator<Answer> _answerValidator;
        private IValidator<AssociatedSubject> _subjectValidator;

        private ISubjectQuery _subjectQuery;

        private readonly Guid _busId;
        private AnswersViewModel _answers;

        public string Title
        {
            get
            {
                return _question.Title;
            }
            set
            {
                if (_question.Title != value)
                {
                    _question.Title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public string Text
        {
            get
            {
                return _question.Text;
            }
            set
            {
                if (_question.Text != value)
                {
                    _question.Text = value;
                    RaisePropertyChanged("Text");
                }
            }
        }

        public string Prompt
        {
            get
            {
                return _question.Prompt;
            }
            set
            {
                if (_question.Prompt != value)
                {
                    _question.Prompt = value;
                    RaisePropertyChanged("Prompt");
                }
            }
        }

        public string Tag
        {
            get
            {
                return _question.Tag;
            }
            set
            {
                if (_question.Tag != value)
                {
                    _question.Tag = value;
                    RaisePropertyChanged("Tag");
                }
            }
        }

        public bool SelectMany
        {
            get
            {
                return _question.SelectMany;
            }
            set
            {
                if (_question.SelectMany != value)
                {
                    _question.SelectMany = value;
                    RaisePropertyChanged("SelectMany");
                }
            }
        }

        public BitmapImage Image
        {
            get
            {
                if (_question.Image == null)
                    return null as BitmapImage;

                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = new MemoryStream(_question.Image);
                img.EndInit();

                return img;
            }
            set
            {
                if (value != null)
                {
                    long streamLength = value.StreamSource.Length;
                    byte[] imageData = new byte[streamLength];
                    value.StreamSource.Read(imageData, 0, Convert.ToInt32(streamLength));

                    _question.Image = imageData;
                    RaisePropertyChanged("Image");
                }
            }
        }

        public AnswersViewModel Answers
        {
            get
            {
                return _answers;
            }
        }

        public ICommand SelectImageCommand
        {
            get
            {
                return _selectImageCommand;
            }
        }

        public Question Question
        {
            get
            {
                return _question;
            }
        }

        #region Initialization Methods

        private void InitializeCommands()
        {
            _selectImageCommand = new RelayCommand(
                () => MessengerInstance.Send<Messages.ChooseFileMessage>(new Messages.ChooseFileMessage(UpdateQuestionImage)
                {
                    Extensions = "jpeg|*.jpg|png|*.png"
                })
            );
        }
            
        private void InitializeAnswers()
        {
            _answers = 
                new AnswersViewModel(
                    _question.Answers,
                    _answerValidator,
                    _subjectValidator,
                    _subjectQuery,
                    _busId
                );

            AttachAnswerEvents(_answers);
        }

        private void AttachAnswerEvents(AnswersViewModel answers)
        {
            answers.AnswerAddedEvent += AnswerAdded;
            answers.AnswerRemovedEvent += AnswerRemoved;
            answers.AnswerChanged += AnswerChanged;
        }

        private void DettachAnswerEvents(AnswersViewModel answers)
        {
            answers.AnswerAddedEvent -= AnswerAdded;
            answers.AnswerRemovedEvent -= AnswerRemoved;
            answers.AnswerChanged -= AnswerChanged;
        }

        #endregion


        private void UpdateQuestionImage(string fileName)
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() =>
                {
                    FileStream stream = File.Open(fileName, FileMode.Open);
                    byte[] imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, Convert.ToInt32(imageData.LongLength));
                    return imageData;
                }
            )
            .ContinueWith(task =>
                {
                    if (task.Exception != null)
                        MessengerInstance.Send<DialogMessage>(new DialogMessage("The image could not be read, please try again or select another image.", msg => { }));

                    _question.Image = task.Result;

                    RaisePropertyChanged("Image");
                },
                scheduler
            );
            
        }

        #region Answers Event Handlers

        void AnswerChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Answers");
        }

        void AnswerAdded(object sender, AnswersEventArgs e)
        {
            if (e.Answer != null)
            {
                _question.Answers.Add(e.Answer);
                RaisePropertyChanged("Answers");
            }
        }

        void AnswerRemoved(object sender, AnswersEventArgs e)
        {
            if (e.Answer != null)
            {
                _question.Answers.Remove(e.Answer);
                RaisePropertyChanged("Answers");
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Prompt")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "Text")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "Tag")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "Title")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "Image")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "SelectMany")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();

                if (columnName == "Answers")
                    return _questionValidator.ValidateProperty(_question, columnName).Errors.Select(x => x.Message).FirstOrDefault();
                        

                _logger.Log(Level.Warning, "Trying to validate {0} of question.", columnName);
                return null;
            }
        }

        #endregion

        #region Overridden methods

        public override void Cleanup()
        {
            base.Cleanup();

            DettachAnswerEvents(_answers);
            _answers.Cleanup();
        }

        #endregion
    }
}
