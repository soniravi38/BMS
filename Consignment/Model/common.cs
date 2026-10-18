
using Consignment.ConsignmentPanel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Net.Http.Server;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using System.Threading.Tasks;
namespace Consignment.Model
{
    public class common
    {
        #region [properties]
        ArrayList ParameterName, ParameterValue;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        connect con = new connect();
        #endregion
        #region[Loging]
        public bool WriteLog(string strMessage)
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Content/Logs/ErrorLog.txt");
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(strMessage);
                    writer.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region[PDFProcess]
        public string FolderCheck(string FolderName)
        {
            string ServerPath = string.Empty;
            ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Content/" + FolderName + "/");
            if (!Directory.Exists(ServerPath))
                Directory.CreateDirectory(ServerPath);
            return ServerPath;
        }
        #endregion

        #region[Image]
        public void ResizeImage(string tFname, string tempPath, string outputFilePath, int widthToConvert, int heightToConvert)
        {
            string fPath = tempPath;
            System.Drawing.Image img;
            System.Drawing.Image imgThumb;
            System.Drawing.Graphics grph;

            img = System.Drawing.Image.FromFile(fPath);
            int wdth = 0; int hgth = 0;
            if (img.Width > 210)
            {
                wdth = 210;
                hgth = img.Height;
            }
            else
            {
                wdth = img.Width;
                hgth = img.Height;
            }
            imgThumb = new Bitmap(wdth, hgth);
            grph = Graphics.FromImage(imgThumb);
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, wdth, hgth);
            grph.DrawImage(img, rec);
            imgThumb.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            imgThumb.Dispose();
            img.Dispose();
            grph.Dispose();

        }
        public void ResizePhoto(string tFname, string tempPath, string outputFilePath, int widthToConvert, int heightToConvert)
        {
            string fPath = tempPath;
            System.Drawing.Image img;
            System.Drawing.Image imgThumb;
            System.Drawing.Graphics grph;

            img = System.Drawing.Image.FromFile(fPath);
            int wdth = 0; int hgth = 0;
            if (img.Width > 100)
            {
                wdth = widthToConvert;
                hgth = heightToConvert;
            }
            else
            {
                wdth = img.Width;
                hgth = img.Height;
            }
            imgThumb = new Bitmap(wdth, hgth);
            grph = Graphics.FromImage(imgThumb);
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, wdth, hgth);
            grph.DrawImage(img, rec);
            imgThumb.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            imgThumb.Dispose();
            img.Dispose();
            grph.Dispose();

        }
        public void FixedSize(string tFname, string tempPath, string outputFilePath, int Width, int Height)
        {
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(tempPath);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            float nPercentW = 0;
            nPercentW = ((float)Width / (float)sourceWidth);
            int destHeight = (int)(sourceHeight * nPercentW);
            Bitmap bmPhoto = new Bitmap(Width, destHeight);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(imgPhoto, 0, 0, (int)Width, (int)destHeight);
            grPhoto.Dispose();
            bmPhoto.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);
        }
        #endregion

        #region[SMS]
        public string SendOTP(string Msg, string Mobile)
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string strGatewayResponse = "Oops...OTP Not Send..";
            string SMSApi = "";
            string spName = "Proc_CompanyMaster";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        SMSApi = dt.Rows[0]["SMSApi"].ToString();
                    }

                    if (Mobile.Length == 10)
                    {
                        Mobile = "91" + Mobile;
                    }
                    else
                    {
                        return "User Mobile Number Invalid";
                    }

                    string URL = SMSApi + Mobile + "/" + Msg;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    strGatewayResponse = SendSMStoNum(URL);
                    if (isAlphaNumeric(strGatewayResponse) == true)
                    {
                        strGatewayResponse = "SUCCESS";
                    }
                    else
                    {
                        strGatewayResponse = "FAILED-" + strGatewayResponse;
                    }
                    return strGatewayResponse;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + "- Message Not Send";
            }
            return strGatewayResponse;
        }

        public string SendSMStoNum(string url)
        {
            Uri objURI = new Uri(url);
            WebRequest objWebRequest = WebRequest.Create(objURI);
            WebResponse objWebResponse = objWebRequest.GetResponse();
            Stream objStream = objWebResponse.GetResponseStream();
            StreamReader objStreamReader = new StreamReader(objStream);
            string strHTML = objStreamReader.ReadToEnd();
            return strHTML;
        }
        #endregion

        #region[ValidateString]
        public bool isAlphaNumeric(string str)
        {
            try
            {
                Regex pattern = new Regex("[^a-zA-Z0-9]");
                if (pattern.IsMatch(str) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string FiveDigits()
        {
            string sixDigitNumber = "";
            Random objRandom = new Random();
            int intValue = objRandom.Next(10000, 99999);
            return sixDigitNumber = intValue.ToString();
        }
        #endregion

        #region[UserData]
        public string GetCurrentDate()
        {
            string Date = "";
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_BookLR";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetCurrentDate");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Date = dt.Rows[0]["Date"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return Date;
        }

        public DataTable GetUserDetails()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_User";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Get");
                ParameterName.Add("@Id"); ParameterValue.Add(Convert.ToInt32(HttpContext.Current.Session["UserId"]));
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return dt;
            }
            return dt;
        }

        public int SaveActivityLog(string Module, string Operation, string Head)
        {
            int IntResult = 0;
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_ActivityLog";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("Insert");
                ParameterName.Add("@Module"); ParameterValue.Add(Module);
                ParameterName.Add("@Operation"); ParameterValue.Add(Operation);
                ParameterName.Add("@Head"); ParameterValue.Add(Head);
                ParameterName.Add("@UserId"); ParameterValue.Add(Convert.ToInt32(HttpContext.Current.Session["UserId"]));
                IntResult = con.ExecuteQueryByStoreProcedureWithParameter(spName, ParameterName, ParameterValue);
            }
            catch (Exception ex)
            {
                return IntResult;
            }
            return IntResult;
        }

        public DataTable GetCompanyData()
        {
            ParameterName = new ArrayList();
            ParameterValue = new ArrayList();
            string spName = "Proc_CompanyMaster";
            try
            {
                ParameterName.Add("@Action"); ParameterValue.Add("GetAll");
                ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        #endregion

        #region[Internal LR]
        // NEW : Returns the database key of the partner company for the currently logged-in company.
        public string GetPartnerDatabaseKey()
        {
            string CurrentDb = Convert.ToString(HttpContext.Current.Session["Database_"]);
            string PartnerDb = ConfigurationManager.AppSettings["PartnerDb_" + CurrentDb];
            if (string.IsNullOrEmpty(PartnerDb))
            {
                if (CurrentDb == "Vayudoot_")
                {
                    PartnerDb = "Universal_";
                }
                else if (CurrentDb == "Universal_")
                {
                    PartnerDb = "Vayudoot_";
                }
                else
                {
                    PartnerDb = "";
                }
            }
            return PartnerDb;
        }
        #endregion

        #region[Data]
        public void ExportToExcel(DataTable dt, string filename)
        {
            GridView GridView1 = new GridView();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.DataSource = dt;
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            GridView1.CssClass = "ExportGridViewStyle";
            GridView1.HeaderStyle.CssClass = "ExportHeaderStyle";
            GridView1.RowStyle.CssClass = "ExportRowStyle";
            GridView1.AlternatingRowStyle.CssClass = "ExportAltRowStyle";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                row.Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public void ExportToPdf(DataTable dt, string strFilePath)
        {
            Document document = new Document();
            string FileFullPath = HttpContext.Current.Server.MapPath("~/App_Content/JunkPdf/") + strFilePath + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FileFullPath, FileMode.Create));
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPRow row = null;
            float[] widths = new float[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
                widths[i] = 4f;

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        table.AddCell(new Phrase(r[h].ToString(), font5));
                    }
                }
            }
            document.Add(table);
            document.Close();

            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFilePath + ".pdf");
            HttpContext.Current.Response.TransmitFile(FileFullPath);
            HttpContext.Current.Response.End();
        }
        #endregion

       
    }
}