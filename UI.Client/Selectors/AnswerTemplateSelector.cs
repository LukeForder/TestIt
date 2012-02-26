using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UI.Client.ViewModels;
using System.Windows;

namespace UI.Client.Selectors
{
    public class AnswerTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            ContentPresenter presenter = container as ContentPresenter;
            if (presenter != null)
            {
                if (item is SetMemberAnswerViewModel)
                    return presenter.FindResource("CheckedAnswerTemplate") as DataTemplate;

                if (item is StandAloneAnswerViewModel)
                    return presenter.FindResource("RadioAnswerTemplate") as DataTemplate; 

            }

            return base.SelectTemplate(item, container);
        }
    }
}
