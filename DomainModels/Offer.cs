using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class Offer
    {
        public Offer()
        {
            Item = new Item();
        }
        public int Id { get; set; }

        public Item Item;

        public DateTime DateTimeOpen { get; set; }
    }
}
