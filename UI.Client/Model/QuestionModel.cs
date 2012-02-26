using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.IO;

namespace UI.Client.Model
{
    public class QuestionModel
    {

        public QuestionModel(Domain.Question question)
        {
            this._question = question;

            InitializeAnswers();
        }

        private Domain.Question _question;
        private ReadOnlyCollection<AnswerModel> _answers;

        public string Tag
        {
            get
            {
                return _question.Tag;
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

        public string Prompt
        {
            get
            {
                return _question.Prompt;
            }
        }

        public BitmapImage Image
        {
            get
            {
                if (_question.Image == null)
                    return null;

                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(_question.Image);
                image.EndInit();

                return image;
            }
        }

        public bool SelectMany
        {
            get
            {
                return _question.SelectMany;
            }
        }

        public ReadOnlyCollection<AnswerModel> Answers
        {
            get
            {
                return _answers;
            }
        }

        private void InitializeAnswers()
        {
            _answers =
                new ReadOnlyCollection<AnswerModel>(
                    _question.Answers
                        .Select(answer => new AnswerModel(answer))
                        .ToList()
                    );
        }

    }
}
