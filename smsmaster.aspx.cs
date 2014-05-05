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

public partial class smsmaster : System.Web.UI.Page
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
                getAllSMS();

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
    public void getAllSMS()
    {
        //Activated SMS List
        getAllActivatedSMS("1");

        //Deactivated SMS List
        getAllActivatedSMS("0");
    }
    public void getAllActivatedSMS(string strUserStatus)
    {
        arrPassParameters = null;
        arrPassParameters = new string[1];
        arrPassParameters[0] = strUserStatus;
        dsObjDataSet.Clear();
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetAllSMS");//SP get all SMS
        if (strUserStatus == "1")
        {
            gvActiveSMS.DataSource = dsObjDataSet;//Acivated SMS List
            gvActiveSMS.DataBind();
        }
        else
        {
            gvDeactiveSMS.DataSource = dsObjDataSet;//Deactivated SMS List
            gvDeactiveSMS.DataBind();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string[] strarrExcelColumnNameAndPosition = new string[1];
        strarrExcelColumnNameAndPosition[0] = "SMSText_1";

        string[,] strarrExtraColumnAndPosition = null;

        string[,] strarrColumnAndValues = null;

        int[] intarrMandatoryColumns = new int[1];
        intarrMandatoryColumns[0] = 0;

        int[] intarrNumericColumns = new int[0];

        string[,] strarrColumnMappings = new string[1, 2];
        strarrColumnMappings[0, 0] = "SMSText";
        strarrColumnMappings[0, 1] = "SMSText";

        string strDataBaseTableName = "tbl_SMSMaster";

        string str = objAuthentication.funUploadExcelNew(fuSMS, "/Sample Excels/", 1, strarrExcelColumnNameAndPosition, strarrExtraColumnAndPosition, strarrColumnAndValues, intarrMandatoryColumns, intarrNumericColumns, strarrColumnMappings, strDataBaseTableName);
        if (str != "Primary key can not duplicate.")
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = "File imported sucessfully.";
            getAllSMS();
        }
        else
        {
            lblErrMsg.Visible = true;
            lblErrMsg.Text = str;
        }        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        arrPassParameters = null;
        arrPassParameters = new string[1];
        arrPassParameters[0] = txtSMSText.Text.Trim();
        dsObjDataSet.Clear();
        objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPInsertNewSMS");//SP insert new SMS
        txtSMSText.Text = "";
        getAllSMS();
    }
    protected void gvActiveSMS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //check condition for active users
        if (e.CommandName == "Active")
        {
            arrPassParameters = null;
            arrPassParameters = new string[1];
            arrPassParameters[0] = (e.CommandArgument).ToString();
            objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPDeactivateSMS");//SP to deactivate sms

            getAllSMS();
        }
    }
    protected void gvActiveSMS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton l = (LinkButton)e.Row.FindControl("lnkActivated");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Do you really want to deactivate this sms.')");
        }
    }
    protected void gvDeactiveSMS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton l = (LinkButton)e.Row.FindControl("lnkDeActivated");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Do you really want to activate this sms.')");
        }
    }
    protected void gvDeactiveSMS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //check condition for active users
        if (e.CommandName == "Deactive")
        {
            arrPassParameters = null;
            arrPassParameters = new string[1];
            arrPassParameters[0] = (e.CommandArgument).ToString();
            objDataInteraction.funWithoutAnyReturn(arrPassParameters, "SPActivateSMS");//SP to activate sms

            getAllSMS();
        }
    }
    protected void lnkSample_Click(object sender, EventArgs e)
    {
        Response.Redirect("~//Sample Excels//SMSMaster.xls");
    }
}