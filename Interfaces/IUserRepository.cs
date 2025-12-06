using LootTradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeInterfaces
{
    public interface IUserRepository
    {
        public UserDTO GetUserById(int id);

        public void CreateUser(UserDTO user);

        public int GetUserIdByLogin(UserDTO user);
    }
}
