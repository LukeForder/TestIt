using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;
using Logging;
using Domain.Exceptions;

namespace Domain.Db
{
    public class Subjects : SubjectQuery, ISubjects
    {
        public Subjects(Database database, IValidator<Subject> validator, ILogger logger)
            : base(database, logger)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");

            _validator = validator;
        }

        private IValidator<Subject> _validator;

        public void Add(Subject subject)
        {
            IValidationResult validationResult = _validator.Validate(subject);
            if (!validationResult.IsValid)
                throw new DomainValidationException("The subject could not be saved due to errors.", validationResult);

            try
            {
                Database.Save(subject);
                Database.Flush();
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error, e.Message);
                throw new DomainException("An error occured and the Subject could not be saved.");
            }
        }

        public void Remove(Subject subject)
        {
            Subject orginalSubject = Database.Load<Subject>(subject.Id);
            if (orginalSubject == null)
                throw new DomainException("The subject was not found.");

            try
            {
                Database.Delete(subject);
                Database.Flush();
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error, e.Message);
                throw new DomainException("An error occured and the Subject could not be deleted.");
            }
        }

        public void Update(Subject subject)
        {
            // ensure the subject is valid
            IValidationResult validationResult = _validator.Validate(subject);
            if (!validationResult.IsValid)
                throw new DomainValidationException("The subject could not be updated due to errors.", validationResult);

            // Using the strong definition of update... if the subject D.N.E throw an exception
            // this also prevents key changes
            Subject orginalSubject = Database.Load<Subject>(subject.Id);
            if (orginalSubject == null)
                throw new DomainException("The subject was not found.");

            try
            {
                Database.Save(subject);
                Database.Flush();
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error, e.Message);
                throw new DomainException("An error occured and the Subject could not be updated.");
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            _validator = null;
        }
    }
}
