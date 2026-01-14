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
            tradeIds = new List<int>();
            traderUsername = new List<string>();
        }

        private List<int> tradeIds { get; set; }

        public List<int> TradeIds
        {
            get => tradeIds;
            set => tradeIds = value;
        }

        private List<string> traderUsername { get; set; }

        public List<string> TraderUsernames
        {
            get => traderUsername;
            set => traderUsername = value;
        }
    }
}
