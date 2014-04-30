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

public partial class createuser : System.Web.UI.Page
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
                dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetUsersType");//SP get user type
                objAuthentication.funBindDropDownList(ddlistUsertype, dsObjDataSet,"RoleID", "RoleName", "UserType");
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
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
                int intMobileLogin = 0;

                arrPassParameters = null;
                arrPassParameters = new string[9];
                arrPassParameters[7] = ddlistUsertype.SelectedValue.Trim();
                arrPassParameters[2] = txtUserId.Text.Trim();
                arrPassParameters[3] = txtpassword.Text.Trim();
                arrPassParameters[6] = txtmobileno.Text.Trim();
                arrPassParameters[0] = txtfirstname.Text.Trim();
                arrPassParameters[1] = txtlastname.Text.Trim();
                arrPassParameters[6] = txtaddress.Text.Trim();
                arrPassParameters[5] = txtemail.Text.Trim();
                arrPassParameters[4] = txtDOJ.Text.Trim();

                if (cbMobileLogin.Checked == true)
                    intMobileLogin = 1;
                else
                    intMobileLogin = 0;

                arrPassParameters[8] = intMobileLogin.ToString();
                objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPInsertNewUser");//SP Insert New User Detail
                
                Response.Redirect("newusercnfrm.aspx?uid="+txtUserId.Text.Trim());
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