using FluentValidation;
using MVCTask.Interface;
using MVCTask.Models;
using MVCTask.Validator;
using StructureMap.Configuration.DSL;

namespace MVCTask.Registers
{
    public class ValidatorRegister : Registry
    {
        public ValidatorRegister()
        {
            For<IUserValidator>().Use<UserValidator>();
            For<AbstractValidator<UserModel>>().Use<UserValidator>();
        }
    }
}