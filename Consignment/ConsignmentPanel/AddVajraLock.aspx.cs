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
using System.Xml.Linq;

namespace Consignment.ConsignmentPanel
{
    public partial class AddVajraLock : System.Web.UI.Page
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
                FillSize();
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update Vajra Lock";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Vajra Lock";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [all-fuctions]
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
                    ddlVehicleSize.DataSource = dt;
                    ddlVehicleSize.DataValueField = "Name";
                    ddlVehicleSize.DataTextField = "Name";
                    ddlVehicleSize.DataBind();
                    ddlVehicleSize.Items.Insert(0, new ListItem("--Select Vehicle Size--", "0"));
                }
                else
                {
                    ddlVehicleSize.Items.Clear();
                    ddlVehicleSize.Items.Insert(0, new ListItem("--Select Vehicle Size--", "0"));
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
            string spName = "Proc_VajraLock";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtVehicleNo.Text = Convert.ToString(dt.Rows[0]["VehicleNo"]);
                    ddlCompany.SelectedValue = Convert.ToString(dt.Rows[0]["Company"]);
                    ddlVehicleSize.SelectedValue = Convert.ToString(dt.Rows[0]["VehSize"]);
                    txtLineLocal.Text = Convert.ToString(dt.Rows[0]["LineLocal"]);
                    txtDoorSize.Text = Convert.ToString(dt.Rows[0]["DoorSize"]);
                    txtLockKeyNo.Text = Convert.ToString(dt.Rows[0]["LockKeyNo"]);
                    txtDriverName.Text = Convert.ToString(dt.Rows[0]["DriverName"]);
                    txtMobileNo.Text = Convert.ToString(dt.Rows[0]["MobileNo"]);
                    txtInstallationDate.Text = Convert.ToString(dt.Rows[0]["DateOfInstallation"]);
                    txtPlace.Text = Convert.ToString(dt.Rows[0]["Place"]);
                    ddlPlateDoorWeld.SelectedValue = Convert.ToString(dt.Rows[0]["PlateDoorWeld"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtVehicleNo.Text = "";
            ddlCompany.SelectedIndex = 0;
            ddlVehicleSize.SelectedIndex = 0;
            txtLineLocal.Text = "";
            txtDoorSize.Text = "";
            txtLockKeyNo.Text = "";
            txtDriverName.Text = "";
            txtMobileNo.Text = "";
            txtInstallationDate.Text = "";
            txtPlace.Text = "";
            ddlPlateDoorWeld.SelectedIndex = 0;
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            string spName = "[Proc_VajraLock]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@Company"); ParameterValue.Add(ddlCompany.SelectedValue);
                    ParameterName.Add("@VehSize"); ParameterValue.Add(ddlVehicleSize.SelectedValue);
                    ParameterName.Add("@LineLocal"); ParameterValue.Add(txtLineLocal.Text);
                    ParameterName.Add("@DoorSize"); ParameterValue.Add(txtDoorSize.Text);
                    ParameterName.Add("@LockKeyNo"); ParameterValue.Add(txtLockKeyNo.Text);
                    ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriverName.Text);
                    ParameterName.Add("@MobileNo"); ParameterValue.Add(txtMobileNo.Text);
                    ParameterName.Add("@DateOfInstallation"); ParameterValue.Add(txtInstallationDate.Text);
                    ParameterName.Add("@Place"); ParameterValue.Add(txtPlace.Text);
                    ParameterName.Add("@PlateDoorWeld"); ParameterValue.Add(ddlPlateDoorWeld.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Vajra Lock", "Inserted", txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListVajraLock.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! something went wrong')</script>");
                        clear();
                    }
                    #endregion
                }
                else
                {
                    #region[Update]
                    ParameterName.Add("@Action"); ParameterValue.Add("Update");
                    ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["Id"]));
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@Company"); ParameterValue.Add(ddlCompany.SelectedValue);
                    ParameterName.Add("@VehSize"); ParameterValue.Add(ddlVehicleSize.SelectedValue);
                    ParameterName.Add("@LineLocal"); ParameterValue.Add(txtLineLocal.Text);
                    ParameterName.Add("@DoorSize"); ParameterValue.Add(txtDoorSize.Text);
                    ParameterName.Add("@LockKeyNo"); ParameterValue.Add(txtLockKeyNo.Text);
                    ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriverName.Text);
                    ParameterName.Add("@MobileNo"); ParameterValue.Add(txtMobileNo.Text);
                    ParameterName.Add("@DateOfInstallation"); ParameterValue.Add(txtInstallationDate.Text);
                    ParameterName.Add("@Place"); ParameterValue.Add(txtPlace.Text);
                    ParameterName.Add("@PlateDoorWeld"); ParameterValue.Add(ddlPlateDoorWeld.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Vajra Lock", "Updated", txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListVajraLock.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! something went wrong')</script>");
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