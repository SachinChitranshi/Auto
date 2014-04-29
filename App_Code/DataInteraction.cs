/*
 * Created on - 29/04/2014
 * Created by - Sachin Chitranshi
 * Copyright RFID4U.com *
 */
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;


public class DataInteraction
{
    
    public DataInteraction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // DB interaction and hold the details
    public DataSet dsGetRecordSet(string[] arrPassParameters, string strSPName)
    {
        DataSet dsRecordSet;
        string strConnectionString;

        try
        {
            strConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSPName, conn);
            SqlDataAdapter sDa = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;

            //Read all SP parameters
            if (arrPassParameters != null)
            {
                for (int intLoopCounter = 0; intLoopCounter < arrPassParameters.Length; intLoopCounter++)
                {
                    SqlParameter sqlParam = new SqlParameter("@SPPARAM_" + intLoopCounter, SqlDbType.NVarChar);
                    sqlParam.Value = arrPassParameters[intLoopCounter];
                    cmd.Parameters.Add(sqlParam);
                }
            }
            
            dsRecordSet = new DataSet("RecordSet");
            sDa.Fill(dsRecordSet);

            conn.Close();
            sDa.Dispose();
            conn.Dispose();

            return dsRecordSet;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    //DB interaction without return
    public void funWithoutAnyReturn(string[] arrPassParameters, string strSPName)
    {
        string strConnectionString;

        try
        {
            strConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSPName, conn);
            SqlDataAdapter sDa = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;

            if (arrPassParameters != null)
            {
                for (int intLoopCounter = 0; intLoopCounter < arrPassParameters.Length; intLoopCounter++)
                {
                    SqlParameter sqlParam = new SqlParameter("@SPPARAM_" + intLoopCounter, SqlDbType.NVarChar);
                    sqlParam.Value = arrPassParameters[intLoopCounter];
                    cmd.Parameters.Add(sqlParam);
                }
            }

            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
}



