using HanLearn.Web.Components;
using HanLearn.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();



builder.Services.AddSingleton<PublishedLessonCatalog>();

var app = builder.Build();
await app.Services.GetRequiredService<PublishedLessonCatalog>().LoadAsync(
    Path.Combine(app.Environment.ContentRootPath, "Content", "Lessons"));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HanLearn.Web.Client._Imports).Assembly);

app.MapGet("/api/lessons", (PublishedLessonCatalog catalog) =>
    TypedResults.Ok(catalog.Summaries));

app.MapGet("/api/lessons/{id}", (string id, PublishedLessonCatalog catalog) =>
{
    var package = catalog.Find(id);
    return package is null
        ? Results.Problem(statusCode: StatusCodes.Status404NotFound, title: "Lesson not found")
        : Results.Ok(package);
});

app.Run();

// Exposes the host entry point to HTTP integration tests.
public partial class Program { }
