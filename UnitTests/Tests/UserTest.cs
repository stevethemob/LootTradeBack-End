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

            string username = "usernameForTesting";
            string email = "test@gmail.com";
            string password = "Password1";
            string repeatedPassword = "Password1";

            ValidatorResponse response = userService.CreateUser(username, password, email, repeatedPassword);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void CreateUserTestFalse()
        {
            UserRepositoryMock userRepositoryMock = new UserRepositoryMock();
            UserValidator validator = new UserValidator();
            UserService userService = new UserService(userRepositoryMock, validator);

            string username = "user";
            string email = "test";
            string password = "password1";
            string repeatedPassword = "password1";

            ValidatorResponse response = userService.CreateUser(username, password, email, repeatedPassword);

            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void GetUserIdByLogin()
        {
            UserRepositoryMock userRepositoryMock = new UserRepositoryMock();
            UserValidator validator = new UserValidator();
            UserService userService = new UserService(userRepositoryMock, validator);

            string username = "user";
            string password = "test";

            int id = userService.GetUserIdByLogin(username, password);

            Assert.IsNotNull(id);
        }
    }
}
