using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.Model
{
    public class AssessmentModel
    {
        private List<OutcomeModel> _outcomes;
        private string _intepretation;
        private string _test;
        
        public AssessmentModel(string testName, List<OutcomeModel> outcomes)
        {
            this._outcomes = outcomes;
            this._test = testName;
        }

        public string Test
        {
            get
            {
                return _test;
            }
        }

        public IEnumerable<OutcomeModel> Outcomes
        {
            get
            {
                return _outcomes;
            }
        }

        public string Interpretation
        {
            get
            {
                return _intepretation;
            }
        }

        public void Intepret(IInterpretationBuilder intepretationBuilder)
        {
            _intepretation = intepretationBuilder.Intepret(this);
        }
    }
}
