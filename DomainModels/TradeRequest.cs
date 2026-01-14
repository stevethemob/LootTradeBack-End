using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class TradeRequest
    {
        public TradeRequest(int offerId) 
        {
            OfferId = offerId;
            ItemIds = new List<int>();
        }
        public int OfferId { get; set; }
        public List<int> ItemIds { get; set; }
    }
}
