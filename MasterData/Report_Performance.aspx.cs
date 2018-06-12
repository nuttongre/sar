using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Star.Security.Cryptography;

public partial class Report_Performance : System.Web.UI.Page
{
    BTC btc = new BTC();

    protected override void OnPreInit(EventArgs e)
    {
        //BTC btc = new BTC();
        //DataView dv = btc.getAdmission(CurrentUser.UserRoleID);
        //if (dv.Count != 0)
        //{
        //    if (Convert.ToInt32(dv[0]["IsManager"]) == 1)
        //    {
        //        this.MasterPageFile = "~/Master/MasterOriginal2.master";
        //    }
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //�礻է�����ҳ
            btc.ckBudgetYear(lblSearchYear, null);

            LinkReport();
            getddlYear(1);
        }
    }
    private void getddlYear(int mode)
    {
        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
        }
    }
    protected void LinkReport()
    {
        string link = " <a href=\"javascript:;\" " + btc.getLinkReportWEP("W") + "  onclick=\"printRpt(6,'w');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"50px;\" height=\"50px;\" title=\"���¡����§ҹ Ẻ�͡��� Word\" src=\"../Image/icon/WordIcon.png\" /></a> "
                    + " <a href=\"javascript:;\" " + btc.getLinkReportWEP("E") + " onclick=\"printRpt(6,'e');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"45px;\" height=\"45px;\" title=\"���¡����§ҹ Ẻ�͡��� Excel\" src=\"../Image/icon/ExcelIcon.png\" /></a> "
                    + " <a href=\"javascript:;\" " + btc.getLinkReportWEP("P") + " onclick=\"printRpt(6,'p');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"45px;\" height=\"45px;\" title=\"���¡����§ҹ Ẻ�͡��� PDF\" src=\"../Image/icon/PdfIcon.png\" /></a> ";
        linkReport.Text = link;
    }
}
