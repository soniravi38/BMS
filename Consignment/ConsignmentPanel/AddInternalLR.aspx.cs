using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Xml.Linq;
using Consignment.Model;

namespace Consignment.ConsignmentPanel
{
    public partial class AddInternalLR : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();
        const string spName = "[Proc_InternalLR]";
        #endregion

        #region [PageLoad]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Database_"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                if (Session["UserType"].ToString() != "A")
                {
                    if (ValidateUserRights("InternalLR") == false)
                    {
                        return;
                    }
                }
                FillOwnCompany();
                if (Request.QueryString["Id"] != null)
                {
                    lblHeading.Text = "Update Internal LR / Settlement";
                    btnSubmit.Text = "Update";
                    pnlSelectLR.Visible = false;
                    pnlDetails.Visible = true;
                    FillData(Convert.ToInt32(Request.QueryString["Id"]));
                }
                else
                {
                    lblHeading.Text = "Create Internal LR / Settlement";
                    btnSubmit.Text = "Create Internal LR";
                    pnlSelectLR.Visible = true;
                    pnlDetails.Visible = false;
                    hfPartnerDb.Value = cm.GetPartnerDatabaseKey();
                    if (hfPartnerDb.Value == "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('The partner company database is not configured in web.config.');", true);
                        return;
                    }
                    FillPartnerCompany();
                    txtLRDateFrom.Text = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
                    txtLRDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    InitVoucherTable();
                    SetButtonState();
                    FillPartnerLR();
                }
            }
        }
        #endregion

        #region [Function]
        private bool ValidateUserRights(string Module)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string sp = "Proc_UserRights";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetByUserId");
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                ds = con.GetDataSetFromSP(sp, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Select("MenuName = '" + Module + "'").CopyToDataTable().Rows[0];
                    string Right = (Request.QueryString["Id"] != null) ? "UPDATE" : "CREATE";
                    if (Convert.ToInt32(dr["LIST"]) != 1 || Convert.ToInt32(dr[Right]) != 1)
                    {
                        Response.Redirect("Dashboard.aspx", false);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, sp);
                Response.Redirect("Dashboard.aspx", false);
                return false;
            }
            return true;
        }

        private void WriteError(Exception ex, string sp)
        {
            cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name + " | SP:" + sp + " | User:" + Convert.ToString(Session["UserName"]) + " | Err:" + ex.Message.ToString());
        }

        private void ShowMessage(string Msg)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg.Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ") + "');", true);
        }

        private decimal ToDec(string Value)
        {
            decimal d = 0;
            decimal.TryParse(Convert.ToString(Value).Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out d);
            return d;
        }

        private string ToDateText(string Value, string Format)
        {
            // yyyy-MM-dd -> Format
            DateTime d;
            if (DateTime.TryParseExact(Convert.ToString(Value), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                return d.ToString(Format);
            }
            return Convert.ToString(Value);
        }

        private void FillOwnCompany()
        {
            try
            {
                DataTable dtC = cm.GetCompanyData();
                if (dtC.Rows.Count > 0)
                {
                    lblOwnCompany.Text = dtC.Rows[0]["CompanyName"].ToString();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, "Proc_CompanyMaster");
            }
        }

        // Partner (LR) company name, address and GSTIN - used for Bill To
        private void FillPartnerCompany()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string sp = "Proc_CompanyMaster";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ds = con.GetDataSetFromSPByDatabase(hfPartnerDb.Value, sp, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    hfPartnerCompany.Value = dr["CompanyName"].ToString();
                    lblPartnerCompany.Text = dr["CompanyName"].ToString();
                    lblBillToCompany.Text = dr["CompanyName"].ToString();
                    txtBillToName.Text = dr["CompanyName"].ToString();
                    txtConsignorName.Text = dr["CompanyName"].ToString();
                    string Address = Convert.ToString(dr["Address1"]).Trim();
                    if (Convert.ToString(dr["Address2"]).Trim() != "")
                    {
                        Address += Environment.NewLine + Convert.ToString(dr["Address2"]).Trim();
                    }
                    txtBillToAddress.Text = Address;
                    txtBillToGSTIN.Text = dr["GSTIN"].ToString();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, sp + " (Partner:" + hfPartnerDb.Value + ")");
            }
        }

        // Partner LRs executed with our OWN vehicles for which an Internal LR has not been created
        private void FillPartnerLR()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            try
            {
                // 1. Own vehicles
                HashSet<string> OwnVehicles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                ParameterName.Add("@Action"); ParameterValue.Add("OwnVehicleList");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    OwnVehicles.Add(dr["VehicleNo"].ToString());
                }

                // 2. Already created
                HashSet<string> Created = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                ParameterName.Add("@Action"); ParameterValue.Add("CreatedPartnerLR");
                ParameterName.Add("@PartnerDb"); ParameterValue.Add(hfPartnerDb.Value);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Created.Add(dr["PartnerLRNo"].ToString().Trim());
                }

                // 3. Partner LR list
                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                ParameterName.Add("@Action"); ParameterValue.Add("PartnerLRList");
                ParameterName.Add("@FromDate"); ParameterValue.Add(txtLRDateFrom.Text);
                ParameterName.Add("@ToDate"); ParameterValue.Add(txtLRDateTo.Text);
                ParameterName.Add("@BookLrNo"); ParameterValue.Add(txtSearchLRNo.Text.Trim());
                ParameterName.Add("@SearchVehicleNo"); ParameterValue.Add(txtSearchVehicle.Text.Trim());
                ds = con.GetDataSetFromSPByDatabase(hfPartnerDb.Value, spName, ParameterName, ParameterValue);
                DataTable dtLR = ds.Tables[0];

                DataTable dtResult = dtLR.Clone();
                foreach (DataRow dr in dtLR.Rows)
                {
                    if (Created.Contains(dr["BookLrNo"].ToString().Trim()))
                    {
                        continue;
                    }
                    if (chkOwnVehicleOnly.Checked && !OwnVehicles.Contains(dr["VehicleNo"].ToString().Replace(" ", "").ToUpper()))
                    {
                        continue;
                    }
                    dtResult.ImportRow(dr);
                }
                gvPartnerLR.DataSource = dtResult;
                gvPartnerLR.DataBind();
                lblLRCount.Text = dtResult.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                WriteError(ex, spName + " (Partner:" + hfPartnerDb.Value + ")");
                ShowMessage("Unable to load LR data of the partner company. Please check the error log.");
            }
        }

        // Auto-fill the form with the selected partner LR
        private void LoadPartnerLR(int BookId)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("PartnerLRGet");
                ParameterName.Add("@BookId"); ParameterValue.Add(BookId);
                ds = con.GetDataSetFromSPByDatabase(hfPartnerDb.Value, spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    hfPartnerLRId.Value = dr["Bookid"].ToString();
                    txtPartnerLRNo.Text = dr["BookLrNo"].ToString();
                    txtPartnerLRDate.Text = dr["BookDate"].ToString();
                    txtVehicleNo.Text = dr["VehicleNo"].ToString();
                    txtFromStation.Text = dr["FromStation"].ToString();
                    txtToStation.Text = dr["ToStation"].ToString();
                    txtPackages.Text = dr["BookQty"].ToString();
                    txtConsigneeName.Text = dr["ConsigneeName"].ToString();
                    txtProdName.Text = dr["ProdName"].ToString();
                    txtUnitName.Text = dr["UnitName"].ToString();
                    txtLRInvoiceNo.Text = dr["InvoiceNo"].ToString();
                    txtLREwayBillNo.Text = dr["EwayBillNo"].ToString();
                    txtLCNo.Text = dr["LCNo"].ToString();
                    txtLRBillTo.Text = dr["BillTo"].ToString();
                    hfLRConsignorName.Value = dr["ConsignorName"].ToString();
                    hfConsigneeAddress.Value = dr["ConsigneeAddress"].ToString();
                    hfDestination.Value = dr["BookDestination"].ToString();
                    hfLRDocNo.Value = dr["BookDocNo"].ToString();
                    hfLRShipNo.Value = dr["BookShipNo"].ToString();
                    hfPartnerFreight.Value = ToDec(dr["LCFreight"].ToString()).ToString("0.00", CultureInfo.InvariantCulture);

                    // Crossing Note date defaults to the LR date (as per reference format)
                    txtCNDate.Text = dr["BookDate"].ToString();
                    txtBillDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtBillInvoiceNo.Text = dr["BookLrNo"].ToString();
                    txtBillAmount.Text = hfPartnerFreight.Value;
                    lblFreightInfo.Text = ToDec(hfPartnerFreight.Value) > 0
                        ? "LC freight Rs. " + hfPartnerFreight.Value + " has been pre-filled and can be changed."
                        : "LC freight not found. The amount can be entered later (Draft Bill).";

                    // Voucher date window : LR date minus 3 days to LR date plus 7 days
                    DateTime LRDate;
                    if (DateTime.TryParseExact(txtPartnerLRDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out LRDate))
                    {
                        txtVchDateFrom.Text = LRDate.AddDays(-3).ToString("yyyy-MM-dd");
                        txtVchDateTo.Text = LRDate.AddDays(7).ToString("yyyy-MM-dd");
                        txtOtherDate.Text = LRDate.ToString("yyyy-MM-dd");
                    }

                    lblChain.Text = hfPartnerCompany.Value + " LR No. " + txtPartnerLRNo.Text + "  &rarr;  " + lblOwnCompany.Text + " Vehicle " + txtVehicleNo.Text
                        + "  &rarr;  Crossing Note (Auto)  &rarr;  Voucher(s)  &rarr;  Bill (Auto)";

                    InitVoucherTable();
                    FetchVouchers();
                    pnlSelectLR.Visible = false;
                    pnlDetails.Visible = true;
                    SetButtonState();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, spName + " (Partner:" + hfPartnerDb.Value + ")");
                ShowMessage("Unable to load LR details. Please check the error log.");
            }
        }

        // Edit mode
        private void FillData(int Id)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    hfPartnerDb.Value = dr["PartnerDb"].ToString();
                    hfPartnerCompany.Value = dr["PartnerCompany"].ToString();
                    hfPartnerLRId.Value = dr["PartnerLRId"].ToString();
                    lblBillToCompany.Text = dr["PartnerCompany"].ToString();
                    txtCNNo.Text = dr["CNNo"].ToString();
                    txtCNDate.Text = dr["CNDate"].ToString();
                    txtPartnerLRNo.Text = dr["PartnerLRNo"].ToString();
                    txtPartnerLRDate.Text = dr["PartnerLRDate"].ToString();
                    txtVehicleNo.Text = dr["VehicleNo"].ToString();
                    txtFromStation.Text = dr["FromStation"].ToString();
                    txtToStation.Text = dr["ToStation"].ToString();
                    txtPackages.Text = dr["Packages"].ToString();
                    txtConsignorName.Text = dr["ConsignorName"].ToString();
                    txtConsigneeName.Text = dr["LRConsigneeName"].ToString();
                    txtProdName.Text = dr["ProdName"].ToString();
                    txtUnitName.Text = dr["UnitName"].ToString();
                    txtLRInvoiceNo.Text = dr["LRInvoiceNo"].ToString();
                    txtLREwayBillNo.Text = dr["LREwayBillNo"].ToString();
                    txtLCNo.Text = dr["LCNo"].ToString();
                    txtLRBillTo.Text = dr["LRBillTo"].ToString();
                    txtRoadPermitNo.Text = dr["RoadPermitNo"].ToString();
                    txtCNRemarks.Text = dr["CNRemarks"].ToString();
                    hfLRConsignorName.Value = dr["LRConsignorName"].ToString();
                    hfConsigneeAddress.Value = dr["LRConsigneeAddress"].ToString();
                    hfDestination.Value = dr["Destination"].ToString();
                    hfLRDocNo.Value = dr["LRDocNo"].ToString();
                    hfLRShipNo.Value = dr["LRShipNo"].ToString();
                    hfPartnerFreight.Value = ToDec(dr["PartnerFreight"].ToString()).ToString("0.00", CultureInfo.InvariantCulture);
                    hfWeight.Value = dr["Weight"].ToString();

                    txtBillNo.Text = dr["BillNo"].ToString();
                    txtBillDate.Text = dr["BillDate"].ToString();
                    txtBillAmount.Text = ToDec(dr["BillAmount"].ToString()).ToString("0.00", CultureInfo.InvariantCulture);
                    txtBillWeight.Text = dr["BillWeight"].ToString();
                    txtBillRate.Text = dr["BillRate"].ToString();
                    txtBillToName.Text = dr["BillToName"].ToString();
                    txtBillToAddress.Text = dr["BillToAddress"].ToString();
                    txtBillToGSTIN.Text = dr["BillToGSTIN"].ToString();
                    txtBillInvoiceNo.Text = dr["BillInvoiceNo"].ToString();
                    txtBillDocNo.Text = dr["BillDocNo"].ToString();
                    txtBillPONo.Text = dr["BillPONo"].ToString();
                    txtBillEwayBillNo.Text = dr["BillEwayBillNo"].ToString();
                    txtBillRemarks.Text = dr["BillRemarks"].ToString();
                    hfBillStatus.Value = dr["BillStatus"].ToString() == "F" ? "F" : "D";
                    lblFreightInfo.Text = ToDec(hfPartnerFreight.Value) > 0 ? "Freight as per the original LC: Rs. " + hfPartnerFreight.Value : "";

                    DateTime LRDate;
                    if (DateTime.TryParseExact(txtPartnerLRDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out LRDate))
                    {
                        txtVchDateFrom.Text = LRDate.AddDays(-3).ToString("yyyy-MM-dd");
                        txtVchDateTo.Text = LRDate.AddDays(7).ToString("yyyy-MM-dd");
                        txtOtherDate.Text = LRDate.ToString("yyyy-MM-dd");
                    }

                    lblChain.Text = hfPartnerCompany.Value + " LR No. " + txtPartnerLRNo.Text + "  &rarr;  Crossing Note No. " + txtCNNo.Text
                        + "  &rarr;  Voucher(s)  &rarr;  Bill No. " + txtBillNo.Text;

                    // Saved vouchers
                    InitVoucherTable();
                    DataTable dtV = (DataTable)ViewState["dtVoucher"];
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow v in ds.Tables[1].Rows)
                        {
                            DataRow n = dtV.NewRow();
                            n["RowKey"] = "V_" + v["VoucherId"].ToString();
                            n["VoucherId"] = Convert.ToInt32(v["VoucherId"]);
                            n["VoucherNo"] = v["VoucherNo"].ToString();
                            n["VoucherType"] = v["VoucherType"].ToString();
                            n["SourceType"] = v["SourceType"].ToString();
                            n["SourceId"] = Convert.ToInt32(v["SourceId"] == DBNull.Value ? 0 : v["SourceId"]);
                            n["VoucherDate"] = v["VoucherDate"].ToString();
                            n["VoucherDateDisp"] = v["VoucherDate1"].ToString();
                            n["Amount"] = ToDec(v["Amount"].ToString());
                            n["Through"] = v["Through"].ToString();
                            n["Station"] = v["Station"].ToString();
                            n["Remarks"] = v["Remarks"].ToString();
                            n["Selected"] = true;
                            dtV.Rows.Add(n);
                        }
                    }
                    ViewState["dtVoucher"] = dtV;
                    BindVoucherGrid();
                    SetButtonState();
                }
                else
                {
                    Response.Redirect("ListInternalLR.aspx", false);
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, spName);
            }
        }

        private void InitVoucherTable()
        {
            DataTable dtV = new DataTable();
            dtV.Columns.Add("RowKey", typeof(string));
            dtV.Columns.Add("VoucherId", typeof(int));
            dtV.Columns.Add("VoucherNo", typeof(string));
            dtV.Columns.Add("VoucherType", typeof(string));
            dtV.Columns.Add("SourceType", typeof(string));
            dtV.Columns.Add("SourceId", typeof(int));
            dtV.Columns.Add("VoucherDate", typeof(string));
            dtV.Columns.Add("VoucherDateDisp", typeof(string));
            dtV.Columns.Add("Amount", typeof(decimal));
            dtV.Columns.Add("Through", typeof(string));
            dtV.Columns.Add("Station", typeof(string));
            dtV.Columns.Add("Remarks", typeof(string));
            dtV.Columns.Add("Selected", typeof(bool));
            ViewState["dtVoucher"] = dtV;
            BindVoucherGrid();
        }

        // Write the grid checkbox state back to the ViewState table
        private void SyncVoucherSelection()
        {
            DataTable dtV = (DataTable)ViewState["dtVoucher"];
            if (dtV == null)
            {
                return;
            }
            foreach (GridViewRow row in gvVoucher.Rows)
            {
                string Key = gvVoucher.DataKeys[row.RowIndex].Value.ToString();
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                DataRow[] found = dtV.Select("RowKey = '" + Key.Replace("'", "''") + "'");
                if (found.Length > 0 && chk != null)
                {
                    found[0]["Selected"] = chk.Checked;
                }
            }
            ViewState["dtVoucher"] = dtV;
        }

        private void BindVoucherGrid()
        {
            DataTable dtV = (DataTable)ViewState["dtVoucher"];
            gvVoucher.DataSource = dtV;
            gvVoucher.DataBind();

            decimal Diesel = 0, Cash = 0, Other = 0;
            if (dtV != null)
            {
                foreach (DataRow r in dtV.Rows)
                {
                    if (Convert.ToBoolean(r["Selected"]) == false)
                    {
                        continue;
                    }
                    decimal Amt = Convert.ToDecimal(r["Amount"]);
                    if (r["VoucherType"].ToString() == "DIESEL") Diesel += Amt;
                    else if (r["VoucherType"].ToString() == "CASH") Cash += Amt;
                    else Other += Amt;
                }
            }
            lblDieselTotal.Text = Diesel.ToString("0.00", CultureInfo.InvariantCulture);
            lblCashTotal.Text = Cash.ToString("0.00", CultureInfo.InvariantCulture);
            lblOtherTotal.Text = Other.ToString("0.00", CultureInfo.InvariantCulture);
            lblVoucherTotal.Text = (Diesel + Cash + Other).ToString("0.00", CultureInfo.InvariantCulture);
        }

        // Fetch existing Diesel / Cash entries and merge them into the voucher list
        private void FetchVouchers()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            try
            {
                SyncVoucherSelection();
                DataTable dtV = (DataTable)ViewState["dtVoucher"];
                string Station = "";
                DataTable dtUser = cm.GetUserDetails();
                if (dtUser.Rows.Count > 0)
                {
                    Station = dtUser.Rows[0]["BranchName"].ToString();
                }

                ParameterName.Add("@Action"); ParameterValue.Add("FetchVouchers");
                ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text.Trim());
                ParameterName.Add("@PartnerLRNo"); ParameterValue.Add(txtPartnerLRNo.Text.Trim());
                ParameterName.Add("@FromDate"); ParameterValue.Add(txtVchDateFrom.Text);
                ParameterName.Add("@ToDate"); ParameterValue.Add(txtVchDateTo.Text);
                ParameterName.Add("@Station"); ParameterValue.Add(Station);
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                int Added = 0;
                foreach (DataRow v in ds.Tables[0].Rows)
                {
                    string SType = v["SourceType"].ToString();
                    int SId = Convert.ToInt32(v["SourceId"]);
                    // Skip if already in the list
                    if (dtV.Select("SourceType = '" + SType + "' AND SourceId = " + SId).Length > 0)
                    {
                        continue;
                    }
                    DataRow n = dtV.NewRow();
                    n["RowKey"] = SType + "_" + SId.ToString();
                    n["VoucherId"] = 0;
                    n["VoucherNo"] = "New";
                    n["VoucherType"] = v["VoucherType"].ToString();
                    n["SourceType"] = SType;
                    n["SourceId"] = SId;
                    n["VoucherDate"] = v["VoucherDate"].ToString();
                    n["VoucherDateDisp"] = ToDateText(v["VoucherDate"].ToString(), "dd-MM-yyyy");
                    n["Amount"] = ToDec(v["Amount"].ToString());
                    n["Through"] = v["Through"].ToString();
                    n["Station"] = v["Station"].ToString();
                    n["Remarks"] = v["Remarks"].ToString();
                    n["Selected"] = true;
                    dtV.Rows.Add(n);
                    Added++;
                }
                ViewState["dtVoucher"] = dtV;
                BindVoucherGrid();
                if (IsPostBack && Added == 0)
                {
                    ShowMessage("No new Diesel or Cash entry was found for this vehicle and date range.");
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, spName);
            }
        }

        private string BuildVoucherXml()
        {
            DataTable dtV = (DataTable)ViewState["dtVoucher"];
            XElement Root = new XElement("V");
            if (dtV != null)
            {
                foreach (DataRow r in dtV.Rows)
                {
                    if (Convert.ToBoolean(r["Selected"]) == false)
                    {
                        continue;
                    }
                    Root.Add(new XElement("R",
                        new XAttribute("VID", r["VoucherId"].ToString()),
                        new XAttribute("T", r["VoucherType"].ToString()),
                        new XAttribute("S", r["SourceType"].ToString()),
                        new XAttribute("SID", r["SourceId"].ToString()),
                        new XAttribute("DT", r["VoucherDate"].ToString()),
                        new XAttribute("AMT", Convert.ToDecimal(r["Amount"]).ToString("0.00", CultureInfo.InvariantCulture)),
                        new XAttribute("TH", Left(r["Through"].ToString(), 300)),
                        new XAttribute("ST", Left(r["Station"].ToString(), 100)),
                        new XAttribute("RM", Left(r["Remarks"].ToString(), 300))));
                }
            }
            return Root.ToString(SaveOptions.DisableFormatting);
        }

        private string Left(string Value, int Len)
        {
            Value = Convert.ToString(Value);
            return Value.Length > Len ? Value.Substring(0, Len) : Value;
        }

        // Lock the form for non-administrators once the Bill is final
        private void SetButtonState()
        {
            bool isEdit = Request.QueryString["Id"] != null;
            bool isAdmin = Convert.ToString(Session["UserType"]) == "A";
            bool isFinal = hfBillStatus.Value == "F";
            bool Locked = isFinal && !isAdmin;

            lblBillStatus.Text = isEdit ? (isFinal ? "<span class='ilr-status-F'>FINAL BILL</span>" : "<span class='ilr-status-D'>DRAFT BILL</span>") : "";

            btnSubmit.Visible = !Locked;
            btnSaveAndPrint.Visible = !Locked;
            btnFetchVoucher.Enabled = !Locked;
            btnAddOther.Enabled = !Locked;
            gvVoucher.Enabled = !Locked;
            txtBillAmount.ReadOnly = isFinal && !isAdmin;
            txtBillDate.ReadOnly = isFinal && !isAdmin;

            btnFinalizeBill.Visible = isEdit && !isFinal;
            btnReopenBill.Visible = isEdit && isFinal && isAdmin;
            btnPrintAll.Visible = isEdit;
            if (isEdit)
            {
                btnPrintAll.HRef = "InternalLRReportViewer.aspx?Id=" + Request.QueryString["Id"].ToString() + "&Print=All";
            }
        }

        // Save (Insert / Update). Return : Id (0 = fail)
        private int SaveData()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            int Id = 0;
            try
            {
                SyncVoucherSelection();
                if (txtPartnerLRNo.Text.Trim() == "")
                {
                    ShowMessage("Please select the original LR first.");
                    return 0;
                }
                if (ToDec(txtBillAmount.Text) < 0)
                {
                    ShowMessage("Please enter a valid Bill amount.");
                    return 0;
                }

                DataTable dtUser = cm.GetUserDetails();
                bool isEdit = Request.QueryString["Id"] != null;

                ParameterName.Add("@Action"); ParameterValue.Add(isEdit ? "Update" : "Insert");
                ParameterName.Add("@Id"); ParameterValue.Add(isEdit ? Convert.ToInt32(Request.QueryString["Id"]) : 0);
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                ParameterName.Add("@BranchId"); ParameterValue.Add(dtUser.Rows[0]["BranchId"].ToString());
                // Crossing note
                ParameterName.Add("@CNDate"); ParameterValue.Add(txtCNDate.Text);
                ParameterName.Add("@ConsignorName"); ParameterValue.Add(txtConsignorName.Text.Trim());
                ParameterName.Add("@RoadPermitNo"); ParameterValue.Add(txtRoadPermitNo.Text.Trim());
                ParameterName.Add("@CNRemarks"); ParameterValue.Add(txtCNRemarks.Text.Trim());
                ParameterName.Add("@PartnerDb"); ParameterValue.Add(hfPartnerDb.Value);
                ParameterName.Add("@PartnerCompany"); ParameterValue.Add(hfPartnerCompany.Value);
                ParameterName.Add("@PartnerLRId"); ParameterValue.Add(hfPartnerLRId.Value == "" ? "0" : hfPartnerLRId.Value);
                ParameterName.Add("@PartnerLRNo"); ParameterValue.Add(txtPartnerLRNo.Text.Trim());
                ParameterName.Add("@PartnerLRDate"); ParameterValue.Add(txtPartnerLRDate.Text);
                ParameterName.Add("@LRConsignorName"); ParameterValue.Add(Left(hfLRConsignorName.Value, 200));
                ParameterName.Add("@LRConsigneeName"); ParameterValue.Add(Left(txtConsigneeName.Text.Trim(), 200));
                ParameterName.Add("@LRConsigneeAddress"); ParameterValue.Add(Left(hfConsigneeAddress.Value, 500));
                ParameterName.Add("@FromStation"); ParameterValue.Add(txtFromStation.Text);
                ParameterName.Add("@ToStation"); ParameterValue.Add(txtToStation.Text);
                ParameterName.Add("@Destination"); ParameterValue.Add(hfDestination.Value);
                ParameterName.Add("@VehicleNo"); ParameterValue.Add(txtVehicleNo.Text);
                ParameterName.Add("@Packages"); ParameterValue.Add(((int)ToDec(txtPackages.Text)).ToString());
                ParameterName.Add("@UnitName"); ParameterValue.Add(txtUnitName.Text);
                ParameterName.Add("@ProdName"); ParameterValue.Add(Left(txtProdName.Text, 100));
                ParameterName.Add("@Weight"); ParameterValue.Add(hfWeight.Value);
                ParameterName.Add("@LRInvoiceNo"); ParameterValue.Add(Left(txtLRInvoiceNo.Text, 500));
                ParameterName.Add("@LREwayBillNo"); ParameterValue.Add(Left(txtLREwayBillNo.Text, 500));
                ParameterName.Add("@LRDocNo"); ParameterValue.Add(hfLRDocNo.Value);
                ParameterName.Add("@LRShipNo"); ParameterValue.Add(hfLRShipNo.Value);
                ParameterName.Add("@LRBillTo"); ParameterValue.Add(Left(txtLRBillTo.Text, 200));
                ParameterName.Add("@LCNo"); ParameterValue.Add(txtLCNo.Text);
                ParameterName.Add("@PartnerFreight"); ParameterValue.Add(ToDec(hfPartnerFreight.Value).ToString("0.00", CultureInfo.InvariantCulture));
                // Bill
                ParameterName.Add("@BillDate"); ParameterValue.Add(txtBillDate.Text);
                ParameterName.Add("@BillAmount"); ParameterValue.Add(ToDec(txtBillAmount.Text).ToString("0.00", CultureInfo.InvariantCulture));
                ParameterName.Add("@BillWeight"); ParameterValue.Add(txtBillWeight.Text.Trim());
                ParameterName.Add("@BillRate"); ParameterValue.Add(txtBillRate.Text.Trim());
                ParameterName.Add("@BillToName"); ParameterValue.Add(txtBillToName.Text.Trim());
                ParameterName.Add("@BillToAddress"); ParameterValue.Add(txtBillToAddress.Text.Trim());
                ParameterName.Add("@BillToGSTIN"); ParameterValue.Add(txtBillToGSTIN.Text.Trim());
                ParameterName.Add("@BillInvoiceNo"); ParameterValue.Add(txtBillInvoiceNo.Text.Trim());
                ParameterName.Add("@BillDocNo"); ParameterValue.Add(txtBillDocNo.Text.Trim());
                ParameterName.Add("@BillPONo"); ParameterValue.Add(txtBillPONo.Text.Trim());
                ParameterName.Add("@BillEwayBillNo"); ParameterValue.Add(txtBillEwayBillNo.Text.Trim());
                ParameterName.Add("@BillRemarks"); ParameterValue.Add(txtBillRemarks.Text.Trim());
                // Vouchers
                ParameterName.Add("@VoucherXml"); ParameterValue.Add(BuildVoucherXml());

                dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Id"]) > 0)
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    cm.SaveActivityLog("Internal LR", isEdit ? "Updated" : "Inserted", "CN No = " + dt.Rows[0]["CNNo"].ToString() + ", LR No = " + txtPartnerLRNo.Text);
                }
                else
                {
                    ShowMessage(dt.Rows.Count > 0 ? dt.Rows[0]["Msg"].ToString() : "Something went wrong. Please try again.");
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, spName);
                ShowMessage("Something went wrong. Please try again.");
            }
            return Id;
        }
        #endregion

        #region [Events]
        protected void btnSearchLR_Click(object sender, EventArgs e)
        {
            FillPartnerLR();
        }

        protected void gvPartnerLR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectLR")
            {
                LoadPartnerLR(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void btnFetchVoucher_Click(object sender, EventArgs e)
        {
            FetchVouchers();
        }

        protected void btnAddOther_Click(object sender, EventArgs e)
        {
            SyncVoucherSelection();
            decimal Amt = ToDec(txtOtherAmount.Text);
            if (Amt <= 0 || txtOtherDate.Text == "")
            {
                ShowMessage("Please enter the Date and Amount for the Other voucher.");
                BindVoucherGrid();
                return;
            }
            DataTable dtV = (DataTable)ViewState["dtVoucher"];
            DataTable dtUser = cm.GetUserDetails();
            DataRow n = dtV.NewRow();
            n["RowKey"] = "M_" + Guid.NewGuid().ToString("N");
            n["VoucherId"] = 0;
            n["VoucherNo"] = "New";
            n["VoucherType"] = "OTHER";
            n["SourceType"] = "M";
            n["SourceId"] = 0;
            n["VoucherDate"] = txtOtherDate.Text;
            n["VoucherDateDisp"] = ToDateText(txtOtherDate.Text, "dd-MM-yyyy");
            n["Amount"] = Amt;
            n["Through"] = txtOtherThrough.Text.Trim();
            n["Station"] = dtUser.Rows.Count > 0 ? dtUser.Rows[0]["BranchName"].ToString() : "";
            n["Remarks"] = "Other Expense";
            n["Selected"] = true;
            dtV.Rows.Add(n);
            ViewState["dtVoucher"] = dtV;
            txtOtherAmount.Text = "";
            txtOtherThrough.Text = "";
            BindVoucherGrid();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int Id = SaveData();
            if (Id > 0)
            {
                string Msg = Request.QueryString["Id"] == null
                    ? "Internal LR, Voucher(s) and Draft Bill have been created successfully."
                    : "The record has been updated successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');window.location='AddInternalLR.aspx?Id=" + Id + "';", true);
            }
            else
            {
                BindVoucherGrid();
            }
        }

        protected void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            int Id = SaveData();
            if (Id > 0)
            {
                Response.Redirect("InternalLRReportViewer.aspx?Id=" + Id + "&Print=All", false);
            }
            else
            {
                BindVoucherGrid();
            }
        }

        protected void btnFinalizeBill_Click(object sender, EventArgs e)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            try
            {
                // Save the current amount first, then finalise
                int Id = SaveData();
                if (Id <= 0)
                {
                    BindVoucherGrid();
                    return;
                }
                ParameterName.Add("@Action"); ParameterValue.Add("FinalizeBill");
                ParameterName.Add("@Id"); ParameterValue.Add(Id);
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Status"]) == 1)
                {
                    cm.SaveActivityLog("Internal LR", "Bill Finalized", "Bill No = " + txtBillNo.Text);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('" + dt.Rows[0]["Msg"].ToString() + "');window.location='AddInternalLR.aspx?Id=" + Id + "';", true);
                }
                else
                {
                    ShowMessage(dt.Rows.Count > 0 ? dt.Rows[0]["Msg"].ToString() : "Something went wrong. Please try again.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "window.location='AddInternalLR.aspx?Id=" + Id + "';", true);
                }
            }
            catch (Exception ex)
            {
                WriteError(ex, spName);
            }
        }

        protected void btnReopenBill_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["UserType"]) != "A" || Request.QueryString["Id"] == null)
            {
                return;
            }
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("ReopenBill");
                ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["Id"]));
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(Session["UserId"]));
                dt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                cm.SaveActivityLog("Internal LR", "Bill Reopened", "Bill No = " + txtBillNo.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('The Bill has been moved back to Draft.');window.location='AddInternalLR.aspx?Id=" + Request.QueryString["Id"].ToString() + "';", true);
            }
            catch (Exception ex)
            {
                WriteError(ex, spName);
            }
        }
        #endregion
    }
}
