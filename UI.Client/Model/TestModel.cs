using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Domain;
using Logging;
using System.Threading.Tasks;

namespace UI.Client.Model
{
    public class TestModel
    {
        public TestModel(Test test, ILogger logger)
        {
            _logger =
                (logger == null) ? NullLogger.Instance : logger;

            if (test == null)
            {
                _logger.Log(Level.Error, "TestModel: test is null");
                throw new ArgumentNullException("test");
            }

            _test = test;
            _assessments = new Dictionary<string, AssessmentModel>();

            InitializeQuestions(test);
        }

        private Test _test;
        private ILogger _logger;
        private ReadOnlyCollection<QuestionModel> _questions;
        private IDictionary<string, AssessmentModel> _assessments;

        public ReadOnlyCollection<QuestionModel> Questions
        {
            get
            {
                return _questions;                    
            }
        }

        public string Name
        {
            get
            {
                return _test.Name;
            }
        }

        public IDictionary<string, AssessmentModel> Assessments
        {
            get
            {
                return _assessments;
            }
        }

        private void InitializeQuestions(Test test)
        {
            _questions = 
                new ReadOnlyCollection<QuestionModel>(
                    _test.Questions
                        .Select(question => new QuestionModel(question))
                        .ToList()
                );               
        }
    }
}
