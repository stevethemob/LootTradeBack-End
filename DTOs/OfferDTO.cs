using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class OfferDTO
    {
        public int Id { get; set; }

        public ItemDTO Item {  get; set; }

        public DateTime DateTimeOpen { get; set; }
    }
}
