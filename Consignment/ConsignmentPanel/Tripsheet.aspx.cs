using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using Consignment.Model;
using Org.BouncyCastle.Asn1.X509;

namespace Consignment.ConsignmentPanel
{
    public partial class Tripsheet : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillVehicle();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("000000");

            Session["TripsheetVehicleId"] = ddlVehicleNo.SelectedValue;
            Session["TripsheetStartDate"] = txtStartDate.Text;
            Session["TripsheetEndDate"] = txtEndDate.Text;
            Session["TripsheetNoDays"] = txtNoOfDays.Text;
            Session["TripsheetId"] = s;
            Response.Redirect("TripsheetLog.aspx",false);
        }

        private void FillVehicle()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "ProcVehicle";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAllForDropdown");
                dt = con.GetDataTableFromSPForTripsheet(spName, ParameterName, ParameterValue);
                if (dt.Rows.Count > 0)
                {
                    ddlVehicleNo.DataSource = dt;
                    ddlVehicleNo.DataValueField = "Id";
                    ddlVehicleNo.DataTextField = "VehicleNo";
                    ddlVehicleNo.DataBind();
                    ddlVehicleNo.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));
                }
                else
                {
                    ddlVehicleNo.Items.Clear();
                    ddlVehicleNo.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
    }
}