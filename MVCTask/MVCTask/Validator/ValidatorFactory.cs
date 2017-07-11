using System;
using FluentValidation;
using StructureMap;

namespace MVCTask.Validator
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IContainer _container;

        public ValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public IValidator<T> GetValidator<T>()
        {
            throw new NotImplementedException();
        }

        public IValidator GetValidator(Type type)
        {
            var abstr = typeof(AbstractValidator<>);
            var valType = abstr.MakeGenericType(type);

            var valid = _container.TryGetInstance(valType);
            return valid as IValidator;
        }
    }
}