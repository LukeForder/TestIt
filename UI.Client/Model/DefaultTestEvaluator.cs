using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Db;
using Logging;
using System.Threading.Tasks;

namespace UI.Client.Model
{
   public class DefaultTestEvaluator : ITestEvaluator
    {
        public DefaultTestEvaluator(ISubjectQuery subjects, TextLoader textLoader,  ILogger logger)
        {
            _subjects = subjects;
            _logger = logger;
            _textLoader = textLoader;

            InitializeSubjects();
        }

        private IEnumerable<Domain.Subject> _domainSubjects;
        private ISubjectQuery _subjects;
        private ILogger _logger;
        private TextLoader _textLoader;

        public AssessmentModel Assess(TestModel test)
        {
            List<OutcomeModel> outcomes = new List<OutcomeModel>();
            
            foreach (var group in  test.Questions.GroupBy(question => question.Tag))
            {
                var totalMarks = CalculateMarks(group.SelectMany(g => g.Answers));
                var achievedMarks = CalculateMarks(group.SelectMany(g => g.Answers).Where(a => a.IsSelected));
                var subjectResult = JoinSubject(CalculatePercentages(achievedMarks, totalMarks));
                
                outcomes.Add(new OutcomeModel(_textLoader, group.Key, subjectResult));
            }

            return new AssessmentModel(test.Name, outcomes);
        }

        private IDictionary<string, double> JoinSubject(IDictionary<Guid, double> percentages)
        {
            return 
                percentages
                    .Join(
                        _domainSubjects,
                        x => x.Key,
                        y => y.Id,
                        (left, right) => new { Name = right.Name, Percentage = left.Value }
                    )
                    .ToDictionary(
                        x => x.Name, x => x.Percentage
                    );
        }

        private IDictionary<Guid, int> CalculateMarks(IEnumerable<AnswerModel> answers)
        {
            return answers
                    .SelectMany(a => a.Subjects)
                    .GroupBy(s => s.Subject)
                    .Select(s => new { Id = s.Key, Marks = s.Sum(x => x.Points) })
                    .ToDictionary(s => s.Id, s => s.Marks);
        }

        private IDictionary<Guid, double> CalculatePercentages(IDictionary<Guid, int> achievedMarks, IDictionary<Guid, int> totalMarks)
        {
            var missingMarks = totalMarks.Select(t => t.Key).Except(achievedMarks.Select(a => a.Key));
            foreach (var missingMark in missingMarks)
            {
                achievedMarks.Add(missingMark, 0);
            }

            return achievedMarks
                    .Join(
                        totalMarks,
                        x => x.Key, 
                        y => y.Key, 
                        (left, right) => new { Id = left.Key, Percent = ((double)left.Value) / right.Value }
                    )
                    .ToDictionary(x => x.Id, x => x.Percent);
        }

        private void InitializeSubjects()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () => _subjects.All
            )
            .ContinueWith(
                task =>
                {
                    _domainSubjects = task.Result;
                },
                scheduler
            );
        }

        public string Name
        {
            get { return "Default"; }
        }
    }
}
