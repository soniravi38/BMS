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

namespace Consignment.ConsignmentPanel
{
    public partial class AddTransporter : System.Web.UI.Page
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
                if (Request.QueryString["id"] != null)
                {
                    lblHeading.Text = "Update Transporter";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add Transporter";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [all-fuctions]

        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Transporter";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTransporterrName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                    txtPAN.Text = Convert.ToString(dt.Rows[0]["PAN"]);
                    txtMobile.Text = Convert.ToString(dt.Rows[0]["ContactNo"]);
                    txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtTransporterrName.Text = "";
            txtPAN.Text = "";
            txtMobile.Text = "";
            txtAddress.Text = "";
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            string spName = "[Proc_Transporter]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@Name"); ParameterValue.Add(txtTransporterrName.Text);
                    ParameterName.Add("@ContactNo"); ParameterValue.Add(txtMobile.Text);
                    ParameterName.Add("@PAN"); ParameterValue.Add(txtPAN.Text);
                    ParameterName.Add("@Address"); ParameterValue.Add(txtAddress.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Transporter", "Inserted", txtTransporterrName.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListTransporter.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! something went wrong')</script>");
                        clear();
                    }
                    #endregion
                }
                else
                {
                    #region[Update]
                    ParameterName.Add("@Action"); ParameterValue.Add("Update");
                    ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["Id"]));
                    ParameterName.Add("@Name"); ParameterValue.Add(txtTransporterrName.Text);
                    ParameterName.Add("@ContactNo"); ParameterValue.Add(txtMobile.Text);
                    ParameterName.Add("@PAN"); ParameterValue.Add(txtPAN.Text);
                    ParameterName.Add("@Address"); ParameterValue.Add(txtAddress.Text);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("Transporter", "Updated", txtTransporterrName.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListTransporter.aspx';", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! something went wrong')</script>");
                        clear();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            #endregion
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        #endregion
    }
}