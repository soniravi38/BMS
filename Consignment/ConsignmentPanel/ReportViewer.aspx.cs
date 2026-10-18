using Consignment.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;


namespace Consignment.ConsignmentPanel
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();
        #endregion

        #region[PageLoad]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["BookId"] != null)
            {

            }
            else
            {
                Response.Redirect("AddBookLr.aspx");
            }
        }
        #endregion

        #region[Function]
        private void PrintAllReport_iText(string BookId, string FileName, string Reporttxt)
        {
            string ReportNo = string.Empty;
            int LoopCount = 0;
            DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + BookId + "'");
            if (dt.Rows[0]["MType"].ToString() == "B")
            {
                int GroupId = Convert.ToInt32((con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + BookId + "'")).Rows[0]["GroupId"]);
                dt = con.GetDatatable("Select * from Vw_RptLRBook where groupid='" + GroupId + "'");
            }
            string spName = "";
            string StrBase64 = "";
            string CompanyImageStr = string.Empty;
            string CompanyName = string.Empty;
            string CompanyGSTIN = string.Empty;
            string CompanyAddress1 = string.Empty;
            string CompanyAddress2 = string.Empty;
            string CompanyPhone1 = string.Empty;
            string CompanyPhone2 = string.Empty;
            string CompanyFax = string.Empty;
            string CompanyEmail = string.Empty;
            string CompanyWebsite = string.Empty;
            string DocumentNo = string.Empty;
            string EwayDetails = string.Empty;
            string InvoiceDetails = string.Empty;

            DataTable dtCompany = con.GetDatatable("select * from CompanyMaster");
            CompanyAddress1 = dtCompany.Rows[0]["Address1"].ToString();
            CompanyAddress2 = dtCompany.Rows[0]["Address2"].ToString();
            CompanyPhone1 = dtCompany.Rows[0]["Phone1"].ToString();
            CompanyPhone2 = dtCompany.Rows[0]["Phone2"].ToString();
            CompanyFax = dtCompany.Rows[0]["Fax"].ToString();
            CompanyEmail = dtCompany.Rows[0]["Email1"].ToString();
            CompanyWebsite = dtCompany.Rows[0]["Website"].ToString();
            CompanyImageStr = dtCompany.Rows[0]["ImageStr"].ToString();
            CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
            CompanyGSTIN = "GSTIN : " + dtCompany.Rows[0]["GSTIN"].ToString() + "                  CIN:-" + dtCompany.Rows[0]["CIN"].ToString() + "                        PAN NO:" + dtCompany.Rows[0]["PAN"].ToString() + "                         FSSAI L No : " + dtCompany.Rows[0]["FSSAI"].ToString();

            string strPdfPath = FolderCheck("App_Content/Report");
            FileStream fs = new FileStream(strPdfPath + "\\" + FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document((iTextSharp.text.PageSize.A4.Rotate()));
            document.SetMargins(30.0f, 30.0f, 30.0f, 30.0f);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            float[] columnWidths = { 98, 117, 123, 230, 273, 165, 101, 409 };
            if (Reporttxt == "") { LoopCount = 4; } else { LoopCount = 1; }
            for (int i = 1; i <= LoopCount; i++)
            {
                for (int z = 0; z < dt.Rows.Count; z++)
                {
                    DocumentNo = "";
                    spName = "Proc_DocumentDetails";
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                    ParameterName.Add("@LRNo"); ParameterValue.Add(dt.Rows[z]["BookLrNo"].ToString());
                    DataTable DocumentDt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (DocumentDt.Rows.Count > 0)
                    {
                        for (int d = 0; d < DocumentDt.Rows.Count; d++)
                        {
                            DocumentNo += " [" + DocumentDt.Rows[d]["DocumentNo"].ToString() + "] ";
                        }
                    }

                    EwayDetails = "EwayBill ";
                    spName = "Proc_EwayBillDetails";
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAllforPrint");
                    ParameterName.Add("@LRNo"); ParameterValue.Add(dt.Rows[z]["BookLrNo"].ToString());
                    DataTable EwayDt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (EwayDt.Rows.Count > 0)
                    {
                        for (int e = 0; e < EwayDt.Rows.Count; e++)
                        {
                            EwayDetails += " [No.:" + EwayDt.Rows[e]["EwayBillNo"].ToString() + ",Date:" + EwayDt.Rows[e]["BillDate"].ToString() + ",Upto:" + EwayDt.Rows[e]["BillUptoDate"].ToString() + "] ";
                        }
                    }

                    InvoiceDetails = "Invoice ";
                    spName = "Proc_InvoiceDetails";
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAllforPrint");
                    ParameterName.Add("@LRNo"); ParameterValue.Add(dt.Rows[z]["BookLrNo"].ToString());
                    DataTable InvoiceDt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (InvoiceDt.Rows.Count > 0)
                    {
                        for (int inv = 0; inv < InvoiceDt.Rows.Count; inv++)
                        {
                            InvoiceDetails += " [No.:" + InvoiceDt.Rows[inv]["InvoiceNo"].ToString() + ",Date:" + InvoiceDt.Rows[inv]["InvoiceDate"].ToString() + ",Value:" + InvoiceDt.Rows[inv]["InvoiceValue"].ToString() + "] ";
                        }
                    }

                    StrBase64 = GenerateBarCode(dt.Rows[z]["BookLrNo"].ToString());
                    if (Reporttxt == "")
                    {
                        if (i == 1) { ReportNo = "CONSIGNOR COPY"; }
                        else if (i == 2) { ReportNo = "CONSIGNEE COPY"; document.NewPage(); }
                        else if (i == 3) { ReportNo = "DRIVER COPY"; document.NewPage(); }
                        else if (i == 4) { ReportNo = "ACCOUNT COPY"; }
                    }
                    else
                    {
                        ReportNo = Reporttxt;
                    }
                    PdfPTable table = null;
                    table = new PdfPTable(columnWidths);
                    table.WidthPercentage = 100;
                    iTextSharp.text.Font fontB = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font ELR = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 16, iTextSharp.text.Font.BOLD, BaseColor.RED));
                    iTextSharp.text.Font Vayudoot = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 16, iTextSharp.text.Font.BOLD, BaseColor.RED));
                    iTextSharp.text.Font Copy = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 16, iTextSharp.text.Font.BOLD, BaseColor.RED));
                    iTextSharp.text.Font VayudootAddress = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font AtOwnerRisk = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font notice = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, BaseColor.RED));
                    iTextSharp.text.Font INSURANCE = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font caution = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font TheConsignmentCovered = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.RED));
                    iTextSharp.text.Font SubjectToPune = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font SubjectToPune1 = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font AddressOfDelivery = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font DoorDelivery = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font TheConsignerHasStated = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font PolicyNo = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font VRCGSTIN = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font txtConName = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font ConName = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font ConAddress = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font ConGSTIN = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font txtConNo = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, BaseColor.RED));
                    iTextSharp.text.Font ConNo = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font txtVehicleNo = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, BaseColor.RED));
                    iTextSharp.text.Font VechicleNo = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font FromTo = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font VechicleSize = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font Sign = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font Date = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));

                    PdfPCell cell = new PdfPCell();
                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 7;
                    cell.Padding = 0;
                    cell.FixedHeight = 6f;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("E-LR", ELR));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    table.AddCell(cell);

                    byte[] imageBytes = Convert.FromBase64String(CompanyImageStr);
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageBytes);
                    jpg.ScaleAbsolute(60f, 60.0f);
                    cell = new PdfPCell(jpg);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 4;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(CompanyName, Vayudoot));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Padding = 0;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("(CARGO MOVERS & TRANSPORT CONTRACTORS) \n Corporate Office : " + CompanyAddress1 + " \n " + CompanyAddress2 + " \n " + CompanyPhone1 + ", " + CompanyPhone2 + "  Fax : " + CompanyFax + " \n Email : " + CompanyEmail + ", Web : " + CompanyWebsite, VayudootAddress));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Rowspan = 3;
                    cell.PaddingBottom = 4;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("CAUTION", caution));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 10;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.AddElement(new Phrase("Subject to pune Jurisdiction. ", SubjectToPune));
                    cell.AddElement(new Phrase("This consignment  will not be detained diverted,re-routed or re-booked without consignee Bank's  written permission.will be delivered at the destination.", SubjectToPune1));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(ReportNo, Copy));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 3;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("AT OWNERS RISK", AtOwnerRisk));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.Border = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("NOTICE", notice));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 3;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Border = 0;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("INSURANCE", INSURANCE));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Border = 0;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    cell.Padding = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("THE CONSIGNMENT COVERED BY THIS SET OF SPECIAL LORRY Form shall be stored at the destination under the control of the Transport Operator and shall be delivered to the order of the consignee Bank Whose name is mention in Lorry Reciept.It will Under no Circumstances be dilivered to anyone with out the written authority from the consignee bank of its order endorsed on the consignee copy or on a separate Letter of authority", TheConsignmentCovered));
                    cell.Colspan = 4;
                    cell.Rowspan = 4;
                    cell.Padding = 10;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    cell.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    table.AddCell(cell);

                    PdfContentByte cb = writer.DirectContent;
                    cb.Rectangle(330f, 432f, 6f, 6f);
                    cb.Rectangle(330f, 423f, 6f, 6f);
                    //if (i != 4) { cb = null; }
                    cell = new PdfPCell(new Phrase("This Consignor has stated that: \n   he has not insured the consignment or \n   he has insured the consignment company", TheConsignerHasStated));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 3;
                    cell.PaddingLeft = 10;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Address of the delivery", AddressOfDelivery));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 5;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Door Delivery", DoorDelivery));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Policy No :                                      Date : \n Amount :                                        Risk :", PolicyNo));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 3;
                    cell.PaddingLeft = 10;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(CompanyGSTIN, VRCGSTIN));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 8;
                    cell.Padding = 5;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Consignor Name :-", txtConName));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsignorName"].ToString(), ConName));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    if (dt.Rows[0]["MType"].ToString() == "B")
                    {
                        cell = new PdfPCell(new Phrase("Consignment No (" + (z + 1) + "/" + dt.Rows.Count.ToString() + ") :", txtConNo));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.Padding = 4;
                        cell.BorderWidthTop = 0;
                        cell.BorderWidthBottom = 1;
                        cell.BorderWidthLeft = 1;
                        cell.BorderWidthRight = 1;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("Consignment No :", txtConNo));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.Padding = 4;
                        cell.BorderWidthTop = 0;
                        cell.BorderWidthBottom = 1;
                        cell.BorderWidthLeft = 1;
                        cell.BorderWidthRight = 1;
                        table.AddCell(cell);
                    }

                    cell = new PdfPCell(new Phrase("Address : -", ConAddress));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsignorAdd"].ToString(), ConAddress));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookLrNo"].ToString(), ConNo));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Date :- " + dt.Rows[z]["BookDate1"].ToString(), Date));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsignorState"].ToString(), ConAddress));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.Colspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("GST NO :-", ConGSTIN));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsignorGST"].ToString(), ConGSTIN));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Vehicle No:-", txtVehicleNo));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Consignee Name", txtConName));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["Consigneename"].ToString(), ConName));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["VchNo"].ToString(), VechicleNo));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Address : -", ConAddress));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsigneeAdd"].ToString() + "," + dt.Rows[z]["ConsigneeState"].ToString(), ConAddress));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("From :- " + dt.Rows[z]["SourceFrom"].ToString(), FromTo));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("To :- " + dt.Rows[z]["ToDestination"].ToString(), FromTo));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("GST NO : - ", ConGSTIN));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    cell.Padding = 4;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsigneeGST"].ToString(), ConGSTIN));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Packages", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Description(said to contain)", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    DataTable dtLock = con.GetDatatable("select * from [tblVajraLock] where vehicleno='" + dt.Rows[z]["VchNo"].ToString() + "'");
                    if (dtLock.Rows.Count > 0)
                    {
                        cell = new PdfPCell(new Phrase("VAJRA LOCK - INSTALLED", notice));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.Padding = 0;
                        cell.BorderWidthTop = 1;
                        cell.BorderWidthBottom = 0;
                        cell.BorderWidthLeft = 1;
                        cell.BorderWidthRight = 0;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("VAJRA LOCK - ", notice));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.Padding = 0;
                        cell.BorderWidthTop = 1;
                        cell.BorderWidthBottom = 0;
                        cell.BorderWidthLeft = 1;
                        cell.BorderWidthRight = 0;
                        table.AddCell(cell);
                    }


                    cell = new PdfPCell(new Phrase("Rate", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Amount", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Shipment No:- " + dt.Rows[z]["BookShipNo"].ToString(), fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookQty"].ToString() + "," + dt.Rows[z]["UnitName"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 4;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ProdName"].ToString(), fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 4;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Vehicle Size", VechicleSize));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Frieght", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("LC No :- " + dt.Rows[z]["LCNo"].ToString(), fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookCustSize"].ToString(), VechicleSize));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Hamali", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Doc. No:-" + DocumentNo, fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.Rowspan = 3;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Collection Charges", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 2;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Total:-  " + dt.Rows[z]["BookQty"].ToString() + "," + dt.Rows[z]["UnitName"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Seal No:- " + dt.Rows[z]["SealNo"].ToString(), fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    if (dt.Rows[z]["BookBillTo"].ToString() == "TOPAY")
                    {
                        cell = new PdfPCell(new Phrase("To be billed To :- " + dt.Rows[z]["Consigneename"].ToString(), font));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.Padding = 4;
                        cell.BorderWidthTop = 1;
                        cell.BorderWidthBottom = 0;
                        cell.BorderWidthLeft = 1;
                        cell.BorderWidthRight = 0;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("To be billed To :- " + dt.Rows[z]["BookBillTo"].ToString(), font));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.Padding = 4;
                        cell.BorderWidthTop = 1;
                        cell.BorderWidthBottom = 0;
                        cell.BorderWidthLeft = 1;
                        cell.BorderWidthRight = 0;
                        table.AddCell(cell);
                    }

                    cell = new PdfPCell(new Phrase("Total", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);





                    cell = new PdfPCell(new Phrase(InvoiceDetails, font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 7;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);



                    cell = new PdfPCell(new Phrase("Barcode", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    cell.Padding = 4;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(EwayDetails, font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 7;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    imageBytes = Convert.FromBase64String(StrBase64);
                    jpg = iTextSharp.text.Image.GetInstance(imageBytes);
                    jpg.ScaleAbsolute(130f, 29.0f);
                    cell = new PdfPCell(jpg);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Rowspan = 2;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Remark", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookRemark1"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 6;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Goods accepted for carriage of the terms & conditions Printed overleaf.", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 5;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Signature of Transport Operator", Sign));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 3;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 0;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookLoading"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    document.Add(table);

                    document.NewPage();
                }


            }

            document.Close();
            document.Dispose();
            writer.Close();
            writer.Dispose();
            fs.Close();
            fs.Dispose();
        }
        private void PrintMBTransport_iText(string BookId, string FileName, string Reporttxt)
        {
            string ReportNo = string.Empty;
            int LoopCount = 0;
            DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + BookId + "'");
            if (dt.Rows[0]["MType"].ToString() == "B" || dt.Rows[0]["MType"].ToString() == "T")
            {
                int GroupId = Convert.ToInt32((con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + BookId + "'")).Rows[0]["GroupId"]);
                dt = con.GetDatatable("Select * from Vw_RptLRBook where groupid='" + GroupId + "'");
            }
            string spName = "";
            string StrBase64 = "";
            string CompanyImageStr = string.Empty;
            string CompanyName = string.Empty;
            string CompanyGSTIN = string.Empty;
            string CompanyAddress1 = string.Empty;
            string CompanyAddress2 = string.Empty;
            string CompanyPhone1 = string.Empty;
            string CompanyPhone2 = string.Empty;
            string CompanyFax = string.Empty;
            string CompanyEmail = string.Empty;
            string CompanyWebsite = string.Empty;
            string DocumentNo = string.Empty;
            string EwayDetails = string.Empty;
            string InvoiceDetails = string.Empty;

            DataTable dtCompany = con.GetDatatable("select * from CompanyMaster");
            CompanyAddress1 = dtCompany.Rows[0]["Address1"].ToString();
            CompanyAddress2 = dtCompany.Rows[0]["Address2"].ToString();
            CompanyPhone1 = dtCompany.Rows[0]["Phone1"].ToString();
            CompanyPhone2 = dtCompany.Rows[0]["Phone2"].ToString();
            CompanyFax = dtCompany.Rows[0]["Fax"].ToString();
            CompanyEmail = dtCompany.Rows[0]["Email1"].ToString();
            CompanyWebsite = dtCompany.Rows[0]["Website"].ToString();
            CompanyImageStr = "iVBORw0KGgoAAAANSUhEUgAADaQAAAFTCAYAAAB8wepTAAAABGdBTUEAALGOfPtRkwAAACBjSFJNAACHDwAAjA8AAP1SAACBQAAAfXkAAOmLAAA85QAAGcxzPIV3AAAKL2lDQ1BJQ0MgUHJvZmlsZQAASMedlndUVNcWh8+9d3qhzTDSGXqTLjCA9C4gHQRRGGYGGMoAwwxNbIioQEQREQFFkKCAAaOhSKyIYiEoqGAPSBBQYjCKqKhkRtZKfHl57+Xl98e939pn73P32XuftS4AJE8fLi8FlgIgmSfgB3o401eFR9Cx/QAGeIABpgAwWempvkHuwUAkLzcXerrICfyL3gwBSPy+ZejpT6eD/0/SrFS+AADIX8TmbE46S8T5Ik7KFKSK7TMipsYkihlGiZkvSlDEcmKOW+Sln30W2VHM7GQeW8TinFPZyWwx94h4e4aQI2LER8QFGVxOpohvi1gzSZjMFfFbcWwyh5kOAIoktgs4rHgRm4iYxA8OdBHxcgBwpLgvOOYLFnCyBOJDuaSkZvO5cfECui5Lj25qbc2ge3IykzgCgaE/k5XI5LPpLinJqUxeNgCLZ/4sGXFt6aIiW5paW1oamhmZflGo/7r4NyXu7SK9CvjcM4jW94ftr/xS6gBgzIpqs+sPW8x+ADq2AiB3/w+b5iEAJEV9a7/xxXlo4nmJFwhSbYyNMzMzjbgclpG4oL/rfzr8DX3xPSPxdr+Xh+7KiWUKkwR0cd1YKUkpQj49PZXJ4tAN/zzE/zjwr/NYGsiJ5fA5PFFEqGjKuLw4Ubt5bK6Am8Kjc3n/qYn/MOxPWpxrkSj1nwA1yghI3aAC5Oc+gKIQARJ5UNz13/vmgw8F4psXpjqxOPefBf37rnCJ+JHOjfsc5xIYTGcJ+RmLa+JrCdCAACQBFcgDFaABdIEhMANWwBY4AjewAviBYBAO1gIWiAfJgA8yQS7YDApAEdgF9oJKUAPqQSNoASdABzgNLoDL4Dq4Ce6AB2AEjIPnYAa8AfMQBGEhMkSB5CFVSAsygMwgBmQPuUE+UCAUDkVDcRAPEkK50BaoCCqFKqFaqBH6FjoFXYCuQgPQPWgUmoJ+hd7DCEyCqbAyrA0bwwzYCfaGg+E1cBycBufA+fBOuAKug4/B7fAF+Dp8Bx6Bn8OzCECICA1RQwwRBuKC+CERSCzCRzYghUg5Uoe0IF1IL3ILGUGmkXcoDIqCoqMMUbYoT1QIioVKQ21AFaMqUUdR7age1C3UKGoG9QlNRiuhDdA2aC/0KnQcOhNdgC5HN6Db0JfQd9Dj6DcYDIaG0cFYYTwx4ZgEzDpMMeYAphVzHjOAGcPMYrFYeawB1g7rh2ViBdgC7H7sMew57CB2HPsWR8Sp4sxw7rgIHA+XhyvHNeHO4gZxE7h5vBReC2+D98Oz8dn4Enw9vgt/Az+OnydIE3QIdoRgQgJhM6GC0EK4RHhIeEUkEtWJ1sQAIpe4iVhBPE68QhwlviPJkPRJLqRIkpC0k3SEdJ50j/SKTCZrkx3JEWQBeSe5kXyR/Jj8VoIiYSThJcGW2ChRJdEuMSjxQhIvqSXpJLlWMkeyXPKk5A3JaSm8lLaUixRTaoNUldQpqWGpWWmKtKm0n3SydLF0k/RV6UkZrIy2jJsMWyZf5rDMRZkxCkLRoLhQWJQtlHrKJco4FUPVoXpRE6hF1G+o/dQZWRnZZbKhslmyVbJnZEdoCE2b5kVLopXQTtCGaO+XKC9xWsJZsmNJy5LBJXNyinKOchy5QrlWuTty7+Xp8m7yifK75TvkHymgFPQVAhQyFQ4qXFKYVqQq2iqyFAsVTyjeV4KV9JUCldYpHVbqU5pVVlH2UE5V3q98UXlahabiqJKgUqZyVmVKlaJqr8pVLVM9p/qMLkt3oifRK+g99Bk1JTVPNaFarVq/2ry6jnqIep56q/ojDYIGQyNWo0yjW2NGU1XTVzNXs1nzvhZei6EVr7VPq1drTltHO0x7m3aH9qSOnI6XTo5Os85DXbKug26abp3ubT2MHkMvUe+A3k19WN9CP16/Sv+GAWxgacA1OGAwsBS91Hopb2nd0mFDkqGTYYZhs+GoEc3IxyjPqMPohbGmcYTxbuNe408mFiZJJvUmD0xlTFeY5pl2mf5qpm/GMqsyu21ONnc332jeaf5ymcEyzrKDy+5aUCx8LbZZdFt8tLSy5Fu2WE5ZaVpFW1VbDTOoDH9GMeOKNdra2Xqj9WnrdzaWNgKbEza/2BraJto22U4u11nOWV6/fMxO3Y5pV2s3Yk+3j7Y/ZD/ioObAdKhzeOKo4ch2bHCccNJzSnA65vTC2cSZ79zmPOdi47Le5bwr4urhWuja7ybjFuJW6fbYXd09zr3ZfcbDwmOdx3lPtKe3527PYS9lL5ZXo9fMCqsV61f0eJO8g7wrvZ/46Pvwfbp8Yd8Vvnt8H67UWslb2eEH/Lz89vg98tfxT/P/PgAT4B9QFfA00DQwN7A3iBIUFdQU9CbYObgk+EGIbogwpDtUMjQytDF0Lsw1rDRsZJXxqvWrrocrhHPDOyOwEaERDRGzq91W7109HmkRWRA5tEZnTdaaq2sV1iatPRMlGcWMOhmNjg6Lbor+wPRj1jFnY7xiqmNmWC6sfaznbEd2GXuKY8cp5UzE2sWWxk7G2cXtiZuKd4gvj5/munAruS8TPBNqEuYS/RKPJC4khSW1JuOSo5NP8WR4ibyeFJWUrJSBVIPUgtSRNJu0vWkzfG9+QzqUvia9U0AV/Uz1CXWFW4WjGfYZVRlvM0MzT2ZJZ/Gy+rL1s3dkT+S453y9DrWOta47Vy13c+7oeqf1tRugDTEbujdqbMzfOL7JY9PRzYTNiZt/yDPJK817vSVsS1e+cv6m/LGtHlubCyQK+AXD22y31WxHbedu799hvmP/jk+F7MJrRSZF5UUfilnF174y/ariq4WdsTv7SyxLDu7C7OLtGtrtsPtoqXRpTunYHt897WX0ssKy13uj9l4tX1Zes4+wT7hvpMKnonO/5v5d+z9UxlfeqXKuaq1Wqt5RPXeAfWDwoOPBlhrlmqKa94e4h+7WetS212nXlR/GHM44/LQ+tL73a8bXjQ0KDUUNH4/wjowcDTza02jV2Nik1FTSDDcLm6eORR67+Y3rN50thi21rbTWouPguPD4s2+jvx064X2i+yTjZMt3Wt9Vt1HaCtuh9uz2mY74jpHO8M6BUytOdXfZdrV9b/T9kdNqp6vOyJ4pOUs4m3924VzOudnzqeenL8RdGOuO6n5wcdXF2z0BPf2XvC9duex++WKvU++5K3ZXTl+1uXrqGuNax3XL6+19Fn1tP1j80NZv2d9+w+pG503rm10DywfODjoMXrjleuvyba/b1++svDMwFDJ0dzhyeOQu++7kvaR7L+9n3J9/sOkh+mHhI6lH5Y+VHtf9qPdj64jlyJlR19G+J0FPHoyxxp7/lP7Th/H8p+Sn5ROqE42TZpOnp9ynbj5b/Wz8eerz+emCn6V/rn6h++K7Xxx/6ZtZNTP+kv9y4dfiV/Kvjrxe9rp71n/28ZvkN/NzhW/l3x59x3jX+z7s/cR85gfsh4qPeh+7Pnl/eriQvLDwG/eE8/s3BCkeAAAACXBIWXMAAC4jAAAuIwF4pT92AAADaWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSfvu78nIGlkPSdXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQnPz4NCjx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iPjxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPjxkYzpjcmVhdG9yPjxyZGY6U2VxIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpsaT5SQVZJIFNPTkk8L3JkZjpsaT48L3JkZjpTZXE+DQoJCQk8L2RjOmNyZWF0b3I+PC9yZGY6RGVzY3JpcHRpb24+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyI+PGV4aWY6RGF0ZVRpbWVPcmlnaW5hbD4yMDI2LTA3LTI2VDIwOjA5OjAyLjUwMjwvZXhpZjpEYXRlVGltZU9yaWdpbmFsPjwvcmRmOkRlc2NyaXB0aW9uPjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSJ1dWlkOmZhZjViZGQ1LWJhM2QtMTFkYS1hZDMxLWQzM2Q3NTE4MmYxYiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIj48eG1wOkNyZWF0ZURhdGU+MjAyNi0wNy0yNlQyMDowOTowMi41MDI8L3htcDpDcmVhdGVEYXRlPjwvcmRmOkRlc2NyaXB0aW9uPjwvcmRmOlJERj48L3g6eG1wbWV0YT4NCjw/eHBh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";
            CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
            CompanyGSTIN = "GSTIN : " + dtCompany.Rows[0]["GSTIN"].ToString() + "                  CIN:-" + dtCompany.Rows[0]["CIN"].ToString() + "                        PAN NO:" + dtCompany.Rows[0]["PAN"].ToString() + "                         FSSAI L No : " + dtCompany.Rows[0]["FSSAI"].ToString();

            string strPdfPath = FolderCheck("App_Content/Report");
            FileStream fs = new FileStream(strPdfPath + "\\" + FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document((iTextSharp.text.PageSize.A4.Rotate()));
            document.SetMargins(30.0f, 30.0f, 30.0f, 30.0f);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            float[] columnWidths = { 189, 189, 189, 189, 189, 189, 189, 189 };
            if (Reporttxt == "") { LoopCount = 4; } else { LoopCount = 1; }
            for (int i = 1; i <= LoopCount; i++)
            {
                for (int z = 0; z < dt.Rows.Count; z++)
                {
                    DocumentNo = "";
                    spName = "Proc_DocumentDetails";
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                    ParameterName.Add("@LRNo"); ParameterValue.Add(dt.Rows[z]["BookLrNo"].ToString());
                    DataTable DocumentDt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (DocumentDt.Rows.Count > 0)
                    {
                        for (int d = 0; d < DocumentDt.Rows.Count; d++)
                        {
                            DocumentNo += " [" + DocumentDt.Rows[d]["DocumentNo"].ToString() + "] ";
                        }
                    }

                    EwayDetails = "EwayBill ";
                    spName = "Proc_EwayBillDetails";
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAllforPrint");
                    ParameterName.Add("@LRNo"); ParameterValue.Add(dt.Rows[z]["BookLrNo"].ToString());
                    DataTable EwayDt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (EwayDt.Rows.Count > 0)
                    {
                        for (int e = 0; e < EwayDt.Rows.Count; e++)
                        {
                            EwayDetails += " [No.:" + EwayDt.Rows[e]["EwayBillNo"].ToString() + ",Date:" + EwayDt.Rows[e]["BillDate"].ToString() + ",Upto:" + EwayDt.Rows[e]["BillUptoDate"].ToString() + "] ";
                        }
                    }

                    InvoiceDetails = "Invoice ";
                    spName = "Proc_InvoiceDetails";
                    ParameterName = new ArrayList();
                    ParameterValue = new ArrayList();
                    ParameterName.Add("@Action"); ParameterValue.Add("GetAllforPrint");
                    ParameterName.Add("@LRNo"); ParameterValue.Add(dt.Rows[z]["BookLrNo"].ToString());
                    DataTable InvoiceDt = con.GetDataTableFromSP(spName, ParameterName, ParameterValue);
                    if (InvoiceDt.Rows.Count > 0)
                    {
                        for (int inv = 0; inv < InvoiceDt.Rows.Count; inv++)
                        {
                            InvoiceDetails += " [No.:" + InvoiceDt.Rows[inv]["InvoiceNo"].ToString() + ",Date:" + InvoiceDt.Rows[inv]["InvoiceDate"].ToString() + ",Value:" + InvoiceDt.Rows[inv]["InvoiceValue"].ToString() + "] ";
                        }
                    }

                    StrBase64 = GenerateBarCode(dt.Rows[z]["BookLrNo"].ToString());
                    if (Reporttxt == "")
                    {
                        if (i == 1) { ReportNo = "CONSIGNOR COPY"; }
                        else if (i == 2) { ReportNo = "CONSIGNEE COPY"; document.NewPage(); }
                        else if (i == 3) { ReportNo = "DRIVER COPY"; document.NewPage(); }
                        else if (i == 4) { ReportNo = "ACCOUNT COPY"; }
                    }
                    else
                    {
                        ReportNo = Reporttxt;
                    }
                    PdfPTable table = null;
                    table = new PdfPTable(columnWidths);
                    table.WidthPercentage = 100;

                    iTextSharp.text.Font font0 = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                    iTextSharp.text.Font fontB = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                    iTextSharp.text.Font font1 = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, BaseColor.RED));
                    iTextSharp.text.Font ccc = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, BaseColor.RED));
                    iTextSharp.text.Font font2 = new iTextSharp.text.Font(FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));

                    PdfPCell cell = new PdfPCell();
                    cell = new PdfPCell(new Phrase("AT OWNER'S RISK", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("SUBJECT TO VISAKHAPATNAM JURISDICTION ONLY", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 4;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("GOODS CONSIGNMENT NOTE", font1));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    byte[] imageBytes = Convert.FromBase64String(CompanyImageStr);
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageBytes);
                    jpg.ScaleAbsolute(300f, 30.0f);
                    cell = new PdfPCell(jpg);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 6;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("INSURANCE", fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("H.O.: # 58-30-50/1, Behind Punjabi Dhaba, Near Airport, Sakethapuram, Visakhapatnam - 530009", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 6;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("• The Customer has stated that he has not insured the consignment \n or \n • he has insured the consignment \n company............................................................ \n\n Policy No..............................Date................... \n Amount..................................Date.....................", font2));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("CAUTION : ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("From :", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("To :", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Delivery At", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Phone :", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);





                    cell = new PdfPCell(new Phrase("This Consignment will not be detained diverted, re-routed or re-booked with out Consignee Banks written permission will be delivered at the destination.", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["SourceFrom"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ToDestination"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Door Delivery", fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);





                    cell = new PdfPCell(new Phrase("Consigner : " + dt.Rows[z]["ConsignorName"].ToString(), fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 3;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Consignee : " + dt.Rows[z]["Consigneename"].ToString(), fontB));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 3;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(ReportNo, ccc));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsignorAdd"].ToString() + ", " + dt.Rows[z]["ConsignorState"].ToString() + "\nGSTIN : " + dt.Rows[z]["ConsignorGST"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 3;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["ConsigneeAdd"].ToString() + ", " + dt.Rows[z]["ConsigneeState"].ToString() + "\nGSTIN : " + dt.Rows[z]["ConsigneeGST"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.Colspan = 3;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Date : " + dt.Rows[z]["BookDate1"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(InvoiceDetails, font0));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Unloading By", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Truck No.", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("GST No.:", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("37ARPPS3900F1ZL", font1));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);





                    cell = new PdfPCell(new Phrase("No (" + (z + 1) + "/" + dt.Rows.Count.ToString() + ") :", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookLrNo"].ToString(), ccc));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font0));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["VchNo"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("PAN No.:", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("ARPPS 3900F", font1));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Articles", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Goods said to Contain", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Actual Weight", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Charged Weight", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("To Pay/Paid ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Remarks", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);





                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookQty"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["UnitName"].ToString() + " • " + dt.Rows[z]["ProdName"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Freight", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(dt.Rows[z]["BookRemark1"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 4;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);























                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Document No. : " + DocumentNo, font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rate/Kg", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);



                    cell = new PdfPCell(new Phrase("GST@", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);
















                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Shipment No. " + dt.Rows[z]["BookShipNo"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);



                    cell = new PdfPCell(new Phrase("Less Advance", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);













                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(EwayDetails, font0));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);



                    cell = new PdfPCell(new Phrase("Balance", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);






                    cell = new PdfPCell(new Phrase("LIABILITY OF GST: \n Tick wherever Applicable", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 3;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Vehicle Owner's Name & PAN No", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("TOTAL", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Seal No. " + dt.Rows[z]["SealNo"].ToString() + "\nSize : " + dt.Rows[z]["BookCustSize"].ToString(), font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 1;
                    cell.Rowspan = 2;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("(1) Consignor (2) Consignee (3) Carrier (4) Exempted", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 3;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 0;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("1. The company is not responsible for leadage & breakage. \n 2. The Consignor is responsible for contrabnad or restricted goods. \n consignee is liable to pay muncipal Octroi & Penalty if any levied by Check - Post Authority", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Consignor's Signature", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("For MB Transport Service", font));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                    cell.Colspan = 2;
                    cell.Rowspan = 1;
                    cell.Padding = 4;
                    cell.BorderWidthTop = 1;
                    cell.BorderWidthBottom = 1;
                    cell.BorderWidthLeft = 1;
                    cell.BorderWidthRight = 1;
                    table.AddCell(cell);



                    document.Add(table);

                    document.NewPage();
                }


            }

            document.Close();
            document.Dispose();
            writer.Close();
            writer.Dispose();
            fs.Close();
            fs.Dispose();
        }
        private string FolderCheck(string FolderName)
        {
            string ServerPath = string.Empty;
            ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/" + FolderName + "/");
            if (!Directory.Exists(ServerPath))
                Directory.CreateDirectory(ServerPath);
            return ServerPath;
        }
        /*
        private string GenerateBarCode(String LrNo)
        {
            LrNo = LrNo.Trim();
            string ImagePath = "/App_Content/BarcodeImage/BrCode_" + LrNo + ".jpg";
            string barcodePath = System.Web.Hosting.HostingEnvironment.MapPath(ImagePath);
            // Generate a Simple BarCode image and save as PNG
            IronBarCode.GeneratedBarcode BarCode = IronBarCode.BarcodeWriter.CreateBarcode("https://ironsoftware.com/csharp/barcode", BarcodeWriterEncoding.Code128);
            BarCode.SaveAsPng(barcodePath);
            // This line opens the image in your default image viewerSystem.Diagnostics.Process.Start("BarCode.png");

            //Converting Image to Base64
            string base64String = string.Empty;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(barcodePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    base64String = Convert.ToBase64String(imageBytes);
                    //return base64String;
                }
            }
            File.Delete(barcodePath);
            return base64String;
        }*/

        private string GenerateBarCode(String LrNo)
        {
            LrNo = LrNo.Trim();
            int pos = LrNo.LastIndexOf("/") + 1;
            LrNo = LrNo.Substring(pos, LrNo.Length - pos);

            var folderPath = System.Web.Hosting.HostingEnvironment.MapPath("~") + "/App_Content/BarcodeImage";
            string ImagePath = "/App_Content/BarcodeImage/BrCode_" + LrNo + ".jpg";
            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.CODABAR;
            var result = barcodeWriter.Write(LrNo);
            string barcodePath = System.Web.Hosting.HostingEnvironment.MapPath(ImagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            //Converting Image to Base64
            string base64String = string.Empty;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(barcodePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    base64String = Convert.ToBase64String(imageBytes);
                    //return base64String;
                }
            }
            File.Delete(barcodePath);
            return base64String;
        }
        #endregion

        #region[Events]
        protected void btnConsignor_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["BookId"] != null)
                {
                    string strHTML = string.Empty;
                    string Reporttxt = "CONSIGNOR COPY";
                    string FileName = "Invoice_Bid" + Request.QueryString["BookId"] + "_" + DateTime.Now.ToString("HH-mm-ss.fff") + ".pdf";
                    string FinalURL = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "App_Content/Report/" + FileName;
                    DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + Request.QueryString["BookId"] + "'");
                    if (dt.Rows[0]["MType"].ToString() == "T")
                    {
                        PrintMBTransport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    else
                    {
                        PrintAllReport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    Response.Redirect(FinalURL, false);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        protected void btnConsignee_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["BookId"] != null)
                {
                    string strHTML = string.Empty;
                    string Reporttxt = "CONSIGNEE COPY";
                    string FileName = "Invoice_Bid" + Request.QueryString["BookId"] + "_" + DateTime.Now.ToString("HH-mm-ss.fff") + ".pdf";
                    string FinalURL = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "App_Content/Report/" + FileName;
                    DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + Request.QueryString["BookId"] + "'");
                    if (dt.Rows[0]["MType"].ToString() == "T")
                    {
                        PrintMBTransport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    else
                    {
                        PrintAllReport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    Response.Redirect(FinalURL, false);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["BookId"] != null)
                {
                    string strHTML = string.Empty;
                    string Reporttxt = "ACCOUNT COPY";
                    string FileName = "Invoice_Bid" + Request.QueryString["BookId"] + "_" + DateTime.Now.ToString("HH-mm-ss.fff") + ".pdf";
                    string FinalURL = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "App_Content/Report/" + FileName;
                    DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + Request.QueryString["BookId"] + "'");
                    if (dt.Rows[0]["MType"].ToString() == "T")
                    {
                        PrintMBTransport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    else
                    {
                        PrintAllReport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    Response.Redirect(FinalURL, false);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        protected void btnDriver_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["BookId"] != null)
                {
                    string strHTML = string.Empty;
                    string Reporttxt = "DRIVER COPY";
                    string FileName = "Invoice_Bid" + Request.QueryString["BookId"] + "_" + DateTime.Now.ToString("HH-mm-ss.fff") + ".pdf";
                    string FinalURL = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "App_Content/Report/" + FileName;
                    DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + Request.QueryString["BookId"] + "'");
                    if (dt.Rows[0]["MType"].ToString() == "T")
                    {
                        PrintMBTransport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    else
                    {
                        PrintAllReport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    Response.Redirect(FinalURL, false);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["BookId"] != null)
                {
                    string strHTML = string.Empty;
                    string Reporttxt = "";
                    string FileName = "Invoice_Bid" + Request.QueryString["BookId"] + "_" + DateTime.Now.ToString("HH-mm-ss.fff") + ".pdf";
                    string FinalURL = System.Configuration.ConfigurationManager.AppSettings["ConfigPath"].ToString() + "App_Content/Report/" + FileName;
                    DataTable dt = con.GetDatatable("Select * from Vw_RptLRBook where Bookid='" + Request.QueryString["BookId"] + "'");
                    if (dt.Rows[0]["MType"].ToString() == "T")
                    {
                        PrintMBTransport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    else
                    {
                        PrintAllReport_iText(Request.QueryString["BookId"], FileName, Reporttxt);
                    }
                    Response.Redirect(FinalURL, false);
                }
            }
            catch (Exception ex)
            {
                cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:  | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());

            }
        }
        #endregion

    }
}