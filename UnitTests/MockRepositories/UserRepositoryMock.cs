using LootTradeInterfaces;
using LootTradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockRepositories
{
    public class UserRepositoryMock : IUserRepository
    {
        public UserDTO GetUserById(int id)
        {
            UserDTO user = new UserDTO();
            user.Id = id;
            user.Username = "test";
            user.role = UserDTO.Role.user;
            user.Email = "test";
            user.Password = "test";
            return user;    
        }

        public bool CreateUser(UserDTO user)
        {
            return true;
        }

        public int GetUserIdByLogin(UserDTO user)
        {
            return 1;
        }
    }
}
