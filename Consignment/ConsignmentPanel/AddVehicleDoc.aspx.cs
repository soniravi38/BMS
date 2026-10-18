using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Consignment.Model;
using System.IO;

namespace Consignment.ConsignmentPanel
{
    public partial class AddVehicleDoc : System.Web.UI.Page
    {

        #region [properties]

        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();

        #endregion


        #region [PageLoad]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillVehicle();

                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update Vehicle Document";
                    btnSubmit.Text = "Update";
                    ddlVehicle.Enabled = false;
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Vehicle Document";
                    btnSubmit.Text = "Add";
                }
            }
        }

        #endregion


        #region [Functions]

        private void FillVehicle()
        {

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();

            string spName = "Proc_Vehicle";

            try
            {

                ParameterName.Add("@Action"); ParameterValue.Add("ListOwn");

                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {

                    ddlVehicle.DataSource = dt;
                    ddlVehicle.DataTextField = "VehicleNo";
                    ddlVehicle.DataValueField = "Id";
                    ddlVehicle.DataBind();

                    ddlVehicle.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

                }
                else
                {

                    ddlVehicle.Items.Clear();
                    ddlVehicle.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

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


        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();

            string spName = "Proc_VehicleDocMang";

            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetById");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);

                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ddlVehicle.SelectedValue = dt.Rows[0]["VehicleId"].ToString();
                    ddlDocumentType.SelectedValue = dt.Rows[0]["DocumentType"].ToString();
                    txtPolicyNo.Text = dt.Rows[0]["InsPolicyNo"].ToString();
                    txtCompany.Text = dt.Rows[0]["InsCompany"].ToString();
                    txtRegistrationDate.Text = dt.Rows[0]["RegistrationDate"].ToString(); 
                    txtExpiryDate.Text = dt.Rows[0]["ExpiryDate"].ToString();
                    txtRenewalDate.Text = dt.Rows[0]["RenewalDate"].ToString();
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


        private void Clear()
        {

            ddlVehicle.SelectedIndex = 0;
            ddlDocumentType.SelectedIndex = 0;
            txtExpiryDate.Text = "";
            txtRegistrationDate.Text = "";
            txtRenewalDate.Text = "";
            txtPolicyNo.Text = "";
            txtCompany.Text = "";
        }

        #endregion


        #region [Events]

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string spName = "Proc_VehicleDocMang";

            try
            {

                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();

                if (Request.QueryString["id"] == null)
                {

                    #region INSERT

                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@VehicleId"); ParameterValue.Add(ddlVehicle.SelectedValue);
                    ParameterName.Add("@DocumentType"); ParameterValue.Add(ddlDocumentType.SelectedItem.Text);
                    ParameterName.Add("@ExpiryDate"); ParameterValue.Add(txtExpiryDate.Text);
                    ParameterName.Add("@RenewalDate"); ParameterValue.Add(txtRenewalDate.Text);
                    ParameterName.Add("@RegistrationDate"); ParameterValue.Add(txtRegistrationDate.Text);
                    ParameterName.Add("@InsPolicyNo"); ParameterValue.Add(txtPolicyNo.Text);
                    ParameterName.Add("@InsCompany"); ParameterValue.Add(txtCompany.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));

                    int result = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);

                    if (result > 0)
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Document Added Successfully');window.location='VehicleDocManagement.aspx';", true);

                    }


                    #endregion

                }
                else
                {

                    #region UPDATE

                    ParameterName.Add("@Action"); ParameterValue.Add("Update");
                    ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["id"]));
                    ParameterName.Add("@VehicleId"); ParameterValue.Add(ddlVehicle.SelectedValue);
                    ParameterName.Add("@DocumentType"); ParameterValue.Add(ddlDocumentType.SelectedItem.Text);
                    ParameterName.Add("@ExpiryDate"); ParameterValue.Add(txtExpiryDate.Text);
                    ParameterName.Add("@RenewalDate"); ParameterValue.Add(txtRenewalDate.Text);
                    ParameterName.Add("@RegistrationDate"); ParameterValue.Add(txtRegistrationDate.Text);
                    ParameterName.Add("@InsPolicyNo"); ParameterValue.Add(txtPolicyNo.Text);
                    ParameterName.Add("@InsCompany"); ParameterValue.Add(txtCompany.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));

                    int result = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);

                    if (result > 0)
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Document Updated Successfully');window.location='VehicleDocManagement.aspx';", true);

                    }

                    #endregion

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


        protected void btnClear_Click(object sender, EventArgs e)
        {

            Clear();

        }

        #endregion

    }
}