using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Test
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IList<Question> Questions
        {
            get;
            set;
        }

        public Test()
        {
            Id = Guid.NewGuid();
            Questions = new List<Question>();
        }
    }
}
