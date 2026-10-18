using Consignment.Model;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Consignment.ConsignmentPanel
{
    public partial class AddPODBulk : System.Web.UI.Page
    {
        #region Properties
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        connect con = new connect();
        common cm = new common();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHeading.Text = "Add POD in Bulk";
                btnSubmit.Text = "Upload";
            }
        }
        #endregion

        #region CSV TO XML
        public string CSVtoXML(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                var lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++) // skip header
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                        continue;

                    string[] col = lines[i].Split(',');

                    sb.Append("<node>");

                    sb.Append("<LRDate>" + col[0].Trim() + "</LRDate>");
                    sb.Append("<ServiceMonth>" + col[1].Trim() + "</ServiceMonth>");
                    sb.Append("<LRNo>" + col[2].Trim() + "</LRNo>");
                    sb.Append("<ConsignorName>" + col[3].Trim() + "</ConsignorName>");
                    sb.Append("<Origin>" + col[4].Trim() + "</Origin>");
                    sb.Append("<Destination>" + col[5].Trim() + "</Destination>");
                    sb.Append("<VehicleNo>" + col[6].Trim() + "</VehicleNo>");
                    sb.Append("<PackageCount>" + col[7].Trim() + "</PackageCount>");
                    sb.Append("<VehicleOwner>" + col[8].Trim() + "</VehicleOwner>");
                    sb.Append("<VehicleType>" + col[9].Trim() + "</VehicleType>");
                    sb.Append("<ExceptionalFlag>" + col[10].Trim() + "</ExceptionalFlag>");
                    sb.Append("<ConcernedBranch>" + col[11].Trim() + "</ConcernedBranch>");

                    sb.Append("</node>");
                }

                return "<nodes>" + sb.ToString() + "</nodes>";
            }
            catch (Exception ex)
            {
                throw new Exception("CSV Parsing Error: " + ex.Message);
            }
        }
        #endregion

        #region Submit Button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!fuUpload.HasFile)
            {
                Response.Write("<script>alert('Please select CSV file')</script>");
                return;
            }

            try
            {
                string ext = Path.GetExtension(fuUpload.FileName);

                if (ext.ToLower() != ".csv")
                {
                    Response.Write("<script>alert('Only CSV file allowed')</script>");
                    return;
                }

                // Save File
                string filePath = Server.MapPath("~/App_Content/CSV/POD/" + Guid.NewGuid() + ".csv");
                fuUpload.SaveAs(filePath);

                // Convert CSV to XML
                string xml = CSVtoXML(filePath);

                // Call SP
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@ASXml"); ParameterValue.Add(xml);
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));

                dt = con.GetDataTableFromSP("Proc_PODBulk", ParameterName, ParameterValue);

                string msg = dt.Rows[0]["Message"].ToString();

                // Activity Log
                if (Convert.ToInt32(dt.Rows[0]["Status"]) != 1)
                {
                    cm.SaveActivityLog("POD", "Bulk Upload", "CSV Upload");
                }

                // Response
                msg = msg.Replace("\n", "").Replace("\r", "");

                ClientScript.RegisterStartupScript(this.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + msg + "');window.location='ListPOD.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now + " | POD Bulk Upload Error | " + ex.Message);

                Response.Write("<script>alert('Error occurred while uploading')</script>");
            }
        }
        #endregion
    }
}