using FSRS.Core.Extensions;
using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<ZhuyinSymbolCatalog>();
builder.Services.AddSingleton<VocabularyCatalog>();
builder.Services.AddScoped<StoryCatalog>();
builder.Services.AddTransient<ExampleVocabularySeeder>();
builder.Services.AddTransient<ExampleStorySeeder>();
builder.Services.AddFSRS(options =>
{
    options.DesiredRetention = 0.9;
    options.MaximumInterval = 365;
});
builder.Services.AddScoped<VocabularyReviewStateService>();

var host = builder.Build();
host.Services.GetRequiredService<ExampleVocabularySeeder>().Seed();
host.Services.GetRequiredService<ExampleStorySeeder>().Seed();

await host.RunAsync();
