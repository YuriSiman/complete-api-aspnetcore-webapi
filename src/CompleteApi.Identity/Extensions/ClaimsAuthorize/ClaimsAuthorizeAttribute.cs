using CompleteApi.Identity.Extensions.ClaimsAuthorize;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompleteApi.Identity.Extensions
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
