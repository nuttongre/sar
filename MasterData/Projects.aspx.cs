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
using System.Text;

public partial class Projects : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    DataView dvWorldClass = null;
    DataView dvRoyalAward = null;
    DataView dvSuffice = null;
    DataView dvFourthAssessment = null;
    DataView dvAssessment = null;

    decimal TotalAmount = 0, TotalSetBudget = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;

            if (!string.IsNullOrEmpty(Request["Cr"]))
            {
                btc.Msg_Head(Img1, MsgHead, true, Request["ckmode"], Convert.ToInt32(Request["Cr"]));
            }

            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);

            getddlYear(0);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            
            string mode = Request.QueryString["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        Session.Remove("WorldClass");
                        Session.Remove("RoyalAward");
                        Session.Remove("Suffice");
                        Session.Remove("FourthAssessment");
                        Session.Remove("Assessment");
                        getddlYear(1);
                        SetItem();
                        string gotoid = Request.QueryString["id"];                       
                        getddlStrategies(1, ddlYearS.SelectedValue);

                        if (!string.IsNullOrEmpty(gotoid))
                        {
                            ddlStrategies.SelectedValue = gotoid;
                        }
                        btc.GenSort(txtSort, "Projects", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        Session.Remove("WorldClass");
                        Session.Remove("RoyalAward");
                        Session.Remove("Suffice");
                        Session.Remove("FourthAssessment");
                        Session.Remove("Assessment");
                        getddlYear(1);
                        //SetItem();
                        btc.btEnable(btSaveAgain, false);
                        btc.btEnable(btSaveAndGo, false);
                        GetData(Request["id"]);
                        break;
                    case "3":
                        MultiView1.ActiveViewIndex = 0;
                        Delete(Request["id"]);
                        break;
                }
            }
            else
            {
                if (btc.ckIdentityName("ckMgSignature"))
                {
                    cbNotManager.Checked = true;
                }
                else
                {
                    cbNotManager.Checked = false;
                }
                DataBind();
            }
        }
        ddlStrategies.Attributes.Add("onchange", "Cktxt(0);");
        txtProjectsDetail.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProjects.Attributes.Add("onkeyup", "Cktxt(0);");
        txtPurpose.Attributes.Add("onkeyup", "Cktxt(0);");
        txtPurpose2.Attributes.Add("onkeyup", "Cktxt(0);");
        txtTarget.Attributes.Add("onkeyup", "Cktxt(0);");
        txtTarget2.Attributes.Add("onkeyup", "Cktxt(0);");
        txtPeriod1.Attributes.Add("onkeyup", "Cktxt(0);");
        txtExpected.Attributes.Add("onkeyup", "Cktxt(0);");
        txtResponsibleName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtResponsiblePosition.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProposeName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProposePosition.Attributes.Add("onkeyup", "Cktxt(0);");
        txtApprovalName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtApprovalPosition.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "return Cktxt(0);");
    }
    private void SetItem()
    {
        lblStrategySPM.Text = btc.getStrategySPMPName() + " : ";

        getlblResponsibleName();

        getddlMission(ddlYearS.SelectedValue);
        getddlGoals(ddlYearS.SelectedValue);
        getcblPolicy(ddlYearS.SelectedValue);
        getcblStandardNation(ddlYearS.SelectedValue);
        getcblStandardMinistry(ddlYearS.SelectedValue);
        getcblNewStandard(ddlYearS.SelectedValue);
        getcblStrategicObjectives(ddlYearS.SelectedValue);
        getcblStrategySPT(ddlYearS.SelectedValue);
        getcblStrategySPM(ddlYearS.SelectedValue);
        getcblStandardSPM(ddlYearS.SelectedValue);
        getcblStrategic(ddlYearS.SelectedValue);
        getcblIndicatorSMS(ddlYearS.SelectedValue);
        getcblPopularity(ddlYearS.SelectedValue);
        getcblPrimePolicy(ddlYearS.SelectedValue);
        getcblPoliciesMinister(ddlYearS.SelectedValue);
        getcblPolicyKSN(ddlYearS.SelectedValue);
        getcblResponseProblems(ddlYearS.SelectedValue);
        btc.getddlCorporateStrategy(divCorporateStrategy, ddlCorporateStrategy, ddlYearS.SelectedValue);
        btc.getcblStrategicPlan(cblStrategicPlan, ddlYearS.SelectedValue);
        btc.getcblCorporateStrategy(cblCorporateStrategy, ddlYearS.SelectedValue);
        btc.getddlStrategicPlan(divStrategicPlan, ddlStrategicPlan, ddlYearS.SelectedValue);

        if (btc.ckIdentityName("ckWorldClass"))
        {
            divWorldClass.Visible = true;
            getddlWorldClassSide(1, ddlYearS.SelectedValue);
            getddlWorldClassStandard(1, ddlYearS.SelectedValue, ddlWorldClassSide.SelectedValue);
        }
        else
        {
            divWorldClass.Visible = false;
        }

        if (btc.ckIdentityName("ckRoyalAward"))
        {
            divRoyalAward.Visible = true;
            getddlRoyalAwardSide(1, ddlYearS.SelectedValue);
            getddlRoyalAwardIndicator(1, ddlYearS.SelectedValue, ddlRoyalAwardSide.SelectedValue);
        }
        else
        {
            divRoyalAward.Visible = false;
        }

        if (btc.ckIdentityName("ckSuffice"))
        {
            divSuffice.Visible = true;
            getddlSufficeSide(1, ddlYearS.SelectedValue);
            getddlSufficeStandard(1, ddlSufficeSide.SelectedValue);
            getddlSufficeIndicator(1, ddlSufficeStandard.SelectedValue);
        }
        else
        {
            divSuffice.Visible = false;
        }

        if (btc.ckIdentityName("ckFourthAssessment"))
        {
            divFourthAssessment.Visible = true;
            getddlFourthAssessmentSide(1, ddlYearS.SelectedValue);
            getddlFourthAssessmentIndicator(1, ddlFourthAssessmentSide.SelectedValue);
        }
        else
        {
            divFourthAssessment.Visible = false;
        }

        if (btc.ckIdentityName("ckTargetMode"))
        {
            divTargetMode.Visible = true;
            getcblTargetMode();
        }
        else
        {
            divTargetMode.Visible = false;
        }

        if (btc.ckIdentityName("ckPlan"))
        {
            divPlan.Visible = true;
            getcblPlan();
        }
        else
        {
            divPlan.Visible = false;
        }

        if (btc.ckIdentityName("ckProjectDes"))
        {
            divProjectDes.Visible = true;
            getcblProjectDes();
        }
        else
        {
            divProjectDes.Visible = false;
        }

        if (btc.ckIdentityName("ckStrategies"))
        {
            divStrategies.Visible = true;
            getcblStrategies(ddlYearS.SelectedValue);
        }
        else
        {
            divStrategies.Visible = false;
        }

        btc.IdentityNameEnable(lblIdentityName, txtIdentityName, "IdentityName", "iNameShow", divIdentityName);
        btc.IdentityNameEnable(lblIdentityName2, txtIdentityName2, "IdentityName2", "iNameShow2", divIdentityName2);
        btc.IdentityNameEnable(lblPlanNo, txtPlanNo, "PlanNo", "ckPlanNo", divPlanNo);
        btc.IdentityNameEnable(lblStatute, txtStatute, "", "ckStatute", divStatute);
        btc.IdentityNameCblEnable(lblNewStandard, cblNewStandard, "", "ckNewStandard", divNewStandard);
        btc.IdentityNameCblEnable(lblStrategicObjectives, cblStrategicObjectives, "", "ckStrategicObjectives", divStrategicObjectives);
        btc.IdentityNameCblEnable(lblStrategySPT, cblStrategySPT, "", "ckStrategySPT", divStrategySPT);
        btc.IdentityNameCblEnable(lblStrategySPM, cblStrategySPM, "", "ckStrategySPM", divStrategySPM);
        btc.IdentityNameCblEnable(lblStandardSPM, cblStandardSPM, "", "ckStandardSPM", divStandardSPM);
        btc.IdentityNameCblEnable(lblIndicatorSMS, cblIndicatorSMS, "", "ckIndicatorSMS", divIndicatorSMS);
        btc.IdentityNameCblEnable(lblPopularity, cblPopularity, "", "ckPopularity", divPopularity);
        btc.IdentityNameCblEnable(lblPrimePolicy, cblPrimePolicy, "", "ckPrimePolicy", divPrimePolicy);
        btc.IdentityNameCblEnable(lblPoliciesMinister, cblPoliciesMinister, "", "ckPoliciesMinister", divPoliciesMinister);
        btc.IdentityNameCblEnable(lblPolicyKSN, cblPolicyKSN, "", "ckPolicyKSN", divPolicyKSN);
        btc.IdentityNameCblEnable(lblPolicy, cblPolicy, "", "ckPolicy", divPolicy);
        btc.IdentityNameCblEnable(lblStandardNation, cblStandardNation, "", "ckStandardNation", divStandardNation);
        btc.IdentityNameCblEnable(lblStandardMinistry, cblStandardMinistry, "", "ckStandardMinistry", divStandardMinistry);
        btc.IdentityNameCblEnable(lblStrategic, cblStrategic, "", "ckStrategic", divStrategic);
        btc.IdentityNameCblEnable(lblResponseProblems, cblResponseProblems, "", "ckResponseProblems", divResponseProblems);
        btc.IdentityNameEnable(lblDeptProjectsName, txtDeptProjectsName, "", "ckDeptProjects", divDeptProjectsName);
    }
    private void getddlYear(int mode)
    {
        if (mode == 0)
        {
            btc.getdllStudyYear(ddlSearchYear);
            btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
        }

        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearS);
            btc.getDefault(ddlYearS, "StudyYear", "StudyYear");
        }
    }
    private void getddlStrategies(int mode, string StudyYear)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, StudyYear);
            if (Cookie.GetValue2("PjStrategies") == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Cookie.GetValue2("PjStrategies").ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch2.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlStrategies(1, ddlStrategies, StudyYear);
        }
    }
    private void getcblTargetMode()
    {
        cblTargetMode.Items.Insert(0, new ListItem(" ด้านผู้เรียน", "0"));
        cblTargetMode.Items.Insert(1, new ListItem(" ด้านการบริหารจัดการ", "1"));
        cblTargetMode.Items.Insert(2, new ListItem(" ด้านครู / บุคลากรทางการศึกษา", "2"));
        cblTargetMode.Items.Insert(3, new ListItem(" ด้านสถานศึกษาและแหล่งเรียนรู้", "3"));
    }
    private void getcblPlan()
    {
        cblPlan.Items.Insert(0, new ListItem(" ด้านคุณภาพผู้เรียน", "0"));
        cblPlan.Items.Insert(1, new ListItem(" ด้านอัตลักษณ์ของสถานศึกษา", "1"));
        cblPlan.Items.Insert(2, new ListItem(" ด้านการสร้างสังคมแห่งการเรียนรู้", "2"));
        cblPlan.Items.Insert(3, new ListItem(" ด้านการบริหารจัดการศึกษา", "3"));
        cblPlan.Items.Insert(4, new ListItem(" ด้านส่งเสริมนโยบาย / จุดเน้น", "4"));
    }
    private void getcblProjectDes()
    {
        cblProjectDes.Items.Insert(0, new ListItem(" โครงการต่อเนื่อง", "0"));
        cblProjectDes.Items.Insert(1, new ListItem(" ยกระดับมาตรฐาน", "1"));
        cblProjectDes.Items.Insert(2, new ListItem(" พัฒนาสู่ความเป็นเลิศ", "2"));
        cblProjectDes.Items.Insert(3, new ListItem(" โครงการใหม่", "3"));
        cblProjectDes.Items.Insert(4, new ListItem(" รักษามาตรฐาน", "4"));
    }
    private void getlblResponsibleName()
    {
        string[] lblName = btc.getlblResponsibleName().Split(',');
        spnResponsibleName.Text = lblName[0];
        spnProjectsApprovalName.Text = lblName[4];
        spnProjectsApprovalName2.Text = lblName[5];
    }
    private void getddlMission(string StudyYear)
    {
        btc.getddlMission(ddlMission, StudyYear);
        btc.getcblMission(cblMission, StudyYear);
    }
    private void getddlGoals(string StudyYear)
    {
        btc.getddlGoals(ddlGoals, StudyYear);
        btc.getcblGoals(cblGoals, StudyYear);
    }
    private void getcblPolicy(string StudyYear)
    {
        btc.getcblPolicy(cblPolicy, StudyYear);
    }
    private void getcblStandardNation(string StudyYear)
    {
        btc.getcblStandardNation(cblStandardNation, StudyYear);
    }
    private void getcblStandardMinistry(string StudyYear)
    {
        btc.getcblStandardMinistry(cblStandardMinistry, StudyYear);
    }
    private void getcblNewStandard(string StudyYear)
    {
        btc.getcblNewStandard(cblNewStandard, StudyYear);
    }
    private void getcblStrategicObjectives(string StudyYear)
    {
        btc.getcblStrategicObjectives(cblStrategicObjectives, StudyYear);
    }
    private void getcblStrategySPT(string StudyYear)
    {
        btc.getcblStrategySPT(cblStrategySPT, StudyYear);
    }
    private void getcblStrategySPM(string StudyYear)
    {
        btc.getcblStrategySPM(cblStrategySPM, StudyYear);
    }
    private void getcblStandardSPM(string StudyYear)
    {
        btc.getcblStandardSPM(cblStandardSPM, StudyYear);
    }
    private void getcblStrategic(string StudyYear)
    {
        btc.getcblStrategic(cblStrategic, StudyYear);
    }
    private void getcblIndicatorSMS(string StudyYear)
    {
        btc.getcblIndicatorSMS(cblIndicatorSMS, StudyYear);
    }
    private void getcblPopularity(string StudyYear)
    {
        btc.getcblPopularity(cblPopularity, StudyYear);
    }
    private void getcblPrimePolicy(string StudyYear)
    {
        btc.getcblPrimePolicy(cblPrimePolicy, StudyYear);
    }
    private void getcblPoliciesMinister(string StudyYear)
    {
        btc.getcblPoliciesMinister(cblPoliciesMinister, StudyYear);
    }
    private void getcblPolicyKSN(string StudyYear)
    {
        btc.getcblPolicyKSN(cblPolicyKSN, StudyYear);
    }
    private void getcblStrategies(string StudyYear)
    {
        btc.getcblStrategies(cblStrategies, StudyYear);
        ckStrategiesError(ddlStrategies.SelectedValue);
    }
    private void ckStrategiesError(string StrategiesCode)
    {
        if (!string.IsNullOrEmpty(StrategiesCode))
        {
            for (int i = 0; i < cblStrategies.Items.Count; i++)
            {
                if (cblStrategies.Items[i].Value.ToUpper() == StrategiesCode.ToUpper())
                {
                    cblStrategies.Items[i].Enabled = false;
                    cblStrategies.Items[i].Selected = false;
                }
                else
                {
                    cblStrategies.Items[i].Enabled = true;
                }
            }
        }
    }
    private void getcblResponseProblems(string StudyYear)
    {
        btc.getcblResponseProblems(cblResponseProblems, StudyYear);
    }
    public override void DataBind()
    {
        TotalAmount = 0;

        string StrSql = " Select a.ProjectsCode, a.StrategiesCode, a.StudyYear, a.ProjectsName, a.Df, IsNull(a.SetBudget, 0) As SetBudget, d.EmpID, d.EmpName, "
                    + " a.Sort, b.Sort Sort1, b.StrategiesName, IsNull(Sum(c.TotalAmount), 0) TotalAmount, '' StandardNation, '' StandardMinistry  "
                    + " From Projects a Left Join Strategies b On a.StrategiesCode = b.StrategiesCode "
                    + " Left Join Activity c On a.ProjectsCode = c.ProjectsCode And c.DelFlag = 0 "
                    + " Left Join Employee d On a.CreateUser = d.EmpID And d.DelFlag = 0 "
                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' "
                    + " And a.SchoolID = '" + CurrentUser.SchoolID + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' ";

        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And a.ProjectsName Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Group By a.ProjectsCode, a.StrategiesCode, a.StudyYear, a.ProjectsName, a.Df, a.SetBudget, d.EmpID, d.EmpName, a.Sort, b.Sort, b.StrategiesName Order By a.Sort Desc "));

        for (int i = 0; i < dv.Count; i++)
        {
           dv[i]["StandardNation"] = btc.getStandardNationName(false, dv[i]["ProjectsCode"].ToString());
           dv[i]["StandardMinistry"] = btc.getStandardMinistryName(false, dv[i]["ProjectsCode"].ToString());
        }

        //เช็คผลรวม
        try
        {
            DataTable dt = dv.ToTable();
            TotalAmount = Convert.ToDecimal(dt.Compute("Sum(TotalAmount)", dv.RowFilter));
            TotalSetBudget = Convert.ToDecimal(dt.Compute("Sum(SetBudget)", dv.RowFilter));
        }
        catch (Exception ex)
        { 
        }
        
        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();

        if (!btc.ckIdentityName("ckMainActivity"))
        {
            GridView1.Columns[GridView1.Columns.Count - 4].Visible = false;
        }

        StrSql = @" Select ckReportProject From MR_School Where SchoolID = '{0}' And DelFlag = 0 ";
        DataView dvCkReport = Conn.Select(string.Format(StrSql, CurrentUser.SchoolID));
        if (dvCkReport.Count != 0)
        {
            string[] ckReportProject = dvCkReport[0]["ckReportProject"].ToString().Split(',');

            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            GridView1.Columns[10].Visible = false;
            for (int i = 0; i < ckReportProject.Length; i++)
            {
                if (ckReportProject[i] == "0")
                {
                    GridView1.Columns[5].Visible = true;
                }
                if (ckReportProject[i] == "1")
                {
                    GridView1.Columns[6].Visible = true;
                }
                if (ckReportProject[i] == "2")
                {
                    GridView1.Columns[7].Visible = true;
                }
                if (ckReportProject[i] == "3")
                {
                    GridView1.Columns[8].Visible = true;
                }
                if (ckReportProject[i] == "4")
                {
                    GridView1.Columns[9].Visible = true;
                }
                if (ckReportProject[i] == "5")
                {
                    GridView1.Columns[10].Visible = true;
                }
            }
        }

        //----GrandTotal-----------
        StrSql = " Select IsNull(Sum(c.TotalAmount), 0) TotalAmount "
                    + " From Projects a Left Join Strategies b On a.StrategiesCode = b.StrategiesCode "
                    + " Left Join Activity c On a.ProjectsCode = c.ProjectsCode And c.DelFlag = 0 "
                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' "
                    + " And a.SchoolID = '" + CurrentUser.SchoolID + "' ";
        DataView dvTotal = Conn.Select(StrSql);
        ToltalBudget.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount"]).ToString("#,##0.00") : "0.00";
        //----EndGrandTotal-----------

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select *, IsNull(SetBudget, 0) Set_Budget From Projects Where ProjectsCode = '" + id + "'"));
        DataView dv0 = Conn.Select(string.Format("Select PolicyCode From dtPolicy Where ProjectsCode = '" + id + "' "));
        DataView dv1 = Conn.Select(string.Format("Select StandardNationCode From dtStandardNation Where ProjectsCode = '" + id + "' "));
        DataView dv2 = Conn.Select(string.Format("Select StandardMinistryCode From dtStandardMinistry Where ProjectsCode = '" + id + "' "));
        DataView dv33 = Conn.Select(string.Format("Select StandardCode From dtNewStandard Where ProjectsCode = '" + id + "' "));
        DataView dv333 = Conn.Select(string.Format("Select StrategicObjectivesCode From dtStrategicObjectives Where ProjectsCode = '" + id + "' "));
        DataView dv3 = Conn.Select(string.Format("Select StrategySPTCode From dtStrategySPT Where ProjectsCode = '" + id + "' "));
        DataView dv4 = Conn.Select(string.Format("Select StrategySPMCode From dtStrategySPM Where ProjectsCode = '" + id + "' "));
        DataView dv5 = Conn.Select(string.Format("Select StandardSPMCode From dtStandardSPM Where ProjectsCode = '" + id + "' "));
        DataView dv6 = Conn.Select(string.Format("Select ResponseProblemsCode From dtResponseProblems Where ProjectsCode = '" + id + "' "));
        DataView dv7 = Conn.Select(string.Format("Select IndicatorSMSCode From dtIndicatorSMS Where ProjectsCode = '" + id + "' "));
        DataView dv8 = Conn.Select(string.Format("Select PopularityID From dtPopularity Where ProjectsCode = '" + id + "' "));
        DataView dv9 = Conn.Select(string.Format("Select StrategicCode From dtStrategic Where ProjectsCode = '" + id + "' "));
        DataView dv10 = Conn.Select(string.Format("Select MissionCode From dtMission Where ProjectsCode = '" + id + "' "));
        DataView dv11 = Conn.Select(string.Format("Select GoalsCode From dtGoals Where ProjectsCode = '" + id + "'"));
        DataView dv12 = Conn.Select(string.Format("Select PrimePolicyID From dtPrimePolicy Where ProjectsCode = '" + id + "'"));
        DataView dv13 = Conn.Select(string.Format("Select PoliciesMinisterID From dtPoliciesMinister Where ProjectsCode = '" + id + "'"));
        DataView dv14 = Conn.Select(string.Format("Select PolicyKSNID From dtPolicyKSN Where ProjectsCode = '" + id + "'"));
        DataView dv15 = Conn.Select(string.Format("Select TargetMode From dtTargetMode Where ProjectsCode = '" + id + "'"));
        DataView dv16 = Conn.Select(string.Format("Select ProjectDes From dtProjectDes Where ProjectsCode = '" + id + "'"));
        DataView dv17 = Conn.Select(string.Format("Select PlanMode From dtPlan Where ProjectsCode = '" + id + "'"));
        DataView dv18 = Conn.Select(string.Format("Select StrategicPlanID From dtStrategicPlan Where ProjectsCode = '" + id + "' "));
        DataView dv19 = Conn.Select(string.Format("Select CorporateStrategyID From dtCorporateStrategy Where ProjectsCode = '" + id + "' "));

        if (dv.Count != 0)
        {
            ddlYearS.SelectedValue = dv[0]["StudyYear"].ToString();
            SetItem();
            getddlStrategies(1, ddlYearS.SelectedValue);
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            ckStrategiesError(ddlStrategies.SelectedValue);
            if (btc.CkUseData(id, "ProjectsCode", "Activity", " And DelFlag = 0 "))
            {
                ddlYearS.Enabled = false;
                //ddlStrategies.Enabled = false;
            }
            ddlMission.SelectedValue = dv[0]["MissionCode"].ToString();
            ddlGoals.SelectedValue = dv[0]["GoalsCode"].ToString();
            ddlStrategicPlan.SelectedValue = dv[0]["StrategicPlanID"].ToString();
            ddlCorporateStrategy.SelectedValue = dv[0]["CorporateStrategyID"].ToString();
            txtProjects.Text = dv[0]["ProjectsName"].ToString();
            txtIdentityName.Text = dv[0]["IdentityName"].ToString();
            txtIdentityName2.Text = dv[0]["IdentityName2"].ToString();
            txtStatute.Text = dv[0]["StatuteNo"].ToString();
            txtPlanNo.Text = dv[0]["PlanNo"].ToString();
            txtPurpose.Text = dv[0]["Purpose"].ToString();
            txtPurpose2.Text = dv[0]["Purpose2"].ToString();
            txtTarget.Text = dv[0]["Target"].ToString();
            txtTarget2.Text = dv[0]["Target2"].ToString();
            txtPeriod1.Text = dv[0]["Period1"].ToString();
            txtProjectsDetail.Text = dv[0]["ProjectsDetail"].ToString();
            txtExpected.Text = dv[0]["Expected"].ToString();
            txtSetBudget.Text = Convert.ToDecimal(dv[0]["Set_Budget"]).ToString("#,##0.00");
            txtResponsibleName.Text = dv[0]["ResponsibleName"].ToString();
            txtResponsiblePosition.Text = dv[0]["ResponsiblePosition"].ToString();
            txtProposeName.Text = dv[0]["ProposeName"].ToString();
            txtProposePosition.Text = dv[0]["ProposePosition"].ToString();
            txtApprovalName.Text = dv[0]["ApprovalName"].ToString();
            txtApprovalPosition.Text = dv[0]["ApprovalPosition"].ToString();
            txtApprovalName2.Text = dv[0]["ApprovalName2"].ToString();
            txtApprovalPosition2.Text = dv[0]["ApprovalPosition2"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
            txtDeptProjectsName.Text = dv[0]["DeptProjectsName"].ToString();
        }
        btc.getCreateUpdateUser(lblCreate, lblUpdate, "Projects", "ProjectsCode", id);
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            lbtEditCreate.Visible = true;
        }

        if (dv18.Count != 0)
        {
            for (int i = 0; i <= cblStrategicPlan.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv18.Count - 1; j++)
                {
                    if (cblStrategicPlan.Items[i].Value == dv18[j]["StrategicPlanID"].ToString())
                    {
                        cblStrategicPlan.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv19.Count != 0)
        {
            for (int i = 0; i <= cblCorporateStrategy.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv19.Count - 1; j++)
                {
                    if (cblCorporateStrategy.Items[i].Value == dv19[j]["CorporateStrategyID"].ToString())
                    {
                        cblCorporateStrategy.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }


        if (dv10.Count != 0)
        {
            for (int i = 0; i <= cblMission.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv10.Count - 1; j++)
                {
                    if (cblMission.Items[i].Value == dv10[j]["MissionCode"].ToString())
                    {
                        cblMission.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv11.Count != 0)
        {
            for (int i = 0; i <= cblGoals.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv11.Count - 1; j++)
                {
                    if (cblGoals.Items[i].Value == dv11[j]["GoalsCode"].ToString())
                    {
                        cblGoals.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv0.Count != 0)
        {
            for (int i = 0; i <= cblPolicy.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv0.Count - 1; j++)
                {
                    if (cblPolicy.Items[i].Value == dv0[j]["PolicyCode"].ToString())
                    {
                        cblPolicy.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv1.Count != 0)
        {
            for (int i = 0; i <= cblStandardNation.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv1.Count - 1; j++)
                {
                    if (cblStandardNation.Items[i].Value == dv1[j]["StandardNationCode"].ToString())
                    {
                        cblStandardNation.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv2.Count != 0)
        {
            for (int i = 0; i <= cblStandardMinistry.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv2.Count - 1; j++)
                {
                    if (cblStandardMinistry.Items[i].Value == dv2[j]["StandardMinistryCode"].ToString())
                    {
                        cblStandardMinistry.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv33.Count != 0)
        {
            for (int i = 0; i <= cblNewStandard.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv33.Count - 1; j++)
                {
                    if (cblNewStandard.Items[i].Value == dv33[j]["StandardCode"].ToString())
                    {
                        cblNewStandard.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv333.Count != 0)
        {
            for (int i = 0; i <= cblStrategicObjectives.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv333.Count - 1; j++)
                {
                    if (cblStrategicObjectives.Items[i].Value == dv333[j]["StrategicObjectivesCode"].ToString())
                    {
                        cblStrategicObjectives.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv3.Count != 0)
        {
            for (int i = 0; i <= cblStrategySPT.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv3.Count - 1; j++)
                {
                    if (cblStrategySPT.Items[i].Value == dv3[j]["StrategySPTCode"].ToString())
                    {
                        cblStrategySPT.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv4.Count != 0)
        {
            for (int i = 0; i <= cblStrategySPM.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv4.Count - 1; j++)
                {
                    if (cblStrategySPM.Items[i].Value == dv4[j]["StrategySPMCode"].ToString())
                    {
                        cblStrategySPM.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv5.Count != 0)
        {
            for (int i = 0; i <= cblStandardSPM.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv5.Count - 1; j++)
                {
                    if (cblStandardSPM.Items[i].Value == dv5[j]["StandardSPMCode"].ToString())
                    {
                        cblStandardSPM.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv6.Count != 0)
        {
            for (int i = 0; i <= cblResponseProblems.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv6.Count - 1; j++)
                {
                    if (cblResponseProblems.Items[i].Value == dv6[j]["ResponseProblemsCode"].ToString())
                    {
                        cblResponseProblems.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv7.Count != 0)
        {
            for (int i = 0; i <= cblIndicatorSMS.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv7.Count - 1; j++)
                {
                    if (cblIndicatorSMS.Items[i].Value == dv7[j]["IndicatorSMSCode"].ToString())
                    {
                        cblIndicatorSMS.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv8.Count != 0)
        {
            for (int i = 0; i <= cblPopularity.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv8.Count - 1; j++)
                {
                    if (cblPopularity.Items[i].Value == dv8[j]["PopularityID"].ToString())
                    {
                        cblPopularity.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv9.Count != 0)
        {
            for (int i = 0; i <= cblStrategic.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv9.Count - 1; j++)
                {
                    if (cblStrategic.Items[i].Value == dv9[j]["StrategicCode"].ToString())
                    {
                        cblStrategic.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv12.Count != 0)
        {
            for (int i = 0; i <= cblPrimePolicy.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv12.Count - 1; j++)
                {
                    if (cblPrimePolicy.Items[i].Value == dv12[j]["PrimePolicyID"].ToString())
                    {
                        cblPrimePolicy.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv13.Count != 0)
        {
            for (int i = 0; i <= cblPoliciesMinister.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv13.Count - 1; j++)
                {
                    if (cblPoliciesMinister.Items[i].Value == dv13[j]["PoliciesMinisterID"].ToString())
                    {
                        cblPoliciesMinister.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv14.Count != 0)
        {
            for (int i = 0; i <= cblPolicyKSN.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv14.Count - 1; j++)
                {
                    if (cblPolicyKSN.Items[i].Value == dv14[j]["PolicyKSNID"].ToString())
                    {
                        cblPolicyKSN.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv15.Count != 0)
        {
            for (int i = 0; i <= cblTargetMode.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv15.Count - 1; j++)
                {
                    if (cblTargetMode.Items[i].Value == dv15[j]["TargetMode"].ToString())
                    {
                        cblTargetMode.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv16.Count != 0)
        {
            for (int i = 0; i <= cblProjectDes.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv16.Count - 1; j++)
                {
                    if (cblProjectDes.Items[i].Value == dv16[j]["ProjectDes"].ToString())
                    {
                        cblProjectDes.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv17.Count != 0)
        {
            for (int i = 0; i <= cblPlan.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv17.Count - 1; j++)
                {
                    if (cblPlan.Items[i].Value == dv17[j]["PlanMode"].ToString())
                    {
                        cblPlan.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (btc.ckIdentityName("ckStrategies"))
        {
            DataView dv100 = Conn.Select(string.Format("Select StrategiesCode From dtStrategies Where ProjectsCode = '" + id + "'"));
            if (dv100.Count != 0)
            {
                for (int i = 0; i <= cblStrategies.Items.Count - 1; i++)
                {
                    for (int j = 0; j <= dv100.Count - 1; j++)
                    {
                        if (cblStrategies.Items[i].Value == dv100[j]["StrategiesCode"].ToString())
                        {
                            cblStrategies.Items[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        if (btc.ckIdentityName("ckWorldClass"))
        {
            string strSql = @" Select dtC.WorldClassStandardID, a.WorldClassSideName As WorldClassSideName, 
                Cast(a.Sort AS nVarChar) + '.' + Cast(b.Sort AS nVarChar) + ' - ' + b.WorldClassStandardName as WorldClassStandardName
                From dtWorldClass dtC, WorldClassSide a, WorldClassStandard b 
                Where dtC.ProjectsCode = '{0}' And dtC.WorldClassStandardID = b.WorldClassStandardID 
                And a.WorldClassSideID = b.WorldClassSideID Order By a.MsSideSort, a.Sort, b.Sort ";
            dvWorldClass = Conn.Select(string.Format(strSql, id));

            if (dvWorldClass.Count != 0)
            {
                btDelWorldClass.Visible = true;
                if (Session["WorldClass"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("WorldClassStandardID");
                    dt1.Columns.Add("WorldClassSideName");
                    dt1.Columns.Add("WorldClassStandardName");

                    DataRow dr;
                    for (int i = 0; i < dvWorldClass.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["WorldClassStandardID"] = dvWorldClass[i]["WorldClassStandardID"].ToString();
                        dr["WorldClassSideName"] = dvWorldClass[i]["WorldClassSideName"].ToString();
                        dr["WorldClassStandardName"] = dvWorldClass[i]["WorldClassStandardName"].ToString();
                        dt1.Rows.Add(dr);
                    }

                    dvWorldClass = dt1.DefaultView;
                    Session["WorldClass"] = dt1;
                    gridWorldClass.DataSource = dvWorldClass;
                    gridWorldClass.CheckListDataField = "WorldClassStandardID";
                    gridWorldClass.DataBind();
                }
            }
        }

        if (btc.ckIdentityName("ckRoyalAward"))
        {
            string strSql = @" Select dtC.RoyalAwardIndicatorID, Cast(a.Sort AS nVarChar) + ' - ' + a.RoyalAwardSideName As RoyalAwardSideName, 
                Cast(a.Sort AS nVarChar) + '.' + Cast(b.Sort AS nVarChar) + ' - ' + b.RoyalAwardIndicatorName as RoyalAwardIndicatorName
                From dtRoyalAward dtC, RoyalAwardSide a, RoyalAwardIndicator b 
                Where dtC.ProjectsCode = '{0}' And dtC.RoyalAwardIndicatorID = b.RoyalAwardIndicatorID 
                And a.RoyalAwardSideID = b.RoyalAwardSideID Order By a.Sort, b.Sort ";
            dvRoyalAward = Conn.Select(string.Format(strSql, id));

            if (dvRoyalAward.Count != 0)
            {
                btDelRoyalAward.Visible = true;
                if (Session["RoyalAward"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("RoyalAwardIndicatorID");
                    dt1.Columns.Add("RoyalAwardSideName");
                    dt1.Columns.Add("RoyalAwardIndicatorName");

                    DataRow dr;
                    for (int i = 0; i < dvRoyalAward.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["RoyalAwardIndicatorID"] = dvRoyalAward[i]["RoyalAwardIndicatorID"].ToString();
                        dr["RoyalAwardSideName"] = dvRoyalAward[i]["RoyalAwardSideName"].ToString();
                        dr["RoyalAwardIndicatorName"] = dvRoyalAward[i]["RoyalAwardIndicatorName"].ToString();
                        dt1.Rows.Add(dr);
                    }

                    dvRoyalAward = dt1.DefaultView;
                    Session["RoyalAward"] = dt1;
                    gridRoyalAward.DataSource = dvRoyalAward;
                    gridRoyalAward.CheckListDataField = "RoyalAwardIndicatorID";
                    gridRoyalAward.DataBind();
                }
            }
        }

        if (btc.ckIdentityName("ckSuffice"))
        {
            string strSql = @" Select dtC.SufficeIndicatorID, a.SufficeSideName As SufficeSideName, 
                Cast(a.Sort AS nVarChar) + '.' + Cast(b.Sort AS nVarChar) + ' - ' + b.SufficeStandardName As SufficeStandardName,
                Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + '.' + Cast(c.Sort AS nVarChar) + ' - ' + c.SufficeIndicatorname As SufficeIndicatorName
                From dtSuffice dtC, SufficeSide a, SufficeStandard b, SufficeIndicator c
                Where dtC.ProjectsCode = '{0}' And dtC.SufficeIndicatorID = c.SufficeIndicatorID And c.SufficeStandardID = b.SufficeStandardID  
                And a.SufficeSideID = b.SufficeSideID Order By a.Sort, b.Sort, c.Sort ";
            dvSuffice = Conn.Select(string.Format(strSql, id));

            if (dvSuffice.Count != 0)
            {
                btDelSuffice.Visible = true;
                if (Session["Suffice"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("SufficeIndicatorID");
                    dt1.Columns.Add("SufficeSideName");
                    dt1.Columns.Add("SufficeStandardName");
                    dt1.Columns.Add("SufficeIndicatorName");

                    DataRow dr;
                    for (int i = 0; i < dvSuffice.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["SufficeIndicatorID"] = dvSuffice[i]["SufficeIndicatorID"].ToString();
                        dr["SufficeSideName"] = dvSuffice[i]["SufficeSideName"].ToString();
                        dr["SufficeStandardName"] = dvSuffice[i]["SufficeStandardName"].ToString();
                        dr["SufficeIndicatorName"] = dvSuffice[i]["SufficeIndicatorName"].ToString();
                        dt1.Rows.Add(dr);
                    }

                    dvSuffice = dt1.DefaultView;
                    Session["Suffice"] = dt1;
                    gridSuffice.DataSource = dvSuffice;
                    gridSuffice.CheckListDataField = "SufficeIndicatorID";
                    gridSuffice.DataBind();
                }
            }
        }

        if (btc.ckIdentityName("ckFourthAssessment"))
        {
            string strSql = @" Select dtC.FourthAssessmentIndicatorID, Cast(a.Sort AS nVarChar) + ' - ' + a.FourthAssessmentSideName As FourthAssessmentSideName, 
                Cast(a.Sort AS nVarChar) + '.' + Cast(b.Sort AS nVarChar) + ' - ' + b.FourthAssessmentIndicatorName as FourthAssessmentIndicatorName
                From dtFourthAssessment dtC, FourthAssessmentSide a, FourthAssessmentIndicator b 
                Where dtC.ProjectsCode = '{0}' And dtC.FourthAssessmentIndicatorID = b.FourthAssessmentIndicatorID 
                And a.FourthAssessmentSideID = b.FourthAssessmentSideID Order By a.Sort, b.Sort ";
            dvFourthAssessment = Conn.Select(string.Format(strSql, id));

            if (dvFourthAssessment.Count != 0)
            {
                btDelFourthAssessment.Visible = true;
                if (Session["FourthAssessment"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("FourthAssessmentIndicatorID");
                    dt1.Columns.Add("FourthAssessmentSideName");
                    dt1.Columns.Add("FourthAssessmentIndicatorName");

                    DataRow dr;
                    for (int i = 0; i < dvFourthAssessment.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["FourthAssessmentIndicatorID"] = dvFourthAssessment[i]["FourthAssessmentIndicatorID"].ToString();
                        dr["FourthAssessmentSideName"] = dvFourthAssessment[i]["FourthAssessmentSideName"].ToString();
                        dr["FourthAssessmentIndicatorName"] = dvFourthAssessment[i]["FourthAssessmentIndicatorName"].ToString();
                        dt1.Rows.Add(dr);
                    }

                    dvFourthAssessment = dt1.DefaultView;
                    Session["FourthAssessment"] = dt1;
                    gridFourthAssessment.DataSource = dvFourthAssessment;
                    gridFourthAssessment.CheckListDataField = "FourthAssessmentIndicatorID";
                    gridFourthAssessment.DataBind();
                }
            }
        }

        string StrSql = " Select ProjectsCode, RecNum As id, IndicatorsName, MethodAss, ToolsAss "
                + " From ProjectsAssessment "
                + " Where ProjectsCode = '{0}' ";
        dvAssessment = Conn.Select(string.Format(StrSql + " Order By RecNum ", id));

        if (dvAssessment.Count != 0)
        {
            btDelAssessment.Visible = true;
            if (Session["Assessment"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("id");
                dt1.Columns.Add("IndicatorsName");
                dt1.Columns.Add("MethodAss");
                dt1.Columns.Add("ToolsAss");

                DataRow dr;
                for (int i = 0; i < dvAssessment.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["id"] = dvAssessment[i]["id"].ToString();
                    dr["IndicatorsName"] = dvAssessment[i]["IndicatorsName"].ToString();
                    dr["MethodAss"] = dvAssessment[i]["MethodAss"].ToString();
                    dr["ToolsAss"] = dvAssessment[i]["ToolsAss"].ToString();
                    dt1.Rows.Add(dr);
                }
                dvAssessment = dt1.DefaultView;
                Session["Assessment"] = dt1;

                GridViewAssessment.DataSource = dvAssessment;
                GridViewAssessment.CheckListDataField = "id";
                GridViewAssessment.DataBind();
            }
        }
    }
    private void ClearAll()
    {
        Session.Remove("WorldClass");
        Session.Remove("RoyalAward");
        Session.Remove("Suffice");
        Session.Remove("FourthAssessment");
        Session.Remove("Assessment");
        txtProjects.Text = "";
        txtProjectsDetail.Text = "";
        txtPurpose.Text = "";
        txtPurpose2.Text = "";
        txtTarget.Text = "";
        txtTarget2.Text = "";
        txtPeriod1.Text = "";
        txtProjectsDetail.Text = "";
        txtExpected.Text = "";
        txtSetBudget.Text = "0";
        txtResponsibleName.Text = "";
        txtResponsiblePosition.Text = "";
        txtProposeName.Text = "";
        txtProposePosition.Text = "";
        txtApprovalName.Text = "";
        txtApprovalPosition.Text = "";
        txtApprovalName2.Text = "";
        txtApprovalPosition2.Text = "";
        txtStatute.Text = "";
        txtDeptProjectsName.Text = "";
        ddlGoals.SelectedIndex = 0;
        ddlMission.SelectedIndex = 0;
        txtSearch.Text = "";
        cblStandardMinistry.ClearSelection();
        cblPolicy.ClearSelection();
        cblStandardNation.ClearSelection();
        cblNewStandard.ClearSelection();
        cblStrategicObjectives.ClearSelection();
        cblStrategySPT.ClearSelection();
        cblStrategySPM.ClearSelection();
        cblStandardSPM.ClearSelection();
        cblIndicatorSMS.ClearSelection();
        cblPopularity.ClearSelection();
        cblResponseProblems.ClearSelection();
        cblStrategic.ClearSelection();
        cblStrategicPlan.ClearSelection();
        cblCorporateStrategy.ClearSelection();
        cblMission.ClearSelection();
        cblGoals.ClearSelection();
        cblPrimePolicy.ClearSelection();
        cblPoliciesMinister.ClearSelection();
        cblPolicyKSN.ClearSelection();
        cblStrategies.ClearSelection();

        gridWorldClass.DataSource = null;
        gridWorldClass.DataBind();
        btDelWorldClass.Visible = false;

        gridRoyalAward.DataSource = null;
        gridRoyalAward.DataBind();
        btDelRoyalAward.Visible = false;

        gridSuffice.DataSource = null;
        gridSuffice.DataBind();
        btDelSuffice.Visible = false;

        gridFourthAssessment.DataSource = null;
        gridFourthAssessment.DataBind();
        btDelFourthAssessment.Visible = false;
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        Int32 i = 0;
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            i = Conn.AddNew("Projects", "ProjectsCode, StrategiesCode, MissionCode, GoalsCode, StudyYear, ProjectsName, Purpose, Purpose2, Target, Target2, Period1, ProjectsDetail, Expected, SetBudget, ResponsibleName, ResponsiblePosition, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2, PlanNo, StatuteNo, IdentityName, IdentityName2, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate, StrategicPlanID, CorporateStrategyID, DeptProjectsName", 
                NewID, ddlStrategies.SelectedValue, ddlMission.SelectedValue, ddlGoals.SelectedValue, ddlYearS.SelectedValue, txtProjects.Text, txtPurpose.Text, txtPurpose2.Text, txtTarget.Text, txtTarget2.Text, txtPeriod1.Text, txtProjectsDetail.Text, txtExpected.Text, Convert.ToDecimal(txtSetBudget.Text), txtResponsibleName.Text, txtResponsiblePosition.Text, txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, txtPlanNo.Text, txtStatute.Text, txtIdentityName.Text, txtIdentityName2.Text, txtSort.Text, 1, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now, ddlStrategicPlan.SelectedValue, ddlCorporateStrategy.SelectedValue, txtDeptProjectsName.Text);

            for (int j = 0; j <= cblStrategicPlan.Items.Count - 1; j++)
            {
                if (cblStrategicPlan.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtStrategicPlan (ProjectsCode, StrategicPlanID)VALUES('{0}','{1}');", NewID, cblStrategicPlan.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblCorporateStrategy.Items.Count - 1; j++)
            {
                if (cblCorporateStrategy.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtCorporateStrategy (ProjectsCode, CorporateStrategyID)VALUES('{0}','{1}');", NewID, cblCorporateStrategy.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblMission.Items.Count - 1; j++)
            {
                if (cblMission.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtMission (ProjectsCode, MissionCode)VALUES('{0}','{1}');", NewID, cblMission.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblGoals.Items.Count - 1; j++)
            {
                if (cblGoals.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtGoals (ProjectsCode, GoalsCode)VALUES('{0}','{1}');", NewID, cblGoals.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblPolicy.Items.Count - 1; j++)
            {
                if (cblPolicy.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtPolicy (ProjectsCode, PolicyCode)VALUES('{0}','{1}');", NewID, cblPolicy.Items[j].Value);
                    //i = Conn.AddNew("dtPolicy", "ProjectsCode, PolicyCode", NewID, cblPolicy.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblStandardNation.Items.Count - 1; j++)
            {
                if (cblStandardNation.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtStandardNation (ProjectsCode, StandardNationCode)VALUES('{0}','{1}');", NewID, cblStandardNation.Items[j].Value);
                    //i = Conn.AddNew("dtStandardNation", "ProjectsCode, StandardNationCode", NewID, cblStandardNation.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblStandardMinistry.Items.Count - 1; j++)
            {
                if (cblStandardMinistry.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtStandardMinistry (ProjectsCode, StandardMinistryCode)VALUES('{0}','{1}');", NewID, cblStandardMinistry.Items[j].Value);
                    //i = Conn.AddNew("dtStandardMinistry", "ProjectsCode, StandardMinistryCode", NewID, cblStandardMinistry.Items[j].Value);
                }
            }

            if (btc.ckIdentityName("ckStrategies"))
            {
                for (int j = 0; j <= cblStrategies.Items.Count - 1; j++)
                {
                    if (cblStrategies.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategies (ProjectsCode, StrategiesCode)VALUES('{0}','{1}');", NewID, cblStrategies.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckWorldClass"))
            {
                //WorldClass
                if (Session["WorldClass"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["WorldClass"];
                    dvWorldClass = dt1.DefaultView;
                    for (int j = 0; j < dvWorldClass.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtWorldClass (ProjectsCode, WorldClassStandardID)VALUES('{0}','{1}');",
                            NewID, dvWorldClass[j]["WorldClassStandardID"].ToString().Trim());
                    }
                }
            }

            if (btc.ckIdentityName("ckRoyalAward"))
            {
                //RoyalAward
                if (Session["RoyalAward"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["RoyalAward"];
                    dvRoyalAward = dt1.DefaultView;
                    for (int j = 0; j < dvRoyalAward.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtRoyalAward (ProjectsCode, RoyalAwardIndicatorID)VALUES('{0}','{1}');",
                            NewID, dvRoyalAward[j]["RoyalAwardIndicatorID"].ToString().Trim());
                    }
                }
            }

            if (btc.ckIdentityName("ckSuffice"))
            {
                //Suffice
                if (Session["Suffice"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["Suffice"];
                    dvSuffice = dt1.DefaultView;
                    for (int j = 0; j < dvSuffice.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtSuffice (ProjectsCode, SufficeIndicatorID)VALUES('{0}','{1}');",
                            NewID, dvSuffice[j]["SufficeIndicatorID"].ToString().Trim());
                    }
                }
            }

            if (btc.ckIdentityName("ckFourthAssessment"))
            {
                //FourthAssessment
                if (Session["FourthAssessment"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["FourthAssessment"];
                    dvFourthAssessment = dt1.DefaultView;
                    for (int j = 0; j < dvFourthAssessment.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtFourthAssessment (ProjectsCode, FourthAssessmentIndicatorID)VALUES('{0}','{1}');",
                            NewID, dvFourthAssessment[j]["FourthAssessmentIndicatorID"].ToString().Trim());
                    }
                }
            }

            //Assessment
            if (Session["Assessment"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Assessment"];
                dvAssessment = dt1.DefaultView;
                for (int j = 0; j < dvAssessment.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ProjectsAssessment (ProjectsCode, RecNum, IndicatorsName, MethodAss, ToolsAss)VALUES('{0}',{1},N'{2}',N'{3}',N'{4}');",
                        NewID, Convert.ToDecimal(dvAssessment[j]["id"]), dvAssessment[j]["IndicatorsName"].ToString().Trim(), dvAssessment[j]["MethodAss"].ToString().Trim(), dvAssessment[j]["ToolsAss"].ToString().Trim());
                }
            }

            if (btc.ckIdentityName("ckTargetMode"))
            {
                for (int j = 0; j <= cblTargetMode.Items.Count - 1; j++)
                {
                    if (cblTargetMode.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtTargetMode (ProjectsCode, TargetMode)VALUES('{0}','{1}');", NewID, cblTargetMode.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPlan"))
            {
                for (int j = 0; j <= cblPlan.Items.Count - 1; j++)
                {
                    if (cblPlan.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPlan (ProjectsCode, PlanMode)VALUES('{0}','{1}');", NewID, cblPlan.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckProjectDes"))
            {
                for (int j = 0; j <= cblProjectDes.Items.Count - 1; j++)
                {
                    if (cblProjectDes.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtProjectDes (ProjectsCode, ProjectDes)VALUES('{0}','{1}');", NewID, cblProjectDes.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckNewStandard"))
            {
                for (int j = 0; j <= cblNewStandard.Items.Count - 1; j++)
                {
                    if (cblNewStandard.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtNewStandard (ProjectsCode, StandardCode)VALUES('{0}','{1}');", NewID, cblNewStandard.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategicObjectives"))
            {
                for (int j = 0; j <= cblStrategicObjectives.Items.Count - 1; j++)
                {
                    if (cblStrategicObjectives.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategicObjectives (ProjectsCode, StrategicObjectivesCode)VALUES('{0}','{1}');", NewID, cblStrategicObjectives.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPT"))
            {
                for (int j = 0; j <= cblStrategySPT.Items.Count - 1; j++)
                {
                    if (cblStrategySPT.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPT (ProjectsCode, StrategySPTCode)VALUES('{0}','{1}');", NewID, cblStrategySPT.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPT", "ProjectsCode, StrategySPTCode", NewID, cblStrategySPT.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPM"))
            {
                for (int j = 0; j <= cblStrategySPM.Items.Count - 1; j++)
                {
                    if (cblStrategySPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPM (ProjectsCode, StrategySPMCode)VALUES('{0}','{1}');", NewID, cblStrategySPM.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPM", "ProjectsCode, StrategySPMCode", NewID, cblStrategySPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardSPM"))
            {
                for (int j = 0; j <= cblStandardSPM.Items.Count - 1; j++)
                {
                    if (cblStandardSPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardSPM (ProjectsCode, StandardSPMCode)VALUES('{0}','{1}');", NewID, cblStandardSPM.Items[j].Value);
                        //i = Conn.AddNew("dtStandardSPM", "ProjectsCode, StandardSPMCode", NewID, cblStandardSPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategic"))
            {
                for (int j = 0; j <= cblStrategic.Items.Count - 1; j++)
                {
                    if (cblStrategic.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategic (ProjectsCode, StrategicCode)VALUES('{0}','{1}');", NewID, cblStrategic.Items[j].Value);
                        //i = Conn.AddNew("dtStrategic", "ProjectsCode, StrategicCode", NewID, cblStrategic.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckIndicatorSMS"))
            {
                for (int j = 0; j <= cblIndicatorSMS.Items.Count - 1; j++)
                {
                    if (cblIndicatorSMS.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtIndicatorSMS (ProjectsCode, IndicatorSMSCode)VALUES('{0}','{1}');", NewID, cblIndicatorSMS.Items[j].Value);
                        //i = Conn.AddNew("dtIndicatorSMS", "ProjectsCode, IndicatorSMSCode", NewID, cblIndicatorSMS.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPopularity"))
            {
                for (int j = 0; j <= cblPopularity.Items.Count - 1; j++)
                {
                    if (cblPopularity.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPopularity (ProjectsCode, PopularityID)VALUES('{0}','{1}');", NewID, cblPopularity.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPrimePolicy"))
            {
                for (int j = 0; j <= cblPrimePolicy.Items.Count - 1; j++)
                {
                    if (cblPrimePolicy.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPrimePolicy (ProjectsCode, PrimePolicyID)VALUES('{0}','{1}');", NewID, cblPrimePolicy.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPoliciesMinister"))
            {
                for (int j = 0; j <= cblPoliciesMinister.Items.Count - 1; j++)
                {
                    if (cblPoliciesMinister.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPoliciesMinister (ProjectsCode, PoliciesMinisterID)VALUES('{0}','{1}');", NewID, cblPoliciesMinister.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPolicyKSN"))
            {
                for (int j = 0; j <= cblPolicyKSN.Items.Count - 1; j++)
                {
                    if (cblPolicyKSN.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPolicyKSN (ProjectsCode, PolicyKSNID)VALUES('{0}','{1}');", NewID, cblPolicyKSN.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckResponseProblems"))
            {
                for (int j = 0; j <= cblResponseProblems.Items.Count - 1; j++)
                {
                    if (cblResponseProblems.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtResponseProblems (ProjectsCode, ResponseProblemsCode)VALUES('{0}','{1}');", NewID, cblResponseProblems.Items[j].Value);
                        //i = Conn.AddNew("dtResponseProblems", "ProjectsCode, ResponseProblemsCode", NewID, cblResponseProblems.Items[j].Value);
                    }
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Session.Remove("WorldClass");
            Session.Remove("RoyalAward");
            Session.Remove("Suffice");
            Session.Remove("FourthAssessment");
            Session.Remove("Assessment");
            if (CkAgain == "N")
            {
                Response.Redirect("Projects.aspx?ckmode=1&Cr=" + i);            
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                SetItem();
                btc.GenSort(txtSort, "Projects", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                GridView2.Visible = true;
                DataBind();
            }
            if (CkAgain == "G")
            {
                Response.Redirect("Activity.aspx?mode=1&id=" + NewID + "&syear=" + ddlYearS.SelectedValue + "&stgid=" + ddlStrategies.SelectedValue);
            }          
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Projects", "Where ProjectsCode = '" + Request["id"] + "' ", "StrategiesCode, MissionCode, GoalsCode, StudyYear, ProjectsName, Purpose, Purpose2, Target, Target2, Period1, ProjectsDetail, Expected, SetBudget, ResponsibleName, ResponsiblePosition, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2, PlanNo, StatuteNo, IdentityName, IdentityName2, Sort, SchoolID, UpdateUser, UpdateDate, StrategicPlanID, CorporateStrategyID, DeptProjectsName", 
                ddlStrategies.SelectedValue, ddlMission.SelectedValue, ddlGoals.SelectedValue, ddlYearS.SelectedValue, txtProjects.Text, txtPurpose.Text, txtPurpose2.Text, txtTarget.Text, txtTarget2.Text, txtPeriod1.Text, txtProjectsDetail.Text, txtExpected.Text, Convert.ToDecimal(txtSetBudget.Text), txtResponsibleName.Text, txtResponsiblePosition.Text, txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, txtPlanNo.Text, txtStatute.Text, txtIdentityName.Text, txtIdentityName2.Text, txtSort.Text, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, ddlStrategicPlan.SelectedValue, ddlCorporateStrategy.SelectedValue, txtDeptProjectsName.Text);

            strbSql.AppendFormat("UPDATE Activity SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);
            strbSql.AppendFormat("UPDATE Indicators2 SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);
            strbSql.AppendFormat("UPDATE Evaluation SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);
            strbSql.AppendFormat("UPDATE QALink SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);

            //Conn.Update("Activity", "Where ProjectsCode = '" + Request["id"] + "' ", "StrategiesCode", ddlStrategies.SelectedValue);
            //Conn.Update("Indicators2", "Where ProjectsCode = '" + Request["id"] + "' ", "StrategiesCode", ddlStrategies.SelectedValue);
            //Conn.Update("Evaluation", "Where ProjectsCode = '" + Request["id"] + "' ", "StrategiesCode", ddlStrategies.SelectedValue);
            //Conn.Update("QALink", "Where ProjectsCode = '" + Request["id"] + "' ", "StrategiesCode", ddlStrategies.SelectedValue);

            strbSql.AppendFormat("Delete dtStrategicPlan Where ProjectsCode = '{0}';", Request["id"]);
            for (int j = 0; j <= cblStrategicPlan.Items.Count - 1; j++)
            {
                if (cblStrategicPlan.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtStrategicPlan (ProjectsCode, StrategicPlanID)VALUES('{0}','{1}');", Request["id"], cblStrategicPlan.Items[j].Value);
                }
            }

            strbSql.AppendFormat("Delete dtCorporateStrategy Where ProjectsCode = '{0}';", Request["id"]);
            for (int j = 0; j <= cblCorporateStrategy.Items.Count - 1; j++)
            {
                if (cblCorporateStrategy.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtCorporateStrategy (ProjectsCode, CorporateStrategyID)VALUES('{0}','{1}');", Request["id"], cblCorporateStrategy.Items[j].Value);
                }
            }

            strbSql.AppendFormat("Delete dtMission Where ProjectsCode = '{0}';", Request["id"]);
            for (int j = 0; j <= cblMission.Items.Count - 1; j++)
            {
                if (cblMission.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtMission (ProjectsCode, MissionCode)VALUES('{0}','{1}');", Request["id"], cblMission.Items[j].Value);
                }
            }

            strbSql.AppendFormat("Delete dtGoals Where ProjectsCode = '{0}';", Request["id"]);
            for (int j = 0; j <= cblGoals.Items.Count - 1; j++)
            {
                if (cblGoals.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtGoals (ProjectsCode, GoalsCode)VALUES('{0}','{1}');", Request["id"], cblGoals.Items[j].Value);
                }
            }

            if (btc.ckIdentityName("ckPolicy"))
            {
                strbSql.AppendFormat("Delete dtPolicy Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtPolicy", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblPolicy.Items.Count - 1; j++)
                {
                    if (cblPolicy.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPolicy (ProjectsCode, PolicyCode)VALUES('{0}','{1}');", Request["id"], cblPolicy.Items[j].Value);
                        //i = Conn.AddNew("dtPolicy", "ProjectsCode, PolicyCode", Request["id"], cblPolicy.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardNation"))
            {
                strbSql.AppendFormat("Delete dtStandardNation Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStandardNation", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStandardNation.Items.Count - 1; j++)
                {
                    if (cblStandardNation.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardNation (ProjectsCode, StandardNationCode)VALUES('{0}','{1}');", Request["id"], cblStandardNation.Items[j].Value);
                        ///i = Conn.AddNew("dtStandardNation", "ProjectsCode, StandardNationCode", Request["id"], cblStandardNation.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardMinistry"))
            {
                strbSql.AppendFormat("Delete dtStandardMinistry Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStandardMinistry", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStandardMinistry.Items.Count - 1; j++)
                {
                    if (cblStandardMinistry.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardMinistry (ProjectsCode, StandardMinistryCode)VALUES('{0}','{1}');", Request["id"], cblStandardMinistry.Items[j].Value);
                        //i = Conn.AddNew("dtStandardMinistry", "ProjectsCode, StandardMinistryCode", Request["id"], cblStandardMinistry.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategies"))
            {
                strbSql.AppendFormat("Delete dtStrategies Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblStrategies.Items.Count - 1; j++)
                {
                    if (cblStrategies.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategies (ProjectsCode, StrategiesCode)VALUES('{0}','{1}');", Request["id"], cblStrategies.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckWorldClass"))
            {
                strbSql.AppendFormat("Delete dtWorldClass Where ProjectsCode = '{0}';", Request["id"]);
                //WorldClass
                if (Session["WorldClass"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["WorldClass"];
                    dvWorldClass = dt1.DefaultView;
                    for (int j = 0; j < dvWorldClass.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtWorldClass (ProjectsCode, WorldClassStandardID)VALUES('{0}','{1}');",
                            Request["id"], dvWorldClass[j]["WorldClassStandardID"].ToString().Trim());
                    }
                }
            }

            if (btc.ckIdentityName("ckRoyalAward"))
            {
                strbSql.AppendFormat("Delete dtRoyalAward Where ProjectsCode = '{0}';", Request["id"]);
                //RoyalAward
                if (Session["RoyalAward"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["RoyalAward"];
                    dvRoyalAward = dt1.DefaultView;
                    for (int j = 0; j < dvRoyalAward.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtRoyalAward (ProjectsCode, RoyalAwardIndicatorID)VALUES('{0}','{1}');",
                            Request["id"], dvRoyalAward[j]["RoyalAwardIndicatorID"].ToString().Trim());
                    }
                }
            }

            if (btc.ckIdentityName("ckSuffice"))
            {
                strbSql.AppendFormat("Delete dtSuffice Where ProjectsCode = '{0}';", Request["id"]);
                //Suffice
                if (Session["Suffice"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["Suffice"];
                    dvSuffice = dt1.DefaultView;
                    for (int j = 0; j < dvSuffice.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtSuffice (ProjectsCode, SufficeIndicatorID)VALUES('{0}','{1}');",
                            Request["id"], dvSuffice[j]["SufficeIndicatorID"].ToString().Trim());
                    }
                }
            }

            if (btc.ckIdentityName("ckFourthAssessment"))
            {
                strbSql.AppendFormat("Delete dtFourthAssessment Where ProjectsCode = '{0}';", Request["id"]);
                //FourthAssessment
                if (Session["FourthAssessment"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["FourthAssessment"];
                    dvFourthAssessment = dt1.DefaultView;
                    for (int j = 0; j < dvFourthAssessment.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtFourthAssessment (ProjectsCode, FourthAssessmentIndicatorID)VALUES('{0}','{1}');",
                            Request["id"], dvFourthAssessment[j]["FourthAssessmentIndicatorID"].ToString().Trim());
                    }
                }
            }

            //Assessment
            if (Session["Assessment"] != null)
            {
                strbSql.AppendFormat("Delete ProjectsAssessment Where ProjectsCode = '{0}';", Request["id"]);
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Assessment"];
                dvAssessment = dt1.DefaultView;
                for (int j = 0; j < dvAssessment.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ProjectsAssessment (ProjectsCode, RecNum, IndicatorsName, MethodAss, ToolsAss)VALUES('{0}',{1},N'{2}',N'{3}',N'{4}');",
                        Request["id"], Convert.ToDecimal(dvAssessment[j]["id"]), dvAssessment[j]["IndicatorsName"].ToString().Trim(), dvAssessment[j]["MethodAss"].ToString().Trim(), dvAssessment[j]["ToolsAss"].ToString().Trim());
                }
            }

            if (btc.ckIdentityName("ckTargetMode"))
            {
                strbSql.AppendFormat("Delete dtTargetMode Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblTargetMode.Items.Count - 1; j++)
                {
                    if (cblTargetMode.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtTargetMode (ProjectsCode, TargetMode)VALUES('{0}','{1}');", Request["id"], cblTargetMode.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPlan"))
            {
                strbSql.AppendFormat("Delete dtPlan Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblPlan.Items.Count - 1; j++)
                {
                    if (cblPlan.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPlan (ProjectsCode, PlanMode)VALUES('{0}','{1}');", Request["id"], cblPlan.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckProjectDes"))
            {
                strbSql.AppendFormat("Delete dtProjectDes Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblProjectDes.Items.Count - 1; j++)
                {
                    if (cblProjectDes.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtProjectDes (ProjectsCode, ProjectDes)VALUES('{0}','{1}');", Request["id"], cblProjectDes.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckNewStandard"))
            {
                strbSql.AppendFormat("Delete dtNewStandard Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblNewStandard.Items.Count - 1; j++)
                {
                    if (cblNewStandard.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtNewStandard (ProjectsCode, StandardCode)VALUES('{0}','{1}');", Request["id"], cblNewStandard.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategicObjectives"))
            {
                strbSql.AppendFormat("Delete dtStrategicObjectives Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblStrategicObjectives.Items.Count - 1; j++)
                {
                    if (cblStrategicObjectives.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategicObjectives (ProjectsCode, StrategicObjectivesCode)VALUES('{0}','{1}');", Request["id"], cblStrategicObjectives.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPT"))
            {
                strbSql.AppendFormat("Delete dtStrategySPT Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStrategySPT", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStrategySPT.Items.Count - 1; j++)
                {
                    if (cblStrategySPT.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPT (ProjectsCode, StrategySPTCode)VALUES('{0}','{1}');", Request["id"], cblStrategySPT.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPT", "ProjectsCode, StrategySPTCode", Request["id"], cblStrategySPT.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPM"))
            {
                strbSql.AppendFormat("Delete dtStrategySPM Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStrategySPM", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStrategySPM.Items.Count - 1; j++)
                {
                    if (cblStrategySPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPM (ProjectsCode, StrategySPMCode)VALUES('{0}','{1}');", Request["id"], cblStrategySPM.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPM", "ProjectsCode, StrategySPMCode", Request["id"], cblStrategySPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardSPM"))
            {
                strbSql.AppendFormat("Delete dtStandardSPM Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStandardSPM", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStandardSPM.Items.Count - 1; j++)
                {
                    if (cblStandardSPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardSPM (ProjectsCode, StandardSPMCode)VALUES('{0}','{1}');", Request["id"], cblStandardSPM.Items[j].Value);
                        //i = Conn.AddNew("dtStandardSPM", "ProjectsCode, StandardSPMCode", Request["id"], cblStandardSPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategic"))
            {
                strbSql.AppendFormat("Delete dtStrategic Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStrategic", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStrategic.Items.Count - 1; j++)
                {
                    if (cblStrategic.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategic (ProjectsCode, StrategicCode)VALUES('{0}','{1}');", Request["id"], cblStrategic.Items[j].Value);
                        //i = Conn.AddNew("dtStrategic", "ProjectsCode, StrategicCode", Request["id"], cblStrategic.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckIndicatorSMS"))
            {
                strbSql.AppendFormat("Delete dtIndicatorSMS Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtIndicatorSMS", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblIndicatorSMS.Items.Count - 1; j++)
                {
                    if (cblIndicatorSMS.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtIndicatorSMS (ProjectsCode, IndicatorSMSCode)VALUES('{0}','{1}');", Request["id"], cblIndicatorSMS.Items[j].Value);
                        //i = Conn.AddNew("dtIndicatorSMS", "ProjectsCode, IndicatorSMSCode", Request["id"], cblIndicatorSMS.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPopularity"))
            {
                strbSql.AppendFormat("Delete dtPopularity Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtPopularity", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblPopularity.Items.Count - 1; j++)
                {
                    if (cblPopularity.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPopularity (ProjectsCode, PopularityID)VALUES('{0}','{1}');", Request["id"], cblPopularity.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", Request["id"], cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPrimePolicy"))
            {
                strbSql.AppendFormat("Delete dtPrimePolicy Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblPrimePolicy.Items.Count - 1; j++)
                {
                    if (cblPrimePolicy.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPrimePolicy (ProjectsCode, PrimePolicyID)VALUES('{0}','{1}');", Request["id"], cblPrimePolicy.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPoliciesMinister"))
            {
                strbSql.AppendFormat("Delete dtPoliciesMinister Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblPoliciesMinister.Items.Count - 1; j++)
                {
                    if (cblPoliciesMinister.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPoliciesMinister (ProjectsCode, PoliciesMinisterID)VALUES('{0}','{1}');", Request["id"], cblPoliciesMinister.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckPolicyKSN"))
            {
                strbSql.AppendFormat("Delete dtPolicyKSN Where ProjectsCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblPolicyKSN.Items.Count - 1; j++)
                {
                    if (cblPolicyKSN.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPolicyKSN (ProjectsCode, PolicyKSNID)VALUES('{0}','{1}');", Request["id"], cblPolicyKSN.Items[j].Value);
                        //i = Conn.AddNew("dtPopularity", "ProjectsCode, PopularityID", NewID, cblPopularity.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckResponseProblems"))
            {
                strbSql.AppendFormat("Delete dtResponseProblems Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtResponseProblems", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblResponseProblems.Items.Count - 1; j++)
                {
                    if (cblResponseProblems.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtResponseProblems (ProjectsCode, ResponseProblemsCode)VALUES('{0}','{1}');", Request["id"], cblResponseProblems.Items[j].Value);
                        //i = Conn.AddNew("dtResponseProblems", "ProjectsCode, ResponseProblemsCode", Request["id"], cblResponseProblems.Items[j].Value);
                    }
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Session.Remove("WorldClass");
            Session.Remove("RoyalAward");
            Session.Remove("Suffice");
            Session.Remove("FourthAssessment");
            Session.Remove("Assessment");
            Response.Redirect("Projects.aspx?ckmode=2&Cr=" + i);  
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        bt_Save("N");
    }
    protected void btSaveAgain_Click(object sender, EventArgs e)
    {
        bt_Save("Y");
    }
    protected void btSaveAndGo_Click(object sender, EventArgs e)
    {
        bt_Save("G");
    }
    private void Delete(string id)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        if (String.IsNullOrEmpty(id)) return;
        Int32 i = 0;
        if (btc.CkUseData(id, "ProjectsCode", "Activity", " And DelFlag = 0 "))
        {
            Response.Redirect("Projects.aspx?ckmode=3&Cr=" + i);  
        }
        else
        {
            i = Conn.Update("Projects", "Where ProjectsCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);

            strbSql.AppendFormat("Delete dtPolicy Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStandardNation Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStandardMinistry Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtNewStandard Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategicObjectives Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategySPT Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategySPM Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStandardSPM Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategic Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtIndicatorSMS Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtPopularity Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtPrimePolicy Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtPoliciesMinister Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtPolicyKSN Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtResponseProblems Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtWorldClass Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtRoyalAward Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtSuffice Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtFourthAssessment Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtTargetMode Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtPlan Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtProjectDes Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategies Where ProjectsCode = '{0}';", Request["id"]);

            result = Conn.Execute(strbSql.ToString());
            Response.Redirect("Projects.aspx?ckmode=3&Cr=" + i);  
        }
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlYearS_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlMission(ddlYearS.SelectedValue);
        getddlGoals(ddlYearS.SelectedValue);
        getcblPolicy(ddlYearS.SelectedValue);
        getcblStandardNation(ddlYearS.SelectedValue);
        getcblStandardMinistry(ddlYearS.SelectedValue);
        getddlStrategies(1, ddlYearS.SelectedValue);
        getcblNewStandard(ddlYearS.SelectedValue);
        getcblStrategicObjectives(ddlYearS.SelectedValue);
        getcblStrategySPT(ddlYearS.SelectedValue);
        getcblStrategySPM(ddlYearS.SelectedValue);
        getcblStandardSPM(ddlYearS.SelectedValue);
        getcblStrategic(ddlYearS.SelectedValue);
        getcblIndicatorSMS(ddlYearS.SelectedValue);
        getcblPopularity(ddlYearS.SelectedValue);
        getcblPrimePolicy(ddlYearS.SelectedValue);
        getcblPoliciesMinister(ddlYearS.SelectedValue);
        getcblPolicyKSN(ddlYearS.SelectedValue);
        getcblStrategies(ddlYearS.SelectedValue);
        getcblResponseProblems(ddlYearS.SelectedValue);
        btc.GenSort(txtSort, "Projects", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("PjStrategies", ddlSearch2.SelectedValue);
        DataBind();
    }
    protected string checkedit(string id, string EmpID, string strName)
    {
        if (EmpID == CurrentUser.ID || btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            if(!string.IsNullOrEmpty(strName))
            {
                return String.Format("<a href=\"javascript:;\" onclick=\"EditItem('{0}');\">{1}</a>", id, strName);
            }
            else
            {
                return String.Format("<a href=\"javascript:;\" onclick=\"EditItem('{0}');\"><img style=\"border: 0; cursor: pointer;\" title=\"แก้ไข\" src=\"../Image/edit.gif\" /></a>", id);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(strName))
            {
                return strName;
            }
            else
            {
                return "";
            }
        }
    }
    protected string checkdelete(string id, string EmpID)
    {
        if (EmpID == CurrentUser.ID || btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            return String.Format("<a href=\"javascript:deleteItem('{0}');\"><img style=\"border: 0; cursor: pointer;\" title=\"ลบ\" src=\"../Image/delete.gif\" /></a>", id);
        }
        else
        {
            return string.Format("");
        }
    }
    public string GetBudget(decimal setBudget, decimal Budget)
    {
        //TotalAmount += Budget;
        if (Budget > setBudget)
        {
            return "<span style=\"color:red;\">" + Budget.ToString("#,##0.00") + "</span>";
        }
        else
        {
            return "<span>" + Budget.ToString("#,##0.00") + "</span>";
        }
    }
    public decimal GetTotalBudget()
    {
        return TotalAmount;
    }
    public decimal GetSetBudget(decimal setBudget)
    {
        //TotalSetBudget += setBudget;
        return setBudget;
    }
    public decimal GetTotalSetBudget()
    {
        return TotalSetBudget;
    }
    private void getddlWorldClassSide(int mode, string StudyYear)
    {
        string strSql = @" Select WorldClassSideID, WorldClassSideName FullName, Sort 
                From WorldClassSide Where DelFlag = 0 And StudyYear = '{0}' Order By MsSideSort, Sort ";
        DataView dv = Conn.Select(string.Format(strSql, StudyYear));
        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlWorldClassSide.DataSource = dv;
                ddlWorldClassSide.DataTextField = "FullName";
                ddlWorldClassSide.DataValueField = "WorldClassSideID";
                ddlWorldClassSide.DataBind();
                ddlWorldClassSide.Enabled = true;
                ddlWorldClassSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlWorldClassSide.SelectedIndex = 0;
            }
            else
            {
                ddlWorldClassSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlWorldClassSide.SelectedIndex = 0;
                ddlWorldClassSide.Enabled = false;
            }
        }
    }
    private void getddlWorldClassStandard(int mode, string StudyYear, string WorldClassSideID)
    {
        DataView dv = null;
        string strSql = "";
        if (Session["WorldClass"] == null)
        {
            strSql = @" Select b.WorldClassStandardID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.WorldClassStandardName FullName, b.Sort 
                From WorldClassSide a, WorldClassStandard b 
                Where a.WorldClassSideID = b.WorldClassSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.WorldClassSideID = '{1}' 
                Order By a.MsSideSort, a.Sort, b.Sort ";
            dv = Conn.Select(string.Format(strSql, StudyYear, WorldClassSideID));
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["WorldClass"];
            DataView dvCk = dt1.DefaultView;

            string strCkID = "";
            if (dvCk.Count != 0)
            {
                for (int i = 0; i < dvCk.Count; i++)
                {
                    strCkID += "'" + dvCk[i]["WorldClassStandardID"].ToString() + "',";
                }
            }

            strSql = @" Select b.WorldClassStandardID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.WorldClassStandardName FullName, b.Sort 
                From WorldClassSide a, WorldClassStandard b 
                Where a.WorldClassSideID = b.WorldClassSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.WorldClassSideID = '{1}' 
                And b.WorldClassStandardID Not In ({2})
                Order By a.MsSideSort, a.Sort, b.Sort ";
            dv = Conn.Select(string.Format(strSql, StudyYear, WorldClassSideID, strCkID.Substring(0, strCkID.Length - 1)));
        }
        
        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlWorldClassStandard.DataSource = dv;
                ddlWorldClassStandard.DataTextField = "FullName";
                ddlWorldClassStandard.DataValueField = "WorldClassStandardID";
                ddlWorldClassStandard.DataBind();
                ddlWorldClassStandard.Enabled = true;
                ddlWorldClassStandard.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlWorldClassStandard.SelectedIndex = 0;
            }
            else
            {
                ddlWorldClassStandard.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlWorldClassStandard.SelectedIndex = 0;
                ddlWorldClassStandard.Enabled = false;
            }
        }
    }
    protected void ddlWorldClassSide_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlWorldClassStandard(1, ddlYearS.SelectedValue, ddlWorldClassSide.SelectedValue);
    }
    protected void ddlWorldClassStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridWorldClass();
    }
    private void BindGridWorldClass()
    {
        if (Session["WorldClass"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("WorldClassStandardID");
            dt1.Columns.Add("WorldClassSideName");
            dt1.Columns.Add("WorldClassStandardName");

            DataRow dr;
            dr = dt1.NewRow();
            dr["WorldClassStandardID"] = ddlWorldClassStandard.SelectedValue;
            dr["WorldClassSideName"] = ddlWorldClassSide.SelectedItem.Text;
            dr["WorldClassStandardName"] = ddlWorldClassStandard.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvWorldClass = dt1.DefaultView;
            Session["WorldClass"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["WorldClass"];

                DataRow dr;
                dr = dt1.NewRow();
                dr["WorldClassStandardID"] = ddlWorldClassStandard.SelectedValue;
                dr["WorldClassSideName"] = ddlWorldClassSide.SelectedItem.Text;
                dr["WorldClassStandardName"] = ddlWorldClassStandard.SelectedItem.Text;
                dt1.Rows.Add(dr);

                dvWorldClass = dt1.DefaultView;
                Session["WorldClass"] = dt1;
        }
        //ลบรายการที่ถูกเลือกออกจาก Dropdownlist
        ddlWorldClassStandard.Items.Remove(ddlWorldClassStandard.Items.FindByValue(ddlWorldClassStandard.SelectedValue));
        dvWorldClass.Sort = "WorldClassStandardName";
        gridWorldClass.DataSource = dvWorldClass;
        gridWorldClass.CheckListDataField = "WorldClassStandardID";
        gridWorldClass.DataBind();
        if (dvWorldClass.Count > 0)
        {
            btDelWorldClass.Visible = true;
        }
    }
    protected void btDelWorldClass_Click(object sender, EventArgs e)
    {
        if (gridWorldClass.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["WorldClass"];
        DataRow[] dra = dt1.Select("WorldClassStandardID in ('" + string.Join("','", gridWorldClass.SelectedItems) + "')");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvWorldClass = dt1.DefaultView;
        if (dvWorldClass.Count != 0)
        {
            Session["WorldClass"] = dt1;
        }
        else
        {
            Session.Remove("WorldClass");
        }
        gridWorldClass.DataSource = dvWorldClass;
        gridWorldClass.DataBind();
        if (dvWorldClass.Count == 0)
        {
            btDelWorldClass.Visible = false;
        }
    }
    private void getddlRoyalAwardSide(int mode, string StudyYear)
    {
        string strSql = @" Select RoyalAwardSideID, Cast(Sort as nvarchar) + ' - ' + RoyalAwardSideName FullName, Sort 
                From RoyalAwardSide Where DelFlag = 0 And StudyYear = '{0}' Order By Sort ";
        DataView dv = Conn.Select(string.Format(strSql, StudyYear));
        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlRoyalAwardSide.DataSource = dv;
                ddlRoyalAwardSide.DataTextField = "FullName";
                ddlRoyalAwardSide.DataValueField = "RoyalAwardSideID";
                ddlRoyalAwardSide.DataBind();
                ddlRoyalAwardSide.Enabled = true;
                ddlRoyalAwardSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlRoyalAwardSide.SelectedIndex = 0;
            }
            else
            {
                ddlRoyalAwardSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlRoyalAwardSide.SelectedIndex = 0;
                ddlRoyalAwardSide.Enabled = false;
            }
        }
    }
    private void getddlRoyalAwardIndicator(int mode, string StudyYear, string RoyalAwardSideID)
    {
        DataView dv = null;
        string strSql = "";
        if (Session["RoyalAward"] == null)
        {
            strSql = @" Select b.RoyalAwardIndicatorID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.RoyalAwardIndicatorName FullName, b.Sort 
                From RoyalAwardSide a, RoyalAwardIndicator b 
                Where a.RoyalAwardSideID = b.RoyalAwardSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.RoyalAwardSideID = '{1}' 
                Order By a.Sort, b.Sort ";
            dv = Conn.Select(string.Format(strSql, StudyYear, RoyalAwardSideID));
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["RoyalAward"];
            DataView dvCk = dt1.DefaultView;

            string strCkID = "";
            if (dvCk.Count != 0)
            {
                for (int i = 0; i < dvCk.Count; i++)
                {
                    strCkID += "'" + dvCk[i]["RoyalAwardIndicatorID"].ToString() + "',";
                }
            }

            strSql = @" Select b.RoyalAwardIndicatorID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.RoyalAwardIndicatorName FullName, b.Sort 
                From RoyalAwardSide a, RoyalAwardIndicator b 
                Where a.RoyalAwardSideID = b.RoyalAwardSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.RoyalAwardSideID = '{1}' 
                And b.RoyalAwardIndicatorID Not In ({2})
                Order By a.Sort, b.Sort ";
            dv = Conn.Select(string.Format(strSql, StudyYear, RoyalAwardSideID, strCkID.Substring(0, strCkID.Length - 1)));
        }

        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlRoyalAwardIndicator.DataSource = dv;
                ddlRoyalAwardIndicator.DataTextField = "FullName";
                ddlRoyalAwardIndicator.DataValueField = "RoyalAwardIndicatorID";
                ddlRoyalAwardIndicator.DataBind();
                ddlRoyalAwardIndicator.Enabled = true;
                ddlRoyalAwardIndicator.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlRoyalAwardIndicator.SelectedIndex = 0;
            }
            else
            {
                ddlRoyalAwardIndicator.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlRoyalAwardIndicator.SelectedIndex = 0;
                ddlRoyalAwardIndicator.Enabled = false;
            }
        }
    }
    protected void ddlRoyalAwardSide_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlRoyalAwardIndicator(1, ddlYearS.SelectedValue, ddlRoyalAwardSide.SelectedValue);
    }
    protected void ddlRoyalAwardIndicator_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridRoyalAward();
    }
    private void BindGridRoyalAward()
    {
        if (Session["RoyalAward"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("RoyalAwardIndicatorID");
            dt1.Columns.Add("RoyalAwardSideName");
            dt1.Columns.Add("RoyalAwardIndicatorName");

            DataRow dr;
            dr = dt1.NewRow();
            dr["RoyalAwardIndicatorID"] = ddlRoyalAwardIndicator.SelectedValue;
            dr["RoyalAwardSideName"] = ddlRoyalAwardSide.SelectedItem.Text;
            dr["RoyalAwardIndicatorName"] = ddlRoyalAwardIndicator.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvRoyalAward = dt1.DefaultView;
            Session["RoyalAward"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["RoyalAward"];

            DataRow dr;
            dr = dt1.NewRow();
            dr["RoyalAwardIndicatorID"] = ddlRoyalAwardIndicator.SelectedValue;
            dr["RoyalAwardSideName"] = ddlRoyalAwardSide.SelectedItem.Text;
            dr["RoyalAwardIndicatorName"] = ddlRoyalAwardIndicator.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvRoyalAward = dt1.DefaultView;
            Session["RoyalAward"] = dt1;
        }
        //ลบรายการที่ถูกเลือกออกจาก Dropdownlist
        ddlRoyalAwardIndicator.Items.Remove(ddlRoyalAwardIndicator.Items.FindByValue(ddlRoyalAwardIndicator.SelectedValue));
        dvRoyalAward.Sort = "RoyalAwardIndicatorName";
        gridRoyalAward.DataSource = dvRoyalAward;
        gridRoyalAward.CheckListDataField = "RoyalAwardIndicatorID";
        gridRoyalAward.DataBind();
        if (dvRoyalAward.Count > 0)
        {
            btDelRoyalAward.Visible = true;
        }
    }
    protected void btDelRoyalAward_Click(object sender, EventArgs e)
    {
        if (gridRoyalAward.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["RoyalAward"];
        DataRow[] dra = dt1.Select("RoyalAwardIndicatorID in ('" + string.Join("','", gridRoyalAward.SelectedItems) + "')");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvRoyalAward = dt1.DefaultView;
        if (dvRoyalAward.Count != 0)
        {
            Session["RoyalAward"] = dt1;
        }
        else
        {
            Session.Remove("RoyalAward");
        }
        gridRoyalAward.DataSource = dvRoyalAward;
        gridRoyalAward.DataBind();
        if (dvRoyalAward.Count == 0)
        {
            btDelRoyalAward.Visible = false;
        }
    }
    protected void ddlStrategies_SelectedIndexChanged(object sender, EventArgs e)
    {
        ckStrategiesError(ddlStrategies.SelectedValue);
    }
    protected void spnResponsibleName_TextChanged(object sender, EventArgs e)
    {
        UpdatelblResponsibleName();
    }
    private void UpdatelblResponsibleName()
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "spnResponsibleName, spnProjectsApprovalName, spnProjectsApprovalName2", 
            spnResponsibleName.Text, spnProjectsApprovalName.Text, spnProjectsApprovalName2.Text);
    }
    private void ModeCreate(Boolean enabled)
    {    
        divSearchCreateUser.Visible = enabled;
        txtSearchCreateUser.Text = "";
        divbtnEditCreate.Visible = enabled;
        divddlCreateUser.Visible = enabled;
        ddlCreateUser.Items.Clear();
        ddlCreateUser.Items.Insert(0, new ListItem("ค้นหารายการก่อน", ""));
        ddlCreateUser.Enabled = false;
    }
    protected void lbtEditCreate_Click(object sender, EventArgs e)
    {
        ModeCreate(true);
    }
    private void getddlEditCreate()
    {
        string strSql = @" Select E.EmpID, E.EmpName + ' ('+ D.DeptName + ')' As EmpName from Employee E, Department D 
                Where E.DelFlag = 0 And E.DeptCode = D.DeptCode And EmpName Like N'%" + txtSearchCreateUser.Text + "%'";
            DataView dv = Conn.Select(strSql);
            if (dv.Count != 0)
            {
                ddlCreateUser.DataSource = dv;
                ddlCreateUser.DataTextField = "EmpName";
                ddlCreateUser.DataValueField = "EmpID";
                ddlCreateUser.DataBind();
                ddlCreateUser.Enabled = true;
            }
            else
            {
                ddlCreateUser.Items.Clear();
                ddlCreateUser.Items.Insert(0, new ListItem("ไม่พบรายการ", ""));
                ddlCreateUser.Enabled = false;
            }
        //}
    }
    protected void btnCancelCreate_Click(object sender, EventArgs e)
    {
        ModeCreate(false);
    }
    protected void btnEditCreate_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlCreateUser.SelectedValue))
        {
            Conn.Update("Projects", "Where ProjectsCode = '" + Request.QueryString["id"] + "'", "CreateUser", ddlCreateUser.SelectedValue);
            ModeCreate(false);
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Projects", "ProjectsCode", Request.QueryString["id"]);
        }
    }
    protected void btSearchCreateUser_Click(object sender, EventArgs e)
    {
        getddlEditCreate();
    }
    private void getddlSufficeSide(int mode, string StudyYear)
    {
        string strSql = @" Select SufficeSideID, Cast(Sort As nVarChar) + '. ' + SufficeSideName FullName, Sort 
                From SufficeSide Where DelFlag = 0 And StudyYear = '{0}' Order By Sort ";
        DataView dv = Conn.Select(string.Format(strSql, StudyYear));
        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlSufficeSide.DataSource = dv;
                ddlSufficeSide.DataTextField = "FullName";
                ddlSufficeSide.DataValueField = "SufficeSideID";
                ddlSufficeSide.DataBind();
                ddlSufficeSide.Enabled = true;
                ddlSufficeSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlSufficeSide.SelectedIndex = 0;
            }
            else
            {
                ddlSufficeSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlSufficeSide.SelectedIndex = 0;
                ddlSufficeSide.Enabled = false;
            }
        }
    }
    private void getddlSufficeStandard(int mode, string SufficeSideID)
    {
        DataView dv = null;
        string strSql = "";
        strSql = @" Select b.SufficeStandardID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.SufficeStandardName FullName, b.Sort 
                From SufficeSide a, SufficeStandard b 
                Where a.SufficeSideID = b.SufficeSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.SufficeSideID = '{0}' 
                Order By a.Sort, b.Sort ";
        dv = Conn.Select(string.Format(strSql, SufficeSideID));

        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlSufficeStandard.DataSource = dv;
                ddlSufficeStandard.DataTextField = "FullName";
                ddlSufficeStandard.DataValueField = "SufficeStandardID";
                ddlSufficeStandard.DataBind();
                ddlSufficeStandard.Enabled = true;
                ddlSufficeStandard.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlSufficeStandard.SelectedIndex = 0;
            }
            else
            {
                ddlSufficeStandard.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlSufficeStandard.SelectedIndex = 0;
                ddlSufficeStandard.Enabled = false;
            }
        }
    }
    private void getddlSufficeIndicator(int mode, string SufficeStandardID)
    {
        DataView dv = null;
        string strSql = "";
        if (Session["Suffice"] == null)
        {
            strSql = @" Select c.SufficeIndicatorID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + '.' + Cast(c.Sort As nVarChar) + ' - ' + c.SufficeIndicatorName FullName, c.Sort 
                From SufficeSide a, SufficeStandard b, SufficeIndicator c
                Where a.SufficeSideID = b.SufficeSideID And b.SufficeStandardID = c.SufficeStandardID And a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0
                And b.SufficeStandardID = '{0}' 
                Order By a.Sort, b.Sort, c.Sort ";
            dv = Conn.Select(string.Format(strSql, SufficeStandardID));
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Suffice"];
            DataView dvCk = dt1.DefaultView;

            string strCkID = "";
            if (dvCk.Count != 0)
            {
                for (int i = 0; i < dvCk.Count; i++)
                {
                    strCkID += "'" + dvCk[i]["SufficeIndicatorID"].ToString() + "',";
                }
            }

            strSql = @" Select c.SufficeIndicatorID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + '.' + Cast(c.Sort As nVarChar) + ' - ' + c.SufficeIndicatorName FullName, c.Sort 
                From SufficeSide a, SufficeStandard b, SufficeIndicator c 
                Where a.SufficeSideID = b.SufficeSideID And b.SufficeStandardID = c.SufficeStandardID And a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 
                And b.SufficeStandardID = '{0}' 
                And c.SufficeIndicatorID Not In ({1})
                Order By a.Sort, b.Sort, c.Sort ";
            dv = Conn.Select(string.Format(strSql, SufficeStandardID, strCkID.Substring(0, strCkID.Length - 1)));
        }

        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlSufficeIndicator.DataSource = dv;
                ddlSufficeIndicator.DataTextField = "FullName";
                ddlSufficeIndicator.DataValueField = "SufficeIndicatorID";
                ddlSufficeIndicator.DataBind();
                ddlSufficeIndicator.Enabled = true;
                ddlSufficeIndicator.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlSufficeIndicator.SelectedIndex = 0;
            }
            else
            {
                ddlSufficeIndicator.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlSufficeIndicator.SelectedIndex = 0;
                ddlSufficeIndicator.Enabled = false;
            }
        }
    }
    protected void ddlSufficeSide_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlSufficeStandard(1, ddlSufficeSide.SelectedValue);
        getddlSufficeIndicator(1, ddlSufficeStandard.SelectedValue);
    }
    protected void ddlSufficeStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlSufficeIndicator(1, ddlSufficeStandard.SelectedValue);
    }
    protected void ddlSufficeIndicator_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridSuffice();
    }
    private void BindGridSuffice()
    {
        if (Session["Suffice"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("SufficeIndicatorID");
            dt1.Columns.Add("SufficeSideName");
            dt1.Columns.Add("SufficeStandardName");
            dt1.Columns.Add("SufficeIndicatorName");

            DataRow dr;
            dr = dt1.NewRow();
            dr["SufficeIndicatorID"] = ddlSufficeIndicator.SelectedValue;
            dr["SufficeSideName"] = ddlSufficeSide.SelectedItem.Text;
            dr["SufficeStandardName"] = ddlSufficeStandard.SelectedItem.Text;
            dr["SufficeIndicatorName"] = ddlSufficeIndicator.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvSuffice = dt1.DefaultView;
            Session["Suffice"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Suffice"];

            DataRow dr;
            dr = dt1.NewRow();
            dr["SufficeIndicatorID"] = ddlSufficeIndicator.SelectedValue;
            dr["SufficeSideName"] = ddlSufficeSide.SelectedItem.Text;
            dr["SufficeStandardName"] = ddlSufficeStandard.SelectedItem.Text;
            dr["SufficeIndicatorName"] = ddlSufficeIndicator.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvSuffice = dt1.DefaultView;
            Session["Suffice"] = dt1;
        }
        //ลบรายการที่ถูกเลือกออกจาก Dropdownlist
        ddlSufficeIndicator.Items.Remove(ddlSufficeIndicator.Items.FindByValue(ddlSufficeIndicator.SelectedValue));
        dvSuffice.Sort = "SufficeIndicatorName";
        gridSuffice.DataSource = dvSuffice;
        gridSuffice.CheckListDataField = "SufficeIndicatorID";
        gridSuffice.DataBind();
        if (dvSuffice.Count > 0)
        {
            btDelSuffice.Visible = true;
        }
    }
    protected void btDelSuffice_Click(object sender, EventArgs e)
    {
        if (gridSuffice.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Suffice"];
        DataRow[] dra = dt1.Select("SufficeIndicatorID in ('" + string.Join("','", gridSuffice.SelectedItems) + "')");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvSuffice = dt1.DefaultView;
        if (dvSuffice.Count != 0)
        {
            Session["Suffice"] = dt1;
        }
        else
        {
            Session.Remove("Suffice");
        }
        gridSuffice.DataSource = dvSuffice;
        gridSuffice.DataBind();
        if (dvSuffice.Count == 0)
        {
            btDelSuffice.Visible = false;
        }
    }
    private void getddlFourthAssessmentSide(int mode, string StudyYear)
    {
        string strSql = @" Select FourthAssessmentSideID, Cast(Sort as nvarchar) + ' - ' + FourthAssessmentSideName FullName, Sort 
                From FourthAssessmentSide Where DelFlag = 0 And StudyYear = '{0}' Order By Sort ";
        DataView dv = Conn.Select(string.Format(strSql, StudyYear));
        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlFourthAssessmentSide.DataSource = dv;
                ddlFourthAssessmentSide.DataTextField = "FullName";
                ddlFourthAssessmentSide.DataValueField = "FourthAssessmentSideID";
                ddlFourthAssessmentSide.DataBind();
                ddlFourthAssessmentSide.Enabled = true;
                ddlFourthAssessmentSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlFourthAssessmentSide.SelectedIndex = 0;
            }
            else
            {
                ddlFourthAssessmentSide.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlFourthAssessmentSide.SelectedIndex = 0;
                ddlFourthAssessmentSide.Enabled = false;
            }
        }
    }
    private void getddlFourthAssessmentIndicator(int mode, string FourthAssessmentSideID)
    {
        DataView dv = null;
        string strSql = "";
        if (Session["FourthAssessment"] == null)
        {
            strSql = @" Select b.FourthAssessmentIndicatorID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.FourthAssessmentIndicatorName FullName, b.Sort 
                From FourthAssessmentSide a, FourthAssessmentIndicator b 
                Where a.FourthAssessmentSideID = b.FourthAssessmentSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.FourthAssessmentSideID = '{0}' 
                Order By a.Sort, b.Sort ";
            dv = Conn.Select(string.Format(strSql, FourthAssessmentSideID));
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["FourthAssessment"];
            DataView dvCk = dt1.DefaultView;

            string strCkID = "";
            if (dvCk.Count != 0)
            {
                for (int i = 0; i < dvCk.Count; i++)
                {
                    strCkID += "'" + dvCk[i]["FourthAssessmentIndicatorID"].ToString() + "',";
                }
            }

            strSql = @" Select b.FourthAssessmentIndicatorID, Cast(a.Sort as nvarchar) + '.' + Cast(b.Sort as nvarchar) + ' - ' + b.FourthAssessmentIndicatorName FullName, b.Sort 
                From FourthAssessmentSide a, FourthAssessmentIndicator b 
                Where a.FourthAssessmentSideID = b.FourthAssessmentSideID And a.DelFlag = 0 And b.DelFlag = 0 And b.FourthAssessmentSideID = '{0}' 
                And b.FourthAssessmentIndicatorID Not In ({1})
                Order By a.Sort, b.Sort ";
            dv = Conn.Select(string.Format(strSql, FourthAssessmentSideID, strCkID.Substring(0, strCkID.Length - 1)));
        }

        if (mode == 1)
        {
            if (dv.Count != 0)
            {
                ddlFourthAssessmentIndicator.DataSource = dv;
                ddlFourthAssessmentIndicator.DataTextField = "FullName";
                ddlFourthAssessmentIndicator.DataValueField = "FourthAssessmentIndicatorID";
                ddlFourthAssessmentIndicator.DataBind();
                ddlFourthAssessmentIndicator.Enabled = true;
                ddlFourthAssessmentIndicator.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlFourthAssessmentIndicator.SelectedIndex = 0;
            }
            else
            {
                ddlFourthAssessmentIndicator.Items.Insert(0, new ListItem("-เลือก-", ""));
                ddlFourthAssessmentIndicator.SelectedIndex = 0;
                ddlFourthAssessmentIndicator.Enabled = false;
            }
        }
    }
    protected void ddlFourthAssessmentSide_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlFourthAssessmentIndicator(1, ddlFourthAssessmentSide.SelectedValue);
    }
    protected void ddlFourthAssessmentIndicator_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridFourthAssessment();
    }
    private void BindGridFourthAssessment()
    {
        if (Session["FourthAssessment"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("FourthAssessmentIndicatorID");
            dt1.Columns.Add("FourthAssessmentSideName");
            dt1.Columns.Add("FourthAssessmentIndicatorName");

            DataRow dr;
            dr = dt1.NewRow();
            dr["FourthAssessmentIndicatorID"] = ddlFourthAssessmentIndicator.SelectedValue;
            dr["FourthAssessmentSideName"] = ddlFourthAssessmentSide.SelectedItem.Text;
            dr["FourthAssessmentIndicatorName"] = ddlFourthAssessmentIndicator.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvFourthAssessment = dt1.DefaultView;
            Session["FourthAssessment"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["FourthAssessment"];

            DataRow dr;
            dr = dt1.NewRow();
            dr["FourthAssessmentIndicatorID"] = ddlFourthAssessmentIndicator.SelectedValue;
            dr["FourthAssessmentSideName"] = ddlFourthAssessmentSide.SelectedItem.Text;
            dr["FourthAssessmentIndicatorName"] = ddlFourthAssessmentIndicator.SelectedItem.Text;
            dt1.Rows.Add(dr);

            dvFourthAssessment = dt1.DefaultView;
            Session["FourthAssessment"] = dt1;
        }
        //ลบรายการที่ถูกเลือกออกจาก Dropdownlist
        ddlFourthAssessmentIndicator.Items.Remove(ddlFourthAssessmentIndicator.Items.FindByValue(ddlFourthAssessmentIndicator.SelectedValue));
        dvFourthAssessment.Sort = "FourthAssessmentIndicatorName";
        gridFourthAssessment.DataSource = dvFourthAssessment;
        gridFourthAssessment.CheckListDataField = "FourthAssessmentIndicatorID";
        gridFourthAssessment.DataBind();
        if (dvFourthAssessment.Count > 0)
        {
            btDelFourthAssessment.Visible = true;
        }
    }
    protected void btDelFourthAssessment_Click(object sender, EventArgs e)
    {
        if (gridFourthAssessment.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["FourthAssessment"];
        DataRow[] dra = dt1.Select("FourthAssessmentIndicatorID in ('" + string.Join("','", gridFourthAssessment.SelectedItems) + "')");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvFourthAssessment = dt1.DefaultView;
        if (dvFourthAssessment.Count != 0)
        {
            Session["FourthAssessment"] = dt1;
        }
        else
        {
            Session.Remove("FourthAssessment");
        }
        gridFourthAssessment.DataSource = dvFourthAssessment;
        gridFourthAssessment.DataBind();
        if (dvFourthAssessment.Count == 0)
        {
            btDelFourthAssessment.Visible = false;
        }
    }
    protected void btaddAssessment_Click(object sender, EventArgs e)
    {
        if (Session["Assessment"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("IndicatorsName");
            dt1.Columns.Add("MethodAss");
            dt1.Columns.Add("ToolsAss");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["IndicatorsName"] = txtIndicatorsName.Text;
            dr["MethodAss"] = txtMethodAss.Text;
            dr["ToolsAss"] = txtToolsAss.Text;
            dt1.Rows.Add(dr);

            dvAssessment = dt1.DefaultView;
            Session["Assessment"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Assessment"];

            if (txtid2.Text == "")
            {
                DataView ckdv = ((DataTable)Session["Assessment"]).DefaultView;
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["IndicatorsName"] = txtIndicatorsName.Text;
                dr["MethodAss"] = txtMethodAss.Text;
                dr["ToolsAss"] = txtToolsAss.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                DataView ckdv = ((DataTable)Session["Assessment"]).DefaultView;
                Int32 i = Convert.ToInt32(txtid2.Text);
                dt1.Rows[i]["IndicatorsName"] = txtIndicatorsName.Text;
                dt1.Rows[i]["MethodAss"] = txtMethodAss.Text;
                dt1.Rows[i]["ToolsAss"] = txtToolsAss.Text;
            }

            dvAssessment = dt1.DefaultView;
            Session["Assessment"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearAssessment();
        GridViewAssessment.DataSource = dvAssessment;
        GridViewAssessment.CheckListDataField = "id";
        GridViewAssessment.DataBind();
        if (dvAssessment.Count > 0)
        {
            btDelAssessment.Visible = true;
        }
    }
    private void ClearAssessment()
    {
        txtid2.Text = "";
        txtIndicatorsName.Text = "";
        txtMethodAss.Text = "";
        txtToolsAss.Text = "";
    }
    protected void btDelAssessment_Click(object sender, EventArgs e)
    {
        if (GridViewAssessment.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Assessment"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewAssessment.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvAssessment = dt1.DefaultView;
        if (dvAssessment.Count != 0)
        {
            Session["Assessment"] = dt1;
        }
        else
        {
            Session.Remove("Assessment");
        }        
        GridViewAssessment.DataSource = dvAssessment;
        GridViewAssessment.DataBind();
        if (dvAssessment.Count == 0)
        {
            btDelAssessment.Visible = false;
        }
    }

    protected void spnProjectsApprovalName_TextChanged(object sender, EventArgs e)
    {
        UpdatelblResponsibleName();
    }
    protected void spnProjectsApprovalName2_TextChanged(object sender, EventArgs e)
    {
        UpdatelblResponsibleName();
    }
}
