using LootTradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeServices
{
    public class TradeService
    {
        private readonly ITradeRepository tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }
        public bool AddTradeOffer(int offerId, List<int> itemIds, int traderId)
        {
            if (itemIds.Count == 0)
            {
                return false;
            }

            return tradeRepository.AddTradeOffer(offerId, itemIds, traderId);
        }
    }
}
