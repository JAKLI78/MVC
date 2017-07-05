using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using MVCTask.Data.Interface;
using MVCTask.Interface;
using MVCTask.Models;

namespace MVCTask.Validator
{
    public class UserValidator : AbstractValidator<UserModel>, IUserValidator
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;

            RuleFor(u => u.Name).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Surname).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.BirthDate).NotEmpty();
            RuleFor(u => u.CompanyId)
                .Must((user, companyId) =>
                {
                    if (companyId == 0)
                        return true;
                    return _userRepository.Get(u => u.CompanyId == companyId).Count() <=
                           _companyRepository.FindById(companyId).MaxCounOfUsers;
                })
                .WithMessage("Max count of users for this company");
        }

        public ValidationResult ValidateUser(UserModel user)
        {
            return Validate(user);
        }
    }
}