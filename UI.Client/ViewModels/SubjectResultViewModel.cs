using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.ViewModels
{
    public class SubjectResultViewModel
    {
        public SubjectResultViewModel(string subjectName, double percentage)
        {
            _subjectName = subjectName;
            _percentage = percentage;
        }

        private double _percentage;
        private string _subjectName;

        public double Percentage
        {
            get
            {
                return _percentage;
            }
        }

        public string SubjectName
        {
            get
            {
                return _subjectName;
            }
        }
    }
}
