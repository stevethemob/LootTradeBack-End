using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDomainModels
{
    public class Inventory
    {
        public Inventory()
        {
            Items = new List<Item>();
        }

        public List<Item> Items { get; set; }
    }
}
