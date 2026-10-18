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
using static System.Net.WebRequestMethods;

namespace Consignment.ConsignmentPanel
{
    public partial class Dashboard : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if ((cm.GetUserDetails().Rows[0]["UserType"]).ToString() != "A")
                {
                    dvConsignor.Visible = false;
                    dvConsignee.Visible = false;
                    dvStation.Visible = false;
                    dvVehicle.Visible = false;
                    dvBranch.Visible = false;
                    dvUnit.Visible = false;
                    dvSize.Visible = false;
                    dvDiesel.Visible = false;
                    dvCashEntry.Visible = false;
                    dvVehicleSize.Visible = false;
                }

                ParameterName = new ArrayList();
                ParameterValue = new ArrayList();
                string spName = "[Proc_DashboardData]";
                try
                {
                    ParameterName.Add("@UserType"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["UserType"]).ToString());
                    ParameterName.Add("@BranchId"); ParameterValue.Add((cm.GetUserDetails().Rows[0]["BranchId"]).ToString());
                    ds = con.GetDataSetFromSP(spName, ParameterName, ParameterValue);
                    if (ds.Tables.Count > 0)
                    {

                        lblConsignor.Text = ds.Tables[0].Rows[0]["Consignor"].ToString();
                        lblConsignee.Text = ds.Tables[1].Rows[0]["Consignee"].ToString();
                        lblStation.Text = ds.Tables[2].Rows[0]["Station"].ToString();
                        lblVehicle.Text = ds.Tables[3].Rows[0]["Vehicle"].ToString();
                        lblBranch.Text = ds.Tables[4].Rows[0]["Branch"].ToString();
                        lblUnit.Text = ds.Tables[5].Rows[0]["Unit"].ToString();
                        lblSize.Text = ds.Tables[6].Rows[0]["Size"].ToString();
                        lblTotalLR.Text = ds.Tables[7].Rows[0]["LREntry"].ToString();
                        lblTodayLR.Text = ds.Tables[8].Rows[0]["TodayLREntry"].ToString();
                        lblDiesel.Text = ds.Tables[9].Rows[0]["Diesel"].ToString();
                        lblCash.Text = ds.Tables[10].Rows[0]["Cash"].ToString();
                        lblVehicleSize.Text = ds.Tables[11].Rows[0]["VehicleSize"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    cm.WriteLog(DateTime.Now.ToString() + " | Page:" + Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + " | Db:" + Session["Database_"] + " | Method/Event:" + System.Reflection.MethodBase.GetCurrentMethod().Name + " | SP:" + spName + " | User:" + Session["UserName"].ToString() + " | Err:" + ex.Message.ToString());
                }
            }
        }
    }
}