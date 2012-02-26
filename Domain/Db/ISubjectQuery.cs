using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;

namespace Domain.Db
{
    /// <summary>
    /// Provides access to the Subjects contained in the data store.
    /// </summary>
    public interface ISubjectQuery : ICleanup
    {
        IEnumerable<Subject> All
        {
            get;
        }

        /// <summary>
        /// Retrieve a subject by its Id.
        /// </summary>
        /// <param name="id">The id of the subject.</param>
        /// <returns>The subject with the given id or null if no subject exists with the id.</returns>
        Subject ById(Guid id);
        
        /// <summary>
        /// Retrieve a subject by its name.
        /// </summary>
        /// <param name="name">The name of the subject.</param>
        /// <returns>The subject with the given name or null if no subject exists with the name.</returns>
        Subject ByName(string name);
    }
}
