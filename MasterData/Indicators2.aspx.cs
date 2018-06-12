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

public partial class Indicators2 : System.Web.UI.Page
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

            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);

            getddlYear(0);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
            getddlActivity(0, ddlSearch.SelectedValue);
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("IndDept").ToString(), Cookie.GetValue2("IndEmp").ToString());

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
                            DataView dvCk = Conn.Select("Select StudyYear, StrategiesCode, ProjectsCode, ActivityCode From Activity Where ActivityCode = '" + gotoid + "'");
                            ddlYearB.SelectedValue = dvCk[0]["StudyYear"].ToString();
                            getddlStrategies(1, ddlYearB.SelectedValue);
                            ddlStrategies.SelectedValue = dvCk[0]["StrategiesCode"].ToString();
                            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
                            ddlProjects.SelectedValue = dvCk[0]["ProjectsCode"].ToString();
                            getddlActivity(1, ddlProjects.SelectedValue);
                            ddlActivity.SelectedValue = gotoid;
                        }
                        else
                        {
                            getddlStrategies(1, ddlYearB.SelectedValue);
                            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
                            getddlActivity(1, ddlProjects.SelectedValue);
                        }
                        getActivityDetail(ddlActivity.SelectedValue);
                        btc.GenSort(txtSort, "Indicators2", " And ActivityCode = '" + ddlActivity.SelectedValue + "'");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
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
                DataBind();
            }
        }
        ddlStrategies.Attributes.Add("onchange", "Cktxt(0);");
        ddlProjects.Attributes.Add("onchange", "Cktxt(0);");
        ddlActivity.Attributes.Add("onchange", "Cktxt(0);");
        txtIndicators2.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
        txtWeight.Attributes.Add("onchange", "CkNum();");
        txtWeight.Attributes.Add("onkeyup", "Cktxt(0); Calculate(0); ckRate();");
        txtAll.Attributes.Add("onchange", "CkNum(); ");
        txtAll.Attributes.Add("onkeyup", "CktxtAll(); Calculate(1);");
        ddlRateType.Attributes.Add("onchange", "ckRateType(0); ckRateDetail();");
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
            if (Cookie.GetValue2("IndStrategies") == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Cookie.GetValue2("IndStrategies").ToString();
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
            btc.getddlProjects(0, ddlSearch, StudyYear, StrategiesCode);
            if (Cookie.GetValue2("IndProjects") == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(StrategiesCode))
                    {
                        ddlSearch.SelectedValue = Cookie.GetValue2("IndProjects").ToString();
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
            if (Cookie.GetValue2("IndActivity") == null)
            {
                ddlSearch1.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(ProjectsCode))
                    {
                        ddlSearch1.SelectedValue = Cookie.GetValue2("IndActivity").ToString();
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
            if (btc.ckIdentityName("ckBudgetYear"))
            {
                lblBudgetYear.Text = dv[0]["StudyYear"].ToString();
            }
            else
            {
                lblBudgetYear.Text = dv[0]["BudgetYear"].ToString();
            }
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
        string StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                    + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, c.Df, "
                    + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort, "
                    + " RateType = Case c.RateType When 0 Then 'เชิงปริมาณ' When 1 Then 'เชิงคุณภาพ' End, "
                    + " Cast (Round(((c.Weight * 100) / CASE c.OffAll WHEN 0 THEN 1 ELSE c.OffAll END),2) As nVarChar) + ' %' As ckPercen "
                    + " From Projects a, Activity b, Indicators2 c Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode " 
                    + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                    + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, c.Df, "
                    + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort, "
                    + " RateType = Case c.RateType When 0 Then 'เชิงปริมาณ' When 1 Then 'เชิงคุณภาพ' End, "
                    + " Cast (Round(((c.Weight * 100) / CASE c.OffAll WHEN 0 THEN 1 ELSE c.OffAll END),2) As nVarChar) + ' %' As ckPercen "
                    + " From Projects a, Activity b, Indicators2 c, dtAcDept d "
                    + " Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And b.ActivityCode = d.ActivityCode " 
                    + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And d.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                        + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, c.Df, "
                        + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort, "
                        + " RateType = Case c.RateType When 0 Then 'เชิงปริมาณ' When 1 Then 'เชิงคุณภาพ' End, "
                        + " Cast (Round(((c.Weight * 100) / CASE c.OffAll WHEN 0 THEN 1 ELSE c.OffAll END),2) As nVarChar) + ' %' As ckPercen "
                        + " From Projects a, Activity b, Indicators2 c, dtAcEmp d "
                        + " Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And b.ActivityCode = d.ActivityCode "
                        + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                            + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, c.Df, "
                            + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort, "
                            + " RateType = Case c.RateType When 0 Then 'เชิงปริมาณ' When 1 Then 'เชิงคุณภาพ' End, "
                            + " Cast (Round(((c.Weight * 100) / CASE c.OffAll WHEN 0 THEN 1 ELSE c.OffAll END),2) As nVarChar) + ' %' As ckPercen "
                            + " From Projects a, Activity b, Indicators2 c, dtAcEmp d, dtAcdept e "
                            + " Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And b.ActivityCode = d.ActivityCode And b.ActivityCode = e.ActivityCode  "
                            + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And e.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
            }
        }
        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.ProjectsCode = '" + ddlSearch.SelectedValue + "' ";
        }
        if (ddlSearch1.SelectedIndex != 0)
        {
            StrSql = StrSql + " And b.ActivityCode = '" + ddlSearch1.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And c.IndicatorsName2 Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Order By a.Sort Desc, b.Sort Desc, c.Sort Desc" ));

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();

        DataView dv1 = Conn.Select(string.Format(StrSql + " And b.ActivityCode = '" + ddlActivity.SelectedValue + "' Order By a.Sort Desc, b.Sort Desc, c.Sort Desc"));
        GridView2.DataSource = dv1;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        string strSql = " Select a.ProjectsCode, a.StudyYear, a.StrategiesCode, b.ActivityCode, c.IndicatorsName2, "
                    + " c.Weight, c.WeightType, c.RateType, c.OffAll, c.OffThat, c.APercent, c.CkCriterion, c.Sort "
                    + " From Projects a, Activity b, Indicators2 c "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.ActivityCode = c.ActivityCode And c.Indicators2Code = '" + id + "'  ";
        DataView dv = Conn.Select(string.Format(strSql));

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
            if (btc.CkUseData(id, "Indicators2Code", "Evaluation", " And DelFlag = 0 "))
            {
                ddlYearB.Enabled = false;
                ddlStrategies.Enabled = false;
                ddlProjects.Enabled = false;
                ddlActivity.Enabled = false;
            }
            txtIndicators2.Text = dv[0]["IndicatorsName2"].ToString();

            if (Convert.ToInt32(dv[0]["WeightType"].ToString()) == 0)
            {
                txtWeight.Text = ((Convert.ToDecimal(dv[0]["OffAll"].ToString()) * 50) / 100).ToString("#,##0");
            }
            else
            {
                txtWeight.Text = string.IsNullOrEmpty(dv[0]["Weight"].ToString()) ? "0" : Convert.ToInt32(dv[0]["Weight"]).ToString("#,##0");
            }          
            ddlRateType.SelectedValue = dv[0]["RateType"].ToString();
            txtAll.Text = string.IsNullOrEmpty(dv[0]["OffAll"].ToString()) ? "0" : Convert.ToInt32(dv[0]["OffAll"]).ToString("#,##0");
            txtSort.Text = dv[0]["Sort"].ToString();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Indicators2", "Indicators2Code", id);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "ckRateType(1); Calculate(0);", true);
        }
    }
    private void ClearAll()
    {
        ddlRateType.SelectedIndex = 0;
        txtIndicators2.Text = "";
        txtWeight.Text = "0";
        txtAll.Text = "0";
        txtSearch.Text = "";
        txtSort.Text = "";
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
            i = Conn.AddNew("Indicators2", "Indicators2Code, StrategiesCode, ProjectsCode, ActivityCode, IndicatorsName2, Weight, WeightType, RateType, OffAll, OffThat, APerCent, CkCriterion, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlStrategies.SelectedValue, ddlProjects.SelectedValue, ddlActivity.SelectedValue, txtIndicators2.Text, Convert.ToDecimal(txtWeight.Text), 1, ddlRateType.SelectedValue, Convert.ToDecimal(txtAll.Text), 0, 0, CkCriterion, txtSort.Text, 1, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

            if (CkAgain == "N")
            {
                Response.Redirect("Indicators2.aspx?ckmode=1&Cr=" + i);     
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                btc.GenSort(txtSort, "Indicators2", " And ActivityCode = '" + ddlActivity.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                GridView2.Visible = true;
                DataBind();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "ckRateType(0);", true);
            }
            if (CkAgain == "G")
            {
                Response.Redirect("Evaluation.aspx?mode=1&id=" + NewID + "&syear=" + ddlYearB.SelectedValue + "&stgid=" + ddlStrategies.SelectedValue + "&pjid=" + ddlProjects.SelectedValue + "&acid=" + ddlActivity.SelectedValue);
            }
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Indicators2", "Where Indicators2Code = '" + Request["id"] + "' ", "StrategiesCode, ProjectsCode, ActivityCode, IndicatorsName2, Weight, WeightType, RateType, OffAll, Sort, SchoolID, UpdateUser, UpdateDate", ddlStrategies.SelectedValue, ddlProjects.SelectedValue, ddlActivity.SelectedValue, txtIndicators2.Text, Convert.ToDecimal(txtWeight.Text), 1, ddlRateType.SelectedValue, Convert.ToDecimal(txtAll.Text), txtSort.Text, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now);
            Conn.Update("Evaluation", "Where Indicators2Code = '" + Request["id"] + "' ", "OffAll, UpdateUser, UpdateDate", Convert.ToDouble(txtAll.Text), CurrentUser.ID, DateTime.Now);
            Response.Redirect("Indicators2.aspx?ckmode=2&Cr=" + i);  
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
        if (String.IsNullOrEmpty(id)) return;
        Int32 i = 0;
        if (btc.CkUseData(id, "Indicators2Code", "Evaluation", " And DelFlag = 0 "))
        {
            Response.Redirect("Indicators2.aspx?ckmode=3&Cr=" + i); 
        }
        else
        {
            i = Conn.Update("Indicators2", "Where Indicators2Code = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Indicators2.aspx?ckmode=3&Cr=" + i); 
        }
    } 
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndStrategies", ddlSearch2.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndProjects", ddlSearch.SelectedValue);
        ddlSearch2.SelectedValue = btc.getStrategiesCodeOfProj(ddlSearch.SelectedValue);
        Cookie.SetValue2("IndStrategies", ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndActivity", ddlSearch1.SelectedValue);
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndEmp", ddlSearchEmp.SelectedValue);
        DataBind();
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlStrategies(1, ddlYearB.SelectedValue);
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void ddlStrategies_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void ddlProjects_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlStrategies.SelectedValue = btc.getStrategiesCodeOfProj(ddlProjects.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void ddlActivity_OnSelectedChanged(object sender, EventArgs e)
    {
        btc.GenSort(txtSort, "Indicators2", " And ActivityCode = '" + ddlActivity.SelectedValue + "'");
        getActivityDetail(ddlActivity.SelectedValue);
    }
}
