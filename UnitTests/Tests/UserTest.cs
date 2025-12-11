using LootTradeDomainModels;
using LootTradeDTOs;
using LootTradeServices;
using LootTradeServices.validators;
using UnitTests.MockRepositories;

namespace UnitTests.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void GetUserByIdTest()
        {
            UserRepositoryMock userRepositoryMock = new UserRepositoryMock();
            UserValidator validator = new UserValidator();
            UserService userService = new UserService(userRepositoryMock, validator);

            int userId = 1;

            User user = userService.GetUserById(userId);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CreateUserTestTrue()
        {
            UserRepositoryMock userRepositoryMock = new UserRepositoryMock();
            UserValidator validator = new UserValidator();
            UserService userService = new UserService(userRepositoryMock, validator);

            User user = new User();
            user.Username = "usernameForTesting";
            user.Email = "test@gmail.com";
            user.Password = "Password1";
            string repeatedPassword = "Password1";

            ValidatorResponse response = userService.CreateUser(user, repeatedPassword);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void CreateUserTestFalse()
        {
            UserRepositoryMock userRepositoryMock = new UserRepositoryMock();
            UserValidator validator = new UserValidator();
            UserService userService = new UserService(userRepositoryMock, validator);

            User user = new User();
            user.Username = "user";
            user.Email = "test";
            user.Password = "password1";
            string repeatedPassword = "password1";

            ValidatorResponse response = userService.CreateUser(user, repeatedPassword);

            Assert.IsFalse(response.Success);
        }

        public void GetUserIdByLogin()
        {
            UserRepositoryMock userRepositoryMock = new UserRepositoryMock();
            UserValidator validator = new UserValidator();
            UserService userService = new UserService(userRepositoryMock, validator);

            User user = new User();
            user.Username = "user";
            user.Password = "test";

            int id = userService.GetUserIdByLogin(user);

            Assert.IsNotNull(id);
        }
    }
}
