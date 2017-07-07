using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using MVCTask.Data.Interface;
using MVCTask.Interface;
using MVCTask.Models;

namespace MVCTask.Validator
{
    public class UserValidator : AbstractValidator<NewEditUserModel>, IUserValidator
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;

            RuleFor(u => u.Name).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Surname).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Email).NotEmpty().EmailAddress().Must((user, email) =>
            {
                var emails = _userRepository.Get().Select(u => u.Email).ToList();
                if (user.Id > 0 && _userRepository.FindById(user.Id).Email.Equals(email))
                    return true;
                return !emails.Contains(email);
            }).WithMessage("This Email already in use.");

            RuleFor(u => u.BirthDate).NotEmpty();
            RuleFor(u => u.CompanyId)
                .Must((user, companyId) =>
                {
                    if (companyId == 0)
                        return true;
                    if (user.Id > 0)
                        if (user.CompanyId == companyId)
                            return true;
                    return _userRepository.Get(u => u.CompanyId == companyId).Count() <
                           _companyRepository.FindById(companyId).MaxCounOfUsers;
                })
                .WithMessage("Max count of users for this company");
        }

        public ValidationResult ValidateUser(NewEditUserModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Validate(user);
        }
    }
}