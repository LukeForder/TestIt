using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dynamo.Ioc;
using Validation;
using Domain;
using Wintellect.Sterling;
using Logging;
using Domain.Db;
using UI.Admin.ViewModels;
using GalaSoft.MvvmLight;
using System.IO;
using Microsoft.Practices.ServiceLocation;
using Dynamo.Extensions.ServiceLocator;
using Wintellect.Sterling.Server.FileSystem;
using System.Windows;

namespace UI.Admin.Infrastructure
{
    public class Bootstrapper
    {
        private readonly IocContainer _container;

        public Bootstrapper()
        {
            _container = new IocContainer();

            InitialiseLogger();
            InitialiseDatabase();
            InitialiseValidators();
            InitialiseViewModels();

            _container.RegisterInstance<IResolver>(_container);

            ServiceLocator.SetLocatorProvider(() => new DynamoServiceLocator(_container));
            
            App.Current.Exit += new System.Windows.ExitEventHandler(TearDown);   
        }

        public IocContainer ServiceProvider
        {
            get
            {
                return _container;
            }
        }

        private void InitialiseLogger()
        {
            BlackBox.LogConfiguration loggerConfig = new BlackBox.LogConfiguration();

            string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string logPath = Path.Combine(applicationData, "TestIt");
            try
            {                
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                loggerConfig.Sinks.Add(new BlackBox.FileSink() { FileName = Path.Combine(logPath, "log.txt"), Name = "File", Format = "$(time(format='HH:mm:ss')): $(message())" });

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("There was an error initializing the logging component.\nThe application will now run without logging");

                loggerConfig.Sinks.Add(new NullSink());
            }

            
            BlackBox.LogKernel kernel = new BlackBox.LogKernel(loggerConfig);

            _container.RegisterInstance<BlackBox.ILogKernel>(kernel);
            _container.Register<BlackBox.ILogger>(x => x.Resolve<BlackBox.ILogKernel>().GetLogger());
            _container.Register<ILogger, BlkBxLogger>();
        }

        private void InitialiseViewModels()
        {
            
            _container.Register<ViewModelBase, ShellViewModel>(typeof(ShellViewModel).Name);
            _container.Register<ViewModelBase, SubjectsViewModel>(typeof(SubjectsViewModel).Name);
            _container.Register<ViewModelBase, TestsViewModel>(typeof(TestsViewModel).Name);
        }


        private void InitialiseValidators()
        {
            _container.Register<IValidator<Test>, TestValidator>();
            _container.Register<IValidator<Question>, QuestionValidator>();
            _container.Register<IValidator<Answer>, AnswerValidator>();
            _container.Register<IValidator<Subject>, SubjectValidator>();
            _container.Register<IValidator<AssociatedSubject>, AssociatedSubjectValidator>();
        }

        private void InitialiseDatabase()
        {

            SterlingEngine engine = new SterlingEngine();
            engine.Activate();

            _container.RegisterInstance<SterlingEngine>(engine);
            _container.RegisterInstance<Database>(engine.SterlingDatabase.RegisterDatabase<Database>(new FileSystemDriver()) as Database);
            _container.Register<ISubjectQuery, SubjectQuery>();
            _container.Register<ISubjects, Subjects>();
            _container.Register<ITests, Tests>();
            _container.Register<ITestQuery, TestQuery>();
        }

        void TearDown(object sender, System.Windows.ExitEventArgs e)
        {
            try
            {
                _container.Resolve<SterlingEngine>()
                    .Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}

