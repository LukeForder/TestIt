using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Domain
{
    public class Answer
    {
        public string Text
        {
            get;
            set;
        }

        public byte[] Image
        {
            get;
            set;
        }

        public IList<AssociatedSubject> Fields
        {
            get;
            set;
        }

        public Answer()
        {
            Fields = new List<AssociatedSubject>();
        }
    }
}
