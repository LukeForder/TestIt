using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using UI.Admin.ViewModels;
using System.Windows;

namespace UI.Admin.Infrastructure.Templating
{
    public class TestTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            ContentPresenter presenter = (ContentPresenter)container;

            if (item == null || item is ObservableCollection<TestViewModel>)
                return presenter.FindResource("TestsTemplate") as DataTemplate;

            if (item is EditingTestViewModel)
                return presenter.FindResource("EditTestTemplate") as DataTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
