using LootTradeDomainModels;
using LootTradeDTOs;
using LootTradeInterfaces;

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
            offer1.Item = new ItemDTO(1, gameId, "test", "test");
            
            OfferDTO offer2 = new OfferDTO();
            offer2.Id = 2;
            offer2.DateTimeOpen = DateTime.Now;
            offer2.Item = new ItemDTO(2, gameId, "test", "test");
            offerDTOs.Add(offer1);
            offerDTOs.Add(offer2);

            return offerDTOs;
        }

        public List<OfferDTO> GetOffersBySearchAndGameId(string searchQuery, int gameId)
        {
            List<OfferDTO> offerDTOs = new List<OfferDTO>();

            OfferDTO offer1 = new OfferDTO();
            offer1.Id = 1;
            offer1.DateTimeOpen = DateTime.Now;
            offer1.Item = new ItemDTO(1, gameId, "test", "test");

            offerDTOs.Add(offer1);

            return offerDTOs;
        }

        public bool DeleteOfferById(int offerId)
        {
            return true;
        }

        public bool CheckIfOfferIsBySameUser(int userId, int offerId)
        {
            return false;
        }

        public List<OfferDTO> GetAllOffersOfSpecificUserByUserIdAndGameId(int userId, int gameId)
        {
            return new List<OfferDTO>();
        }

        public OfferDTO GetOfferDetailsByOfferId(int offerId)
        {
            return new OfferDTO();
        }

    }
}
