using HanLearn.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<ZhuyinCardCatalog>();
builder.Services.AddSingleton<StudyCardCollectionService>();
builder.Services.AddScoped<DialogueDbService>();

await builder.Build().RunAsync();
