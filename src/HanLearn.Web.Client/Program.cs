using FSRS.Core.Extensions;
using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<ZhuyinSymbolCatalog>();
builder.Services.AddSingleton<Lexicon>();
builder.Services.AddScoped<StoryCatalog>();
builder.Services.AddTransient<ExampleLexiconSeeder>();
builder.Services.AddTransient<ExampleStorySeeder>();
builder.Services.AddFSRS(options =>
{
    options.DesiredRetention = 0.9;
    options.MaximumInterval = 365;
});
builder.Services.AddScoped<LexicalSenseReviewStateService>();

var host = builder.Build();
host.Services.GetRequiredService<ExampleLexiconSeeder>().Seed();
host.Services.GetRequiredService<ExampleStorySeeder>().Seed();

await host.RunAsync();
