using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ReportCountActivity : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    protected override void OnPreInit(EventArgs e)
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 2)
        {
            this.MasterPageFile = "~/Master/MasterManageView.master";
        }
    }
    DataView dv1;
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, null);

            getddlYear(1);
            GetData();
        }
    }
    private void getddlYear(int mode)
    {
        BTC btc = new BTC();
        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
        }
    }
    private void GetData()
    {
        string strSideName = "'ด้านที่ '";
        string strStandardName = "'มาตรฐานที่ '";
        string strIndicatorName = "'ตัวบ่งชี้ที่ '";
        if (btc.ckIdentityName("ckStandardNew"))
        {
            strSideName = "'มาตรฐานที่ '";
            strStandardName = "'ประเด็นฯที่ '";
            strIndicatorName = "'ประเด็นฯย่อยที่'";
        }

        string strSql = " Select c.SideCode, {0} + Cast(c.Sort As nVarChar) + '. ' + c.SideName FullSideName, c.SideName, c.Sort, "
                + " a.StandardCode, {1} + Cast(a.Sort As nVarChar) + '. ' + a.StandardName FullStandardName, a.StandardName, a.Sort, "
                + " b.IndicatorsCode, {2} + Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' ' + b.IndicatorsName FullIndicatorsName, b.IndicatorsName, b.Sort, "
                + " 0 As CountAc, 0 As CountSt "
                + " From Side c Left Join Standard a On c.SideCode = a.SideCode " 
                + " Left Join Indicators b On a.StandardCode = b.StandardCode "
                + " Where c.DelFlag = 0 And a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlYearB.SelectedValue + "' "  
                + " Group By c.SideCode, c.SideName, c.Sort, a.StandardCode, a.StandardName, a.Sort, b.IndicatorsCode, b.IndicatorsName, b.Sort "
                + " Order By c.Sort, a.Sort, b.Sort ";
        dv1 = Conn.Select(string.Format(strSql, strSideName, strStandardName, strIndicatorName));

        if (dv1.Count != 0)
        {
            Int32 CountAct = 0;
            for (int i = 0; i < dv1.Count; i++)
            {
                dv1[i]["FullStandardName"] = (dv1[i]["FullStandardName"].ToString().Length > 100 ? dv1[i]["FullStandardName"].ToString().Substring(0, 100) + "..." : dv1[i]["FullStandardName"]);
                dv1[i]["FullIndicatorsName"] = (dv1[i]["FullIndicatorsName"].ToString().Length > 100 ? dv1[i]["FullIndicatorsName"].ToString().Substring(0, 100) + "..." : dv1[i]["FullIndicatorsName"]);

                strSql = " Select IsNull(Count(Distinct(ActivityCode)), 0) ActCount From Evaluation "
                    + " Where StandardCode = '" + dv1[i]["StandardCode"].ToString() + "' "
                    + " And IndicatorsCode = '" + dv1[i]["IndicatorsCode"].ToString() + "' "
                    + " And DelFlag = 0 And StudyYear = '" + ddlYearB.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ";
                DataView ckDv = Conn.Select(strSql);

                if (ckDv.Count != 0)
                {
                    dv1[i]["CountAc"] = Convert.ToInt32(ckDv[0]["ActCount"]);
                }
                else
                {
                    dv1[i]["CountAc"] = 0;
                }
            }
        
        DataView dv2 = dv1.ToTable(true, "SideCode", "SideName", "FullSideName", "StandardCode", "FullStandardName", "StandardName").DefaultView;

        rptStandard.DataSource = dv2;
        rptStandard.DataBind();
        }
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
        GetData();
    }
    protected void rptStandard_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater repD = (Repeater)e.Item.FindControl("rptIndicators");
        string MID = ((DataRowView)e.Item.DataItem)["StandardCode"].ToString();
        dv1.RowFilter = string.Format("StandardCode='{0}'", MID);
        repD.DataSource = dv1;
        repD.DataBind();
    }
    string ckSideName = "";
    protected string gettxtSideName(string SideName, string FullSideName)
    {
        string Link = "";
        if (ckSideName != SideName)
        {
            Link = "<li style=\"list-style-image: url(../Image/icon/arrow-right.png)\"><span class=\"spantxtStandardAct\" style=\"font-size:22px;\" title=\"" + SideName + "\">" + FullSideName + "</span></li>";
        }
        ckSideName = SideName;
        return Link;
    }
}
