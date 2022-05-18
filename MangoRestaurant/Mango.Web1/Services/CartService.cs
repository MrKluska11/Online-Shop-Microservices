using Mango.Services.ProdcutAPI;
using Mango.Web1.Models;
using Mango.Web1.Services.IServices;

namespace Mango.Web1.Services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPI + "api/cart/",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPI + "api/cart/GetCart/" +userId,
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ShoppingCartAPI + "api/cart/RemoveCart/" + cartId,
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPI + "api/cart/UpdateCart",
                AccessToken = token
            });
        }
    }
}
