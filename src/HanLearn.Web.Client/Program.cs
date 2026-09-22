using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<ZhuyinCardCatalog>();
builder.Services.AddSingleton<StudyCardCollectionService>();
builder.Services.AddScoped<StoryCollectionService>();
builder.Services.AddTransient<AutomatedStudyCardAddition>();
builder.Services.AddTransient<AutomatedStoryAddition>();

var host = builder.Build();
host.Services.GetRequiredService<AutomatedStudyCardAddition>().AddExampleCards();
host.Services.GetRequiredService<AutomatedStoryAddition>().AddExampleStories();

await host.RunAsync();
