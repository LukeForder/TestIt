using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;
using Domain;

namespace Validation
{
    public class AssociatedSubjectValidator : AbstractValidator<AssociatedSubject>, Validation.IValidator<AssociatedSubject>
    {
        public AssociatedSubjectValidator()
        {
            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(-10)
                    .WithMessage("The minimum points allocation for a question is -10")
                .LessThanOrEqualTo(10)
                    .WithMessage("The maximum points alloction for a question is 10");
        }

        IValidationResult IValidator<AssociatedSubject>.Validate(AssociatedSubject instance)
        {
            return new FVValidationResultAdapter(base.Validate(instance));
        }

        IValidationResult IValidator<AssociatedSubject>.ValidateProperty(AssociatedSubject instance, string propertyName)
        {
            return new FVValidationResultAdapter(FluentValidation.DefaultValidatorExtensions.Validate(this, instance, new string[] { propertyName }));
        }
    }
}
