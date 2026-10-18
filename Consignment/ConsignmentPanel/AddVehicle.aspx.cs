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

namespace Consignment.ConsignmentPanel
{
    public partial class AddVehicle : System.Web.UI.Page
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
                BindModes();
                FillSize();
                FillTransporter();
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update Vehicle";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Vehicle";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [all-fuctions]
        private void BindModes()
        {
            List<ListItem> modes;
            if ((cm.GetUserDetails().Rows[0]["UserType"]).ToString() == "A")
            {
                // Create a dictionary or list of modes
                 modes = new List<ListItem>
                {
                    new ListItem("--Select Mode--", ""),
                    new ListItem("OWN", "OWN"),
                    new ListItem("HIRED", "HIRED")
                };
            }
            else
            {
                 modes = new List<ListItem>
                {
                    new ListItem("--Select Mode--", ""),
                    //new ListItem("OWN", "OWN"),
                    new ListItem("HIRED", "HIRED")
                };
            }

            // Bind to DropDownList
            ddlMode.Items.Clear(); // Clear existing items
            ddlMode.Items.AddRange(modes.ToArray());
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

        private void FillSize()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Size";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlSize.DataSource = dt;
                    ddlSize.DataValueField = "Id";
                    ddlSize.DataTextField = "Name";
                    ddlSize.DataBind();
                    ddlSize.Items.Insert(0, new ListItem("--Select Size--", "0"));
                }
                else
                {
                    ddlSize.Items.Clear();
                    ddlSize.Items.Insert(0, new ListItem("--Select Size--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Vehicle";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                    txtVehicleNo.Text = Convert.ToString(dt.Rows[0]["VehicleNo"]);
                    ddlMode.SelectedValue = Convert.ToString(dt.Rows[0]["Mode"]);
                    ddlSize.SelectedItem.Text = Convert.ToString(dt.Rows[0]["Size"]);
                    txtOwnerName.Text = Convert.ToString(dt.Rows[0]["OwnerName"]);
                    txtChasisNo.Text = Convert.ToString(dt.Rows[0]["ChasisNo"]);
                    txtEngineNo.Text = Convert.ToString(dt.Rows[0]["EngineNo"]);
                    txtOwnerAddress.Text = Convert.ToString(dt.Rows[0]["VehicleAdd"]);
                    ddlTransporter.SelectedValue = Convert.ToString(dt.Rows[0]["TransporterId"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtName.Text = "";
            txtVehicleNo.Text = "";
            ddlMode.SelectedIndex = 0;
            ddlSize.SelectedIndex = 0;
            txtOwnerName.Text = "";
            txtChasisNo.Text = "";
            txtEngineNo.Text = "";
            txtOwnerAddress.Text = "";
            ddlTransporter.SelectedIndex = 0;
        }

        private int isExists(string VehicleNo)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Vehicle";
            try
            {
                ParameterName.Add("@VehicleNo"); ParameterValue.Add(VehicleNo);
                ParameterName.Add("@Action"); ParameterValue.Add("isExists");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Status"]);
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            string spName = "";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    if (isExists(txtVehicleNo.Text) == 1)
                    {
                        Response.Write("<script>alert('Vehicle No Is already Exists......')</script>");
                        return;
                    }
                    #region[Insert]
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    Int32 intResult = 0;
                    spName = "[Proc_Vehicle]";

                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@Name"); ParameterValue.Add(txtName.Text);
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@Mode"); ParameterValue.Add(ddlMode.SelectedValue);
                    ParameterName.Add("@Size"); ParameterValue.Add(ddlSize.SelectedItem.Text.ToString());
                    ParameterName.Add("@OwnerName"); ParameterValue.Add(txtOwnerName.Text);
                    ParameterName.Add("@ChasisNo"); ParameterValue.Add(txtChasisNo.Text);
                    ParameterName.Add("@EngineNo"); ParameterValue.Add(txtEngineNo.Text);
                    ParameterName.Add("@VehicleAdd"); ParameterValue.Add(txtOwnerAddress.Text);
                    ParameterName.Add("@TransporterId"); ParameterValue.Add(ddlTransporter.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Vehicle", "Inserted", txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListVehicle.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Vehicle No Is already Exists')</script>");
                        clear();
                    }
                    #endregion
                }
                else
                {
                    #region[Update]
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    Int32 intResult = 0;
                    spName = "[Proc_Vehicle]";

                    ParameterName.Add("@Action"); ParameterValue.Add("Update");
                    ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["Id"]));
                    ParameterName.Add("@Name"); ParameterValue.Add(txtName.Text);
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@Mode"); ParameterValue.Add(ddlMode.SelectedValue);
                    ParameterName.Add("@Size"); ParameterValue.Add(ddlSize.SelectedItem.Text.ToString());
                    ParameterName.Add("@OwnerName"); ParameterValue.Add(txtOwnerName.Text);
                    ParameterName.Add("@ChasisNo"); ParameterValue.Add(txtChasisNo.Text);
                    ParameterName.Add("@EngineNo"); ParameterValue.Add(txtEngineNo.Text);
                    ParameterName.Add("@VehicleAdd"); ParameterValue.Add(txtOwnerAddress.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@TransporterId"); ParameterValue.Add(ddlTransporter.SelectedValue);
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Vehicle", "Updated", txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListVehicle.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Vehicle No Is already Exists')</script>");
                        clear();
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        #endregion
    }
}