using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consignment
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Response.Redirect("ConsignmentPanel/Default.aspx", false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}