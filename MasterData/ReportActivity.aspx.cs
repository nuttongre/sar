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
using Star.Web.UI.Controls;
using System.Text;

public partial class ReportActivity : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //�礻է�����ҳ
            btc.ckBudgetYear(lblSearchYear, null);

            Cookie.SetValue2("ckActivityStatus", btc.ckIdentityName("ckActivityStatus")); //�������Դ����ҹ

            getddlYear(0);
            getddlMonth();
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("AcDept").ToString(), Cookie.GetValue2("AcEmp").ToString());
            getrbtlStatus();
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                }
            }
            else
            {
                DataBind();
            }
        }
    }
    private void getddlMonth()
    {
        btc.getddlMonth(ddlSearchMonth);
    }
    private void getddlYear(int mode)
    {
        if (mode == 0)
        {
            btc.getdllStudyYear(ddlSearchYear);
            btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
        }
    }
    private void getddlStrategies(int mode, string StudyYear)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, StudyYear);
        }
    }
    private void getddlProjects(int mode, string StudyYear, string StrategiesCode)
    {
        if (mode == 0)
        {
            btc.getddlProjects(0, ddlSearch, StudyYear, StrategiesCode);
        }
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
    private void getColorBudget(Label lbl, decimal budget)
    {
        if (budget == 0)
        {
            lbl.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            if (budget > 0)
            {
                lbl.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    private void getrbtlStatus()
    {
        rbtlStatus.Items.Clear();
        int[] sts = { 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 6; i++)
        {
            sts[i] = new BTC().getCountActivity(2, i, ddlSearchDept.SelectedValue, ddlSearchEmp.SelectedValue, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue, ddlSearch.SelectedValue, ddlSearchMonth.SelectedValue);
        }

        rbtlStatus.Items.Insert(0, new ListItem(" <img style=\"border:none;\" src=\"../Image/00.png\" width=\"25px\" height=\"25px\"/> �ʹ��Թ��� &nbsp;(" + sts[0] + ")&nbsp;", "0"));
        rbtlStatus.Items.Insert(1, new ListItem(" <img style=\"border:none;\" src=\"../Image/02.png\" width=\"25px\" height=\"25px\"/> ��¡�˹���� &nbsp;(" + sts[2] + ")&nbsp;", "2"));
        rbtlStatus.Items.Insert(2, new ListItem(" <img style=\"border:none;\" src=\"../Image/01.png\" width=\"25px\" height=\"25px\"/> ���ѧ���Թ��� &nbsp;(" + sts[1] + ")&nbsp;", "1"));
        rbtlStatus.Items.Insert(3, new ListItem(" <img style=\"border:none;\" src=\"../Image/03.png\" width=\"25px\" height=\"25px\"/> ���Թ���������� &nbsp;(" + sts[3] + ")&nbsp;", "3"));
        rbtlStatus.Items.Insert(4, new ListItem(" <img style=\"border:none;\" src=\"../Image/04.png\" width=\"25px\" height=\"25px\"/> �����¡�˹���� &nbsp;(" + sts[4] + ")&nbsp;", "4"));
        rbtlStatus.Items.Insert(5, new ListItem(" <img style=\"border:none;\" src=\"../Image/09.png\" width=\"25px\" height=\"25px\"/> �ءʶҹ�(" + sts[5] + ")&nbsp; ", "5"));

        if (Session["sts"] == null)
        {
            rbtlStatus.SelectedIndex = 2;
        }
        else
        {
            rbtlStatus.SelectedValue = Session["sts"].ToString();
        }
    }
    protected string DateFormat(object startDate, object endDate)
    {
        return Convert.ToDateTime(startDate).ToString("dd/MM/yy") + " - " + Convert.ToDateTime(endDate).ToString("dd/MM/yy");
    }
    public override void DataBind()
    {
        string StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, b.Status, '' DeptName, b.Sort, "
                        + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                        + " From Projects a, Activity b "
                        + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 "
                        + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, b.Status, '' DeptName, b.Sort, "
                        + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                        + " From Projects a, Activity b, dtAcDept c "
                        + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                        + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, b.Status, '' DeptName, b.Sort, "
                            + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                            + " From Projects a, Activity b, dtAcEmp c "
                            + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                            + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, b.Status, '' DeptName, b.Sort, "
                                + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                                + " From Projects a, Activity b, dtAcEmp c, dtAcDept d "
                                + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode "
                                + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        if (rbtlStatus.SelectedIndex != 5)
        {
            StrSql = StrSql + " And b.Status = '" + rbtlStatus.SelectedValue + "'";
        }

        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.ProjectsCode = '" + ddlSearch.SelectedValue + "'";
        }
        if (ddlSearchMonth.SelectedIndex != 0)
        {
            int CkYear = Convert.ToInt32(ddlSearchYear.SelectedValue) - 543;
            StrSql = StrSql + " And Month(b.SDate) = " + ddlSearchMonth.SelectedValue + " And Year(b.SDate) = " + CkYear + " ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And b.ActivityName Like '%" + txtSearch.Text + "%' ";
        }

        DataView dv = Conn.Select(string.Format(StrSql + " Order By b.Sort "));

        for (int j = 0; j < dv.Count; j++)
        {
            dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["ActivityCode"].ToString());
            if (btc.getNTotalAmount(dv[j]["ActivityCode"].ToString()) != 0)
            {
                dv[j]["TotalAmount2"] = btc.getNTotalAmount(dv[j]["ActivityCode"].ToString());
            }
            dv[j]["TotalBalance"] = (Convert.ToDecimal(dv[j]["TotalAmount"]) - Convert.ToDecimal(dv[j]["TotalAmount2"])).ToString();
        }

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getrbtlStatus();
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getrbtlStatus();
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSearch2.SelectedValue = btc.getStrategiesCodeOfProj(ddlSearch.SelectedValue);
        getrbtlStatus();
        DataBind();
    }
    protected void ddlSearchMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        getrbtlStatus();
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcDept", ddlSearchDept.SelectedValue);
        Session["sts"] = rbtlStatus.SelectedValue;
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        getrbtlStatus();
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcEmp", ddlSearchEmp.SelectedValue);
        Session["sts"] = rbtlStatus.SelectedValue;
        getrbtlStatus();
        DataBind();
    }
    protected void rbtlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["sts"] = rbtlStatus.SelectedValue;
        DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.ForeColor = btc.getColorRowsGrid(DataBinder.Eval(e.Row.DataItem, "status").ToString());
        }
    }
    decimal TotalAmount1;
    decimal TotalAmount2;
    decimal TotalAmount3;
    public decimal GetTotalAmount1(decimal Budget)
    {
        TotalAmount1 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount1()
    {
        return TotalAmount1;
    }
    public decimal GetTotalAmount2(decimal Budget)
    {
        TotalAmount2 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount2()
    {
        return TotalAmount2;
    }
    public decimal GetTotalAmount3(decimal Budget)
    {
        TotalAmount3 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount3()
    {
        return TotalAmount3;
    }
    protected string checkStatus(string id)
    {
        DataView dv = Conn.Select("Select Status From Activity Where DelFlag = 0 And ActivityCode = '" + id + "' And Status = 3");
        if (dv.Count != 0)
        {

            return String.Format("<a href=\"../GtReport/Viewer.aspx?rpt=23&id={0}\" target=\"_blank\"><img style=\"border: 0; cursor :pointer;\" title=\"���¡����§ҹ�š�û�Ժѵ�ҹ���Ἱ��Ժѵԡ��\" src=\"../Image/reporticon.gif\"</a>", id);
        }
        else
        {
            return string.Format("");
        }
    }
    protected string getActivityStatus(string ActivityStatus)
    {
        return btc.getSpanColorStatus(Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")), ActivityStatus);
    }
}
