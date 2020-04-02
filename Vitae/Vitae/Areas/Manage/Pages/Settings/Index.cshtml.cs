using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using Persistency.Data;

using Vitae.Code;

namespace Vitae.Areas.Manage.Pages.Settings
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_SETTINGS = "_Settings";

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        #endregion

        #region AJAX

        #endregion


        #region HELPER
        protected override void FillSelectionViewModel()
        {

        }
        #endregion
    }
}
