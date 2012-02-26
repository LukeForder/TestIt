using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using Dynamo.Ioc;
using GalaSoft.MvvmLight;

namespace UI.Admin.Infrastructure
{
    public class DynamicViewModelLocator : DynamicObject
    {
        private static IocContainer _container;

        static DynamicViewModelLocator()
        {
            _container = new Bootstrapper().ServiceProvider;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            ViewModelBase vm;
            var success = _container.TryResolve<ViewModelBase>(binder.Name, out vm);
            result = vm;
            return success;
        }
    }
}
