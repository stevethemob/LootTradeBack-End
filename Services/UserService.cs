using FluentValidation;
using LootTradeDomainModels;
using LootTradeDTOs;
using LootTradeInterfaces;
using LootTradeServices;

namespace LootTradeServices
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly IValidator<User> userValidator;

        public UserService(IUserRepository userRepository, IValidator<User> userValidator)
        {
            this.userRepository = userRepository;
            this.userValidator = userValidator;
        }

        public User GetUserById(int userId)
        {
            UserDTO userDTO = userRepository.GetUserById(userId);
            User user = new User(userDTO);
            user.Id = userDTO.Id;
            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;

            return user;
        }

        public ValidatorResponse CreateUser(string username, string password, string email, string repeatedPassword)
        {
            User user = new User(username, password, email, User.UserRole.User);
            var result = userValidator.Validate(user);

            if (!result.IsValid)
            {
                return new ValidatorResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            userRepository.CreateUser(username, password, email);

            return new ValidatorResponse
            {
                Success = true,
                Errors = new List<string>()
            };
        }

        public int GetUserIdByLogin(string username, string password)
        {

            int userId = userRepository.GetUserIdByLogin(username, password);

            return userId;
        }
    }
}
