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
    public partial class AddCashData : System.Web.UI.Page
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
                FillVehicle();
                FillFromAndTo();
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update Cash Data";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Cash Data";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [all-fuctions]
        private void FillVehicle()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Vehicle";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("ListOwn");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlVehicle.DataSource = dt;
                    ddlVehicle.DataValueField = "VehicleNo";
                    ddlVehicle.DataTextField = "VehicleNo";
                    ddlVehicle.DataBind();
                    ddlVehicle.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));
                }
                else
                {
                    ddlVehicle.Items.Clear();
                    ddlVehicle.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void FillFromAndTo()
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
                    ddlFrom.DataSource = dt;
                    ddlFrom.DataValueField = "Name";
                    ddlFrom.DataTextField = "Name";
                    ddlFrom.DataBind();
                    ddlFrom.Items.Insert(0, new ListItem("--Select From--", "0"));

                    ddlTo.DataSource = dt;
                    ddlTo.DataValueField = "Name";
                    ddlTo.DataTextField = "Name";
                    ddlTo.DataBind();
                    ddlTo.Items.Insert(0, new ListItem("--Select To--", "0"));
                }
                else
                {
                    ddlFrom.Items.Clear();
                    ddlFrom.Items.Insert(0, new ListItem("--Select From--", "0"));

                    ddlTo.Items.Clear();
                    ddlTo.Items.Insert(0, new ListItem("--Select To--", "0"));
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
            string spName = "Proc_CashReconciliation";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtDate.Text = Convert.ToString(dt.Rows[0]["Date"]);
                    ddlVehicle.SelectedValue = Convert.ToString(dt.Rows[0]["VehicleNo"]);
                    txtDriver.Text = Convert.ToString(dt.Rows[0]["Driver"]);
                    ddlFrom.SelectedValue = Convert.ToString(dt.Rows[0]["From"]);
                    ddlTo.SelectedValue = Convert.ToString(dt.Rows[0]["To"]);
                    txtAmount.Text = Convert.ToString(dt.Rows[0]["Amount"]);
                    txtPurpose.Text = Convert.ToString(dt.Rows[0]["Purpose"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtDate.Text = "";
            ddlVehicle.SelectedIndex = 0;
            txtDriver.Text = "";
            ddlFrom.SelectedIndex = 0;
            ddlTo.SelectedIndex = 0;
            txtAmount.Text = "";
            txtPurpose.Text = "";
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        #endregion


        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string spName = "[Proc_CashReconciliation]";
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            ParameterName.Add("@Action"); ParameterValue.Add("CheckDateValidate");
            ParameterName.Add("@Date"); ParameterValue.Add(txtDate.Text);
            ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails()).Rows[0]["UserType"]);
            DataTable dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
            if (Convert.ToInt32(dt.Rows[0]["status"]) == 1)
            {
                Response.Write("<script>alert('Oops! You can Only entry two days back')</script>");
                return;
            }

            if(txtAmount.Text == "")
            {
                Response.Write("<script>alert('Oops! Amount should not be blank')</script>");
                return;
            }

            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            spName = "[Proc_CashReconciliation]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@Date"); ParameterValue.Add(txtDate.Text);
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(ddlVehicle.SelectedValue);
                    ParameterName.Add("@Driver"); ParameterValue.Add(txtDriver.Text);
                    ParameterName.Add("@From"); ParameterValue.Add(ddlFrom.SelectedValue);
                    ParameterName.Add("@To"); ParameterValue.Add(ddlTo.SelectedValue);
                    ParameterName.Add("@Amount"); ParameterValue.Add(txtAmount.Text);
                    ParameterName.Add("@Purpose"); ParameterValue.Add(txtPurpose.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@UpdatedBy"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@PayingBranch"); ParameterValue.Add(Convert.ToInt32((cm.GetUserDetails()).Rows[0]["BranchId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Cash Data", "Inserted", txtDate.Text+"|"+ ddlVehicle.SelectedValue);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListCashData.aspx';", true);
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
                    ParameterName.Add("@Date"); ParameterValue.Add(txtDate.Text);
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(ddlVehicle.SelectedValue);
                    ParameterName.Add("@Driver"); ParameterValue.Add(txtDriver.Text);
                    ParameterName.Add("@From"); ParameterValue.Add(ddlFrom.SelectedValue);
                    ParameterName.Add("@To"); ParameterValue.Add(ddlTo.SelectedValue);
                    ParameterName.Add("@Amount"); ParameterValue.Add(txtAmount.Text);
                    ParameterName.Add("@Purpose"); ParameterValue.Add(txtPurpose.Text);
                    ParameterName.Add("@UpdatedBy"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    ParameterName.Add("@PayingBranch"); ParameterValue.Add(Convert.ToInt32((cm.GetUserDetails()).Rows[0]["BranchId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Cash Data", "Updated", txtDate.Text + "|" + ddlVehicle.SelectedValue);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListCashData.aspx';", true);
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