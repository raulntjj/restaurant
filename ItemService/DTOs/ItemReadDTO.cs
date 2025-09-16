namespace ItemService.DTOs
{
    public class ItemReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int RestaurantId { get; set; }
    }
}