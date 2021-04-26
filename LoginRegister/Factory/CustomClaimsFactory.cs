using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Oblivion.Models.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityByExamples.Factory
{
    public class CustomClaimsFactory : UserClaimsPrincipalFactory<User,IdentityRole>
    {
        public CustomClaimsFactory(UserManager<User> userManager,RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager ,optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user )
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("username", user.UserName));
            //identity.AddClaim(new Claim("lastname", user.LastName));


            return identity;
        }
    }
}
