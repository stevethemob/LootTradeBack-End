using LootTradeDTOs;
using LootTradeInterfaces;
using LootTradeDomainModels;

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

        public AllTrades GetAllTradeIdsByGameIdAndUserId(int gameId, int userId)
        {
            AllTrades allTrades = new AllTrades();
            AllTradesDTO allTradesDTO = tradeRepository.GetAllTradeIdsByGameIdAndUserId(gameId, userId);
            foreach (int tradeId in allTradesDTO.TradeIds)
            {
                allTrades.TradeIds.Add(tradeId);
            }
            foreach(string traderUsers in allTradesDTO.TraderUsernames)
            {
                allTrades.TraderUsernames.Add(traderUsers);
            }

            return allTrades;
        }

        public Trade GetTradeByTradeId(int tradeId)
        {
            TradeDTO tradeDTO = tradeRepository.GetTradeByTradeId(tradeId);

            Item itemTradeOffer = new Item(tradeDTO.ItemOffer.Id, tradeDTO.ItemOffer.GameId, tradeDTO.ItemOffer.Name, tradeDTO.ItemOffer.Description);

            Trade trade = new Trade(tradeDTO.Id, itemTradeOffer, tradeDTO.TraderUser);

            foreach (ItemDTO itemDTO in tradeDTO.TradeOffers)
            {
                Item item = new Item(itemDTO.Id, itemDTO.GameId, itemDTO.Name, itemDTO.Description);
                trade.TradeOffers.Add(item);
            }

            return trade;
        }

        public bool AcceptTrade(int tradeId, int userId)
        {
            if (!tradeRepository.CheckIfTradeIsBySameUser(tradeId, userId))
            {
                return tradeRepository.AcceptTrade(tradeId);
            }

            return false;
        }

        public List<TradeAdmin> GetAllTradesByGameId(int gameId)
        {
            List<TradeAdminDTO> tradeDTOs = tradeRepository.GetAllTradesByGameId(gameId);

            List<TradeAdmin> trades = new List<TradeAdmin>();

            foreach(TradeAdminDTO tradeDTO in tradeDTOs)
            {
                TradeAdmin trade = new TradeAdmin(tradeDTO.TradeId, tradeDTO.OffererName, tradeDTO.TraderName);
                trades.Add(trade);
            }

            return trades;
        }
    }
}
