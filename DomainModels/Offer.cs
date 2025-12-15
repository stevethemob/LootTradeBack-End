using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class Offer
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public DateTime DateTimeOpen { get; set; }
    }
}
