using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Domain;
using Domain.Db;

namespace Validation
{
    public sealed class SubjectValidator : AbstractValidator<Subject>, IValidator<Subject>
    {
        public SubjectValidator(ISubjectQuery subjects)
        {
            if (subjects == null)
                throw new ArgumentNullException("subjects");

            RuleFor(s => s.Name)
                .NotEmpty()
                    .WithMessage("The subject must be given a name.")
                .Must((subject, name) =>
                    {
                        var searchResult = subjects.ByName(name);
                        if (searchResult == null)
                            return true;

                        return (searchResult.Id == subject.Id) ? true : false;
                    })
                    .WithMessage("A subject already exists with that name");
        }

        IValidationResult IValidator<Subject>.Validate(Subject instance)
        {
            return new FVValidationResultAdapter(base.Validate(instance));
        }

        IValidationResult IValidator<Subject>.ValidateProperty(Subject instance, string propertyName)
        {
            return new FVValidationResultAdapter(this.Validate<Subject>(instance, propertyName));
        }
    }
}
