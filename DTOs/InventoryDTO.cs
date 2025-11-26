using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class InventoryDTO
    {
        public InventoryDTO()
        {
            Items = new List<ItemDTO>();
        }

        public List<ItemDTO> Items;
    }
}
