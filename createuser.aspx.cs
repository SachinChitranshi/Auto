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

public partial class createuser : System.Web.UI.Page
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    string[] arrPassParameters1 = null;
    DataSet dsObjDataSet1 = new DataSet();
    Authentication objAuthentication = new Authentication();
    DataInteraction objDataInteraction = new DataInteraction();
    WebMsgBox objWebMsgBox = new WebMsgBox();
    clsCrypto objclsCrypto = new clsCrypto();
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
                objAuthentication.funBindDropDownList(ddlistUsertype, dsObjDataSet, "RoleName", "RoleID", "UserType");
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
        arrPassParameters1 = null;
        arrPassParameters1 = new string[3];
        arrPassParameters1[0] = txtUserId.Text.Trim();
        arrPassParameters1[1] = txtmobileno.Text.Trim();
        arrPassParameters1[2] = txtemail.Text.Trim();
        dsObjDataSet1.Clear();
        dsObjDataSet1 = objDataInteraction.dsGetRecordSet(arrPassParameters1, "SPCheckDuplicateUsersDetails");//SP check user details
        int cntUserID =Convert.ToInt32(dsObjDataSet1.Tables[0].Rows[0]["LoginName"]);
        int cntMobileNo = Convert.ToInt32(dsObjDataSet1.Tables[1].Rows[0]["MobileNo"]);
        int cntEmailID = Convert.ToInt32(dsObjDataSet1.Tables[2].Rows[0]["EmailID"]);

        if (cntUserID > 0){
            objWebMsgBox.Show("UserId already exists");}
        else if (cntMobileNo > 0){
            objWebMsgBox.Show("Mobile no. already exists");}
        else if (cntEmailID > 0){
            objWebMsgBox.Show("Email id already exists");}
        else if (ddlistUsertype.SelectedIndex == 0){
            objWebMsgBox.Show("Please select user role");}
        else if (txtfirstname.Text.Trim() == ""){
            objWebMsgBox.Show("Please insert user first name");}
        else if (txtUserId.Text.Trim() == ""){
            objWebMsgBox.Show("Please insert userid");}
        else if (txtpassword.Text.Trim() == ""){
            objWebMsgBox.Show("Please insert password");}
        else if (txtemail.Text.Trim() == ""){
            objWebMsgBox.Show("Please insert email id");}
        else if (txtmobileno.Text.Trim() == ""){
            objWebMsgBox.Show("Please insert mobile no.");}
        else if(!objAuthentication.funIsValidPhoneNo(txtmobileno.Text.Trim())){
            objWebMsgBox.Show("Please insert correct mobile no.");}
        else if (!objAuthentication.funIsValidEmailId(txtemail.Text.Trim())){
            objWebMsgBox.Show("Please insert correct email id");}
        else if (!objAuthentication.funIsValidPassword(txtpassword.Text.Trim(),6,20)){
            objWebMsgBox.Show("Please insert password between 6 to 20 characters");}
        else{ 
            try{            
                    int intMobileLogin = 0;

                    arrPassParameters = null;
                    arrPassParameters = new string[9];
                    arrPassParameters[0] = txtfirstname.Text.Trim();
                    arrPassParameters[1] = txtlastname.Text.Trim();
                    arrPassParameters[2] = txtUserId.Text.Trim();
                    arrPassParameters[3] = objclsCrypto.sha256encrypt(txtpassword.Text.Trim());
                    arrPassParameters[4] = txtDOJ.Text.Trim();
                    arrPassParameters[5] = txtemail.Text.Trim();
                    arrPassParameters[6] = txtmobileno.Text.Trim();
                    arrPassParameters[7] = ddlistUsertype.SelectedValue.Trim();

                    if (cbMobileLogin.Checked == true)
                        intMobileLogin = 1;
                    else
                        intMobileLogin = 0;

                    arrPassParameters[8] = intMobileLogin.ToString();
                    objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPInsertNewUser");//SP Insert New User Detail

                    Response.Redirect("newusercnfrm.aspx?uid=" + txtUserId.Text.Trim() + "&flag=0");
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
}