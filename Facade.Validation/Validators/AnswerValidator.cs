using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Domain;

namespace Validation
{
    public class AnswerValidator : AbstractValidator<Answer>, IValidator<Answer>
    {
        public AnswerValidator(IValidator<AssociatedSubject> subjectValidator)
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .Unless(x => x.Image != null)
                .WithMessage("The answer must either have answer text or an image.");
            
            RuleFor(x => x.Image)
                .NotNull()
                .Unless(x => !string.IsNullOrEmpty(x.Text))
                .WithMessage("The answer must either have answer text or an image.");

            RuleFor(x => x.Fields)
                .Must(x =>
                    {
                        return !x.Any(e => !subjectValidator.Validate(e).IsValid);
                    })
                    .WithMessage("Some of the subjects associated with this answer have errors.");
        }

        private bool HaveAtLeastOnePointsAlloction(IList<AssociatedSubject> subjects)
        {
            foreach (AssociatedSubject subject in subjects)
            {
                if (subject.Points != 0)
                    return true;
            }

            return false;
        }

        IValidationResult IValidator<Answer>.Validate(Answer instance)
        {
            return new FVValidationResultAdapter(base.Validate(instance));
        }


        IValidationResult IValidator<Answer>.ValidateProperty(Answer instance, string propertyName)
        {
            return new FVValidationResultAdapter(DefaultValidatorExtensions.Validate(this, instance, new string[] { propertyName }));
        }
    }
}
