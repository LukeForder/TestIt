using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Admin.Infrastructure
{
    class NullSink : BlackBox.LogSink
    {
        protected override void WriteEntry(BlackBox.ILogEntry entry)
        {
            //do nothing
        }
    }
}
