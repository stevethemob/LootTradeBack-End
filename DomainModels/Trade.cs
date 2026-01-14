using LootTradeDTOs;
using System.Net.Http.Headers;

namespace LootTradeDomainModels
{
    public class Trade
    {
        public Trade()
        {
            TradeOffers = new List<Item>();
        }

        public Trade(int id, Item itemOffer, string traderUser)
        {
            Id = id;
            ItemOffer = itemOffer;
            TraderUser = traderUser;
            TradeOffers = new List<Item>();
        }

        public int Id { get; set; }
        public Item ItemOffer { get; set; } = null!;

        public List<Item> TradeOffers { get; set; }

        public string TraderUser { get; set; }
    }
}
