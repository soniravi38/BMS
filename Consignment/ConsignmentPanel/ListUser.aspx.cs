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

namespace Consignment.ConsignmentPanel
{
    public partial class ListUser : System.Web.UI.Page
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
                fillList();
                FillUser();
                FillBranch();
                GridViewSortDirection = SortDirection.Descending;
                lblHeading.Text = "List User";
                lblInsert1.Text = "Add User";
                lblInsert2.Text = "Add User";
            }
        }
        #endregion

        #region [All-Function]
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
        private void FillUser()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_User";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("ListSearch");
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

        private void fillList()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_User]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ParameterName.Add("@Id"); ParameterValue.Add(ddlUser.SelectedValue);
                ParameterName.Add("@BranchId"); ParameterValue.Add(ddlBranch.SelectedValue);
                ParameterName.Add("@UserType"); ParameterValue.Add(ddlUserType.SelectedValue);
                ParameterName.Add("@isActive"); ParameterValue.Add(ddlActive.SelectedValue);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                gvReport.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                gvReport.DataSource = dt;
                gvReport.DataBind();
                litrecordcount.Text = dt.Rows.Count.ToString();
                ViewState["dtExport"] = dt;

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
            
            string spName = "[Proc_User]";
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
                    int intResult = cm.SaveActivityLog("User", "Deleted", Convert.ToString(dtOperation.Rows[0]["Name"]));
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
                    int intResult = cm.SaveActivityLog("User", "Activated/Decativated", Convert.ToString(dtOperation.Rows[0]["Name"]));
                    fillList();
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
        #endregion

        #region [ddlPaging]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillList();
        }
        #endregion

        #region[Events]
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("isDelete");
                dtTemp.Columns.Remove("Created_Date");
                dtTemp.Columns.Remove("Modified_Date");
                dtTemp.Columns.Remove("BranchId");
                dtTemp.Columns.Remove("Picture");
                cm.ExportToExcel(dtTemp, "User");
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
                dtTemp.Columns.Remove("BranchId");
                dtTemp.Columns.Remove("Picture");
                cm.ExportToPdf(dtTemp, "User");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillList();
        }
        #endregion
    }
}