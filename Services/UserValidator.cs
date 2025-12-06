    using FluentValidation;
    using LootTradeDomainModels;

    namespace LootTradeServices.validators
    {
        public class UserValidator : AbstractValidator<User>
        {
            public UserValidator()
            {
                RuleFor(user => user.Username)
                    .NotEmpty().WithMessage("Username can't be empty")
                    .MaximumLength(20).WithMessage("Username cant be longer than 20 characters");

                RuleFor(user => user.Password)
                    .NotEmpty().WithMessage("Password needs to not be empty")
                    .MaximumLength(20).WithMessage("Password cant be longer than 20 characters")
                    .MinimumLength(8).WithMessage("Password needs to be longer than 8 characters");

                RuleFor(user => user.Email)
                    .NotEmpty().WithMessage("Email can't be empty") 
                    .EmailAddress().WithMessage("Email is not valid");
            }
        }
    }
