
using Microsoft.Net.Http.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consignment.Model
{
    public class connect
    {
        ArrayList ParameterName = new ArrayList();
        ArrayList ParameterValue = new ArrayList();
        public SqlConnection con;
        public OleDbConnection excelConnection;
        DataTable table;
        SqlCommand sqlcmd;
        SqlDataReader sqlrd;
        SqlDataAdapter sqlda;
        SqlParameter para;
        string query;
        SqlDataReader rd;
        string str;

        public SqlConnection CreateConnection()
        {
            if (HttpContext.Current.Session["Database_"] != null)
            {
                string Database = HttpContext.Current.Session["Database_"].ToString();
                str = ConfigurationManager.ConnectionStrings[Database].ConnectionString;
                con = new SqlConnection(str);
                return (con);
            }
            else
            {
                HttpContext.Current.Response.Redirect("../ConsignmentPanel/Default.aspx", false);
            }
            return (con);
        }
        public SqlConnection CreateConnectionForTripsheet()
        {

            str = ConfigurationManager.ConnectionStrings["Tripsheet_"].ConnectionString;
            con = new SqlConnection(str);
            return (con);
        }
        public OleDbConnection CreateExcelConnection(string dir)
        {
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dir + ";Extended Properties=Excel 12.0;Persist Security Info=False";
            excelConnection = new OleDbConnection(excelConnectionString);
            return (excelConnection);
        }
        public OleDbConnection OpenExcelConnection()
        {
            try
            {
                if (excelConnection.State == ConnectionState.Closed)
                {
                    excelConnection.Open();
                }
                OleDbConnection.ReleaseObjectPool();
            }
            catch (Exception ex)
            {
                OleDbConnection.ReleaseObjectPool();
            }
            return (excelConnection);
        }
        public SqlConnection OpenConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlConnection.ClearPool(con);
            }
            catch (Exception ex)
            {
                SqlConnection.ClearPool(con);
                CreateConnection();
                OpenConnection();
            }
            return (con);
        }
        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public SqlDataReader GetDatareader(string Query)
        {
            CreateConnection();
            OpenConnection();
            sqlcmd = new SqlCommand(Query, con);
            sqlrd = sqlcmd.ExecuteReader();
            return (sqlrd);
        }
        public int ExecuteQueryByStoreProcedureWithParameter(String ProcedureName, ArrayList ParameterNames, ArrayList ParameterValue)
        {
            int result;
            CreateConnection();
            OpenConnection();
            para = new SqlParameter();
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandTimeout = 1200;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < ParameterNames.Count; i++)
            {

                sqlcmd.Parameters.Add(ParameterNames[i].ToString(), SqlDbType.VarChar, 500000).Value = ParameterValue[i].ToString();
            }
            result = sqlcmd.ExecuteNonQuery();
            CloseConnection();
            return (result);
        }
        public DataTable GetDatatable(string Query)
        {
            CreateConnection();
            OpenConnection();
            table = new DataTable();
            table.Clear();
            sqlda = new SqlDataAdapter(Query, con);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }
        public int executeQuery(string Query)
        {
            int result;
            CreateConnection();
            OpenConnection();
            sqlcmd = new SqlCommand(Query, con);
            result = sqlcmd.ExecuteNonQuery();
            CloseConnection();
            return (result);
        }
        public int ExecuteQueryByStoreProcedure(string ProcedureName)
        {
            int result;
            CreateConnection();
            OpenConnection();
            para = new SqlParameter();
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            result = sqlcmd.ExecuteNonQuery();
            CloseConnection();
            return (result);
        }
        public DataSet GetDataset(string Query, string table)
        {
            DataSet ds = new DataSet();
            CreateConnection();
            OpenConnection();
            sqlda = new SqlDataAdapter(Query, con);
            int i = sqlda.Fill(ds, table);
            return (ds);
        }
        public DataTable GetDataTableFromTable(String query, ArrayList ParameterNames, ArrayList ParameterValue)
        {
            table = new DataTable();
            table.Clear();

            CreateConnection();
            OpenConnection();
            sqlcmd = new SqlCommand(query, con);
            sqlcmd.CommandTimeout = 1200;
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                sqlcmd.Parameters.Add(ParameterNames[i].ToString(), SqlDbType.VarChar, 8000).Value = ParameterValue[i].ToString();
            }
            sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }
        public DataSet GetDataSetFromSP(String ProcedureName, ArrayList ParameterNames, ArrayList ParameterValue)
        {
            DataSet table = new DataSet();
            table.Clear();

            CreateConnection();
            OpenConnection();
            para = new SqlParameter();
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandTimeout = 1200;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                sqlcmd.Parameters.Add(ParameterNames[i].ToString(), SqlDbType.VarChar, 8000).Value = ParameterValue[i].ToString();
            }
            sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }
        public DataSet GetDataSetFromSPWithoutParam(String ProcedureName)
        {
            DataSet table = new DataSet();
            table.Clear();

            CreateConnection();
            OpenConnection();
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandTimeout = 1200;
            sqlcmd.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }
        public DataTable GetDataTableFromSP(String ProcedureName, ArrayList ParameterNames, ArrayList ParameterValue)
        {
            table = new DataTable();
            table.Clear();

            CreateConnection();
            OpenConnection();
            para = new SqlParameter();
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandTimeout = 1200;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                sqlcmd.Parameters.Add(ParameterNames[i].ToString(), SqlDbType.VarChar, 1500000000).Value = ParameterValue[i].ToString();
            }
            sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }

        public DataTable GetDataTableFromSPForTripsheet(String ProcedureName, ArrayList ParameterNames, ArrayList ParameterValue)
        {
            table = new DataTable();
            table.Clear();

            CreateConnectionForTripsheet();
            OpenConnection();
            para = new SqlParameter();
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandTimeout = 1200;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                sqlcmd.Parameters.Add(ParameterNames[i].ToString(), SqlDbType.VarChar, 1500000000).Value = ParameterValue[i].ToString();
            }
            sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }

        #region[Partner Database - Internal LR]
        // NEW : Executes a stored procedure against any company database (web.config connection key).
        public SqlConnection CreateConnectionByKey(string DatabaseKey)
        {
            str = ConfigurationManager.ConnectionStrings[DatabaseKey].ConnectionString;
            con = new SqlConnection(str);
            return (con);
        }
        public DataSet GetDataSetFromSPByDatabase(string DatabaseKey, String ProcedureName, ArrayList
        ParameterNames, ArrayList ParameterValue)
        {
            DataSet table = new DataSet();
            table.Clear();
            CreateConnectionByKey(DatabaseKey);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            sqlcmd = new SqlCommand(ProcedureName, con);
            sqlcmd.CommandTimeout = 1200;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                sqlcmd.Parameters.Add(ParameterNames[i].ToString(), SqlDbType.VarChar, -1).Value = ParameterValue[i].ToString();
            }
            sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(table);
            CloseConnection();
            return (table);
        }
        #endregion
    }
}