using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;
using Domain.Exceptions;
using Logging;

namespace Domain.Db
{
    public class Tests : TestQuery, ITests
    {
        public Tests(Database database, IValidator<Test> validator, ILogger logger)
            : base(database, logger)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");

            _validator = validator;
        }

        private IValidator<Test> _validator;

        public void Add(Test test)
        {
            IValidationResult validationResult = _validator.Validate(test);
            if (!validationResult.IsValid)
                throw new DomainValidationException("The test could not be saved due to errors", validationResult);

            try
            {
                Database.Save(test);
                Database.Flush();
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error, e.Message);
                throw new DomainException("The test couldn't be saved due to an error");
            }
        }

        public void Remove(Test test)
        {
            var originalTest = Database.Load<Test>(test.Id);
            if (originalTest == null)
                throw new DomainException("The test was not found so could not be deleted.");

            try
            {
                Database.Delete(test);
                Database.Flush();
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error, e.Message);
                throw new DomainException("The test couldn't be deleted due to an error");
            }
        }

        public void Update(Test test)
        {
            IValidationResult validationResult = _validator.Validate(test);
            if (!validationResult.IsValid)
                throw new DomainValidationException("The test could not be updated due to errors", validationResult);

            var originalTest = Database.Load<Test>(test.Id);
            if (originalTest == null)
                throw new DomainException("The test was not found so couldn't be updated.");

            try
            {
                Database.Save(test);
                Database.Flush();
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error, e.Message);
                throw new DomainException("The test couldn't be updated due to an error");
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            _validator = null;
        }
    }
}
