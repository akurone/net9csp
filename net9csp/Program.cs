using net9csp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();
app.UseSecurityHeaders(new HeaderPolicyCollection().AddContentSecurityPolicy(ConfigureCSP));

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(net9csp.Client._Imports).Assembly);

app.Run();

static void ConfigureCSP(CspBuilder x)
{
  _ = x.AddObjectSrc().None();
  _ = x.AddBlockAllMixedContent();
  _ = x.AddScriptSrc().Self().WasmUnsafeEval().WithHash256("vlN1CPb7yCrmpLSNM/W73E5scOnXpKRhURuKb5W+3B0=");
}