using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.Messages
{
    public class NavigateTo
    {
        public NavigateTo(string page)
        {
            _page = page;
        }

        private readonly string _page;
       
        public string Page
        {
            get
            {
                return _page;
            }
        }
    }
}
