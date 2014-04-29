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
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using RKLib.ExportData;
using Ionic.Zip;
using System.Diagnostics;
using System.Data.SqlClient;





/// <summary>
/// Summary description for Authentication
/// </summary>
public class Authentication
{
    string[] arrPassParameters = null;
    DataSet dsObjDataSet = new DataSet();
    DataInteraction objDataInteraction = new DataInteraction();


    public Authentication()
    {
        //
        // TODO: Add constructor logic here
        //
    }

   public string funGetMomryStream(string strQuestionId, string strString)
    {
        string strFlag="0";
        dsObjDataSet.Clear();
        arrPassParameters = null;
        arrPassParameters = new string[2];
        arrPassParameters[0] = strQuestionId;
        arrPassParameters[1] = strString;
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetImage");

        int intRecordCount = dsObjDataSet.Tables[0].Rows.Count;
        if (intRecordCount > 0)
        {
            byte[] bytObjImageData = new byte[0];
            bytObjImageData = (byte[])dsObjDataSet.Tables[0].Rows[0]["ImageData"];
            FileStream fsObjFileStream = new FileStream(HttpContext.Current.Server.MapPath("~\\AnsImages\\" + strQuestionId + "_" + strString + ".jpg"), FileMode.Create, FileAccess.Write);
            fsObjFileStream.Write(bytObjImageData, 0, bytObjImageData.Length);
            fsObjFileStream.Close();
            strFlag = "1";
        }
        return strFlag;

        //return strGUID;
    }

    public string funGetSignatureMemoryStream(string strQuestionId, string strString)
    {
        string strFlag = "0";
        dsObjDataSet.Clear();
        arrPassParameters = null;
        arrPassParameters = new string[2];
        arrPassParameters[0] = strQuestionId;
        arrPassParameters[1] = strString;
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPGetSignature");

        int intRecordCount = dsObjDataSet.Tables[0].Rows.Count;
        if (intRecordCount > 0)
        {
            byte[] bytObjImageData = new byte[0];
            bytObjImageData = (byte[])dsObjDataSet.Tables[0].Rows[0]["ImageData"];
            FileStream fsObjFileStream = new FileStream(HttpContext.Current.Server.MapPath("~\\AnsSignatures\\" + strQuestionId + "_" + strString + ".jpg"), FileMode.Create, FileAccess.Write);
            fsObjFileStream.Write(bytObjImageData, 0, bytObjImageData.Length);
            fsObjFileStream.Close();
            strFlag = "1";
        }
        return strFlag;

        //return strGUID;
    }


    public void funCheckSession()
    {
        HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        HttpContext.Current.Response.Expires = -1500;
        HttpContext.Current.Response.CacheControl = "no-cache";
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        try
        {
            if (HttpContext.Current.Session["User_Role"] == null)
            {
                funSignOut();
                HttpContext.Current.Response.Redirect("Default.aspx?msg=expired");

            }
        }

        catch (Exception)
        {
            funSignOut();
            HttpContext.Current.Response.Redirect("Default.aspx?msg=expired");

        }
    }


    public void funCheckAuthrization()
    {
        try
        {
            if (HttpContext.Current.Session["User_Role"].ToString() != "3")
            {
                funSignOut();
                HttpContext.Current.Response.Redirect("Default.aspx?msg=not_permitted");

            }
        }

        catch (Exception)
        {
            funSignOut();
            HttpContext.Current.Response.Redirect("Default.aspx?msg=not_permitted");

        }
    }

    private void funSignOut()
    {
        FormsAuthentication.SignOut();
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Session.RemoveAll();
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Request.Cookies.Clear();
    }

