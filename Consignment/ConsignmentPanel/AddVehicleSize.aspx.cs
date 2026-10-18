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
    public partial class AddVehicleSize : System.Web.UI.Page
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
                    lblHeading.Text = "Update Vehicle Size";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Vehicle Size";
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
            string spName = "Proc_VehicleReconciliation";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtVehicleNo.Text = Convert.ToString(dt.Rows[0]["VehicleNo"]);
                    txtLenght.Text = Convert.ToString(dt.Rows[0]["Lenght"]);
                    txtHeight.Text = Convert.ToString(dt.Rows[0]["Height"]);
                    txtWidth.Text = Convert.ToString(dt.Rows[0]["Width"]);
                    txtOwnMarket.Text = Convert.ToString(dt.Rows[0]["OwnMarket"]);
                    txtCFT.Text = Convert.ToString(dt.Rows[0]["CFT"]);
                    txtCode.Text = Convert.ToString(dt.Rows[0]["Code"]);
                    txtLastUpdated.Text = Convert.ToString(dt.Rows[0]["LastUpdated"]);
                    ddlVehicleSize.SelectedValue = Convert.ToString(dt.Rows[0]["Size"]);
                    txtRemark.Text = Convert.ToString(dt.Rows[0]["Remark"]);
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
            txtLenght.Text = "";
            txtHeight.Text = "";
            txtWidth.Text = "";
            txtOwnMarket.Text = "";
            txtCFT.Text = "";
            txtCode.Text = "";
            txtLastUpdated.Text = "";
            ddlVehicleSize.SelectedIndex = 0;
            txtRemark.Text = "";
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            string spName = "[Proc_VehicleReconciliation]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@Lenght"); ParameterValue.Add(txtLenght.Text);
                    ParameterName.Add("@Height"); ParameterValue.Add(txtHeight.Text);
                    ParameterName.Add("@Width"); ParameterValue.Add(txtWidth.Text);
                    ParameterName.Add("@OwnMarket"); ParameterValue.Add(txtOwnMarket.Text);
                    ParameterName.Add("@CFT"); ParameterValue.Add(txtCFT.Text);
                    ParameterName.Add("@Code"); ParameterValue.Add(txtCode.Text);
                    ParameterName.Add("@LastUpdated"); ParameterValue.Add(txtLastUpdated.Text);
                    ParameterName.Add("@Size"); ParameterValue.Add(ddlVehicleSize.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@Remark"); ParameterValue.Add(txtRemark.Text);
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Vehicle Size", "Inserted", txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListVehicleSize.aspx';", true);
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
                    ParameterName.Add("@Lenght"); ParameterValue.Add(txtLenght.Text);
                    ParameterName.Add("@Height"); ParameterValue.Add(txtHeight.Text);
                    ParameterName.Add("@Width"); ParameterValue.Add(txtWidth.Text);
                    ParameterName.Add("@OwnMarket"); ParameterValue.Add(txtOwnMarket.Text);
                    ParameterName.Add("@CFT"); ParameterValue.Add(txtCFT.Text);
                    ParameterName.Add("@Code"); ParameterValue.Add(txtCode.Text);
                    ParameterName.Add("@LastUpdated"); ParameterValue.Add(txtLastUpdated.Text);
                    ParameterName.Add("@Size"); ParameterValue.Add(ddlVehicleSize.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@Remark"); ParameterValue.Add(txtRemark.Text);
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Vehicle Size", "Updated", txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListVehicleSize.aspx';", true);
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