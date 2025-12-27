namespace LootTradeDTOs
{
    public class ItemDTO
    {
        public ItemDTO(int Id, int GameId, string Name, string Description)
        {
            this.Id = Id;
            this.GameId = GameId;
            this.Name = Name;
            this.Description = Description;
        }
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
