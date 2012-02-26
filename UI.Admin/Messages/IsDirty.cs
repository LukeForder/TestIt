using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Admin.Messages
{
    class IsDirty
    {
        private readonly static IsDirty _dirtyInstance = new IsDirty(true);
        private readonly static IsDirty _cleanInstance = new IsDirty(false);

        public static IsDirty DirtyMessage
        {
            get
            {
                return _dirtyInstance;
            }
        }

        public static IsDirty CleanMessage
        {
            get
            {
                return _cleanInstance;
            }
        }

        public IsDirty(bool dirty)
        {
            this._dirty = dirty;
        }

        private readonly bool _dirty;

        public bool Dirty
        {
            get
            {
                return _dirty;
            }
        }
    }
}
