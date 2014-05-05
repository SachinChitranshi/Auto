/*
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

public partial class newpassword : System.Web.UI.Page
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    Authentication objAuthentication = new Authentication();
    DataInteraction objDataInteraction = new DataInteraction();
    WebMsgBox objWebMsgBox = new WebMsgBox();
    clsCrypto objclsCrypto = new clsCrypto();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["LoginUserName"]) == null || Convert.ToString(Session["LoginUserName"]) == "")
                Response.Redirect("Logout.aspx");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        arrPassParameters = null;
        arrPassParameters = new string[2];
        arrPassParameters[0] = Convert.ToString(Session["LoginUserName"]);
        arrPassParameters[1] = objclsCrypto.sha256encrypt(txtOldPassword.Text.Trim());
        dsObjDataSet.Clear();
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPCheckOldPassword");//SP for check old password
        int rowValue = Convert.ToInt32(dsObjDataSet.Tables[0].Rows[0]["cnt"]);

        if(txtOldPassword.Text.Trim()=="")
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "Please type old password.";
        }
        else if (txtNewPassword.Text.Trim() == "")
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "Please type new password.";
        }
        else if (txtConfirmNewPassword.Text.Trim() == "")
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "Please type confirm new password.";
        }
        else if (txtNewPassword.Text.Trim() != txtConfirmNewPassword.Text.Trim())
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "New password not matched.";
        }
        else if(rowValue==0)
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "Old password not matched.";
        }
        else
        {
            arrPassParameters = null;
            arrPassParameters = new string[2];
            arrPassParameters[0] = Convert.ToString(Session["LoginUserName"]);
            arrPassParameters[1] = objclsCrypto.sha256encrypt(txtNewPassword.Text.Trim());
            objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPAgainUpdatePassword");//SP again update password for change login status

            lblErrMsg.Visible = true;
            lblErrMsg.Text = "Password changed successfully, please logout and login again.";
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("logout.aspx");
    }
}