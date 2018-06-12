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

public partial class ReportTreeView : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    DataView dvChildIndicators2 = null, dvChildActivity = null, dv2ChildActivity = null;
    DataView dvChildProjects = null, dv2ChildProjects = null, dv3ChildProjects = null;
    DataView dvChildIndicators = null, dv2ChildIndicators = null, dv3ChildIndicators = null, dv4ChildIndicators = null;
    DataView dvChildStandard = null, dv2ChildStandard = null, dv3ChildStandard = null, dv4ChildStandard = null, dv5ChildStandard = null;
    DataView dvChildSide = null, dv2ChildSide = null, dv3ChildSide = null, dv4ChildSide = null, dv5ChildSide = null, dv6ChildSide = null;

    protected override void OnPreInit(EventArgs e)
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 2)
        {
            this.MasterPageFile = "~/Master/MasterManageView.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //�礻է�����ҳ
            btc.ckBudgetYear(lblSearchYear, null);

            getddlYear();
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("TrDept").ToString(), Cookie.GetValue2("TrEmp").ToString());

            ddlYearB.Attributes.Add("onchange", "var fnc=parent.eval('loadEmp'); if(typeof(fnc)=='function' && fnc) fnc(); Loading.show();");
            ddlSearchDept.Attributes.Add("onchange", "var fnc=parent.eval('loadEmp'); if(typeof(fnc)=='function' && fnc) fnc(); Loading.show();");
            ddlSearchEmp.Attributes.Add("onchange", "var fnc=parent.eval('loadEmp'); if(typeof(fnc)=='function' && fnc) fnc(); Loading.show();");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "function loadEmp(){" + Page.ClientScript.GetPostBackEventReference(btn1, null, false) + ";}", true);

            DataBind();
        }
    }
    private void getddlYear()
    {
        btc.getdllStudyYear(ddlYearB);
        btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-������-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    private void getddlEmpByDept(int mode, string DeptCode)
    {
        if (mode == 0)
        {
            btc.getddlEmpByDept(0, ddlSearchEmp, DeptCode);
        }
    }
    private void BindMainDataView()
    {
        /////////////Indicators2
        string strSql = @" Select a.Indicators2Code, '��Ǫ���Ѵ - ' + a.IndicatorsName2 IndicatorsName2, '%' As WeightType, a.Sort, b.APercent, b.CkCriterion,
                    b.ProjectsCode, b.IndicatorsCode, b.ActivityCode
                    From Indicators2 a, Evaluation b 
                    Where b.StudyYear = '{0}' And b.SchoolID = '{1}' And a.DelFlag = 0 And b.DelFlag = 0 And a.Indicators2Code = b.Indicators2Code  
                    Group By a.Indicators2Code, a.IndicatorsName2, a.WeightType, a.Sort, b.APercent, b.CkCriterion, b.ProjectsCode, b.IndicatorsCode, b.ActivityCode 
                    Order By a.Sort";
        dvChildIndicators2 = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, CurrentUser.SchoolID));


        /////////////Activity
        strSql = @" Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName,
                    b.IndicatorsCode, a.ProjectsCode
                    From Activity a, Evaluation b 
                    Where b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode ";

        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName,
                    b.IndicatorsCode, a.ProjectsCode
                    From Activity a, Evaluation b, dtAcDept c 
                    Where b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.DelFlag = 0 And b.DelFlag = 0 And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName,
                        b.IndicatorsCode, a.ProjectsCode
                        From Activity a, Evaluation b, dtAcEmp c 
                        Where b.StudyYear = '{0}' And b.SchoolID = '{1}'
                        And a.DelFlag = 0 And b.DelFlag = 0 And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName,
                            b.IndicatorsCode, a.ProjectsCode
                            From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                            Where b.StudyYear = '{0}' And b.SchoolID = '{1}'
                            And a.DelFlag = 0 And b.DelFlag = 0 And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode ";
            }
        }
        dvChildActivity = Conn.Select(string.Format(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort, b.IndicatorsCode, a.ProjectsCode Order By a.Sort", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        for (int j = 0; j < dvChildActivity.Count; j++)
        {
            dvChildActivity[j]["DeptName"] = btc.getAcDeptName(dvChildActivity[j]["ActivityCode"].ToString());
        }

        //--------------Activity2
        strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b 
                Where a.DelFlag = 0 And b.DelFlag = 0  
                And b.StudyYear = '{0}' And b.SchoolID = '{1}' And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
            From Activity a, Evaluation b, dtAcDept c 
            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
            And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcEmp c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode ";
            }
        }
        dv2ChildActivity = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, CurrentUser.SchoolID));


        //////////////Projects
        strSql = @" Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort, b.IndicatorsCode 
                    From Projects a, Evaluation b, Activity c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort, b.IndicatorsCode 
                    From Projects a, Evaluation b, Activity c, dtAcDept d 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                    And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode And c.ActivityCode = d.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort, b.IndicatorsCode 
                        From Projects a, Evaluation b, Activity c, dtAcEmp d 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                        And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode And c.ActivityCode = d.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort, b.IndicatorsCode 
                        From Projects a, Evaluation b, Activity c, dtAcEmp d, dtAcDept e 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                        And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode And c.ActivityCode = d.ActivityCode And c.ActivityCode = e.ActivityCode                          
                        And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And e.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dvChildProjects = Conn.Select(string.Format(strSql + " Group By a.ProjectsCode, a.ProjectsName, a.Sort, b.IndicatorsCode Order By a.Sort ", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //---------Projects2
        strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  
                    And b.StudyYear = '{0}' And b.SchoolID = '{1}' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcDept c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b, dtAcEmp c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                        From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                        And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode                        
                        And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv2ChildProjects = Conn.Select(string.Format(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort, b.IndicatorsCode, a.ProjectsCode Order By a.Sort", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //--------Projects3
        strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
            From Activity a, Evaluation b, dtAcDept c 
            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
            And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcEmp c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode  
                    From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode                    
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv3ChildProjects = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, CurrentUser.SchoolID));

        string strIndicatorName = "'��Ǻ觪���� '";
        if (btc.ckIdentityName("ckStandardNew"))
        {
            strIndicatorName = "'���������Ϸ�� '";
        }
        ///////////////Indicators
        strSql = @" Select b.IndicatorsCode, b.WeightScore, {1} + Cast(a.Sort As nVarChar) + '.' 
                    + Cast(b.Sort As nVarChar) + ' - ' + b.IndicatorsName As IndicatorsName, b.ScoreGroupID, b.StandardCode 
                    From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '{0}' Order By a.Sort, b.Sort ";
        dvChildIndicators = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, strIndicatorName));

        //-----------Indicators2
        strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcDept c 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                        And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcEmp c 
                            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                            And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                            And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode                            
                            And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv2ChildIndicators = Conn.Select(string.Format(strSql + " Group By a.ProjectsCode, b.IndicatorsCode ", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //---------Indicators3
        strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode
                    From Activity a, Evaluation b 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b, dtAcDept c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                    And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode  And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b, dtAcEmp c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode  And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                        From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                        And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode                        
                        And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv3ChildIndicators = Conn.Select(string.Format(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort, b.IndicatorsCode, a.ProjectsCode Order By a.Sort", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //----------Indicators4
        strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode  
                    From Activity a, Evaluation b 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
            From Activity a, Evaluation b, dtAcDept c 
            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
            And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcEmp c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode                    
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv4ChildIndicators = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, CurrentUser.SchoolID));

        string strStandardName = "'�ҵðҹ��� '";
        if (btc.ckIdentityName("ckStandardNew"))
        {
            strStandardName = "'�����Ϸ�� '";
        }
        /////////////////Standard
        strSql = @" Select b.StandardCode, {1} + Cast(b.Sort As nVarChar) + '. - ' + b.StandardName as StandardName, 
                IsNull(Sum(c.WeightScore), 0) WeightScore, b.Sort, b.ScoreGroupID, b.SideCode 
                From Side a Left Join Standard b On a.SideCode = b.SideCode 
                Left Join Indicators c On b.StandardCode = c.StandardCode 
                Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 
                And a.StudyYear = '{0}' And b.StudyYear = '{0}' 
                Group By b.StandardCode, StandardName, b.Sort, b.ScoreGroupID, b.SideCode 
                Order By b.Sort ";
        dvChildStandard = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, strStandardName));

        //-------------Standard2
        strSql = @" Select b.IndicatorsCode, b.WeightScore, Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + SubString(b.IndicatorsName,1,80) + ' ( ' + Cast(b.WeightScore As nVarChar) + ' )' As IndicatorsName, b.StandardCode 
                 From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '{0}' 
                 Order By a.Sort, b.Sort ";
        dv2ChildStandard = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue));

        //-------------Standard3
        strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcDept c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcEmp c 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                        And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                            And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode 
                            And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv3ChildStandard = Conn.Select(string.Format(strSql + " Group By a.ProjectsCode, b.IndicatorsCode ", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //------------Standard4
        strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                 From Activity a, Evaluation b 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                 And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcDept c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                    From Activity a, Evaluation b, dtAcEmp c 
                    Where a.DelFlag = 0 And b.DelFlag = 0  And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode
                        From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                        And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode 
                        And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv4ChildStandard = Conn.Select(string.Format(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort, b.IndicatorsCode, a.ProjectsCode Order By a.Sort", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //----------Standard5
        strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                 From Activity a, Evaluation b 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                 And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
            From Activity a, Evaluation b, dtAcDept c 
            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
            And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcEmp c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode
                    From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode 
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv5ChildStandard = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, CurrentUser.SchoolID));

        /////////////////Side
        //---------------Side2
        strSql = @" Select b.StandardCode, Cast(b.Sort As nVarChar) + ' - ' + b.StandardName as StandardName, 
                IsNull(Sum(c.WeightScore), 0) WeightScore, b.Sort, b.SideCode 
                From Side a Left Join Standard b On a.SideCode = b.SideCode 
                Left Join Indicators c On b.StandardCode = c.StandardCode 
                Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 
                And a.StudyYear = '{0}' And b.StudyYear = '{0}' 
                Group By b.StandardCode, StandardName, b.Sort, b.SideCode Order By b.Sort ";
        dv2ChildSide = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue));

        //---------------Side3
        strSql = @" Select b.IndicatorsCode, b.WeightScore, Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + SubString(b.IndicatorsName,1,80) + ' ( ' + Cast(b.WeightScore As nVarChar) + ' )' As IndicatorsName, b.StandardCode 
                 From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '{0}' Order By a.Sort, b.Sort ";
        dv3ChildSide = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue));

        //---------------Side4
        strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                 And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcDept c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcEmp c 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                        And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ProjectsCode, b.IndicatorsCode From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                         Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                         And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode 
                         And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv4ChildSide = Conn.Select(string.Format(strSql + " Group By a.ProjectsCode, b.IndicatorsCode ", ddlYearB.SelectedValue, CurrentUser.SchoolID));

        //---------------Side5
        strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode
                 From Activity a, Evaluation b 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                 And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode 
                From Activity a, Evaluation b, dtAcDept c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode  
                    From Activity a, Evaluation b, dtAcEmp c 
                    Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                    And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent, b.IndicatorsCode, a.ProjectsCode  
                        From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                        Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}' 
                        And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode                        
                        And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv5ChildSide = Conn.Select(string.Format(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort, b.IndicatorsCode, a.ProjectsCode Order By a.Sort ", ddlYearB.SelectedValue, CurrentUser.SchoolID));
    
        //-------------Side6
        strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode
                 From Activity a, Evaluation b 
                 Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                 And a.ActivityCode = b.ActivityCode ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode 
            From Activity a, Evaluation b, dtAcDept c 
            Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
            And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode  
                From Activity a, Evaluation b, dtAcEmp c 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode 
                And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'  ";
            }
            else
            {
                strSql = @" Select a.ActivityCode, b.IndicatorsCode, a.ProjectsCode  
                From Activity a, Evaluation b, dtAcEmp c, dtAcDept d 
                Where a.DelFlag = 0 And b.DelFlag = 0 And b.StudyYear = '{0}' And b.SchoolID = '{1}'
                And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode 
                And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        dv6ChildSide = Conn.Select(string.Format(strSql, ddlYearB.SelectedValue, CurrentUser.SchoolID));
    }

    public override void DataBind() //��ҹ
    {
        DataView dv, dv1, dv2, dv3, dv4, dv5;

        string strSql = " Select a.SideCode, Cast(a.Sort As nVarChar) + ' - ' + a.SideName as SideName, "
                + " IsNull(Sum(c.WeightScore), 0) WeightScore, a.Sort, a.ScoreGroupID "
                + " From Side a Left Join Standard b On a.SideCode = b.SideCode "
                + " Left Join Indicators c On b.StandardCode = c.StandardCode "
                + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
                + " And a.StudyYear = '" + ddlYearB.SelectedValue + "' And b.StudyYear = '" + ddlYearB.SelectedValue + "' "
                + " Group By a.SideCode, SideName, a.Sort, a.ScoreGroupID "
                + " Order By a.Sort ";
        dv5 = Conn.Select(strSql);

        BindMainDataView();

        double SummarizeCount = 0;
        double SummarizeAvg = 0;
        string strColor = "black";

        for (int m = 0; m < dv5.Count; m++)
        {
            //strSql = " Select b.StandardCode, Cast(b.Sort As nVarChar) + ' - ' + b.StandardName as StandardName, "
            //    + " IsNull(Sum(c.WeightScore), 0) WeightScore, b.Sort "
            //    + " From Side a Left Join Standard b On a.SideCode = b.SideCode "
            //    + " Left Join Indicators c On b.StandardCode = c.StandardCode "
            //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
            //    + " And a.StudyYear = '" + ddlYearB.SelectedValue + "' And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SideCode = '" + dv5[m]["SideCode"].ToString() + "' "
            //    + " Group By b.StandardCode, StandardName, b.Sort "
            //    + " Order By b.Sort ";
            //dv = Conn.Select(strSql);

            dv2ChildSide.RowFilter = " SideCode = '" + dv5[m]["SideCode"].ToString() + "' ";

            double CkCriterion = 0;
            double stPercent = 0;
            double ckStScore = 0;
            for (int i = 0; i < dv2ChildSide.Count; i++)
            {
                //strSql = " Select b.IndicatorsCode, b.WeightScore, Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + SubString(b.IndicatorsName,1,80) + ' ( ' + Cast(b.WeightScore As nVarChar) + ' )' As IndicatorsName "
                //     + " From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode "
                //     + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlYearB.SelectedValue + "' "
                //     + " And b.StandardCode = '" + dv2ChildSide[i]["StandardCode"].ToString() + "' Order By a.Sort, b.Sort ";
                //dv1 = Conn.Select(strSql);

                dv3ChildSide.RowFilter = " StandardCode = '" + dv2ChildSide[i]["StandardCode"].ToString() + "' ";

                double IndPercent = 0;
                double ckIndScore = 0;
                for (int j = 0; j < dv3ChildSide.Count; j++)
                {
                    //strSql = " Select a.ProjectsCode From Activity a, Evaluation b "
                    //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                    //            + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' "
                    //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                    //if (ddlSearchDept.SelectedIndex != 0)
                    //{
                    //    strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcDept c "
                    //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                    //            + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' "
                    //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //            + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    //}
                    //if (ddlSearchEmp.SelectedIndex != 0)
                    //{
                    //    if (ddlSearchDept.SelectedIndex == 0)
                    //    {
                    //        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c "
                    //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                    //                + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' "
                    //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                    //    }
                    //    else
                    //    {
                    //        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                    //                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                    //                    + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' "
                    //                    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //                    + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    //    }
                    //}
                    //dv2 = Conn.Select(strSql + " Group By a.ProjectsCode ");

                    dv4ChildSide.RowFilter = " IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' ";

                    double PjPercent = 0;
                    for (int k = 0; k < dv4ChildSide.Count; k++)
                    {
                        //strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                        //        + " From Activity a, Evaluation b "
                        //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  "
                        //        + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                        //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                        //if (ddlSearchDept.SelectedIndex != 0)
                        //{
                        //    strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                        //        + " From Activity a, Evaluation b, dtAcDept c "
                        //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                        //        + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                        //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        //        + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                        //}
                        //if (ddlSearchEmp.SelectedIndex != 0)
                        //{
                        //    if (ddlSearchDept.SelectedIndex == 0)
                        //    {
                        //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                        //            + " From Activity a, Evaluation b, dtAcEmp c "
                        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                        //            + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                        //    }
                        //    else
                        //    {
                        //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                        //                + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                        //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                        //                + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                        //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                        //    }
                        //}
                        //dv3 = Conn.Select(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort Order By a.Sort ");

                        dv5ChildSide.RowFilter = " IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' ";

                        double AcPercent = 0;
                        for (int l = 0; l < dv5ChildSide.Count; l++)
                        {
                            //strSql = " Select a.ActivityCode "
                            //    + " From Activity a, Evaluation b "
                            //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                            //    + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                            //    + " And b.ActivityCode = '" + dv5ChildSide[l]["ActivityCode"].ToString() + "' "
                            //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                            //if (ddlSearchDept.SelectedIndex != 0)
                            //{
                            //    strSql = " Select a.ActivityCode "
                            //    + " From Activity a, Evaluation b, dtAcDept c "
                            //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                            //    + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                            //    + " And b.ActivityCode = '" + dv5ChildSide[l]["ActivityCode"].ToString() + "' "
                            //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            //    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                            //}
                            //if (ddlSearchEmp.SelectedIndex != 0)
                            //{
                            //    if (ddlSearchDept.SelectedIndex == 0)
                            //    {
                            //        strSql = " Select a.ActivityCode "
                            //        + " From Activity a, Evaluation b, dtAcEmp c "
                            //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                            //        + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                            //        + " And b.ActivityCode = '" + dv5ChildSide[l]["ActivityCode"].ToString() + "' "
                            //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            //        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                            //    }
                            //    else
                            //    {
                            //        strSql = " Select a.ActivityCode "
                            //        + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                            //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                            //        + " And b.IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' "
                            //        + " And b.ActivityCode = '" + dv5ChildSide[l]["ActivityCode"].ToString() + "' "
                            //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            //        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                            //    }
                            //}
                            //dv4 = Conn.Select(strSql);

                            dv6ChildSide.RowFilter = " IndicatorsCode = '" + dv3ChildSide[j]["IndicatorsCode"].ToString() + "' And ProjectsCode = '" + dv4ChildSide[k]["ProjectsCode"].ToString() + "' And ActivityCode = '" + dv5ChildSide[l]["ActivityCode"].ToString() + "' ";

                            AcPercent += (Convert.ToDouble(dv5ChildSide[l]["CkAPercent"]) * 100) / (Convert.ToDouble(dv6ChildSide.Count) * 100);
                        }

                        PjPercent += (AcPercent * 100) / (Convert.ToDouble(dv5ChildSide.Count) * 100);
                    }

                    if (dv4ChildSide.Count != 0)
                    {
                        IndPercent = (PjPercent * 100) / (Convert.ToDouble(dv4ChildSide.Count) * 100);
                    }
                    else
                    {
                        IndPercent = 0;
                    }

                    double IndScore = 0;
                    IndScore = (IndPercent * Convert.ToDouble(dv3ChildSide[j]["WeightScore"])) / 100;

                    //string[] IndCriterion = new BTC().ckTCriteria(Convert.ToDouble(dv1[j]["WeightScore"]), IndScore).Split(':');
                    //ckIndScore += Convert.ToDouble(IndCriterion[0]);

                    //�礡�û����Թ�ԧ�س�Ҿ
                    IndScore = btc.getQuality(dv3ChildSide[j]["IndicatorsCode"].ToString(), IndScore);

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

            string[] Tcriterion = new BTC().ckTCriteria(dv5[m]["ScoreGroupID"].ToString(), hScore).Split(':');
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

            TreeNode tNodeSide = new TreeNode();
            //tNode.Text = string.Format("<span style=\"float:left;color:black;font-size:12pt; font-weight:bold;\">{0}</span><span style=\"float:Right;color:#303895;font-size:10pt; font-weight:bold;\">{5}, {4}%, {3}, {2}, {1}</span>", dv[i]["StandardName"].ToString(), dv1[0]["LevelScore"].ToString(), dv1[0]["Weight"].ToString(), dv2[0]["ct"].ToString(), avg, grade);
            //tNodeSide.Text = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "icon1g" : "icon1r") + ".png\" style=\"float:left;border:none;\"/>&nbsp;&nbsp;<span style=\"color:Gray;font-size:12pt;font-weight:bold;width:630px;overflow:hidden;\" title=\"{5}\">{0}&nbsp;&nbsp;&nbsp;(��ṹ��� {4})</span><span style=\"width:70px; text-align:right ;float:right; margin:0 80px 0 0;color:" + strColor + ";font-size:12pt; font-weight:bold;\">{2}</span><span style=\"width:50px; text-align:right; float:Right; padding:0 50px 0 0; color:" + strColor + "; font-size:12pt; font-weight:bold;\">{1}%</span><span style=\"float:right; padding:0 50px 0 0; color:" + strColor + "; font-size:12pt; font-weight:bold;\">{3}</span><p style=\"clear:both; float:none;\"></p><hr />", (dv5[m]["SideName"].ToString().Length > 77 ? dv5[m]["SideName"].ToString().Substring(0, 77) + "..." : dv5[m]["SideName"]), ckPercen.ToString("#,##0.00"), ckScore + " (" + ckTranslation + ") ", " " + hScore.ToString("#,##0.00") + " ", dv5[m]["WeightScore"], dv5[m]["SideName"]);
            tNodeSide.Text = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "icon1g" : "icon1r") + ".png\" style=\"float:left;border:none;padding:0 0 0 20px;\"/>&nbsp;&nbsp;<span style=\"color:" + ((rbtlView1.Checked) ? "#424242" : strColor) + ";font-size:large;font-weight:bold;width:630px;overflow:hidden;\" title=\"{5}\">{0}</span><span style=\"width:100px; text-align:right ;float:right; margin:0 20px 0 0;color:" + strColor + ";font-size:large; font-weight:bold;\">{2}</span><span style=\"width:30px; text-align:right; float:Right; padding:0 70px 0 0; color:" + strColor + "; font-size:large; font-weight:bold;\">{1}</span><span style=\"float:right; text-align:right; width:70px; padding:0 90px 0 0; color:" + strColor + "; font-size:large; font-weight:bold;\">{3}</span><span style=\"float:right; padding:0 110px 0 0; color:#000000; font-size:large; font-weight:bold;\">{4}</span><p style=\"clear:both; float:none;\"></p><hr />", (dv5[m]["SideName"].ToString().Length > btc.strCut1 ? dv5[m]["SideName"].ToString().Substring(0, btc.strCut1) + "..." : dv5[m]["SideName"]), ckScore, ckTranslation, " " + hScore.ToString("#,##0.00") + " ", dv5[m]["WeightScore"], dv5[m]["SideName"]);
            //tNode.Text = dv[i]["StandardName"].ToString();
            tNodeSide.Value = dv5[m]["SideCode"].ToString();
            AddChildStandard(tNodeSide);
            TreeView1.Nodes.Add(tNodeSide);

        }
        TreeView1.CollapseAll();
        TreeView1.NodeStyle.Width = new Unit("100%");

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

        string strSideName = "��ҹ";
        if (btc.ckIdentityName("ckStandardNew"))
        {
            strSideName = "�ҵðҹ";
        }
        //tNodeStandard.Text = string.Format("<span style=\"float:left;color:#484848;font-size:10pt;font-weight:bold;width:650px;overflow:hidden;\">{0}</span><span style=\"width:50px; text-align:right ;float:right; padding:0 20px 0 0;color:" + strColor + ";font-size:10pt; font-weight:bold;\">{2}</span><span style=\"width:50px; text-align:right; float:Right; padding:0 50px 0 0; color:" + strColor + "; font-size:10pt; font-weight:bold;\">{1}%</span><span style=\"float:right; padding:0 50px 0 0; color:" + strColor + "; font-size:10pt; font-weight:bold;\">{3}</span>", dv[i]["StandardName"].ToString(), ckPercen.ToString("#,##0.00"), ckTranslation + " (" + ckScore + ") ", " (" + hScore.ToString("#,##0.00") + ") ");
        //lblSummarize.Text = string.Format("<img src=\"../Image/icon/" + ((ckScoreTotal > 1) ? "icon0g" : "icon0r") + ".png\" style=\"float:left;border:none;\"/>&nbsp;<span style=\"color:black;font-size:medium;font-weight:bold;width:600px;overflow:hidden;\" title=\"����ء�ҵðҹ\">{0}</span><span style=\"width:100px; text-align:right; float:right; margin:0 70px 0 0; color:" + strColor + "; font-size:13pt; font-weight:bold;\">{2}</span><span style=\"width:50px; text-align:right;float:Right; padding:0 35px 0 0; color:" + strColor + "; font-size:medium; font-weight:bold;\">{1}</span><span style=\"float:right; padding:0 10px 0 0;color:" + strColor + ";font-size:medium; font-weight:bold;\">{3}</span><p style=\"clear:both; float:none;\"></p><hr /><br />", "����ء��ҹ&nbsp;&nbsp;&nbsp;(��ṹ��� 100)", SummarizeAvg.ToString("#,##0.00"), ckScoreTotal + " (" + ckTranslationTotal + ")", " " + hScoreTotal.ToString("#,##0.00") + " ");
        lblSummarize.Text = string.Format("<img src=\"../Image/icon/" + ((ckScoreTotal > 1) ? "ballg" : "ballr") + ".png\" style=\"float:left;border:none;width:25px;height:28px;\"/>&nbsp;&nbsp;<span style=\"color:" + ((rbtlView1.Checked) ? "#000000" : strColor) + ";font-size:x-large;font-weight:bold;width:600px;overflow:hidden;\" title=\"������ " + dv5.Count.ToString("0") + " " + strSideName + "\">{0}</span><span style=\"width:100px; text-align:right; float:right; margin:0 20px 0 0; color:" + strColor + "; font-size:x-large; font-weight:bold;\">{2}</span><span style=\"width:30px; text-align:right;float:Right; padding:0 50px 0 0; color:" + strColor + "; font-size:x-large; font-weight:bold;\">{1}</span><span style=\"float:right; width:65px; padding:0 65px 0 0;color:" + strColor + ";font-size:x-large; font-weight:bold;\">{3}</span><span style=\"float:right; padding:0 125px 0 0; color:#000000; font-size:x-large; font-weight:bold;\">100</span><p style=\"clear:both; float:none;\"></p><hr /><br />", "������ " + dv5.Count.ToString("0") + " " + strSideName + "", ckScoreTotal, ckTranslationTotal, " " + hScoreTotal.ToString("#,##0.00") + " ");
    }

    private void AddChildStandard(TreeNode tNodeSide)  //�ҵðҹ
    {
        DataView dv, dv1, dv2, dv3, dv4;

        //string strSql = " Select b.StandardCode, '�ҵðҹ��� ' + Cast(b.Sort As nVarChar) + '. - ' + b.StandardName as StandardName, "
        //        + " IsNull(Sum(c.WeightScore), 0) WeightScore, b.Sort, b.ScoreGroupID "
        //        + " From Side a Left Join Standard b On a.SideCode = b.SideCode "
        //        + " Left Join Indicators c On b.StandardCode = c.StandardCode "
        //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
        //        + " And a.StudyYear = '" + ddlYearB.SelectedValue + "'And b.StudyYear = '" + ddlYearB.SelectedValue + "' "
        //        + " And b.SideCode = '" + tNodeSide.Value + "' "
        //        + " Group By b.StandardCode, StandardName, b.Sort, b.ScoreGroupID "
        //        + " Order By b.Sort ";
        //dv = Conn.Select(strSql);

        dvChildStandard.RowFilter = " SideCode = '" + tNodeSide.Value + "' ";

        double CkCriterion = 0;
        string strColor = "black";
        double SummarizeAvg = 0;
        double SummarizeCount = 0;

        for (int i = 0; i < dvChildStandard.Count; i++)
        {
            //string strSql = " Select b.IndicatorsCode, b.WeightScore, Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + SubString(b.IndicatorsName,1,80) + ' ( ' + Cast(b.WeightScore As nVarChar) + ' )' As IndicatorsName "
            //     + " From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode "
            //     + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlYearB.SelectedValue + "' "
            //     + " And b.StandardCode = '" + dvChildStandard[i]["StandardCode"].ToString() + "' Order By a.Sort, b.Sort ";
            //dv1 = Conn.Select(strSql);

            dv2ChildStandard.RowFilter = " StandardCode = '" + dvChildStandard[i]["StandardCode"].ToString() + "' ";

            double IndPercent = 0;
            double ckIndScore = 0;
            for (int j = 0; j < dv2ChildStandard.Count; j++)
            {
                //string strSql = " Select a.ProjectsCode From Activity a, Evaluation b "
                //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                //            + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' "
                //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                //if (ddlSearchDept.SelectedIndex != 0)
                //{
                //    strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcDept c "
                //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                //            + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' "
                //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //            + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                //}
                //if (ddlSearchEmp.SelectedIndex != 0)
                //{
                //    if (ddlSearchDept.SelectedIndex == 0)
                //    {
                //        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c "
                //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                //                + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' "
                //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                //    }
                //    else
                //    {
                //        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                //                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                //                    + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' "
                //                    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //                    + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                //    }
                //}
                //dv2 = Conn.Select(strSql + " Group By a.ProjectsCode ");

                dv3ChildStandard.RowFilter = " IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' ";

                double PjPercent = 0;
                for (int k = 0; k < dv3ChildStandard.Count; k++)
                {
                    //string strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                    //        + " From Activity a, Evaluation b "
                    //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  "
                    //        + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                    //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                    //if (ddlSearchDept.SelectedIndex != 0)
                    //{
                    //    strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                    //        + " From Activity a, Evaluation b, dtAcDept c "
                    //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                    //        + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                    //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //        + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    //}
                    //if (ddlSearchEmp.SelectedIndex != 0)
                    //{
                    //    if (ddlSearchDept.SelectedIndex == 0)
                    //    {
                    //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                    //            + " From Activity a, Evaluation b, dtAcEmp c "
                    //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                    //            + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                    //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                    //    }
                    //    else
                    //    {
                    //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                    //                + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                    //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                    //                + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                    //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    //    }
                    //}
                    //dv3 = Conn.Select(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort Order By a.Sort");

                    dv4ChildStandard.RowFilter = " IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' ";

                    double AcPercent = 0;
                    for (int l = 0; l < dv4ChildStandard.Count; l++)
                    {
                        //string strSql = " Select a.ActivityCode "
                        //    + " From Activity a, Evaluation b "
                        //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                        //    + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                        //    + " And b.ActivityCode = '" + dv4ChildStandard[l]["ActivityCode"].ToString() + "' "
                        //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                        //if (ddlSearchDept.SelectedIndex != 0)
                        //{
                        //    strSql = " Select a.ActivityCode "
                        //    + " From Activity a, Evaluation b, dtAcDept c "
                        //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                        //    + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                        //    + " And b.ActivityCode = '" + dv4ChildStandard[l]["ActivityCode"].ToString() + "' "
                        //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        //    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                        //}
                        //if (ddlSearchEmp.SelectedIndex != 0)
                        //{
                        //    if (ddlSearchDept.SelectedIndex == 0)
                        //    {
                        //        strSql = " Select a.ActivityCode "
                        //        + " From Activity a, Evaluation b, dtAcEmp c "
                        //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                        //        + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                        //        + " And b.ActivityCode = '" + dv4ChildStandard[l]["ActivityCode"].ToString() + "' "
                        //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        //        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                        //    }
                        //    else
                        //    {
                        //        strSql = " Select a.ActivityCode "
                        //            + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                        //            + " And b.IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' "
                        //            + " And b.ActivityCode = '" + dv4ChildStandard[l]["ActivityCode"].ToString() + "' "
                        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                        //    }
                        //}
                        //dv4 = Conn.Select(strSql);

                        dv5ChildStandard.RowFilter = " IndicatorsCode = '" + dv2ChildStandard[j]["IndicatorsCode"].ToString() + "' And ProjectsCode = '" + dv3ChildStandard[k]["ProjectsCode"].ToString() + "' And ActivityCode = '" + dv4ChildStandard[l]["ActivityCode"].ToString() + "' ";

                        AcPercent += (Convert.ToDouble(dv4ChildStandard[l]["CkAPercent"]) * 100) / (Convert.ToDouble(dv5ChildStandard.Count) * 100);
                    }

                    PjPercent += (AcPercent * 100) / (Convert.ToDouble(dv4ChildStandard.Count) * 100);
                }

                if (dv3ChildStandard.Count != 0)
                {
                    IndPercent = (PjPercent * 100) / (Convert.ToDouble(dv3ChildStandard.Count) * 100);
                }
                else
                {
                    IndPercent = 0;
                }

                double IndScore = 0;
                IndScore = (IndPercent * Convert.ToDouble(dv2ChildStandard[j]["WeightScore"])) / 100;

                //�礡�û����Թ�ԧ�س�Ҿ
                IndScore = btc.getQuality(dv2ChildStandard[j]["IndicatorsCode"].ToString(), IndScore);

                //string[] IndCriterion = new BTC().ckTCriteria(Convert.ToDouble(dv1[j]["WeightScore"]), IndScore).Split(':');
                //ckIndScore += Convert.ToDouble(IndCriterion[0]);

                ckIndScore += Convert.ToDouble(IndScore.ToString("#0.00"));
            }

            double hScore = 0;
            //hScore = (ckIndScore / Convert.ToDouble(dv1.Count));
            hScore = ckIndScore;

            string[] Tcriterion = new BTC().ckTCriteria(dvChildStandard[i]["ScoreGroupID"].ToString(), hScore).Split(':');
            double ckScore = Convert.ToDouble(Tcriterion[0]);
            string ckTranslation = Tcriterion[1].ToString();

            //strColor = btc.getColorMaster(hScore, Convert.ToDouble(dv[i]["WeightScore"]));
            if (ckScore > 1)
            {
                strColor = "#0B610B";
            }
            else
            {
                strColor = "Red";
            }

            double ckPercen = (hScore * 100) / Convert.ToDouble(dvChildStandard[i]["WeightScore"]);

            TreeNode tNodeStandard = new TreeNode();
            //tNode.Text = string.Format("<span style=\"float:left;color:black;font-size:12pt; font-weight:bold;\">{0}</span><span style=\"float:Right;color:#303895;font-size:10pt; font-weight:bold;\">{5}, {4}%, {3}, {2}, {1}</span>", dv[i]["StandardName"].ToString(), dv1[0]["LevelScore"].ToString(), dv1[0]["Weight"].ToString(), dv2[0]["ct"].ToString(), avg, grade);
            //tNodeStandard.Text = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "icon2g" : "icon2r") + ".png\" style=\"float:left;border:none;\"/>&nbsp;&nbsp;<span style=\"color:#484848;font-size:medium;font-weight:bold;width:610px;overflow:hidden;\" title=\"{5}\">{0}&nbsp;&nbsp;&nbsp;(��ṹ��� {4})</span><span style=\"width:100px; text-align:right ;float:right; margin:0 80px 0 0;color:" + strColor + ";font-size:medium; font-weight:bold;\">{2}</span><span style=\"width:50px; text-align:right; float:Right; padding:0 50px 0 0; color:" + strColor + "; font-size:medium; font-weight:bold;\">{1}%</span><span style=\"float:right; padding:0 50px 0 0; color:" + strColor + "; font-size:medium; font-weight:bold;\">{3}</span><p style=\"clear:both; float:none;\"></p><hr />", (dv[i]["StandardName"].ToString().Length > 77 ? dv[i]["StandardName"].ToString().Substring(0, 65) + "..." : dv[i]["StandardName"]), ckPercen.ToString("#,##0.00"), ckScore + " (" + ckTranslation + ") ", " " + hScore.ToString("#,##0.00") + " ", dv[i]["WeightScore"], dv[i]["StandardName"]);
            tNodeStandard.Text = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "icon2g" : "icon2r") + ".png\" style=\"float:left;border:none;padding:0 0 0 20px;\"/>&nbsp;&nbsp;<span style=\"color:" + ((rbtlView1.Checked) ? "#08088A" : strColor) + ";font-size:medium;font-weight:bold;width:610px;overflow:hidden;\" title=\"{5}\">{0}</span><span style=\"width:100px; text-align:right ;float:right; margin:0 20px 0 0;color:" + strColor + ";font-size:12pt; font-weight:bold;\">{2}</span><span style=\"width:30px; text-align:right; float:Right; padding:0 50px 0 0; color:" + strColor + "; font-size:12pt; font-weight:bold;\">{1}</span><span style=\"float:right; width:60px; padding:0 60px 0 0; color:" + strColor + "; font-size:12pt; font-weight:bold;\">{3}</span><span style=\"float:right; padding:0 150px 0 0; color:#000000; font-size:12pt; font-weight:bold;\">{4}</span><p style=\"clear:both; float:none;\"></p><hr />", (dvChildStandard[i]["StandardName"].ToString().Length > btc.strCut2 ? dvChildStandard[i]["StandardName"].ToString().Substring(0, btc.strCut2) + "..." : dvChildStandard[i]["StandardName"]), ckScore, ckTranslation, " " + hScore.ToString("#,##0.00") + " ", dvChildStandard[i]["WeightScore"], dvChildStandard[i]["StandardName"]);

            tNodeStandard.Value = dvChildStandard[i]["StandardCode"].ToString();
            AddChildIndicators(tNodeStandard);
            tNodeSide.ChildNodes.Add(tNodeStandard);

            SummarizeCount += ckScore;
        }
        //TreeView1.CollapseAll();
        //TreeView1.NodeStyle.Width = new Unit("100%");
    }

    private void AddChildIndicators(TreeNode tNodeStandard)  //��Ǻ觪��
    {
        DataView dv, dv1, dv2, dv3;

        //string strSql = " Select b.IndicatorsCode, b.WeightScore, '��Ǻ觪���� ' + Cast(a.Sort As nVarChar) + '.' "
        //            + " + Cast(b.Sort As nVarChar) + ' - ' + b.IndicatorsName As IndicatorsName, b.ScoreGroupID "
        //            + " From Standard a Left Join Indicators b On a.StandardCode = b.StandardCode "
        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlYearB.SelectedValue + "' "
        //            + " And b.StandardCode = '" + tNodeStandard.Value + "' Order By a.Sort, b.Sort ";
        //dv = Conn.Select(strSql);

        dvChildIndicators.RowFilter = " StandardCode = '" + tNodeStandard.Value + "' ";

        double CkCriterion = 0;
        string strColor = "#2b8216";
        for (int i = 0; i < dvChildIndicators.Count; i++)
        {
            //string strSql = " Select a.ProjectsCode From Activity a, Evaluation b "
            //                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
            //                    + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' "
            //                    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
            //if (ddlSearchDept.SelectedIndex != 0)
            //{
            //    strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcDept c "
            //                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
            //                    + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' "
            //                    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //                    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            //}
            //if (ddlSearchEmp.SelectedIndex != 0)
            //{
            //    if (ddlSearchDept.SelectedIndex == 0)
            //    {
            //        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c "
            //                        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
            //                        + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' "
            //                        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //                        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
            //    }
            //    else
            //    {
            //        strSql = " Select a.ProjectsCode From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
            //                        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
            //                        + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' "
            //                        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //                        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            //    }
            //}
            //dv1 = Conn.Select(strSql + " Group By a.ProjectsCode ");

            dv2ChildIndicators.RowFilter = " IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' ";

            double PjPercent = 0;
            for (int j = 0; j < dv2ChildIndicators.Count; j++)
            {
                //string strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                //        + " From Activity a, Evaluation b "
                //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  "
                //        + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                //if (ddlSearchDept.SelectedIndex != 0)
                //{
                //    strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                //        + " From Activity a, Evaluation b, dtAcDept c "
                //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  And a.ActivityCode = c.ActivityCode "
                //        + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //        + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                //}
                //if (ddlSearchEmp.SelectedIndex != 0)
                //{
                //    if (ddlSearchDept.SelectedIndex == 0)
                //    {
                //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                //            + " From Activity a, Evaluation b, dtAcEmp c "
                //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  And a.ActivityCode = c.ActivityCode "
                //            + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                //    }
                //    else
                //    {
                //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
                //                + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                //                + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                //    }
                //}
                //dv2 = Conn.Select(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort Order By a.Sort");

                dv3ChildIndicators.RowFilter = " IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "'";

                double AcPercent = 0;
                for (int k = 0; k < dv3ChildIndicators.Count; k++)
                {
                    //string strSql = " Select a.ActivityCode "
                    //    + " From Activity a, Evaluation b "
                    //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                    //    + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                    //    + " And b.ActivityCode = '" + dv3ChildIndicators[k]["ActivityCode"].ToString() + "' "
                    //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                    //if (ddlSearchDept.SelectedIndex != 0)
                    //{
                    //    strSql = " Select a.ActivityCode "
                    //    + " From Activity a, Evaluation b, dtAcDept c "
                    //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                    //    + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                    //    + " And b.ActivityCode = '" + dv3ChildIndicators[k]["ActivityCode"].ToString() + "' "
                    //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    //}
                    //if (ddlSearchEmp.SelectedIndex != 0)
                    //{
                    //    if (ddlSearchDept.SelectedIndex == 0)
                    //    {
                    //        strSql = " Select a.ActivityCode "
                    //        + " From Activity a, Evaluation b, dtAcEmp c "
                    //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                    //        + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                    //        + " And b.ActivityCode = '" + dv3ChildIndicators[k]["ActivityCode"].ToString() + "' "
                    //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                    //    }
                    //    else
                    //    {
                    //        strSql = " Select a.ActivityCode "
                    //            + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                    //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                    //            + " And b.IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And a.ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' "
                    //            + " And b.ActivityCode = '" + dv3ChildIndicators[k]["ActivityCode"].ToString() + "' "
                    //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                    //    }
                    //}
                    //dv3 = Conn.Select(strSql);
                    dv4ChildIndicators.RowFilter = " IndicatorsCode = '" + dvChildIndicators[i]["IndicatorsCode"].ToString() + "' And ProjectsCode = '" + dv2ChildIndicators[j]["ProjectsCode"].ToString() + "' And ActivityCode = '" + dv3ChildIndicators[k]["ActivityCode"].ToString() + "' ";

                    AcPercent += (Convert.ToDouble(dv3ChildIndicators[k]["CkAPercent"]) * 100) / (Convert.ToDouble(dv4ChildIndicators.Count) * 100);
                }

                PjPercent += (AcPercent * 100) / (Convert.ToDouble(dv3ChildIndicators.Count) * 100);
            }

            if (dv2ChildIndicators.Count != 0)
            {
                CkCriterion = (PjPercent * 100) / (Convert.ToDouble(dv2ChildIndicators.Count) * 100);
            }
            else
            {
                CkCriterion = 0;
            }

            double avg = 0;
            avg = CkCriterion;

            double hScore = 0;
            hScore = (avg * Convert.ToDouble(dvChildIndicators[i]["WeightScore"])) / 100;

            //�礡�û����Թ�ԧ�س�Ҿ
            hScore = btc.getQuality(dvChildIndicators[i]["IndicatorsCode"].ToString(), hScore);

            string[] Tcriterion = new BTC().ckTCriteria(dvChildIndicators[i]["ScoreGroupID"].ToString(), hScore).Split(':');
            double ckScore = Convert.ToDouble(Tcriterion[0]);
            string ckTranslation = Tcriterion[1].ToString();

            //strColor = btc.getColorMaster(hScore, Convert.ToDouble(dv[i]["WeightScore"]));
            if (ckScore > 1)
            {
                strColor = "#0B610B";
            }
            else
            {
                strColor = "Red";
            }

            TreeNode tNodeIndicators = new TreeNode();
            //tNode.Text = dv[i]["IndicatorsName"].ToString();
            //tNode.Text = string.Format("<span style=\"float:left;color:#303895;font-size:10pt; font-weight:bold;\">{0}</span><span style=\"float:Right;color:#303895;font-size:10pt; font-weight:bold;\">{5}, {4}%, {3}, {2}, {1}</span>", dv[i]["IndicatorsName"].ToString(), dv1[0]["LevelScore"].ToString(), dv1[0]["Weight"].ToString(), dv2[0]["ct"].ToString(), avg, grade);
            //tNodeIndicators.Text = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "icon3g" : "icon3r") + ".png\" style=\"float:left;border:none;\"/>&nbsp;&nbsp;<span style=\"color:#303895;font-size:10pt;font-weight:bold;width:580px;overflow:hidden;\" title=\"{5}\">{0}&nbsp;&nbsp;&nbsp;(��ṹ��� {4})</span><span style=\"width:70px; text-align:right; float:right; margin:0 80px 0 0; color:" + strColor + "; font-size:10pt;\">{2}</span><span style=\"width:50px; text-align:right; float:Right; padding:0 50px 0 0; color:" + strColor + "; font-size:10pt;\">{1}%</span><span style=\"float:right; padding:0 50px 0 0; color:" + strColor + "; font-size:10pt; \">{3}</span><p style=\"clear:both; float:none;\"></p><hr />", (dv[i]["IndicatorsName"].ToString().Length > 77 ? dv[i]["IndicatorsName"].ToString().Substring(0, 77) + "..." : dv[i]["IndicatorsName"].ToString()), avg.ToString("#,##0.00"), ckScore + " (" + ckTranslation + ") ", " " + hScore.ToString("#,##0.00") + " ", dv[i]["WeightScore"], dv[i]["IndicatorsName"]);
            tNodeIndicators.Text = string.Format("<img src=\"../Image/icon/" + ((ckScore > 1) ? "icon3g" : "icon3r") + ".png\" style=\"float:left;border:none;padding:0 0 0 25px;\"/>&nbsp;&nbsp;<span style=\"color:" + ((rbtlView1.Checked) ? "#045FB4" : strColor) + ";font-size:medium;font-weight:bold;width:580px;overflow:hidden;\" title=\"{5}\">{0}</span><span style=\"width:100px; text-align:right; float:right; margin:0 20px 0 0; color:" + strColor + "; font-size:12pt;\">{2}</span><span style=\"width:30px; text-align:right; float:Right; padding:0 35px 0 0; color:" + strColor + "; font-size:12pt; font-weight:bold;\">{1}</span><span style=\"float:right; width:52px; padding:0 52px 0 0; color:" + strColor + "; font-size:12pt; font-weight:bold;\">{3}</span><span style=\"float:right; padding:0 150px 0 0; color:#000000; font-size:12pt; font-weight:bold;\">{4}</span><p style=\"clear:both; float:none;\"></p><hr />", (dvChildIndicators[i]["IndicatorsName"].ToString().Length > btc.strCut2 ? dvChildIndicators[i]["IndicatorsName"].ToString().Substring(0, btc.strCut2) + "..." : dvChildIndicators[i]["IndicatorsName"].ToString()), ckScore, ckTranslation, " " + hScore.ToString("#,##0.00") + " ", dvChildIndicators[i]["WeightScore"], dvChildIndicators[i]["IndicatorsName"]);

            tNodeIndicators.Value = dvChildIndicators[i]["IndicatorsCode"].ToString();
            AddChildProjects(tNodeIndicators);
            tNodeStandard.ChildNodes.Add(tNodeIndicators);
        }
    }
    private void AddChildProjects(TreeNode tNodeIndicators) //�ç���
    {
        DataView dv, dv1, dv2;

        //string strSql = " Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort "
        //            + " From Projects a, Evaluation b, Activity c "
        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
        //            + " And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode "
        //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' "
        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
        //if (ddlSearchDept.SelectedIndex != 0)
        //{
        //    strSql = " Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort "
        //            + " From Projects a, Evaluation b, Activity c, dtAcDept d "
        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
        //            + " And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode And c.ActivityCode = d.ActivityCode "
        //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' "
        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //            + " And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
        //}
        //if (ddlSearchEmp.SelectedIndex != 0)
        //{
        //    if (ddlSearchDept.SelectedIndex == 0)
        //    {
        //        strSql = " Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort "
        //                + " From Projects a, Evaluation b, Activity c, dtAcEmp d "
        //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
        //                + " And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode And c.ActivityCode = d.ActivityCode "
        //                + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' "
        //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //                + " And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
        //    }
        //    else
        //    {
        //        strSql = " Select a.ProjectsCode, '�ç��� - ' + a.ProjectsName ProjectsName, a.Sort "
        //                + " From Projects a, Evaluation b, Activity c, dtAcEmp d, dtAcDept e "
        //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 "
        //                + " And b.ActivityCode = c.ActivityCode And c.ProjectsCode = a.ProjectsCode And c.ActivityCode = d.ActivityCode And c.ActivityCode = e.ActivityCode "
        //                + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' "
        //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //                + " And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And e.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
        //    }
        //}
        //dv = Conn.Select(strSql + " Group By a.ProjectsCode, a.ProjectsName, a.Sort Order By a.Sort ");

        dvChildProjects.RowFilter = " IndicatorsCode = '" + tNodeIndicators.Value + "' ";

        double CkAPercent = 0;
        string strColor = "#2b8216";
        string strEva;
        for (int i = 0; i < dvChildProjects.Count; i++)
        {
            CkAPercent = 0;
            //string strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
            //        + " From Activity a, Evaluation b "
            //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode  "
            //        + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
            //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
            //if (ddlSearchDept.SelectedIndex != 0)
            //{
            //    strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
            //        + " From Activity a, Evaluation b, dtAcDept c "
            //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
            //        + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
            //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //        + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            //}
            //if (ddlSearchEmp.SelectedIndex != 0)
            //{
            //    if (ddlSearchDept.SelectedIndex == 0)
            //    {
            //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
            //            + " From Activity a, Evaluation b, dtAcEmp c "
            //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
            //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
            //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
            //    }
            //    else
            //    {
            //        strSql = " Select a.ActivityCode, a.ActivityName, a.Sort, Sum(b.APercent) CkAPercent "
            //                + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
            //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
            //                + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
            //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            //    }
            //}
            //dv1 = Conn.Select(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort Order By a.Sort");

            dv2ChildProjects.RowFilter = " IndicatorsCode = '" + tNodeIndicators.Value + "' And ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' ";

            double AcPercent = 0;
            for (int j = 0; j < dv2ChildProjects.Count; j++)
            {
                //string strSql = " Select a.ActivityCode "
                //    + " From Activity a, Evaluation b "
                //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
                //    + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
                //    + " And b.ActivityCode = '" + dv2ChildProjects[j]["ActivityCode"].ToString() + "' "
                //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
                //if (ddlSearchDept.SelectedIndex != 0)
                //{
                //    strSql = " Select a.ActivityCode "
                //    + " From Activity a, Evaluation b, dtAcDept c "
                //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                //    + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
                //    + " And b.ActivityCode = '" + dv2ChildProjects[j]["ActivityCode"].ToString() + "' "
                //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                //}
                //if (ddlSearchEmp.SelectedIndex != 0)
                //{
                //    if (ddlSearchDept.SelectedIndex == 0)
                //    {
                //        strSql = " Select a.ActivityCode "
                //        + " From Activity a, Evaluation b, dtAcEmp c "
                //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
                //        + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
                //        + " And b.ActivityCode = '" + dv2ChildProjects[j]["ActivityCode"].ToString() + "' "
                //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
                //    }
                //    else
                //    {
                //        strSql = " Select a.ActivityCode "
                //            + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
                //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
                //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' "
                //            + " And b.ActivityCode = '" + dv2ChildProjects[j]["ActivityCode"].ToString() + "' "
                //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
                //    }
                //}
                //dv2 = Conn.Select(strSql);
                dv3ChildProjects.RowFilter = " IndicatorsCode = '" + tNodeIndicators.Value + "' And ProjectsCode = '" + dvChildProjects[i]["ProjectsCode"].ToString() + "' And ActivityCode = '" + dv2ChildProjects[j]["ActivityCode"].ToString() + "' ";

                AcPercent += (Convert.ToDouble(dv2ChildProjects[j]["CkAPercent"]) * 100) / (Convert.ToDouble(dv3ChildProjects.Count) * 100);
            }

            CkAPercent = (AcPercent * 100) / (Convert.ToDouble(dv2ChildProjects.Count) * 100);

            if (CkAPercent < 50)
            {
                strColor = "Red";// "#f87373";
                strEva = "(����ҹ)";
            }
            else
            {
                strColor = "#0B610B";
                strEva = "(��ҹ)";
            }

            TreeNode tNodeProjects = new TreeNode();
            //tNode.Text = string.Format("<span style=\"float:left;\">{0}</span><span style=\"float:Right;\">{2}</span><a href=\"javascript:deleteItem('{1}');\" style=\"float:right;\" ><img style=\"border: 0; cursor: pointer;\" alt=\"ź\" src=\"../Image/delete.gif\" /></a><a href=\"Budget_ExpenseType.aspx?mode=2&id={1}\" style=\"float:right;\" ><img style=\"border: 0; cursor: pointer;\" alt=\"���\" src=\"../Image/edit.gif\" /></a>", dv[i]["ActivityName"].ToString(), dv[i]["ActivityCode"].ToString(), dv[i]["LevelScore"].ToString());
            //tNode.Text = string.Format("<span style=\"float:left;color:#2b8216;\">{0}</span><span style=\"float:Right;color:#2b8216;\">{5}, {4}%, {3}, {2}, {1}</span>", dv[i]["ActivityName"].ToString(), dv[i]["LevelScore"].ToString(), dv[i]["Weight"].ToString(), dv2[0]["ct"].ToString(), avg, grade);
            tNodeProjects.Text = string.Format("<img src=\"../Image/icon/" + ((CkAPercent >= 50) ? "icon4g" : "icon4r") + ".gif\" style=\"float:left;border:none;padding:0 0 0 30px;\"/>&nbsp;&nbsp;<span style=\"font-size:medium;color:" + ((rbtlView1.Checked) ? "#5E610B" : strColor) + ";\">{0}</span>&nbsp;&nbsp;&nbsp;<span style=\"font-size:medium;color:" + strColor + ";\">{2}%</span>&nbsp;&nbsp;&nbsp;<span style=\"font-size:medium;color:" + strColor + ";\">{1}</span>", dvChildProjects[i]["ProjectsName"].ToString(), strEva, CkAPercent.ToString("#,##0.00"));
            tNodeProjects.Value = dvChildProjects[i]["ProjectsCode"].ToString();
            AddChildActivity(tNodeProjects, tNodeIndicators);
            tNodeIndicators.ChildNodes.Add(tNodeProjects);
        }
    }
    private void AddChildActivity(TreeNode tNodeProjects, TreeNode tNodeIndicators) //�Ԩ����
    {
        DataView dv, dv1;

        //string strSql = " Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName "
        //            + " From Activity a, Evaluation b "
        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
        //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
        //if (ddlSearchDept.SelectedIndex != 0)
        //{
        //    strSql = " Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName "
        //            + " From Activity a, Evaluation b, dtAcDept c "
        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode  "
        //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //            + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
        //}
        //if (ddlSearchEmp.SelectedIndex != 0)
        //{
        //    if (ddlSearchDept.SelectedIndex == 0)
        //    {
        //        strSql = " Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName "
        //                + " From Activity a, Evaluation b, dtAcEmp c "
        //                + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode  "
        //                + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
        //                + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
        //    }
        //    else
        //    {
        //        strSql = " Select a.ActivityCode, '�Ԩ���� - ' + a.ActivityName ActivityName, a.Sort, Sum(b.APercent) CkAPercent, '' DeptName "
        //                    + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
        //                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
        //                    + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
        //                    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //                    + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
        //    }
        //}
        //dv = Conn.Select(strSql + " Group By a.ActivityCode, a.ActivityName, a.Sort Order By a.Sort");

        //for (int j = 0; j < dv.Count; j++)
        //{
        //    dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["ActivityCode"].ToString());
        //}

        dvChildActivity.RowFilter = " IndicatorsCode = '" + tNodeIndicators.Value + "' And ProjectsCode = '" + tNodeProjects.Value + "' ";

        double CkAPercent;
        string strColor;
        string strEva = "";
        for (int i = 0; i < dvChildActivity.Count; i++)
        {
            //string strSql = " Select a.ActivityCode "
            //    + " From Activity a, Evaluation b "
            //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode "
            //    + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
            //    + " And b.ActivityCode = '" + dvChildActivity[i]["ActivityCode"].ToString() + "' "
            //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
            //if (ddlSearchDept.SelectedIndex != 0)
            //{
            //    strSql = " Select a.ActivityCode "
            //    + " From Activity a, Evaluation b, dtAcDept c "
            //    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
            //    + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
            //    + " And b.ActivityCode = '" + dvChildActivity[i]["ActivityCode"].ToString() + "' "
            //    + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            //}
            //if (ddlSearchEmp.SelectedIndex != 0)
            //{
            //    if (ddlSearchDept.SelectedIndex == 0)
            //    {
            //        strSql = " Select a.ActivityCode "
            //        + " From Activity a, Evaluation b, dtAcEmp c "
            //        + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode "
            //        + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
            //        + " And b.ActivityCode = '" + dvChildActivity[i]["ActivityCode"].ToString() + "' "
            //        + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' ";
            //    }
            //    else
            //    {
            //        strSql = " Select a.ActivityCode "
            //            + " From Activity a, Evaluation b, dtAcEmp c, dtAcDept d "
            //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.ActivityCode = b.ActivityCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode "
            //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' And a.ProjectsCode = '" + tNodeProjects.Value + "' "
            //            + " And b.ActivityCode = '" + dvChildActivity[i]["ActivityCode"].ToString() + "' "
            //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
            //            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            //    }
            //}
            //dv1 = Conn.Select(strSql);

            dv2ChildActivity.RowFilter = " IndicatorsCode = '" + tNodeIndicators.Value + "' And ProjectsCode = '" + tNodeProjects.Value + "' And ActivityCode = '" + dvChildActivity[i]["ActivityCode"].ToString() + "' ";

            CkAPercent = (Convert.ToDouble(dvChildActivity[i]["CkAPercent"]) * 100) / (Convert.ToDouble(dv2ChildActivity.Count) * 100);

            if (CkAPercent < 50)
            {
                strColor = "Red";//"#f87373";
                //strEva = "(����ҹ)";
            }
            else
            {
                strColor = "#0B610B";
                //strEva = "(��ҹ)";
            }
            TreeNode tNodeActivity = new TreeNode();
            //tNode.Text = string.Format("<span style=\"float:left;color:" + strColor + ";\">{0}</span><span style=\"float:Right;color:" + strColor + ";\">{2}, {1}</span>", dv[i]["DtSourceName"].ToString(), dv[i]["LevelScore"].ToString(), dv[i]["Weight"].ToString());
            //tNode.Text = dv[i]["DtSourceName"].ToString();
            tNodeActivity.Text = string.Format("<img src=\"../Image/icon/" + ((CkAPercent >= 50) ? "icon5g" : "icon5r") + ".png\" style=\"float:left;border:none;padding:0 0 0 35px;\"/>&nbsp;&nbsp;<span onclick=\"getPopUpActivity('{4}')\" title=\"˹��§ҹ : {3}\" style=\"font-size:medium;color:" + ((rbtlView1.Checked) ? "#6E6E6E" : strColor) + ";\">{0}</span>&nbsp;&nbsp;&nbsp;<span style=\"font-size:medium;color:" + strColor + ";\">{2}%</span>&nbsp;&nbsp;&nbsp;<span style=\"font-size:12pt;color:" + strColor + ";\">{1}</span>", dvChildActivity[i]["ActivityName"].ToString(), strEva, CkAPercent.ToString("#,##0.00"), dvChildActivity[i]["DeptName"].ToString(), dvChildActivity[i]["ActivityCode"].ToString());
            tNodeActivity.Value = dvChildActivity[i]["ActivityCode"].ToString();

            AddChildIndicators2(tNodeActivity, tNodeProjects, tNodeIndicators);
            tNodeProjects.ChildNodes.Add(tNodeActivity);
        }
    }
    private void AddChildIndicators2(TreeNode tNodeActivity, TreeNode tNodeProjects, TreeNode tNodeIndicators) //��Ǫ���Ѵ
    {
        //string strSql = " Select a.Indicators2Code, '��Ǫ���Ѵ - ' + a.IndicatorsName2 IndicatorsName2, '%' As WeightType, a.Sort, b.APercent, b.CkCriterion "
        //            + " From Indicators2 a, Evaluation b "
        //            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.Indicators2Code = b.Indicators2Code  "
        //            + " And b.ProjectsCode = '" + tNodeProjects.Value + "' "
        //            + " And b.IndicatorsCode = '" + tNodeIndicators.Value + "' "
        //            + " And b.ActivityCode = '" + tNodeActivity.Value + "' "
        //            + " And b.StudyYear = '" + ddlYearB.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
        //            + " Group By a.Indicators2Code, a.IndicatorsName2, a.WeightType, a.Sort, b.APercent, b.CkCriterion Order By a.Sort";
        //DataView dv = Conn.Select(strSql);

        dvChildIndicators2.RowFilter = " ProjectsCode = '" + tNodeProjects.Value + "' And IndicatorsCode = '" + tNodeIndicators.Value + "' And ActivityCode = '" + tNodeActivity.Value + "' ";

        string strColor;
        string strEva = "";
        for (int i = 0; i < dvChildIndicators2.Count; i++)
        {
            if (dvChildIndicators2[i]["CkCriterion"].ToString() == "0")
            {
                strColor = "Red";//"#f87373";
                //strEva = "(����ҹ)";
            }
            else
            {
                strColor = "#0B610B";
                //strEva = "(��ҹ)";
            }
            TreeNode tNodeIndicators2 = new TreeNode();
            //tNode.Text = string.Format("<span style=\"float:left;color:" + strColor + ";\">{0}</span><span style=\"float:Right;color:" + strColor + ";\">{2}, {1}</span>", dv[i]["DtSourceName"].ToString(), dv[i]["LevelScore"].ToString(), dv[i]["Weight"].ToString());
            //tNode.Text = dv[i]["DtSourceName"].ToString();
            tNodeIndicators2.Text = string.Format("<img src=\"../Image/icon/" + ((dvChildIndicators2[i]["CkCriterion"].ToString() == "1") ? "icon6g" : "icon6r") + ".png\" style=\"float:left;border:none;padding:0 0 0 40px;\"/>&nbsp;&nbsp;<span style=\"font-size:medium;color:" + strColor + ";\">{0}</span>&nbsp;&nbsp;&nbsp;<span style=\"font-size:medium;color:" + strColor + ";\">{2}" + dvChildIndicators2[i]["WeightType"].ToString() + "</span>&nbsp;&nbsp;&nbsp;<span style=\"font-size:medium;color:" + strColor + ";\">{1}</span>", dvChildIndicators2[i]["IndicatorsName2"].ToString(), strEva, Convert.ToDouble(dvChildIndicators2[i]["APercent"]).ToString("#,##0.00"));
            tNodeIndicators2.Value = dvChildIndicators2[i]["Indicators2Code"].ToString();
            tNodeActivity.ChildNodes.Add(tNodeIndicators2);
        }
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
       TreeView1.Nodes.Clear(); 
       DataBind();
       ScriptManager.RegisterStartupScript(btn1, typeof(Page), "gotoReferer", "Loading.close();", true);
       //TreeView1.ExpandAll();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("TrDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        TreeView1.Nodes.Clear();
        DataBind();
        ScriptManager.RegisterStartupScript(btn1, typeof(Page), "gotoReferer", "Loading.close();", true);
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("TrEmp", ddlSearchEmp.SelectedValue);
        TreeView1.Nodes.Clear();
        DataBind();
        ScriptManager.RegisterStartupScript(btn1, typeof(Page), "gotoReferer", "Loading.close();", true);
    }
    protected void rbtView_OnSelectedChanged(object sender, EventArgs e)
    {
        TreeView1.Nodes.Clear();
        DataBind();
    }
    protected void TreeView1_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < TreeView1.Nodes.Count; i++)
        {
            if (TreeView1.Nodes[i].Selected == true)
            {
                if (TreeView1.Nodes[i].Expanded == false)
                {
                    TreeView1.Nodes[i].Expanded = true;
                }
            }
            for (int j = 0; j < TreeView1.Nodes[i].ChildNodes.Count; j++)
            {
                if (TreeView1.Nodes[i].ChildNodes[j].Selected == true)
                {
                    TreeView1.Nodes[i].ChildNodes[j].Expanded = true;
                }
                for (int k = 0; k < TreeView1.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                {
                    if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Selected == true)
                    {
                        TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].Expanded = true;
                    }
                    for (int l = 0; l < TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                    {
                        if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Selected == true)
                        {
                            TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Expanded = true;
                        }
                        for (int m = 0; m < TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                        {
                            if (TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Selected == true)
                            {
                                TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Expanded = true;
                            }
                            else
                            {
                                TreeView1.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Expanded = false;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void btn1_OnClick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(200);
        //ScriptManager.ClientScript.RegisterStartupScript(this.GetType(), "LoadingClose", "Loading.close();", true);
    }
}
