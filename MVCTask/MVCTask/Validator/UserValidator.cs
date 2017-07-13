using System;
using System.Configuration;
using System.Data.Entity;
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
        
        private readonly IUserRepository _userRepository;
        private readonly IConfig _configManager;

        public UserValidator(IUserRepository userRepository, IConfig configManager)
        {
            if (userRepository == null)
                throw new ArgumentNullException(nameof(userRepository), $"{nameof(userRepository)} cannot be null.");
            if (configManager == null)
                throw new ArgumentNullException(nameof(configManager), $"{nameof(configManager)} cannot be null.");
            _userRepository = userRepository;            
            _configManager = configManager;

            RuleFor(u => u.Name).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Surname).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Email).NotEmpty().EmailAddress().Must((user,email) => _userRepository.IsEmailInUse(email, user.Id)).WithMessage("This Email already in use.");
            RuleFor(u => u.BirthDate).NotEmpty();
            RuleFor(u => u.CompanyId)
                .Must((user, companyId) =>
                {
                    return ValidateCompanyProperty(user, companyId);
                })
                .WithMessage("Max count of users for this company");
        }

        public ValidationResult ValidateUser(NewEditUserModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Validate(user);
        }

        private bool ValidateCompanyProperty(NewEditUserModel user, int companyId)
        {
            if (companyId == 0)
                return true;
            if (user.Id > 0)
                if (user.CompanyId == companyId)
                    return true;
            return _userRepository.Query().Count(u => u.CompanyId == companyId) <
                   int.Parse(_configManager.GetSittingsValueByKey("MaxCountOfUsersInCompany"));
        }
    }
}