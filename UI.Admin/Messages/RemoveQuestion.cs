using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using UI.Admin.ViewModels;

namespace UI.Admin.Messages
{
    class RemoveQuestion
    {
        public RemoveQuestion(QuestionViewModel question)
        {
            if (question == null)
                throw new ArgumentNullException("question");

            _question = question;
        }

        private readonly QuestionViewModel _question;

        public QuestionViewModel Question
        {
            get
            {
                return _question;
            }
        }
    }
}
