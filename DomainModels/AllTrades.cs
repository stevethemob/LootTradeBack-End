using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class AllTrades
    {
        public List<int> TradeIds { get; set; } = new();

        public List<string> TraderUsernames { get; set; } = new();
    }
}
