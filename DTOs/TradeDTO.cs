using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class TradeDTO
    {
        public int id {  get; set; }
        public List<int> ItemIds {  get; set; }

        public List<int> UserIds { get; set; }


    }
}
