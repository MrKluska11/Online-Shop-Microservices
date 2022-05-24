namespace Mango.Services.ProdcutAPI
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPI { get; set; }
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
