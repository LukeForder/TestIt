using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation
{
    public interface IValidationResult
    {
        bool IsValid { get; }

        IEnumerable<IValidationError> Errors { get; }
    }
}
