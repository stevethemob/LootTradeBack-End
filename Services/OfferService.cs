using LootTradeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
