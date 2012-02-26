using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using Dynamo.Ioc;
using GalaSoft.MvvmLight;

namespace UI.Client.Infrastructure
{
    public class DynamicViewModelLocator : DynamicObject
    {
        private static IocContainer _container;

        static DynamicViewModelLocator()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            _container = bootstrapper.Container;
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
