using FluentValidation.Results;
using MVCTask.Models;

namespace MVCTask.Interface
{
    public interface IUserValidator
    {
        ValidationResult ValidateUser(UserModel user);
    }
}