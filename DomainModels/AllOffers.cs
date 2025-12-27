using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class AllOffers
    {
        public AllOffers(int id, DateTime dateTimeOpen, int itemId, string itemName, string itemDesciption)
        {
            Id = id;
            DateTimeOpen = dateTimeOpen;
            ItemId = itemId;
            ItemName = itemName;
            ItemDescription = itemDesciption;
        }
        public int Id { get; set; }

        public DateTime DateTimeOpen { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }
    }
}
