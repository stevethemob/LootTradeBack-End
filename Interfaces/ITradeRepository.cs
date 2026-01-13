using LootTradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeInterfaces
{
    public interface ITradeRepository
    {
        public bool AddTradeOffer(int offerId, List<int> itemIds, int traderId);
        public AllTradesDTO GetAllTradeIdsByGameIdAndUserId(int gameId, int userId);
        public TradeDTO GetTradeByTradeId(int tradeId);
        public bool AcceptTrade(int tradeId);
    }
}
