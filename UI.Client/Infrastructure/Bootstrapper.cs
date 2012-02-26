using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Ioc;
using Dynamo.Ioc.Registration;
using Logging;
using System.IO;
using GalaSoft.MvvmLight;
using UI.Client.Models;
using Domain.Db;
using Wintellect.Sterling;
using Wintellect.Sterling.Server.FileSystem;
using UI.Client.Model;

namespace UI.Client.Infrastructure
{
    internal class Bootstrapper
    {
        private IocContainer _container;

        public IocContainer Container
        {
            get
            {
                return _container;
            }
        }

        public void Run()
        {
            _container = InitializeContainer();
            InitializeLogger();

            RegisterDatabase(_container);
            RegisterModels(_container);
            RegisterViewModels(_container);
            RegisterComponents(_container);


            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(
                new Microsoft.Practices.ServiceLocation.ServiceLocatorProvider(() => new Dynamo.Extensions.ServiceLocator.DynamoServiceLocator(_container))
            );
        }

        private void RegisterComponents(IocContainer container)
        {
            container.Register<Model.TextLoader>(resolver => new Model.TextLoader(resolver.Resolve<ILogger>()));
            container.Register<Model.IInterpretationBuilder>(resolver =>  new Model.DefaultAssessmentInterpreter());
            container.Register<Model.ITestEvaluator>(resolver => new Model.DefaultTestEvaluator(resolver.Resolve<ISubjectQuery>(), resolver.Resolve<TextLoader>(), resolver.Resolve<ILogger>()));
        }

        private void RegisterDatabase(IocContainer container)
        {
            SterlingEngine engine =
                new SterlingEngine();
            engine.Activate();

            container.RegisterInstance<Domain.Database>(engine.SterlingDatabase.RegisterDatabase<Domain.Database>(new FileSystemDriver()) as Domain.Database);
            container.Register<Domain.Db.ITestQuery, Domain.Db.TestQuery>();
            container.Register<Domain.Db.ISubjectQuery, Domain.Db.SubjectQuery>();
        }


        protected virtual IocContainer InitializeContainer()
        {
            return new IocContainer();
        }

        protected virtual void InitializeLogger()
        {
            BlackBox.FileSink fileTarget = new BlackBox.FileSink(){
                FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TestIt", "test.client.log"),
                Name = "FileLogger"
            };

            BlackBox.LogConfiguration configuration = new BlackBox.LogConfiguration();
            configuration.Sinks.Add(fileTarget);

            BlackBox.ILogKernel kernel = new BlackBox.LogKernel(configuration);

            _container.RegisterInstance<BlackBox.ILogKernel>(kernel);
            _container.Register<BlackBox.ILogger>(x => x.Resolve<BlackBox.ILogKernel>().GetLogger());
            _container.Register<ILogger, BlkBxLogger>();
        }

        protected virtual void RegisterViewModels(IocContainer container)
        {
            container.Register<ViewModelBase, ViewModels.ShellViewModel>("ShellViewModel");
            container.Register<ViewModelBase, ViewModels.WelcomeViewModel>("WelcomeViewModel");
            container.Register<ViewModelBase, ViewModels.UserRegistrationViewModel>("UserRegistrationViewModel");
            container.Register<ViewModelBase, ViewModels.TestPreviewViewModel>("TestPreviewViewModel");
            container.Register<ViewModelBase, ViewModels.TestSelectionViewModel>("TestSelectionViewModel");
            container.Register<ViewModelBase, ViewModels.TestViewModel>("TestViewModel");
            container.Register<ViewModelBase, ViewModels.TestEvaluationViewModel>("TestEvaluationViewModel");
        }


        private void RegisterModels(IocContainer container)
        {
            container
                .Register<IUserModel>(resolver => new Model.UserModel(resolver.Resolve<ILogger>()))
                .SetLifetime(new ContainerLifetime());
        }
    }
}
