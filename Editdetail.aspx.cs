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


public partial class Editdetail : System.Web.UI.Page
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    Authentication objAuthentication = new Authentication();
    DataInteraction objDataInteraction = new DataInteraction();
    WebMsgBox objWebMsgBox = new WebMsgBox();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /////////////////////////////////Start Bind User Role///////////////////////////////////////////////////////////////////////
            arrPassParameters = null;
            arrPassParameters = new string[0];
            dsObjDataSet.Clear();
            dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetUsersType");//SP get user type
            objAuthentication.funBindDropDownList(ddlistUsertype, dsObjDataSet,"RoleName","RoleID", "UserType");
            /////////////////////////////////End Bind User Role///////////////////////////////////////////////////////////////////////
            arrPassParameters = null;
            arrPassParameters = new string[2];
            arrPassParameters[0] = Request.QueryString["loginname"].Trim();
            arrPassParameters[1] = Request.QueryString["incrid"].Trim();
            dsObjDataSet.Clear();
            dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetUserWiseDetails");//SP users details
            int rowCount = dsObjDataSet.Tables[0].Rows.Count;
            if (rowCount > 0)
            {
                ddlistUsertype.SelectedValue = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["RoleID"]);//RoleID                
                lblUerID.Text = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["LoginName"]);
                txtmobileno.Text = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["MobileNo"]);
                txtfirstname.Text = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["FirstName"]);
                txtlastname.Text = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["LastName"]);
                txtDOJ.Text = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["DOJ"]);
                txtemail.Text = Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["EmailId"]);
                if (Convert.ToString(dsObjDataSet.Tables[0].Rows[0]["MobileLogin"]) == "1")
                    cbMobileLogin.Checked = true;
                else
                    cbMobileLogin.Checked = false;
            }
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        arrPassParameters = null;
        arrPassParameters = new string[2];
        arrPassParameters[0] = ddlistUsertype.SelectedValue.Trim();
        arrPassParameters[1] = lblUerID.Text;
        arrPassParameters[2] = txtmobileno.Text.Trim();
        arrPassParameters[3] = txtfirstname.Text.Trim();
        arrPassParameters[4] = txtlastname.Text.Trim();
        arrPassParameters[5] = txtDOJ.Text.Trim();
        arrPassParameters[6] = txtemail.Text.Trim();
        if (cbMobileLogin.Checked == true)
            arrPassParameters[7] = "1";
        else
            arrPassParameters[7] = "1";
        arrPassParameters[8] = Request.QueryString["incrid"].Trim();
        objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPEditUserWiseDetails");//SP edit users details

        Response.Redirect("activeusers.aspx?id" + Request.QueryString["loginname"].Trim() + "&flag=1");
    }
}