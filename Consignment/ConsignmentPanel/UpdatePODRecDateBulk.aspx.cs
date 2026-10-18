using Consignment.Model;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;

namespace Consignment.ConsignmentPanel
{
    public partial class UpdatePODRecDateBulk : System.Web.UI.Page
    {
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        connect con = new connect();
        common cm = new common();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string CSVtoXML(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                var lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                        continue;

                    string[] col = lines[i].Split(',');

                    sb.Append("<node>");

                    sb.Append("<LRNo>" + col[0].Trim() + "</LRNo>");
                    sb.Append("<RecDateHO>" + col[1].Trim() + "</RecDateHO>");

                    sb.Append("</node>");
                }

                return "<nodes>" + sb.ToString() + "</nodes>";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuUpload.HasFile)
                {
                    Response.Write("<script>alert('Please upload CSV')</script>");
                    return;
                }

                string ext = Path.GetExtension(fuUpload.FileName);

                if (ext.ToLower() != ".csv")
                {
                    Response.Write("<script>alert('Only CSV allowed')</script>");
                    return;
                }

                string filePath = Server.MapPath("~/App_Content/CSV/POD/" + Guid.NewGuid() + ".csv");

                fuUpload.SaveAs(filePath);

                string xml = CSVtoXML(filePath);

                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@ASXml");
                ParameterValue.Add(xml);

                ParameterName.Add("@UserId");
                ParameterValue.Add(Session["UserId"]);

                dt = con.GetDataTableFromSP("Proc_PODBulkRecDateUpdate", ParameterName, ParameterValue);

                string msg = dt.Rows[0]["Message"].ToString();

                ClientScript.RegisterStartupScript(this.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + msg + "');window.location='ListPOD.aspx';",
                    true);

            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now + " | POD RecDate Bulk Update Error | " + ex.Message);

                Response.Write("<script>alert('Error occurred')</script>");
            }
        }
    }
}