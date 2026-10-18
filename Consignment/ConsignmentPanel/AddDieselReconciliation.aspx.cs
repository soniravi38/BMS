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
    public partial class AddDieselReconciliation : System.Web.UI.Page
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
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update Diesel Data";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Diesel Data";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [all-fuctions]
        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_DieselReconciliation";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtLRDate.Text = Convert.ToString(dt.Rows[0]["LrDate"]);
                    txtLrNo.Text = Convert.ToString(dt.Rows[0]["LRNo"]);
                    txtVehicleShortNo.Text = Convert.ToString(dt.Rows[0]["VechicleShortNo"]);
                    txtVehicleNo.Text = Convert.ToString(dt.Rows[0]["VechicleFullNo"]);
                    txtVehicleMode.Text = Convert.ToString(dt.Rows[0]["VehicleMode"]);
                    txtFCNo.Text = Convert.ToString(dt.Rows[0]["FCNo"]);
                    txtRemark.Text = Convert.ToString(dt.Rows[0]["Remarks"]);
                    txtAmount.Text = Convert.ToString(dt.Rows[0]["Amount"]);
                    txtTotalCMS.Text = Convert.ToString(dt.Rows[0]["TotalCMS"]);
                    txtDestination.Text = Convert.ToString(dt.Rows[0]["Destination"]);
                    txtFrom.Text = Convert.ToString(dt.Rows[0]["From"]);
                    txtTo.Text = Convert.ToString(dt.Rows[0]["To"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtLRDate.Text = "";
            txtLrNo.Text = "";
            txtVehicleShortNo.Text = "";
            txtVehicleNo.Text = "";
            txtVehicleMode.Text = "";
            txtFCNo.Text = "";
            txtRemark.Text = "";
            txtAmount.Text = "";
            txtTotalCMS.Text = "";
            txtDestination.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            string spName = "[Proc_DieselReconciliation]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@LrDate"); ParameterValue.Add(txtLRDate.Text);
                    ParameterName.Add("@LRNo"); ParameterValue.Add(txtLrNo.Text);
                    ParameterName.Add("@VechicleShortNo"); ParameterValue.Add(txtVehicleShortNo.Text);
                    ParameterName.Add("@VechicleFullNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@VehicleMode"); ParameterValue.Add(txtVehicleMode.Text);
                    ParameterName.Add("@FCNo"); ParameterValue.Add(txtFCNo.Text);
                    ParameterName.Add("@Remarks"); ParameterValue.Add(txtRemark.Text);
                    ParameterName.Add("@Amount"); ParameterValue.Add(txtAmount.Text);
                    ParameterName.Add("@TotalCMS"); ParameterValue.Add(txtTotalCMS.Text);
                    ParameterName.Add("@Destination"); ParameterValue.Add(txtDestination.Text);
                    ParameterName.Add("@From"); ParameterValue.Add(txtFrom.Text);
                    ParameterName.Add("@To"); ParameterValue.Add(txtTo.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Diesel", "Inserted", txtLRDate.Text+"|"+ txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListDieselReconciliation.aspx';", true);
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
                    ParameterName.Add("@LrDate"); ParameterValue.Add(txtLRDate.Text);
                    ParameterName.Add("@LRNo"); ParameterValue.Add(txtLrNo.Text);
                    ParameterName.Add("@VechicleShortNo"); ParameterValue.Add(txtVehicleShortNo.Text);
                    ParameterName.Add("@VechicleFullNo"); ParameterValue.Add(txtVehicleNo.Text);
                    ParameterName.Add("@VehicleMode"); ParameterValue.Add(txtVehicleMode.Text);
                    ParameterName.Add("@FCNo"); ParameterValue.Add(txtFCNo.Text);
                    ParameterName.Add("@Remarks"); ParameterValue.Add(txtRemark.Text);
                    ParameterName.Add("@Amount"); ParameterValue.Add(txtAmount.Text);
                    ParameterName.Add("@TotalCMS"); ParameterValue.Add(txtTotalCMS.Text);
                    ParameterName.Add("@Destination"); ParameterValue.Add(txtDestination.Text);
                    ParameterName.Add("@From"); ParameterValue.Add(txtFrom.Text);
                    ParameterName.Add("@To"); ParameterValue.Add(txtTo.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Diesel", "Updated", txtLRDate.Text + "|" + txtVehicleNo.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListDieselReconciliation.aspx';", true);
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