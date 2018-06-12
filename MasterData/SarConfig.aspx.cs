
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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class SarConfig : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (btc.ckIdentityName("ckAnuban"))
            {
                lblStrategySPM.InnerHtml = "กลยุทธ์ สพป. : ";
                lblStrategySPMP.Text = "สำนักงานเขตพื้นที่การศึกษาประถมศึกษา";
            }
            getcblReportProject();
            getcblReportActivity();
            GetRate();
            getddlYear();
            GetData(CurrentUser.SchoolID);
            ckFullText();
        }
        txtUpdateLink.Attributes.Add("onkeyup", "Cktxt(0);");
        txtLogSarLink.Attributes.Add("onkeyup", "Cktxt(0);");
        txtForwardMail.Attributes.Add("onkeyup", "Cktxt(0);");
        //txtIdentity.Attributes.Add("onkeyup", "CkIdentity();");
        //txtIdentity2.Attributes.Add("onkeyup", "CkIdentity2();");
    }
    private void GetRate()
    {
        rbtlRate.Items.Insert(0, new ListItem(" แบบเก่า (1 ตารางทุกหมวดน้ำหนักคะแนน)", "0"));
        rbtlRate.Items.Insert(1, new ListItem(" แบบใหม่ (2 ตารางในบางหมวดน้ำหนักคะแนน)", "1"));
        rbtlRate.Items.Insert(2, new ListItem(" แบบเขต 1 กทม. (12 มาตรฐาน)", "2"));
        rbtlRate.Items.Insert(3, new ListItem(" แบบมาตรฐานใหม่ (4 มาตรฐาน)", "3"));
        rbtlRate.DataBind();
    }
    private void getddlYear()
    {
        btc.getdllStudyYear(ddlYearS);
        btc.getDefault(ddlYearS, "StudyYear", "StudyYear");

        btc.getdllStudyYear(ddlYear);
        btc.getDefault(ddlYear, "StudyYear", "StudyYear");
    }
    private void getcblReportProject()
    {
        cblReportProject.Items.Insert(0, new ListItem(" แบบที่ 1 ", "0"));
        cblReportProject.Items.Insert(1, new ListItem(" แบบที่ 2 ", "1"));
        cblReportProject.Items.Insert(2, new ListItem(" แบบที่ 3 ", "2"));
        cblReportProject.Items.Insert(3, new ListItem(" แบบที่ 4 ", "3"));
        cblReportProject.Items.Insert(4, new ListItem(" แบบที่ 5 ", "4"));
        cblReportProject.Items.Insert(5, new ListItem(" แบบที่ 6 ", "5"));
    }
    private void getcblReportActivity()
    {
        cblReportActivity.Items.Insert(0, new ListItem(" แบบเก่า ", "0"));
        cblReportActivity.Items.Insert(1, new ListItem(" แบบใหม่ ", "1"));
        cblReportActivity.Items.Insert(2, new ListItem(" แบบที่ 3 ", "2"));
    }
    private void getReport(string strReport, CheckBoxList cbl)
    {
        string[] ckReport = strReport.Split(',');
        for (int i = 0; i <= cbl.Items.Count - 1; i++)
        {
            for (int j = 0; j <= ckReport.Length - 1; j++)
            {
                if (cbl.Items[i].Value == ckReport[j].ToString())
                {
                    cbl.Items[i].Selected = true;
                    break;
                }
            }
        }
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select *, IsNull(ckSale, 0) ck_Sale From MR_School Where SchoolID = '" + id + "'"));

        if (dv.Count != 0)
        {
            txtIdentity.Text = dv[0]["IdentityName"].ToString();
            txtIdentity2.Text = dv[0]["IdentityName2"].ToString();
            txtUpdateLink.Text = dv[0]["UpdateLink"].ToString();
            txtLogSarLink.Text = dv[0]["LogSarLink"].ToString();
            txtForwardMail.Text = dv[0]["MailTo"].ToString();
            txtPositionResponsible.Text = dv[0]["PositionResponsible"].ToString();
            txtPositionHeadGroupSara.Text = dv[0]["PositionHeadGroupSara"].ToString();
            txtPositionHeadGroup.Text = dv[0]["PositionHeadGroup"].ToString();

            rbtlYearType.SelectedValue = Convert.ToInt32(dv[0]["ckBudgetYear"]).ToString();
            rbtlIdentity1.SelectedValue = Convert.ToInt32(dv[0]["iNameShow"]).ToString();
            rbtlIdentity2.SelectedValue = Convert.ToInt32(dv[0]["iNameShow2"]).ToString();
            txtPlanNo.Text = dv[0]["PlanNo"].ToString();
            rbtlPlanNo.SelectedValue = Convert.ToInt32(dv[0]["ckPlanNo"]).ToString();
            rbtlPolicy.SelectedValue = Convert.ToInt32(dv[0]["ckPolicy"]).ToString();
            rbtlNewStandard.SelectedValue = Convert.ToInt32(dv[0]["ckNewStandard"]).ToString();
            rbtlStrategicObjectives.SelectedValue = Convert.ToInt32(dv[0]["ckStrategicObjectives"]).ToString();
            rbtlStrategySPT.SelectedValue = Convert.ToInt32(dv[0]["ckStrategySPT"]).ToString();
            rbtlStrategySPM.SelectedValue = Convert.ToInt32(dv[0]["ckStrategySPM"]).ToString();
            rbtlStandardSPM.SelectedValue = Convert.ToInt32(dv[0]["ckStandardSPM"]).ToString();
            rbtlJulaporn.SelectedValue = Convert.ToInt32(dv[0]["ckJuraporn"]).ToString();
            rbtlStrategic.SelectedValue = Convert.ToInt32(dv[0]["ckStrategic"]).ToString();
            rbtlResource.SelectedValue = Convert.ToInt32(dv[0]["ckResource"]).ToString();
            rbtlQA.SelectedValue = Convert.ToInt32(dv[0]["ckQA"]).ToString();
            rbtlWorldClass.SelectedValue = Convert.ToInt32(dv[0]["ckWorldClass"]).ToString();
            rbtlRoyalAward.SelectedValue = Convert.ToInt32(dv[0]["ckRoyalAward"]).ToString();
            rbtlPopularity.SelectedValue = Convert.ToInt32(dv[0]["ckPopularity"]).ToString();
            rbtlPrimePolicy.SelectedValue = Convert.ToInt32(dv[0]["ckPrimePolicy"]).ToString();
            rbtlPoliciesMinister.SelectedValue = Convert.ToInt32(dv[0]["ckPoliciesMinister"]).ToString();
            rbtlPolicyKSN.SelectedValue = Convert.ToInt32(dv[0]["ckPolicyKSN"]).ToString();
            rbtlCorporateStrategy.SelectedValue = Convert.ToInt32(dv[0]["ckCorporateStrategy"]).ToString();
            rbtlStrategicPlan.SelectedValue = Convert.ToInt32(dv[0]["ckStrategicPlan"]).ToString();
            rbtlMainActivity.SelectedValue = Convert.ToInt32(dv[0]["ckMainActivity"]).ToString();
            rbtlTargetMode.SelectedValue = Convert.ToInt32(dv[0]["ckTargetMode"]).ToString();
            rbtlPlan.SelectedValue = Convert.ToInt32(dv[0]["ckPlan"]).ToString();
            rbtlProjectDes.SelectedValue = Convert.ToInt32(dv[0]["ckProjectDes"]).ToString();
            rbtlStrategies.SelectedValue = Convert.ToInt32(dv[0]["ckStrategies"]).ToString();
            rbtlRisk.SelectedValue = Convert.ToInt32(dv[0]["ckRisk"]).ToString();
            rbtlDeptProjects.SelectedValue = Convert.ToInt32(dv[0]["ckDeptProjects"]).ToString();
            rbtlEducation21.SelectedValue = Convert.ToInt32(dv[0]["ckEducation21"]).ToString();
            rbtlSuffice.SelectedValue = Convert.ToInt32(dv[0]["ckSuffice"]).ToString();
            rbtlFourthAssessment.SelectedValue = Convert.ToInt32(dv[0]["ckFourthAssessment"]).ToString();
            rbtlActivityStatus.SelectedValue = Convert.ToInt32(dv[0]["ckActivityStatus"]).ToString();
            rbtlAcAssessment.SelectedValue = Convert.ToInt32(dv[0]["ckAcAssessment"]).ToString();
            rbtBudgetTerm.SelectedValue = Convert.ToInt32(dv[0]["ckBudgetTerm"]).ToString();
            rbtlSuan.SelectedValue = Convert.ToInt32(dv[0]["ckSuan"]).ToString();
            rbtlResponseProblems.SelectedValue = Convert.ToInt32(dv[0]["ckResponseProblems"]).ToString();
            rbtlStatute.SelectedValue = Convert.ToInt32(dv[0]["ckStatute"]).ToString();
            rbtlFullText.SelectedValue = Convert.ToInt32(dv[0]["ckFullText"]).ToString();
            rbtlAnuban.SelectedValue = Convert.ToInt32(dv[0]["ckAnuban"]).ToString();
            rbtlIndicatorSMS.SelectedValue = Convert.ToInt32(dv[0]["ckIndicatorSMS"]).ToString();
            rbtlStandardNew.SelectedValue = Convert.ToInt32(dv[0]["ckStandardNew"]).ToString();
            rbtlMgSignature.SelectedValue = Convert.ToInt32(dv[0]["ckMgSignature"]).ToString();

            rbtlStandardNation.SelectedValue = Convert.ToInt32(dv[0]["ckStandardNation"]).ToString();
            rbtlStandardMinistry.SelectedValue = Convert.ToInt32(dv[0]["ckStandardMinistry"]).ToString();

            rbtlRate.SelectedValue = Convert.ToInt32(dv[0]["ckRate"]).ToString();

            rbtlEstimate.SelectedValue = Convert.ToInt32(dv[0]["ckEstimate"]).ToString();
            rbtlCostsCenter.SelectedValue = Convert.ToInt32(dv[0]["ckCostsCenter"]).ToString();

            rbtlSale.SelectedValue = Convert.ToInt32(dv[0]["ck_Sale"]).ToString();

            cbReportW.Checked = Convert.ToBoolean(dv[0]["ckWord"]);
            cbReportE.Checked = Convert.ToBoolean(dv[0]["ckExcel"]);
            cbReportP.Checked = Convert.ToBoolean(dv[0]["ckPDF"]);

            getReport(dv[0]["ckReportProject"].ToString(), cblReportProject);
            getReport(dv[0]["ckReportActivity"].ToString(), cblReportActivity);

            if (btc.ckGetAdmission(CurrentUser.UserRoleID) != 1)
            {
                btSave.Visible = false;
            }
            else
            {
                btSave.Visible = true;
            }
        }
    }
    private string ckReport(CheckBoxList cbl)
    {
        string ckReport = "";
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            if (cbl.Items[i].Selected == true)
            {
                ckReport += cbl.Items[i].Value +",";
            }
        }
        if (ckReport != "")
        {
            return ckReport.Substring(0, ckReport.Length - 1);
        }
        else
        {
            return ckReport;
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        string ckReportPj = ckReport(cblReportProject);
        string ckReportAc = ckReport(cblReportActivity);

        Int32 i = Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "' ", "IdentityName, IdentityName2, iNameShow, iNameShow2, PlanNo, ckPlanNo, ckNewStandard, ckStrategicObjectives, ckStrategySPT, ckStrategySPM, ckStandardSPM, ckStatute, ckStandardNation, ckStandardMinistry, ckRate, ckJuraporn, ckStrategic, ckResponseProblems, ckCostsCenter, ckEstimate, ckPolicy, ckFullText, ckBudgetYear, ckSale, UpdateLink, LogSarLink, MailTo, PositionResponsible, PositionHeadGroupSara, PositionHeadGroup, ckAnuban, ckIndicatorSMS, ckResource, ckQA, ckWorldClass, ckRoyalAward, ckPopularity, ckPrimePolicy, ckPoliciesMinister, ckPolicyKSN, ckStrategicPlan, ckCorporateStrategy, ckMainActivity, ckTargetMode, ckPlan, ckProjectDes, ckStrategies, ckRisk, ckDeptProjects, ckEducation21, ckSuffice, ckFourthAssessment, ckActivityStatus, ckAcAssessment, ckBudgetTerm, ckSuan, ckWord, ckExcel, ckPDF, UpdateUser, UpdateDate, ckReportProject, ckReportActivity, ckStandardNew, ckMgSignature",
            txtIdentity.Text, txtIdentity2.Text, rbtlIdentity1.SelectedValue, rbtlIdentity2.SelectedValue, txtPlanNo.Text, rbtlPlanNo.SelectedValue, rbtlNewStandard.SelectedValue, rbtlStrategicObjectives.SelectedValue, rbtlStrategySPT.SelectedValue, rbtlStrategySPM.SelectedValue, rbtlStandardSPM.SelectedValue, rbtlStatute.SelectedValue, rbtlStandardNation.SelectedValue, rbtlStandardMinistry.SelectedValue, Convert.ToInt32(rbtlRate.SelectedValue), rbtlJulaporn.SelectedValue, rbtlStrategic.SelectedValue, rbtlResponseProblems.SelectedValue, rbtlCostsCenter.SelectedValue, rbtlEstimate.SelectedValue, rbtlPolicy.SelectedValue, rbtlFullText.SelectedValue, rbtlYearType.SelectedValue, rbtlSale.SelectedValue, txtUpdateLink.Text, txtLogSarLink.Text, txtForwardMail.Text, txtPositionResponsible.Text, txtPositionHeadGroupSara.Text, txtPositionHeadGroup.Text, rbtlAnuban.SelectedValue, rbtlIndicatorSMS.SelectedValue, rbtlResource.SelectedValue, rbtlQA.SelectedValue, rbtlWorldClass.SelectedValue, rbtlRoyalAward.SelectedValue, rbtlPopularity.SelectedValue, rbtlPrimePolicy.SelectedValue, rbtlPoliciesMinister.SelectedValue, rbtlPolicyKSN.SelectedValue, rbtlStrategicPlan.SelectedValue, rbtlCorporateStrategy.SelectedValue, rbtlMainActivity.SelectedValue, rbtlTargetMode.SelectedValue, rbtlPlan.SelectedValue, rbtlProjectDes.SelectedValue, rbtlStrategies.SelectedValue, rbtlRisk.SelectedValue, rbtlDeptProjects.SelectedValue, rbtlEducation21.SelectedValue, rbtlSuffice.SelectedValue, rbtlFourthAssessment.SelectedValue, rbtlActivityStatus.SelectedValue, rbtlAcAssessment.SelectedValue, rbtBudgetTerm.SelectedValue, rbtlSuan.SelectedValue, cbReportW.Checked, cbReportE.Checked, cbReportP.Checked, CurrentUser.ID, DateTime.Now, ckReportPj, ckReportAc, rbtlStandardNew.SelectedValue, rbtlMgSignature.SelectedValue);

        if (rbtlckRate.SelectedIndex == 0)
        {
            try
            {
                Conn.Execute("GenRate", "RateType", rbtlRate.SelectedValue);
                Conn.Execute("GenScoreGroupSideAndStandard", "RateType, StudyYear", rbtlRate.SelectedValue, Convert.ToInt32(ddlYearS.SelectedValue));
                Conn.Execute("GenIndRate", "RateType, StudyYear", rbtlRate.SelectedValue, Convert.ToInt32(ddlYearS.SelectedValue));
            }
            catch (Exception ex)
            {

            }
        }

        if (rbtlYearType.SelectedValue == "1") // ปีงบประมาณ
        {
            Conn.Execute("ckMenuBudgetYear", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuBudgetYear", "ck", 0);
        }

        if (rbtlStandardNation.SelectedValue == "1") // มาตรฐานชาติ
        {
           Conn.Execute("ckMenuStandardNation", "ck", 1);
        }
        else
        {
           Conn.Execute("ckMenuStandardNation", "ck", 0);
        }

        if (rbtlStandardMinistry.SelectedValue == "1") // มาตรฐานกระทรวง
        { 
           Conn.Execute("ckMenuStandardMinistry", "ck", 1);
        }
        else
        {
           Conn.Execute("ckMenuStandardMinistry", "ck", 0);
        }

        if (rbtlNewStandard.SelectedValue == "1") // มาตรฐานใหม่
        {
            Conn.Execute("ckMenuNewStandard", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuNewStandard", "ck", 0);
        }

        if (rbtlStrategicObjectives.SelectedValue == "1") // วัตถุประสงค์เชิงกลยุทธ์
        {
            Conn.Execute("ckMenuStrategicObjectives", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuStrategicObjectives", "ck", 0);
        }

        if (rbtlStrategySPT.SelectedValue == "1") // กลยุทธ์ สพฐ.
        {
            Conn.Execute("ckMenuStrategySPT", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuStrategySPT", "ck", 0);
        }

        if (rbtlStrategySPM.SelectedValue == "1") // กลยุทธ์ สพม. / สพป. (อนุบาล)
        {
            Conn.Execute("ckMenuStrategySPM", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuStrategySPM", "ck", 0);
        }

        if (rbtlStandardSPM.SelectedValue == "1") // มาตรฐาน สพม.
        {
            Conn.Execute("ckMenuStandardSPM", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuStandardSPM", "ck", 0);
        }

        if (rbtlIndicatorSMS.SelectedValue == "1") // ตัวบ่งชี้ สมศ.
        {
            Conn.Execute("ckMenuIndicatorSMS", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuIndicatorSMS", "ck", 0);
        }

        if (rbtlJulaporn.SelectedValue == "1") // โหมดกลุ่มโรงเรียนจุฬาภรณ์
        {
            Conn.Execute("ckModeJulaporn", "ck", 1);
        }
        else
        {
            Conn.Execute("ckModeJulaporn", "ck", 0);
        }

        if (rbtlStrategic.SelectedValue == "1") // ยุทธศาสตร์
        {
            Conn.Execute("ckMenuStrategic", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuStrategic", "ck", 0);
        }

        if (rbtlResponseProblems.SelectedValue == "1") // ปัญหาโครงการ
        {
            Conn.Execute("ckMenuResponseProblems", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuResponseProblems", "ck", 0);
        }

        if (rbtlQA.SelectedValue == "1") // เกณฑ์รางวัลคุณภาพ
        {
            Conn.Execute("ckMenuQA", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuQA", "ck", 0);
        }

        if (rbtlWorldClass.SelectedValue == "1") // เกณฑ์โรงเรียนมาตรฐานสากล
        {
            Conn.Execute("ckMenuWorldClass", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuWorldClass", "ck", 0);
        }

        if (rbtlRoyalAward.SelectedValue == "1") // เกณฑ์โรงเรียนรางวัลพระราชทาน
        {
            Conn.Execute("ckMenuRoyalAward", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuRoyalAward", "ck", 0);
        }

        if (rbtlPopularity.SelectedValue == "1") // ค่านิยม 12 ประการ
        {
            Conn.Execute("ckMenuPopularity", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuPopularity", "ck", 0);
        }

        if (rbtlPrimePolicy.SelectedValue == "1") // นโยบายนายกรัฐมนตรี
        {
            Conn.Execute("ckMenuPrimePolicy", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuPrimePolicy", "ck", 0);
        }

        if (rbtlPoliciesMinister.SelectedValue == "1") // นโยบายรัฐมนตรีว่าการกระทรวง
        {
            Conn.Execute("ckMenuPoliciesMinister", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuPoliciesMinister", "ck", 0);
        }

        if (rbtlPolicyKSN.SelectedValue == "1") // นโยบายการศึกษานอกโรงเรียน กศน.
        {
            Conn.Execute("ckMenuPolicyKSN", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuPolicyKSN", "ck", 0);
        }

        if (rbtlStrategicPlan.SelectedValue == "1") // กลยุทธ์ระดับแผนงาน
        {
            Conn.Execute("ckMenuStrategicPlan", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuStrategicPlan", "ck", 0);
        }

        if (rbtlCorporateStrategy.SelectedValue == "1") // กลยุทธ์ระดับองค์กร
        {
            Conn.Execute("ckMenuCorporateStrategy", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuCorporateStrategy", "ck", 0);
        }

        if (rbtlMainActivity.SelectedValue == "1") // กิจกรรมหลัก
        {
            Conn.Execute("ckMenuMainActivity", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuMainActivity", "ck", 0);
        }

        if (rbtlCostsCenter.SelectedValue == "1") // ต้นทุนศูนย์
        {
            Conn.Execute("ckMenuCostsCenter", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuCostsCenter", "ck", 0);
        }

        //if (rbtlEstimate.SelectedValue == "1") // แบบประเมิน
        //{
        //    Conn.Execute("ckMenuEstimate", "ck", 1);
        //}
        //else
        //{
        //    Conn.Execute("ckMenuEstimate", "ck", 0);
        //}

        if (rbtlPolicy.SelectedValue == "1") // นโยบาย
        {
            Conn.Execute("ckMenuPolicy", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuPolicy", "ck", 0);
        }

        if (rbtlEducation21.SelectedValue == "1") // โหมดจัดการศึกษาตามแนวทางศตวรรษที่ 21
        {
            Conn.Execute("ckMenuEducation21", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuEducation21", "ck", 0);
        }

        if (rbtlSuffice.SelectedValue == "1") // โหมดหลักปรัชญาของเศรษฐกิจพอเพียง
        {
            Conn.Execute("ckMenuSuffice", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuSuffice", "ck", 0);
        }

        if (rbtlFourthAssessment.SelectedValue == "1") // โหมดการประเมินคุณภาพภายนอกรอบ 4
        {
            Conn.Execute("ckMenuFourthAssessment", "ck", 1);
        }
        else
        {
            Conn.Execute("ckMenuFourthAssessment", "ck", 0);
        }

        if (rbtlStandardNew.SelectedValue == "1") //โหมด 4 มาตรฐานใหม่
        {
            Conn.Execute("ckMenuModeStandardNew", "ck", 1);
        }
        else { 
            Conn.Execute("ckMenuModeStandardNew", "ck", 0);
        }
        
        btc.Msg_Head(Img1, MsgHead, true, "2", i);
        rbtlckRate.SelectedIndex = 1;
        PanelRate.Enabled = false;
        GetData(CurrentUser.SchoolID);
    }
    protected void rbtlJulaporn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtlJulaporn.SelectedValue == "1")
        {
            rbtlStrategic.SelectedValue = "1";
        }
        else
        {
            rbtlStrategic.SelectedValue = "0";
        }
    }
    protected void rbtlFullText_SelectedIndexChanged(object sender, EventArgs e)
    {
        ckFullText();
    }
    private void ckFullText()
    {
        if (rbtlFullText.SelectedIndex == 0)
        {
            lblExFullText.Text = "Ex. มาตรฐานที่ 1. xxxxxxxxxxx";
        }
        else
        {
            lblExFullText.Text = "Ex. มาตฐานที่ 1, 2, 3, 4";
        }
    }
    protected void rbtlckRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtlckRate.SelectedIndex == 0)
        {
            PanelRate.Enabled = true;
        }
        else
        {
            PanelRate.Enabled = false;
        }
    }
    protected void rbtlAnuban_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtlAnuban.SelectedIndex == 0)
        {
            lblStrategySPM.InnerHtml = "กลยุทธ์ สพป. : ";
            lblStrategySPMP.Text = "สำนักงานเขตพื้นที่การศึกษาประถมศึกษา";
            rbtlIndicatorSMS.SelectedValue = "1";
            rbtlResource.SelectedValue = "1";
            DivAnubanMaster.Visible = true;
        }
        else
        {
            lblStrategySPM.InnerHtml = "กลยุทธ์ สพม. : ";
            lblStrategySPMP.Text = "สำนักงานเขตพื้นที่การศึกษามัธยมศึกษา";
            rbtlIndicatorSMS.SelectedValue = "0";
            rbtlResource.SelectedValue = "0";
            DivAnubanMaster.Visible = false;
        }
    }
    protected void btCkYear_Click(object sender, EventArgs e)
    {
        int result = 0;

        string strSql = "Select SideCode From Side Where StudyYear = '" + ddlYear.SelectedValue + "'";
        DataView dv = Conn.Select(strSql);
        if(dv.Count > 0)
        {
            result += 1;
        }

        strSql = "Select StandardCode From Standard Where StudyYear = '" + ddlYear.SelectedValue + "'";
        DataView dv1 = Conn.Select(strSql);
        if(dv1.Count > 0)
        {
            result += 1;
        }

        strSql = "Select b.IndicatorsCode From Standard a, Indicators b "
            + " Where a.StandardCode = b.StandardCode And a.StudyYear = '" + ddlYear.SelectedValue + "'";
        DataView dv2 = Conn.Select(strSql);
        if(dv2.Count > 0)
        {
            result += 1;
        }

        if (result == 0)
        {
            Conn.Execute("GenMasterAnuban", "StudyYear",  Convert.ToInt32(ddlYear.SelectedValue));
            lblAnuban.Text = "บันทึกรายการเรียบร้อย";
            lblAnuban.ForeColor = Color.Green;
        }
        else
        {
            lblAnuban.Text = "ไม่สามารถบันทึกได้  เนื่องจากมีข้อมูลในปีที่เลือกนี้  ใช้งานอยู่  ถ้าต้องการบันทึกโปรดลบรายการออกก่อน";
            lblAnuban.ForeColor = Color.Red;
        }
    }
    protected void cblReportProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblReportProject.Text = "";
        if (cblReportProject.SelectedIndex == 0)
        {
            lblReportProject.Text = "- แสดงรายละเอียดกิจกรรมประงบประมาณตามประเภทของงบประมาณ";
        }
        if (cblReportProject.SelectedIndex == 1)
        {
            lblReportProject.Text = "- แสดงรายละเอียดกิจกรรมตามมาตรฐานและตัวบ่งชี้";
        }
        if (cblReportProject.SelectedIndex == 2)
        {
            lblReportProject.Text = "- แสดงรายละเอียดกิจกรรม แจงรายละเอียดขั้นตอนดำเนินงาน PDCA";
        }
        if (cblReportProject.SelectedIndex == 3)
        {
            lblReportProject.Text = "- แสดงรายละเอียดกิจกรรมแบบแยกภาคเรียน";
        }
        if (cblReportProject.SelectedIndex == 4)
        {
            lblReportProject.Text = "- แสดงรายละเอียดกิจกรรมแบบตารางการดำเนินงาน";
        }
        if (cblReportProject.SelectedIndex == 5)
        {
            lblReportProject.Text = "- แสดงรายละเอียดกิจกรรมแบบแจงค่าใช้จ่ายรายกิจกรรม";
        }
    }
}
