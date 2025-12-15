using LootTradeDTOs;
using LootTradeInterfaces;
using LootTradeDomainModels;

namespace LootTradeServices
{
    public class OfferService
    {
        private readonly IOfferRepository offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            this.offerRepository = offerRepository;
        }

        public bool AddOffer(int inventoryId)
        {
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
    }
}
