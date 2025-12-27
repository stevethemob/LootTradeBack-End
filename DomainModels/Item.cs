namespace LootTradeDomainModels
{
    public class Item
    {
        public Item(int id, int gameId, string name, string description)
        {
            Id = id;
            GameId = gameId;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
