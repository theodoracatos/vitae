﻿using Library.Constants;
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

using Persistency.Data;

using System;
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

        [Obsolete]
        public BasePageModel() 
        { 
        }

        public BasePageModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
        {
            this.localizer = localizer;
            this.vitaeContext = vitaeContext;
            this.requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            this.identity = httpContextAccessor.HttpContext.User.Identities.Single();
            Guid.TryParse(identity.Claims.Single(c => c.Type == Claims.CV_IDENTIFIER).Value, out curriculumID);
            this.repository = repository;
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

        protected abstract void FillSelectionViewModel();
    }
}