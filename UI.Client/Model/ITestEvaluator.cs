using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.Model
{
    public interface ITestEvaluator
    {
        string Name
        {
            get;
        }

        AssessmentModel Assess(TestModel test);
    }
}
