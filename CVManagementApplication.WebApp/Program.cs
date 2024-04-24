using CVManagementApplication.WebApp;
using CVManagementApplication.WebApp.Interfaces;
using CVManagementApplication.WebApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7291/api/") });

builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IDegreeService, DegreeService>();

await builder.Build().RunAsync();
