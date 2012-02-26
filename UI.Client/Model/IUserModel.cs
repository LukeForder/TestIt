using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using UI.Client.Model;

namespace UI.Client.Models
{
    public interface IUserModel
    {
        string Name
        {
            get;
            set;
        }

        TestModel CurrentTest
        {
            get;
        }

        void BeginTest(Domain.Test test);
        TestModel EndTest(ITestEvaluator evalutor);
    }
}
