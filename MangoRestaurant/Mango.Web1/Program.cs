using Mango.Services.ProdcutAPI;
using Mango.Web1.Services;
using Mango.Web1.Services.IServices;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductService, ProductServices>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<ICouponService, CouponService>();

SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
SD.ShoppingCartAPI = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

builder.Services.AddScoped<IProductService, ProductServices>(); 
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddHttpClient(Options.DefaultName, c => {
}).ConfigurePrimaryHttpMessageHandler(() => {
    return new HttpClientHandler {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, certChain, policyErrors) => true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
