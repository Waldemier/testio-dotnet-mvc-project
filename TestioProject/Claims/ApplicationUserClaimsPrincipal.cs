namespace TestioProject.Claims
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using TestioProject.DAL.Models;
    
    // NOT USED
    
    public class ApplicationUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> options)
            : base(userManager, options)
        {
            
        }
        
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Banned", user.Baned.ToString()));
            return identity;
        }
    }
}
