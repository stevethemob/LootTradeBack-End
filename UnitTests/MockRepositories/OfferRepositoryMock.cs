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
            return true;
        }

        public List<OfferDTO> GetAllOffersOfSpecificUserByUserIdAndGameId(int userId, int gameId)
        {
            List<OfferDTO> offers = new List<OfferDTO>();

            OfferDTO offer1 = new OfferDTO();
            ItemDTO item1 = new ItemDTO(1, 1, "test", "test");
            offer1.Item = item1;
            offer1.Id = 1;
            offer1.DateTimeOpen = DateTime.Now;

            OfferDTO offer2 = new OfferDTO();
            ItemDTO item2 = new ItemDTO(2, 1, "test", "test");
            offer2.Item = item2;
            offer2.Id = 2;
            offer2.DateTimeOpen = DateTime.Now;

            OfferDTO offer3 = new OfferDTO();
            ItemDTO item3 = new ItemDTO(3, 1, "test", "test");
            offer3.Item = item3;
            offer3.Id = 3;
            offer3.DateTimeOpen = DateTime.Now;

            offers.Add(offer1);
            offers.Add(offer2);
            offers.Add(offer3);

            return offers;
        }

        public OfferDTO GetOfferDetailsByOfferId(int offerId)
        {
            OfferDTO offer = new OfferDTO();
            ItemDTO item = new ItemDTO(1, 1, "test", "test");
            offer.Item = item;
            offer.Id = 1;
            offer.DateTimeOpen = DateTime.Now;

            return offer;
        }

    }
}
