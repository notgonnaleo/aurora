using Frontend.Web;
using Frontend.Web.Models.Client;
using Frontend.Web.Repository.Client;
using Frontend.Web.Services.Authentication;
using Frontend.Web.Services.Products;
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
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
