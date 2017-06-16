using System.Web.Http;
using Thinktecture.IdentityModel.Authorization;

namespace ClaimsBasedAuthorization.Controllers.Api
{
    public class CustomersController : ApiController
    {
        public string Get()
        {
            return "OK";
        }

        public string Get(int id)
        {
            var allowed = ClaimsAuthorization.CheckAccess("Get", "Customer", id.ToString());

            if (allowed)
            {
                return "OK " + id.ToString();
            }

            return "NOK";
        }
    }
}
