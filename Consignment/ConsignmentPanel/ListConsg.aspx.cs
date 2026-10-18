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
    public partial class ListConsg : System.Web.UI.Page
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
                FillState();
                FillStation(); 
                fillList();
                if (Session["UserType"].ToString() != "A")
                {
                    ValidateUserRights("Party", 13);
                }
                GridViewSortDirection = SortDirection.Descending;
                lblHeading.Text = "List Party";
                lblInsert1.Text = "Add Party";
                lblInsert2.Text = "Add Party";
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
        private void FillConsg()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Consign";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAllByMode");
                ParameterName.Add("@ConsgMode"); ParameterValue.Add(ddlMode.SelectedValue);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlConsg.DataSource = dt;
                    ddlConsg.DataValueField = "Id";
                    ddlConsg.DataTextField = "Consgname";
                    ddlConsg.DataBind();
                    ddlConsg.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlConsg.Items.Clear();
                    ddlConsg.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillState()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_State";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataValueField = "Id";
                    ddlState.DataTextField = "Name";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
                }
                else
                {
                    ddlState.Items.Clear();
                    ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillStation()
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
                    ddlSource.DataSource = dt;
                    ddlSource.DataValueField = "Id";
                    ddlSource.DataTextField = "Name";
                    ddlSource.DataBind();
                    ddlSource.Items.Insert(0, new ListItem("--Select Source--", "0"));
                }
                else
                {
                    ddlSource.Items.Clear();
                    ddlSource.Items.Insert(0, new ListItem("--Select Source--", "0"));
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
            string spName = "[Proc_Consign]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ParameterName.Add("@Id"); ParameterValue.Add(ddlConsg.SelectedValue);
                if(Convert.ToInt32(ddlState.SelectedValue) > 0)
                {
                    ParameterName.Add("@ConsState"); ParameterValue.Add(ddlState.SelectedItem.Text.ToString());
                }
                ParameterName.Add("@ConsSource"); ParameterValue.Add(ddlSource.SelectedValue);
                ParameterName.Add("@ConsType"); ParameterValue.Add(ddlType.SelectedValue);
                ParameterName.Add("@isActive"); ParameterValue.Add(ddlActive.SelectedValue);
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
            
            string spName = "[Proc_Consign]";
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
                    int intResult = cm.SaveActivityLog("Party", "Deleted", Convert.ToString(dtOperation.Rows[0]["Consgname"]));
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
                    int intResult = cm.SaveActivityLog("Party", "Activated/Deactivated", Convert.ToString(dtOperation.Rows[0]["Consgname"]));
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillList();
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
                dtTemp.Columns.Remove("ConsSource");
                cm.ExportToExcel(dtTemp, "Consignor-Consignee");
            }
        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMode.SelectedValue.ToString() != "")
            {
                FillConsg();
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
                dtTemp.Columns.Remove("ConsSource");
                cm.ExportToPdf(dtTemp, "Consignor-Consignee");
            }
        }
        #endregion
    }
}