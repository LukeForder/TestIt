using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;
using Logging;
using Domain.Exceptions;

namespace Domain.Db
{
    public class TestQuery : ITestQuery
    {
        public TestQuery( Database database, ILogger logger)
        {
            _db = database;
            _logger = logger ?? NullLogger.Instance;            
        }

        private Database _db;
        private ILogger _logger;

        protected ILogger Logger
        {
            get
            {
                return _logger;
            }
        }

        internal Database Database
        {
            get
            {
                return _db;
            }
        }

        public Test ById(Guid id)
        {
            return _db.Load<Test>(id);
        }

        public Test ByName(string name)
        {
            var result = _db.Query<Test, string, Guid>(Database.TEST_NAME_IDX)
                .Where(q => q.Index == name)
                .Select(q => q.LazyValue)
                .FirstOrDefault();

            return (result == null) ? null : result.Value;
        }

        public IEnumerable<Test> All
        {
            get 
            {
                return _db.Query<Test, Guid>()
                    .Select(t => t.LazyValue)
                    .Where(l => l != null)
                    .Select(l => l.Value)
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