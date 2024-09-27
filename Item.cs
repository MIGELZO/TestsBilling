namespace kukiluli
{
    internal class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Item(int itemId, string name, decimal price)
        {
            ItemId = itemId;
            Name = name;
            Price = price;
        }
    }
}
