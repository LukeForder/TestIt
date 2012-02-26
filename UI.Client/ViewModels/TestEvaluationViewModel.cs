using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using UI.Client.Model;
using UI.Client.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using System.IO;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Printing;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Messaging;

namespace UI.Client.ViewModels
{
    public class TestEvaluationViewModel : ViewModelBase
    {
        public TestEvaluationViewModel(
            IUserModel user,
            ITestEvaluator evaluator,
            IInterpretationBuilder interpreter
        )
        {
            if (evaluator == null)
                throw new ArgumentNullException("evaluator");

            if (interpreter == null)
                throw new ArgumentNullException("interpreter");

            if (user == null)
                throw new ArgumentNullException("user");

            _userName = user.Name;

            InitializeAssessment(user, evaluator, interpreter);
            InitalizeCommands();
        }
        
        private string _userName;
        private IEnumerable<OutcomeViewModel> _outcomes;
        private AssessmentModel _assessment;
        
        private RelayCommand<UIElement> _printPageCommand;
        private RelayCommand<UIElement> _savePageCommand;

        public IEnumerable<OutcomeViewModel> Outcomes
        {
            get
            {
                return _outcomes;
            }
        }

        public string Test
        {
            get
            {
                return (_assessment == null) ? null : _assessment.Test;
            }
        }

        public string User
        {
            get
            {
                return _userName;
            }
        }

        public ICommand PrintPageCommand
        {
            get
            {
                return _printPageCommand;
            }
        }

        public ICommand SavePageCommand
        {
            get
            {
                return _savePageCommand;
            }
        }

        private void InitializeAssessment(IUserModel user, ITestEvaluator evaluator, IInterpretationBuilder interpreter)
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    TestModel test = user.EndTest(evaluator);

                    AssessmentModel assessment = test.Assessments[test.Assessments.Keys.ElementAt(0)];

                    interpreter.Intepret(assessment);

                    var outcomeViewModels = assessment.Outcomes.Select(x => new OutcomeViewModel(x)).ToList();

                    return new { Assessment = assessment, Outcomes = outcomeViewModels };
                }
            )
            .ContinueWith(
                task =>
                {
                    _outcomes = task.Result.Outcomes;
                    _assessment = task.Result.Assessment;

                    RaisePropertyChanged(string.Empty);
                },
                scheduler
            );
        }

        private void InitalizeCommands()
        {
            _printPageCommand = new RelayCommand<UIElement>(
                ui => PrintPage(ui)
            );

            _savePageCommand = new RelayCommand<UIElement>(
                ui => SavePage(ui)
            );
        }

        private void PrintPage(UIElement visual)
        {
             var documentViewer = visual as FlowDocumentScrollViewer;
             if (documentViewer != null)
             {
                 documentViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                 MessengerInstance.Send<Messages.PrintPage>(new Messages.PrintPage(documentViewer, "Printing assessment..."));
             }
        }

        private void SavePage(UIElement control)
        {
            if (control != null)
            {
                MessengerInstance.Send<Messages.SavePage>(new Messages.SavePage(s => WritePage(s, control)));
            }
        }

        private void WritePage(string filepath, UIElement toSave)
        {
            try
            {
                if (File.Exists(filepath))
                    File.Delete(filepath);

                XpsDocument xpsDocument = new XpsDocument(filepath, FileAccess.Write);
                XpsDocumentWriter documentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                var documentViewer = toSave as FlowDocumentScrollViewer;
                if (documentViewer != null)
                {
                    FlowDocument document = documentViewer.Document;

                    Size a4 = new Size(8.27 * 96, 11.69 * 96);
                    documentViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    
                    documentViewer.Measure(a4);
                    documentViewer.Arrange(new Rect(a4));

                    VisualsToXpsDocument visualCollator = (VisualsToXpsDocument)documentWriter.CreateVisualsCollator();
                    visualCollator.Write(documentViewer);
                    visualCollator.EndBatchWrite();
                    xpsDocument.Close();

                    documentViewer.LayoutTransform = null;
                    documentViewer.UpdateLayout();
                }
            }
            catch (Exception)
            {
                MessengerInstance.Send<DialogMessage>(new DialogMessage("The assessment report could not be saved.", msg => {}));
            }
        }
    }
}
