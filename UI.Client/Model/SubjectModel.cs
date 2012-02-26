using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Client.Model
{
    public class SubjectModel
    {
        private Domain.AssociatedSubject _associatedSubject;

        public SubjectModel(Domain.AssociatedSubject associatedSubject)
        {
            this._associatedSubject = associatedSubject;
        }

        public Guid Subject
        {
            get
            {
                return _associatedSubject.SubjectId;
            }
        }

        public int Points
        {
            get
            {
                return _associatedSubject.Points;
            }
        }
    }
}
