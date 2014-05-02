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
        lblUserID.Text = " " + Request.QueryString["id"];

        if (Request.QueryString["flag"] == "1")
            divEditUser.Visible = true;
        else if (Request.QueryString["flag"] == "0")
            divNewUser.Visible = true;
    }
    protected void lnkMoveBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("createuser.aspx");
    }
    protected void lnkMoveToEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Editdetail.aspx");
    }
}