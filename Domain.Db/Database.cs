//pre-processor defined for testing environment
#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.Sterling.Database;

namespace Domain
{
    public sealed class Database : BaseDatabaseInstance
    {
        public const string TEST_NAME_IDX = "IX_Test_Name";
        public const string SUBJECT_NAME_IDX = "IX_Test_Name";

        public override string Name
        {
            get
            {
#if !TEST
                return "FeatherDB";
#else
                return "FeatherTestDB";
#endif
            }
        }

        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition> 
            {
                CreateTableDefinition<Test, Guid>(x => x.Id)
                    .WithIndex<Test, string, Guid>(TEST_NAME_IDX, x => x.Name),
                CreateTableDefinition<Subject, Guid>(x => x.Id)
                    .WithIndex<Subject, string, Guid>(SUBJECT_NAME_IDX, x => x.Name)
            };
        }
    }
}
