using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using UI.Client.Model;

namespace UI.Client.ViewModels
{
    public class StandAloneAnswerViewModel : AnswerViewModel
    {
        public StandAloneAnswerViewModel(AnswerModel answer, Guid questionId)
            : base(answer)
        {
            _questionId = questionId;
        }

        private Guid _questionId;

        public Guid QuestionId
        {
            get
            {
                return _questionId;
            }
        }

    }
}
