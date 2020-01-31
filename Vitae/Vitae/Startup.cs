using Library.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Persistency.Data;

using System.Collections.Generic;
using System.Globalization;

namespace Vitae
{
    public class Startup
    {
        private const string DEFAULT_CULTURE = "en";
        private readonly IWebHostEnvironment hostingEnvironment;
        private CultureInfo[] SupportedCultures = new[] { new CultureInfo(DEFAULT_CULTURE), new CultureInfo("de") };

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            hostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();
            var builder = services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddAreaPageRoute("CV", "/CV/index", "id");
                });

            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }

            services.AddLocalization();
            services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(DEFAULT_CULTURE);
                options.SupportedCultures = SupportedCultures;
                options.SupportedUICultures = SupportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider> { new AcceptLanguageHeaderRequestCultureProvider() };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Catch Internal Server errors (500)
                app.UseExceptionHandler($"/Error/500");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(DEFAULT_CULTURE),
                SupportedCultures = SupportedCultures, // Formatting numbers, dates, etc.
                SupportedUICultures = SupportedCultures // UI strings that we have localized.
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}