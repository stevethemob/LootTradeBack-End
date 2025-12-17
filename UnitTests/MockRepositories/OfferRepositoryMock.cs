using LootTradeInterfaces;
using LootTradeDTOs;

namespace UnitTests.MockRepositories
{
    public class OfferRepositoryMock : IOfferRepository
    {
        public bool AddOffer(int inventoryId)
        {
            return true;
        }

        public List<OfferDTO> GetAllOffersByGameId(int gameId)
        {
            List<OfferDTO> offerDTOs = new List<OfferDTO>();

            OfferDTO offer1 = new OfferDTO();
            offer1.Id = 1;
            offer1.DateTimeOpen = DateTime.Now;
            offer1.Item.Id = 1;
            offer1.Item.GameId = gameId;
            offer1.Item.Description = "test";
            offer1.Item.Name = "test";
            
            OfferDTO offer2 = new OfferDTO();
            offer2.Id = 2;
            offer2.DateTimeOpen = DateTime.Now;
            offer2.Item.Id = 2;
            offer2.Item.GameId = gameId;
            offer2.Item.Description = "test";
            offer2.Item.Name = "test";

            offerDTOs.Add(offer1);
            offerDTOs.Add(offer2);

            return offerDTOs;
        }
    }
}
