using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class AllTradesDTO
    {
        public AllTradesDTO() 
        { 
            TradeIds = new List<int>();
            TraderUsernames = new List<string>();
        }
        public List<int> TradeIds;

        public List<string> TraderUsernames;
    }
}
