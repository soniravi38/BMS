using Consignment.Model;
using System;
using System.Collections;
using System.Data;

namespace Consignment.ConsignmentPanel
{
    public partial class AddPOD : System.Web.UI.Page
    {
        ArrayList p, v;
        connect con = new connect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update POD";
                    btnSubmit.Text = "Update";
                    LoadData();
                }
                else
                {
                    lblHeading.Text = "Add POD";
                }

                ManageControlsByUserType();
            }
        }

        void ManageControlsByUserType()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "U")
            {
                // Disable all controls except Remarks Branch

                txtLRDate.Enabled = false;
                txtServiceMonth.Enabled = false;
                txtLRNo.Enabled = false;
                txtConsignorName.Enabled = false;
                txtOrigin.Enabled = false;
                txtDestination.Enabled = false;
                txtVehicleNo.Enabled = false;
                txtPackageCount.Enabled = false;
                txtVehicleOwner.Enabled = false;
                txtVehicleType.Enabled = false;
                txtExceptionalFlag.Enabled = false;
                txtConcernedBranch.Enabled = false;

                ddlAckStatus.Enabled = false;
               
                txtRemarksHO.Enabled = false;
                txtRecDateHO.Enabled = false;

                // Only this will remain enabled
                txtRemarksBranch.Enabled = true;
                txtSentByBranchDate.Enabled = true;
                txtSentThrough.Enabled = true;
            }
        }

        void LoadData()
        {
            p = new ArrayList();
            v = new ArrayList();

            p.Add("@Action"); v.Add("GetById");
            p.Add("@Id"); v.Add(Request.QueryString["id"]);

            DataTable dt = con.GetDataTableFromSP("Proc_POD", p, v);

            if (dt.Rows.Count > 0)
            {
                txtLRDate.Text = Convert.ToDateTime(dt.Rows[0]["LRDate"]).ToString("yyyy-MM-dd");
                txtServiceMonth.Text = dt.Rows[0]["ServiceMonth"].ToString();
                txtLRNo.Text = dt.Rows[0]["LRNo"].ToString();
                txtConsignorName.Text = dt.Rows[0]["ConsignorName"].ToString();
                txtOrigin.Text = dt.Rows[0]["Origin"].ToString();
                txtDestination.Text = dt.Rows[0]["Destination"].ToString();
                txtVehicleNo.Text = dt.Rows[0]["VehicleNo"].ToString();
                txtPackageCount.Text = dt.Rows[0]["PackageCount"].ToString();
                txtVehicleOwner.Text = dt.Rows[0]["VehicleOwner"].ToString();
                txtVehicleType.Text = dt.Rows[0]["VehicleType"].ToString();
                txtExceptionalFlag.Text = dt.Rows[0]["ExceptionalFlag"].ToString();
                txtConcernedBranch.Text = dt.Rows[0]["ConcernedBranch"].ToString();

                ddlAckStatus.SelectedValue = dt.Rows[0]["AckStatus"].ToString();
                if (dt.Rows[0]["SentByBranchDate"] != DBNull.Value)
                {
                    txtSentByBranchDate.Text =
                        Convert.ToDateTime(dt.Rows[0]["SentByBranchDate"])
                        .ToString("yyyy-MM-dd");
                }
                txtSentThrough.Text = dt.Rows[0]["SentThrough"].ToString();
                txtRemarksBranch.Text = dt.Rows[0]["RemarksBranch"].ToString();
                txtRemarksHO.Text = dt.Rows[0]["RemarksHO"].ToString();
                if (dt.Rows[0]["RecDateHO"] != DBNull.Value)
                {
                    txtRecDateHO.Text =
                        Convert.ToDateTime(dt.Rows[0]["RecDateHO"])
                        .ToString("yyyy-MM-dd");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            p = new ArrayList();
            v = new ArrayList();

            string action = Request.QueryString["id"] != null ? "Update" : "Insert";

            p.Add("@Action"); v.Add(action);
            p.Add("@Id"); v.Add(Request.QueryString["id"]);

            p.Add("@LRDate"); v.Add(txtLRDate.Text);
            p.Add("@ServiceMonth"); v.Add(txtServiceMonth.Text);
            p.Add("@LRNo"); v.Add(txtLRNo.Text);
            p.Add("@ConsignorName"); v.Add(txtConsignorName.Text);
            p.Add("@Origin"); v.Add(txtOrigin.Text);
            p.Add("@Destination"); v.Add(txtDestination.Text);
            p.Add("@VehicleNo"); v.Add(txtVehicleNo.Text);
            p.Add("@PackageCount"); v.Add(txtPackageCount.Text);
            p.Add("@VehicleOwner"); v.Add(txtVehicleOwner.Text);
            p.Add("@VehicleType"); v.Add(txtVehicleType.Text);
            p.Add("@ExceptionalFlag"); v.Add(txtExceptionalFlag.Text);
            p.Add("@ConcernedBranch"); v.Add(txtConcernedBranch.Text);

            p.Add("@AckStatus"); v.Add(ddlAckStatus.SelectedValue);
            p.Add("@SentByBranchDate"); v.Add(txtSentByBranchDate.Text);
            p.Add("@SentThrough"); v.Add(txtSentThrough.Text);
            p.Add("@RemarksBranch"); v.Add(txtRemarksBranch.Text);
            p.Add("@RemarksHO"); v.Add(txtRemarksHO.Text);
            p.Add("@RecDateHO"); v.Add(txtRecDateHO.Text);

            p.Add("@UserId"); v.Add(Session["UserId"]);

            con.GetDataTableFromSP("Proc_POD", p, v);

            Response.Write("<script>alert('Updated Successfully');window.location='ListPOD.aspx';</script>");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPOD.aspx");
        }
    }
}