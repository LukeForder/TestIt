using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.ComponentModel;

namespace UI.Client.Model
{
    public class AnswerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AnswerModel(Domain.Answer answer)
        {
            this._answer = answer;

            InitializeSubjects();
        }

        private IEnumerable<SubjectModel> _subjects;
        private Domain.Answer _answer;
        private bool _selected;

        public string Text
        {
            get
            {
                return _answer.Text;
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

                return image;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                }
            }
        }

        public IEnumerable<SubjectModel> Subjects
        {
            get
            {
                return _subjects;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeSubjects()
        {
            _subjects = _answer.Fields.Select(f => new SubjectModel(f)).ToList();
        }
    }
}
