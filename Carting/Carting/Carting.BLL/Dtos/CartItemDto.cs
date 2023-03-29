namespace CartingService.Carting.BLL.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
