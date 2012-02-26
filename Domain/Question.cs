using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Domain
{
    public class Question
    {
        public string Title
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string Prompt
        {
            get;
            set;
        }

        public byte[] Image
        {
            get;
            set;
        }

        public bool SelectMany
        {
            set;
            get;
        }

        public string Tag
        {
            get;
            set;
        }

        public IList<Answer> Answers
        {
            get;
            set;
        }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}
