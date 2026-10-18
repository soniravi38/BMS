using Consignment.Model;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace Consignment.ConsignmentPanel
{
    public partial class ListPOD : System.Web.UI.Page
    {
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillBranch();
                fillList();
                if (Session["UserType"].ToString() != "A")
                {
                    ValidateUserRights("POD", 14);
                }
                lblHeading.Text = "List POD";
                //lblInsert1.Text = "Add POD";
            }
        }
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
        void FillBranch()
        {
            try
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@Action");
                ParameterValue.Add("GetBranch");

                dt = con.GetDataTableFromSP("Proc_POD",
                    ParameterName,
                    ParameterValue);

                ddlConcernedBranch.DataSource = dt;
                ddlConcernedBranch.DataTextField = "ConcernedBranch";
                ddlConcernedBranch.DataValueField = "ConcernedBranch";
                ddlConcernedBranch.DataBind();

              
            }
            catch (Exception ex)
            {
                cm.WriteLog("Branch Fill Error : " + ex.Message);
            }
        }

        private void fillList()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();

            try
            {
                ParameterName.Add("@Action");
                ParameterValue.Add("GetAll");

                ParameterName.Add("@SearchLRNo");
                ParameterValue.Add(txtSearchLRNo.Text.Trim());

                ParameterName.Add("@SearchConcernedBranch");
                ParameterValue.Add(ddlConcernedBranch.SelectedValue);

                ds = con.GetDataSetFromSP("Proc_POD",
                    ParameterName,
                    ParameterValue);

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    gvReport.PageSize =
                        Convert.ToInt32(ddlPageSize.SelectedValue);

                    gvReport.DataSource = dt;
                    gvReport.DataBind();

                    litrecordcount.Text = dt.Rows.Count.ToString();

                    ViewState["dtExport"] = dt;
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog("POD List Error: " + ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillList();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchLRNo.Text = "";

            ddlConcernedBranch.SelectedIndex = 0;

            fillList();
        }

        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            fillList();
        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "IsDelete")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@Action");
                ParameterValue.Add("IsDelete");

                ParameterName.Add("@Id");
                ParameterValue.Add(e.CommandArgument);

                con.GetDataTableFromSP("Proc_POD",
                    ParameterName,
                    ParameterValue);

                cm.SaveActivityLog("POD",
                    "Deleted",
                    e.CommandArgument.ToString());

                fillList();
            }
        }

        protected void gvReport_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dtExport"];

            DataView dv = new DataView(dt);

            dv.Sort = e.SortExpression + " ASC";

            gvReport.DataSource = dv;
            gvReport.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillList();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];

            cm.ExportToExcel(dtTemp, "POD");
        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];

            cm.ExportToPdf(dtTemp, "POD");
        }
    }
}