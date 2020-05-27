using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using System.Security.Claims;
using System.Threading.Tasks;

namespace Vitae.Code.Identity
{
    public class CurriculumClaimFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        public CurriculumClaimFactory(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            //if (!identity.Claims.Any(c => c.Type == Claims.CURRICULUM_ID))
            //{
            //    var claim = (await UserManager.GetClaimsAsync(user)).Single(c => c.Type == Claims.CURRICULUM_ID);
            //    identity.AddClaim(claim);
            //}     
                
            return identity;
        }
    }
}
