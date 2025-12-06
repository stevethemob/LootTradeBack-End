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
            User user = new User();
            UserDTO userDTO = userRepository.GetUserById(userId);
            user.Id = userDTO.Id;
            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;

            return user;
        }

        public ValidatorResponse CreateUser(User user, string repeatedPassword)
        {
            var result = userValidator.Validate(user);

            if (!result.IsValid)
            {
                return new ValidatorResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            UserDTO userDTO = new UserDTO();
            userDTO.Username = user.Username;
            userDTO.Password = user.Password;
            userDTO.Email = user.Email;

            userRepository.CreateUser(userDTO);

            return new ValidatorResponse
            {
                Success = true,
                Errors = new List<string>()
            };
        }

        public int GetUserIdByLogin(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Username = user.Username;
            userDTO.Password = user.Password;

            int userId = userRepository.GetUserIdByLogin(userDTO);

            return userId;
        }
    }
}
