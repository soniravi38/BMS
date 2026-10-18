using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Consignment.Model;
using System.IO;

namespace Consignment.ConsignmentPanel
{
    public partial class VehicleDocManagement : System.Web.UI.Page
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
                if (Session["UserType"].ToString() != "A")
                {
                    ValidateUserRights("VehicleDoc", 6);
                }

                FillVehicle();
                BindGrid();
                LoadCounts();
            }
        }

        private void FillVehicle()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();

            string spName = "Proc_Vehicle";

            ParameterName.Add("@Action"); ParameterValue.Add("ListOwn");

            ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);

            ddlVehicle.DataSource = ds.Tables[0];
            ddlVehicle.DataTextField = "VehicleNo";
            ddlVehicle.DataValueField = "Id";
            ddlVehicle.DataBind();

            ddlVehicle.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));
        }

        private void BindGrid()
        {

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();

            string spName = "Proc_VehicleDocMang";

            ParameterName.Add("@Action"); ParameterValue.Add("GetAll");

            if (ddlVehicle.SelectedValue != "0")
            {
                ParameterName.Add("@VehicleId"); ParameterValue.Add(ddlVehicle.SelectedValue);
            }
            else
            {
                ParameterName.Add("@VehicleId"); ParameterValue.Add(0);
            }

            if (ddlApprovalStatus.SelectedValue != "4")
            {
                ParameterName.Add("@ApprovalFilter"); ParameterValue.Add(ddlApprovalStatus.SelectedValue);
            }
            else
            {
                ParameterName.Add("@ApprovalFilter"); ParameterValue.Add(4);
            }

            if (ddlDocumentStatus.SelectedValue != "4")
            {
                ParameterName.Add("@DocumentFilter"); ParameterValue.Add(ddlDocumentStatus.SelectedValue);
            }
            else
            {
                ParameterName.Add("@DocumentFilter"); ParameterValue.Add(4);
            }

            ParameterName.Add("@DocumentType"); ParameterValue.Add(ddlDocumentType.SelectedValue);
            ParameterName.Add("@ExpiryDate"); ParameterValue.Add(txtExpiryDate.Text);
            ParameterName.Add("@RenewalDate"); ParameterValue.Add("");

            ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);

            gvVehicles.DataSource = ds.Tables[0];
            gvVehicles.DataBind();
            ViewState["dtExport"] = ds.Tables[0];
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
                        
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["UPDATE"])) == 1 ? true : false) == false)
                    {
                        gvVehicles.Columns[index].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["DELETE"])) == 1 ? true : false) == false)
                    {
                        gvVehicles.Columns[index + 2].Visible = false;
                    }
                    if (((Convert.ToInt32((dt.Select("MenuName = '" + Module + "'").CopyToDataTable()).Rows[0]["ACTIVE"])) == 1 ? true : false) == false)
                    {
                        gvVehicles.Columns[index + 1].Visible = false;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlVehicle.SelectedIndex = 0;
            ddlDocumentType.SelectedIndex = 0;
            txtExpiryDate.Text = "";

            BindGrid();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddVehicleDoc.aspx");
        }
        private void LoadCounts()
        {

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();

            string spName = "Proc_VehicleDocMang";

            try
            {

                ParameterName.Add("@Action"); ParameterValue.Add("DashboardCounts");

                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    lblTotalVehicles.Text = dt.Rows[0]["TotalVehicles"].ToString();

                    lblPendingApproval.Text = dt.Rows[0]["PendingApprovals"].ToString();
                    lblExpiredDocs.Text = dt.Rows[0]["ExpiredDocs"].ToString();
                    lblExpire7.Text = dt.Rows[0]["ExpireIn7Days"].ToString();
                    lblExpire15.Text = dt.Rows[0]["ExpireIn15Days"].ToString();
                    lblExpire30.Text = dt.Rows[0]["ExpireIn30Days"].ToString();

                }

            }
            catch (Exception ex)
            {

                cm.WriteLog(DateTime.Now.ToString() + " | Page:" +
                Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) +
                " | Method:" + System.Reflection.MethodBase.GetCurrentMethod().Name +
                " | Err:" + ex.Message);

            }

        }
        protected void gvVehicles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "UpdateDoc")
            {
                Response.Redirect("AddVehicleDoc.aspx?id=" + id);
            }

            if (e.CommandName == "Approve")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@Action"); ParameterValue.Add("Approved");
                ParameterName.Add("@Id"); ParameterValue.Add(id);
                ParameterName.Add("@ApprovalStatus"); ParameterValue.Add(1);

                con.ExecuteQueryByStoreProcedureWithParameter("Proc_VehicleDocMang", ParameterName, ParameterValue);

                BindGrid();
                LoadCounts();   // IMPORTANT
            }

            if (e.CommandName == "Reject")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@Action"); ParameterValue.Add("Approved");
                ParameterName.Add("@Id"); ParameterValue.Add(id);
                ParameterName.Add("@ApprovalStatus"); ParameterValue.Add(2);

                con.ExecuteQueryByStoreProcedureWithParameter("Proc_VehicleDocMang", ParameterName, ParameterValue);

                BindGrid();
                LoadCounts();   // IMPORTANT
            }

            if (e.CommandName == "DeleteDoc")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                ParameterName.Add("@Action"); ParameterValue.Add("Delete");
                ParameterName.Add("@Id"); ParameterValue.Add(id);

                con.ExecuteQueryByStoreProcedureWithParameter("Proc_VehicleDocMang", ParameterName, ParameterValue);

                BindGrid();
                LoadCounts();
            }
        }

        protected void gvVehicles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehicles.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("VehicleId");
                dtTemp.Columns.Remove("CreatedBy");
                dtTemp.Columns.Remove("UpdatedBy");
                dtTemp.Columns.Remove("CreatedDate");
                dtTemp.Columns.Remove("UpdatedDate");
                cm.ExportToExcel(dtTemp, "VehicleDocument");
            }
        }
        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtTemp = (DataTable)ViewState["dtExport"];
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.Columns.Remove("Id");
                dtTemp.Columns.Remove("VehicleId");
                dtTemp.Columns.Remove("CreatedBy");
                dtTemp.Columns.Remove("UpdatedBy");
                dtTemp.Columns.Remove("CreatedDate");
                dtTemp.Columns.Remove("UpdatedDate");
                cm.ExportToPdf(dtTemp, "VehicleDocument");
            }
        }

    }
}