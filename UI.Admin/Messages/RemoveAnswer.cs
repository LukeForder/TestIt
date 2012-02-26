using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace UI.Admin.Messages
{
    public class RemoveAnswer
    {
        public RemoveAnswer(Question question, Answer answer)
        {
            _question = question;
            _answer = answer;
        }

        private readonly Question _question;
        private readonly Answer _answer;

        public Question Question
        {
            get
            {
                return _question;
            }
        }

        public Answer Answer
        {
            get
            {
                return _answer;
            }
        }
            
    }
}
