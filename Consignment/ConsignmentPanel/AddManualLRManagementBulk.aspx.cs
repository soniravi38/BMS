using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Consignment.Model;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Consignment.ConsignmentPanel
{
    public partial class AddManualLRManagementBulk : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();
        #endregion

        #region [pageload]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillBranch();
                lblHeading.Text = "Add Manual LR in Bulk";
                btnSubmit.Text = "Submit";
            }
        }
        #endregion

        #region [all-fuctions]
        private void FillBranch()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Branch";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlBranch.DataSource = dt;
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void clear()
        {
            ddlBranch.SelectedIndex = 0;
        }

        public string CSVtoXML(string FilePath)
        {
            StringBuilder sbXML = new StringBuilder();
            DataTable dt = new DataTable();
            DataSet DS = new DataSet();
            object objreturn = "";
            try
            {
                if (FilePath != "")
                {
                    sbXML.AppendLine();
                    string csvData = File.ReadAllText(FilePath);
                    dt.Columns.AddRange(new DataColumn[1]
                    {
                        new DataColumn("1", typeof(string)),
                    });
                    int Count = 0;
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();
                            int j = 0;
                            int a = 0;
                            if (Count > 0)
                            {
                                a = 0;
                            }
                            else
                            {
                                a = 1;
                            }
                            Regex regx = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            foreach (string cell in regx.Split(row))
                            {
                                if (cell.Trim() == "-")
                                {
                                    dt.Rows[dt.Rows.Count - 1][j] = "0";
                                }
                                else if (cell.Trim() != string.Empty)
                                {
                                    dt.Rows[dt.Rows.Count - 1][j] = cell;
                                }
                                j++;
                            }
                            if (a == 0)
                            {
                                sbXML.AppendLine("<nodes>");
                                sbXML.AppendLine("  <node>");
                                sbXML.AppendLine("<LrNo>" + dt.Rows[Count]["1"].ToString() + "</LrNo>");
                                sbXML.AppendLine("  </node>");
                                sbXML.AppendLine("</nodes>");
                            }
                        }
                        Count++;
                    }
                }
                return sbXML.ToString();
            }
            catch (Exception ex)
            {
                objreturn = ex.Message.ToString();
                throw;
            }
            finally
            {
                //SqlCrsv2process.Close();
            }
            return sbXML.ToString();
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (fuUpload.HasFile)
            {

                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                Int32 intResult = 0;
                string spName = "[Proc_ManualLrManagementBulk]";
                try
                {
                    Random random = new Random();
                    int RndNumber = random.Next(100000, 1000000);
                    string fileName = Path.GetFileName(fuUpload.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fuUpload.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/App_Content/CSV/ManualLR/" + RndNumber.ToString() + ".csv");
                    if (fileExtension == ".csv")
                    {
                        fuUpload.SaveAs(fileLocation);
                        ParameterName.Add("@BranchId"); ParameterValue.Add(ddlBranch.SelectedValue);
                        ParameterName.Add("@ASXml"); ParameterValue.Add(CSVtoXML(fileLocation));
                        ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                        dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                        intResult = cm.SaveActivityLog("Manual LR", "Uploaded", "CSV Format File");
                        if (dt.Rows[0]["Message"].ToString() == "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListManualLRManagement.aspx';", true);
                        }
                        else
                        {
                            string NotInsertedLrNos = dt.Rows[0]["Message"].ToString().Replace("\n", "").Replace("\r", "");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted Successfully but some LR already exists - "+ NotInsertedLrNos + "');window.location='ListManualLRManagement.aspx';", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Only CSV file is allowed');window.location='ListManualLRManagement.aspx';", true);
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }

            }
            else
            {
                Response.Write("<script>alert('Oops! please Select CSV File')</script>");
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        #endregion
    }
}