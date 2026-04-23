using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IstreamBlazor;
using IstreamBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HRM API base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7177/")
});

builder.Services.AddScoped<IEmployeeApiService, EmployeeApiService>();
builder.Services.AddSingleton<AppState>();

await builder.Build().RunAsync();
