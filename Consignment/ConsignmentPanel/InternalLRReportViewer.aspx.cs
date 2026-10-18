using Consignment.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consignment.ConsignmentPanel
{
    public partial class InternalLRReportViewer : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();

        DataRow drH;                // Internal LR header
        DataTable dtVoucher;        // Vouchers
        DataRow drC;                // Own company (CompanyMaster)

        iTextSharp.text.Font fCompany = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15, BaseColor.BLACK);
        iTextSharp.text.Font fCompanyBig = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22, BaseColor.BLACK);
        iTextSharp.text.Font fSmall = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);
        iTextSharp.text.Font fSmallB = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.BLACK);
        iTextSharp.text.Font fNormal = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);
        iTextSharp.text.Font fBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.BLACK);
        iTextSharp.text.Font fBold10 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);
        iTextSharp.text.Font fTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
        iTextSharp.text.Font fItalicB = FontFactory.GetFont(FontFactory.HELVETICA_BOLDOBLIQUE, 9, BaseColor.BLACK);
        iTextSharp.text.Font fItalic = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9, BaseColor.BLACK);
        iTextSharp.text.Font fUnderline = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK);
        iTextSharp.text.Font fAmount = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.BLACK);
        iTextSharp.text.Font fDraft = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED);
        #endregion

        #region[PageLoad]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Database_"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
            if (Request.QueryString["Id"] == null)
            {
                Response.Redirect("ListInternalLR.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                if (LoadData() == false)
                {
                    Response.Redirect("ListInternalLR.aspx", false);
                    return;
                }
                lblInfo.Text = "Crossing Note No. " + drH["CNNo"] + "  |  Original LR No. " + drH["PartnerLRNo"] + " (" + drH["PartnerCompany"] + ")"
                    + "  |  Voucher(s) : " + dtVoucher.Rows.Count + "  |  Bill No. " + drH["BillNo"] + " (" + drH["BillStatusName"] + ")";
                lnkEdit.HRef = "AddInternalLR.aspx?Id=" + Request.QueryString["Id"].ToString();

                // One click : "Generate & Print All"
                if (Convert.ToString(Request.QueryString["Print"]) == "All")
                {
                    GenerateAndRedirect("ALL");
                }
            }
        }
        #endregion

        #region[Function]
        private bool LoadData()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_InternalLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(Request.QueryString["Id"]));
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                drH = ds.Tables[0].Rows[0];
                dtVoucher = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();

                DataTable dtCompany = cm.GetCompanyData();
                if (dtCompany.Rows.Count == 0)
                {
                    return false;
                }
                drC = dtCompany.Rows[0];
                return true;
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                return false;
            }
        }

        private void GenerateAndRedirect(string DocType)
        {
            try
            {
                if (drH == null && LoadData() == false)
                {
                    return;
                }
                string FileName = "InternalLR_" + drH["Id"].ToString() + "_" + DocType + ".pdf";
                GeneratePdf(DocType, FileName);
                string FinalURL = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "App_Content/InternalLR_Report/" + FileName;
                cm.SaveActivityLog("Internal LR", "Printed", "CN No = " + drH["CNNo"].ToString() + " (" + DocType + ")");
                Response.Redirect(FinalURL, false);
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP: | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Unable to generate the PDF. Please check the error log.');", true);
            }
        }

        // DocType : ALL / CN / VCH / BILL
        private void GeneratePdf(string DocType, string FileName)
        {
            string strPdfPath = FolderCheck("App_Content/InternalLR_Report");
            using (FileStream fs = new FileStream(strPdfPath + "\\" + FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document(PageSize.A4);
                document.SetMargins(25f, 25f, 25f, 25f);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                bool NeedNewPage = false;
                if (DocType == "ALL" || DocType == "CN")
                {
                    AddCrossingNote(document);
                    NeedNewPage = true;
                }
                if ((DocType == "ALL" || DocType == "VCH") && dtVoucher.Rows.Count > 0)
                {
                    if (NeedNewPage) document.NewPage();
                    AddVouchers(document);
                    NeedNewPage = true;
                }
                if (DocType == "VCH" && dtVoucher.Rows.Count == 0)
                {
                    document.Add(new Paragraph("No voucher linked with this Internal LR.", fBold10));
                }
                if (DocType == "ALL" || DocType == "BILL")
                {
                    if (NeedNewPage) document.NewPage();
                    AddBill(document);
                }

                document.Close();
                writer.Close();
            }
        }

        #region[Crossing Note]
        private void AddCrossingNote(Document document)
        {
            // ---- Company header ----
            PdfPTable head = new PdfPTable(1);
            head.WidthPercentage = 100;
            head.AddCell(Cell(S(drC["CompanyName"]).ToUpper(), fCompany, Element.ALIGN_CENTER, 0));
            head.AddCell(Cell("(CARGO MOVERS & TRANSPORT CONTRACTORS)", fSmallB, Element.ALIGN_CENTER, 0));
            head.AddCell(Cell("Corporate Office : " + JoinText(", ", S(drC["Address1"]), S(drC["Address2"])), fSmallB, Element.ALIGN_CENTER, 0));
            head.AddCell(Cell(JoinText(",", S(drC["Phone1"]), S(drC["Phone2"])) + (S(drC["Fax"]) != "" ? " Fax: " + S(drC["Fax"]) : "") + (S(drC["Email1"]) != "" ? ", Email: " + S(drC["Email1"]) : ""), fSmall, Element.ALIGN_CENTER, 0));
            if (S(drC["CIN"]) != "")
            {
                head.AddCell(Cell("CIN No. : " + S(drC["CIN"]), fSmallB, Element.ALIGN_CENTER, 0));
            }
            head.SpacingAfter = 8f;
            document.Add(head);

            // ---- Consignor / Consignee + Crossing Note box ----
            PdfPTable top = new PdfPTable(new float[] { 62f, 38f });
            top.WidthPercentage = 100;

            PdfPCell left = new PdfPCell();
            left.Border = 0;
            left.AddElement(new Paragraph("CROSSING DOCUMENTS ONLY", fTitle));
            left.AddElement(Para("Consignor's Name & Address:  ", fBold, S(drH["ConsignorName"]).ToUpper(), fNormal, 6f));
            left.AddElement(Para("G.S.T. No. ", fBold, "", fNormal, 4f));
            left.AddElement(Para("Consignee's Name & Address:  ", fBold, S(drH["LRConsigneeName"]).ToUpper(), fNormal, 6f));
            left.AddElement(Para("G.S.T. No. ", fBold, "", fNormal, 4f));
            top.AddCell(left);

            PdfPTable box = new PdfPTable(new float[] { 35f, 65f });
            box.WidthPercentage = 100;
            PdfPCell bh = Cell("CROSSING NOTE", fBold10, Element.ALIGN_CENTER, 15);
            bh.Colspan = 2;
            bh.Padding = 4f;
            box.AddCell(bh);
            AddBoxRow(box, "No.", S(drH["CNNo"]));
            AddBoxRow(box, "Date", S(drH["CNDate1"]));
            AddBoxRow(box, "From", S(drH["FromStation"]).ToUpper());
            AddBoxRow(box, "To", S(drH["ToStation"]).ToUpper());
            PdfPCell right = new PdfPCell(box);
            right.Border = 0;
            right.Padding = 0;
            top.AddCell(right);
            top.SpacingAfter = 6f;
            document.Add(top);

            // ---- Goods table ----
            PdfPTable g = new PdfPTable(new float[] { 11f, 56f, 13f, 15f, 5f });
            g.WidthPercentage = 100;
            PdfPCell c;
            c = Cell("Packages", fBold10, Element.ALIGN_CENTER, 15); c.Rowspan = 2; c.VerticalAlignment = Element.ALIGN_MIDDLE; g.AddCell(c);
            c = Cell("Description said to Contain", fBold10, Element.ALIGN_CENTER, 15); c.Rowspan = 2; c.VerticalAlignment = Element.ALIGN_MIDDLE; g.AddCell(c);
            c = Cell("Rate", fBold10, Element.ALIGN_CENTER, 15); c.Rowspan = 2; c.VerticalAlignment = Element.ALIGN_MIDDLE; g.AddCell(c);
            c = Cell("Amount To Pay/Paid", fBold, Element.ALIGN_CENTER, 15); c.Colspan = 2; g.AddCell(c);
            g.AddCell(Cell("Rs.", fBold, Element.ALIGN_LEFT, 15));
            g.AddCell(Cell("P.", fBold, Element.ALIGN_CENTER, 15));

            g.AddCell(Cell(S(drH["Packages"]), fNormal, Element.ALIGN_CENTER, 15));

            PdfPCell desc = new PdfPCell();
            desc.Border = 15;
            desc.PaddingLeft = 4f;
            desc.AddElement(Para("Crossing against  L/R No  of Transport  ", fNormal, S(drH["PartnerLRNo"]), fBold10, 0f));
            desc.AddElement(Para("M/s  ", fBold, S(drH["LRConsigneeName"]).ToUpper(), fNormal, 0f));
            desc.AddElement(Para("Ex. : ", fBold, S(drH["FromStation"]).ToUpper() + "                         To :  " + S(drH["ToStation"]).ToUpper(), fNormal, 0f));
            Paragraph pInt = new Paragraph("This Document used for Internal office record only", fBold10);
            pInt.Alignment = Element.ALIGN_CENTER;
            desc.AddElement(pInt);
            desc.AddElement(Para("Vehicle No. :  ", fBold, S(drH["VehicleNo"]), fNormal, 0f));
            desc.AddElement(Para("Road Permit No. : ", fBold, S(drH["RoadPermitNo"]), fNormal, 0f));
            if (S(drH["CNRemarks"]) != "")
            {
                desc.AddElement(Para("Remarks : ", fBold, S(drH["CNRemarks"]), fNormal, 0f));
            }
            g.AddCell(desc);

            PdfPCell rate = new PdfPCell();
            rate.Border = 15;
            Paragraph pF = new Paragraph("Freight", fBold); pF.Alignment = Element.ALIGN_RIGHT;
            rate.AddElement(pF);
            rate.MinimumHeight = 95f;
            g.AddCell(rate);
            g.AddCell(Cell("", fNormal, Element.ALIGN_RIGHT, 15));
            g.AddCell(Cell("", fNormal, Element.ALIGN_RIGHT, 15));

            g.AddCell(Cell("", fNormal, Element.ALIGN_CENTER, 15));
            g.AddCell(Cell("", fNormal, Element.ALIGN_CENTER, 15));
            g.AddCell(Cell("Total", fBold, Element.ALIGN_RIGHT, 15));
            g.AddCell(Cell("", fNormal, Element.ALIGN_RIGHT, 15));
            g.AddCell(Cell("", fNormal, Element.ALIGN_RIGHT, 15));
            document.Add(g);

            PdfPTable foot = new PdfPTable(new float[] { 50f, 50f });
            foot.WidthPercentage = 100;
            foot.AddCell(Cell("Accounts   Copy H.O.", fBold, Element.ALIGN_LEFT, 0));
            foot.AddCell(Cell("Signature of Transport Operator", fBold, Element.ALIGN_RIGHT, 0));
            document.Add(foot);
        }

        private void AddBoxRow(PdfPTable box, string Label, string Value)
        {
            PdfPCell l = Cell(Label, fBold, Element.ALIGN_LEFT, 15);
            l.Padding = 4f;
            box.AddCell(l);
            PdfPCell v = Cell(Value, fBold10, Element.ALIGN_LEFT, 15);
            v.Padding = 4f;
            box.AddCell(v);
        }
        #endregion

        #region[Vouchers]
        private void AddVouchers(Document document)
        {
            int Count = 0;
            foreach (DataRow v in dtVoucher.Rows)
            {
                if (Count > 0 && Count % 3 == 0)
                {
                    document.NewPage();
                }
                AddOneVoucher(document, v);
                Count++;
            }
        }

        private void AddOneVoucher(Document document, DataRow v)
        {
            decimal Amount = ToDec(v["Amount"]);
            string VType = S(v["VoucherType"]);
            string VTypeText = VType == "DIESEL" ? "Diesel" : (VType == "CASH" ? "Cash Advance" : "Other");
            string LRDate = FormatDate(S(drH["PartnerLRDate"]), "dd-MM-yy");
            string VDate = FormatDate(S(v["VoucherDate"]), "dd-MM-yy");

            PdfPTable t = new PdfPTable(new float[] { 17f, 22f, 14f, 22f, 14f, 11f });
            t.WidthPercentage = 100;
            PdfPCell c;

            // Row 1
            c = Cell(S(v["VoucherNo"]), fBold, Element.ALIGN_LEFT, 13); t.AddCell(c);       // Border 13 = Top + Left + Right (1 + 4 + 8)
            c = Cell(S(drC["CompanyName"]), fCompany, Element.ALIGN_CENTER, 1 | 8); c.Colspan = 3; t.AddCell(c);
            t.AddCell(Cell("Station", fNormal, Element.ALIGN_LEFT, 15));
            t.AddCell(Cell(S(v["Station"]), fItalicB, Element.ALIGN_CENTER, 15));

            // Row 2
            c = Cell(JoinText(", ", S(drC["Address1"]), S(drC["Address2"])), fNormal, Element.ALIGN_CENTER, 4);
            c.Colspan = 4; t.AddCell(c);
            t.AddCell(Cell("Date", fNormal, Element.ALIGN_LEFT, 15));
            t.AddCell(Cell(VDate, fItalicB, Element.ALIGN_CENTER, 15));

            // Row 3
            c = Cell("Debit Voucher- " + VTypeText, fBold, Element.ALIGN_CENTER, 4 | 8); c.Colspan = 4; t.AddCell(c);
            c = Cell("", fNormal, Element.ALIGN_CENTER, 15); c.Colspan = 2; t.AddCell(c);

            // Row 4
            t.AddCell(Cell("To", fNormal, Element.ALIGN_LEFT, 15));
            c = Cell("", fNormal, Element.ALIGN_LEFT, 15); c.Colspan = 2; t.AddCell(c);
            t.AddCell(Cell("A/c", fNormal, Element.ALIGN_RIGHT, 15));
            c = Cell("Advance Freight", fItalic, Element.ALIGN_CENTER, 15); c.Colspan = 2; t.AddCell(c);

            // Row 5
            c = Cell("Description", fNormal, Element.ALIGN_CENTER, 15); c.Colspan = 4; t.AddCell(c);
            t.AddCell(Cell("Rs.", fNormal, Element.ALIGN_CENTER, 15));
            t.AddCell(Cell("Ps.", fNormal, Element.ALIGN_CENTER, 15));

            // Row 6
            c = Cell("Being amount paid as Advance Freight/ Balance Freight against LC no. " + S(drH["LCNo"]), fNormal, Element.ALIGN_LEFT, 15); c.Colspan = 4; t.AddCell(c);
            c = Cell(Math.Floor(Amount).ToString("0"), fItalicB, Element.ALIGN_CENTER, 15); c.Rowspan = 4; t.AddCell(c);
            c = Cell(((int)Math.Round((Amount - Math.Floor(Amount)) * 100)).ToString("00"), fItalicB, Element.ALIGN_CENTER, 15); c.Rowspan = 4; t.AddCell(c);

            // Row 7
            t.AddCell(Cell("Lorry Receipt no.", fNormal, Element.ALIGN_LEFT, 15));
            t.AddCell(Cell(S(drH["PartnerLRNo"]), fItalicB, Element.ALIGN_CENTER, 15));
            t.AddCell(Cell("Lorry No.", fNormal, Element.ALIGN_LEFT, 15));
            t.AddCell(Cell(S(drH["VehicleNo"]), fItalicB, Element.ALIGN_CENTER, 15));

            // Row 8
            t.AddCell(Cell("Dated", fNormal, Element.ALIGN_LEFT, 15));
            t.AddCell(Cell(LRDate, fItalicB, Element.ALIGN_CENTER, 15));
            t.AddCell(Cell("From  " + S(drH["FromStation"]).ToUpper(), fItalicB, Element.ALIGN_LEFT, 15));
            t.AddCell(Cell("To  " + S(drH["ToStation"]).ToUpper(), fItalicB, Element.ALIGN_LEFT, 15));

            // Row 9
            t.AddCell(Cell("Through", fNormal, Element.ALIGN_LEFT, 15));
            c = Cell(S(v["Through"]), fItalicB, Element.ALIGN_CENTER, 15); c.Colspan = 3; t.AddCell(c);

            // Row 10
            c = Cell("Amount in Words:", fNormal, Element.ALIGN_CENTER, 15); c.PaddingTop = 8f; c.PaddingBottom = 8f; t.AddCell(c);
            c = Cell(AmountInWords(Amount), fItalicB, Element.ALIGN_CENTER, 15); c.Colspan = 3; c.VerticalAlignment = Element.ALIGN_MIDDLE; t.AddCell(c);
            c = Cell(Amount.ToString("0.##", CultureInfo.InvariantCulture), fItalicB, Element.ALIGN_CENTER, 15); c.Colspan = 2; c.VerticalAlignment = Element.ALIGN_MIDDLE; t.AddCell(c);

            // Row 11
            c = Cell("Passed By", fNormal, Element.ALIGN_CENTER, 15); c.FixedHeight = 38f; t.AddCell(c);
            c = Cell("Certified By", fNormal, Element.ALIGN_CENTER, 15); c.Colspan = 2; t.AddCell(c);
            t.AddCell(Cell("Signature", fNormal, Element.ALIGN_CENTER, 15));
            c = Cell("Receiver", fNormal, Element.ALIGN_CENTER, 15); c.Colspan = 2; t.AddCell(c);

            t.SpacingAfter = 30f;
            document.Add(t);
        }
        #endregion

        #region[Bill]
        private void AddBill(Document document)
        {
            CultureInfo In = new CultureInfo("en-IN");
            decimal BillAmount = ToDec(drH["BillAmount"]);
            bool isDraft = S(drH["BillStatus"]) != "F";
            PdfPCell c;

            // Line 1 : Jurisdiction + Tel / Fax
            PdfPTable l1 = new PdfPTable(new float[] { 50f, 50f });
            l1.WidthPercentage = 100;
            c = Cell("Subject To Pune Jurisdiction" + (isDraft ? "        DRAFT BILL" : ""), isDraft ? fDraft : fNormal, Element.ALIGN_LEFT, 1 | 4);
            if (isDraft)
            {
                c = new PdfPCell();
                c.Border = 1 | 4;
                Paragraph p = new Paragraph();
                p.Add(new Chunk("Subject To Pune Jurisdiction", fNormal));
                p.Add(new Chunk("          DRAFT BILL", fDraft));
                c.AddElement(p);
            }
            l1.AddCell(c);
            l1.AddCell(Cell("Tel.: " + JoinText(", ", S(drC["Phone1"]), S(drC["Phone2"])) + (S(drC["Fax"]) != "" ? "\nFax: " + S(drC["Fax"]) : ""), fNormal, Element.ALIGN_RIGHT, 1 | 8));
            document.Add(l1);

            // Line 2 : Logo + Company
            PdfPTable l2 = new PdfPTable(new float[] { 15f, 85f });
            l2.WidthPercentage = 100;
            PdfPCell logo = new PdfPCell();
            logo.Border = 4 | 2;
            logo.Padding = 4f;
            try
            {
                if (S(drC["ImageStr"]) != "")
                {
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(S(drC["ImageStr"])));
                    img.ScaleToFit(65f, 55f);
                    logo = new PdfPCell(img, false);
                    logo.Border = 4 | 2;
                    logo.Padding = 4f;
                    logo.HorizontalAlignment = Element.ALIGN_CENTER;
                    logo.VerticalAlignment = Element.ALIGN_MIDDLE;
                }
            }
            catch { }
            l2.AddCell(logo);
            PdfPCell comp = new PdfPCell();
            comp.Border = 8 | 2;
            Paragraph pName = new Paragraph(S(drC["CompanyName"]).ToUpper(), fCompanyBig); pName.Alignment = Element.ALIGN_CENTER;
            comp.AddElement(pName);
            Paragraph pAdd = new Paragraph();
            pAdd.Add(new Chunk("Corporate Office: ", fBold));
            pAdd.Add(new Chunk(JoinText(", ", S(drC["Address1"]), S(drC["Address2"])).ToUpper(), fNormal));
            pAdd.Alignment = Element.ALIGN_CENTER;
            comp.AddElement(pAdd);
            if (S(drC["Email1"]) != "" || S(drC["Website"]) != "")
            {
                Paragraph pMail = new Paragraph();
                if (S(drC["Email1"]) != "") { pMail.Add(new Chunk("E-Mail:  ", fBold)); pMail.Add(new Chunk(S(drC["Email1"]) + "        ", fNormal)); }
                if (S(drC["Website"]) != "") { pMail.Add(new Chunk("Web-site: ", fBold)); pMail.Add(new Chunk(S(drC["Website"]), fNormal)); }
                pMail.Alignment = Element.ALIGN_CENTER;
                comp.AddElement(pMail);
            }
            comp.PaddingBottom = 6f;
            l2.AddCell(comp);
            document.Add(l2);

            // Line 3 : Bill no / title / date
            PdfPTable l3 = new PdfPTable(new float[] { 35f, 30f, 35f });
            l3.WidthPercentage = 100;
            l3.AddCell(CellChunks(4, Element.ALIGN_LEFT, new Chunk("Bill No.  ", fBold10), new Chunk(S(drH["BillNo"]), fTitle)));
            l3.AddCell(Cell("BILL OF SUPPLY", fUnderline, Element.ALIGN_CENTER, 0));
            l3.AddCell(CellChunks(8, Element.ALIGN_LEFT, new Chunk("Bill Date   ", fBold10), new Chunk(S(drH["BillDate1"]), fTitle)));
            document.Add(l3);

            // Line 4 : Bill to + own GST/PAN
            PdfPTable l4 = new PdfPTable(new float[] { 62f, 38f });
            l4.WidthPercentage = 100;
            PdfPCell to = new PdfPCell();
            to.Border = 4 | 2;
            to.PaddingBottom = 6f;
            to.AddElement(new Paragraph("M/s. " + S(drH["BillToName"]).ToUpper(), fBold10));
            Paragraph pTo = new Paragraph(S(drH["BillToAddress"]).ToUpper(), fBold);
            pTo.IndentationLeft = 20f;
            to.AddElement(pTo);
            if (S(drH["BillToGSTIN"]) != "")
            {
                Paragraph pG = new Paragraph("GSTIN -  " + S(drH["BillToGSTIN"]).ToUpper(), fBold);
                pG.IndentationLeft = 20f;
                to.AddElement(pG);
            }
            l4.AddCell(to);
            PdfPCell own = new PdfPCell();
            own.Border = 8 | 2;
            if (S(drC["CIN"]) != "") own.AddElement(new Paragraph("CIN - " + S(drC["CIN"]), fBold10));
            if (S(drC["PAN"]) != "") own.AddElement(new Paragraph("PAN No. : " + S(drC["PAN"]), fBold10));
            if (S(drC["GSTIN"]) != "") own.AddElement(new Paragraph("G.S.T. No.: " + S(drC["GSTIN"]), fBold10));
            l4.AddCell(own);
            document.Add(l4);

            // Line 5 : interest
            PdfPTable l5 = new PdfPTable(1);
            l5.WidthPercentage = 100;
            l5.AddCell(Cell("Interest @24% per annum will be charged on all outstanding bills.", fNormal, Element.ALIGN_CENTER, 4 | 8));
            document.Add(l5);

            // Line 6 : Particulars table
            PdfPTable g = new PdfPTable(new float[] { 10f, 12f, 47f, 10f, 10f, 13f });
            g.WidthPercentage = 100;
            string[] Heads = { "CN No.", "Date", "Particulars", "Weight\nKg/Tons", "Rate Per\nKg/Tons", "Amount" };
            foreach (string h in Heads)
            {
                g.AddCell(Cell(h, fBold10, h == "Particulars" ? Element.ALIGN_CENTER : Element.ALIGN_LEFT, 15));
            }

            g.AddCell(TopCell(S(drH["CNNo"]), fNormal, Element.ALIGN_CENTER));
            g.AddCell(TopCell(S(drH["CNDate1"]), fNormal, Element.ALIGN_CENTER));

            PdfPCell part = new PdfPCell();
            part.Border = 15;
            part.MinimumHeight = 330f;
            part.AddElement(new Paragraph("Material Lifted From  " + S(drH["FromStation"]).ToUpper(), fNormal));
            part.AddElement(new Paragraph("Delivered to:" + S(drH["LRConsigneeName"]).ToUpper(), fNormal));
            part.AddElement(new Paragraph(S(drH["ToStation"]).ToUpper(), fNormal));
            part.AddElement(new Paragraph("Transportation Charge", fNormal));
            Paragraph gap = new Paragraph(" ", fNormal); gap.SpacingBefore = 90f;
            part.AddElement(gap);
            Paragraph sac = new Paragraph("SAC No. of Transporter : 996511", fBold10); sac.Alignment = Element.ALIGN_CENTER; sac.SpacingAfter = 12f;
            part.AddElement(sac);
            part.AddElement(BillRefLine("Invoice No.  ", S(drH["BillInvoiceNo"])));
            part.AddElement(BillRefLine("Doc No.  ", S(drH["BillDocNo"])));
            part.AddElement(BillRefLine("P.O.No.  ", S(drH["BillPONo"])));
            part.AddElement(BillRefLine("Vehicle No.   ", S(drH["VehicleNo"])));
            part.AddElement(BillRefLine("eWay Bill No.  ", S(drH["BillEwayBillNo"])));
            g.AddCell(part);

            g.AddCell(TopCell(S(drH["BillWeight"]), fNormal, Element.ALIGN_CENTER));
            g.AddCell(TopCell(S(drH["BillRate"]), fNormal, Element.ALIGN_CENTER));
            g.AddCell(TopCell(BillAmount > 0 ? BillAmount.ToString("N2", In) : "", fAmount, Element.ALIGN_RIGHT));

            c = Cell("", fNormal, Element.ALIGN_LEFT, 4 | 2); c.Colspan = 4; g.AddCell(c);
            g.AddCell(Cell("TOTAL", fTitle, Element.ALIGN_LEFT, 15));
            g.AddCell(Cell(BillAmount > 0 ? BillAmount.ToString("N2", In) : "", fAmount, Element.ALIGN_RIGHT, 15));
            document.Add(g);

            // Line 7 : footer
            PdfPTable f = new PdfPTable(new float[] { 33f, 34f, 33f });
            f.WidthPercentage = 100;
            c = Cell("GTA to GTA Exemption Notification No.12/2017-Central Tax ( Rate )- SR NO-18", fBold, Element.ALIGN_CENTER, 4 | 8 | 2);
            c.Colspan = 3; c.PaddingTop = 14f; c.PaddingBottom = 14f; f.AddCell(c);
            c = CellChunks(15, Element.ALIGN_LEFT, new Chunk("Remarks : ", fBold10), new Chunk(S(drH["BillRemarks"]), fNormal));
            c.Colspan = 3; f.AddCell(c);
            f.AddCell(Cell("Checked By:", fNormal, Element.ALIGN_LEFT, 4));
            f.AddCell(Cell("E. & O.E.", fNormal, Element.ALIGN_CENTER, 0));
            f.AddCell(Cell("For " + S(drC["CompanyName"]).ToUpper(), fBold10, Element.ALIGN_RIGHT, 8));
            c = Cell("\nEnclosure", fNormal, Element.ALIGN_LEFT, 4 | 2); c.FixedHeight = 70f; f.AddCell(c);
            c = Cell("\n\n\nPlease Pay A/c. Payee Cheque Only", fNormal, Element.ALIGN_CENTER, 2); f.AddCell(c);
            c = Cell("\n\n\n\nSignature", fNormal, Element.ALIGN_CENTER, 8 | 2); f.AddCell(c);
            document.Add(f);
        }

        private Paragraph BillRefLine(string Label, string Value)
        {
            Paragraph p = new Paragraph(Label + Value, fNormal);
            p.IndentationLeft = 15f;
            p.SpacingAfter = 5f;
            return p;
        }

        private PdfPCell TopCell(string Text, iTextSharp.text.Font font, int Align)
        {
            PdfPCell c = new PdfPCell(new Phrase(Text, font));
            c.HorizontalAlignment = Align;
            c.VerticalAlignment = Element.ALIGN_MIDDLE;
            c.Border = 15;
            c.PaddingTop = 60f;
            return c;
        }
        #endregion

        #region[Helpers]
        // Border : 0 = none, 15 = all  (1 Top, 2 Bottom, 4 Left, 8 Right)
        private PdfPCell Cell(string Text, iTextSharp.text.Font font, int Align, int Border)
        {
            PdfPCell c = new PdfPCell(new Phrase(Text, font));
            c.HorizontalAlignment = Align;
            c.VerticalAlignment = Element.ALIGN_MIDDLE;
            c.Border = Border;
            c.Padding = 3f;
            return c;
        }

        private PdfPCell CellChunks(int Border, int Align, params Chunk[] Chunks)
        {
            Phrase ph = new Phrase();
            foreach (Chunk ch in Chunks) ph.Add(ch);
            PdfPCell c = new PdfPCell(ph);
            c.Border = Border;
            c.HorizontalAlignment = Align;
            c.VerticalAlignment = Element.ALIGN_MIDDLE;
            c.Padding = 5f;
            return c;
        }

        private Paragraph Para(string Label, iTextSharp.text.Font LabelFont, string Value, iTextSharp.text.Font ValueFont, float SpacingBefore)
        {
            Paragraph p = new Paragraph();
            p.Add(new Chunk(Label, LabelFont));
            p.Add(new Chunk(Value, ValueFont));
            p.SpacingBefore = SpacingBefore;
            return p;
        }

        private string S(object Value)
        {
            return Value == null || Value == DBNull.Value ? "" : Convert.ToString(Value).Trim();
        }

        private decimal ToDec(object Value)
        {
            decimal d = 0;
            decimal.TryParse(S(Value), NumberStyles.Any, CultureInfo.InvariantCulture, out d);
            return d;
        }

        private string JoinText(string Sep, params string[] Values)
        {
            return string.Join(Sep, Values.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        // yyyy-MM-dd -> given format
        private string FormatDate(string Value, string Format)
        {
            DateTime d;
            if (DateTime.TryParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                return d.ToString(Format);
            }
            return Value;
        }

        // Indian number system : Rupees Twenty Seven Thousand Five Hundred Only
        private string AmountInWords(decimal Amount)
        {
            long Rupees = (long)Math.Floor(Amount);
            int Paise = (int)Math.Round((Amount - Rupees) * 100);
            string Words = "Rupees " + (Rupees == 0 ? "Zero" : NumberToWords(Rupees));
            if (Paise > 0)
            {
                Words += " and " + NumberToWords(Paise) + " Paise";
            }
            return Words + " Only";
        }

        private string NumberToWords(long Number)
        {
            string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            Func<long, string> Two = null;
            Two = n => n < 20 ? Ones[n] : (Tens[n / 10] + (n % 10 > 0 ? " " + Ones[n % 10] : ""));
            List<string> Parts = new List<string>();
            long Crore = Number / 10000000; Number %= 10000000;
            long Lakh = Number / 100000; Number %= 100000;
            long Thousand = Number / 1000; Number %= 1000;
            long Hundred = Number / 100; Number %= 100;
            if (Crore > 0) Parts.Add(NumberToWords(Crore) + " Crore");
            if (Lakh > 0) Parts.Add(Two(Lakh) + " Lakh");
            if (Thousand > 0) Parts.Add(Two(Thousand) + " Thousand");
            if (Hundred > 0) Parts.Add(Ones[Hundred] + " Hundred");
            if (Number > 0) Parts.Add(Two(Number));
            return string.Join(" ", Parts);
        }

        private string FolderCheck(string FolderName)
        {
            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/" + FolderName + "/");
            if (!Directory.Exists(ServerPath))
                Directory.CreateDirectory(ServerPath);
            return ServerPath;
        }
        #endregion
        #endregion

        #region[Events]
        protected void btnPrintAll_Click(object sender, EventArgs e)
        {
            GenerateAndRedirect("ALL");
        }
        protected void btnPrintCN_Click(object sender, EventArgs e)
        {
            GenerateAndRedirect("CN");
        }
        protected void btnPrintVoucher_Click(object sender, EventArgs e)
        {
            GenerateAndRedirect("VCH");
        }
        protected void btnPrintBill_Click(object sender, EventArgs e)
        {
            GenerateAndRedirect("BILL");
        }
        #endregion
    }
}
