using Library.Attributes;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Globalization;

using Vitae.Code;

namespace Vitae
{
    public class Startup
    {
        private const string DEFAULT_CULTURE = "en";
        private readonly IWebHostEnvironment hostingEnvironment;
        private CultureInfo[] SupportedCultures = new[] { new CultureInfo("de-CH") };
        private CultureInfo[] SupportedUiCultures = new[] { new CultureInfo(DEFAULT_CULTURE), new CultureInfo("de") };

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            hostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VitaeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<CurriculumClaimFactory>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();
            services.AddTransient<Repository, Repository>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            var builder = services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Manage", "/");
                    options.Conventions.AddAreaPageRoute("CV", "/CV/index", "id");
                });

            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }

            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => string.Format(SharedResource.ValueIsInvalidAccessor, x));
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => string.Format(SharedResource.ValueMustBeANumberAccessor, x));
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => string.Format(SharedResource.MissingBindRequiredValueAccessor, x));
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => string.Format(SharedResource.AttemptedValueIsInvalidAccessor, x, y));
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => string.Format(SharedResource.MissingKeyOrValueAccessor));
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => string.Format(SharedResource.UnknownValueIsInvalidAccessor, x));
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => string.Format(SharedResource.ValueMustNotBeNullAccessor, x));
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => string.Format(SharedResource.MissingRequestBodyRequiredValueAccessor));
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => string.Format(SharedResource.NonPropertyAttemptedValueIsInvalidAccessor, x));
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => string.Format(SharedResource.NonPropertyUnknownValueIsInvalidAccessor));
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => string.Format(SharedResource.NonPropertyValueMustBeANumberAccessor));
            });
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
            app.UseCookiePolicy();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(DEFAULT_CULTURE),
                SupportedCultures = SupportedCultures, // Formatting numbers, dates, etc.
                SupportedUICultures = SupportedUiCultures // UI strings that we have localized.
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