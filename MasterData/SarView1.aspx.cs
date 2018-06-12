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

public partial class SarView1 : System.Web.UI.Page
{
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
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            getddlYear();
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            
            string yearid = Request["year"];
            string ckview = Request["ckview"];
            string deptid = Request["deptid"];
            string empid = Request["empid"];
            if (!string.IsNullOrEmpty(yearid))
            {
                ddlYearB.SelectedValue = yearid;
            }
            if (!string.IsNullOrEmpty(ckview))
            {
                if (ckview == "0")
                {
                    rbtlView1.Checked = true;
                    rbtlView2.Checked = false;
                }
                else
                {
                    rbtlView1.Checked = false;
                    rbtlView2.Checked = true;
                }
            }
            if (!string.IsNullOrEmpty(deptid))
            {
                ddlSearchDept.SelectedValue = deptid;
            }
            if (!string.IsNullOrEmpty(empid))
            {
                getddlEmpByDept(0, ddlSearchDept.SelectedValue);
                ddlSearchEmp.SelectedValue = empid;
            }

            btc.CkGroupNotSelect(ddlSearchDept, ddlSearchEmp);
            DataBind();
        }
    }
    private void getddlYear()
    {
        BTC btc = new BTC();
        btc.getdllStudyYear(ddlYearB);
        btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
    }
    private void getddlDepartment()
    {
        BTC btc = new BTC();
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-������-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    private void getddlEmpByDept(int mode, string DeptCode)
    {
        BTC btc = new BTC();
        if (mode == 0)
        {
            btc.getddlEmpByDept(0, ddlSearchEmp, DeptCode);
        }
    }
    public override void DataBind() //��ҹ
    {
        DataView dv, dv1, dv2, dv3, dv4, dv5;
        Connection Conn = new Connection();
        BTC btc = new BTC();

        string strSql = " Select a.SideCode, Cast(a.Sort As nVarChar) + ' - ' + a.SideName as SideName, "
                + " IsNull(Sum(c.WeightScore), 0) WeightScore, a.Sort, '' As FullName, a.ScoreGroupID "
                + " From Side a Left Join Standard b On a.SideCode = b.SideCode "
                + " Left Join Indicators c On b.StandardCode = c.StandardCode "
                + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 And a.StudyYear = '" + ddlYearB.SelectedValue + "' And b.StudyYear = '" + ddlYearB.SelectedValue + "' "
                + " Group By a.SideCode, SideName, a.Sort "
                + " Order By a.Sort ";
        dv5 = Conn.Select(strSql);

        double SummarizeCount = 0;
        double SummarizeAvg = 0;
        string strColor = "black";

        for (int m = 0; m < dv5.Count; m++)
        {
            strSql = " Select b.StandardCode, Cast(b.Sort As nVarChar) + ' - ' + b.StandardName as StandardName, "
                + " IsNull(Sum(c.WeightScore), 0) WeightScore, b.Sort "
                + " From Side a Left Join Standard b On a.SideCode = b.SideCode "
                + " Left Join Indicators c On b.StandardCode = c.StandardCode "
                + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
                + " And a.StudyYear = '" + ddlYearB.SelectedValue + "' And b.StudyYear = '" + ddlYearB.SelectedValue + "' "
                + " And b.SideCode = '" + dv5[m]["SideCode"].ToString() + "' "
                + " Group By b.StandardCode, StandardName, b.Sort "
                + " Order By b.Sort ";
            dv = Conn.Select(strSql);

            double CkCriterion = 0;
            double stPercent = 0;
            double ckStScore = 0;
            for (int i = 0; i < dv.Count; i++)
            {
                strSql = " Select b.IndicatorsCode, b.WeightScore, Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + SubString(b.IndicatorsName,1,80) + ' ( ' + Cast(b.WeightScore As nVarChar) + ' )' As IndicatorsName "
                     + " From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode "
                     + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlYearB.SelectedValue + "' "
                     + " And b.StandardCode = '" + dv[i]["StandardCode"].ToString() + "' Order By a.Sort, b.Sort ";
                dv1 = Conn.Select(strSql);

                double IndPercent = 0;
                double ckIndScore = 0;
                for (int j = 0; j < dv1.Count; j++)
                {
                    strSql = " Select a.ProjectsCode From Activity a, Evaluation b "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                    if (ddlSearchDept.SelectedIndex != 0)
                    {
                        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcDept c "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    }
                    if (ddlSearchEmp.SelectedIndex != 0)
                    {
                        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                    }
                    dv2 = Conn.Select(strSql + " Group By a.ProjectsCode ");

                    double PjPercent = 0;
                    for (int k = 0; k < dv2.Count; k++)
                    {
                        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                                + " From Activity a, Evaluation b "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2[k]["ProjectsCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                        if (ddlSearchDept.SelectedIndex != 0)
                        {
                            strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                                + " From Activity a, Evaluation b, dtAcDept c "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2[k]["ProjectsCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                        }
                        if (ddlSearchEmp.SelectedIndex != 0)
                        {
                            strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                                + " From Activity a, Evaluation b, dtAcEmp c "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2[k]["ProjectsCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                        }
                        dv3 = Conn.Select(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort Order By a.Sort ");

                        double AcPercent = 0;
                        for (int l = 0; l < dv3.Count; l++)
                        {
                            strSql = " Select a.ActivityCode "
                                + " From Activity a, Evaluation b "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2[k]["ProjectsCode"].ToString() + "' "
                                + " And b.ActivityCode = '" + dv3[l]["ActivityCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                            if (ddlSearchDept.SelectedIndex != 0)
                            {
                                strSql = " Select a.ActivityCode "
                                + " From Activity a, Evaluation b, dtAcDept c "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2[k]["ProjectsCode"].ToString() + "' "
                                + " And b.ActivityCode = '" + dv3[l]["ActivityCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                            }
                            if (ddlSearchEmp.SelectedIndex != 0)
                            {
                                strSql = " Select a.ActivityCode "
                                + " From Activity a, Evaluation b, dtAcEmp c "
                                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                                + " And b.IndicatorsCode = '" + dv1[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2[k]["ProjectsCode"].ToString() + "' "
                                + " And b.ActivityCode = '" + dv3[l]["ActivityCode"].ToString() + "' "
                                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                            }
                            dv4 = Conn.Select(strSql);

                            AcPercent += (Convert.ToDouble(dv3[l]["CkAPercent"]) * 100) / (Convert.ToDouble(dv4.Count) * 100);
                        }

                        PjPercent += (AcPercent * 100) / (Convert.ToDouble(dv3.Count) * 100);
                    }

                    if (dv2.Count != 0)
                    {
                        IndPercent = (PjPercent * 100) / (Convert.ToDouble(dv2.Count) * 100);
                    }
                    else
                    {
                        IndPercent = 0;
                    }

                    double IndScore = 0;
                    IndScore = (IndPercent * Convert.ToDouble(dv1[j]["WeightScore"])) / 100;

                    //string[] IndCriterion = new BTC().ckTCriteria(Convert.ToDouble(dv1[j]["WeightScore"]), IndScore).Split(':');
                    //ckIndScore += Convert.ToDouble(IndCriterion[0]);

                    ckIndScore += Convert.ToDouble(IndScore.ToString("#0.00"));
                }

                double StScore = 0;
                //StScore = (ckIndScore / Convert.ToDouble(dv1.Count));

                //string[] StCriterion = new BTC().ckTCriteria(Convert.ToDouble(dv[i]["WeightScore"]), StScore).Split(':');
                //ckStScore += Convert.ToDouble(StCriterion[0]);

                ckStScore += Convert.ToDouble(ckIndScore.ToString("#.00"));
            }

            double hScore = 0;
            //hScore = (ckStScore / Convert.ToDouble(dv.Count));

            hScore = ckStScore;

            string[] Tcriterion = new BTC().ckTCriteria(Convert.ToDouble(dv5[m]["ScoreGroupID"]).ToString(), hScore).Split(':');
            double ckScore = Convert.ToDouble(Tcriterion[0]);
            string ckTranslation = Tcriterion[1].ToString();

            //strColor = btc.getColorMaster(hScore, Convert.ToDouble(dv5[m]["WeightScore"]));
            if (ckScore > 1)
            {
                strColor = "#0B610B";
            }
            else
            {
                strColor = "Red";
            }

            double ckPercen = (hScore * 100) / Convert.ToDouble(dv5[m]["WeightScore"]);

            SummarizeCount += ckStScore;

            dv5[m]["FullName"] = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "ballg" : "ballr") + ".png\" style=\"border:none;width:32px;height:35px;\"/>&nbsp;&nbsp;<span style=\"color:" + ((rbtlView1.Checked) ? "#424242" : strColor) + ";font-size: " + btc.fontView2 + "pt;font-weight:bold;width:630px;overflow:hidden;\" title=\"{5}\">{0}</span><span style=\"width:100px; text-align:right ;float:right; margin:20px 20px 0 0;color:" + strColor + ";font-size:" + btc.fontView2 + "pt; font-weight:bold;\">{2}</span><span style=\"width:50px; text-align:right; float:Right; padding:20px 55px 0 0; color:" + strColor + "; font-size:" + btc.fontView2 + "pt; font-weight:bold;\">{1}</span><span style=\"float:right; width:70px; padding:20px 60px 0 0; color:" + strColor + "; font-size:" + btc.fontView2 + "pt; text-align:right; font-weight:bold;\">{3}</span><span style=\"float:right; padding:20px 115px 0 0; color:#000000; font-size:" + btc.fontView2 + "pt; font-weight:bold;\">{4}</span><p style=\"clear:both; float:none;\"></p><hr />", (dv5[m]["SideName"].ToString().Length > btc.strCut1 ? dv5[m]["SideName"].ToString().Substring(0, btc.strCut1) + "..." : dv5[m]["SideName"]), ckScore, ckTranslation, " " + hScore.ToString("#,##0.00") + " ", dv5[m]["WeightScore"], dv5[m]["SideName"]);

        }
        //�Դ����ء�ҵðҹ
        string[] TcriterionTotal = new BTC().ckTCriteria("63B1FF9E-CD47-4C46-9B6D-9F14CEA6E0C0", SummarizeCount).Split(':');
        double ckScoreTotal = Convert.ToDouble(TcriterionTotal[0]);
        string ckTranslationTotal = TcriterionTotal[1].ToString();

        //double hScoreTotal = SummarizeCount / Convert.ToDouble(dv5.Count);

        double hScoreTotal = SummarizeCount;
        SummarizeAvg = (hScoreTotal * 100) / 100;

        //strColor = btc.getColorMaster(SummarizeAvg, 100);
        if (ckScoreTotal > 1)
        {
            strColor = "#0B610B";
        }
        else
        {
            strColor = "Red";
        }

        lblSummarize.Text = string.Format("<img src=\"../Image/icon/" + ((ckScoreTotal > 1) ? "ballg" : "ballr") + ".png\" style=\"border:none;width:36px;height:39px;\"/>&nbsp;&nbsp;<span style=\"color:" + ((rbtlView1.Checked) ? "#000000" : strColor) + ";font-size:" + btc.fontView1 + "pt;font-weight:bold;width:600px;overflow:hidden;\" title=\"������ 5 ��ҹ\">{0}</span><span style=\"width:100px; text-align:right; float:right; margin:20px 20px 0 0; color:" + strColor + "; font-size:" + btc.fontView1 + "pt; font-weight:bold;\">{2}</span><span style=\"width:50px; text-align:right; float:Right; padding:20px 55px 0 0; color:" + strColor + "; font-size:" + btc.fontView1 + "pt; font-weight:bold;\">{1}</span><span style=\"float:right; padding:20px 60px 0 0;color:" + strColor + ";font-size:" + btc.fontView1 + "pt; font-weight:bold;\">{3}</span><span style=\"float:right; padding:20px 130px 0 0; color:#000000; font-size:" + btc.fontView1 + "pt; font-weight:bold;\">100</span><p style=\"clear:both; float:none;\"></p><hr /><br />", "������ 5 ��ҹ", ckScoreTotal, ckTranslationTotal, " " + hScoreTotal.ToString("#,##0.00") + " ");

        Repeater1.DataSource = dv5;
        Repeater1.DataBind();
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
       DataBind();
    }
    protected void rbtView_OnSelectedChanged(object sender, EventArgs e)
    {
       DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
}
