using AutoMapper;
using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Helpers;
using CartingService.Carting.DAL.Models;
using CartingService.Carting.DAL.Repositories;

namespace CartingService.Carting.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository,
                            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public IEnumerable<CartItemDto> GetCartItems(Guid cartId)
        {
            var cartItems = _cartRepository.GetItemsFromCart(cartId);
            if (cartItems == null || cartItems?.Count == 0)
            {
                return new List<CartItemDto>();
            }
            return cartItems.Select(item => _mapper.Map<CartItemDto>(item));
        }

        public Guid AddCartItem(Guid cartId, CartItemDto cartItemDto)
        {
            var item = CartItemHelpers.GetNewCartItemFromDto(cartItemDto);
            return _cartRepository.AddItemToCart(cartId, item);
        }

        public bool RemoveCartItem(Guid cartId, Guid itemId)
        {
            return _cartRepository.RemoveItemFromCart(cartId, itemId);
        }

        public CartDto GetCartById(Guid id)
        {
            var cart = _cartRepository.GetCartById(id);
            if (cart == null)
            {
                _cartRepository.AddCart(id, new Cart(id));
            }
            return _mapper.Map<CartDto>(cart);
        }
    }
}
