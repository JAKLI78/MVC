using System.Linq;
using FluentValidation.Validators;
using MVCTask.Data.Interface;

namespace MVCTask.Validator
{
    public class CompanyPropertyValidator : PropertyValidator
    {
        public CompanyPropertyValidator() : base("Max count of users for this company")
        {
        }

        public ICompanyRepository _companyRepository { get; set; }
        public IUserRepository _userRepository { get; set; }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var maxCount = _companyRepository.FindById((int) context.PropertyValue).MaxCounOfUsers;
            var currentCount = _userRepository.Get(u => u.CompanyId == (int) context.PropertyValue).Count();
            return currentCount < maxCount;
        }
    }
}