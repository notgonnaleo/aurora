using Frontend.Web;
using Frontend.Web.Models.Client;
using Frontend.Web.Repository.Authentication;
using Frontend.Web.Repository.Categories;
using Frontend.Web.Repository.Client;
using Frontend.Web.Repository.SubCategories;
using Frontend.Web.Repository.TenantRepository;
using Frontend.Web.Services.Authentication;
using Frontend.Web.Services.Categories;
using Frontend.Web.Services.Products;
using Frontend.Web.Services.SubCategories;
using Frontend.Web.Services.Tenants;
using Frontend.Web.Util.Environments;
using Frontend.Web.Util.Session;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); // this should move to environment handler file
builder.Services.AddScoped<SessionStorageAccessor>();
builder.Services.AddScoped<HttpClientRepository>();
builder.Services.AddScoped<HttpRequestHeader>();
builder.Services.AddScoped<EnvironmentHandler>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthenticationRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<TenantRepository>();
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SubCategoryRepository>();
builder.Services.AddScoped<SubCategoriesService>();

await builder.Build().RunAsync();
