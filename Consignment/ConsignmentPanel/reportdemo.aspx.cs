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
    public partial class reportdemo : System.Web.UI.Page
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        common cm = new common();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PrintAllReport_iText("111", "254745.pdf", "abc");
        }

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

        public void PrintAllReport_iText1(string BookId, string FileName, string Reporttxt)
        {

            string strPdfPath = FolderCheck("App_Content/LC_Report");
            FileStream fs = new FileStream(strPdfPath + "\\" + FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document((iTextSharp.text.PageSize.A4.Rotate()));
            document.SetMargins(30.0f, 30.0f, 30.0f, 30.0f);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            float[] columnWidths = { 152, 152, 152, 152, 152, 152, 152, 152, 152, 152 };

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
            cell = new PdfPCell(new Phrase(" Company Name", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 6;
            cell.Rowspan = 4;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" LORRY CHALLAN", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Hire Charges will be paid at", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase(" after receiving the goods in good condition at the destination", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Vechicle No", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 2020045", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Date", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 24-07-2020", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" FROM", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Bangalore", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 6;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Rate", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Received Advanced Rs ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 4;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Vechicle No", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 2020045", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Date", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 24-07-2020", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" FROM", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Bangalore", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Per Ton", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Chashis No ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 35701654646545 ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Engine No ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 30M62301869 ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Freight ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("OWNER NAME  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("UNIVERSAL ROADLINES  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hamali  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Received Full Paymaent against this challan ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);







            cell = new PdfPCell(new Phrase("OWNER ADDRESS  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("oFC nO 402, 4TH fLOOR   ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Detention CHarge  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Date of Payment ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Driver Name ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Driver Lic No ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase(" Driver Mobile ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" D/delivery ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Document No ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" abdul hasid ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" MHDX234@LE ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 9326809777 ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


















            cell = new PdfPCell(new Phrase(" Consignment No ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Station ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase(" Pkgs ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Weight in Kgs ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Length CHarges ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Cheque No ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 2020001 ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Pune ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 333 nos ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Fixed ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" The owner and driver of the Public carrier under take to deliver the above noted`The owner and driver of the Public carrier under take to deliver the above noted`The owner and driver of the Public carrier under take to deliver the above noted` ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 6;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Total ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Bal AMount ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" SIGNATURE OF OWNER/DRIVER ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 6;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" LES ADVANCE ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" BAR CODE ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Transporter / Broker ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" UNIVERSAL ROADLINES ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Balance", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Barcode Image", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Transporter/Broker Contact No", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" 9326809777", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" PAN CARD", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" AGNPT1313R", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" FOR VAYUDOOT ROAD CARRIERS PVT LTD", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Name   Signature", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 4;
            cell.Rowspan = 1;
            cell.Padding = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            table.AddCell(cell);
            document.Add(table);
            document.Close();
            document.Dispose();
            writer.Close();
            writer.Dispose();
            fs.Close();
            fs.Dispose();
        }
        private void PrintAllReport_iText(string BookId, string FileName, string Reporttxt)
        {
            string ReportNo = string.Empty;

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

            string strPdfPath = FolderCheck("App_Content/LC_Report");
            FileStream fs = new FileStream(strPdfPath + "\\" + FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document((iTextSharp.text.PageSize.A4.Rotate()));
            document.SetMargins(30.0f, 30.0f, 30.0f, 30.0f);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            float[] columnWidths = { 190, 190, 190, 190, 190, 190, 190, 190 };




            StrBase64 = GenerateBarCode("5555888");

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
            cell.Colspan = 6;
            cell.Padding = 0;
            cell.FixedHeight = 6f;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("E", ELR));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 4;
            table.AddCell(cell);

            byte[] imageBytes = Convert.FromBase64String(CompanyImageStr);
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageBytes);
            jpg.ScaleAbsolute(60f, 60.0f);
            cell = new PdfPCell(jpg);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
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

            cell = new PdfPCell(new Phrase("LORRY CHALLAN", ELR));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.Padding = 4;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", caution));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("VEHICLE HIRE NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("2020045", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("DATE", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("24-07-2025", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("FROM", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("BANGLORE", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hire charges will be paid at", SubjectToPune1));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase(" ", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 8;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

           







            cell = new PdfPCell(new Phrase("VEHICLE  NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("RJ14CZ9999", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("VEHICLE SIZE", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("CQH1Z", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("TO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("PUNE", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);
            

            cell = new PdfPCell(new Phrase("after receiving the goods in good condition at the destination", SubjectToPune1));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase(" ", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 8;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

          













            cell = new PdfPCell(new Phrase("CONSIGNMENT  NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("STATION", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("PKGS", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("WEIGHT (KGS)", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("DETAILS", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("AMOUNT", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);





            cell = new PdfPCell(new Phrase("202001", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.PaddingTop = 15;
            cell.PaddingBottom = 15;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("Pune", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.PaddingTop = 15;
            cell.PaddingBottom = 15;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("666 nos", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.PaddingTop = 15;
            cell.PaddingBottom = 15;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.Rowspan = 2;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fixed", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.PaddingTop = 15;
            cell.PaddingBottom = 15;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.Rowspan = 2;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Freight", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("The owner and driver of the Public Carrier under take to deliver the above noted cargo to the right person at the destination and are resposible for its safety during transit and are here by indemnify the carrier aganist all claims demanded and losses.", SubjectToPune1));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.PaddingTop = 10;
            cell.PaddingBottom = 10;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.Colspan = 6;
            cell.Rowspan = 2;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            

            cell = new PdfPCell(new Phrase("Hamali", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("CHASIS NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.PaddingTop = 10;
            cell.PaddingBottom = 10;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("357010FUQ", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Colspan = 2;
            cell.PaddingTop = 10;
            cell.PaddingBottom = 10;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("ENGINE NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.PaddingTop = 10;
            cell.PaddingBottom = 10;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("30M62301869", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Colspan = 2;
            cell.PaddingTop = 10;
            cell.PaddingBottom = 10;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Detention", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("DRIVER'S NAME", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("DRIVER'S LICENCE NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("DRIVER MOBILE NO", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("Other CHarges", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("ABDUL HASID", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MHDX234/LE", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("9326809777", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("OWNER'S NAME", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.PaddingTop = 8;
            cell.PaddingBottom = 8;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("UNIVERSAL ROADLINES", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 5;
            cell.Rowspan = 2;
            cell.PaddingTop = 8;
            cell.PaddingBottom = 8;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);




            cell = new PdfPCell(new Phrase("Other Duduction", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);













            cell = new PdfPCell(new Phrase("OWNER'S ADDRESS", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.PaddingTop = 8;
            cell.PaddingBottom = 8;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("OFFICE NO 402, 4TH FLOOR PENTAGONE-1, MAGARPATTA CITY HADAPSAR, PUNE, MAHARASTHRA", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 5;
            cell.Rowspan = 2;
            cell.PaddingTop = 8;
            cell.PaddingBottom = 8;
            cell.PaddingLeft = 5;
            cell.PaddingRight = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);




            cell = new PdfPCell(new Phrase("TDS", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("SIGNATURE OF OWNER / DRIVER", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 6;
            cell.Colspan = 6;
            cell.Padding = 3;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("Total", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("(-) : Advance", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("Balance", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("P", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("TRANSPORTER", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 3;
            cell.Colspan = 1;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("UNIVERSAL ROADLINES", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Rowspan = 3;
            cell.Colspan = 5;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("FOR Vayudoot Road Carriers Pvt Ltd", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", VRCGSTIN));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Signature", Date));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Colspan = 2;
            cell.Padding = 5;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 1;
            cell.BorderWidthLeft = 1;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);

            document.Add(table);




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

    }

}