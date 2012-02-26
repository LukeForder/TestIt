using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;
using UI.Client.Model;

namespace UI.Client.ViewModels
{
    public abstract class AnswerViewModel : ViewModelBase
    {
        public AnswerViewModel(AnswerModel answer)
        {
            if (answer == null)
                throw new ArgumentNullException("answer");

            _answer = answer;
        }

        private AnswerModel _answer;

        internal AnswerModel Answer
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
                return _answer.Text;
            }
        }

        public BitmapImage Image
        {
            get
            {
                return _answer.Image;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _answer.IsSelected;
            }
            set
            {
                if (_answer.IsSelected != value)
                {
                    _answer.IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }
    }
}
