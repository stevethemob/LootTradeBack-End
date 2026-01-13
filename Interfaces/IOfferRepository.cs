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

        public List<OfferDTO> GetOffersBySearchAndGameId(string searchQuery, int gameId);

        public bool DeleteOfferById(int offerId);

        public bool CheckIfOfferIsBySameUser(int userId, int offerId);

        public List<OfferDTO> GetAllOffersOfSpecificUserByUserIdAndGameId(int userId, int gameId);
    }
}
