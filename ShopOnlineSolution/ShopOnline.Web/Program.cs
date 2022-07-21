using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopOnline.Web;
using ShopOnline.Web.Services;
using ShopOnline.Web.Services.Contracts;
using ShopOnline.Web.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7220/") });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();

var shoppingOnlineConfig = new ShoppingOnlineConfig();
builder.Configuration.Bind("customConfigs", shoppingOnlineConfig);

builder.Services.AddSingleton<ShoppingOnlineConfig>(shoppingOnlineConfig);

await builder.Build().RunAsync();
