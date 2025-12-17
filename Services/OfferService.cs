using LootTradeDTOs;
using LootTradeInterfaces;
using LootTradeDomainModels;

namespace LootTradeServices
{
    public class OfferService
    {
        private readonly IOfferRepository offerRepository;
        private readonly IInventoryRepository inventoryRepository;

        public OfferService(IOfferRepository offerRepository, IInventoryRepository inventoryRepository)
        {
            this.offerRepository = offerRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public bool AddOffer(int userId, int itemId)
        {
            int inventoryId = GetInventoryIdByUserIdAndItemId(userId, itemId);
            return offerRepository.AddOffer(inventoryId);
        }

        public List<Offer> GetAllOffersByGameId(int gameId)
        {
            List<Offer> offers = new List<Offer>();

            List<OfferDTO> offerDTOs = offerRepository.GetAllOffersByGameId(gameId);

            foreach (OfferDTO offerDTO in offerDTOs)
            {
                Offer offer = new Offer();

                offer.Id = offerDTO.Id;
                offer.DateTimeOpen = offerDTO.DateTimeOpen;
                offer.Item.Id = offerDTO.Item.Id;
                offer.Item.Name = offerDTO.Item.Name;
                offer.Item.Description = offerDTO.Item.Description;

                offers.Add(offer);
            }

            return offers;
        }

        private int GetInventoryIdByUserIdAndItemId(int userId, int itemId)
        {
            return inventoryRepository.GetInventoryIdByUserIdAndItemId(userId, itemId);
        }

        public List<Offer> GetOffersBySearch(string searchQuery)
        {
            List<Offer> offers = new List<Offer>();

            List<OfferDTO> offerDTOs = offerRepository.GetOffersBySearch(searchQuery);

            foreach(OfferDTO offerDTO in offerDTOs)
            {
                Offer offer = new Offer();
                offer.Id = offerDTO.Id;
                offer.DateTimeOpen = offerDTO.DateTimeOpen;
                offer.Item.Id = offerDTO.Item.Id;
                offer.Item.Name = offerDTO.Item.Name;
                offer.Item.Description = offerDTO.Item.Description;
                offers.Add(offer);
            }

            return offers;
        }
    }
}
