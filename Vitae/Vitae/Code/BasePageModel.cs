using Library.Helper;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;

using Model.ViewModels;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Vitae.Code
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly IStringLocalizer<SharedResource> localizer;
        protected readonly VitaeContext vitaeContext;
        protected readonly IRequestCultureFeature requestCulture;
        protected readonly Guid curriculumID;
        protected readonly ClaimsIdentity identity;
        protected readonly Repository repository;
        protected readonly HttpContext httpContext;

        public bool IsLoggedIn { get { return this.curriculumID != Guid.Empty; } }

        [BindProperty]
        public bool AddToTop { get; set; }

        public BasePageModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
        {
            this.localizer = localizer;
            this.vitaeContext = vitaeContext;
            this.requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            this.identity = httpContextAccessor.HttpContext.User.Identities.Single();
            this.curriculumID = CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext);
            this.repository = repository;
            this.httpContext = httpContextAccessor.HttpContext;
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

        protected abstract void FillSelectionViewModel();
    }
}