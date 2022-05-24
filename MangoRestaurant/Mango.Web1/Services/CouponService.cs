using Mango.Services.ProdcutAPI;
using Mango.Web1.Models;
using Mango.Web1.Services.IServices;

namespace Mango.Web1.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "api/coupon/" + couponCode,
                AccessToken = token
            });
        }

    }
}
