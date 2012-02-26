using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;

namespace Domain.Db
{
    public interface ITestQuery : ICleanup
    {
        IEnumerable<Test> All
        {
            get;
        }

        /// <summary>
        /// Get a test by its id.
        /// </summary>
        /// <param name="id">The id of the test to retrieve.</param>
        /// <returns>The test with the given id or null if no such test is found.</returns>
        Test ById(Guid id);

        /// <summary>
        /// Get a test by its name.
        /// </summary>
        /// <param name="name">The name of the test to retrieve.</param>
        /// <returns>The test with the given name or null if no such test is found.</returns>
        Test ByName(string name);
    }
}
