using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation
{
    public interface IValidator<TType>
    {
        IValidationResult Validate(TType instance);
        IValidationResult ValidateProperty(TType instance, string propertyName);
    }
}
