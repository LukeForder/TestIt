using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;
using Domain.Exceptions;
using Logging;

namespace Domain.Db
{
    public class SubjectQuery : ISubjectQuery
    {
        public SubjectQuery(Database database,  ILogger logger)
        {
            _db = database;
            _logger = logger ?? NullLogger.Instance;
        }

        private Database _db;
        private ILogger _logger;

        internal Database Database
        {
            get
            {
                return _db;
            }
        }

        protected ILogger Logger
        {
            get
            {
                return _logger;
            }
        }


        public Subject ById(Guid id)
        {
            try
            {
                return _db.Load<Subject>(id);
            }
            catch (Exception e)
            {
                _logger.Log(Level.Error, e.Message);
                throw new DomainException("Something went horribly wrong and the subject couldn't be retrieved.");
            }
        }

        public Subject ByName(string name)
        {
            try
            {
                var result =
                    _db.Query<Subject, string, Guid>(Database.SUBJECT_NAME_IDX)
                        .Where(x => x.Index == name)
                        .Select(s => s.LazyValue)
                        .FirstOrDefault();

                return (result == null) ? null : result.Value;
            }
            catch (Exception e)
            {
                _logger.Log(Level.Error, e.Message);
                throw new DomainException("Something went horribly wrong and the subject couldn't be retrieved.");
            }
        }

        public IEnumerable<Subject> All
        {
            get 
            {
                return
                    _db.Query<Subject, Guid>()
                        .Select(s => s.LazyValue.Value)
                        .ToList();
            }
        }

        public virtual void Cleanup()
        {
            _db = null;
            _logger = null;
        }
    }
}
