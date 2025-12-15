using ClientUI.Components;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents().AddCircuitOptions(options =>
        {
            options.DetailedErrors = builder.Environment.IsDevelopment();
        }
    );

builder.Services.AddLocalization();

var app = builder.Build();

#region CultureDefenition
var enCulture = new CultureInfo("en-US");
enCulture.DateTimeFormat.Calendar = new GregorianCalendar();

var arCulture = new CultureInfo("ar-SA");
arCulture.DateTimeFormat.Calendar = new UmAlQuraCalendar();

var supportedCultures = new[] { enCulture, arCulture };

app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(enCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion



app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    .AddInteractiveServerRenderMode();

app.Run();
