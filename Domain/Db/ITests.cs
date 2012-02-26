using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Db
{
    public interface ITests : ITestQuery
    {
        /// <summary>
        /// Add a test to the data store.
        /// </summary>
        /// <param name="test">The test to add.</param>
        /// <exception cref="DomainException">Thrown if the save fails.</exception>
        void Add(Test test);

        /// <summary>
        /// Remove an existing test from the data store.
        /// </summary>
        /// <param name="test">The test to remove.</param>
        /// <exception cref="DomainException">Thrown if the save fails.</exception>
        void Remove(Test test);

        /// <summary>
        /// Update an existing test
        /// </summary>
        /// <param name="test">The test to update.</param>
        /// <exception cref="DomainException">Thrown if the save fails.</exception>
        void Update(Test test);
    }
}
