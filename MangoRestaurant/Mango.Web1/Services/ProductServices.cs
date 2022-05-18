using Mango.Services.ProdcutAPI;
using Mango.Services.ProdcutAPI.Models.DTO;
using Mango.Web1.Models;
using Mango.Web1.Services.IServices;

namespace Mango.Web1.Services
{
    public class ProductServices : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductServices(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products/AddCart",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetProductAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductById<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }
    }
}
