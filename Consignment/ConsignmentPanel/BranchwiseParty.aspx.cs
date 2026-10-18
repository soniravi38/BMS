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
    public partial class BranchwiseParty : System.Web.UI.Page
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
                FillBranch();
                lblHeading.Text = "Branchwise Party";
            }
        }
        #endregion

        #region [All-Function]
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
        private void fillList()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "[Proc_Consign]";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] DrConsignor = ds.Tables[0].Select("ConsgMode = 'A'");
                        chkConsignor.DataSource = DrConsignor.CopyToDataTable();
                        chkConsignor.DataTextField = "Consgname";
                        chkConsignor.DataValueField = "Id";
                        chkConsignor.DataBind();

                        DataRow[] DrConsignee = ds.Tables[0].Select("ConsgMode = 'S'");
                        chkConsignee.DataSource = DrConsignee.CopyToDataTable();
                        chkConsignee.DataTextField = "Consgname";
                        chkConsignee.DataValueField = "Id";
                        chkConsignee.DataBind();

                        DataRow[] DrBillingParty = ds.Tables[0].Select("ConsgMode = 'B'");
                        chkBillingParty.DataSource = DrBillingParty.CopyToDataTable();
                        chkBillingParty.DataTextField = "Consgname";
                        chkBillingParty.DataValueField = "Id";
                        chkBillingParty.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }

        }
        #endregion

        #region[Events]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ConsignorItemValue = "";
            string ConsigneeItemValue = "";
            string BillingPartyItemValue = "";
            string AllCheckboxSelectedValue = "";

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            int IntResult = 0;
            string spName = "[Proc_BranchwiseConsig]";
            try
            {
                if (ddlBranch.SelectedIndex > 0)
                {

                    foreach (ListItem item in chkConsignor.Items)
                    {
                        if (item.Selected)
                        {
                            ConsignorItemValue += item.Value + ",";
                        }
                    }

                    foreach (ListItem item in chkConsignee.Items)
                    {
                        if (item.Selected)
                        {
                            ConsigneeItemValue += item.Value + ",";
                        }
                    }

                    foreach (ListItem item in chkBillingParty.Items)
                    {
                        if (item.Selected)
                        {
                            BillingPartyItemValue += item.Value + ",";
                        }
                    }
                    AllCheckboxSelectedValue = ConsignorItemValue + ConsigneeItemValue + BillingPartyItemValue;

                    ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                    ParameterName.Add("@BranchId"); ParameterValue.Add(ddlBranch.SelectedValue);
                    ParameterName.Add("@PartyStr"); ParameterValue.Add(AllCheckboxSelectedValue);
                    IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                    if (IntResult > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data Updated successfully.');window.location='BranchwiseParty.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string spName = "[Proc_BranchwiseConsig]";
            try
            {
                if (Convert.ToInt32(ddlBranch.SelectedValue) > 0)
                {
                    fillList();
                    DataTable Consignordt;
                    DataTable Consigneedt;
                    DataTable BillingPartydt;

                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                    ParameterName.Add("@BranchId"); ParameterValue.Add(ddlBranch.SelectedValue);
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow[] DrConsignor = ds.Tables[0].Select("ConsgMode = 'A'");
                            if (DrConsignor.Length > 0)
                            {
                                Consignordt = DrConsignor.CopyToDataTable();
                                for (int i = 0; i < Consignordt.Rows.Count; i++)
                                {
                                    chkConsignor.Items.FindByValue(Consignordt.Rows[i]["Consgid"].ToString()).Selected = true;
                                }
                            }

                            DataRow[] DrConsignee = ds.Tables[0].Select("ConsgMode = 'S'");
                            if (DrConsignee.Length > 0)
                            {
                                Consigneedt = DrConsignee.CopyToDataTable();
                                for (int i = 0; i < Consigneedt.Rows.Count; i++)
                                {
                                    chkConsignee.Items.FindByValue(Consigneedt.Rows[i]["Consgid"].ToString()).Selected = true;
                                }
                            }

                            DataRow[] DrBillingParty = ds.Tables[0].Select("ConsgMode = 'B'");
                            if (DrBillingParty.Length > 0)
                            {
                                BillingPartydt = DrBillingParty.CopyToDataTable();
                                for (int i = 0; i < BillingPartydt.Rows.Count; i++)
                                {
                                    chkBillingParty.Items.FindByValue(BillingPartydt.Rows[i]["Consgid"].ToString()).Selected = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        #endregion
    }
}
