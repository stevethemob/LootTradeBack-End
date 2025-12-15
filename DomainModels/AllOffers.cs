using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class AllOffers
    {
        public int Id { get; set; }

        public DateTime DateTimeOpen { get; set; }

        public int ItemId { get; set; }

        public string itemName { get; set; }

        public string itemDescription { get; set; }
    }
}
