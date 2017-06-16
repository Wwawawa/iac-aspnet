using System.Web.Http;
using Thinktecture.IdentityModel.Authorization;

namespace ClaimsBasedAuthorization.Controllers.Api
{
    public class CustomersController : ApiController
    {
        // http://localhost:1096/api/customers/
        public string Get()
        {
            return "OK";
        }
        // http://localhost:1096/api/customers/1
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
