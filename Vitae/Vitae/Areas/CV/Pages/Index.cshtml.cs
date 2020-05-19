using Library.Constants;
using Library.Helper;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Model.Enumerations;
using Model.ViewModels;

using Newtonsoft.Json.Linq;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Vitae.Code.PageModels;

namespace Vitae.Areas.CV.Pages
{
    [Area("CV")]
    public class IndexModel : BasePageModel
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;

        [BindProperty]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Password), Prompt = nameof(SharedResource.Password))]
        public string Password { get; set; }

        [BindProperty]
        public CheckVM CheckVM { get; set; }

        public PersonalDetailVM PersonalDetail { get; set; } = new PersonalDetailVM();

        public IList<AboutVM> Abouts { get; set; } = new List<AboutVM>();
        public IList<AwardVM> Awards { get; set; } = new List<AwardVM>();
        public IList<EducationVM> Educations { get; set; } = new List<EducationVM>();
        public IList<ExperienceVM> Experiences { get; set; } = new List<ExperienceVM>();
        public IList<CourseVM> Courses { get; set; } = new List<CourseVM>();
        public IList<AbroadVM> Abroads { get; set; } = new List<AbroadVM>();
        public IList<InterestVM> Interests { get; set; } = new List<InterestVM>();
        public IList<LanguageSkillVM> LanguageSkills { get; set; } = new List<LanguageSkillVM>();
        public IList<SkillVM> Skills { get; set; } = new List<SkillVM>();
        public IList<SocialLinkVM> SocialLinks { get; set; } = new List<SocialLinkVM>();
        public IList<CertificateVM> Certificates { get; set; } = new List<CertificateVM>();
        public IList<ReferenceVM> References { get; set; } = new List<ReferenceVM>();

        public IEnumerable<LanguageVM> Languages { get; set; }
        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<MaritalStatusVM> MaritalStatuses { get; set; }
        public IEnumerable<IndustryVM> Industries { get; set; }
        public IEnumerable<HierarchyLevelVM> HierarchyLevels { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) 
        {
            this.configuration = configuration;
            this.clientFactory = clientFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        #region SYNC

        public async Task<IActionResult> OnGetAsync(Guid? id, string culture)
        {
            CheckVM = LoadCheckModel(id, culture);

            if (CheckVM.HasValidCurriculumID) // Valid
            {
                if(!CheckVM.Challenge)
                {
                    await LoadPageAsync();
                }

                return Page();
            }
            else if(CheckVM.Challenge) // Bot?
            {
                return Page();
            }
            else // No parameter
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id, string culture)
        {
            CheckVM = LoadCheckModel(id, culture);
            var isCaptchaOk = CheckVM.MustCheckCaptcha ? await CheckCaptcha() : true;
            var isPasswordEntered = CheckVM.MustCheckPassword ? ModelState.IsValid : true;

            if (isCaptchaOk && isPasswordEntered)
            {
                if (CheckVM.HasValidCurriculumID)
                {
                    if (!CheckVM.MustCheckPassword || AesHandler.Decrypt(CheckVM.Secret, CheckVM.CurriculumID.ToString()) == Password)
                    {
                        CheckVM.Challenge = false;
                        return await LoadPageAsync();
                    }
                    else
                    {
                        // Wrong password
                        return StatusCode(StatusCodes.Status403Forbidden);
                    }
                }
                else
                {
                    // Someone modified the id or a bot is bruteforcing
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            else
            {
                // Retry
                return Page();
            }
        }

        public IActionResult OnGetOpenFile(Guid identifier)
        {
            var vfile = repository.GetFile(identifier);

            if(vfile != null)
            {
                return File(vfile.Content, vfile.MimeType, vfile.FileName);
            }
            else
            {
                throw new FileNotFoundException(identifier.ToString());
            }
        }

        #endregion

        #region Helper

        private async Task<bool> CheckCaptcha()
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

                HttpResponseMessage response = await client.PostAsync(Globals.GOOGLE_CAPCHA, new FormUrlEncodedContent(parameters));
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

        private async Task<PageResult> LoadPageAsync()
        {
            var curriculum = await repository.GetCurriculumAsync(CheckVM.CurriculumID.Value);

            List<Task> tasks = new List<Task>
            {
                Task.Factory.StartNew(() => PersonalDetail = repository.GetPersonalDetail(curriculum)),
                Task.Factory.StartNew(() => Abouts = repository.GetAbouts(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => SocialLinks = repository.GetSocialLinks(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Educations = repository.GetEducations(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Experiences = repository.GetExperiences(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Courses = repository.GetCourses(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Abroads = repository.GetAbroads(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => LanguageSkills = repository.GetLanguageSkills(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Interests = repository.GetInterests(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Awards = repository.GetAwards(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Skills = repository.GetSkills(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => Certificates = repository.GetCertificates(curriculum, CheckVM.LanguageCode)),
                Task.Factory.StartNew(() => References = repository.GetReferences(curriculum, CheckVM.LanguageCode))
            };

            Task.WaitAll(tasks.ToArray());

            if (CheckVM.Anonymize)
            {
                Anonymize();
            }

            // Log
            if (!CheckVM.IsLoggedIn && CheckVM.PublicationID.Value.ToString() != Globals.DEMO_PUBLICATIONID)
            {
                await repository.LogAsync(curriculum.CurriculumID, CheckVM.PublicationID.Value, LogArea.Access, LogLevel.Information, CodeHelper.GetCalledUri(httpContext), CodeHelper.GetUserAgent(httpContext), requestCulture.RequestCulture.UICulture.Name, httpContext.Connection.RemoteIpAddress.ToString());
            }

            FillSelectionViewModel();

            return Page();
        }

        private void Anonymize()
        {
            PersonalDetail.Firstname = "---";
            PersonalDetail.Lastname = "---";
            PersonalDetail.Nationalities.Clear();
            Abouts.ToList().ForEach(a => a.Photo = string.Empty);
        }

        protected override void FillSelectionViewModel() 
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            MaritalStatuses = repository.GetMaritalStatuses(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Industries = repository.GetIndustries(requestCulture.RequestCulture.UICulture.Name);
            HierarchyLevels = repository.GetHierarchyLevels(requestCulture.RequestCulture.UICulture.Name);
        }

        private CheckVM LoadCheckModel(Guid? id, string lang)
        {
            var checkVM = new CheckVM()
            {
                IsLoggedIn = curriculumID != Guid.Empty
            };

            if (id.HasValue)
            {
                // External or internal?
                var publication = vitaeContext.Publications.Include(p => p.Curriculum).Include(p => p.CurriculumLanguage).SingleOrDefault(p => p.PublicationIdentifier == id);

                if(publication != null)
                {
                    // Found a publication
                    checkVM.CurriculumID = publication.Curriculum.CurriculumID;
                    checkVM.PublicationID = publication.PublicationID;
                    checkVM.Secret = publication.Password;
                    checkVM.Anonymize = publication.Anonymize;
                    checkVM.LanguageCode = publication.CurriculumLanguage.LanguageCode;
                    checkVM.MustCheckCaptcha = publication.Secure;
                    checkVM.Challenge = checkVM.MustCheckCaptcha || checkVM.MustCheckPassword;
                    checkVM.BackgroundColor = publication.Color;
                }
                else
                {
                    // Could be a bot, bruteforcing URL's
                    checkVM.MustCheckCaptcha = true;
                    checkVM.Challenge = true;
                }
            }
            else if(curriculumID != Guid.Empty)
            {
                // Preview...
                checkVM.CurriculumID = curriculumID;
                checkVM.LanguageCode = lang;
            }

            return checkVM;
        }

        #endregion
    }
}