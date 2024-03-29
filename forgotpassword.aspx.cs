﻿/*
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
using UniqueKey;

public partial class forgotpassword : System.Web.UI.Page
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    Authentication objAuthentication = new Authentication();
    DataInteraction objDataInteraction = new DataInteraction();
    WebMsgBox objWebMsgBox = new WebMsgBox();
    clsCrypto objclsCrypto = new clsCrypto();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmailId.Text.Trim() == "" && txtUserId.Text.Trim() == "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please insert userid or emailid";
            }
            else if(!objAuthentication.funIsValidEmailId(txtEmailId.Text.Trim()))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please insert correct emailid";
            }
            else
            {
                arrPassParameters = null;
                arrPassParameters = new string[2];

                arrPassParameters[0] = txtUserId.Text.Trim();
                arrPassParameters[1] = txtEmailId.Text.Trim();

                dsObjDataSet.Clear();
                dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPRecoverPassword");//SP to recover password
                int intRecordCount = Convert.ToInt32(dsObjDataSet.Tables[0].Rows[0]["Total"]);

                if (intRecordCount == 0)
                {
                    objWebMsgBox.Show("Invalid details.");
                }
                else
                {
                    string strUniqueKey=UniqueKey.KeyGenerator.GetUniqueKey(8);
                    objAuthentication.funSendMail(Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["EmailID"]), "Dear " + Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["firstname"]) + "," + System.Environment.NewLine + "Your new password is:" + strUniqueKey);

                    //////////////Send New Password///////////////////////
                    arrPassParameters = null;
                    arrPassParameters = new string[2];

                    arrPassParameters[0] = Convert.ToString(dsObjDataSet.Tables[1].Rows[0]["EmailID"]).Trim();
                    arrPassParameters[1] = objclsCrypto.sha256encrypt(strUniqueKey);

                    dsObjDataSet.Clear();
                    objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPUpdatePassword");//SP to update password
                    ///////////////Update New Password/////////////////////   

                    lblMessage.Visible = true;
                    lblMessage.Text = "Mail sent.";
                }
            }
        }
        catch (Exception ex)
        {
            arrPassParameters = null;
            dsObjDataSet.Clear();
            lblMessage.Visible = true;
            lblMessage.Text = ex.ToString();
        }
        finally
        {
            arrPassParameters = null;
            dsObjDataSet.Clear();
        }
    }
}