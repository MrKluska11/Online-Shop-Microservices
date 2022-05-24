using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartFromDb = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartFromDb.CouponCode = couponCode;
            _dbContext.CartHeaders.Update(cartFromDb);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = _dbContext.CartHeaders.FirstOrDefault(u => u.UserId == userId);
            if(cartHeaderFromDb != null)
            {
                //Remove Range ponieważ w 1 koszyku może być wiecej zamowień
                _dbContext.CartDetails.RemoveRange(_dbContext.CartDetails.Where(u => u.CartHeaderId == 
                cartHeaderFromDb.CartHeaderId));
                _dbContext.CartHeaders.Remove(cartHeaderFromDb);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);  

            //check if product exists in database, if not create it
            var prodInDb = _dbContext.Products
                .FirstOrDefault(x => x.ProductId == cartDto.CartDetails.FirstOrDefault().ProductId);

            if(prodInDb == null)
            {
                _dbContext.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _dbContext.SaveChangesAsync();
            }

            //check if header is null
            var cartHeaderFromDb = _dbContext.CartHeaders
                .FirstOrDefault(u => u.UserId == cart.CartHeader.UserId);

            if(cartHeaderFromDb == null)
            {
                //create header and details
                _dbContext.CartDetails.AsNoTracking();   //nie wiadomo czy zadziała!!!
                _dbContext.CartHeaders.Add(cart.CartHeader);
                await _dbContext.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _dbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                //if header is not null
                //check if details has the same product
                var cartDetailsFromDb = _dbContext.CartDetails.AsNoTracking().FirstOrDefault(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if(cartDetailsFromDb == null)
                {
                    //create details
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _dbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    //Update the count or cartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    _dbContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _dbContext.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartDto>(cart);

        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new Cart();
            cart.CartHeader = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cart.CartDetails = _dbContext.CartDetails.Where(u => u.CartHeader.CartHeaderId ==
            cart.CartHeader.CartHeaderId).Include(u => u.Product);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartFromDb = await _dbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartFromDb.CouponCode = "";
            _dbContext.CartHeaders.Update(cartFromDb);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _dbContext.CartDetails
                    .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = _dbContext.CartDetails
                    .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                _dbContext.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _dbContext.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _dbContext.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }


        }
    }
}
