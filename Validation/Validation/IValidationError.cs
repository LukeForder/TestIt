using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation
{
    public interface IValidationError
    {
        string PropertyName
        {
            get;
        }

        string Message
        {
            get;
        }
    }
}
