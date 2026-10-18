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
    public partial class DieselCashReport : System.Web.UI.Page
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
                //fillList();
                GetMsg();
                FillBranch();
                if (Session["UserType"].ToString() != "A")
                {
                    ValidateUserRights("Diesel", 17);
                    ValidateUserRights1("Cash_Data", 14);
                }
                GridViewSortDirection = SortDirection.Descending;
                lblHeading.Text = "List Diesel Data";
                lblInsert1.Text = "Add Diesel Data";

                lblHeading1.Text = "List Cash Data";
                lblInsertCash.Text = "Add Cash Data";
            }
        }
        #endregion

        #region [All-Function]
        private void ValidateUserRights(string Module, int index)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_UserRights";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetByUserId");
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["LIST"])) == 1 ? true : false) == false)
                    {
                        Response.Redirect("Dashboard.aspx", false);
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["CREATE"])) == 1 ? true : false) == false)
                    {
                        btnAdd.Visible = false;
                        btnAdd1.Visible = false;
                        btnAddBulk.Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["UPDATE"])) == 1 ? true : false) == false)
                    {
                        gvReport.Columns[index].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["DELETE"])) == 1 ? true : false) == false)
                    {
                        gvReport.Columns[index + 1].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["ACTIVE"])) == 1 ? true : false) == false)
                    {
                        gvReport.Columns[index + 2].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["EXPORT"])) == 1 ? true : false) == false)
                    {
                        imgBtnExcel.Visible = false;
                        imgBtnPdf.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }

        private void ValidateUserRights1(string Module, int index)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_UserRights";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetByUserId");
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["LIST"])) == 1 ? true : false) == false)
                    {
                        Response.Redirect("Dashboard.aspx", false);
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["CREATE"])) == 1 ? true : false) == false)
                    {
                        btnAdd1.Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["UPDATE"])) == 1 ? true : false) == false)
                    {
                        gvReport1.Columns[index].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["DELETE"])) == 1 ? true : false) == false)
                    {
                        gvReport1.Columns[index + 1].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["ACTIVE"])) == 1 ? true : false) == false)
                    {
                        gvReport1.Columns[index + 2].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["EXPORT"])) == 1 ? true : false) == false)
                    {
                        imgBtnExcel1.Visible = false;
                        imgBtnPdf1.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        private void fillList()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_DieselReconciliation]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAllforDieselCash");
                ParameterName.Add("@VechicleFullNo"); ParameterValue.Add(txtVehicleNo.Text);
                ParameterName.Add("@LrDateFrom"); ParameterValue.Add(txtDateFrom.Text);
                ParameterName.Add("@LrDateTo"); ParameterValue.Add(txtDateTo.Text);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    gvReport.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                    litrecordcount.Text = dt.Rows.Count.ToString();
                    ViewState["dtExport"] = dt;

                    lblDieselAMount.Text = ds.Tables[1].Rows[0]["Amount"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }

        }
        private void FillBranch()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Branch";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlBranch.DataSource = dt;
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                }
                else
                {
                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        private void fillListCash()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_CashReconciliation]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAllForDieselCash");
                ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                if (Convert.ToInt32(ddlBranch.SelectedIndex) > 0)
                {
                    ParameterName.Add("@PayingBranch"); ParameterValue.Add(ddlBranch.SelectedValue);
                }
                ParameterName.Add("@DateFrom"); ParameterValue.Add(txtDateFrom.Text);
                ParameterName.Add("@DateTo"); ParameterValue.Add(txtDateTo.Text);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    gvReport1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                    gvReport1.DataSource = dt;
                    gvReport1.DataBind();
                    litrecordcount1.Text = dt.Rows.Count.ToString();
                    ViewState["dtExport1"] = dt;

                    lblCashAmount.Text = ds.Tables[1].Rows[0]["Amount"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }

        }
        private void GetMsg()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_DieselReconciliation]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetMaxDate");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0)
                {
                    lblMsg.Text = "DIESEL DATA UPDATED FROM 01 DEC 2021 TO " + ds.Tables[0].Rows[0]["LrDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }

        }
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }
        #endregion

        #region [GridViewEvents]
        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string spName = "[Proc_DieselReconciliation]";
            DataTable dtOperation = new DataTable(); ;
            if (e.CommandName == "IsDelete" || e.CommandName == "IsActive")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                dtOperation = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
            }

            #region [IsDelete]
            if (e.CommandName == "IsDelete")
            {
                try
                {
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("IsDelete");
                    ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                    dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    int intResult = cm.SaveActivityLog("Diesel", "Deleted", Convert.ToString(dtOperation.Rows[0]["LrDate"]) + "|" + Convert.ToString(dtOperation.Rows[0]["VechicleFullNo"]));
                    fillList();

                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
            #endregion

            #region [IsActive]
            if (e.CommandName == "IsActive")
            {
                DataTable dt1 = new DataTable();
                try
                {
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("IsActive");
                    ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                    dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    int intResult = cm.SaveActivityLog("Diesel", "Activated/Deactivated", Convert.ToString(dtOperation.Rows[0]["LrDate"]) + "|" + Convert.ToString(dtOperation.Rows[0]["VechicleFullNo"]));
                    fillList();
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
            #endregion
        }

        protected void gvReport1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string spName = "[Proc_CashReconciliation]";
            DataTable dtOperation = new DataTable(); ;
            if (e.CommandName == "IsDelete" || e.CommandName == "IsActive")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                dtOperation = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
            }

            #region [IsDelete]
            if (e.CommandName == "IsDelete")
            {
                try
                {
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("IsDelete");
                    ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                    dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    int intResult = cm.SaveActivityLog("Cash Data", "Deleted", Convert.ToString(dtOperation.Rows[0]["Date"]) + "|" + Convert.ToString(dtOperation.Rows[0]["VehicleNo"]));

                    fillListCash();

                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
            #endregion

            #region [IsActive]
            if (e.CommandName == "IsActive")
            {
                DataTable dt1 = new DataTable();
                try
                {
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("IsActive");
                    ParameterName.Add("@id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                    dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    int intResult = cm.SaveActivityLog("Cash Data", "Activated/Deactivated", Convert.ToString(dtOperation.Rows[0]["Date"]) + "|" + Convert.ToString(dtOperation.Rows[0]["VehicleNo"]));
                    fillListCash();
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
            #endregion
        }
        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            fillList();
        }

        protected void gvReport1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport1.PageIndex = e.NewPageIndex;
            fillListCash();
        }
        protected void gvReport_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["dtExport"];
                DataView dv = new DataView(dt);
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    dv.Sort = e.SortExpression + " DESC";
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    dv.Sort = e.SortExpression + " ASC";
                }
                gvReport.DataSource = dv;
                gvReport.DataBind();
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        protected void gvReport1_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["dtExport1"];
                DataView dv = new DataView(dt);
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    dv.Sort = e.SortExpression + " DESC";
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    dv.Sort = e.SortExpression + " ASC";
                }
                gvReport1.DataSource = dv;
                gvReport1.DataBind();
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        #endregion

        #region [ddlPaging]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillList();
            fillListCash();
        }
        #endregion

        #region[Events]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillList();
            fillListCash();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("isDelete");
                dtTemp.Columns.Remove("Created_Date");
                dtTemp.Columns.Remove("Modified_Date");
                dtTemp.Columns.Remove("isActive");
                cm.ExportToExcel(dtTemp, "Diesel");
            }
        }

        protected void imgBtnExcel1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport1"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("isDelete");
                dtTemp.Columns.Remove("Created_Date");
                dtTemp.Columns.Remove("Modified_Date");
                dtTemp.Columns.Remove("isActive");
                cm.ExportToExcel(dtTemp, "Cash Data");
            }
        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("isDelete");
                dtTemp.Columns.Remove("Created_Date");
                dtTemp.Columns.Remove("Modified_Date");
                dtTemp.Columns.Remove("isActive");
                cm.ExportToPdf(dtTemp, "Diesel");
            }
        }

        protected void imgBtnPdf1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport1"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("isDelete");
                dtTemp.Columns.Remove("Created_Date");
                dtTemp.Columns.Remove("Modified_Date");
                dtTemp.Columns.Remove("isActive");
                cm.ExportToPdf(dtTemp, "Cash Data");
            }
        }
        #endregion
    }
}