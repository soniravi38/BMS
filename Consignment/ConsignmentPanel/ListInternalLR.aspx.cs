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
    public partial class ListInternalLR : System.Web.UI.Page
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
            if (Session["UserId"] == null || Session["Database_"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                if (Session["UserType"].ToString() != "A")
                {
                    ValidateUserRights("InternalLR", 18);
                }
                GridViewSortDirection = SortDirection.Descending;
                lblHeading.Text = "List Internal LR / Settlement";
                lblInsert1.Text = "Create Internal LR";
                lblInsert2.Text = "Create Internal LR";
                txtDateFrom.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                fillList();
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
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["UPDATE"])) == 1 ? true : false) == false)
                    {
                        gvReport.Columns[index].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["DELETE"])) == 1 ? true : false) == false)
                    {
                        gvReport.Columns[index + 1].Visible = false;
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
        private void fillList()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_InternalLR]";
            try
            {
                DataTable dtUser = cm.GetUserDetails();
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ParameterName.Add("@UserType"); ParameterValue.Add(dtUser.Rows[0]["UserType"].ToString());
                ParameterName.Add("@BranchId"); ParameterValue.Add(dtUser.Rows[0]["BranchId"].ToString());
                ParameterName.Add("@FromDate"); ParameterValue.Add(txtDateFrom.Text);
                ParameterName.Add("@ToDate"); ParameterValue.Add(txtDateTo.Text);
                ParameterName.Add("@BookLrNo"); ParameterValue.Add(txtLRNo.Text.Trim());
                ParameterName.Add("@SearchCNNo"); ParameterValue.Add(txtCNNo.Text.Trim());
                ParameterName.Add("@SearchVehicleNo"); ParameterValue.Add(txtVehicleNo.Text.Trim());
                ParameterName.Add("@SearchBillStatus"); ParameterValue.Add(ddlBillStatus.SelectedValue);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    gvReport.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                    litrecordcount.Text = dt.Rows.Count.ToString();
                    ViewState["dtExport"] = dt;
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
            try
            {
                if (e.CommandName == "EditRecord")
                {
                    Response.Redirect("AddInternalLR.aspx?Id=" + e.CommandArgument.ToString(), false);
                }
                if (e.CommandName == "isDelete")
                {
                    // A record with a Final Bill can be deleted only by an Administrator
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    string spName = "[Proc_InternalLR]";
                    ParameterName.Add("@Action"); ParameterValue.Add("Get");
                    ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["BillStatus"].ToString() == "F" && Session["UserType"].ToString() != "A")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('A record with a Final Bill can be deleted only by an Administrator.');", true);
                            return;
                        }
                        string CNNo = ds.Tables[0].Rows[0]["CNNo"].ToString();

                        ParameterName = new ArrayList();
                        ParameterValue = new ArrayList();
                        ParameterName.Add("@Action"); ParameterValue.Add("isDelete");
                        ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                        ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                        con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                        int IntResult = cm.SaveActivityLog("Internal LR", "Deleted", "CN No = " + CNNo);
                    }
                    fillList();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP: | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
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
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP: | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvReport.PageIndex = 0;
            fillList();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                cm.ExportToExcel(dtTemp, "InternalLR");
            }
        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                cm.ExportToPdf(dtTemp, "InternalLR");
            }
        }
        #endregion
    }
}
