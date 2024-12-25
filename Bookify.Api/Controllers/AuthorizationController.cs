using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Bookify.Api.Controllers
{
    public abstract class AuthorizationController : ApiController
    {
		public Guid? UserId {
			get
			{
                var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                if (principal.Claims.Where(c => c.Type == "userId").ToList().Count == 0)
                    return null;

                var customClaimValue = principal.Claims.Where(c => c.Type == "userId").Single().Value;
                return Guid.Parse(customClaimValue);

            }
        }

	}
}
