using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Domain;

namespace Validation
{
    public class QuestionValidator : AbstractValidator<Question>, Validation.IValidator<Question>
    {
        public QuestionValidator(IValidator<Answer> answerValidator)
        {
            if (answerValidator == null)
                throw new ArgumentNullException("answerValidator");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("The question must be given a title.");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("The question must be given text.");

            RuleFor(x => x.Prompt)
                .NotEmpty()
                .WithMessage("The question must be given an answer prompt.");

            RuleFor(x => x.Tag)
                .NotEmpty()
                .WithMessage("The question must be given a category tag.");

            RuleFor(q => q.Answers)
                .NotEmpty()
                    .WithMessage("A question must have at least one possible answer.")
                .Must(answers =>
                    {
                        foreach (Answer answer in answers)
                        {
                            if (!answerValidator.Validate(answer).IsValid)
                                return false;
                        }

                        return true;
                    })
                    .WithMessage("Some of your answers have errors.");
        }

        IValidationResult IValidator<Question>.Validate(Question instance)
        {
            return new FVValidationResultAdapter(base.Validate(instance));
        }


        IValidationResult IValidator<Question>.ValidateProperty(Question instance, string propertyName)
        {
            return new FVValidationResultAdapter(FluentValidation.DefaultValidatorExtensions.Validate(this, instance, new string[]{propertyName}));
        }
    }
}
