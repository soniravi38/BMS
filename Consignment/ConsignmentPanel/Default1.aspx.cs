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
    public partial class Default : System.Web.UI.Page
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
                title.InnerText = "Consignment Login";
                dvLogin.Visible = true;
                dvOTP.Visible = false;
                CheckCookiesVar();
            }
        }
        #endregion

        #region[Function]
        public void CheckCookiesVar()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["userpassword"] != null && Request.Cookies["Database_"] != null)
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                string spName = "[Proc_User]";
                try
                {
                    Session["Database_"] = Request.Cookies["Database_"].Value.ToString();
                    ParameterName.Add("@Action"); ParameterValue.Add("Login");
                    ParameterName.Add("@Username"); ParameterValue.Add(Request.Cookies["username"].Value);
                    ParameterName.Add("@Password"); ParameterValue.Add(Request.Cookies["userpassword"].Value);
                    dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (dt.Rows.Count > 0)
                    {
                        Session["UserId"] = dt.Rows[0]["Id"].ToString();
                        Session["UserName"] = dt.Rows[0]["Name"].ToString();
                        Session["UserPicture"] = dt.Rows[0]["Picture"].ToString();
                        Session["UserMobile"] = dt.Rows[0]["Mobile"].ToString();
                        Session["UserEmail"] = dt.Rows[0]["Email"].ToString();
                        Session["UserType"] = dt.Rows[0]["UserType"].ToString();
                        Session["UserBranchId"] = dt.Rows[0]["BranchId"].ToString();
                        Response.Redirect("Dashboard.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:  | Err:" + ex.Message.ToString());
                }
            }
        }

        public void clear()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            ddlCompany.SelectedIndex = 0;
        }

        private void FillCompanyData()
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
                    title.InnerText = "Login - " + dt.Rows[0]["CompanyName"].ToString();
                    imgCompanyLogo.ImageUrl = "../App_Content/Company/" + dt.Rows[0]["CompanyLogo"].ToString();
                    hfUnderMaintenance.Value = dt.Rows[0]["isUnderMaintenance"].ToString();
                    if (Convert.ToInt32(dt.Rows[0]["isSMSServiceEnabled"]) == 1)
                    {
                        btnSend.Text = "SEND OTP";
                    }
                    else
                    {
                        btnSend.Text = "LOGIN";
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:  | Err:" + ex.Message.ToString());
            }
        }
        #endregion

        #region[Events]
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Database_"] = ddlCompany.SelectedValue.ToString();
            FillCompanyData();
        }

        protected void btnOTP_Click(object sender, EventArgs e)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_User]";
            try
            {
                if (Session["UserOTP"] != null)
                {
                    if (Session["UserOTP"].ToString() == txtOTP.Text.ToString())
                    {
                        ParameterName.Add("@Action"); ParameterValue.Add("Login");
                        ParameterName.Add("@Username"); ParameterValue.Add(Session["UserName"].ToString());
                        ParameterName.Add("@Password"); ParameterValue.Add(Session["UserPassword"].ToString());
                        dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                        if (dt.Rows.Count > 0)
                        {
                            Session["UserId"] = dt.Rows[0]["Id"].ToString();
                            Session["UserName"] = dt.Rows[0]["Name"].ToString();
                            Session["UserPicture"] = dt.Rows[0]["Picture"].ToString();
                            Session["UserMobile"] = dt.Rows[0]["Mobile"].ToString();
                            Session["UserEmail"] = dt.Rows[0]["Email"].ToString();
                            Session["UserType"] = dt.Rows[0]["UserType"].ToString();
                            Session["UserBranchId"] = dt.Rows[0]["BranchId"].ToString();
                            Response.Redirect("Dashboard.aspx", false);
                        }
                        else
                        {
                            Response.Write("<script>alert('Oops! Invalid User..')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Oops! Incorrect OTP..')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:  | Err:" + ex.Message.ToString());
            }
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            string OTPNumb = cm.FiveDigits();
            string MsgRes = cm.SendOTP(OTPNumb, Session["UserMobile"].ToString());
            if (MsgRes == "SUCCESS" || ((MsgRes != "SUCCESS") && (Session["UserType"].ToString() == "A")))
            {
                Session["UserOTP"] = OTPNumb;
                lblOTP.Text = "0101" + OTPNumb + "0101";
            }
            else
            {
                Response.Write("<script>alert('Oops! OTP Sending Failed')</script>");
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string AppVersion = "1";
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_User]";
            ParameterName.Add("@Action"); ParameterValue.Add("AppVersion");
            dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
            if (dt.Rows[0]["AppVersion"].ToString() != AppVersion)
            {
                Response.Write("<script>alert('You have old version of App. Please Contact to Administrator to get New Updated App..')</script>");
                return;
            }

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            spName = "[Proc_User]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Login");
                ParameterName.Add("@Username"); ParameterValue.Add(txtUsername.Text);
                ParameterName.Add("@Password"); ParameterValue.Add(txtPassword.Text);
                dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                if (dt.Rows.Count > 0)
                {
                    if ((Convert.ToInt32(hfUnderMaintenance.Value) == 1) && (dt.Rows[0]["UserType"].ToString() != "A"))
                    {
                        Response.Redirect("UnderMaintenance.aspx", false);
                    }
                    else
                    {
                        if (chkRemember.Checked == true)
                        {
                            Response.Cookies["username"].Value = txtUsername.Text.ToString();
                            Response.Cookies["userpassword"].Value = txtPassword.Text.ToString();
                            Response.Cookies["Database_"].Value = Session["Database_"].ToString();
                            Response.Cookies["Database_"].Expires = DateTime.Now.AddDays(15);
                            Response.Cookies["username"].Expires = DateTime.Now.AddDays(15);
                            Response.Cookies["userpassword"].Expires = DateTime.Now.AddDays(15);
                        }
                        else
                        {
                            Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["userpassword"].Expires = DateTime.Now.AddDays(-1);
                        }

                        if (btnSend.Text.ToString() == "SEND OTP")
                        {
                            string OTPNumb = cm.FiveDigits();
                            string MsgRes = cm.SendOTP(OTPNumb, dt.Rows[0]["Mobile"].ToString());
                            if (MsgRes == "SUCCESS" || ((MsgRes != "SUCCESS") && (dt.Rows[0]["UserType"].ToString() == "A")))
                            {
                                dvLogin.Visible = false;
                                dvOTP.Visible = true;
                                Session["UserOTP"] = OTPNumb;
                                lblOTP.Text = "0101" + OTPNumb + "0101";
                                Session["UserName"] = txtUsername.Text.ToString();
                                Session["UserPassword"] = txtPassword.Text.ToString();
                                Session["UserType"] = dt.Rows[0]["UserType"].ToString();
                                Session["UserMobile"] = dt.Rows[0]["Mobile"].ToString();
                            }
                            else
                            {
                                Response.Write("<script>alert('Oops! OTP Sending Failed')</script>");
                            }
                        }
                        else
                        {
                            Session["UserId"] = dt.Rows[0]["Id"].ToString();
                            Session["UserName"] = dt.Rows[0]["Name"].ToString();
                            Session["UserPicture"] = dt.Rows[0]["Picture"].ToString();
                            Session["UserMobile"] = dt.Rows[0]["Mobile"].ToString();
                            Session["UserEmail"] = dt.Rows[0]["Email"].ToString();
                            Session["UserType"] = dt.Rows[0]["UserType"].ToString();
                            Session["UserBranchId"] = dt.Rows[0]["BranchId"].ToString();
                            Response.Redirect("Dashboard", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Oops! Invalid Login Details')</script>");
                    clear();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:  | Err:" + ex.Message.ToString());
            }
        }
        #endregion
    }
}