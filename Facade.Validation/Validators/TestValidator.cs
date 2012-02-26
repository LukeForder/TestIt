using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

using FluentValidation;
using Domain.Db;

namespace Validation
{
    public sealed class TestValidator : AbstractValidator<Test>, Validation.IValidator<Test>
    {
        public TestValidator(ITestQuery queryStore, Validation.IValidator<Question> questionValidator)
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                    .WithMessage("The test must be given a name.")
                .Must((test, name) =>
                    {
                        var subject = queryStore.ByName(name);

                        if (subject == null)
                            return true;

                        return (subject.Id == test.Id) ? true : false;
                    })
                    .WithMessage("A test already exists with this name.");
            
            RuleFor(t => t.Questions)
                .NotEmpty()
                    .WithMessage("A test have have at least one question.");

            RuleFor(t => t.Questions)
                .Must(questions =>
                {
                    foreach (Question question in questions)
                    {
                        if (!questionValidator.Validate(question).IsValid)
                            return false;
                    }

                    return true;
                })
                .WithMessage("At least one of the questions belonging to this test has errors.");
        }

        IValidationResult IValidator<Test>.Validate(Test instance)
        {
            return new FVValidationResultAdapter(base.Validate(instance));
        }


        public IValidationResult ValidateProperty(Test instance, string propertyName)
        {
            return new FVValidationResultAdapter(DefaultValidatorExtensions.Validate(this, instance, new string[]{ propertyName }));
        }
    }
}
