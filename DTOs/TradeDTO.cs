using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class TradeDTO
    {
        public TradeDTO(int id, ItemDTO itemOffer, string traderUser)
        {
            Id = id;
            ItemOffer = itemOffer;
            TraderUser = traderUser;
            TradeOffers = new List<ItemDTO>();
        }
        public int Id {  get; set; }
        public ItemDTO ItemOffer { get; set; }

        public List<ItemDTO> TradeOffers { get; set; }

        public string TraderUser { get; set; }
    }
}
