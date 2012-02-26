using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Db
{
    public interface ISubjects : ISubjectQuery
    {
        /// <summary>
        /// Add a subject to the data store
        /// </summary>
        /// <param name="subject">The subject to add</param>
        /// <exception cref="DomainException">In the event to an error occuring.</exception>
        void Add(Subject subject);

        /// <summary>
        /// Remove a subject from the data store.
        /// </summary>
        /// <param name="subject">The subject to remove.</param>
        void Remove(Subject subject);

        /// <summary>
        /// Update an existing subject with the data store.
        /// </summary>
        /// <param name="subject">The subject to update.</param>
        void Update(Subject subject);
    }
}
