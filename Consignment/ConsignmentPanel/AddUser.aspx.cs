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
    public partial class AddUser : System.Web.UI.Page
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
                FillBranch();
                if (Request.QueryString["id"] != null)
                {
                    if (cm.GetUserDetails().Rows[0]["UserType"].ToString() != "A")
                    {
                        dvBranch.Visible = false;
                        dvEmail.Visible = false;
                        dvMobile.Visible = false;
                        dvName.Visible = false;
                        dvNavigation.Visible = false;
                        dvPicture.Visible = false;
                        dvUserType.Visible = false;
                        btnClear.Visible = false;
                    }
                    lblHeading.Text = "Update User";
                    btnSubmit.Text = "Update";
                    FillData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    lblHeading.Text = "Add User";
                    btnSubmit.Text = "Add";
                }
            }
        }
        #endregion

        #region [all-fuctions]
        private void FillBranch()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Branch";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlBranch.DataSource = dt;
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                }
                else
                {
                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private string uploadPicture()
        {
            if (fuUpload.HasFile == true)
            {
                if (fuUpload.PostedFile.FileName != "")
                {
                    string spath = Server.MapPath("../App_Content/User/");
                    string Extension = System.IO.Path.GetExtension(fuUpload.FileName).ToLower();
                    if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                    {
                        string FileName = DateTime.Now.Ticks + fuUpload.FileName.ToString();
                        string fullpath = Convert.ToString(spath + FileName);
                        fuUpload.PostedFile.SaveAs(fullpath);
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
        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_User";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    ddlType.SelectedValue = dt.Rows[0]["UserType"].ToString();
                    ddlBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
                    txtUsername.Text = dt.Rows[0]["Username"].ToString();
                    txtPassword.Attributes["value"] = dt.Rows[0]["Password"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void clear()
        {
            txtName.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            ddlType.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
        #endregion

        #region [All-Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            #region [update]
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string CheckFolder = cm.FolderCheck("User");
            string UserPicture = uploadPicture();
            Int32 intResult = 0;
            string spName = "[Proc_User]";
            try
            {
                if (Request.QueryString["Id"] == null)
                {
                    #region[Insert]
                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@Name"); ParameterValue.Add(txtName.Text);
                    ParameterName.Add("@Picture"); ParameterValue.Add(UserPicture);
                    ParameterName.Add("@Username"); ParameterValue.Add(txtUsername.Text);
                    ParameterName.Add("@Password"); ParameterValue.Add(txtPassword.Text);
                    ParameterName.Add("@Mobile"); ParameterValue.Add(txtMobile.Text);
                    ParameterName.Add("@Email"); ParameterValue.Add(txtEmail.Text);
                    ParameterName.Add("@UserType"); ParameterValue.Add(ddlType.SelectedValue); 
                    ParameterName.Add("@BranchId"); ParameterValue.Add(ddlBranch.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("User", "Inserted", txtName.Text);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Inserted successfully.');window.location='ListUser.aspx';", true);
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
                    ParameterName.Add("@Name"); ParameterValue.Add(txtName.Text);
                    ParameterName.Add("@Picture"); ParameterValue.Add(UserPicture);
                    ParameterName.Add("@Username"); ParameterValue.Add(txtUsername.Text);
                    ParameterName.Add("@Password"); ParameterValue.Add(txtPassword.Text);
                    ParameterName.Add("@Mobile"); ParameterValue.Add(txtMobile.Text);
                    ParameterName.Add("@Email"); ParameterValue.Add(txtEmail.Text);
                    ParameterName.Add("@UserType"); ParameterValue.Add(ddlType.SelectedValue);
                    ParameterName.Add("@BranchId"); ParameterValue.Add(ddlBranch.SelectedValue);
                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                    intResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (intResult > 0)
                    {
                        intResult = cm.SaveActivityLog("User", "Updated", txtName.Text);
                        if (cm.GetUserDetails().Rows[0]["UserType"].ToString() != "A")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='Dashboard.aspx';", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='ListUser.aspx';", true);
                        }
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