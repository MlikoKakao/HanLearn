using FSRS.Core.Extensions;
using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<ZhuyinSymbolCatalog>();
builder.Services.AddSingleton<Lexicon>();
builder.Services.AddSingleton<StoryCatalog>();
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddSingleton<LessonPackageValidator>();
builder.Services.AddScoped<LessonPackageLoader>();
builder.Services.AddFSRS(options =>
{
    options.DesiredRetention = 0.9;
    options.MaximumInterval = 365;
});
builder.Services.AddScoped<LexicalSenseReviewStateService>();

var host = builder.Build();
await host.Services
    .GetRequiredService<LessonPackageLoader>()
    .LoadIntoCatalogsAsync("content/breakfast-shop-morning.json");

await host.RunAsync();
