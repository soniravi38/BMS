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
    public partial class AddVehicleSizeBulk : System.Web.UI.Page
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
                lblHeading.Text = "Add Vehicle Size in Bulk";
                btnSubmit.Text = "Submit";
            }
        }
        #endregion

        #region [all-fuctions]
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
                    dt.Columns.AddRange(new DataColumn[10]
                    {
                        new DataColumn("1", typeof(string)),
                        new DataColumn("2", typeof(string)),
                        new DataColumn("3", typeof(string)),
                        new DataColumn("4", typeof(string)),
                        new DataColumn("5", typeof(string)),
                        new DataColumn("6", typeof(string)),
                        new DataColumn("7", typeof(string)),
                        new DataColumn("8", typeof(string)),
                        new DataColumn("9", typeof(string)),
                        new DataColumn("10", typeof(string)),
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
                                sbXML.AppendLine("<VehicleNo>" + dt.Rows[Count][0].ToString() + "</VehicleNo>");
                                sbXML.AppendLine("<Lenght>" + dt.Rows[Count][1].ToString() + "</Lenght>");
                                sbXML.AppendLine("<Width>" + dt.Rows[Count][2].ToString() + "</Width>");
                                sbXML.AppendLine("<Height>" + dt.Rows[Count][3].ToString() + "</Height>");
                                sbXML.AppendLine("<OwnMarket>" + dt.Rows[Count][4].ToString() + "</OwnMarket>");
                                sbXML.AppendLine("<CFT>" + dt.Rows[Count][5].ToString() + "</CFT>");
                                sbXML.AppendLine("<Code>" + dt.Rows[Count][6].ToString() + "</Code>");
                                sbXML.AppendLine("<LastUpdated>" + dt.Rows[Count][7].ToString() + "</LastUpdated>");
                                sbXML.AppendLine("<Size>" + dt.Rows[Count][8].ToString() + "</Size>");
                                sbXML.AppendLine("<Remark>" + dt.Rows[Count][9].ToString() + "</Remark>");
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
                string spName = "[Proc_VehicleReconciliationBulk]";
                try
                {
                    Random random = new Random();
                    int RndNumber = random.Next(100000, 1000000);
                    string fileName = Path.GetFileName(fuUpload.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fuUpload.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/App_Content/CSV/VehicleSize/" + RndNumber.ToString() + ".csv");
                    if (fileExtension == ".csv")
                    {
                        fuUpload.SaveAs(fileLocation);
                        string xmlStr = CSVtoXML(fileLocation);
                        ParameterName.Add("@ASXml"); ParameterValue.Add(xmlStr);
                        ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                        dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                        intResult = cm.SaveActivityLog("Vehicle Size", "Uploaded", "CSV Format File");
                        if (dt.Rows[0]["Message"].ToString() == "")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListVehicleSize.aspx';", true);
                        }
                        else
                        {
                            string Msg = dt.Rows[0]["Message"].ToString().Replace("\n", "").Replace("\r", "");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');window.location='ListVehicleSize.aspx';", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Only CSV file is allowed');window.location='ListVehicleSize.aspx';", true);
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
        #endregion
    }
}