using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Logging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading.Tasks;
using Validation;
using Domain.Db;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;

namespace UI.Admin.ViewModels
{
    public class AnswerViewModel : ViewModelExtended, IDataErrorInfo
    {
        public event EventHandler RequestsRemovalEvent;

        public AnswerViewModel(
            Answer answer, 
            IValidator<Answer> answerValidator,
            IValidator<AssociatedSubject> subjectValidator,
            ISubjectQuery subjectQuery,
            Guid busId)
        {
            _answer = answer;
            _busId = busId;

            _answerValidator = answerValidator;
            _subjectValidator = subjectValidator;
            _subjectQuery = subjectQuery;

            InitializeCommands();
            InitializeSubjects();

            _logger.Log(Level.Trace, "Created a new answer");
        }

        private void InitializeSubjects()
        {
            _associatedSubjects = 
                new AssociatedSubjectsViewModel(
                    _answer.Fields,
                    _subjectValidator,
                    _subjectQuery,
                    _busId
                );

            _associatedSubjects.SubjectAddedEvent += SubjectAddedHandler;
            _associatedSubjects.SubjectRemovedEvent += SubjectRemovedHandler;
        }

        private readonly Guid _busId;
        private Answer _answer;
        private ISubjectQuery _subjectQuery;
        private IValidator<AssociatedSubject> _subjectValidator;
        private IValidator<Answer> _answerValidator;
        
        private RelayCommand _selectImageCommand;
        private RelayCommand _requestRemovalCommand;
        private RelayCommand _makeImageNullCommand;
        private AssociatedSubjectsViewModel _associatedSubjects;


        internal Answer Answer
        {
            get
            {
                return _answer;
            }
        }

        public string Text
        {
            get
            {
                _logger.Log(Level.Trace, "Got the text for an answer");

                return _answer.Text;
            }
            set
            {
                if (_answer.Text != value)
                {
                    _answer.Text = value;
                    RaisePropertyChanged(string.Empty);

                    _logger.Log(Level.Trace, "Changed the text for an answer");
                }
            }
        }

        public BitmapImage Image
        {
            get
            {
                if (_answer.Image == null)
                    return null;

                var image = new BitmapImage();

                image.BeginInit();
                image.StreamSource = new MemoryStream(_answer.Image);
                image.EndInit();

                _logger.Log(Level.Trace, "Got the image for an answer");

                return image;
            }
        }

        public ICommand SelectImageCommand
        {
            get
            {
                return _selectImageCommand;
            }
        }

        public ICommand RequestRemovalCommand
        {
            get
            {
                return _requestRemovalCommand;
            }
        }

        public ICommand MakeImageNullCommand
        {
            get
            {
                return _makeImageNullCommand;
            }
        }

        public AssociatedSubjectsViewModel Subjects
        {
            get
            {
                return _associatedSubjects;
            }
        }

        private void InitializeCommands()
        {
            _requestRemovalCommand =
                new RelayCommand(
                    () =>
                    {
                        RaiseRequestRemoval();
                    });

            _selectImageCommand = new RelayCommand(
                () =>
                {
                    MessengerInstance.Send<Messages.ChooseFileMessage>(new Messages.ChooseFileMessage(LoadImage));
                });

            _makeImageNullCommand = new RelayCommand(
                () =>
                {
                    _answer.Image = null;
                    RaisePropertyChanged(string.Empty);
                }
            );
        }

        private void LoadImage(string filePath)
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    byte[] imageData = null;
                    using (var stream = File.Open(filePath, FileMode.Open))
                    {
                        imageData = new byte[stream.Length];
                        stream.Read(imageData, 0, imageData.Length);
                        stream.Close();
                    }
                    return imageData;
                }
            )
            .ContinueWith(
                task =>
                {
                    if (task.Exception != null)
                    {
                        MessengerInstance.Send<DialogMessage>(new DialogMessage("The image could not be read, please try again or select another image.", m => {}));
                        _logger.Log(Level.Error, "Attempting to set the image for an answer threw an exception.");

                    }
                    else
                    {
                        _answer.Image = task.Result;
                        RaisePropertyChanged(string.Empty);

                        _logger.Log(Level.Trace, "Changed the image for an answer");
                    }
                },
                scheduler
            );
        }
        
        private void RaiseRequestRemoval()
        {
            var handler = RequestsRemovalEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get 
            {
                if (columnName == "Text")
                    return _answerValidator.ValidateProperty(this._answer, columnName).Errors.Select(e => e.Message).FirstOrDefault();

                if (columnName == "Image")
                    return _answerValidator.ValidateProperty(this._answer, columnName).Errors.Select(e => e.Message).FirstOrDefault();

                if (columnName == "Subjects")
                    return _answerValidator.ValidateProperty(this._answer, "Fields").Errors.Select(e => e.Message).FirstOrDefault();

                _logger.Log(Level.Warning, "Tried to validate {0} on answer.", columnName);
                return null;
            }
        }


        void SubjectRemovedHandler(object sender, SubjectEventArgs e)
        {
            RemoveSubject(e.Subject);
        }

        private void RemoveSubject(AssociatedSubject associatedSubject)
        {
            if (associatedSubject != null)
                _answer.Fields.Remove(associatedSubject);
        }

        void SubjectAddedHandler(object sender, SubjectEventArgs e)
        {
            AddSubject(e.Subject);
        }

        private void AddSubject(AssociatedSubject associatedSubject)
        {
            if (associatedSubject != null)
                _answer.Fields.Add(associatedSubject);
        }

    }
}
