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
public partial class activeusers : System.Web.UI.Page
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    Authentication objAuthentication = new Authentication();
    DataInteraction objDataInteraction = new DataInteraction();
    WebMsgBox objWebMsgBox = new WebMsgBox();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getAllUsers();

                lblErrMsg.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = ex.ToString();
            arrPassParameters = null;
            dsObjDataSet.Clear();
        }
        finally
        {
            arrPassParameters = null;
            dsObjDataSet.Clear();
        }
    }
    public void getAllUsers()
    {
        //Activated Users
        getAllActivatedUsers("1");

        //Deactivated Users
        getAllActivatedUsers("0");
    }
    public void getAllActivatedUsers(string strUserStatus)
    {
        arrPassParameters = null;
        arrPassParameters = new string[1];
        arrPassParameters[0] = strUserStatus;
        dsObjDataSet.Clear();
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetAllUsers");//SP get all users
        if (strUserStatus == "1") {
            gvActivatedUsers.DataSource = dsObjDataSet;//Acivated Users List
            gvActivatedUsers.DataBind();
        }
        else {
            gvDeActivatedUsers.DataSource = dsObjDataSet;//Deactivated Users List
            gvDeActivatedUsers.DataBind();        
        }
    }
    protected void gvActivatedUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //check condition for active users
        if (e.CommandName == "Active")
        {
            arrPassParameters = null;
            arrPassParameters = new string[1];
            arrPassParameters[0] = (e.CommandArgument).ToString();
            objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPDeactivateUser");//SP to deactivate users

            getAllUsers();
        }
    }
    protected void gvActivatedUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton l = (LinkButton)e.Row.FindControl("lnkActivated");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Do you really want to deactivate this user.')");
        }
    }
    protected void gvDeActivatedUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton l = (LinkButton)e.Row.FindControl("lnkDeActivated");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Do you really want to activate this user.')");
        }
    }
    protected void gvDeActivatedUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //check condition for active users
        if (e.CommandName == "DeActive")
        {
            arrPassParameters = null;
            arrPassParameters = new string[1];
            arrPassParameters[0] = (e.CommandArgument).ToString();
            objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPActivateUser");//SP to activate users

            getAllUsers();
        }
    }
}

