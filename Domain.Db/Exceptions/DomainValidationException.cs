using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;

namespace Domain.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public DomainValidationException(string message)
            : this(message, null)
        {
        }

        public DomainValidationException(string message, IValidationResult validationResult)
            : base(message)
        {
            _validationResult = validationResult;
        }

        private IValidationResult _validationResult;

        public IEnumerable<IValidationError> Errors
        {
            get
            {
                return (_validationResult == null) ? new IValidationError[0] : _validationResult.Errors;
            }
        }
    }
}
