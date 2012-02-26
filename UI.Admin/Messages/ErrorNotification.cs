using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Admin.Messages
{
    public class ErrorNotification
    {
        public ErrorNotification(string message)
        {
            _message = message;
        }

        private readonly string _message;

        public string Message
        {
            get
            {
                return _message;
            }
        }


    }
}
