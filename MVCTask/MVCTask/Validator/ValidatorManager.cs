using FluentValidation;
using FluentValidation.Results;

namespace MVCTask.Validator
{
    public class ValidatorManager
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidatorManager(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public ValidationResult Validate<T>(T entity) where T : class
        {
            var validator = _validatorFactory.GetValidator(entity.GetType());
            var result = validator.Validate(entity);
            return result;
        }
    }
}