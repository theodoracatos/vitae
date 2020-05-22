using Library.Constants;
using Library.Helper;
using Library.Repository;
using Library.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Model.Poco;
using Model.ViewModels;
using Newtonsoft.Json.Linq;
using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vitae.Code.PageModels
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly IConfiguration configuration;
        protected readonly IHttpClientFactory clientFactory;
        protected readonly IStringLocalizer<SharedResource> localizer;
        protected readonly VitaeContext vitaeContext;
        protected readonly IRequestCultureFeature requestCulture;
        protected readonly Guid curriculumID;
        protected readonly ClaimsIdentity identity;
        protected readonly Repository repository;
        protected readonly HttpContext httpContext;
        protected readonly UserManager<IdentityUser> userManager;

        public bool IsLoggedIn { get { return this.curriculumID != Guid.Empty; } }

        [BindProperty]
        public bool AddToTop { get; set; }

        [BindProperty]
        public bool Collapsed { get; set; }

        [BindProperty]
        public string CurriculumLanguageCode { get; set; }

        public IEnumerable<CurriculumLanguageVM> CurriculumLanguages { get; set; }

        public string BaseUrl
        {
            get
            {
                return $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            }
        }

        public BasePageModel(IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this.localizer = localizer;
            this.vitaeContext = vitaeContext;
            this.requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            this.identity = httpContextAccessor.HttpContext.User.Identities.Single();
            this.curriculumID = CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext);
            this.repository = repository;
            this.httpContext = httpContextAccessor.HttpContext;
            this.userManager = userManager;
        }

        protected PartialViewResult GetPartialViewResult(string viewName, string modelName = "IndexModel")
        {
            // Ajax
            var dataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { { modelName, this } };
            dataDictionary.Model = this;

            PartialViewResult result = new PartialViewResult()
            {
                ViewName = viewName,
                ViewData = dataDictionary,
            };

            return result;
        }

        protected int CorrectDate(int year, int month, int day)
        {
            DateTime tempDate;
            do
            {
                if (!DateTime.TryParse($"{year}-{month}-{day}", out tempDate))
                {
                    --day; // Decrement (wrong day)
                }
            } while (tempDate == DateTime.MinValue);

            return day;
        }

        protected IList<T> CheckOrdering<T>(IList<T> items) where T : BaseVM
        {
            var copyList = new T[items.Count];
            if (AddToTop && items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (i == 0)
                    {
                        copyList[0] = items[items.Count - 1]; // last item to first
                    }
                    else
                    {
                        copyList[i] = items[i - 1];
                    }
                    copyList[i].Order = i;
                }
            }
            else
            {
                copyList = items.ToArray();
            }
            
            return copyList.ToList();
        }

        protected void Delete<T>(IList<T> items, int order) where T : BaseVM
        {
            items.Remove(items.Single(e => e.Order == order));

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Order = i;
            }
        }

        protected void Remove<T>(IList<T> items) where T : BaseVM
        {
            if (items.Count > 0)
            {
                items.RemoveAt(items.Count - 1);
            }
        }

        protected void Remove<T>(IList<T> items, int order) where T : BaseVM
        {
            if (items.Count > 0)
            {
                items.Remove(items.Single(i => i.Order == order));
            }

            // Reorder
            for(int i = 0; i < items.Count; i++)
            {
                items[i].Order = i;
            }
        }

        protected void Up<T>(IList<T> items, int order) where T : BaseVM
        {
            var oldItem = items[order];
            oldItem.Order = order - 1;
            items[order] = items[order - 1];
            items[order].Order = order;
            items[order - 1] = oldItem;
        }

        protected void Down<T>(IList<T> items, int order) where T : BaseVM
        {
            var oldItem = items[order];
            oldItem.Order = order + 1;
            items[order] = items[order + 1];
            items[order].Order = order;
            items[order + 1] = oldItem;
        }

        protected void Collapse<T>(IList<T> items) where T : BaseVM 
        {
            foreach (var item in items)
            {
                item.Collapsed = Collapsed;
            }
        }

        protected void LoadLanguageCode(Curriculum curriculum)
        {
            CurriculumLanguageCode ??= (curriculum.CurriculumLanguages.Any(c => c.IsSelected) ? curriculum.CurriculumLanguages.Single(c => c.IsSelected).Language.LanguageCode : curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode);
        }

        protected async Task SaveLanguageChangeAsync()
        {
            if (ModelState.IsValid)
            {
                await vitaeContext.CurriculumLanguages.Where(cl => cl.CurriculumID == curriculumID).ForEachAsync(c => c.IsSelected = false);
                vitaeContext.CurriculumLanguages.Single(c => c.CurriculumID == curriculumID && c.Language.LanguageCode == CurriculumLanguageCode).IsSelected = true;
                await vitaeContext.SaveChangesAsync();
            }
        }

        protected async Task<bool> CheckCaptcha()
        {
            bool success;
            string recaptchaResponse = this.Request.Form["g-recaptcha-response"];
            var client = clientFactory.CreateClient();
            try
            {
                var parameters = new Dictionary<string, string>
             {
                {"secret", this.configuration["reCAPTCHA:SecretKey"]},
                {"response", recaptchaResponse},
                {"remoteip", this.HttpContext.Connection.RemoteIpAddress.ToString()}
                };

                var response = await client.PostAsync(Globals.GOOGLE_CAPCHA, new FormUrlEncodedContent(parameters));
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();
                dynamic apiJson = JObject.Parse(apiResponse);
                success = apiJson.success;
                if (apiJson.success != true)
                {
                    this.ModelState.AddModelError(string.Empty, "There was an unexpected problem processing this request. Please try again.");
                }
            }
            catch (HttpRequestException)
            {
                // Something went wrong with the API. Let the request through.
                success = true;
            }

            return success;
        }

        protected abstract void FillSelectionViewModel();
    }
}