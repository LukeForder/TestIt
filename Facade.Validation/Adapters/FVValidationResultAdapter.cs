using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation
{
    public class FVValidationResultAdapter : IValidationResult
    {
        public FVValidationResultAdapter(FluentValidation.Results.ValidationResult result)
        {
            _result = result;
        }

        private readonly FluentValidation.Results.ValidationResult _result;

        public bool IsValid
        {
            get { return _result.IsValid; }
        }

        public IEnumerable<IValidationError> Errors
        {
            get { return _result.Errors.Select(e => new FVValidationFailureAdapter(e)); }
        }
    }
}
