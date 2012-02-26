using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace UI.Client.Messages
{
    class PrintPage
    {
        public PrintPage(UIElement element, string description)
        {
            _description = description;
            _element = element;
        }

        private UIElement _element;
        private string _description;

        public UIElement Element
        {
            get
            {
                return _element;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }
    }
}