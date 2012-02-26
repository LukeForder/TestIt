using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.Model
{
    public class DefaultAssessmentInterpreter : IInterpretationBuilder
    {
        public string Intepret(AssessmentModel assessment)
        {
            return string.Empty;
        }
    }
}
