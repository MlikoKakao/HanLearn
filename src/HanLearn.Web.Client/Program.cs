using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<ZhuyinCardCatalog>();

await builder.Build().RunAsync();
