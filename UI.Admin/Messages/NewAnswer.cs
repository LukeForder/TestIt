using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace UI.Admin.Messages
{
    public class NewAnswer
    {
        public NewAnswer(Question question)
        {
            _question = question;
        }

        private readonly Question _question;

        public Question Question
        {
            get
            {
                return _question;
            }
        }
    }
}
