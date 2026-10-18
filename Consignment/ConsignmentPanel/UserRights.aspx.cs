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
    public partial class UserRights : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                FillUser();
            }
        }

        #region[Function]
        private void FillUser()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_User";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("ListExcluedingAdmin");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlUser.DataSource = dt;
                    ddlUser.DataValueField = "Id";
                    ddlUser.DataTextField = "Username";
                    ddlUser.DataBind();
                    ddlUser.Items.Insert(0, new ListItem("--Select User--", "0"));
                }
                else
                {
                    ddlUser.Items.Clear();
                    ddlUser.Items.Insert(0, new ListItem("--Select User--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void FillRightsMenu()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_UserRights";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetByUserId");
                ParameterName.Add("@UserId"); ParameterValue.Add(ddlUser.SelectedValue);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataView dv1 = dt.DefaultView;
                    DataTable dtNew = dv1.ToTable();
                    rpMenu.DataSource = dtNew.DefaultView;
                    rpMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        #endregion

        #region[Events]
        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRightsMenu();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string spName = "[Proc_UserRights]";
            try
            {
                
                foreach (RepeaterItem item in rpMenu.Items)
                {
                    Label HeadName = (Label)item.FindControl("HeadName");
                    CheckBox chkList = (CheckBox)item.FindControl("chkList");
                    CheckBox chkCreate = (CheckBox)item.FindControl("chkCreate");
                    CheckBox chkUpdate = (CheckBox)item.FindControl("chkUpdate");
                    CheckBox chkDelete = (CheckBox)item.FindControl("chkDelete");
                    CheckBox chkActive = (CheckBox)item.FindControl("chkActive");
                    CheckBox chkExport = (CheckBox)item.FindControl("chkExport");
                    dic.Add(HeadName.Text, ((chkList.Checked == true) ? "1" : "0") + "," + ((chkCreate.Checked == true) ? "1" : "0") + "," + ((chkUpdate.Checked == true) ? "1" : "0") + "," + ((chkDelete.Checked == true) ? "1" : "0") + "," + ((chkActive.Checked == true) ? "1" : "0") + "," + ((chkExport.Checked == true) ? "1" : "0"));
                }

                ParameterName.Add("@Action"); ParameterValue.Add("UPDATE");
                ParameterName.Add("@UserId"); ParameterValue.Add(ddlUser.SelectedValue);
                ParameterName.Add("@Branch"); ParameterValue.Add(dic["Branch"].ToString());
                ParameterName.Add("@Size"); ParameterValue.Add(dic["Size"].ToString());
                ParameterName.Add("@Unit"); ParameterValue.Add(dic["Unit"].ToString());
                ParameterName.Add("@State"); ParameterValue.Add(dic["State"].ToString());
                ParameterName.Add("@Party"); ParameterValue.Add(dic["Party"].ToString());
                ParameterName.Add("@Station"); ParameterValue.Add(dic["Station"].ToString());
                ParameterName.Add("@Vehicle"); ParameterValue.Add(dic["Vehicle"].ToString());
                ParameterName.Add("@LRManagement"); ParameterValue.Add(dic["LR_Management"].ToString());
                ParameterName.Add("@ManualLRManagement"); ParameterValue.Add(dic["Manual_LR_Management"].ToString());
                ParameterName.Add("@VajraLock"); ParameterValue.Add(dic["Vajra_Lock"].ToString());
                ParameterName.Add("@VehicleSize"); ParameterValue.Add(dic["Vehicle_Size"].ToString());
                ParameterName.Add("@Diesel"); ParameterValue.Add(dic["Diesel"].ToString());
                ParameterName.Add("@CashData"); ParameterValue.Add(dic["Cash_Data"].ToString());
                ParameterName.Add("@ConsignmentEntry"); ParameterValue.Add(dic["Consignment_Entry"].ToString());
                ParameterName.Add("@ConsignmentReport"); ParameterValue.Add(dic["Consignment_Report"].ToString());
                ParameterName.Add("@ViewConsignment"); ParameterValue.Add(dic["View_Consignment"].ToString());
                ParameterName.Add("@DispatchReport"); ParameterValue.Add(dic["Dispatch_Report"].ToString());
                ParameterName.Add("@BNGExport"); ParameterValue.Add(dic["BNG_Export"].ToString());
                ParameterName.Add("@VehicleStatus"); ParameterValue.Add(dic["VehicleStatus"].ToString());
                ParameterName.Add("@Driver"); ParameterValue.Add(dic["Driver"].ToString());
                ParameterName.Add("@Transporter"); ParameterValue.Add(dic["Transporter"].ToString());
                ParameterName.Add("@LCEntry"); ParameterValue.Add(dic["LCEntry"].ToString());
                ParameterName.Add("@LCManagement"); ParameterValue.Add(dic["LC_Management"].ToString());
                ParameterName.Add("@LCReport"); ParameterValue.Add(dic["LC_Report"].ToString());
                ParameterName.Add("@VehicleDoc"); ParameterValue.Add(dic["VehicleDoc"].ToString());
                ParameterName.Add("@BranchwiseParty"); ParameterValue.Add(dic["BranchwiseParty"].ToString());
                ParameterName.Add("@POD"); ParameterValue.Add(dic["POD"].ToString());
                ParameterName.Add("@LRManagementMBT"); ParameterValue.Add(dic["LrManagementMBT"].ToString());
                ParameterName.Add("@ConsignmentReportMBT"); ParameterValue.Add(dic["Consignment_Report_MBT"].ToString());
                ParameterName.Add("@ViewConsignmentMBT"); ParameterValue.Add(dic["View_Consignment_MBT"].ToString());
                ParameterName.Add("@DispatchReportMBT"); ParameterValue.Add(dic["Dispatch_Report_MBT"].ToString());
                ParameterName.Add("@InternalLR"); ParameterValue.Add(dic["InternalLR"].ToString());
                intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                if (intResult > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='UserRights.aspx';", true);
                }
                else
                {
                    Response.Write("<script>alert('Oops! something went wrong')</script>");
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