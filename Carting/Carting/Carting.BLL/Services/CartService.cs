using AutoMapper;
using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Models;
using CartingService.Carting.DAL.Repositories;

namespace CartingService.Carting.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(
                            ICartRepository cartRepository,
                            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public IEnumerable<CartItemDto> GetCartItems(string cartId)
        {
            var cart = _cartRepository.GetById(cartId);
            var items = cart.Items;
            return items.Select(item => _mapper.Map<CartItemDto>(item));
        }

        public void AddCartItem(string cartId, CartItemDto itemDto)
        {
            var item = _mapper.Map<CartItem>(itemDto);
            _cartRepository.AddItem(cartId, item);
        }

        public bool RemoveCartItem(string cartId, int itemId)
        {
           return _cartRepository.RemoveItem(cartId, itemId);
        }

        public CartDto GetCartById(string id)
        {
            var cart = _cartRepository.GetById(id);
            return _mapper.Map<CartDto>(cart);
        }
    }
}
