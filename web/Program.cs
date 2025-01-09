using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var backendAddress = "http://localhost:8080";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendAddress) });

await builder.Build().RunAsync();
