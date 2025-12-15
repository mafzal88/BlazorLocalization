using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

using WebApp.Components;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),   
                    new CultureInfo("ar-BH")                                       
                };
                options.DefaultRequestCulture = new RequestCulture("ar-BH");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                // Optional: To make cookie provider the first one to be checked
                var cookieProvider = options.RequestCultureProviders
                   .OfType<CookieRequestCultureProvider>()
                   .First();
                options.RequestCultureProviders.Remove(cookieProvider);
                options.RequestCultureProviders.Insert(0, cookieProvider);



            });
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            
            Console.WriteLine("Request localization configured with supported cultures: " +
                string.Join(", ", app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value.SupportedCultures.Select(c => c.Name)));

            Console.WriteLine($"Default culture set to: {CultureInfo.CurrentCulture.Name} And {CultureInfo.CurrentUICulture.Name}" );

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapControllers(); // Ensure controllers are mapped
            app.Run();
        }
    }
}
