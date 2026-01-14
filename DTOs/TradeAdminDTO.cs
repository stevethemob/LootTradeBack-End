using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class TradeAdminDTO
    {
        public TradeAdminDTO(int tradeId, string offererName, string traderName)
        {
            TradeId = tradeId;
            OffererName = offererName;
            TraderName = traderName;
        }

        public int TradeId { get; set; }
        public string OffererName { get; set; }
        public string TraderName { get; set; }

    }
}
