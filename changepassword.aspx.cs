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

public partial class changepassword : System.Web.UI.Page
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
                arrPassParameters = null;
                arrPassParameters = new string[0];
                dsObjDataSet.Clear();
                dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetActiveUsers");//SP to role wise users
                objAuthentication.funBindDropDownList(ddlUserId, dsObjDataSet, "LoginName", "LoginName", "UserName");
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
        finally {
            arrPassParameters = null;
            dsObjDataSet.Clear();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserId.SelectedIndex == 0)
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Please select username first.";
            }
            else if (txtConfirmNewPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Password not matched.";
            }
            else
            {
                arrPassParameters = null;
                arrPassParameters = new string[3];

                arrPassParameters[0] = ddlUserId.SelectedValue.Trim();
                arrPassParameters[1] = txtNewPassword.Text.Trim();
                arrPassParameters[2] = txtConfirmNewPassword.Text.Trim();
                objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPChangeUsersPassword");//SP to change password

                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Password changed successfully.";
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
}