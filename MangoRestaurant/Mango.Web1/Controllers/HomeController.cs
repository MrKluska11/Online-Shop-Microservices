using Mango.Services.ProdcutAPI.Models.DTO;
using Mango.Web1.Models;
using Mango.Web1.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto model = new();
            var response = await _productService.GetProductById<ResponseDto>(productId);

            if (response != null)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            return Ok(model);
        }

        [HttpPost]
        [ActionName("Details")]
        //Adding product to basket
        public async Task<IActionResult> DetailsPost([FromBody] ProductDto productDto)
        {
            CartDto cartDto = new() {
                CartHeader = new CartHeaderDto 
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDto cartDetails = new CartDetailsDto() {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };

            var response = await _productService.GetProductById<ResponseDto>(productDto.ProductId);

            if(response != null && response.IsSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDtos;

            var addToCartResp = await _cartService.AddToCartAsync<ResponseDto>(cartDto);

            if (addToCartResp != null && addToCartResp.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return NoContent();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}


