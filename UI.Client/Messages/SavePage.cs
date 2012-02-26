using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace UI.Client.Messages
{
    class SavePage
    {
        public SavePage(Action<string> callback)
        {
            _callback = callback;
        }

        private Action<string> _callback;

        public Action<string> Callback
        {
            get
            {
                return _callback;
            }
        }
    }
}
