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
    public partial class ViewLRBook : System.Web.UI.Page
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
                if (Session["UserType"].ToString() != "A")
                {
                    ValidateUserRights("View_Consignment", 11);
                }
                GridViewSortDirection = SortDirection.Descending;
                lblHeading.Text = "List LR";
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
            string spName = "[Proc_ViewConsignment]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("ViewConsignment");
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ParameterName.Add("@BookBranchId"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["BranchId"]).ToString());

                ParameterName.Add("@FromDate"); ParameterValue.Add(txtDateFrom.Text);
                ParameterName.Add("@ToDate"); ParameterValue.Add(txtDateTo.Text);
                ParameterName.Add("@LrFrom"); ParameterValue.Add(txtLRFrom.Text);
                ParameterName.Add("@LrTo"); ParameterValue.Add(txtLRTo.Text);
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
                if (e.CommandName == "Edit")
                {
                    Response.Redirect("AddBookLR.aspx?BookId=" + e.CommandArgument.ToString(), true);
                }
                if (e.CommandName == "isDelete")
                {
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    string spName = "[Proc_BookLR]";
                    ParameterName.Add("@bookid"); ParameterValue.Add(Convert.ToInt32(e.CommandArgument));
                    ParameterName.Add("@Action"); ParameterValue.Add("Delete");
                    dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    int IntResult = cm.SaveActivityLog("LR Entry", "Deleted", "BookId = " + (e.CommandArgument).ToString());
                    fillList();
                }
            }
            catch(Exception ex)
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
            fillList();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
              
                cm.ExportToExcel(dtTemp, "LR");
            }
        }

        

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
               
                cm.ExportToPdf(dtTemp, "LR");
            }
        }
        #endregion
    }
}