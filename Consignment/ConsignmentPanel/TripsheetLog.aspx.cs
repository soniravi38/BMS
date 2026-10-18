using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Consignment.Model;
using System.Collections;

namespace Consignment.ConsignmentPanel
{
    public partial class TripsheetLog : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        connect con = new connect();
        #endregion

        #region [pageload]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDriver();
                FillVehicle();
                txtStartDate.Text = Session["TripsheetStartDate"].ToString();
                txtEndDate.Text = Session["TripsheetEndDate"].ToString();
                ddlVehicleNo.SelectedValue = Session["TripsheetVehicleId"].ToString();
                txtNoOfDays.Text = Session["TripsheetNoDays"].ToString();
                GetVehicleDetails();
                GetTripAutoDataFromExport();
                FillList();
            }
        }
        
        private void FillDriver()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            DataTable dt = new DataTable();
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAllForDropdown");
                dt = con.GetDataTableFromSPForTripsheet("[ProcDriver]", ParameterName, ParameterValue);
                ddlDriver.DataSource = dt;
                ddlDriver.DataValueField = "Id";
                ddlDriver.DataTextField = "Name";
                ddlDriver.DataBind();
                ddlDriver.Items.Insert(0, new ListItem("Select Driver", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        private void GetTripAutoDataFromExport()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "ProcTripsheetHead";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("InsertFromExportFile");
                ParameterName.Add("@TripSheetId"); ParameterValue.Add(Convert.ToInt64(Session["TripsheetId"]));
                ParameterName.Add("@StartDate"); ParameterValue.Add(txtStartDate.Text);
                ParameterName.Add("@EndDate"); ParameterValue.Add(txtEndDate.Text);
                ParameterName.Add("@VehicleSize"); ParameterValue.Add(ddlVehicleNo.SelectedItem.Text);
                dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                if (dt.Rows.Count > 0)
                {
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FillList()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "ProcTripsheetHead";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetByTripsheetId");
                ParameterName.Add("@TripSheetId"); ParameterValue.Add(Convert.ToInt64(Session["TripsheetId"]));
                dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                gvReport.DataSource = dt;
                gvReport.DataBind();

            }
            catch (Exception ex)
            {

            }

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
                
            }
        }
        public void GetVehicleDetails()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            DataTable dt = new DataTable();
            try
            {
                if (Convert.ToInt32(ddlVehicleNo.SelectedValue) > 0)
                {
                    ParameterName.Add("@Action"); ParameterValue.Add("GetVehicleDetails");
                    ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(ddlVehicleNo.SelectedValue));
                    dt = con.GetDataTableFromSPForTripsheet("[ProcVehicle]", ParameterName, ParameterValue);
                    if (dt.Rows.Count > 0)
                    {
                        txtVehicleSize.Text = dt.Rows[0]["VehicleSize"].ToString();
                        if (dt.Rows[0]["DriverId"].ToString() != "")
                        {
                            ddlDriver.SelectedValue = dt.Rows[0]["DriverId"].ToString();
                        }
                        else
                        {
                            ddlDriver.SelectedIndex = 0;
                        }
                        txtGuarantor.Text = dt.Rows[0]["Gaurantor"].ToString();
                        ddlVehicleType.SelectedValue = dt.Rows[0]["VehicleType"].ToString();
                        if (ddlVehicleType.SelectedValue.ToString() == "REEFER")
                        {
                            DivLoadType.Visible = true;
                        }
                        else
                        {
                            DivLoadType.Visible = false;

                        }
                    }
                }
                else
                {
                    txtVehicleSize.Text = "";
                    ddlDriver.SelectedIndex = 0;
                    ddlVehicleType.SelectedIndex = 0;
                    txtGuarantor.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


    }
}