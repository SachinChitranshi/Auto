using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newusercnfrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblUserName.Text = " "+Request.QueryString["uid"];
    }
    protected void lnkMoveBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("createuser.aspx");
    }
}