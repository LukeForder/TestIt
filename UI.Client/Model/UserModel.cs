using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.Client.Models;
using System.ComponentModel;
using Domain;
using Logging;

namespace UI.Client.Model
{
    public class UserModel : IUserModel, INotifyPropertyChanged
    {
        public UserModel(ILogger logger)
        {
            _logger = (logger == null) ? NullLogger.Instance : logger;
        }

        private string _name;
        private ILogger _logger;
        private TestModel _currentTest;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void BeginTest(Test test)
        {
            if (_currentTest != null)
                throw new InvalidOperationException("Finish the current test before starting a new test");

            _currentTest = new TestModel(test, _logger);

            RaisePropertyChanged("CurrentTest");
        }


        public TestModel CurrentTest
        {
            get { return _currentTest; }
        }

        public TestModel EndTest(ITestEvaluator evaluator)
        {
             _currentTest.Assessments.Add(evaluator.Name, evaluator.Assess(_currentTest));
             TestModel test = _currentTest;
             _currentTest = null;
             return test;
        }
    }
}
