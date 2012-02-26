using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Admin.Messages
{
    public class ChooseFileMessage
    {
        public ChooseFileMessage(Action<string> callback)
        {
            _action = callback;
        }

        public string Extensions
        {
            get;
            set;
        }

        private Action<string> _action;

        public Action<string> ProcessResult
        {
            get
            {
                return _action;
            }
        }
    }
}
