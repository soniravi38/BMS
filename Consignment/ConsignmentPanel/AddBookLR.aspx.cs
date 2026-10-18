using Consignment.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace Consignment.ConsignmentPanel
{
    public partial class AddBookLR : System.Web.UI.Page
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
            divsave.Visible = false;
            hfbrid.Value = (cm.GetUserDetails().Rows[0]["BranchId"]).ToString();
            
            if (!IsPostBack)
            {
                FillConsignor();
                FillConsignee();
                FillBillingParty();
                FillSource();
                FillUnit();
                FillSize();
                Clear();
                txtdate.Text = cm.GetCurrentDate();
                if (hfbrid.Value != "7" && Session["UserType"].ToString() != "A")
                {
                    ddlMode.Items.Remove(ddlMode.Items.FindByValue("T"));
                }
                if (Request.QueryString["BookId"] != null)
                {
                    HFid.Value = Request.QueryString["BookId"].ToString();
                    GetLRData(Convert.ToInt32(HFid.Value));
                    ddConsignee_SelectedIndexChanged(sender, e);
                    ddConsignor_SelectedIndexChanged(sender, e);
                    ddlVchNo_SelectedIndexChanged(sender, e);
                }
            }
        }
        #endregion

        #region [all-fuctions]
        private void FillConsignor()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BranchwiseConsig";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetPartyByBranchId");
                ParameterName.Add("@BranchId"); ParameterValue.Add(Convert.ToInt32(hfbrid.Value));
                ParameterName.Add("@ConsgMode"); ParameterValue.Add("A");
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddConsignor.DataSource = dt;
                    ddConsignor.DataValueField = "Id";
                    ddConsignor.DataTextField = "Consgname";
                    ddConsignor.DataBind();
                    ddConsignor.Items.Insert(0, new ListItem("--Select Consignor--", "0"));
                }
                else
                {
                    ddConsignor.Items.Clear();
                    ddConsignor.Items.Insert(0, new ListItem("--Select Consignor--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillConsignee()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BranchwiseConsig";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetPartyByBranchId");
                ParameterName.Add("@BranchId"); ParameterValue.Add(Convert.ToInt32(hfbrid.Value));
                ParameterName.Add("@ConsgMode"); ParameterValue.Add("S");
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddConsignee.DataSource = dt;
                    ddConsignee.DataValueField = "Id";
                    ddConsignee.DataTextField = "Consgname";
                    ddConsignee.DataBind();
                    ddConsignee.Items.Insert(0, new ListItem("--Select Consignee--", "0"));
                }
                else
                {
                    ddConsignee.Items.Clear();
                    ddConsignee.Items.Insert(0, new ListItem("--Select Consignee--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillBillingParty()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BranchwiseConsig";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetPartyByBranchId");
                ParameterName.Add("@BranchId"); ParameterValue.Add(Convert.ToInt32(hfbrid.Value));
                ParameterName.Add("@ConsgMode"); ParameterValue.Add("B");
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlBillto.DataSource = dt;
                    ddlBillto.DataValueField = "Consgname";
                    ddlBillto.DataTextField = "Consgname";
                    ddlBillto.DataBind();
                    ddlBillto.Items.Insert(0, new ListItem("--Select Billing Party--", "0"));
                }
                else
                {
                    ddlBillto.Items.Clear();
                    ddlBillto.Items.Insert(0, new ListItem("--Select Billing Party--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillSource()
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
                    ddSourceFrom.DataSource = dt;
                    ddSourceFrom.DataValueField = "Id";
                    ddSourceFrom.DataTextField = "Name";
                    ddSourceFrom.DataBind();
                    ddSourceFrom.Items.Insert(0, new ListItem("--Select Source From--", "0"));

                    ddSourceTo.DataSource = dt;
                    ddSourceTo.DataValueField = "Id";
                    ddSourceTo.DataTextField = "Name";
                    ddSourceTo.DataBind();
                    ddSourceTo.Items.Insert(0, new ListItem("--Select Source To--", "0"));
                }
                else
                {
                    ddSourceFrom.Items.Clear();
                    ddSourceFrom.Items.Insert(0, new ListItem("--Select Source From--", "0"));

                    ddSourceTo.Items.Clear();
                    ddSourceTo.Items.Insert(0, new ListItem("--Select Source To--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillVehicleById(int VehicleId)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Vehicle";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(VehicleId);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    ddlVchNo.DataSource = dt1;
                    ddlVchNo.DataValueField = "Id";
                    ddlVchNo.DataTextField = "VehicleNo";
                    ddlVchNo.DataBind();
                }
                else
                {
                    ddlVchNo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillSize()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Size";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlSize.DataSource = dt;
                    ddlSize.DataValueField = "Name";
                    ddlSize.DataTextField = "Name";
                    ddlSize.DataBind();
                    ddlSize.Items.Insert(0, new ListItem("--Select Size--", "0"));
                }
                else
                {
                    ddlSize.Items.Clear();
                    ddlSize.Items.Insert(0, new ListItem("--Select Size--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        private void FillUnit()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Unit";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("List");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlunit.DataSource = dt;
                    ddlunit.DataValueField = "Name";
                    ddlunit.DataTextField = "Name";
                    ddlunit.DataBind();
                    ddlunit.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                }
                else
                {
                    ddlunit.Items.Clear();
                    ddlunit.Items.Insert(0, new ListItem("--Select Unit--", "0"));
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        public void Clear()
        {
            txtLCNo.Text = "";

            txtAmount.Text = "0";
            txtdate.Text = "";
            txtDestination.Text = "";
            txtDocNo.Text = "";
            txtEwayBillNo.Text = "";
            txtfromdate.Text = "";
            txtinvdate.Text = "";
            txtinvno.Text = "";
            txtLoading.Text = "";
            TxtLrNo.Text = "";
            txtQty.Text = "0";
            TxtLrNo.ReadOnly = false;
            txtRemarks.Text = "";
            txtShipNo.Text = "";
            txttodate.Text = "";
            HFid.Value = "";
            lblConsigneeAddress.Text = "";
            lblConsigneeGst.Text = "";
            lblConsigneeState.Text = "";
            lblConsignorAddress.Text = "";
            lblConsignorGST.Text = "";
            lblConsignorState.Text = "";
            ddlBillto.SelectedIndex = 0;
            txtsealno.Text = "";
            txtDriverMobile.Text = "";
            txtDriverAdhar.Text = "";
            ddlSize.SelectedIndex = 0;
            txtRemarks1.Text = "";
            ddConsignee.SelectedIndex = 0;
            txtProduct.Text = "";
            
            ddlunit.SelectedIndex = 0;
            ddlVchNo.SelectedIndex = 0;
            ddSourceFrom.SelectedIndex = 0;
            ddSourceTo.SelectedIndex = 0;
            ddVType.SelectedIndex = 0;
            TxtLrNo.Text = "";
            if (GetLastLrNo() != "")
            {
                lblcongo.Text = GetLastLrNo();
            }

            txtDocumentNo1.Text = "";
            txtDocumentNo2.Text = "";
            txtDocumentNo3.Text = "";
            txtDocumentNo4.Text = "";
            txtDocumentNo5.Text = "";
           

            txtInvoiceNo1.Text = "";
            InvoiceDate1.Text = "";
            InvoiceValue1.Text = "";

            txtInvoiceNo2.Text = "";
            InvoiceDate2.Text = "";
            InvoiceValue2.Text = "";

            txtInvoiceNo3.Text = "";
            InvoiceDate3.Text = "";
            InvoiceValue3.Text = "";

            txtInvoiceNo4.Text = "";
            InvoiceDate4.Text = "";
            InvoiceValue4.Text = "";

            txtInvoiceNo5.Text = "";
            InvoiceDate5.Text = "";
            InvoiceValue5.Text = "";

           

            txtEwayBillNo1.Text = "";
            txtEwayBillDate1.Text = "";
            txtValidUptoDate1.Text = "";


            txtEwayBillNo2.Text = "";
            txtEwayBillDate2.Text = "";
            txtValidUptoDate2.Text = "";

            txtEwayBillNo3.Text = "";
            txtEwayBillDate3.Text = "";
            txtValidUptoDate3.Text = "";

            txtEwayBillNo4.Text = "";
            txtEwayBillDate4.Text = "";
            txtValidUptoDate4.Text = "";

            txtEwayBillNo5.Text = "";
            txtEwayBillDate5.Text = "";
            txtValidUptoDate5.Text = "";

          
        }
        public void ClearForMulti()
        {
            //txtLCNo.Text = "";
            txtAmount.Text = "0";
            txtdate.Text = "";
            txtDestination.Text = "";
            txtDocNo.Text = "";
            txtEwayBillNo.Text = "";
            txtfromdate.Text = "";
            txtinvdate.Text = "";
            txtinvno.Text = "";
            txtLoading.Text = "";
            TxtLrNo.Text = "";
            txtQty.Text = "0";
            TxtLrNo.ReadOnly = false;
            txtRemarks.Text = "";
            txtShipNo.Text = "";
            txttodate.Text = "";
            HFid.Value = "";
            lblConsigneeAddress.Text = "";
            lblConsigneeGst.Text = "";
            lblConsigneeState.Text = "";
            ddlBillto.SelectedIndex = 0;
            txtsealno.Text = "";
            txtDriverMobile.Text = "";
            txtDriverAdhar.Text = "";
            txtRemarks1.Text = "";
            ddConsignee.SelectedIndex = 0;
            txtProduct.Text = "";
            ddlunit.SelectedIndex = 0;
            ddSourceTo.SelectedIndex = 0;
            ddVType.SelectedIndex = 0;
            TxtLrNo.Text = "";
            if (GetLastLrNo() != "")
            {
                lblcongo.Text = GetLastLrNo();
            }
            txtdate.Text = cm.GetCurrentDate();

            txtDocumentNo1.Text = "";
            txtDocumentNo2.Text = "";
            txtDocumentNo3.Text = "";
            txtDocumentNo4.Text = "";
            txtDocumentNo5.Text = "";
           

            txtInvoiceNo1.Text = "";
            InvoiceDate1.Text = "";
            InvoiceValue1.Text = "";

            txtInvoiceNo2.Text = "";
            InvoiceDate2.Text = "";
            InvoiceValue2.Text = "";

            txtInvoiceNo3.Text = "";
            InvoiceDate3.Text = "";
            InvoiceValue3.Text = "";

            txtInvoiceNo4.Text = "";
            InvoiceDate4.Text = "";
            InvoiceValue4.Text = "";

            txtInvoiceNo5.Text = "";
            InvoiceDate5.Text = "";
            InvoiceValue5.Text = "";


          

            txtEwayBillNo1.Text = "";
            txtEwayBillDate1.Text = "";
            txtValidUptoDate1.Text = "";


            txtEwayBillNo2.Text = "";
            txtEwayBillDate2.Text = "";
            txtValidUptoDate2.Text = "";


            txtEwayBillNo3.Text = "";
            txtEwayBillDate3.Text = "";
            txtValidUptoDate3.Text = "";


            txtEwayBillNo4.Text = "";
            txtEwayBillDate4.Text = "";
            txtValidUptoDate4.Text = "";


            txtEwayBillNo5.Text = "";
            txtEwayBillDate5.Text = "";
            txtValidUptoDate5.Text = "";

          
        }
        public string GetLastLrNo()
        {
            string LastLrNo = "";
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetLastLR");
                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    LastLrNo = dt.Rows[0]["BookLrNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return LastLrNo;
        }
        public string GetLastLrNoMBT()
        {
            string LastLrNo = "";
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetLastLRMBT");
                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    LastLrNo = dt.Rows[0]["BookLrNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return LastLrNo;
        }
        public int GetLRCount()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetLRCount");
                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }

        public int GetLRCountMBT()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action");
                ParameterValue.Add("GetLRCountMBT");
                ParameterName.Add("@BookBranchid");
                ParameterValue.Add(hfbrid.Value);
                ParameterName.Add("@BookLrNo");
                ParameterValue.Add(TxtLrNo.Text);
                ParameterName.Add("@UserType");
                ParameterValue.Add(cm.GetUserDetails().Rows[0]["UserType"].ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"]?.ToString() + " | Method/Event:" + MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }

        public int ValidateShipment()
        {
            string text = ddConsignor.SelectedItem.Text.Trim();
            string value = txtShipNo.Text.Trim();
            if (text.StartsWith("ITC", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return 1;
        }
        public int GetLCCount()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetLCCount");
                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
        public int GetMnaualLRCount()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetManualLRCount");
                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
        public int CheckDuplicateLR()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("CheckDuplicate");
                ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
        public int CheckDuplicateLR1()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("CheckDuplicate1");
                ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                ParameterName.Add("@BookId"); ParameterValue.Add(HFid.Value);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }

        public int CheckDuplicateLC()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("CheckDuplicateLC");
                ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
        public int CheckDuplicateLC1()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("CheckDuplicateLC1");
                ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                ParameterName.Add("@BookId"); ParameterValue.Add(HFid.Value);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
        public void GetLRData(int BookId)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@BookId"); ParameterValue.Add(BookId);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddVType.SelectedValue = dt.Rows[0]["BookType"].ToString();
                    TxtLrNo.Text = dt.Rows[0]["BookLrNo"].ToString();
                    TxtLrNo.ReadOnly = true;
                    txtdate.Text = dt.Rows[0]["BookDate"].ToString();
                    ddSourceFrom.SelectedValue = dt.Rows[0]["SourceFromid"].ToString();
                    ddSourceTo.SelectedValue = dt.Rows[0]["SourceToid"].ToString();
                    txtDestination.Text = dt.Rows[0]["BookDestination"].ToString();
                    ddConsignor.SelectedValue = dt.Rows[0]["BookConsiginorid"].ToString();
                    ddConsignee.SelectedValue = dt.Rows[0]["BookConsigieeid"].ToString();
                    FillVehicleById(Convert.ToInt32(dt.Rows[0]["BookVchid"]));
                    txtinvno.Text = dt.Rows[0]["BookInvNo"].ToString();
                    txtinvdate.Text = dt.Rows[0]["BookInvDate"].ToString();
                    txtAmount.Text = dt.Rows[0]["BookAmount"].ToString();
                    txtEwayBillNo.Text = dt.Rows[0]["BookEwayBillNo"].ToString();
                    txtfromdate.Text = dt.Rows[0]["BookFromDate"].ToString();
                    txttodate.Text = dt.Rows[0]["BookValidDate"].ToString();
                    txtShipNo.Text = dt.Rows[0]["BookShipNo"].ToString();
                    txtDocNo.Text = dt.Rows[0]["BookDocNo"].ToString();
                    txtLoading.Text = dt.Rows[0]["BookLoading"].ToString();

                    txtRemarks.Text = dt.Rows[0]["BookRemarks"].ToString();
                    txtProduct.Text = dt.Rows[0]["ProdName"].ToString();
                    txtDriverName.Text = dt.Rows[0]["DriverName"].ToString();
                    ddlMode.SelectedValue = dt.Rows[0]["Mtype"].ToString();
                    ddlunit.SelectedValue = dt.Rows[0]["UnitName"].ToString();
                    ddlBillto.SelectedValue = dt.Rows[0]["BookBillTo"].ToString();
                    ddlSize.SelectedValue = dt.Rows[0]["BookCustSize"].ToString();
                    txtsealno.Text = dt.Rows[0]["SealNo"].ToString();
                    txtQty.Text = dt.Rows[0]["BookQty"].ToString();
                    hfbrid.Value = dt.Rows[0]["BookBranchid"].ToString();
                    txtDriverMobile.Text = dt.Rows[0]["BookDriverMobileNo"].ToString();
                    txtRemarks1.Text = dt.Rows[0]["BookRemark1"].ToString();
                    txtDriverAdhar.Text = dt.Rows[0]["DriverUID"].ToString();
                    txtLCNo.Text = dt.Rows[0]["LCNo"].ToString();
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    spName = "Proc_DocumentDetails";

                    ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                    ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 1)
                    {
                        txtDocumentNo1.Text = dt.Rows[0]["DocumentNo"].ToString();
                    }
                    if (dt.Rows.Count == 2)
                    {
                        txtDocumentNo1.Text = dt.Rows[0]["DocumentNo"].ToString();
                        txtDocumentNo2.Text = dt.Rows[1]["DocumentNo"].ToString();
                    }
                    if (dt.Rows.Count == 3)
                    {
                        txtDocumentNo1.Text = dt.Rows[0]["DocumentNo"].ToString();
                        txtDocumentNo2.Text = dt.Rows[1]["DocumentNo"].ToString();
                        txtDocumentNo3.Text = dt.Rows[2]["DocumentNo"].ToString();
                    }
                    if (dt.Rows.Count == 4)
                    {
                        txtDocumentNo1.Text = dt.Rows[0]["DocumentNo"].ToString();
                        txtDocumentNo2.Text = dt.Rows[1]["DocumentNo"].ToString();
                        txtDocumentNo3.Text = dt.Rows[2]["DocumentNo"].ToString();
                        txtDocumentNo4.Text = dt.Rows[3]["DocumentNo"].ToString();
                    }
                    if (dt.Rows.Count == 5)
                    {
                        txtDocumentNo1.Text = dt.Rows[0]["DocumentNo"].ToString();
                        txtDocumentNo2.Text = dt.Rows[1]["DocumentNo"].ToString();
                        txtDocumentNo3.Text = dt.Rows[2]["DocumentNo"].ToString();
                        txtDocumentNo4.Text = dt.Rows[3]["DocumentNo"].ToString();
                        txtDocumentNo5.Text = dt.Rows[4]["DocumentNo"].ToString();
                    }
                   


                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    spName = "Proc_InvoiceDetails";

                    ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                    ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 1)
                    {
                        txtInvoiceNo1.Text = dt.Rows[0]["InvoiceNo"].ToString();
                        InvoiceDate1.Text = dt.Rows[0]["InvoiceDate"].ToString();
                        InvoiceValue1.Text = dt.Rows[0]["InvoiceValue"].ToString();
                        
                    }
                    if (dt.Rows.Count == 2)
                    {
                        txtInvoiceNo1.Text = dt.Rows[0]["InvoiceNo"].ToString();
                        InvoiceDate1.Text = dt.Rows[0]["InvoiceDate"].ToString();
                        InvoiceValue1.Text = dt.Rows[0]["InvoiceValue"].ToString();
                        

                        txtInvoiceNo2.Text = dt.Rows[1]["InvoiceNo"].ToString();
                        InvoiceDate2.Text = dt.Rows[1]["InvoiceDate"].ToString();
                        InvoiceValue2.Text = dt.Rows[1]["InvoiceValue"].ToString();
                        
                    }
                    if (dt.Rows.Count == 3)
                    {
                        txtInvoiceNo1.Text = dt.Rows[0]["InvoiceNo"].ToString();
                        InvoiceDate1.Text = dt.Rows[0]["InvoiceDate"].ToString();
                        InvoiceValue1.Text = dt.Rows[0]["InvoiceValue"].ToString();
                        

                        txtInvoiceNo2.Text = dt.Rows[1]["InvoiceNo"].ToString();
                        InvoiceDate2.Text = dt.Rows[1]["InvoiceDate"].ToString();
                        InvoiceValue2.Text = dt.Rows[1]["InvoiceValue"].ToString();
                        

                        txtInvoiceNo3.Text = dt.Rows[2]["InvoiceNo"].ToString();
                        InvoiceDate3.Text = dt.Rows[2]["InvoiceDate"].ToString();
                        InvoiceValue3.Text = dt.Rows[2]["InvoiceValue"].ToString();
                        
                    }

                    if (dt.Rows.Count == 4)
                    {
                        txtInvoiceNo1.Text = dt.Rows[0]["InvoiceNo"].ToString();
                        InvoiceDate1.Text = dt.Rows[0]["InvoiceDate"].ToString();
                        InvoiceValue1.Text = dt.Rows[0]["InvoiceValue"].ToString();
                        

                        txtInvoiceNo2.Text = dt.Rows[1]["InvoiceNo"].ToString();
                        InvoiceDate2.Text = dt.Rows[1]["InvoiceDate"].ToString();
                        InvoiceValue2.Text = dt.Rows[1]["InvoiceValue"].ToString();

                        txtInvoiceNo3.Text = dt.Rows[2]["InvoiceNo"].ToString();
                        InvoiceDate3.Text = dt.Rows[2]["InvoiceDate"].ToString();
                        InvoiceValue3.Text = dt.Rows[2]["InvoiceValue"].ToString();

                        txtInvoiceNo4.Text = dt.Rows[3]["InvoiceNo"].ToString();
                        InvoiceDate4.Text = dt.Rows[3]["InvoiceDate"].ToString();
                        InvoiceValue4.Text = dt.Rows[3]["InvoiceValue"].ToString();
                    }
                    if (dt.Rows.Count == 5)
                    {
                        txtInvoiceNo1.Text = dt.Rows[0]["InvoiceNo"].ToString();
                        InvoiceDate1.Text = dt.Rows[0]["InvoiceDate"].ToString();
                        InvoiceValue1.Text = dt.Rows[0]["InvoiceValue"].ToString();

                        txtInvoiceNo2.Text = dt.Rows[1]["InvoiceNo"].ToString();
                        InvoiceDate2.Text = dt.Rows[1]["InvoiceDate"].ToString();
                        InvoiceValue2.Text = dt.Rows[1]["InvoiceValue"].ToString();

                        txtInvoiceNo3.Text = dt.Rows[2]["InvoiceNo"].ToString();
                        InvoiceDate3.Text = dt.Rows[2]["InvoiceDate"].ToString();
                        InvoiceValue3.Text = dt.Rows[2]["InvoiceValue"].ToString();

                        txtInvoiceNo4.Text = dt.Rows[3]["InvoiceNo"].ToString();
                        InvoiceDate4.Text = dt.Rows[3]["InvoiceDate"].ToString();
                        InvoiceValue4.Text = dt.Rows[3]["InvoiceValue"].ToString();

                        txtInvoiceNo5.Text = dt.Rows[4]["InvoiceNo"].ToString();
                        InvoiceDate5.Text = dt.Rows[4]["InvoiceDate"].ToString();
                        InvoiceValue5.Text = dt.Rows[4]["InvoiceValue"].ToString();


                    }

                   

                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    spName = "Proc_EwayBillDetails";

                    ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                    ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 1)
                    {
                        txtEwayBillNo1.Text = dt.Rows[0]["EwayBillNo"].ToString();
                        txtEwayBillDate1.Text = dt.Rows[0]["BillDate"].ToString();
                        txtValidUptoDate1.Text = dt.Rows[0]["BillUptoDate"].ToString();
                    }
                    if (dt.Rows.Count == 2)
                    {
                        txtEwayBillNo1.Text = dt.Rows[0]["EwayBillNo"].ToString();
                        txtEwayBillDate1.Text = dt.Rows[0]["BillDate"].ToString();
                        txtValidUptoDate1.Text = dt.Rows[0]["BillUptoDate"].ToString();

                        txtEwayBillNo2.Text = dt.Rows[1]["EwayBillNo"].ToString();
                        txtEwayBillDate2.Text = dt.Rows[1]["BillDate"].ToString();
                        txtValidUptoDate2.Text = dt.Rows[1]["BillUptoDate"].ToString();
                    }
                    if (dt.Rows.Count == 3)
                    {
                        txtEwayBillNo1.Text = dt.Rows[0]["EwayBillNo"].ToString();
                        txtEwayBillDate1.Text = dt.Rows[0]["BillDate"].ToString();
                        txtValidUptoDate1.Text = dt.Rows[0]["BillUptoDate"].ToString();

                        txtEwayBillNo2.Text = dt.Rows[1]["EwayBillNo"].ToString();
                        txtEwayBillDate2.Text = dt.Rows[1]["BillDate"].ToString();
                        txtValidUptoDate2.Text = dt.Rows[1]["BillUptoDate"].ToString();

                        txtEwayBillNo3.Text = dt.Rows[2]["EwayBillNo"].ToString();
                        txtEwayBillDate3.Text = dt.Rows[2]["BillDate"].ToString();
                        txtValidUptoDate3.Text = dt.Rows[2]["BillUptoDate"].ToString();
                    }
                    if (dt.Rows.Count == 4)
                    {
                        txtEwayBillNo1.Text = dt.Rows[0]["EwayBillNo"].ToString();
                        txtEwayBillDate1.Text = dt.Rows[0]["BillDate"].ToString();
                        txtValidUptoDate1.Text = dt.Rows[0]["BillUptoDate"].ToString();

                        txtEwayBillNo2.Text = dt.Rows[1]["EwayBillNo"].ToString();
                        txtEwayBillDate2.Text = dt.Rows[1]["BillDate"].ToString();
                        txtValidUptoDate2.Text = dt.Rows[1]["BillUptoDate"].ToString();

                        txtEwayBillNo3.Text = dt.Rows[2]["EwayBillNo"].ToString();
                        txtEwayBillDate3.Text = dt.Rows[2]["BillDate"].ToString();
                        txtValidUptoDate3.Text = dt.Rows[2]["BillUptoDate"].ToString();

                        txtEwayBillNo4.Text = dt.Rows[3]["EwayBillNo"].ToString();
                        txtEwayBillDate4.Text = dt.Rows[3]["BillDate"].ToString();
                        txtValidUptoDate4.Text = dt.Rows[3]["BillUptoDate"].ToString();
                    }
                    if (dt.Rows.Count == 5)
                    {
                        txtEwayBillNo1.Text = dt.Rows[0]["EwayBillNo"].ToString();
                        txtEwayBillDate1.Text = dt.Rows[0]["BillDate"].ToString();
                        txtValidUptoDate1.Text = dt.Rows[0]["BillUptoDate"].ToString();

                        txtEwayBillNo2.Text = dt.Rows[1]["EwayBillNo"].ToString();
                        txtEwayBillDate2.Text = dt.Rows[1]["BillDate"].ToString();
                        txtValidUptoDate2.Text = dt.Rows[1]["BillUptoDate"].ToString();

                        txtEwayBillNo3.Text = dt.Rows[2]["EwayBillNo"].ToString();
                        txtEwayBillDate3.Text = dt.Rows[2]["BillDate"].ToString();
                        txtValidUptoDate3.Text = dt.Rows[2]["BillUptoDate"].ToString();

                        txtEwayBillNo4.Text = dt.Rows[3]["EwayBillNo"].ToString();
                        txtEwayBillDate4.Text = dt.Rows[3]["BillDate"].ToString();
                        txtValidUptoDate4.Text = dt.Rows[3]["BillUptoDate"].ToString();

                        txtEwayBillNo5.Text = dt.Rows[4]["EwayBillNo"].ToString();
                        txtEwayBillDate5.Text = dt.Rows[4]["BillDate"].ToString();
                        txtValidUptoDate5.Text = dt.Rows[4]["BillUptoDate"].ToString();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        public string MaxBookId()
        {
            string BookId = "";
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("MaxBookId");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    BookId = dt.Rows[0]["BookId"].ToString();
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
            return BookId;
        }
        #endregion

        #region[Events]
        public int CheckInvoiceValue()
        {
            int check = 0;
            if (InvoiceValue1.Text != "")
            {
                if (!(Regex.IsMatch(InvoiceValue1.Text, @"^\d+$")))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Invoice Value not in corrent format');", true);
                    check = 1;
                }
            }
            if (InvoiceValue2.Text != "")
            {
                if (!(Regex.IsMatch(InvoiceValue2.Text, @"^\d+$")))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Invoice Value not in corrent format');", true);
                    check = 1;
                }
            }
            if (InvoiceValue3.Text != "")
            {
                if (!(Regex.IsMatch(InvoiceValue3.Text, @"^\d+$")))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Invoice Value not in corrent format');", true);
                    check = 1;
                }
            }
            if (InvoiceValue4.Text != "")
            {
                if (!(Regex.IsMatch(InvoiceValue4.Text, @"^\d+$")))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Invoice Value not in corrent format');", true);
                    check = 1;
                }
            }
            if (InvoiceValue5.Text != "")
            {
                if (!(Regex.IsMatch(InvoiceValue5.Text, @"^\d+$")))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Invoice Value not in corrent format');", true);
                    check = 1;
                }
            }
            return check;
        }
        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            int Count = 0;
            int Count1 = 0;
            string sSql = "";
            CultureInfo cultuers = new CultureInfo("en-US");
            Int32 IntResult = 0;
            string spName = "";
            try
            {
                if (txtCode.Text == Session["OTP"].ToString())
                {
                    if (CheckInvoiceValue() == 1)
                    {
                        return;
                    }
                    if ((ddlMode.SelectedIndex == 2 || ddlMode.SelectedIndex == 3) && HFid.Value == "")
                    {
                        if (Session["GroupSessionId"] == null)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to add atleast one LR');", true);
                        }
                        else
                        {
                            ParameterName = new ArrayList();
                            ParameterValue = new ArrayList();
                            spName = "Proc_BookLR";
                            ParameterName.Add("@Action"); ParameterValue.Add("MoveFromTMPtoMain");
                            ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                            IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            if (IntResult > 0)
                            {
                                Session["GroupSessionId"] = null;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('All Data SuccessFully inserted!');window.location='ReportViewer.aspx?BookId=" + MaxBookId() + "';", true);
                                Session["PrintBookId"] = MaxBookId();
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        if (ddlMode.SelectedIndex == 0)
                        {
                            Count = GetLRCount();
                        }
                        else
                        {
                            Count = GetMnaualLRCount();
                        }

                        Count1 = GetLCCount();

                        if (HFid.Value == "")
                        {
                            if (CheckDuplicateLR() > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LR No!');", true);
                            }
                            else if (Count == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for Consignment No!');TxtLrNo.focus;", true);
                            }
                            else if (CheckDuplicateLC() > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LC No!');", true);
                            }
                            else if (Count1 == 0 && txtLCNo.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('please contact to admin for lc no!');txtlcno.focus;", true);
                            }
                            else if (ValidateShipment() == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Shipment Number must be Entered!');txtShipNo.focus;", true);
                                return;
                            }
                            //else if (ddlMode.SelectedIndex == 0 && TxtLrNo.Text.Length != 7)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Consigment No Must be in 7 Digits!');TxtLrNo.focus;", true);
                            //}
                            else
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                spName = "Proc_BookLR";
                                ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                ParameterName.Add("@BookType"); ParameterValue.Add(ddVType.SelectedValue);
                                ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@BookDate"); ParameterValue.Add(txtdate.Text);
                                ParameterName.Add("@SourceFromid"); ParameterValue.Add(ddSourceFrom.SelectedValue);
                                ParameterName.Add("@SourceToid"); ParameterValue.Add(ddSourceTo.SelectedValue);
                                ParameterName.Add("@BookDestination"); ParameterValue.Add(txtDestination.Text);
                                ParameterName.Add("@BookConsiginorid"); ParameterValue.Add(ddConsignor.SelectedValue);
                                ParameterName.Add("@BookConsigieeid"); ParameterValue.Add(ddConsignee.SelectedValue);
                                ParameterName.Add("@BookPartyid"); ParameterValue.Add(0);
                                ParameterName.Add("@BookVchid"); ParameterValue.Add(ddlVchNo.SelectedValue);
                                ParameterName.Add("@BookInvNo"); ParameterValue.Add(txtinvno.Text);
                                ParameterName.Add("@BookInvDate"); ParameterValue.Add(txtinvdate.Text);
                                ParameterName.Add("@BookAmount"); ParameterValue.Add(txtAmount.Text);
                                ParameterName.Add("@BookEwayBillNo"); ParameterValue.Add(txtEwayBillNo.Text);
                                ParameterName.Add("@BookFromDate"); ParameterValue.Add(txtfromdate.Text);
                                ParameterName.Add("@BookValidDate"); ParameterValue.Add(txttodate.Text);
                                ParameterName.Add("@BookShipNo"); ParameterValue.Add(txtShipNo.Text);
                                ParameterName.Add("@BookDocNo"); ParameterValue.Add(txtDocNo.Text);
                                ParameterName.Add("@BookLoading"); ParameterValue.Add(txtLoading.Text);
                                ParameterName.Add("@BookRemarks"); ParameterValue.Add(txtRemarks.Text);
                                ParameterName.Add("@ProdName"); ParameterValue.Add(txtProduct.Text.Trim());
                                ParameterName.Add("@UnitName"); ParameterValue.Add(ddlunit.SelectedValue);
                                ParameterName.Add("@BookQty"); ParameterValue.Add(txtQty.Text);
                                ParameterName.Add("@ActQty"); ParameterValue.Add(0);
                                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                                ParameterName.Add("@BookDriverMobileNo"); ParameterValue.Add(txtDriverMobile.Text);
                                ParameterName.Add("@BookCustSize"); ParameterValue.Add(ddlSize.SelectedValue);
                                ParameterName.Add("@BookRemark1"); ParameterValue.Add(txtRemarks1.Text);
                                ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriverName.Text);
                                ParameterName.Add("@SealNo"); ParameterValue.Add(txtsealno.Text);
                                ParameterName.Add("@BookBillTo"); ParameterValue.Add(ddlBillto.SelectedValue);
                                ParameterName.Add("@Mtype"); ParameterValue.Add(ddlMode.SelectedValue);
                                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                                ParameterName.Add("@DriverUID"); ParameterValue.Add(txtDriverAdhar.Text);
                                ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                if (IntResult > 0)
                                {
                                    string MaxBookId1 = MaxBookId();
                                    spName = "Proc_DocumentDetails";

                                    if (txtDocumentNo1.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo1.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo2.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo2.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo3.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo3.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo4.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo4.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo5.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo5.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                   

                                    spName = "Proc_InvoiceDetails";
                                    if (txtInvoiceNo1.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo1.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate1.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue1.Text);
                                        
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo2.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo2.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate2.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue2.Text);
                                        
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo3.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo3.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate3.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue3.Text);
                                        
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo4.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo4.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate4.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue4.Text);
                                        
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo5.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo5.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate5.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue5.Text);
                                        
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                  

                                    spName = "Proc_EwayBillDetails";
                                    if (txtEwayBillNo1.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo1.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate1.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate1.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo2.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo2.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate2.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate2.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo3.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo3.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate3.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate3.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo4.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo4.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate4.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate4.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo5.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo5.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate5.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate5.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                  

                                    //LC Entry
                                    ParameterName = new ArrayList();
                                    ParameterValue = new ArrayList();
                                    spName = "Proc_BookLR";
                                    ParameterName.Add("@Action"); ParameterValue.Add("LCbyLR");
                                    ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                                    ParameterName.Add("@BookDate"); ParameterValue.Add(txtdate.Text);
                                    ParameterName.Add("@SourceFromid"); ParameterValue.Add(ddSourceFrom.SelectedValue);
                                    ParameterName.Add("@SourceToid"); ParameterValue.Add(ddSourceTo.SelectedValue);
                                    ParameterName.Add("@BookDestination"); ParameterValue.Add(txtDestination.Text);
                                    ParameterName.Add("@BookVchid"); ParameterValue.Add(ddlVchNo.SelectedValue);
                                    ParameterName.Add("@UnitName"); ParameterValue.Add(ddlunit.SelectedValue);
                                    ParameterName.Add("@BookQty"); ParameterValue.Add(txtQty.Text);
                                    ParameterName.Add("@BookCustSize"); ParameterValue.Add(ddlSize.SelectedValue);
                                    ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                                    ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                                    ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                                    ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriverName.Text);
                                    ParameterName.Add("@DriverUID"); ParameterValue.Add(txtDriverAdhar.Text);
                                    ParameterName.Add("@BookDriverMobileNo"); ParameterValue.Add(txtDriverMobile.Text);
                                    IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);

                                    IntResult = cm.SaveActivityLog("LR Entry", "Inserted", TxtLrNo.Text);



                                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data SuccessFully inserted!');window.location='ReportViewer.aspx?BookId=" + MaxBookId() + "';", true);
                                    if (txtEwayBillNo.Text.Trim() != "")
                                    {
                                        Session["PrintBookId"] = MaxBookId1;
                                    }


                                    GTropyAPIService service = new GTropyAPIService();
                                    await service.SendToGtropyAPI(ddlVchNo.SelectedItem.Text, TxtLrNo.Text, Convert.ToDateTime(txtdate.Text), ddSourceFrom.SelectedItem.Text, ddSourceTo.SelectedItem.Text);



                                    Clear();


                                }
                            }
                        }
                        else
                        {
                            if (CheckDuplicateLR1() > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LR No!');", true);
                            }
                            else if (Count == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for Consignment No!');TxtLrNo.focus;", true);
                            }
                            else if (CheckDuplicateLC1() > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LC No!');", true);
                            }
                            else if (Count1 == 0 && txtLCNo.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for LC No!');txtLCNo.focus;", true);
                            }
                            else if (ValidateShipment() == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Shipment Number must be Entered!');txtShipNo.focus;", true);
                                return;
                            }
                            else
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                spName = "Proc_BookLR";
                                ParameterName.Add("@Action"); ParameterValue.Add("Update");
                                ParameterName.Add("@BookId"); ParameterValue.Add(HFid.Value);
                                ParameterName.Add("@BookType"); ParameterValue.Add(ddVType.SelectedValue);
                                ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@BookDate"); ParameterValue.Add(txtdate.Text);
                                ParameterName.Add("@SourceFromid"); ParameterValue.Add(ddSourceFrom.SelectedValue);
                                ParameterName.Add("@SourceToid"); ParameterValue.Add(ddSourceTo.SelectedValue);
                                ParameterName.Add("@BookDestination"); ParameterValue.Add(txtDestination.Text);
                                ParameterName.Add("@BookConsiginorid"); ParameterValue.Add(ddConsignor.SelectedValue);
                                ParameterName.Add("@BookConsigieeid"); ParameterValue.Add(ddConsignee.SelectedValue);
                                
                                ParameterName.Add("@BookPartyid"); ParameterValue.Add(0);
                                ParameterName.Add("@BookVchid"); ParameterValue.Add(ddlVchNo.SelectedValue);
                                ParameterName.Add("@BookInvNo"); ParameterValue.Add(txtinvno.Text);
                                ParameterName.Add("@BookInvDate"); ParameterValue.Add(txtinvdate.Text);
                                ParameterName.Add("@BookAmount"); ParameterValue.Add(txtAmount.Text);
                                ParameterName.Add("@BookEwayBillNo"); ParameterValue.Add(txtEwayBillNo.Text);
                                ParameterName.Add("@BookFromDate"); ParameterValue.Add(txtfromdate.Text);
                                ParameterName.Add("@BookValidDate"); ParameterValue.Add(txttodate.Text);
                                ParameterName.Add("@BookShipNo"); ParameterValue.Add(txtShipNo.Text);
                                ParameterName.Add("@BookDocNo"); ParameterValue.Add(txtDocNo.Text);
                                ParameterName.Add("@BookLoading"); ParameterValue.Add(txtLoading.Text);
                                ParameterName.Add("@BookRemarks"); ParameterValue.Add(txtRemarks.Text);
                                ParameterName.Add("@ProdName"); ParameterValue.Add(txtProduct.Text.Trim());
                                ParameterName.Add("@UnitName"); ParameterValue.Add(ddlunit.SelectedValue);
                                ParameterName.Add("@BookQty"); ParameterValue.Add(txtQty.Text);
                                ParameterName.Add("@ActQty"); ParameterValue.Add(0);
                                ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                                ParameterName.Add("@BookDriverMobileNo"); ParameterValue.Add(txtDriverMobile.Text);
                                ParameterName.Add("@BookCustSize"); ParameterValue.Add(ddlSize.SelectedValue);
                                ParameterName.Add("@BookRemark1"); ParameterValue.Add(txtRemarks1.Text);
                                ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriverName.Text);
                                ParameterName.Add("@SealNo"); ParameterValue.Add(txtsealno.Text);
                                ParameterName.Add("@BookBillTo"); ParameterValue.Add(ddlBillto.SelectedValue);
                                ParameterName.Add("@Mtype"); ParameterValue.Add(ddlMode.SelectedValue);
                                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                                ParameterName.Add("@DriverUID"); ParameterValue.Add(txtDriverAdhar.Text);
                                ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                if (IntResult > 0)
                                {
                                    ParameterName = new ArrayList();
                                    ParameterValue = new ArrayList();
                                    spName = "Proc_DocumentDetails";

                                    ParameterName.Add("@Action"); ParameterValue.Add("delete");
                                    ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                    IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);

                                    ParameterName = new ArrayList();
                                    ParameterValue = new ArrayList();
                                    spName = "Proc_InvoiceDetails";

                                    ParameterName.Add("@Action"); ParameterValue.Add("delete");
                                    ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                    IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);

                                    ParameterName = new ArrayList();
                                    ParameterValue = new ArrayList();
                                    spName = "Proc_EwayBillDetails";

                                    ParameterName.Add("@Action"); ParameterValue.Add("delete");
                                    ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                    IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);


                                    spName = "Proc_DocumentDetails";
                                    if (txtDocumentNo1.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo1.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo2.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo2.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo3.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo3.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo4.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo4.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtDocumentNo5.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo5.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }

                                   

                                    spName = "Proc_InvoiceDetails";

                                    if (txtInvoiceNo1.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo1.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate1.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue1.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo2.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo2.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate2.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue2.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo3.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo3.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate3.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue3.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo4.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo4.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate4.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue4.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtInvoiceNo5.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo5.Text);
                                        ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate5.Text);
                                        ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue5.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                  


                                    spName = "Proc_EwayBillDetails";
                                    if (txtEwayBillNo1.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo1.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate1.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate1.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo2.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo2.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate2.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate2.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo3.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo3.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate3.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate3.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo4.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo4.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate4.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate4.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                    if (txtEwayBillNo5.Text != "")
                                    {
                                        ParameterName = new ArrayList();
                                        ParameterValue = new ArrayList();
                                        ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                                        ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                        ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo5.Text);
                                        ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate5.Text);
                                        ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate5.Text);
                                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                                    }
                                  

                                    IntResult = cm.SaveActivityLog("LR Entry", "Updated", TxtLrNo.Text);
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Data SuccessFully Updated!');window.location='ReportViewer.aspx?BookId=" + HFid.Value + "';", true);
                                    if (txtEwayBillNo.Text.Trim() != "")
                                    {
                                        Session["PrintBookId"] = MaxBookId();
                                    }
                                    Clear();
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('In Correct OTP!');", true);
                    txtCode.Text = "";
                    divsave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        protected void btnSaveLocal_Click(object sender, EventArgs e)
        {
            int Count = 0;
            int Count1 = 0;
            CultureInfo cultuers = new CultureInfo("en-US");
            Int32 IntResult = 0;
            string OTPNumb = cm.FiveDigits();
            string spName = "";
            try
            {
                if (CheckInvoiceValue() == 1)
                {
                    return;
                }
                if (txtDriverMobile.Text.Length != 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Driver Mobile Must be 10 Digit!');", true);
                    return;
                }

                if (txtInvoiceNo1.Text == "" && txtInvoiceNo2.Text == "" && txtInvoiceNo3.Text == "" &&
     txtInvoiceNo4.Text == "" && txtInvoiceNo5.Text == "" )
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script",
                        "alert('You must have to Enter at least one Invoice No');", true);
                    return;
                }

                if (txtEwayBillNo1.Text == "" && txtEwayBillNo2.Text == "" && txtEwayBillNo3.Text == "" &&
     txtEwayBillNo4.Text == "" && txtEwayBillNo5.Text == "" )
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script",
                        "alert('You must have to Enter at least one Eway Bill No');", true);
                    return;
                }

                if (txtDocumentNo1.Text == "" && txtDocumentNo2.Text == "" && txtDocumentNo3.Text == "" &&
     txtDocumentNo4.Text == "" && txtDocumentNo5.Text == "" )
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script",
                        "alert('You must have to Enter at least one Document No');", true);
                    return;
                }

                if (txtEwayBillNo1.Text != "")
                {
                    if (txtEwayBillNo1.Text.Length != 12)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                        return;
                    }
                }
                if (txtEwayBillNo2.Text != "")
                {
                    if (txtEwayBillNo2.Text.Length != 12)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                        return;
                    }
                }
                if (txtEwayBillNo3.Text != "")
                {
                    if (txtEwayBillNo3.Text.Length != 12)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                        return;
                    }
                }
                if (txtEwayBillNo4.Text != "")
                {
                    if (txtEwayBillNo4.Text.Length != 12)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                        return;
                    }
                }
                if (txtEwayBillNo5.Text != "")
                {
                    if (txtEwayBillNo5.Text.Length != 12)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                        return;
                    }
                }
                

                if (txtInvoiceNo1.Text != "")
                {
                    if (InvoiceDate1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                        return;
                    }
                    if (InvoiceValue1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                        return;
                    }
                }
                if (txtInvoiceNo2.Text != "")
                {
                    if (InvoiceDate2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                        return;
                    }
                    if (InvoiceValue2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                        return;
                    }
                }
                if (txtInvoiceNo3.Text != "")
                {
                    if (InvoiceDate3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                        return;
                    }
                    if (InvoiceValue3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                        return;
                    }
                }
                if (txtInvoiceNo4.Text != "")
                {
                    if (InvoiceDate4.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                        return;
                    }
                    if (InvoiceValue4.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                        return;
                    }
                }
                if (txtInvoiceNo5.Text != "")
                {
                    if (InvoiceDate5.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                        return;
                    }
                    if (InvoiceValue5.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                        return;
                    }
                }
               

                if (txtEwayBillNo1.Text != "")
                {
                    if (txtValidUptoDate1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                        return;
                    }
                    if (txtEwayBillDate1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                        return;
                    }
                }
                if (txtEwayBillNo2.Text != "")
                {
                    if (txtValidUptoDate2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                        return;
                    }
                    if (txtEwayBillDate2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                        return;
                    }
                }
                if (txtEwayBillNo3.Text != "")
                {
                    if (txtValidUptoDate3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                        return;
                    }
                    if (txtEwayBillDate3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                        return;
                    }
                }
                if (txtEwayBillNo4.Text != "")
                {
                    if (txtValidUptoDate4.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                        return;
                    }
                    if (txtEwayBillDate4.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                        return;
                    }
                }
                if (txtEwayBillNo5.Text != "")
                {
                    if (txtValidUptoDate5.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                        return;
                    }
                    if (txtEwayBillDate5.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                        return;
                    }
                }
               


                if (ddlMode.SelectedIndex == 0)
                {
                    Count = GetLRCount();
                }
                else if (ddlMode.SelectedIndex == 1)
                {
                    Count = GetMnaualLRCount();
                }
                else if (ddlMode.SelectedIndex == 3)
                {
                    Count = GetLRCountMBT();

                    if (Count == 0)
                        Count = GetMnaualLRCount();
                }
                else
                {
                    Count = GetLRCount();
                    if (Count == 0)
                    {
                        Count = GetMnaualLRCount();
                    }
                }
                Count1 = GetLCCount();

                if (HFid.Value == "")
                {
                    if (CheckDuplicateLR() > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LR No!');", true);
                    }
                    else if (Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for Consignment No!');TxtLrNo.focus;", true);
                    }
                    else if (CheckDuplicateLC() > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LC No!');", true);
                    }
                    else if (Count1 == 0 && txtLCNo.Text != "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for LC No!');txtLCNo.focus;", true);
                    }
                    else if (ValidateShipment() == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Shipment Number must be Entered!');txtShipNo.focus;", true);
                        return;
                    }

                    else
                    {
                        if (Session["GroupSessionId"] == null)
                        {
                            Session["GroupSessionId"] = OTPNumb;
                        }

                        ParameterName = new ArrayList();
                        ParameterValue = new ArrayList();
                        spName = "Proc_BookLR";
                        ParameterName.Add("@Action"); ParameterValue.Add("InsertTMP");
                        ParameterName.Add("@BookType"); ParameterValue.Add(ddVType.SelectedValue);
                        ParameterName.Add("@BookLrNo"); ParameterValue.Add(TxtLrNo.Text);
                        ParameterName.Add("@BookDate"); ParameterValue.Add(txtdate.Text);
                        ParameterName.Add("@SourceFromid"); ParameterValue.Add(ddSourceFrom.SelectedValue);
                        ParameterName.Add("@SourceToid"); ParameterValue.Add(ddSourceTo.SelectedValue);
                        ParameterName.Add("@BookDestination"); ParameterValue.Add(txtDestination.Text);
                        ParameterName.Add("@BookConsiginorid"); ParameterValue.Add(ddConsignor.SelectedValue);
                        ParameterName.Add("@BookConsigieeid"); ParameterValue.Add(ddConsignee.SelectedValue);
                        ParameterName.Add("@BookPartyid"); ParameterValue.Add(0);
                        ParameterName.Add("@BookVchid"); ParameterValue.Add(ddlVchNo.SelectedValue);
                        ParameterName.Add("@BookInvNo"); ParameterValue.Add(txtinvno.Text);
                        ParameterName.Add("@BookInvDate"); ParameterValue.Add(txtinvdate.Text);
                        ParameterName.Add("@BookAmount"); ParameterValue.Add(txtAmount.Text);
                        ParameterName.Add("@BookEwayBillNo"); ParameterValue.Add(txtEwayBillNo.Text);
                        ParameterName.Add("@BookFromDate"); ParameterValue.Add(txtfromdate.Text);
                        ParameterName.Add("@BookValidDate"); ParameterValue.Add(txttodate.Text);
                        ParameterName.Add("@BookShipNo"); ParameterValue.Add(txtShipNo.Text);
                        ParameterName.Add("@BookDocNo"); ParameterValue.Add(txtDocNo.Text);
                        ParameterName.Add("@BookLoading"); ParameterValue.Add(txtLoading.Text);
                        ParameterName.Add("@BookRemarks"); ParameterValue.Add(txtRemarks.Text);
                        ParameterName.Add("@ProdName"); ParameterValue.Add(txtProduct.Text.Trim());
                        ParameterName.Add("@UnitName"); ParameterValue.Add(ddlunit.SelectedValue);
                        ParameterName.Add("@BookQty"); ParameterValue.Add(txtQty.Text);
                        ParameterName.Add("@ActQty"); ParameterValue.Add(0);
                        ParameterName.Add("@BookBranchid"); ParameterValue.Add(hfbrid.Value);
                        ParameterName.Add("@BookDriverMobileNo"); ParameterValue.Add(txtDriverMobile.Text);
                        ParameterName.Add("@BookCustSize"); ParameterValue.Add(ddlSize.SelectedValue);
                        ParameterName.Add("@BookRemark1"); ParameterValue.Add(txtRemarks1.Text);
                        ParameterName.Add("@DriverName"); ParameterValue.Add(txtDriverName.Text);
                        ParameterName.Add("@SealNo"); ParameterValue.Add(txtsealno.Text);
                        ParameterName.Add("@BookBillTo"); ParameterValue.Add(ddlBillto.SelectedValue);
                        ParameterName.Add("@Mtype"); ParameterValue.Add(ddlMode.SelectedValue);
                        ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                        ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                        ParameterName.Add("@DriverUID"); ParameterValue.Add(txtDriverAdhar.Text);
                        ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                        IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                        if (IntResult > 0)
                        {
                            string MaxBookId1 = MaxBookId();
                            spName = "Proc_DocumentDetails";

                            if (txtDocumentNo1.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo1.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtDocumentNo2.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo2.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtDocumentNo3.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo3.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtDocumentNo4.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo4.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtDocumentNo5.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@DocumentNo"); ParameterValue.Add(txtDocumentNo5.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                          

                            spName = "Proc_InvoiceDetails";
                            if (txtInvoiceNo1.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo1.Text);
                                ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate1.Text);
                                ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue1.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtInvoiceNo2.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo2.Text);
                                ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate2.Text);
                                ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue2.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtInvoiceNo3.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo3.Text);
                                ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate3.Text);
                                ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue3.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtInvoiceNo4.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo4.Text);
                                ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate4.Text);
                                ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue4.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtInvoiceNo5.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@InvoiceNo"); ParameterValue.Add(txtInvoiceNo5.Text);
                                ParameterName.Add("@InvoiceDate"); ParameterValue.Add(InvoiceDate5.Text);
                                ParameterName.Add("@InvoiceValue"); ParameterValue.Add(InvoiceValue5.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                         

                            spName = "Proc_EwayBillDetails";
                            if (txtEwayBillNo1.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo1.Text);
                                ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate1.Text);
                                ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate1.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtEwayBillNo2.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo2.Text);
                                ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate2.Text);
                                ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate2.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtEwayBillNo3.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo3.Text);
                                ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate3.Text);
                                ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate3.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtEwayBillNo4.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo4.Text);
                                ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate4.Text);
                                ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate4.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            if (txtEwayBillNo5.Text != "")
                            {
                                ParameterName = new ArrayList();
                                ParameterValue = new ArrayList();
                                ParameterName.Add("@Action"); ParameterValue.Add("InsertTmp");
                                ParameterName.Add("@LrNo"); ParameterValue.Add(TxtLrNo.Text);
                                ParameterName.Add("@EwayBillNo"); ParameterValue.Add(txtEwayBillNo5.Text);
                                ParameterName.Add("@BillDate"); ParameterValue.Add(txtEwayBillDate5.Text);
                                ParameterName.Add("@BillUptoDate"); ParameterValue.Add(txtValidUptoDate5.Text);
                                ParameterName.Add("@GroupId"); ParameterValue.Add(Convert.ToInt32(Session["GroupSessionId"]));
                                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
                            }
                            // 6
                           
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data SuccessFully inserted!');", true);
                            if (txtEwayBillNo.Text.Trim() != "")
                            {
                                Session["PrintBookId"] = MaxBookId();
                            }
                            ClearForMulti();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string msg;
                if (ddlMode.SelectedIndex == 2 || ddlMode.SelectedIndex == 3)
                {
                    if (Session["GroupSessionId"] == null && HFid.Value == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to add atleast one LR');", true);
                        return;
                    }
                }

                if (ddlMode.SelectedIndex != 2 && ddlMode.SelectedIndex != 3)
                {
                    if (txtDriverMobile.Text.Length != 10)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Driver Mobile Must be 10 Digit!');", true);
                        return;
                    }

                    // Invoice No check
                    if (txtInvoiceNo1.Text == "" && txtInvoiceNo2.Text == "" && txtInvoiceNo3.Text == "" && txtInvoiceNo4.Text == "" && txtInvoiceNo5.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter atlest one Invoice No');", true);
                        return;
                    }

                    // EwayBill No check
                    if (txtEwayBillNo1.Text == "" && txtEwayBillNo2.Text == "" && txtEwayBillNo3.Text == "" && txtEwayBillNo4.Text == "" && txtEwayBillNo5.Text == "" )
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter atlest one LR No');", true);
                        return;
                    }
                    if (ValidateShipment() == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Shipment Number must be Entered!');",  true);
                        return;
                    }


                    // Document No check
                    if (txtDocumentNo1.Text == "" && txtDocumentNo2.Text == "" && txtDocumentNo3.Text == "" && txtDocumentNo4.Text == "" && txtDocumentNo5.Text == "" )
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter atlest one Document No');", true);
                        return;
                    }


                    if (txtInvoiceNo1.Text != "")
                    {
                        if (InvoiceDate1.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                            return;
                        }
                        if (InvoiceValue1.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                            return;
                        }
                    }
                    if (txtInvoiceNo2.Text != "")
                    {
                        if (InvoiceDate2.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                            return;
                        }
                        if (InvoiceValue2.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                            return;
                        }
                    }
                    if (txtInvoiceNo3.Text != "")
                    {
                        if (InvoiceDate3.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                            return;
                        }
                        if (InvoiceValue3.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                            return;
                        }
                    }
                    if (txtInvoiceNo4.Text != "")
                    {
                        if (InvoiceDate4.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                            return;
                        }
                        if (InvoiceValue4.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                            return;
                        }
                    }
                    if (txtInvoiceNo5.Text != "")
                    {
                        if (InvoiceDate5.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Date');", true);
                            return;
                        }
                        if (InvoiceValue5.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Invoice Value');", true);
                            return;
                        }
                    }
                  

                    if (txtEwayBillNo1.Text != "")
                    {
                        if (txtValidUptoDate1.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                            return;
                        }
                        if (txtEwayBillDate1.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo2.Text != "")
                    {
                        if (txtValidUptoDate2.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                            return;
                        }
                        if (txtEwayBillDate2.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo3.Text != "")
                    {
                        if (txtValidUptoDate3.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                            return;
                        }
                        if (txtEwayBillDate3.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo4.Text != "")
                    {
                        if (txtValidUptoDate4.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                            return;
                        }
                        if (txtEwayBillDate4.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo5.Text != "")
                    {
                        if (txtValidUptoDate5.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Valid Upto Date');", true);
                            return;
                        }
                        if (txtEwayBillDate5.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('You must have to Enter Eway Bill Date');", true);
                            return;
                        }
                    }
                    // 6
                    


                    int Count = 0;
                    int Count1 = 0;
                    if (ddlMode.SelectedIndex == 0)
                    {
                        Count = GetLRCount();
                    }
                    else if (ddlMode.SelectedIndex == 1)
                    {
                        Count = GetMnaualLRCount();
                    }
                    else if (ddlMode.SelectedIndex == 3)
                    {
                        Count = GetLRCountMBT();
                        if (Count == 0)
                        {
                            Count = GetMnaualLRCount();
                        }
                    }
                    else
                    {
                        Count = GetLRCount();
                        if (Count == 0)
                        {
                            Count = GetMnaualLRCount();
                        }
                    }

                    Count1 = GetLCCount();

                    if (CheckInvoiceValue() == 1)
                    {
                        return;
                    }

                    if (HFid.Value == "")
                    {
                        if (CheckDuplicateLR() > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LR No!');", true);
                            return;
                        }
                    }
                    else
                    {
                        if (CheckDuplicateLR1() > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LR No!');", true);
                            return;
                        }
                    }

                    if (HFid.Value == "")
                    {
                        if (CheckDuplicateLC() > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LC No!');", true);
                            return;
                        }
                    }
                    else
                    {
                        if (CheckDuplicateLC1() > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Data Already Exits on this LC No!');", true);
                            return;
                        }
                    }


                    if (Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for Consignment No!');TxtLrNo.focus;", true);
                        return;
                    }

                    if (Count1 == 0 && txtLCNo.Text != "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('please contact to admin for lc no!');txtlcno.focus;", true);
                        return;
                    }
                }

                if (ddlMode.SelectedIndex != 2 && ddlMode.SelectedIndex != 3)
                {
                    if (txtEwayBillNo1.Text != "")
                    {
                        if (txtEwayBillNo1.Text.Length != 12)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo2.Text != "")
                    {
                        if (txtEwayBillNo2.Text.Length != 12)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo3.Text != "")
                    {
                        if (txtEwayBillNo3.Text.Length != 12)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo4.Text != "")
                    {
                        if (txtEwayBillNo4.Text.Length != 12)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                            return;
                        }
                    }
                    if (txtEwayBillNo5.Text != "")
                    {
                        if (txtEwayBillNo5.Text.Length != 12)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('E-Way Bill No Must be 12 Digit!');", true);
                            return;
                        }
                    }
                    
                }

                if ((cm.GetUserDetails().Rows[0]["Mobile"]).ToString() == "")
                {
                    msg = "alert('Please Input User No and again login.');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", msg, true);
                    return;
                }

                string OTPNumb = cm.FiveDigits();
                if (Convert.ToInt32((cm.GetCompanyData()).Rows[0]["isSMSServiceEnabled"]) == 0)
                {
                    Session["OTP"] = OTPNumb;
                    txtCode.Text = Session["OTP"].ToString();
                }
                else
                {
                    string MsgRes = cm.SendOTP(OTPNumb, (cm.GetUserDetails().Rows[0]["Mobile"]).ToString());
                    if (MsgRes == "SUCCESS" || ((MsgRes != "SUCCESS") && (Session["UserType"].ToString() == "A")))
                    {
                        msg = "alert('OTP Sent.Please check your Mobile');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", msg, true);
                        Session["OTP"] = OTPNumb;
                    }
                    else
                    {
                        msg = "alert('Send OTP Failed');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", msg, true);
                        Session["OTP"] = OTPNumb;
                    }
                }
                divsave.Visible = true;
                btnSaveLocal.Visible = false;
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP: | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        protected void ddConsignor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddConsignor.SelectedIndex > 0)
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                string spName = "Proc_Consign";
                try
                {
                    ParameterName.Add("@Action"); ParameterValue.Add("Get");
                    ParameterName.Add("@Id"); ParameterValue.Add(ddConsignor.SelectedValue);
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblConsignorAddress.Text = dt.Rows[0]["ConsgAdd"].ToString();
                        lblConsignorGST.Text = dt.Rows[0]["ConsgGSTNo"].ToString();
                        lblConsignorState.Text = dt.Rows[0]["ConsState"].ToString();
                        ddSourceFrom.SelectedValue = dt.Rows[0]["ConsSource"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
        }
        protected void ddlVchNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_Vehicle";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(ddlVchNo.SelectedValue);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblVchSize.Text = dt.Rows[0]["Size"].ToString();
                }

                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                spName = "Proc_VajraLock";
                ParameterName.Add("@Action"); ParameterValue.Add("GetByVehicleNo");
                ParameterName.Add("@VehicleNo"); ParameterValue.Add(ddlVchNo.SelectedItem.Text);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Vajra Lock Already Installed In This Vehicle');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Vajra Lock is not installed in this vehicle');", true);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }

        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() != "")
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                string spName = "Proc_Vehicle";
                try
                {
                    ParameterName.Add("@Action"); ParameterValue.Add("ListByVehicleNo");
                    ParameterName.Add("@VehicleNo"); ParameterValue.Add(TextBox1.Text.Trim());
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ddlVchNo.DataSource = dt;
                        ddlVchNo.DataValueField = "Id";
                        ddlVchNo.DataTextField = "VehicleNo";
                        ddlVchNo.DataBind();
                        ddlVchNo.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

                        lblMessage.Text = "Total Result " + dt.Rows.Count.ToString();
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
        }
        protected void TxtLrNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int Count = 0;

                if (ddlMode.SelectedIndex == 0)
                {
                    Count = GetLRCount();
                }
                else if (ddlMode.SelectedIndex == 1)
                {
                    Count = GetMnaualLRCount();
                }
                else if (ddlMode.SelectedIndex == 3)
                {
                    Count = GetLRCountMBT();
                    if (Count == 0)
                    {
                        Count = GetMnaualLRCount();
                    }
                }
                else
                {
                    Count = GetLRCount();
                    if (Count == 0)
                    {
                        Count = GetMnaualLRCount();
                    }
                }

                if (Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('Please Contact To Admin for Consignment No!');TxtLrNo.focus;", true);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }
        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ddlMode.SelectedIndex == 0 || ddlMode.SelectedIndex == 2)
                if (ddlMode.SelectedIndex == 0)
                {
                    txtdate.Enabled = false;
                    txtdate.Text = cm.GetCurrentDate();
                    lblcongo.Text = GetLastLrNo();
                }
                else
                {
                    txtdate.Enabled = true;
                }

                if (ddlMode.SelectedIndex == 1 || ddlMode.SelectedIndex == 2)
                {
                    lblcongo.Text = GetLastLrNo();
                }

                if (ddlMode.SelectedIndex == 3)
                {
                    lblcongo.Text = GetLastLrNoMBT();
                }


                if (ddlMode.SelectedIndex == 2 || ddlMode.SelectedIndex == 3)
                {
                    btnSaveLocal.Visible = true;
                }
                else
                {
                    btnSaveLocal.Visible = false;
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
            }
        }

        /*
        protected void txtLCNo_TextChanged(object sender, EventArgs e)
        {
            if(GetLCCount() == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('please contact to admin for lc no!');txtlcno.focus;", true);
            }
        }
        */
        protected void ddConsignee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddConsignee.SelectedIndex > 0)
            {
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                string spName = "Proc_Consign";
                try
                {
                    ParameterName.Add("@Action"); ParameterValue.Add("Get");
                    ParameterName.Add("@Id"); ParameterValue.Add(ddConsignee.SelectedValue);
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblConsigneeAddress.Text = dt.Rows[0]["ConsgAdd"].ToString();
                        lblConsigneeGst.Text = dt.Rows[0]["ConsgGSTNo"].ToString();
                        lblConsigneeState.Text = dt.Rows[0]["ConsState"].ToString();
                        ddSourceTo.SelectedValue = dt.Rows[0]["ConsSource"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
        }
        #endregion
    }
}