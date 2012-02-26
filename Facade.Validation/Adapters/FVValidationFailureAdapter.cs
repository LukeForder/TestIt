using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation
{
    public sealed class FVValidationFailureAdapter : IValidationError
    {
        public FVValidationFailureAdapter(FluentValidation.Results.ValidationFailure failure)
        {
            this._failure = failure;
        }

        private readonly FluentValidation.Results.ValidationFailure _failure;

        public string PropertyName
        {
            get { return _failure.PropertyName; }
        }

        public string Message
        {
            get { return _failure.ErrorMessage; }
        }
    }
}
