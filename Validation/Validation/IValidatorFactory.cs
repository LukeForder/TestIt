using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;

namespace Validation
{
    public interface IValidatorFactory
    {
        IValidator<T> Build<T>();
    }
}
