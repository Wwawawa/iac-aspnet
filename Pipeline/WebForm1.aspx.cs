using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iac_aspnet
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Thread.CurrentPrincipal;
            user = HttpContext.Current.User;
            user = User;

            //recommended by 4.5
            user = ClaimsPrincipal.Current;
        }
    }
}
