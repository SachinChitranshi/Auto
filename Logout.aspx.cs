/*
 * Created on - 29/04/2014
 * Created by - Sachin Chitranshi
 * Copyright RFID4U.com *
 */
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        Response.Cache.SetNoStore();
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Request.Cookies.Clear();
        Response.Redirect("Default.aspx");        
    }
}
