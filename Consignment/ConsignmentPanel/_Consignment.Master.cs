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
using System.Web.UI.WebControls.Expressions;

namespace Consignment.ConsignmentPanel
{
    public partial class _Consignment : System.Web.UI.MasterPage
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();
        #endregion

        #region[PageLoad]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["UserName"] != null && Session["Database_"] != null)
                {
                    if(Session["UserType"].ToString() != "A")
                    {
                        mnuTripsheetLog.Visible = false;
                        ValidateUserRights();
                    }
                    FillCompanyData();
                    FillUserData(Convert.ToInt32(Session["UserId"]));
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
        #endregion

        #region[Functions]
        private void FillCompanyData()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_CompanyMaster";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    titles.InnerText = "Consignment Panel - " + Convert.ToString(dt.Rows[0]["CompanyName"]);
                    lblFooter.Text = dt.Rows[0]["FooterText"].ToString();
                    imgCompanyLogo.ImageUrl = "../App_Content/Company/" + dt.Rows[0]["CompanyLogo"].ToString();
                    imgCompanyShortLogo.ImageUrl = "../App_Content/Company/" + dt.Rows[0]["CompanyShortLogo"].ToString();
                    hfUnderMaintenance.Value = dt.Rows[0]["isUnderMaintenance"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }

        private void ValidateUserRights()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_UserRights";
            try
            {
                mnuAdministration.Visible = false;

                ParameterName.Add("@Action"); ParameterValue.Add("GetByUserId");
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    mnuBranch.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Branch'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuSize.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Size'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuUnit.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Unit'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuState.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'State'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuParty.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Party'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuStation.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Station'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVehicle.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Vehicle'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuLRManagement.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'LR_Management'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuManualLRManagement.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Manual_LR_Management'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVajraLock.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Vajra_Lock'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVehicleSize.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Vehicle_Size'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuDiesel.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Diesel'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuCashData.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Cash_Data'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuConsignmentEntry.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Consignment_Entry'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuConsignmentReport.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Consignment_Report'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuViewConsignment.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'View_Consignment'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuDispatchReport.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Dispatch_Report'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVajraLock1.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Vajra_Lock'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVehicleSize1.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Vehicle_Size'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuBNGExport.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'BNG_Export'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVehicleStatus.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'VehicleStatus'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuDriver.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Driver'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuTransporter.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Transporter'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuLCEntry.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'LCEntry'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuLCManagement.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'LC_Management'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuLCReport.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'LC_Report'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuVehicleDocMang.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'VehicleDoc'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuBranchwiseParty.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'BranchwiseParty'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuPOD.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'POD'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuLRManagementMBT.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'LrManagementMBT'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuConsignmentReportMBT.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Consignment_Report_MBT'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuViewConsignmentMBT.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'View_Consignment_MBT'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuDispatchReportMBT.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'Dispatch_Report_MBT'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    mnuInternalLR.Visible = ((Convert.ToInt32((dt.Select("MenuName = 'InternalLR'").CopyToDataTable()).Rows[0]["List"])) == 1 ? true : false);
                    if (mnuDiesel.Visible == true && mnuCashData.Visible == true)
                    {
                        mnuDieselCashReport.Visible = true;
                    }
                    else
                    {
                        mnuDieselCashReport.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        private void FillUserData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_User";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    UserImage.ImageUrl = "../App_Content/User/" + dt.Rows[0]["Picture"].ToString();
                    lblUserName1.Text = dt.Rows[0]["Name"].ToString();
                    lblBranch.Text = "Branch : " + dt.Rows[0]["BranchName"].ToString();
                    lblUserName2.Text = dt.Rows[0]["Name"].ToString();
                    if ((Convert.ToInt32(hfUnderMaintenance.Value) == 1) && (dt.Rows[0]["UserType"].ToString() != "A"))
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["userpassword"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Database_"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect("Default.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        #endregion

        #region[event]
        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["userpassword"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Database_"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("Default.aspx", false);
        }

        protected void lbtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx?Id=" + Session["UserId"].ToString(), false);
        }
        #endregion
    }
}