    public bool funStringLengthValidation(string strString, int intLength)
    {
        try
        {
            if (strString.Length != intLength)
                return false;

            else
                return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


  public void funBindDropDownList(DropDownList ddlTargetedDropDownList, DataSet dsObjDataForFilling, string strTextField, string strValueField, string strSelectName)
    {
        try
        {
            ddlTargetedDropDownList.Items.Clear();
            int intTotalRecords = dsObjDataForFilling.Tables[0].Rows.Count;
            if (intTotalRecords > 0)
            {
                ddlTargetedDropDownList.DataSource = dsObjDataForFilling;
                ddlTargetedDropDownList.DataTextField = strTextField;
                ddlTargetedDropDownList.DataValueField = strValueField;
                ddlTargetedDropDownList.DataBind();
            }

            ddlTargetedDropDownList.Items.Insert(0, new ListItem("-- Select " + strSelectName + " --", "0"));
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void funFillDropDownList(DropDownList ddlTargetedDropDownList, DataSet dsObjDataForFilling)
    {
        try
        {
            int intTotalRecords = dsObjDataForFilling.Tables[0].Rows.Count;
            for (int intLoopCounter = 0; intLoopCounter < intTotalRecords; intLoopCounter++)
            {
                ddlTargetedDropDownList.Items.Insert(intLoopCounter, new ListItem(dsObjDataForFilling.Tables[0].Rows[intLoopCounter][0].ToString(), dsObjDataForFilling.Tables[0].Rows[intLoopCounter][1].ToString()));
            }
            ddlTargetedDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void funFillDropDownList(DropDownList ddlTargetedDropDownList, DataTable dtObjDataForFilling)
    {
        try
        {
            int intTotalRecords = dtObjDataForFilling.Rows.Count;
            for (int intLoopCounter = 0; intLoopCounter < intTotalRecords; intLoopCounter++)
            {
                ddlTargetedDropDownList.Items.Insert(intLoopCounter, new ListItem(dtObjDataForFilling.Rows[intLoopCounter][0].ToString()));
            }
            ddlTargetedDropDownList.Items.Insert(0, "--Select--");
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void funFillGridView(GridView gvTargetedGridView, DataSet dsObjDataForFilling, Label lblTargetedMessage)
    {
        try
        {
            int intTotalRecords = dsObjDataForFilling.Tables[0].Rows.Count;
            if (intTotalRecords > 0)
            {
                gvTargetedGridView.DataSource = dsObjDataForFilling;
                gvTargetedGridView.DataBind();
                gvTargetedGridView.Visible = true;
                lblTargetedMessage.Text = Convert.ToString(intTotalRecords);
            }
            else
            {
                gvTargetedGridView.Visible = false;
                lblTargetedMessage.Text = "No record found.";
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }



    public void funFillGridView(GridView gvTargetedGridView, DataTable dtObjDataForFilling, Label lblTargetedMessage)
    {
        try
        {
            int intTotalRecords = dtObjDataForFilling.Rows.Count;
            if (intTotalRecords > 0)
            {
                gvTargetedGridView.DataSource = dtObjDataForFilling;
                gvTargetedGridView.DataBind();
                gvTargetedGridView.Visible = true;
                lblTargetedMessage.Text = Convert.ToString(intTotalRecords);
            }
            else
            {
                gvTargetedGridView.Visible = false;
                lblTargetedMessage.Text = "No record found.";
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool funHasCharacters(string strMobileNo)
    {
        try
        {
            bool boolContainsLetter = true;
            string strStringToValidate = strMobileNo.Trim();
            for (int intLoopCounter = 0; intLoopCounter < strStringToValidate.Length; intLoopCounter++)
            {
                if (!char.IsNumber(strStringToValidate[intLoopCounter]))
                {
                    boolContainsLetter = false;
                }
            }

            return boolContainsLetter;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool funIsDropDownListSelected(DropDownList ddlTargatedDropDownList)
    {
        try
        {
            if (ddlTargatedDropDownList.SelectedIndex == 0)
                return false;
            else
                return true;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool funMobileNoLengthValidation(int intMobileNoLength)
    {
        try
        {
            if ((intMobileNoLength != 10))
                return false;

            else
                return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int funConvertToInt(string strString)
    {
        try
        {
            return Convert.ToInt32(strString);
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

   

    public string funSendSMS(string strMobileNo, string strSMSMessage)
    {
        string strStatusCode, strStatusDescription, strSMSResponseText, strURL, strEncodedMessage;

        strEncodedMessage = System.Web.HttpUtility.UrlEncode(strSMSMessage);

        if (strMobileNo.Substring(0, 2) == "91")
            strURL = "";
        else
            strURL = "";


        HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
        objHttpWebRequest.Method = "POST";
        objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
        Stream objStream = objHttpWebRequest.GetRequestStream();
        objStream.Close();

        WebResponse objWebResponse = objHttpWebRequest.GetResponse();
        strStatusCode = Convert.ToString(((HttpWebResponse)objWebResponse).StatusCode);
        strStatusDescription = ((HttpWebResponse)objWebResponse).StatusDescription;

        HttpWebResponse objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
        StreamReader objStreamReader = new StreamReader(objHttpWebResponse.GetResponseStream());
        strSMSResponseText = objStreamReader.ReadToEnd();
        return strStatusCode + "@" + strSMSResponseText;
    }

    public void funSendMail(string strEmailId, string strMailMessage)
    {
        MailMessage objMailMessage = new MailMessage();
        objMailMessage.To.Add(strEmailId);
        objMailMessage.From = new MailAddress("sachin@mobiquest.com");
        objMailMessage.Subject = "m'apps - Application";
        objMailMessage.Body = strMailMessage;
        objMailMessage.IsBodyHtml = true;
        SmtpClient objSmtpClient = new SmtpClient();
        objSmtpClient.Host = "abc.abc.com";
        objSmtpClient.Credentials = new System.Net.NetworkCredential("sachin@mobiquest.com", "abcde12345");
        objSmtpClient.Send(objMailMessage);
    }

    

    public void funDownloadFile(string strFileName)
    {
        string strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\" + strFileName);
        FileInfo objFileInfo = new FileInfo(strFilePath);
        if (objFileInfo.Exists)
        {
            HttpContext.Current.Response.ClearContent();
            //If you want to bypass the Open/Save/Cancel dialog you just need to replace 'attachment' by the 'inline'.
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + objFileInfo.Name);
            HttpContext.Current.Response.AddHeader("Content-Length", objFileInfo.Length.ToString());
            HttpContext.Current.Response.ContentType = ReturnExtension(objFileInfo.Extension.ToLower());
            HttpContext.Current.Response.TransmitFile(objFileInfo.FullName);
            HttpContext.Current.Response.End();
        }
    }


    public void funDownloadJADFile(string strMobileNo, string strFeedbackFormId, string strAuthentication, string strAllocation, string strApplicationType, string strScreenSize, string strSection, string strTypes)
    {
        string strFilePath;
        string strCompanyName = ConfigurationManager.AppSettings["CompanyName"].Trim();

        if(strTypes == "0")
            strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\Jad_mapps_sonyerricson_V1.txt");
        else if (strScreenSize == "128 X 160")
            strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\Jad_mapps128-160_V1.txt");
        else if (strScreenSize == "240 X 320")
            strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\Jad_mapps240-320_V1.txt");
        else if (strScreenSize == "320 X 240")
            strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\Jad_mapps320-240_V1.txt");
        else
            strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\Jad_mapps640-340_V1.txt");
        
        FileInfo objFileInfo = new FileInfo(strFilePath);
        string strInput = null;

        if (objFileInfo.Exists)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=mapps.jad");
            HttpContext.Current.Response.ContentType = ReturnExtension(".jad");

            StreamReader srObjStreamReader = File.OpenText(strFilePath);
            strInput = srObjStreamReader.ReadLine();
            while (strInput != null)
            {
                HttpContext.Current.Response.Write(strInput + "\r\n");
                strInput = srObjStreamReader.ReadLine();
            }
            srObjStreamReader.Close();

            HttpContext.Current.Response.Write("User.MobileNo: " + strMobileNo + "\r\n");
            HttpContext.Current.Response.Write("FeedBackId: " + strFeedbackFormId + "\r\n");
            HttpContext.Current.Response.Write("auth: " + strAuthentication + "\r\n");
            HttpContext.Current.Response.Write("alloc: " + strAllocation + "\r\n");
            HttpContext.Current.Response.Write("section: " + strSection + "\r\n");
            HttpContext.Current.Response.Write("User.CompanyId: mapps_" + strCompanyName);            
            HttpContext.Current.Response.End();
        }
        else
        {
            HttpContext.Current.Response.Write("Requested file not found.");
        }
    }


    public void funDownloadJADFileForBlackBerry(string strMobileNo, string strFeedbackFormId, string strAuthentication, string strAllocation, string strApplicationType, string strSection)
    {
        string strFilePath;
        string strCompanyName = ConfigurationManager.AppSettings["CompanyName"].Trim();

        strFilePath = HttpContext.Current.Server.MapPath("~\\JAD\\Jad_mapps_BlackBerry_V1.txt");        

        FileInfo objFileInfo = new FileInfo(strFilePath);
        string strInput = null;

        if (objFileInfo.Exists)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=mapps.jad");
            HttpContext.Current.Response.ContentType = ReturnExtension(".jad");

            StreamReader srObjStreamReader = File.OpenText(strFilePath);
            strInput = srObjStreamReader.ReadLine();
            while (strInput != null)
            {
                HttpContext.Current.Response.Write(strInput + "\r\n");
                strInput = srObjStreamReader.ReadLine();
            }
            srObjStreamReader.Close();

            HttpContext.Current.Response.Write("User.MobileNo: " + strMobileNo + "\r\n");
            HttpContext.Current.Response.Write("FeedBackId: " + strFeedbackFormId + "\r\n");
            HttpContext.Current.Response.Write("auth: " + strAuthentication + "\r\n");
            HttpContext.Current.Response.Write("alloc: " + strAllocation + "\r\n");
            HttpContext.Current.Response.Write("section: " + strSection + "\r\n");
            HttpContext.Current.Response.Write("User.CompanyId: mapps_" + strCompanyName);
            HttpContext.Current.Response.End();
        }
        else
        {
            HttpContext.Current.Response.Write("Requested file not found.");
        }
    }

    private string ReturnExtension(string strFileExtension)
    {
        switch (strFileExtension)
        {
            case ".jad":
                return "text/vnd.sun.j2me.app-descriptor";
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }



    public bool funIsValidUserId(string strUserId, int intMinLength, int intMaxLength)
    {
        Regex objRegex = new Regex(@"^(?=.*[a-zA-Z])[^\*\s]{" + intMinLength + "," + intMaxLength + "}$");
        if (objRegex.IsMatch(strUserId))
            return true;

        else
            return false;
    }

    public bool funIsValidPassword(string strPassword, int intMinLength, int intMaxLength)
    {
        //Regex objRegex = new Regex(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{" + intMinLength + "," + intMaxLength + "})$");
        //if (objRegex.IsMatch(strPassword))
        //    return true;

        //else
        //    return false;
        if ((strPassword.Length >= intMinLength) && (strPassword.Length <= intMaxLength))
            return true;
        else
            return false;
    }

    public bool funIsValidEmailId(string strEmailId)
    {
        Regex objRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        if (objRegex.IsMatch(strEmailId))
            return true;

        else
            return false;
    }

    public bool funIsValidNumeric(string strString)
    {
        Regex objRegex = new Regex(@"^[0-9]{1,18}$");
        if (objRegex.IsMatch(strString))
            return true;

        else
            return false;
    }

    public bool funIsNotBlank(string strString)
    {
        if (strString != "")
            return true;

        else
            return false;
    }

    public bool funIsValidPhoneNo(string strPhoneNo)
    {
        Regex objRegex = new Regex(@"^[+0-9\s,-]{1,200}$");
        if (objRegex.IsMatch(strPhoneNo))
            return true;

        else
            return false;
    }

    public bool funIsValidAddress(string strAddress)
    {
        Regex objRegex = new Regex(@"^[^;>;&;<;%?$@{}*!~'`;:"";+=|]{1,400}$");
        if (objRegex.IsMatch(strAddress))
            return true;

        else
            return false;
    }

    public bool funIsNotDropDownSelect(DropDownList objDropDownList)
    {
        if (objDropDownList.SelectedIndex > 0)
            return true;

        else
            return false;
    }


    public string funGetSessionValue(string strSessionName)
    {
        return Convert.ToString(HttpContext.Current.Session[strSessionName]);
        //return "1";

    }

    public bool funIsUserIdNotAvailable(string strUserId, string strUserIdColumnName, string strTableName)
    {
        arrPassParameters = null;
        arrPassParameters = new string[3];

        arrPassParameters[0] = strUserId;
        arrPassParameters[1] = strUserIdColumnName;
        arrPassParameters[2] = strTableName;

        dsObjDataSet.Clear();
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPCheckUserIdAvailability");
        int intRecordCount = Convert.ToInt32(dsObjDataSet.Tables[0].Rows[0]["Total"]);

        if (intRecordCount == 0)
            return true;

        else
            return false;
    }


    public bool funIsIdNotAvailable(string strUserId, string strUserIdColumnName, string strTableName)
    {
        arrPassParameters = null;
        arrPassParameters = new string[3];

        arrPassParameters[0] = strUserId;
        arrPassParameters[1] = strUserIdColumnName;
        arrPassParameters[2] = strTableName;

        dsObjDataSet.Clear();
        dsObjDataSet = objDataInteraction.dsGetRecordSet(arrPassParameters, "SPCheckUserIdAvailability");
        int intRecordCount = Convert.ToInt32(dsObjDataSet.Tables[0].Rows[0]["Total"]);

        if (intRecordCount == 1)
            return true;

        else
            return false;
    }


    public void funExportToExcelWithOpenDialogue(DataSet dsObjDataSet, int intDataSetTableIndex, string strExcelHeading)
    {
        DataTable dtObjDataTable = dsObjDataSet.Tables[intDataSetTableIndex].Copy();
        HttpContext hcObjHttpContext = HttpContext.Current;
        hcObjHttpContext.Response.Clear();
        hcObjHttpContext.Response.Buffer = true;
        hcObjHttpContext.Response.ContentType = "application/ms-excel";
        hcObjHttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Report_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
        hcObjHttpContext.Response.Write(strExcelHeading);
        hcObjHttpContext.Response.Write(Environment.NewLine);

        StringWriter swObjStringWriter = new StringWriter();
        HtmlTextWriter htwObjHtmlTextWriter = new HtmlTextWriter(swObjStringWriter);
        GridView gvObjGridView = new GridView();
        gvObjGridView.DataSource = dtObjDataTable;
        gvObjGridView.DataBind();
        gvObjGridView.RenderControl(htwObjHtmlTextWriter);
        HttpContext.Current.Response.Write(swObjStringWriter.ToString());
        HttpContext.Current.Response.End();
    }

    public void funExportToExcelWithOpenDialogue(DataTable dtObjDataTable, string strExcelHeading)
    {
        HttpContext hcObjHttpContext = HttpContext.Current;
        hcObjHttpContext.Response.Clear();
        hcObjHttpContext.Response.Buffer = true;
        hcObjHttpContext.Response.ContentType = "application/ms-excel";
        hcObjHttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Report_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
        hcObjHttpContext.Response.Write(strExcelHeading);
        hcObjHttpContext.Response.Write(Environment.NewLine);

        StringWriter swObjStringWriter = new StringWriter();
        HtmlTextWriter htwObjHtmlTextWriter = new HtmlTextWriter(swObjStringWriter);
        GridView gvObjGridView = new GridView();
        gvObjGridView.DataSource = dtObjDataTable;
        gvObjGridView.DataBind();
        gvObjGridView.RenderControl(htwObjHtmlTextWriter);
        HttpContext.Current.Response.Write(swObjStringWriter.ToString());
        HttpContext.Current.Response.End();
    }




    public void funExportToCSVWithOpenDialogue(DataSet dsObjDataSet, int intDataSetTableIndex, string strCSVHeading)
    {

        int intLoopCounter;
        DataTable dtObjDataTable = dsObjDataSet.Tables[intDataSetTableIndex].Copy();

        HttpContext hcObjHttpContext = HttpContext.Current;
        hcObjHttpContext.Response.Clear();
        hcObjHttpContext.Response.ContentType = "text/csv";
        hcObjHttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Report_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
        hcObjHttpContext.Response.Write(strCSVHeading);

        for (intLoopCounter = 0; intLoopCounter < dtObjDataTable.Columns.Count; intLoopCounter++)
        {
            if (intLoopCounter > 0)
            {
                hcObjHttpContext.Response.Write(",");
            }
            hcObjHttpContext.Response.Write(dtObjDataTable.Columns[intLoopCounter].ColumnName);
        }

        hcObjHttpContext.Response.Write(Environment.NewLine);

        foreach (DataRow drObjDataRow in dtObjDataTable.Rows)
        {

            for (intLoopCounter = 0; intLoopCounter < dtObjDataTable.Columns.Count; intLoopCounter++)
            {
                if (intLoopCounter > 0)
                {
                    hcObjHttpContext.Response.Write(",");
                }
                hcObjHttpContext.Response.Write(drObjDataRow.ItemArray[intLoopCounter].ToString());
            }

            hcObjHttpContext.Response.Write(Environment.NewLine);
        }

        hcObjHttpContext.Response.End();
    }


    public void funExportToCSVWithOpenDialogue(DataTable dtObjDataTable, string strCSVHeading)
    {

        int intLoopCounter;
        HttpContext hcObjHttpContext = HttpContext.Current;
        hcObjHttpContext.Response.Clear();
        hcObjHttpContext.Response.ContentType = "text/csv";
        hcObjHttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Report_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
        hcObjHttpContext.Response.Write(strCSVHeading);

        for (intLoopCounter = 0; intLoopCounter < dtObjDataTable.Columns.Count; intLoopCounter++)
        {
            if (intLoopCounter > 0)
            {
                hcObjHttpContext.Response.Write(",");
            }
            hcObjHttpContext.Response.Write(dtObjDataTable.Columns[intLoopCounter].ColumnName);
        }

        hcObjHttpContext.Response.Write(Environment.NewLine);

        foreach (DataRow drObjDataRow in dtObjDataTable.Rows)
        {

            for (intLoopCounter = 0; intLoopCounter < dtObjDataTable.Columns.Count; intLoopCounter++)
            {
                if (intLoopCounter > 0)
                {
                    hcObjHttpContext.Response.Write(",");
                }
                hcObjHttpContext.Response.Write(drObjDataRow.ItemArray[intLoopCounter].ToString());
            }

            hcObjHttpContext.Response.Write(Environment.NewLine);
        }

        hcObjHttpContext.Response.End();
    }


    public void funExportToExcelSaveSilently(DataSet dsObjDataSet, int intDataSetTableIndex, int[] iColumns, string strExcelFileNameWithPathWithoutExtension)
    {
        DataTable dtObjDataTable = dsObjDataSet.Tables[intDataSetTableIndex].Copy();
        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Win");
        objExport.ExportDetails(dtObjDataTable, iColumns, Export.ExportFormat.Excel, strExcelFileNameWithPathWithoutExtension + ".xls");
    }

    public void funExportToExcelSaveSilently(DataTable dtObjDataTable, int[] iColumns, string strExcelFileNameWithPathWithoutExtension)
    {
        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Win");
        objExport.ExportDetails(dtObjDataTable, iColumns, Export.ExportFormat.Excel, strExcelFileNameWithPathWithoutExtension + ".xls");
    }

    public void funExportToCSVSaveSilently(DataSet dsObjDataSet, int intDataSetTableIndex, int[] iColumns, string strExcelFileNameWithPathWithoutExtension)
    {
        DataTable dtObjDataTable = dsObjDataSet.Tables[intDataSetTableIndex].Copy();
        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Win");
        objExport.ExportDetails(dtObjDataTable, iColumns, Export.ExportFormat.CSV, strExcelFileNameWithPathWithoutExtension + ".csv");
    }

    public void funExportToCSVSaveSilently(DataTable dtObjDataTable, int[] iColumns, string strExcelFileNameWithPathWithoutExtension)
    {
        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Win");
        objExport.ExportDetails(dtObjDataTable, iColumns, Export.ExportFormat.CSV, strExcelFileNameWithPathWithoutExtension + ".csv");
    }



    public void funMakeZipFile(string[] arrstrFilesToZip, string strPathAndNameOfZip, string strPassword)
    {
        using (ZipFile zip = new ZipFile(strPathAndNameOfZip + ".zip"))
        {
            zip.Password = strPassword;
            foreach (string strFileToZip in arrstrFilesToZip)
            {
                zip.AddItem(strFileToZip, "");
            }

            zip.Save();
        }
    }


    public void funMakeZipFile(string[] arrstrFilesToZip, string strPathAndNameOfZip)
    {
        using (ZipFile zip = new ZipFile(strPathAndNameOfZip + ".zip"))
        {
            foreach (string strFileToZip in arrstrFilesToZip)
            {
                zip.AddItem(strFileToZip, "");
            }

            zip.Save();
        }
    }


    public string funUploadExcel(FileUpload fuExcelFileUpload, string strExcelPath, int intNoOfExcelColumns, string[] strarrExcelColumnNameAndPosition, string[,] strarrExtraColumnAndPosition, string[,] strarrColumnAndValues, int[] intarrMandatoryColumns, string strDataBaseTableName)
    {
        int intFlagCounterForExcelFormat = 0;
        string strImportFileExtension, strMessage = "";

        if (fuExcelFileUpload.HasFile)
        {
            strImportFileExtension = System.IO.Path.GetExtension(fuExcelFileUpload.FileName);
            if ((strImportFileExtension == ".xls") || (strImportFileExtension == ".xlsx"))
            {
                try
                {
                    string _strExcelAbsolutePath;
                    string strExcelFileName;
                    string _strExcelRelativePathWithName;

                    strExcelFileName = fuExcelFileUpload.PostedFile.FileName;
                    _strExcelAbsolutePath = System.IO.Path.GetFileName(strExcelFileName);
                    fuExcelFileUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~" + strExcelPath + _strExcelAbsolutePath));
                    _strExcelRelativePathWithName = HttpContext.Current.Server.MapPath("~" + strExcelPath + _strExcelAbsolutePath);

                    string strConnectionStringForExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _strExcelRelativePathWithName + ";Extended Properties='Excel 8.0'; ";

                    using (OleDbConnection objOleDBConnection = new OleDbConnection(strConnectionStringForExcel))
                    {
                        objOleDBConnection.Open();

                        DataTable dtExcelColumnsSchema;
                        string[] strarrRestrictionsForExcel = { null, null, "Sheet1$", null };
                        dtExcelColumnsSchema = objOleDBConnection.GetSchema("Columns", strarrRestrictionsForExcel);
                        int intTotalExcelColumns = dtExcelColumnsSchema.Rows.Count;
                        if (intTotalExcelColumns != intNoOfExcelColumns)
                        {
                            if (intTotalExcelColumns == intNoOfExcelColumns + 1)
                            {
                                strMessage = "One column is extra.";
                            }
                            else if (intTotalExcelColumns > intNoOfExcelColumns + 1)
                            {
                                strMessage = (intTotalExcelColumns - intNoOfExcelColumns) + " columns are extra.";
                            }
                            else if (intTotalExcelColumns == intNoOfExcelColumns - 1)
                            {
                                strMessage = "One column is missing.";
                            }
                            else if (intTotalExcelColumns < intNoOfExcelColumns - 1)
                            {
                                strMessage = (intNoOfExcelColumns - intTotalExcelColumns) + " columns are missing.";
                            }

                        }

                        else
                        {
                            for (int intLoopCounter = 0; intLoopCounter < intNoOfExcelColumns; intLoopCounter++)
                            {
                                bool val = funCheckExcelSheetColumnsPosition(dtExcelColumnsSchema.Rows[intLoopCounter][3].ToString(), dtExcelColumnsSchema.Rows[intLoopCounter][6].ToString(), strarrExcelColumnNameAndPosition);
                                if (!val)
                                {
                                    intFlagCounterForExcelFormat = 1;
                                    strMessage = "you have column name " + dtExcelColumnsSchema.Rows[intLoopCounter][3].ToString() + " on postion " + dtExcelColumnsSchema.Rows[intLoopCounter][6].ToString() + " which is wrong";
                                }

                            }

                            if (intFlagCounterForExcelFormat != 1)
                            {
                                OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter("Select * FROM [Sheet1$]", objOleDBConnection);
                                OleDbCommand objOleDBCommand = new OleDbCommand("Select count(*) FROM [Sheet1$]", objOleDBConnection);
                                objOleDBCommand.CommandTimeout = 30;

                                DataSet dsObjForExcelImportToDB = new DataSet();
                                objOleDbDataAdapter.Fill(dsObjForExcelImportToDB);

                                for (int i = 0; i < strarrExtraColumnAndPosition.GetLength(0); i++)
                                {
                                    dsObjForExcelImportToDB.Tables[0].Columns.Add(Convert.ToString(strarrExtraColumnAndPosition[i, 1]));
                                    dsObjForExcelImportToDB.Tables[0].Columns[strarrExtraColumnAndPosition[i, 1]].SetOrdinal(Convert.ToInt32(strarrExtraColumnAndPosition[i, 0]));
                                }

                                foreach (DataRow dr in dsObjForExcelImportToDB.Tables[0].Rows)
                                {
                                    for (int j = 0; j < strarrColumnAndValues.GetLength(0); j++)
                                    {
                                        dr[strarrColumnAndValues[j, 0]] = strarrColumnAndValues[j, 1];
                                    }
                                }

                                int intExcelRowsCount = dsObjForExcelImportToDB.Tables[0].Rows.Count;

                                for (int intLoopCounter = 0; intLoopCounter < intExcelRowsCount; intLoopCounter++)
                                {
                                    foreach (int intColumnPosition in intarrMandatoryColumns)
                                    {
                                        if (dsObjForExcelImportToDB.Tables[0].Rows[intLoopCounter][intColumnPosition].ToString() == "")
                                        {
                                            intFlagCounterForExcelFormat = 1;
                                            strMessage = "Mandatory field can not be left blank, please check row no " + (intLoopCounter + 2);
                                        }

                                    }
                                }

                                if (intFlagCounterForExcelFormat != 1)
                                {
                                    string strSqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                    using (SqlBulkCopy sbcObjSQLbulkCopy = new SqlBulkCopy(strSqlConnectionString))
                                    {
                                        sbcObjSQLbulkCopy.DestinationTableName = strDataBaseTableName;
                                        sbcObjSQLbulkCopy.WriteToServer(dsObjForExcelImportToDB.Tables[0]);
                                        objOleDBConnection.Dispose();
                                        File.Delete(_strExcelRelativePathWithName);
                                        strMessage = "File imported successfully";
                                    }
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~" + strExcelPath + System.IO.Path.GetFileName(fuExcelFileUpload.PostedFile.FileName)));
                    string strErrorResponseMessage = ex.Message;
                    string[] strarrErrMessageRegardingColumn = Regex.Split(strErrorResponseMessage, "column");
                    string[] strarrErrMessageRegardingPK = Regex.Split(strErrorResponseMessage, "constraint");
                    string[] strarrErrMessageRegardingDataType = Regex.Split(strErrorResponseMessage, "cannot be converted");

                    string strErrMessageRegardingColumn = strarrErrMessageRegardingColumn[0];
                    string strErrMessageRegardingPK = strarrErrMessageRegardingPK[0];
                    string strErrMessageRegardingDataType = strarrErrMessageRegardingDataType[0];

                    if (strErrMessageRegardingColumn == "Cannot find ")
                    {
                        strMessage = "Excel sheet columns do not matche";
                    }
                    else if (strErrMessageRegardingPK == "Violation of PRIMARY KEY ")
                    {
                        strMessage = "Ticket no. is already exist";
                    }
                    else if (strErrMessageRegardingDataType == "The given value of type String from the data source")
                    {
                        strMessage = "Please check format of excel sheet";
                    }
                    else
                    {
                        strMessage = "Error in loading report." + strErrorResponseMessage;
                    }
                }
            }
            else
            {
                strMessage = "File format is wrong. Please select a excel file to import";
            }
        }
        else
        {
            strMessage = "Please select a excel file to import";
        }

        return strMessage;
    }


    private bool funCheckExcelSheetColumnsPosition(string strExcelColumnName, string strExcelColumnPosition, string[] strarrExcelColumnNameAndPosition)
    {
        try
        {
            int k = 0;
            for (int i = 0; i < strarrExcelColumnNameAndPosition.Length; i++)
            {
                if (strExcelColumnName + "_" + strExcelColumnPosition == strarrExcelColumnNameAndPosition[i])
                {
                    k = 1;
                }
            }
            if (k == 0)
                return false;
            else
                return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }


    public string[] funSplitString(string strStringToSplit, char[] arrchrDelimiter)
    {
        string[] strarrSplitValues = strStringToSplit.Split(arrchrDelimiter);
        return strarrSplitValues;
    }


    public string funGenerateStringUniqueId()
    {
        long lgVariable = 1;
        foreach (byte bytVariable in Guid.NewGuid().ToByteArray())
        {
            lgVariable *= ((int)bytVariable + lgVariable);
        }
        return string.Format("{0:x}", lgVariable - DateTime.Now.Ticks);
    }


    public long funGenerateLongUniqueId()
    {
        byte[] bytBuffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(bytBuffer, 0);
    }


    public string funGenerateGUID()
    {
        Guid gudObjGuid = Guid.NewGuid();
        return Convert.ToString(gudObjGuid);
    }

    public void funGetSorting(GridView gvObjGridView, DataSet dsObjDataSet, int intDataSetTableIndex, GridViewSortEventArgs e)
    {
        DataTable dtObjDataTable = dsObjDataSet.Tables[intDataSetTableIndex].Copy();

        if (dtObjDataTable != null)
        {
            DataView dvObjDataView = new DataView(dtObjDataTable);
            dvObjDataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            gvObjGridView.DataSource = dvObjDataView;
            gvObjGridView.DataBind();
        }
    }

    public void funGetSorting(GridView gvObjGridView, DataTable dtObjDataTable, GridViewSortEventArgs e)
    {
        if (dtObjDataTable != null)
        {
            DataView dvObjDataView = new DataView(dtObjDataTable);
            dvObjDataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            gvObjGridView.DataSource = dvObjDataView;
            gvObjGridView.DataBind();
        }
    }
    //System.Web.UI.StateBag _viewstate = new StateBag();

    private string GetSortDirection(string strColumn)
    {

        string strSortDirection = "ASC";
        string strSortExpression = HttpContext.Current.Session["SortExpression"] as string;

        if (strSortExpression != null)
        {
            if (strSortExpression == strColumn)
            {
                string strLastDirection = HttpContext.Current.Session["SortDirection"] as string;
                if ((strLastDirection != null) && (strLastDirection == "ASC"))
                {
                    strSortDirection = "DESC";
                }
            }
        }

        HttpContext.Current.Session["SortDirection"] = strSortDirection;
        HttpContext.Current.Session["SortExpression"] = strColumn;

        return strSortDirection;
    }
    public string funUploadExcelNew(FileUpload fuExcelFileUpload, string strExcelPath, int intNoOfExcelColumns, string[] strarrExcelColumnNameAndPosition, string[,] strarrExtraColumnAndPosition, string[,] strarrColumnAndValues, int[] intarrMandatoryColumns, int[] intarrNumericColumns, string[,] strarrColumnMappings, string strDataBaseTableName)
    {
        int intFlagCounterForExcelFormat = 0;
        string strImportFileExtension, strMessage = "";

        if (fuExcelFileUpload.HasFile)
        {
            strImportFileExtension = System.IO.Path.GetExtension(fuExcelFileUpload.FileName);
            if ((strImportFileExtension == ".xls") || (strImportFileExtension == ".xlsx"))
            {
                try
                {
                    string _strExcelAbsolutePath;
                    string strExcelFileName;
                    string _strExcelRelativePathWithName;

                    strExcelFileName = fuExcelFileUpload.PostedFile.FileName;
                    _strExcelAbsolutePath = System.IO.Path.GetFileName(strExcelFileName);
                    fuExcelFileUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~" + strExcelPath + _strExcelAbsolutePath));
                    _strExcelRelativePathWithName = HttpContext.Current.Server.MapPath("~" + strExcelPath + _strExcelAbsolutePath);

                    string strConnectionStringForExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _strExcelRelativePathWithName + ";Extended Properties='Excel 8.0'; ";

                    using (OleDbConnection objOleDBConnection = new OleDbConnection(strConnectionStringForExcel))
                    {
                        objOleDBConnection.Open();

                        DataTable dtExcelColumnsSchema;
                        string[] strarrRestrictionsForExcel = { null, null, "Sheet1$", null };
                        dtExcelColumnsSchema = objOleDBConnection.GetSchema("Columns", strarrRestrictionsForExcel);
                        int intTotalExcelColumns = dtExcelColumnsSchema.Rows.Count;
                        if (intTotalExcelColumns != intNoOfExcelColumns)
                        {
                            if (intTotalExcelColumns == intNoOfExcelColumns + 1)
                            {
                                strMessage = "One column is extra.";
                            }
                            else if (intTotalExcelColumns > intNoOfExcelColumns + 1)
                            {
                                strMessage = (intTotalExcelColumns - intNoOfExcelColumns) + " columns are extra.";
                            }
                            else if (intTotalExcelColumns == intNoOfExcelColumns - 1)
                            {
                                strMessage = "One column is missing.";
                            }
                            else if (intTotalExcelColumns < intNoOfExcelColumns - 1)
                            {
                                strMessage = (intNoOfExcelColumns - intTotalExcelColumns) + " columns are missing.";
                            }

                        }

                        else
                        {
                            for (int intLoopCounter = 0; intLoopCounter < intNoOfExcelColumns; intLoopCounter++)
                            {
                                bool val = funCheckExcelSheetColumnsPosition(dtExcelColumnsSchema.Rows[intLoopCounter][3].ToString(), dtExcelColumnsSchema.Rows[intLoopCounter][6].ToString(), strarrExcelColumnNameAndPosition);
                                if (!val)
                                {
                                    intFlagCounterForExcelFormat = 1;
                                    strMessage = "you have column name " + dtExcelColumnsSchema.Rows[intLoopCounter][3].ToString() + " on postion " + dtExcelColumnsSchema.Rows[intLoopCounter][6].ToString() + " which is wrong";
                                }

                            }

                            if (intFlagCounterForExcelFormat != 1)
                            {
                                OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter("Select * FROM [Sheet1$]", objOleDBConnection);
                                OleDbCommand objOleDBCommand = new OleDbCommand("Select count(*) FROM [Sheet1$]", objOleDBConnection);
                                objOleDBCommand.CommandTimeout = 30;

                                DataSet dsObjForExcelImportToDB = new DataSet();
                                objOleDbDataAdapter.Fill(dsObjForExcelImportToDB);

                                if (strarrExtraColumnAndPosition != null)
                                {
                                    for (int i = 0; i < strarrExtraColumnAndPosition.GetLength(0); i++)
                                    {
                                        dsObjForExcelImportToDB.Tables[0].Columns.Add(Convert.ToString(strarrExtraColumnAndPosition[i, 1]));
                                        dsObjForExcelImportToDB.Tables[0].Columns[strarrExtraColumnAndPosition[i, 1]].SetOrdinal(Convert.ToInt32(strarrExtraColumnAndPosition[i, 0]));
                                    }

                                    foreach (DataRow dr in dsObjForExcelImportToDB.Tables[0].Rows)
                                    {
                                        for (int j = 0; j < strarrColumnAndValues.GetLength(0); j++)
                                        {
                                            dr[strarrColumnAndValues[j, 0]] = strarrColumnAndValues[j, 1];
                                        }
                                    }
                                }

                                int intExcelRowsCount = dsObjForExcelImportToDB.Tables[0].Rows.Count;

                                if (intarrMandatoryColumns != null)
                                {
                                    for (int intLoopCounter = 0; intLoopCounter < intExcelRowsCount; intLoopCounter++)
                                    {
                                        foreach (int intColumnPosition in intarrMandatoryColumns)
                                        {
                                            if (dsObjForExcelImportToDB.Tables[0].Rows[intLoopCounter][intColumnPosition].ToString() == "")
                                            {
                                                intFlagCounterForExcelFormat = 1;
                                                strMessage = "Mandatory field can not be left blank, please check row no " + (intLoopCounter + 2);
                                            }
                                        }
                                    }
                                }

                                if (intarrNumericColumns != null)
                                {
                                    for (int intLoopCounter = 0; intLoopCounter < intExcelRowsCount; intLoopCounter++)
                                    {
                                        foreach (int intColumnPosition in intarrNumericColumns)
                                        {
                                            Regex objRegex = new Regex(@"^\d{1,10}(\.\d{1,5})?$");
                                            if (!objRegex.IsMatch(dsObjForExcelImportToDB.Tables[0].Rows[intLoopCounter][intColumnPosition].ToString()))
                                            {
                                                intFlagCounterForExcelFormat = 1;
                                                strMessage = "Numeric field can not have character, please check row no " + (intLoopCounter + 2);
                                            }

                                        }
                                    }
                                }

                                if (intFlagCounterForExcelFormat != 1)
                                {
                                    string strSqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                    using (SqlBulkCopy sbcObjSQLbulkCopy = new SqlBulkCopy(strSqlConnectionString))
                                    {
                                        sbcObjSQLbulkCopy.DestinationTableName = strDataBaseTableName;
                                        if (strarrColumnMappings != null)
                                        {
                                            for (int intCounter = 0; intCounter < strarrColumnMappings.Length / 2; intCounter++)
                                            {
                                                sbcObjSQLbulkCopy.ColumnMappings.Add(strarrColumnMappings[intCounter, 0], strarrColumnMappings[intCounter, 1]);
                                            }
                                        }
                                        sbcObjSQLbulkCopy.WriteToServer(dsObjForExcelImportToDB.Tables[0]);
                                        objOleDBConnection.Dispose();
                                        File.Delete(_strExcelRelativePathWithName);
                                        strMessage = "File imported successfully.";
                                    }
                                }
                            }
                        }
                    }

                }

                catch (Exception ex)
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~" + strExcelPath + System.IO.Path.GetFileName(fuExcelFileUpload.PostedFile.FileName)));
                    string strErrorResponseMessage = ex.Message;
                    string[] strarrErrMessageRegardingColumn = Regex.Split(strErrorResponseMessage, "column");
                    string[] strarrErrMessageRegardingPK = Regex.Split(strErrorResponseMessage, "constraint");
                    string[] strarrErrMessageRegardingDataType = Regex.Split(strErrorResponseMessage, "cannot be converted");

                    string strErrMessageRegardingColumn = strarrErrMessageRegardingColumn[0];
                    string strErrMessageRegardingPK = strarrErrMessageRegardingPK[0];
                    string strErrMessageRegardingDataType = strarrErrMessageRegardingDataType[0];

                    if (strErrMessageRegardingColumn == "Cannot find ")
                    {
                        strMessage = "Excel sheet columns are not matching.";
                    }
                    else if (strErrMessageRegardingPK == "Violation of PRIMARY KEY ")
                    {
                        strMessage = "Primary key can not duplicate.";
                    }
                    else if (strErrMessageRegardingDataType == "The given value of type String from the data source")
                    {
                        strMessage = "Please check excel sheet format.";
                    }
                    else
                    {
                        strMessage = "Error in loading report." + strErrorResponseMessage;
                    }
                }
            }
            else
            {
                strMessage = "File format is wrong. Please select excel file to import.";
            }
        }
        else
        {
            strMessage = "Please select a excel file to import";
        }

        return strMessage;
    }
    public void funExportToExcelWithOpenDialogue1(DataSet dsObjDataSet, int intDataSetTableIndex, string strExcelHeading)
    {
        DataTable dtObjDataTable = dsObjDataSet.Tables[intDataSetTableIndex].Copy();
        HttpContext hcObjHttpContext = HttpContext.Current;
        hcObjHttpContext.Response.Clear();
        hcObjHttpContext.Response.Buffer = true;
        hcObjHttpContext.Response.ContentType = "application/ms-excel";
        hcObjHttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Report_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
        hcObjHttpContext.Response.Write(strExcelHeading);
        hcObjHttpContext.Response.Write(Environment.NewLine);

        StringWriter swObjStringWriter = new StringWriter();
        HtmlTextWriter htwObjHtmlTextWriter = new HtmlTextWriter(swObjStringWriter);
        GridView gvObjGridView = new GridView();
        gvObjGridView.DataSource = dtObjDataTable;
        gvObjGridView.DataBind();
        gvObjGridView.RenderControl(htwObjHtmlTextWriter);
        HttpContext.Current.Response.Write(swObjStringWriter.ToString());
        HttpContext.Current.Response.End();
    }
    public void funExportToExcelWithOpenDialogue1(DataTable dtObjDataTable, string strExcelHeading)
    {
        HttpContext hcObjHttpContext = HttpContext.Current;
        hcObjHttpContext.Response.Clear();
        hcObjHttpContext.Response.Buffer = true;
        hcObjHttpContext.Response.ContentType = "application/ms-excel";
        hcObjHttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Report_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
        hcObjHttpContext.Response.Write(strExcelHeading);
        hcObjHttpContext.Response.Write(Environment.NewLine);

        StringWriter swObjStringWriter = new StringWriter();
        HtmlTextWriter htwObjHtmlTextWriter = new HtmlTextWriter(swObjStringWriter);
        GridView gvObjGridView = new GridView();
        gvObjGridView.DataSource = dtObjDataTable;
        gvObjGridView.DataBind();
        gvObjGridView.RenderControl(htwObjHtmlTextWriter);
        HttpContext.Current.Response.Write(swObjStringWriter.ToString());
        HttpContext.Current.Response.End();
    }
}







