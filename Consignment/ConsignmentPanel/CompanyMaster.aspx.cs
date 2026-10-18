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
    public partial class CompanyMaster : System.Web.UI.Page
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
                lblHeading.Text = "Company Master";
                btnSubmit.Text = "Update";
                FillData();
            }
        }
        #endregion

        #region [all-fuctions]
        private string uploadCompanyLogo()
        {
            if (fuCompanyLogo.HasFile == true)
            {
                if (fuCompanyLogo.PostedFile.FileName != "")
                {
                    string spath = Server.MapPath("../App_Content/Company/");
                    string Extension = System.IO.Path.GetExtension(fuCompanyLogo.FileName).ToLower();
                    if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                    {
                        string FileName = DateTime.Now.Ticks + fuCompanyLogo.FileName.ToString();
                        string fullpath = Convert.ToString(spath + FileName);
                        fuCompanyLogo.PostedFile.SaveAs(fullpath);
                        return FileName;
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! Please Select Valid Image!')</script>");
                    }
                }
            }
            return " ";
        }

        private string uploadCompanyShortLogo()
        {
            if (fuCompanyShortLogo.HasFile == true)
            {
                if (fuCompanyShortLogo.PostedFile.FileName != "")
                {
                    string spath = Server.MapPath("../App_Content/Company/");
                    string Extension = System.IO.Path.GetExtension(fuCompanyShortLogo.FileName).ToLower();
                    if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                    {
                        string FileName = DateTime.Now.Ticks + fuCompanyShortLogo.FileName.ToString();
                        string fullpath = Convert.ToString(spath + FileName);
                        fuCompanyShortLogo.PostedFile.SaveAs(fullpath);
                        return FileName;
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! Please Select Valid Image!')</script>");
                    }
                }
            }
            return " ";
        }
        private void FillData()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_CompanyMaster";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtCompanyName.Text = Convert.ToString(dt.Rows[0]["CompanyName"]);
                    txtOwnerName.Text = Convert.ToString(dt.Rows[0]["OwnerName"]);
                    txtGSTIN.Text = Convert.ToString(dt.Rows[0]["GSTIN"]);
                    txtCIN.Text = Convert.ToString(dt.Rows[0]["CIN"]);
                    txtFSSAI.Text = Convert.ToString(dt.Rows[0]["FSSAI"]);
                    txtPAN.Text = Convert.ToString(dt.Rows[0]["PAN"]);
                    txtMobile1.Text = Convert.ToString(dt.Rows[0]["Mobile1"]);
                    txtMobile2.Text = Convert.ToString(dt.Rows[0]["Mobile2"]);
                    txtEmail1.Text = Convert.ToString(dt.Rows[0]["Email1"]);
                    txtEmail2.Text = Convert.ToString(dt.Rows[0]["Email2"]);
                    txtWebsite.Text = Convert.ToString(dt.Rows[0]["Website"]);
                    txtAddress1.Text = Convert.ToString(dt.Rows[0]["Address1"]);
                    txtAddress2.Text = Convert.ToString(dt.Rows[0]["Address2"]);
                    txtGoogleMapAPI.Text = Convert.ToString(dt.Rows[0]["GoogleMapApi"]);
                    txtWhatsappMobile.Text = Convert.ToString(dt.Rows[0]["WhatsappMobile"]);
                    txtSMSMobile.Text = Convert.ToString(dt.Rows[0]["SMSMobile"]);
                    txtSMSApi.Text = Convert.ToString(dt.Rows[0]["SMSApi"]);
                    txtFacebook.Text = Convert.ToString(dt.Rows[0]["Facebook"]);
                    txtInstagram.Text = Convert.ToString(dt.Rows[0]["Instagram"]);
                    txtTwitter.Text = Convert.ToString(dt.Rows[0]["Twitter"]);
                    txtGooglePlus.Text = Convert.ToString(dt.Rows[0]["GooglePlus"]);
                    txtLinkdin.Text = Convert.ToString(dt.Rows[0]["Linkdin"]);
                    txtFooter.Text = Convert.ToString(dt.Rows[0]["FooterText"]);
                    txtPhone1.Text = Convert.ToString(dt.Rows[0]["Phone1"]);
                    txtPhone2.Text = Convert.ToString(dt.Rows[0]["Phone2"]);
                    txtFax.Text = Convert.ToString(dt.Rows[0]["Fax"]);
                    ddlSMSServiceEnabled.SelectedValue = dt.Rows[0]["isSMSServiceEnabled"].ToString();
                    ddlUnderMaintenance.SelectedValue = dt.Rows[0]["isUnderMaintenance"].ToString();
                    txtImageStr.Text = Convert.ToString(dt.Rows[0]["ImageStr"]);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCompanyName.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtFacebook.Text = "";
            txtFooter.Text = "";
            txtGoogleMapAPI.Text = "";
            txtGooglePlus.Text = "";
            txtGSTIN.Text = "";
            txtCIN.Text = "";
            txtFSSAI.Text = "";
            txtInstagram.Text = "";
            txtLinkdin.Text = "";
            txtMobile1.Text = "";
            txtMobile2.Text = "";
            txtOwnerName.Text = "";
            txtPAN.Text = "";
            txtSMSApi.Text = "";
            txtSMSMobile.Text = "";
            txtTwitter.Text = "";
            txtWebsite.Text = "";
            txtWhatsappMobile.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtFax.Text = "";
            ddlSMSServiceEnabled.SelectedIndex = 0;
            ddlUnderMaintenance.SelectedIndex = 0;
            txtImageStr.Text = "";
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            Int32 intResult = 0;
            string spName = "[Proc_CompanyMaster]";
            try
            {
                string CheckFolder = cm.FolderCheck("Company");
                string CompanyLogo = uploadCompanyLogo();
                string CompanyShortLogo = uploadCompanyShortLogo();
                ParameterName.Add("@Action"); ParameterValue.Add("Update");
                ParameterName.Add("@CompanyName"); ParameterValue.Add(txtCompanyName.Text);
                ParameterName.Add("@OwnerName"); ParameterValue.Add(txtOwnerName.Text);
                ParameterName.Add("@CompanyLogo"); ParameterValue.Add(CompanyLogo);
                ParameterName.Add("@CompanyShortLogo"); ParameterValue.Add(CompanyShortLogo);
                ParameterName.Add("@GSTIN"); ParameterValue.Add(txtGSTIN.Text);
                ParameterName.Add("@CIN"); ParameterValue.Add(txtCIN.Text);
                ParameterName.Add("@FSSAI"); ParameterValue.Add(txtFSSAI.Text);
                ParameterName.Add("@PAN"); ParameterValue.Add(txtPAN.Text);
                ParameterName.Add("@Phone1"); ParameterValue.Add(txtPhone1.Text);
                ParameterName.Add("@Phone2"); ParameterValue.Add(txtPhone2.Text);
                ParameterName.Add("@Mobile1"); ParameterValue.Add(txtMobile1.Text);
                ParameterName.Add("@Mobile2"); ParameterValue.Add(txtMobile2.Text);
                ParameterName.Add("@Email1"); ParameterValue.Add(txtEmail1.Text);
                ParameterName.Add("@Email2"); ParameterValue.Add(txtEmail2.Text);
                ParameterName.Add("@Website"); ParameterValue.Add(txtWebsite.Text);
                ParameterName.Add("@Address1"); ParameterValue.Add(txtAddress1.Text);
                ParameterName.Add("@Address2"); ParameterValue.Add(txtAddress2.Text);
                ParameterName.Add("@GoogleMapApi"); ParameterValue.Add(txtGoogleMapAPI.Text);
                ParameterName.Add("@WhatsappMobile"); ParameterValue.Add(txtWhatsappMobile.Text);
                ParameterName.Add("@SMSMobile"); ParameterValue.Add(txtSMSMobile.Text);
                ParameterName.Add("@SMSApi"); ParameterValue.Add(txtSMSApi.Text);
                ParameterName.Add("@Facebook"); ParameterValue.Add(txtFacebook.Text);
                ParameterName.Add("@Instagram"); ParameterValue.Add(txtInstagram.Text);
                ParameterName.Add("@Twitter"); ParameterValue.Add(txtTwitter.Text);
                ParameterName.Add("@GooglePlus"); ParameterValue.Add(txtGooglePlus.Text);
                ParameterName.Add("@Linkdin"); ParameterValue.Add(txtLinkdin.Text);
                ParameterName.Add("@FooterText"); ParameterValue.Add(txtFooter.Text);
                ParameterName.Add("@isSMSServiceEnabled"); ParameterValue.Add(ddlSMSServiceEnabled.SelectedValue);
                ParameterName.Add("@isUnderMaintenance"); ParameterValue.Add(ddlUnderMaintenance.SelectedValue);
                ParameterName.Add("@ImageStr"); ParameterValue.Add(txtImageStr.Text);
                ParameterName.Add("@Fax"); ParameterValue.Add(txtFax.Text);
                intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                if (intResult > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='Dashboard.aspx';", true);
                }
                else
                {
                    Response.Write("<script>alert('Oops! something went wrong')</script>");
                    clear();
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