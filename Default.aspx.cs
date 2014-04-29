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

public partial class _default : System.Web.UI.Page
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    Authentication objAuthentication = new Authentication();
    DataInteraction objDataInteraction = new DataInteraction();
    WebMsgBox objWebMsgBox = new WebMsgBox();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["msg"] == "expired")
        {
            lblMsg.Text = "You have logged out from the application.<br/><br/>";
            lblMsg.Visible = true;
        }
        else
        {
            lblMsg.Visible = false;
        }

        if (Request.QueryString["msg"] == "not_permitted")
        {
            lblMsg.Text = "You are not authorize to view this page.<br/><br/>";
            lblMsg.Visible = true;
        }
        else
        {
            lblMsg.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            userLogin();
        }
        catch (Exception ex)
        {
            arrPassParameters = null;
            dsObjDataSet.Clear();
            lblMsg.Visible = true;
            lblMsg.Text = ex.ToString();
        }
        finally
        {
            arrPassParameters = null;
            dsObjDataSet.Clear();
        }
    }
    public void userLogin()
    {
        int intRecordCount;
        bool boolIsLoginNameBlank, boolIsLPasswordBlank;

        boolIsLoginNameBlank = objAuthentication.funIsNotBlank(txtLogin.Text.Trim());// Check UserId Validation (true/false)
        boolIsLPasswordBlank = objAuthentication.funIsNotBlank(txtPassword.Text.Trim());// Check Password Validation (true/false)

        if (!boolIsLoginNameBlank)
            objWebMsgBox.Show("Plese enter login name.");
        else if (!boolIsLPasswordBlank)
            objWebMsgBox.Show("Plese enter password.");
        else
        {
            arrPassParameters = null;
            arrPassParameters = new string[2];

            arrPassParameters[0] = txtLogin.Text.Trim();
            arrPassParameters[1] = txtPassword.Text.Trim();

            dsObjDataSet.Clear();
            dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPCheckUserAuthentication");//SP to check user authentication
            intRecordCount = Convert.ToInt32(dsObjDataSet.Tables[0].Rows[0]["Total"]);

            if (intRecordCount == 0 || Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["loginpassword"]).Trim() != txtPassword.Text.Trim())
            {
                objWebMsgBox.Show("Invalid credentials.");
            }
            else
            {
                //Create Session to User Authentication and Authorization in entire applications
                Session["LoginUserName"] = txtLogin.Text.Trim();
                Session["CompanyName"] = ConfigurationManager.AppSettings["CompanyName"];// "BMWVehicleService";
                Session["Role"] = Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["roleid"]);
                Session["MobileNo"] = Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["mobileno"]);
                Session["EmailId"] = Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["EmailID"]);
                Session["FirstName"] = Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["FirstName"]);

                if (Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["roleid"]) == "1")
                {
                    Response.Redirect("1.aspx");
                }
                else if (Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["roleid"]) == "2")
                {
                    Response.Redirect("2.aspx");
                }
                else if (Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["roleid"]) == "3")
                {
                    Response.Redirect("3.aspx");
                }
                else if (Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["roleid"]) == "4")
                {
                    Response.Redirect("4.aspx");
                }
            }
        }
    }
    protected void lnkForgotPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("forgotpassword.aspx");
    }
}
