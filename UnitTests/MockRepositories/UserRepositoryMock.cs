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
            UserDTO user = new UserDTO(id, "test", "test", "test", UserDTO.UserRole.User);
            
            return user;    
        }

        public bool CreateUser(string username, string password, string email)
        {
            return true;
        }

        public int GetUserIdByLogin(string username, string password)
        {
            return 1;
        }
    }
}
