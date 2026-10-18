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
using static System.Net.WebRequestMethods;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Consignment.ConsignmentPanel
{
    public partial class AddBookLC : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();
        #endregion

        #region [PageLoad]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillSource();
                FillUnit();
                //FillDriver();
                FillTransporter();
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update LC";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add LC";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [Function]
        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLC";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    TextBox1.Text = Convert.ToString(dt.Rows[0]["VehicleNo"]);
                    VehicleFilter();
                    txtVehicleHirNo.Text = Convert.ToString(dt.Rows[0]["VehicleHireNo"]);

                    DateTime parsedDate = DateTime.ParseExact(Convert.ToString(dt.Rows[0]["LCDate"]), "dd-MM-yyyy", null);
                    string formattedDate = parsedDate.ToString("yyyy-MM-dd");

                    txtdate.Text = formattedDate;
                    ddSourceFrom.SelectedValue = Convert.ToString(dt.Rows[0]["SourceFromid"]);
                    ddSourceTo.SelectedValue = Convert.ToString(dt.Rows[0]["SourceToid"]);
                    ddlVchNo.SelectedValue = Convert.ToString(dt.Rows[0]["VehicleId"]);
                    txtVehicleOwnerName.Text = Convert.ToString(dt.Rows[0]["VehicleOwnerName"]);
                    txtVehicleOwnerAddress.Text = Convert.ToString(dt.Rows[0]["VehicleOwnerAddress"]);
                    txtChasisNo.Text = Convert.ToString(dt.Rows[0]["VehicleChasisNo"]);
                    txtEngine.Text = Convert.ToString(dt.Rows[0]["VehicleEngineNo"]);
                    txtVehicleSize.Text = Convert.ToString(dt.Rows[0]["VehicleSize"]);
                    txtConsignmentNo.Text = Convert.ToString(dt.Rows[0]["ConsignmentNo"]);
                    txtDestination.Text = Convert.ToString(dt.Rows[0]["Destination"]);
                    ddlunit.SelectedValue = Convert.ToString(dt.Rows[0]["Unit"]);
                    txtQty.Text = Convert.ToString(dt.Rows[0]["Qty"]);
                    txtWeight.Text = Convert.ToString(dt.Rows[0]["Weight"]);
                    txtDriver.Text = Convert.ToString(dt.Rows[0]["DriverName"]);
                    txtDriverLicenseNo.Text = Convert.ToString(dt.Rows[0]["DriverLicenseNo"]);
                    txtDriverMobile.Text = Convert.ToString(dt.Rows[0]["DriverMobile"]);
                    ddlTransporter.SelectedValue = Convert.ToString(dt.Rows[0]["TransporterId"]);
                    txtFreight.Text = Convert.ToString(dt.Rows[0]["Freight"]);
                    txtHamali.Text = Convert.ToString(dt.Rows[0]["Hamali"]);
                    txtDetention.Text = Convert.ToString(dt.Rows[0]["Detention"]);
                    txtOtherCharges.Text = Convert.ToString(dt.Rows[0]["OtherCharges"]);
                    txtDelayCharges.Text = Convert.ToString(dt.Rows[0]["DelayCharges"]);
                    txtOtherDeduction.Text = Convert.ToString(dt.Rows[0]["OtherDeduction"]);
                    txtTDS.Text = Convert.ToString(dt.Rows[0]["TDS"]);
                    txtTotal.Text = Convert.ToString(dt.Rows[0]["Total"]);
                    txtAdvance.Text = Convert.ToString(dt.Rows[0]["Advance"]);
                    txtBalance.Text = Convert.ToString(dt.Rows[0]["Balance"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void FillUnit()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Unit";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlunit.DataSource = dt;
                    ddlunit.DataValueField = "Name";
                    ddlunit.DataTextField = "Name";
                    ddlunit.DataBind();
                    ddlunit.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                }
                else
                {
                    ddlunit.Items.Clear();
                    ddlunit.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void FillSource()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Station";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddSourceFrom.DataSource = dt;
                    ddSourceFrom.DataValueField = "Id";
                    ddSourceFrom.DataTextField = "Name";
                    ddSourceFrom.DataBind();
                    ddSourceFrom.Items.Insert(0, new ListItem("--Select Source From--", "0"));

                    ddSourceTo.DataSource = dt;
                    ddSourceTo.DataValueField = "Id";
                    ddSourceTo.DataTextField = "Name";
                    ddSourceTo.DataBind();
                    ddSourceTo.Items.Insert(0, new ListItem("--Select Source To--", "0"));
                }
                else
                {
                    ddSourceFrom.Items.Clear();
                    ddSourceFrom.Items.Insert(0, new ListItem("--Select Source From--", "0"));

                    ddSourceTo.Items.Clear();
                    ddSourceTo.Items.Insert(0, new ListItem("--Select Source To--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void FillTransporter()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Transporter";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlTransporter.DataSource = dt;
                    ddlTransporter.DataValueField = "Id";
                    ddlTransporter.DataTextField = "Name";
                    ddlTransporter.DataBind();
                    ddlTransporter.Items.Insert(0, new ListItem("--Select Transporter--", "0"));
                }
                else
                {
                    ddlTransporter.Items.Clear();
                    ddlTransporter.Items.Insert(0, new ListItem("--Select Transporter--", "0"));

                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void VehicleFilter()
        {
            if (TextBox1.Text.Trim() != "")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                string spName = "Proc_Vehicle";
                try
                {
                    ParameterName.Add("@Action"); ParameterValue.Add("ListByVehicleNo");
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(TextBox1.Text.Trim());
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        ddlVchNo.DataSource = dt1;
                        ddlVchNo.DataValueField = "Id";
                        ddlVchNo.DataTextField = "VehicleNo";
                        ddlVchNo.DataBind();
                        ddlVchNo.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

                        lblMessage.Text = "Total Result " + dt1.Rows.Count.ToString();
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
        }
        private void Clear()
        {
            txtVehicleHirNo.Text = "";
            txtdate.Text = "";
            ddSourceFrom.SelectedIndex = 0;
            ddSourceTo.SelectedIndex = 0;
            TextBox1.Text = "";
            ddlVchNo.SelectedIndex = 0;
            txtVehicleOwnerName.Text = "";
            txtVehicleOwnerAddress.Text = "";
            txtChasisNo.Text = "";
            txtEngine.Text = "";
            txtVehicleSize.Text = "";
            txtConsignmentNo.Text = "";
            txtDestination.Text = "";
            ddlunit.SelectedIndex = 0;
            txtQty.Text = "";
            txtWeight.Text = "Fixed";
            txtDriver.Text = "";
            txtDriverLicenseNo.Text = "";
            txtDriverMobile.Text = "";
            ddlTransporter.SelectedIndex = 0;
            txtFreight.Text = "0";
            txtHamali.Text = "0";
            txtDetention.Text = "0";
            txtOtherCharges.Text = "0";
            txtDelayCharges.Text = "0";
            txtOtherDeduction.Text = "0";
            txtTDS.Text = "0";
            txtTotal.Text = "0";
            txtAdvance.Text = "0";
            txtBalance.Text = "0";

        }
        #endregion

        #region [Events]
        

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            VehicleFilter();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            string spName = "[Proc_BookLC]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@VehicleHireNo"); ParameterValue.Add(txtVehicleHirNo.Text);
                    ParameterName.Add("@LCDate"); ParameterValue.Add(txtdate.Text);
                    ParameterName.Add("@SourceFromid"); ParameterValue.Add(ddSourceFrom.SelectedValue);
                    ParameterName.Add("@SourceToid"); ParameterValue.Add(ddSourceTo.SelectedValue);
                    ParameterName.Add("@VehicleId"); ParameterValue.Add(ddlVchNo.SelectedValue);
                    ParameterName.Add("@VehicleOwnerName"); ParameterValue.Add(txtVehicleOwnerName.Text);
                    ParameterName.Add("@VehicleOwnerAddress"); ParameterValue.Add(txtVehicleOwnerAddress.Text);
                    ParameterName.Add("@VehicleChasisNo"); ParameterValue.Add(txtChasisNo.Text);
                    ParameterName.Add("@VehicleEngineNo"); ParameterValue.Add(txtEngine.Text);
                    ParameterName.Add("@VehicleSize"); ParameterValue.Add(txtVehicleSize.Text);
                    ParameterName.Add("@ConsignmentNo"); ParameterValue.Add(txtConsignmentNo.Text);
                    ParameterName.Add("@Destination"); ParameterValue.Add(txtDestination.Text);
                    ParameterName.Add("@Unit"); ParameterValue.Add(ddlunit.SelectedValue);
                    ParameterName.Add("@Qty"); ParameterValue.Add(txtQty.Text);
                    ParameterName.Add("@Weight"); ParameterValue.Add(txtWeight.Text);
                    ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriver.Text);
                    ParameterName.Add("@DriverLicenseNo"); ParameterValue.Add(txtDriverLicenseNo.Text);
                    ParameterName.Add("@DriverMobile"); ParameterValue.Add(txtDriverMobile.Text);
                    ParameterName.Add("@TransporterId"); ParameterValue.Add(ddlTransporter.SelectedValue);
                    ParameterName.Add("@Freight"); ParameterValue.Add(txtFreight.Text);
                    ParameterName.Add("@Hamali"); ParameterValue.Add(txtHamali.Text);
                    ParameterName.Add("@Detention"); ParameterValue.Add(txtDetention.Text);
                    ParameterName.Add("@OtherCharges"); ParameterValue.Add(txtOtherCharges.Text);
                    ParameterName.Add("@DelayCharges"); ParameterValue.Add(txtDelayCharges.Text);
                    ParameterName.Add("@OtherDeduction"); ParameterValue.Add(txtOtherDeduction.Text);
                    ParameterName.Add("@TDS"); ParameterValue.Add(txtTDS.Text);
                    ParameterName.Add("@Total"); ParameterValue.Add(Request.Form[txtTotal.UniqueID]);
                    ParameterName.Add("@Advance"); ParameterValue.Add(txtAdvance.Text);
                    ParameterName.Add("@Balance"); ParameterValue.Add(Request.Form[txtBalance.UniqueID]);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@BookBranchid"); ParameterValue.Add(Convert.ToInt32((cm.GetUserDetails().Rows[0]["BranchId"]).ToString()));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("LC", "Inserted", txtVehicleHirNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='LCReportViewer.aspx?id=" + txtVehicleHirNo.Text + "';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! something went wrong')</script>");
                        Clear();
                    }
                    #endregion
                }
                else
                {
                    #region[Update]
                    ParameterName.Add("@Action"); ParameterValue.Add("Update");
                    ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["Id"]));
                    ParameterName.Add("@VehicleHireNo"); ParameterValue.Add(txtVehicleHirNo.Text);
                    ParameterName.Add("@LCDate"); ParameterValue.Add(txtdate.Text);
                    ParameterName.Add("@SourceFromid"); ParameterValue.Add(ddSourceFrom.SelectedValue);
                    ParameterName.Add("@SourceToid"); ParameterValue.Add(ddSourceTo.SelectedValue);
                    ParameterName.Add("@VehicleId"); ParameterValue.Add(ddlVchNo.SelectedValue);
                    ParameterName.Add("@VehicleOwnerName"); ParameterValue.Add(txtVehicleOwnerName.Text);
                    ParameterName.Add("@VehicleOwnerAddress"); ParameterValue.Add(txtVehicleOwnerAddress.Text);
                    ParameterName.Add("@VehicleChasisNo"); ParameterValue.Add(txtChasisNo.Text);
                    ParameterName.Add("@VehicleEngineNo"); ParameterValue.Add(txtEngine.Text);
                    ParameterName.Add("@VehicleSize"); ParameterValue.Add(txtVehicleSize.Text);
                    ParameterName.Add("@ConsignmentNo"); ParameterValue.Add(txtConsignmentNo.Text);
                    ParameterName.Add("@Destination"); ParameterValue.Add(txtDestination.Text);
                    ParameterName.Add("@Unit"); ParameterValue.Add(ddlunit.SelectedValue);
                    ParameterName.Add("@Qty"); ParameterValue.Add(txtQty.Text);
                    ParameterName.Add("@Weight"); ParameterValue.Add(txtWeight.Text);
                    ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriver.Text);
                    ParameterName.Add("@DriverLicenseNo"); ParameterValue.Add(txtDriverLicenseNo.Text);
                    ParameterName.Add("@DriverMobile"); ParameterValue.Add(txtDriverMobile.Text);
                    ParameterName.Add("@TransporterId"); ParameterValue.Add(ddlTransporter.SelectedValue);
                    ParameterName.Add("@Freight"); ParameterValue.Add(txtFreight.Text);
                    ParameterName.Add("@Hamali"); ParameterValue.Add(txtHamali.Text);
                    ParameterName.Add("@Detention"); ParameterValue.Add(txtDetention.Text);
                    ParameterName.Add("@OtherCharges"); ParameterValue.Add(txtOtherCharges.Text);
                    ParameterName.Add("@DelayCharges"); ParameterValue.Add(txtDelayCharges.Text);
                    ParameterName.Add("@OtherDeduction"); ParameterValue.Add(txtOtherDeduction.Text);
                    ParameterName.Add("@TDS"); ParameterValue.Add(txtTDS.Text);
                    ParameterName.Add("@Total"); ParameterValue.Add(txtTotal.Text);
                    ParameterName.Add("@Advance"); ParameterValue.Add(txtAdvance.Text);
                    ParameterName.Add("@Balance"); ParameterValue.Add(txtBalance.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("LC", "Updated", txtVehicleHirNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='LCReportViewer.aspx?id="+ txtVehicleHirNo.Text + "';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! something went wrong')</script>");
                        Clear();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            #endregion
        }
        protected void ddlVchNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Vehicle";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(ddlVchNo.SelectedValue);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtVehicleSize.Text = dt.Rows[0]["Size"].ToString();
                    txtVehicleOwnerName.Text = dt.Rows[0]["OwnerName"].ToString();
                    txtVehicleOwnerAddress.Text = dt.Rows[0]["VehicleAdd"].ToString();
                    txtChasisNo.Text = dt.Rows[0]["ChasisNo"].ToString();
                    txtEngine.Text = dt.Rows[0]["EngineNo"].ToString();
                }

                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                spName = "Proc_VajraLock";
                ParameterName.Add("@Action"); ParameterValue.Add("GetByVehicleNo");
                ParameterName.Add("@VehicleNo"); ParameterValue.Add(ddlVchNo.SelectedItem.Text);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Vajra Lock Already Installed In This Vehicle');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Vajra Lock is not installed in this vehicle');", true);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }

        }
        #endregion


    }
}