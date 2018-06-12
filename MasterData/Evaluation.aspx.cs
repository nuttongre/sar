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

public partial class Evaluation : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            if (!string.IsNullOrEmpty(Request["Cr"]))
            {
                btc.Msg_Head(Img1, MsgHead, true, Request["ckmode"], Convert.ToInt32(Request["Cr"]));
            }

            if (btc.ckIdentityName("ckStandardNew"))
            {
                lblTitle1.Text = "เชื่อมโยงประเด็นพิจารณา";
                lblTitle2.Text = "ตัวชี้วัดที่เชื่อมโยงกับประเด็นพิจารณา";
                lblSideSearch.Text = "มาตรฐาน";
                lblStandardSearch.Text = "ประเด็นการพิจารณา";
                lblIndicatorSearch.Text = "ประเด็นย่อยการพิจารณา";
                lblSide.Text = "มาตรฐาน";
                lblStandard.Text = "ประเด็นการพิจารณา";
                lblIndicator.Text = "ประเด็นย่อยการพิจารณา";
                GridView1.Columns[3].HeaderText = "มาตรฐาน";
                GridView1.Columns[4].HeaderText = "ประเด็นฯ";
                GridView1.Columns[5].HeaderText = "ประเด็นฯย่อย";

                GridView2.Columns[1].HeaderText = "มาตรฐาน";
                GridView2.Columns[2].HeaderText = "ประเด็นฯ";
                GridView2.Columns[3].HeaderText = "ประเด็นฯย่อย";

                btAdd.Text = "       เชื่อมโยงประเด็นพิจารณาใหม่";
                btAdd.ToolTip = "เชื่อมโยงประเด็นพิจารณาใหม่";
                btSaveAgain.Text = "       บันทึกและเชื่อมโยงประเด็นพิจารณาใหม่";
                btSaveAgain.ToolTip = "บันทึกการเชื่อมโยงนี้และเชื่อมโยงประเด็นพิจารณาใหม่";
            }
            else
            {
                lblTitle1.Text = "เชื่อมโยงตัวบ่งชี้";
                lblTitle2.Text = "ตัวชี้วัดที่เชื่อมโยงกับตัวบ่งชี้";
                lblSideSearch.Text = "ด้าน";
                lblStandardSearch.Text = "มาตรฐาน";
                lblIndicatorSearch.Text = "ตัวบ่งชี้";
                lblSide.Text = "ด้าน";
                lblStandard.Text = "มาตรฐาน";
                lblIndicator.Text = "ตัวบ่งชี้";
                GridView1.Columns[3].HeaderText = "ด้าน";
                GridView1.Columns[4].HeaderText = "มาตรฐาน";
                GridView1.Columns[5].HeaderText = "ตัวบ่งชี้";

                GridView2.Columns[1].HeaderText = "ด้าน";
                GridView2.Columns[2].HeaderText = "มาตรฐาน";
                GridView2.Columns[3].HeaderText = "ตัวบ่งชี้";

                btAdd.Text = "       เชื่อมโยงตัวบ่งชี้ใหม่";
                btAdd.ToolTip = "เชื่อมโยงตัวบ่งชี้ใหม่";
                btSaveAgain.Text = "       บันทึกและเชื่อมโยงตัวบ่งชี้ใหม่";
                btSaveAgain.ToolTip = "บันทึกการเชื่อมโยงนี้และเชื่อมโยงตัวบ่งชี้ใหม่";
            }  

            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);

            getddlYear(0);
            getddlSide(0);
            getddlStandard(0, ddlSearchSide.SelectedValue);
            getddlIndicators(0, ddlSearchStandard.SelectedValue);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
            getddlActivity(0, ddlSearch.SelectedValue);
            getddlIndicators2(0, ddlSearch1.SelectedValue);
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("EvaDept").ToString(), Cookie.GetValue2("EvaEmp").ToString());
           
            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        string gotoid = Request.QueryString["id"];
                        if (!string.IsNullOrEmpty(gotoid))
                        {
                            GetDataGo(gotoid);  //ดึงข้อมูลมาทำต่อ
                            getddlSide(1);
                        }
                        else
                        {
                            getddlSide(1);
                            getddlStrategies(1, ddlYearB.SelectedValue);
                            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
                            getddlActivity(1, ddlProjects.SelectedValue);
                            getActivityDetail(ddlActivity.SelectedValue);
                            getddlIndicators2(1, ddlActivity.SelectedValue);
                            getWeight(ddlIndicators2.SelectedValue);
                        }
                        getddlStandard(1, ddlSide.SelectedValue);
                        ddlStandard.SelectedIndex = 0;
                        getddlIndicators(1, ddlStandard.SelectedValue);
                        if (btc.ckIdentityName("ckQA"))
                        {
                            divQA.Visible = true;
                            getddlQACategory(1, ddlYearB.SelectedValue);
                            getddlQACategorySub(1, ddlYearB.SelectedValue);
                        }
                        else
                        {
                            divQA.Visible = false;
                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        btc.btEnable(btSaveAgain, false);
                        GetData(Request["id"]);
                        if (btc.ckIdentityName("ckQA"))
                        {
                            divQA.Visible = true;
                            getddlQACategory(1, ddlYearB.SelectedValue);
                            getddlQACategorySub(1, ddlYearB.SelectedValue);
                        }
                        else
                        {
                            divQA.Visible = false;
                        }
                        break;
                    case "3":
                        MultiView1.ActiveViewIndex = 0;
                        Delete(Request["id"]);
                        break;
                    case "4":
                        MultiView1.ActiveViewIndex = 0;
                        DelItemQA(Request["id"]);
                        break;
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript2", "function loadDetail(){" + Page.ClientScript.GetPostBackEventReference(btAc, null, false) + ";}", true);
            }
            else
            {
                DataBind();
            }
        }
        ddlStandard.Attributes.Add("onchange", "Cktxt(0); CktxtOld();");
        ddlIndicators.Attributes.Add("onchange", "Cktxt(0); CktxtOld();");
        ddlStrategies.Attributes.Add("onchange", "Cktxt(0);");
        ddlProjects.Attributes.Add("onchange", "Cktxt(0);");
        ddlActivity.Attributes.Add("onchange", "Cktxt(0);");
        txtAll.Attributes.Add("onkeyup", "CkNum();");
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
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
        }
    }
    private void getddlStrategies(int mode, string StudyYear)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, StudyYear);
            if (Cookie.GetValue2("EvaStrategies") == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Cookie.GetValue2("EvaStrategies").ToString();
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
    private void getddlProjects(int mode, string StudyYear, string StrategiesCode)
    {
        if (mode == 0)
        {
            if (string.IsNullOrEmpty(StrategiesCode))
            {
                
            }
            btc.getddlProjects(0, ddlSearch, StudyYear, StrategiesCode);
            if (Cookie.GetValue2("EvaProjects") == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(StrategiesCode))
                    {
                        ddlSearch.SelectedValue = Cookie.GetValue2("EvaProjects").ToString();
                    }
                    else
                    {
                        ddlSearch.SelectedIndex = 0;
                    }                   
                }
                catch (Exception ex)
                {
                    ddlSearch.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlProjects(1, ddlProjects, StudyYear, StrategiesCode);
        }
    }
    private void getddlActivity(int mode, string ProjectsCode)
    {
        if (mode == 0)
        {
            btc.getddlActivity(0, ddlSearch1, ProjectsCode);
            if (Cookie.GetValue2("EvaActivity") == null)
            {
                ddlSearch1.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(ProjectsCode))
                    {
                        ddlSearch1.SelectedValue = Cookie.GetValue2("EvaActivity").ToString();
                    }
                    else
                    {
                        ddlSearch1.SelectedIndex = 0;
                    }                 
                }
                catch (Exception ex)
                {
                    ddlSearch1.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlActivity(1, ddlActivity, ProjectsCode);
        }
    }
    private void getddlIndicators2(int mode, string ActivityCode)
    {
        if (mode == 0)
        {
            btc.getddlIndicators2(0, ddlSearch3, ActivityCode);
            if (Cookie.GetValue2("EvaIndicators2") == null)
            {
                ddlSearch3.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(ActivityCode))
                    {
                        ddlSearch3.SelectedValue = Cookie.GetValue2("EvaIndicators2").ToString();
                    }
                    else
                    {
                        ddlSearch3.SelectedIndex = 0;
                    }                       
                }
                catch (Exception ex)
                {
                    ddlSearch3.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlIndicators2(1, ddlIndicators2, ActivityCode);
        }
    }
    private void getddlQACategory(int mode, string StudyYear)
    {
        if (mode == 0)
        {
        }
        if (mode == 1)
        {
            btc.getddlQACategory(mode, BTC.DDl.QACategory, ddlQACategory, StudyYear, "", ddlIndicators2.SelectedValue);
        }
    }
    private void getddlQACategorySub(int mode, string StudyYear)
    {
        if (mode == 0)
        {
        }
        if (mode == 1)
        {
            btc.getddlQACategory(mode, BTC.DDl.QACategorySub, ddlQACategorySub, StudyYear, ddlQACategory.SelectedValue, ddlIndicators2.SelectedValue);
        }
    }
    private void getWeight(string Indicators2Code)
    {
        DataView dv;
        dv = btc.getWeight(Indicators2Code);
        if (dv.Count != 0)
        {
            txtWeight.Text = Convert.ToInt32(dv[0]["Weight"]).ToString("#,##0");
            txtAll.Text = Convert.ToInt32(dv[0]["OffAll"]).ToString("#,##0");
            lblWeightType.InnerText = (Convert.ToInt32(dv[0]["WeightType"]) == 0) ? "%" : "";
        }
        else
        {
            txtWeight.Text = "0";
            txtAll.Text = "0";
            lblWeightType.InnerText = "%";
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "javascript", "CkPercent();", true);
        }
    }
    private void getddlSide(int mode)
    {
        if (mode == 0)
        {
            btc.getddlSide(0, ddlSearchSide, ddlSearchYear.SelectedValue);
            if (Cookie.GetValue2("EvaSide") == null)
            {
                ddlSearchSide.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearchSide.SelectedValue = Cookie.GetValue2("EvaSide").ToString();
                }
                catch (Exception ex)
                {
                    ddlSearchSide.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlSide(1, ddlSide, ddlYearB.SelectedValue);
        }
    }
    private void getddlStandard(int mode, string SideCode)
    {
        if (mode == 0)
        {
            btc.getddlStandard(0, ddlSearchStandard, ddlSearchYear.SelectedValue, SideCode);
            if (Cookie.GetValue2("EvaStandard") == null)
            {
                ddlSearchStandard.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearchStandard.SelectedValue = Cookie.GetValue2("EvaStandard").ToString();
                }
                catch (Exception ex)
                {
                    ddlSearchStandard.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlStandard(1, ddlStandard, ddlYearB.SelectedValue, ddlSide.SelectedValue);
        }
    }
    private void getddlIndicators(int mode, string StandardCode)
    {
        if (mode == 0)
        {
            btc.getddlIndicators(0, ddlSearchIndicators, StandardCode);
            if (Cookie.GetValue2("EvaIndicators") == null)
            {
                ddlSearchIndicators.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearchIndicators.SelectedValue = Cookie.GetValue2("EvaIndicators").ToString();
                }
                catch (Exception ex)
                {
                    ddlSearchIndicators.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            if (Request.QueryString["mode"] == "1")
            {
                btc.getddlIndicatorsNotInEvaluation(1, ddlIndicators, StandardCode, ddlIndicators2.SelectedValue);
            }
            else
            {
                btc.getddlIndicators(1, ddlIndicators, StandardCode);
            }
        }
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    private void getddlEmpByDept(int mode, string DeptCode)
    {
        if (mode == 0)
        {
            btc.getddlEmpByDept(0, ddlSearchEmp, DeptCode);
        }
    }
    private void getActivityDetail(string ActivityCode)
    {
        DataView dv = btc.getActivityDetail(ActivityCode);
        if (dv.Count != 0)
        {
            txtActivityDetail.Text = dv[0]["ActivityDetail"].ToString();
            lblType.Text = dv[0]["CostsType"].ToString();
            lblTerm.Text = dv[0]["Term"].ToString() + "/" + dv[0]["YearB"].ToString();
            lblBudgetYear.Text = dv[0]["BudgetYear"].ToString();
            txtTotalAmount.Text = Convert.ToDouble(dv[0]["TotalAmount"]).ToString("#,##0.00");
            lblDepartment.Text = dv[0]["DeptName"].ToString();
            lblEmpName.Text = dv[0]["EmpName"].ToString();
            txtSDay.Text = Convert.ToDateTime(dv[0]["SDate"]).ToShortDateString();
            txtEDay.Text = Convert.ToDateTime(dv[0]["EDate"]).ToShortDateString();
        }
        else
        {
            txtActivityDetail.Text = "";
            lblType.Text = "";
            lblTerm.Text = "";
            lblBudgetYear.Text = "";
            txtTotalAmount.Text = "0.00";
            lblDepartment.Text = "";
            lblEmpName.Text = "";
            txtSDay.Text = "";
            txtEDay.Text = "";
        }
    }
    public override void DataBind()
    {
        string strSideName = "'ด้านที่ '";
        string strStandardName = "'มาตรฐานที่ '";
        string strIndicatorName = "'ตัวบ่งชี้ที่ '";
        if (btc.ckIdentityName("ckStandardNew"))
        {
            strSideName = "'มาตรฐานที่ '";
            strStandardName = "'ประเด็นฯที่ ' + Cast(s.Sort As nVarChar) + '.' ";
            strIndicatorName = "'ประเด็นฯย่อยที่ ' + Cast(s.Sort As nVarChar)  + '.' ";
        }
        string StrSql = "Select a.EvaluationCode, b.StandardCode, {1} + Cast(b.Sort As nVarChar) StandardName, {0} + Cast(s.Sort As nVarChar) SideName, "
                    + " {2} + Cast(b.Sort As nVarChar) + '.' + Cast(h.Sort As nVarChar) IndicatorsName, "
                    + " a.IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                    + " a.OffAll, a.OffThat, a.APercent, a.CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                    + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, b.Sort SortStd, h.Sort SortInt, s.Sort SortSide "
                    + " From Evaluation a, Standard b, Projects c, Activity d, Indicators2 e, Strategies f, Indicators h, Side s "
                    + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And a.StandardCode = b.StandardCode And a.ProjectsCode = c.ProjectsCode And b.SideCode = s.SideCode "
                    + " And a.ActivityCode = d.ActivityCode And a.Indicators2Code = e.Indicators2Code And a.StrategiesCode = f.StrategiesCode "
                    + " And a.IndicatorsCode = h.IndicatorsCode ";

 
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = "Select a.EvaluationCode, b.StandardCode, {1} + Cast(b.Sort As nVarChar) StandardName, {0} + Cast(s.Sort As nVarChar) SideName, "
                    + " {2} + Cast(b.Sort As nVarChar) + '.' + Cast(h.Sort As nVarChar) IndicatorsName, "
                    + " a.IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                    + " a.OffAll, a.OffThat, a.APercent, a.CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                    + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, b.Sort SortStd, h.Sort SortInt, s.Sort SortSide "
                    + " From Evaluation a, Standard b, Projects c, Activity d, Indicators2 e, Strategies f, dtAcDept g, Indicators h, Side s "
                    + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And g.DeptCode = '" + ddlSearchDept.SelectedValue + "' "
                    + " And a.StandardCode = b.StandardCode And a.ProjectsCode = c.ProjectsCode And b.SideCode = s.SideCode "
                    + " And a.ActivityCode = d.ActivityCode And a.Indicators2Code = e.Indicators2Code And a.StrategiesCode = f.StrategiesCode "
                    + " And a.IndicatorsCode = h.IndicatorsCode And d.ActivityCode = g.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select a.EvaluationCode, b.StandardCode, {1} + Cast(b.Sort As nVarChar) StandardName, {0} + Cast(s.Sort As nVarChar) SideName, "
                        + " {2} + Cast(b.Sort As nVarChar) + '.' + Cast(h.Sort As nVarChar) IndicatorsName, "
                        + " a.IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                        + " a.OffAll, a.OffThat, a.APercent, a.CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                        + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, b.Sort SortStd, h.Sort SortInt, s.Sort SortSide "
                        + " From Evaluation a, Standard b, Projects c, Activity d, Indicators2 e, Strategies f, dtAcEmp g, Indicators h, Side s "
                        + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And g.EmpCode = '" + ddlSearchEmp.SelectedValue + "' "
                        + " And a.StandardCode = b.StandardCode And a.ProjectsCode = c.ProjectsCode And b.SideCode = s.SideCode "
                        + " And a.ActivityCode = d.ActivityCode And a.Indicators2Code = e.Indicators2Code And a.StrategiesCode = f.StrategiesCode "
                        + " And a.IndicatorsCode = h.IndicatorsCode And d.ActivityCode = g.ActivityCode ";
            }
            else
            {
                StrSql = "Select a.EvaluationCode, b.StandardCode, {1} + Cast(b.Sort As nVarChar) StandardName, {0} + Cast(s.Sort As nVarChar) SideName, "
                        + " {2} + Cast(b.Sort As nVarChar) + '.' + Cast(h.Sort As nVarChar) IndicatorsName, "
                        + " a.IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                        + " a.OffAll, a.OffThat, a.APercent, a.CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                        + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, b.Sort SortStd, h.Sort SortInt, s.Sort SortSide "
                        + " From Evaluation a, Standard b, Projects c, Activity d, Indicators2 e, Strategies f, dtAcEmp g, Indicators h, dtAcDept i, Side s "
                        + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And g.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And i.DeptCode = '" + ddlSearchDept.SelectedValue + "' "
                        + " And a.StandardCode = b.StandardCode And a.ProjectsCode = c.ProjectsCode And b.SideCode = s.SideCode "
                        + " And a.ActivityCode = d.ActivityCode And a.Indicators2Code = e.Indicators2Code And a.StrategiesCode = f.StrategiesCode "
                        + " And a.IndicatorsCode = h.IndicatorsCode And d.ActivityCode = g.ActivityCode And d.ActivityCode = i.ActivityCode ";
            }
        }     
        if (ddlSearchStandard.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StandardCode = '" + ddlSearchStandard.SelectedValue + "' ";
        }
        if (ddlSearchIndicators.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.IndicatorsCode = '" + ddlSearchIndicators.SelectedValue + "' ";
        }
        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And f.StrategiesCode = '" + ddlSearch2.SelectedValue + "' ";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And c.ProjectsCode = '" + ddlSearch.SelectedValue + "' ";
        }
        if (ddlSearch1.SelectedIndex != 0)
        {
            StrSql = StrSql + " And d.ActivityCode = '" + ddlSearch1.SelectedValue + "' ";
        }
        if (ddlSearch3.SelectedIndex != 0)
        {
            StrSql = StrSql + " And e.Indicators2Code = '" + ddlSearch3.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And (d.ActivityName Like '%" + txtSearch.Text + "%' Or e.IndicatorsName2 Like '%" + txtSearch.Text + "%') ";
        }
        if ((ddlSearchStandard.SelectedIndex == 0) && (ddlSearchIndicators.SelectedIndex == 0))
        {
            if ((ddlSearchDept.SelectedIndex == 0) && (ddlSearchEmp.SelectedIndex == 0))
            {
                StrSql = StrSql + " Union "
                            + " Select '' As EvaluationCode, '' As StandardCode, '' As StandardName, '' As SideName, "
                            + " '' As IndicatorsName, "
                            + " '' As IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                            + " 0 As OffAll, 0 As OffThat, 0 As APercent, 0 As CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                            + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, 0 As SortStd, 0 As SortInt, 0 As SortSide "
                            + " From Projects c, Activity d, Indicators2 e, Strategies f "
                            + " Where e.DelFlag = 0 And f.StrategiesCode = c.StrategiesCode "
                            + " And c.ProjectsCode = d.ProjectsCode "
                            + " And d.ActivityCode = e.ActivityCode "
                            + " And d.StudyYear = '" + ddlSearchYear.SelectedValue + "' And d.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And e.Indicators2Code Not In(Select Indicators2Code From Evaluation) ";
            }
            else
            {
                if ((ddlSearchDept.SelectedIndex != 0) && (ddlSearchEmp.SelectedIndex == 0))
                {
                    StrSql = StrSql + " Union "
                            + " Select '' As EvaluationCode, '' As StandardCode, '' As StandardName, '' As SideName, "
                            + " '' As IndicatorsName, "
                            + " '' As IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                            + " 0 As OffAll, 0 As OffThat, 0 As APercent, 0 As CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                            + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, 0 As SortStd, 0 As SortInt, 0 As SortSide "
                            + " From Projects c, Activity d, Indicators2 e, Strategies f, dtAcDept g "
                            + " Where e.DelFlag = 0 And f.StrategiesCode = c.StrategiesCode "
                            + " And c.ProjectsCode = d.ProjectsCode "
                            + " And d.ActivityCode = e.ActivityCode "
                            + " And d.ActivityCode = g.ActivityCode "
                            + " And d.StudyYear = '" + ddlSearchYear.SelectedValue + "' And d.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And e.Indicators2Code Not In(Select Indicators2Code From Evaluation) "
                            + " And g.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                }
                if (ddlSearchEmp.SelectedIndex != 0)
                {
                    StrSql = StrSql + " Union "
                            + " Select '' As EvaluationCode, '' As StandardCode, '' As StandardName, '' As SideName, "
                            + " '' As IndicatorsName, "
                            + " '' As IndicatorsCode, c.ProjectsCode, c.ProjectsName, d.ActivityCode, d.ActivityName, d.SDate, d.EDate, d.Status, "
                            + " 0 As OffAll, 0 As OffThat, 0 As APercent, 0 As CkCriterion, e.Indicators2Code, e.IndicatorsName2, '' DeptName, "
                            + " f.Sort SortStg, c.Sort SortPj, d.Sort SortAc, e.Sort SortInt2, 0 As SortStd, 0 As SortInt, 0 As SortSide "
                            + " From Projects c, Activity d, Indicators2 e, Strategies f, dtAcEmp g "
                            + " Where e.DelFlag = 0 And f.StrategiesCode = c.StrategiesCode "
                            + " And c.ProjectsCode = d.ProjectsCode "
                            + " And d.ActivityCode = e.ActivityCode "
                            + " And d.ActivityCode = g.ActivityCode "
                            + " And d.StudyYear = '" + ddlSearchYear.SelectedValue + "' And d.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And e.Indicators2Code Not In(Select Indicators2Code From Evaluation) "
                            + " And g.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
                }
            }
            if (ddlSearch2.SelectedIndex != 0)
            {
                StrSql = StrSql + " And f.StrategiesCode = '" + ddlSearch2.SelectedValue + "' ";
            }
            if (ddlSearch.SelectedIndex != 0)
            {
                StrSql = StrSql + " And c.ProjectsCode = '" + ddlSearch.SelectedValue + "' ";
            }
            if (ddlSearch1.SelectedIndex != 0)
            {
                StrSql = StrSql + " And d.ActivityCode = '" + ddlSearch1.SelectedValue + "' ";
            }
            if (ddlSearch3.SelectedIndex != 0)
            {
                StrSql = StrSql + " And e.Indicators2Code = '" + ddlSearch3.SelectedValue + "' ";
            }
            if (txtSearch.Text != "")
            {
                StrSql = StrSql + " And (d.ActivityName Like '%" + txtSearch.Text + "%' Or e.IndicatorsName2 Like '%" + txtSearch.Text + "%') ";
            }
        }
        //Response.Write(string.Format(StrSql + " Order By SortStg Desc, SortPj Desc, SortAc Desc, SortInt2 Desc, SortStd, SortInt, SortSide", strSideName, strStandardName, strIndicatorName)); return;
        DataView dv = Conn.Select(string.Format(StrSql + " Order By SortStg Desc, SortPj Desc, SortAc Desc, SortInt2 Desc, SortStd, SortInt, SortSide", strSideName, strStandardName, strIndicatorName));

        for (int j = 0; j < dv.Count; j++)
        {
            dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["ActivityCode"].ToString());
        }
        if (dv.Count != 0)
        {
            btnDelete.Visible = true;
        }
        else
        {
            btnDelete.Visible = false;
        }
        //if (!btc.ckIdentityName("ckQA"))
        //{
        //    GridView1.Columns[GridView1.Columns.Count - 3].Visible = false;
        //    GridView1.Columns[GridView1.Columns.Count - 4].Visible = false;
        //}
        GridView1.DataSource = dv;
        GridView1.CheckListDataField = "EvaluationCode";
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();

        StrSql = " Select a.EvaluationCode, a.StandardCode, a.ProjectsCode, a.ActivityCode, a.Indicators2Code, {1} + Cast(b.Sort As nVarChar) + ' - ' + b.StandardName StandardName, "
                + " a.IndicatorsCode, {2} + Cast(b.Sort As nVarChar) + '.' + Cast(c.Sort As nVarChar) + ' - ' + c.IndicatorsName IndicatorsName, "
                + " s.SideCode, {0} + Cast(s.Sort As nVarChar) + ' - ' + s.SideName SideName "
                + " From Evaluation a, Standard b, Indicators c,  Projects P, Activity Ac, Side s "
                + " Where a.StandardCode = b.StandardCode And a.IndicatorsCode = c.IndicatorsCode And b.SideCode = s.SideCode And a.ProjectsCode = P.ProjectsCode And a.ActivityCode = Ac.ActivityCode "
                + " And a.Indicators2Code = '" + ddlIndicators2.SelectedValue + "' ";

        DataView dv1 = Conn.Select(string.Format(StrSql + " Order By b.Sort, c.Sort", strSideName, strStandardName, strIndicatorName));
        //if (!btc.ckIdentityName("ckQA"))
        //{
        //    GridView2.Columns[GridView2.Columns.Count - 1].Visible = false;
        //    GridView2.Columns[GridView2.Columns.Count - 2].Visible = false;
        //}
        GridView2.DataSource = dv1;
        GridView2.DataBind();
    }
    protected string DateFormat(object startDate, object endDate)
    {
        return Convert.ToDateTime(startDate).ToString("dd/MM/yy") + " - " + Convert.ToDateTime(endDate).ToString("dd/MM/yy");
    }
    protected string strEvaluation(string Indicators2Code, string Name)
    {
        if (Name == "")
        {
            return "<a href=\"Evaluation.aspx?mode=1&id=" + Indicators2Code + "\"><img style=\"border: 0; cursor: pointer;\" title=\"เชื่อมโยงตัวบ่งชี้ภายใต้ตัวชี้วัดนี้\" src=\"../Image/goto.png\" /></a>";
        }
        else
        {
            return Name;
        }
    }
    protected string strQA(String mode, String QACode,String ProjectsCode , String ActivityCode, String Indicators2Code)
    {
       DataView dv = null;
       String StrSql = "";
       String Name = "<img src=\"../Image/waitiCon.gif\" alt=\"ติดต่อเจ้าหน้าที่เพื่อเปิดโหมด\" title=\"ติดต่อเจ้าหน้าที่เพื่อเปิดโหมด\" />";
       if (btc.ckIdentityName("ckQA"))
       {
           if (mode == "0")
           { //หมวดเกณฑ์
               Name = "หมวดที่ ";
               StrSql = @" Select distinct QC.Sort, Cast(QC.Sort As nVarChar)  As QName From QALink Q, QACategory QC Where Q.QACategoryID = QC.QACategoryID 
                    And Q.ProjectsCode = '{0}' And Q.ActivityCode = '{1}' And Q.Indicators2Code = '{2}' Order By QC.Sort";
           }
           else if (mode == "1")
           {//หัวข้อเกณฑ์
               Name = "หัวข้อที่ ";
               StrSql = @" Select distinct QC.Sort, QS.Sort, Q.QALinkCode, Cast(QC.Sort As nVarChar) + '.' +  Cast(QS.Sort As nVarChar)  As QName 
                    From QALink Q, QACategory QC, QACategorySub QS Where Q.QACategoryID = QC.QACategoryID And Q.QACategorySubID = QS.QACategorySubID  
                    And Q.ProjectsCode = '{0}' And Q.ActivityCode = '{1}' And Q.Indicators2Code = '{2}' Order By QC.Sort, QS.Sort";
           }

           dv = Conn.Select(String.Format(StrSql, ProjectsCode, ActivityCode, Indicators2Code));
           if (dv.Count > 0)
           {
               for (int i = 0; i < dv.Count; i++)
               {
                   if (mode == "0")
                   { //หมวดเกณฑ์
                       Name += dv[i]["QName"].ToString() + ",";
                   }
                   else if (mode == "1")
                   {//หัวข้อเกณฑ์
                       Name += "<a href=\"javascript:;\" title=\"คลิ๊กเพื่อลบ\" onclick=\"delItem('" + dv[i]["QALinkCode"].ToString() + "');\" >" + dv[i]["QName"].ToString() + "</a>" + ",";
                   }
               }
               if (Name.Length > 0)
                   Name = Name.Substring(0, Name.Length - 1);
           }
           else
           {
               Name = "<img src=\"../Image/waitiCon.gif\" alt=\"ติดต่อเจ้าหน้าที่เพื่อเปิดโหมด\" title=\"ติดต่อเจ้าหน้าที่เพื่อเปิดโหมด\" />";
           }       
       }
        return Name;
    }
    private void GetDataGo(string id)
    {
        DataView dv;
        if (String.IsNullOrEmpty(id)) return;
        string strSql = " Select a.*, b.StudyYear, c.ActivityDetail, c.CostsType, IsNull(c.TotalAmount, 0) TotalAmount, c.BudgetYear, c.Term, c.YearB, "
                + " c.DeptCode, c.SDate, c.EDate "
                + " From Indicators2 a, Projects b, Activity c "
                + " Where a.ProjectsCode = b.ProjectsCode And a.ActivityCode = c.ActivityCode "
                + " And a.Indicators2Code =  '" + id + "' ";
        dv = Conn.Select(string.Format(strSql));
        if (dv.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlStrategies(1, ddlYearB.SelectedValue);
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
            ddlProjects.SelectedValue = dv[0]["ProjectsCode"].ToString();
            getddlActivity(1, ddlProjects.SelectedValue);
            ddlActivity.SelectedValue = dv[0]["ActivityCode"].ToString();
            getActivityDetail(ddlActivity.SelectedValue);
            getddlIndicators2(1, ddlActivity.SelectedValue);
            ddlIndicators2.SelectedValue = dv[0]["Indicators2Code"].ToString();
            getWeight(ddlIndicators2.SelectedValue);
            lblWeightType.InnerText = (Convert.ToInt32(dv[0]["WeightType"]) == 0) ? "%" : "";
            txtTotalAmount.Text = Convert.ToDouble(dv[0]["TotalAmount"]).ToString("#,##0.00");

            if (ddlActivity.SelectedIndex == 0)
            {
                A1.Visible = false;
            }
            else
            {
                A1.Visible = true;
            }
        }
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv;
        dv = Conn.Select(string.Format("Select a.EvaluationCode, a.StandardCode, a.SideCode, "
            + " a.IndicatorsCode, c.StrategiesCode, a.ProjectsCode, a.ActivityCode, a.Indicators2Code, "
            + " a.OffAll, a.OffThat, a.APercent, a.CkCriterion, c.StudyYear, c.StrategiesCode "
            + " From Evaluation a, Standard b, Projects c, Activity d "
            + " Where a.DelFlag = '0' And a.StandardCode = b.StandardCode And a.ProjectsCode = c.ProjectsCode "
            + " And a.ActivityCode = d.ActivityCode And a.EvaluationCode = '" + id + "' "));

        if (dv.Count != 0)
        {
            getddlYear(1);
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlSide(1);
            ddlSide.SelectedValue = dv[0]["SideCode"].ToString();
            getddlStandard(1, ddlSide.SelectedValue);
            ddlStandard.SelectedValue = dv[0]["StandardCode"].ToString();
            getddlIndicators(1, ddlStandard.SelectedValue);
            ddlIndicators.SelectedValue = dv[0]["IndicatorsCode"].ToString();         
            getddlStrategies(1, ddlYearB.SelectedValue);
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
            ddlProjects.SelectedValue = dv[0]["ProjectsCode"].ToString();
            getddlActivity(1, ddlProjects.SelectedValue);
            ddlActivity.SelectedValue = dv[0]["ActivityCode"].ToString();
            getActivityDetail(ddlActivity.SelectedValue);
            getddlIndicators2(1, ddlActivity.SelectedValue);
            ddlIndicators2.SelectedValue = dv[0]["Indicators2Code"].ToString();
            getWeight(ddlIndicators2.SelectedValue);
            txtAll.Text = Convert.ToInt32(dv[0]["OffAll"]).ToString("#,##0");
            //getddlDevelopLevel(ddlIndicators.SelectedValue);
            //ddlDevelopLevel.SelectedValue = dv.Table.Rows[0]["DevelopLevelCode"].ToString();

            //DataView dv1;
            //dv1 = new BTC().getDevelopLevelDetail(ddlDevelopLevel.SelectedValue);
            //txtLevelDetail.Text = dv1.Table.Rows[0]["LevelDetail"].ToString();
            //ddlActivity.SelectedValue = dv.Table.Rows[0]["ActivityCode"].ToString();
        }
    }
    private void ClearAll()
    {
        txtSearch.Text = "";
        ddlSide.SelectedIndex = 0;
        ddlStandard.SelectedIndex = 0;
        ddlIndicators.SelectedIndex = 0;
        if (btc.ckIdentityName("ckQA"))
        {
            ddlQACategory.SelectedIndex = 0;
            ddlQACategorySub.SelectedIndex = 0;
        }
       
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        Int32 CkCriterion = 0;
        Int32 i = 0;
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            if ((ddlStandard.SelectedIndex != 0) && (ddlIndicators.SelectedIndex != 0))
            {
                i = Conn.AddNew("Evaluation", "EvaluationCode, StandardCode, IndicatorsCode, StrategiesCode, ProjectsCode, ActivityCode, Indicators2Code, OffAll, OffThat, APerCent, CkCriterion, DelFlag, StudyYear, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlStandard.SelectedValue, ddlIndicators.SelectedValue, ddlStrategies.SelectedValue, ddlProjects.SelectedValue, ddlActivity.SelectedValue, ddlIndicators2.SelectedValue, Convert.ToDouble(txtAll.Text), 0, 0, CkCriterion, 0, ddlYearB.SelectedValue, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
            }
            //Conn.Update("Activity", "Where ActivityCode = '" + ddlActivity.SelectedValue + "'", "Status", "1");
            if (btc.ckIdentityName("ckQA"))
            {
                if ((ddlQACategory.SelectedIndex != 0) && (ddlQACategorySub.SelectedIndex != 0))
                {
                    String NewQAID = Guid.NewGuid().ToString();
                    i = Conn.AddNew("QALink", "QALinkCode, QACategoryID, QACategorySubID, StrategiesCode, ProjectsCode, ActivityCode, Indicators2Code, DelFlag, StudyYear, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlQACategory.SelectedValue, ddlQACategorySub.SelectedValue, ddlStrategies.SelectedValue, ddlProjects.SelectedValue, ddlActivity.SelectedValue, ddlIndicators2.SelectedValue, 0, ddlYearB.SelectedValue, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
                }
            }

            btc.Msg_Head(Img1, MsgHead, true, "1", i);

            if (CkAgain == "N")
            {
                Response.Redirect("Evaluation.aspx?ckmode=1&Cr=" + i);   
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                ddlIndicators.Enabled = false;
                GridView2.Visible = true;
                DataBind();                
            }
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Evaluation", "Where EvaluationCode = '" + Request["id"] + "' ", "StandardCode, IndicatorsCode, StrategiesCode, ProjectsCode, ActivityCode, Indicators2Code, OffAll, OffThat, APerCent, CkCriterion, StudyYear, SchoolID, UpdateUser, UpdateDate", ddlStandard.SelectedValue, ddlIndicators.SelectedValue, ddlStrategies.SelectedValue, ddlProjects.SelectedValue, ddlActivity.SelectedValue, ddlIndicators2.SelectedValue, Convert.ToDouble(txtAll.Text), 0, 0, CkCriterion, ddlYearB.SelectedValue, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now);
            if (btc.ckIdentityName("ckQA"))
            {
            }
            
            Response.Redirect("Evaluation.aspx?ckmode=2&Cr=" + i);  
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (GridView1.SelectedItems.Length == 0) return;
        string query = "DELETE Evaluation WHERE EvaluationCode IN('";
        query += String.Join("', '", GridView1.SelectedItems);
        query += "')";

        string strSql = " Select EvaluationCode, Indicators2Code from Evaluation WHERE EvaluationCode IN('";
        strSql += String.Join("', '", GridView1.SelectedItems);
        strSql += "')";
        DataView dv = Conn.Select(strSql);
        if (dv.Count != 0) 
        {
            for (int i = 0; i < dv.Count; i++)
            {
                strSql = @" Select Indicators2Code from Evaluation Where EvaluationCode = '" + dv[i]["EvaluationCode"].ToString() + "'";
                DataView dv2 = Conn.Select(string.Format(strSql));
                if (dv2.Count != 0)
                {
                    strSql = @" Select Indicators2Code From Evaluation Where Indicators2Code = '" + dv2[0]["Indicators2Code"].ToString() + "'";
                    DataView dv1 = Conn.Select(string.Format(strSql));
                    if (dv1.Count < 2)
                    {
                        Conn.Delete("QALink", "Where Indicators2Code = '" + dv2[0]["Indicators2Code"].ToString() + "'");
                    }
                }
            } 
        }

        //query = "DELETE QALink WHERE QALinkCode IN('";
        //query += String.Join("', '", GridView1.SelectedItems);
        //query += "')";
       
        try
        {
            Conn.Execute(query);
            //Response.Redirect(Request.RawUrl);
            MultiView1.ActiveViewIndex = 0;
            btc.Msg_Head(Img1, MsgHead, true, "3", 1);
            DataBind();
        }
        catch (Exception ex)
        {
            MultiView1.ActiveViewIndex = 0;
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
            DataBind();
            //lblMessage.Text = "<img src=\"../Shared/images/no.png\" alt=\"เกิดข้อผิดพลาด\" />เกิดข้อผิดพลาดระหว่างดำเนินการ " + ex.Message;
        }
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        //DataView dv = Conn.Select(string.Format("Select DevelopLevelCode From Develop Where DevelopLevelCode = '" + id + "' And DelFlag = '0' "));
        //if (dv.Table.Rows.Count > 0)
        //{
        //    btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        //}
        //else
        //{
            string strSql = @" Select Indicators2Code from Evaluation Where EvaluationCode = '" + id + "'";
            DataView dv = Conn.Select(string.Format(strSql));
            if (dv.Count != 0)
            {
                strSql = @" Select Indicators2Code From Evaluation Where Indicators2Code = '" + dv[0]["Indicators2Code"].ToString() + "'";
                DataView dv1 = Conn.Select(string.Format(strSql));
                if (dv1.Count < 2)
                {
                    Conn.Delete("QALink", "Where Indicators2Code = '" + dv1[0]["Indicators2Code"].ToString() + "'");
                }
            }
            Int32 j = Conn.Delete("Evaluation", "Where EvaluationCode = '" + id + "'");
            Response.Redirect("Evaluation.aspx?ckmode=3&Cr=" + j); 
    }
    private void DelItemQA(string id)
    {
        Int32 i = Conn.Delete("QALink", "Where QALinkCode = '" + id + "'");
        Response.Redirect("Evaluation.aspx?ckmode=3&Cr=" + i); 
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlSide(1);
        getddlStandard(1, ddlSide.SelectedValue);
        getddlIndicators(1, ddlStandard.SelectedValue);
        getddlStrategies(1, ddlYearB.SelectedValue);
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
        getddlIndicators2(1, ddlActivity.SelectedValue);
        getWeight(ddlIndicators2.SelectedValue);
        if (ddlActivity.SelectedIndex == 0)
        {
            A1.Visible = false;
        }
        else
        {
            A1.Visible = true;
        }
    }
    protected void ddlSide_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStandard(1, ddlSide.SelectedValue);
        getddlIndicators(1, ddlStandard.SelectedValue);
    }
    protected void ddlStandard_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlIndicators(1, ddlStandard.SelectedValue);
    }
    protected void ddlStrategies_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
        getddlIndicators2(1, ddlActivity.SelectedValue);
        getWeight(ddlIndicators2.SelectedValue);
        if (ddlActivity.SelectedIndex == 0)
        {
            A1.Visible = false;
        }
        else
        {
            A1.Visible = true;
        }
    }
    protected void ddlProjects_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlStrategies.SelectedValue = btc.getStrategiesCodeOfProj(ddlProjects.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
        getddlIndicators2(1, ddlActivity.SelectedValue);
        getWeight(ddlIndicators2.SelectedValue);
        if (ddlActivity.SelectedIndex == 0)
        {
            A1.Visible = false;
        }
        else
        {
            A1.Visible = true;
        }
    }
    protected void ddlActivity_OnSelectedChanged(object sender, EventArgs e)
    {
        if (ddlActivity.SelectedIndex == 0)
        {
            A1.Visible = false;
        }
        else
        {
            A1.Visible = true;
        }
        getActivityDetail(ddlActivity.SelectedValue);
        getddlIndicators2(1, ddlActivity.SelectedValue);
        getWeight(ddlIndicators2.SelectedValue);
    }
    protected void ddlIndicators2_OnSelectedChanged(object sender, EventArgs e)
    {
        getWeight(ddlIndicators2.SelectedValue);
        if (ddlIndicators2.SelectedIndex == 0)
        {
            ddlStandard.SelectedIndex = 0;
            ddlStandard.Enabled = false;
            getddlIndicators(1, ddlStandard.SelectedValue);
        }
        else
        {
            ddlStandard.SelectedIndex = 0;
            ddlStandard.Enabled = true;
            getddlIndicators(1, ddlStandard.SelectedValue);
        }
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStandard(0, ddlSearchSide.SelectedValue);
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        getddlIndicators2(0, ddlSearch1.SelectedValue);
        DataBind();
    }
    protected void ddlSearchSide_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaSide", ddlSearchSide.SelectedValue);
        getddlStandard(0, ddlSearchSide.SelectedValue);
        getddlIndicators(0, ddlSearchStandard.SelectedValue);
        DataBind();
    }
    protected void ddlSearchStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaStandard", ddlSearchStandard.SelectedValue);
        getddlIndicators(0, ddlSearchStandard.SelectedValue);
        DataBind();
    }
    protected void ddlSearchIndicators_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaIndicators", ddlSearchIndicators.SelectedValue);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaStrategies", ddlSearch2.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        getddlIndicators2(0, ddlSearch1.SelectedValue);
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaProjects", ddlSearch.SelectedValue);
        ddlSearch2.SelectedValue = btc.getStrategiesCodeOfProj(ddlSearch.SelectedValue);
        Cookie.SetValue2("EvaStrategies", ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        getddlIndicators2(0, ddlSearch1.SelectedValue);
        DataBind();
    }
    protected void ddlSearch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaActivity", ddlSearch1.SelectedValue);
        getddlIndicators2(0, ddlSearch1.SelectedValue);
        DataBind();
    }
    protected void ddlSearch3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaIndicators2", ddlSearch3.SelectedValue);
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("EvaEmp", ddlSearchEmp.SelectedValue);
        DataBind();
    }
    protected void btAc_OnClick(object sender, EventArgs e)
    {
        var id = ddlActivity.SelectedValue;
        getddlActivity(1, ddlProjects.SelectedValue);
        ddlActivity.SelectedValue = id;
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((DataBinder.Eval(e.Row.DataItem, "StandardName").ToString() == "") && (DataBinder.Eval(e.Row.DataItem, "IndicatorsName").ToString() == ""))
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void ddlQACategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        btc.getddlQACategory(1, BTC.DDl.QACategorySub, ddlQACategorySub, ddlYearB.SelectedValue, ddlQACategory.SelectedValue, ddlIndicators2.SelectedValue);
    }


}
