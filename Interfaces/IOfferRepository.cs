using LootTradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeInterfaces
{
    public interface IOfferRepository
    {
        public bool AddOffer(int inventoryId);

        public List<OfferDTO> GetAllOffersByGameId(int gameId);

        public List<OfferDTO> GetOffersBySearch(string searchQuery);
    }
}
