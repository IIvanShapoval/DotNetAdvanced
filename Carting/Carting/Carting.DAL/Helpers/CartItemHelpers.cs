using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Models;

namespace CartingService.Carting.DAL.Helpers
{
    public static class CartItemHelpers
    {
        public static CartItem GetNewCartItemFromDto(CartItemDto cartItemDto) => new CartItem(cartItemDto.Id,
                                                                                                    cartItemDto.Name,
                                                                                                    cartItemDto.Price,
                                                                                                    cartItemDto.Quantity,
                                                                                                    cartItemDto.CategoryId,
                                                                                                    cartItemDto.ImageUrl,
                                                                                                    cartItemDto.AltText);

        public static CartItem GetNewCartItemFromExistingItem(CartItem cartItem) => new CartItem(cartItem.Id,
                                                                                            cartItem.Name,
                                                                                            cartItem.Price,
                                                                                            cartItem.Quantity,
                                                                                            cartItem.CategoryId,
                                                                                            cartItem.ImageUrl,
                                                                                            cartItem.AltText);
        public static CartItem GetNewCartItemFromExistingItem(CartItem cartItem, int updatedQuantity) => new CartItem(cartItem.Id,
                                                                                    cartItem.Name,
                                                                                    cartItem.Price,
                                                                                    updatedQuantity,
                                                                                    cartItem.CategoryId,
                                                                                    cartItem.ImageUrl,
                                                                                    cartItem.AltText);

    }
}
