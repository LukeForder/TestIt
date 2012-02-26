using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace UI.Client.ViewModels
{
    public class OutcomeViewModel : ViewModelBase
    {
        private Model.OutcomeModel _outcome;
        private IEnumerable<SubjectResultViewModel> _subjectResults;

        public OutcomeViewModel(Model.OutcomeModel outcome)
        {
            this._outcome = outcome;

            InitializeSubjectResults();
        }

        public string Category
        {
            get
            {
                return _outcome.Category;
            }
        }

        public IEnumerable<SubjectResultViewModel> SubjectResult
        {
            get
            {
                return _subjectResults;
            }
        }

        public string Description
        {
            get
            {
                return _outcome.Description;
            }
        }

        private void InitializeSubjectResults()
        {
            TaskScheduler scheduler = TaskScheduler.Current;
            Task.Factory.StartNew(
                () => _outcome.SubjectOutcomes.Select(x => new SubjectResultViewModel(x.Key, x.Value)).ToList()
            )
            .ContinueWith(
                task =>
                {
                    _subjectResults = task.Result;
                    RaisePropertyChanged("SubjectResult");
                },
                scheduler
            );
        }
    }
}
