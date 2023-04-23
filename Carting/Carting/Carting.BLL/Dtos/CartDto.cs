namespace CartingService.Carting.BLL.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IList<CartItemDto> Items { get; set; }
    }

}
