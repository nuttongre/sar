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

using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Protocols;
using System.IO;
using System.Xml;


public partial class DrawMoney : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    decimal TotalAmount1 = 0;
    decimal TotalAmount2 = 0;
    decimal TotalAmount3 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            btc.LinkReport(linkReport);

            //àªç¤»Õ§º»ÃÐÁÒ³
            btc.ckBudgetYear(lblSearchYear, null);

            Cookie.SetValue2("ckActivityStatus", btc.ckIdentityName("ckActivityStatus")); //àªç¤âËÁ´µÔ´µÒÁ§Ò¹

            getddlYear(0);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("DrDept").ToString(), Cookie.GetValue2("DrEmp").ToString());

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
            if (Session["AcStrategies"] == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Session["AcStrategies"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch2.SelectedIndex = 0;
                }
            }
        }
    }
    private void getddlProjects(int mode, string StudyYear, string StrategiesCode)
    {
        if (mode == 0)
        {
            if (string.IsNullOrEmpty(StrategiesCode))
            {
                Session.Remove("AcProjects");
            }
            btc.getddlProjects(0, ddlSearch, StudyYear, StrategiesCode);
            if (Session["AcProjects"] == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch.SelectedValue = Session["AcProjects"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch.SelectedIndex = 0;
                }
            }
        }
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-·Ñé§ËÁ´-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    private void getddlEmpByDept(int mode, string DeptCode)
    {
        if (mode == 0)
        {
            btc.getddlEmpByDept(0, ddlSearchEmp, DeptCode);
        }
    }
    protected string DateFormat(object startDate, object endDate)
    {
        return Convert.ToDateTime(startDate).ToString("dd/MM/yy") + " - " + Convert.ToDateTime(endDate).ToString("dd/MM/yy");
    }
    public override void DataBind()
    {
        string StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, "
                    + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, "
                    + " IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                    + " From Projects a, Activity b "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, "
                    + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, "
                    + " IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                    + " From Projects a, Activity b, dtAcDept c "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, "
                        + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, "
                        + " IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                        + " From Projects a, Activity b, dtAcEmp c "
                        + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                        + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, "
                            + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, 0.0 As TotalBalance, "
                            + " IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                            + " From Projects a, Activity b, dtAcEmp c, dtAcDept d "
                            + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode "
                            + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
            }
        }
        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.ProjectsCode = '" + ddlSearch.SelectedValue + "'";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And b.ActivityName Like '%" + txtSearch.Text + "%' ";
        }

        DataView dv = Conn.Select(string.Format(StrSql + " And ApproveFlag = 1 Order By a.Sort Desc, b.Sort Desc "));

        for (int j = 0; j < dv.Count; j++)
        {
            decimal TtAmount2 = Convert.ToDecimal(btc.getNTotalAmount(dv[j]["ActivityCode"].ToString()));
            dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["ActivityCode"].ToString());

            if (TtAmount2 != 0)
            {
                dv[j]["TotalAmount2"] = TtAmount2.ToString();
            }
            dv[j]["TotalBalance"] = (Convert.ToDecimal(dv[j]["TotalAmount"]) - Convert.ToDecimal(dv[j]["TotalAmount2"])).ToString();
        }

        //àªç¤¼ÅÃÇÁ
        try
        {
            DataTable dt = dv.ToTable();
            TotalAmount1 = Convert.ToDecimal(dt.Compute("Sum(TotalAmount)", dv.RowFilter));
            TotalAmount2 = Convert.ToDecimal(dt.Compute("Sum(TotalAmount2)", dv.RowFilter));
            TotalAmount3 = Convert.ToDecimal(dt.Compute("Sum(TotalBalance)", dv.RowFilter));
        }
        catch (Exception ex)
        { 
        }

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();

        ////----GrandTotal-----------
        //StrSql = "Select IsNull(Sum(c.TotalAmount), 0) TotalAmount1, IsNull(Sum(c.TotalAmount2), 0) TotalAmount2, "
        //            + " (IsNull(Sum(c.TotalAmount), 0) - IsNull(Sum(c.TotalAmount2), 0)) TotalBalance "
        //            + " From Activity c "
        //            + " Where c.DelFlag = 0 "
        //            + " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
        //            + " And c.ApproveFlag = 1 ";
        //if (ddlSearchDept.SelectedIndex != 0)
        //{
        //    StrSql = "Select IsNull(Sum(c.TotalAmount), 0) TotalAmount1, IsNull(Sum(c.TotalAmount2), 0) TotalAmount2, "
        //            + " (IsNull(Sum(c.TotalAmount), 0) - IsNull(Sum(c.TotalAmount2), 0)) TotalBalance "
        //            + " From Activity c, dtAcDept d "
        //            + " Where c.DelFlag = 0 And c.ActivityCode = d.ActivityCode "
        //            + " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
        //            + " And d.DeptCode = '" + ddlSearchDept.SelectedValue + "'"
        //            + " And c.ApproveFlag = 1 ";
        //}
        //if (ddlSearchEmp.SelectedIndex != 0)
        //{
        //    if (ddlSearchDept.SelectedIndex == 0)
        //    {
        //        StrSql = "Select IsNull(Sum(c.TotalAmount), 0) TotalAmount1, IsNull(Sum(c.TotalAmount2), 0) TotalAmount2, "
        //            + " (IsNull(Sum(c.TotalAmount), 0) - IsNull(Sum(c.TotalAmount2), 0)) TotalBalance "
        //                + " From Activity c, dtAcEmp d "
        //                + " Where c.DelFlag = 0 And c.ActivityCode = d.ActivityCode "
        //                + " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
        //                + " And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "'"
        //                + " And c.ApproveFlag = 1 ";
        //    }
        //    else
        //    {
        //        StrSql = "Select IsNull(Sum(c.TotalAmount), 0) TotalAmount1, IsNull(Sum(c.TotalAmount2), 0) TotalAmount2, "
        //            + " (IsNull(Sum(c.TotalAmount), 0) - IsNull(Sum(c.TotalAmount2), 0)) TotalBalance "
        //                    + " From Activity c, dtAcEmp d, dtAcDept e "
        //                    + " Where c.DelFlag = 0 And c.ActivityCode = d.ActivityCode And c.ActivityCode = e.ActivityCode "
        //                    + " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
        //                    + " And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And e.DeptCode = '" + ddlSearchDept.SelectedValue + "'"
        //                    + " And c.ApproveFlag = 1 ";
        //    }
        //}

        //----GrandTotal-----------
        StrSql = "Select a.ActivityCode, IsNull(Sum(a.TotalAmount), 0) TotalAmount, IsNull(Sum(a.TotalAmount2), 0) TotalAmount2 "
                    + " From Activity a, Projects b "
                    + " Where a.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And a.ApproveFlag = 1 "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = "Select a.ActivityCode, IsNull(Sum(a.TotalAmount), 0) TotalAmount, IsNull(Sum(a.TotalAmount2), 0) TotalAmount2 "
                    + " From Activity a, Projects b, dtAcDept c "
                    + " Where a.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And a.ActivityCode = c.ActivityCode And a.ApproveFlag = 1 "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select a.ActivityCode, IsNull(Sum(a.TotalAmount), 0) TotalAmount, IsNull(Sum(a.TotalAmount2), 0) TotalAmount2 "
                        + " From Activity a, Projects b, dtAcEmp c "
                        + " Where a.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And a.ActivityCode = c.ActivityCode And a.ApproveFlag = 1 "
                        + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = "Select a.ActivityCode, IsNull(Sum(a.TotalAmount), 0) TotalAmount, IsNull(Sum(a.TotalAmount2), 0) TotalAmount2 "
                            + " From Activity a, Projects b, dtAcEmp c, dtAcDept d "
                            + " Where a.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And a.ActivityCode = c.ActivityCode And a.ActivityCode = d.ActivityCode And a.ApproveFlag = 1 "
                            + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }

        DataView dvTotal = Conn.Select(StrSql + " Group By a.ActivityCode ");

        for (int j = 0; j < dvTotal.Count; j++)
        {
            decimal TtAmount2 = Convert.ToDecimal(btc.getNTotalAmount(dvTotal[j]["ActivityCode"].ToString()));

            if (TtAmount2 != 0)
            {
                dvTotal[j]["TotalAmount2"] = TtAmount2.ToString();
            }
        }

        try
        {
            DataTable dt = dvTotal.ToTable();
            ToltalBudget.InnerHtml = Convert.ToDecimal(dt.Compute("Sum(TotalAmount)", dvTotal.RowFilter)).ToString("#,##0.00");
            ToltalBudget2.InnerHtml = Convert.ToDecimal(dt.Compute("Sum(TotalAmount2)", dvTotal.RowFilter)).ToString("#,##0.00");
            TotalBalance.InnerHtml = (Convert.ToDecimal(ToltalBudget.InnerHtml) - Convert.ToDecimal(ToltalBudget2.InnerHtml)).ToString("#,##0.00");
        }
        catch (Exception ex)
        {
        }

        //ToltalBudget.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount"]).ToString("#,##0.00") : "0.00";
        //ToltalBudget2.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount2"]).ToString("#,##0.00") : "0.00";
        //TotalBalance.InnerHtml = (dvTotal.Count != 0) ? (Convert.ToDecimal(dvTotal[0]["TotalAmount"]) - Convert.ToDecimal(dvTotal[0]["TotalAmount2"])).ToString("#,##0.00") : "0.00";

        //----EndGrandTotal-----------
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["AcStrategies"] = ddlSearch2.SelectedValue;
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["AcProjects"] = ddlSearch.SelectedValue;
        ddlSearch2.SelectedValue = btc.getStrategiesCodeOfProj(ddlSearch.SelectedValue);
        Session["AcStrategies"] = ddlSearch2.SelectedValue;
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("DrDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("DrEmp", ddlSearchEmp.SelectedValue);
        DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBalance").ToString()) == 0)
            {
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBalance").ToString()) > 0)
                {
                    e.Row.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }       
        }
    }
    public decimal GetTotalAmount1(decimal Budget)
    {
        //TotalAmount1 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount1()
    {
        return TotalAmount1;
    }
    public decimal GetTotalAmount2(decimal Budget)
    {
        //TotalAmount2 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount2()
    {
        return TotalAmount2;
    }
    public decimal GetTotalAmount3(decimal Budget)
    {
        //TotalAmount3 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount3()
    {
        return TotalAmount3;
    }
    protected string getActivityStatus(string ActivityStatus)
    {
        return btc.getSpanColorStatus(Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")), ActivityStatus);
    }
}
