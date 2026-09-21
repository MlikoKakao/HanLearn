using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<ZhuyinCardCatalog>();
builder.Services.AddSingleton<StudyCardCollectionService>();
builder.Services.AddScoped<DialogueDbService>();
builder.Services.AddTransient<AutomatedStudyCardAddition>();

var host = builder.Build();
host.Services.GetRequiredService<AutomatedStudyCardAddition>().AddExampleCards();
await host.RunAsync();
