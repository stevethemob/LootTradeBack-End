using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootTradeDTOs
{
    public class GameDTO
    {
        public GameDTO(int Id, string Title)
        {
            this.Id = Id;
            this.Title = Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
