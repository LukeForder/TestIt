using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace UI.Admin.Infrastructure.Templating
{
    public class SubjectTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item is ViewModels.SubjectViewModel)
                return ((ContentPresenter)container).FindResource("SubjectViewTemplate") as System.Windows.DataTemplate;

            if (item is ViewModels.EditingSubjectViewModel)
                return ((ContentPresenter)container).FindResource("EditingSubjectViewTemplate") as System.Windows.DataTemplate;

            return null;
        }
    }
}
