using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.Model
{
    public interface IInterpretationBuilder
    {
        string Intepret(AssessmentModel assessment);
    }
}
