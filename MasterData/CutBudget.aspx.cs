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

public partial class CutBudget : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    decimal TotalAmount1 = 0;
    decimal TotalAmount2 = 0;
    decimal TotalAmount3 = 0;

    protected override void OnInit(EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "function loadPage(){" + Page.ClientScript.GetPostBackEventReference(btAutoRefresh, null, false) + ";}", true);
        base.OnInit(e);
    }
    protected override void OnPreInit(EventArgs e)
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 2)
        {
            this.MasterPageFile = "~/Master/MasterManageView.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "setupRefresh();", true);
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            if (btc.ckIdentityName("ckBudgetYear"))
            {
                rbtStudyYear.Text = " �է�����ҳ";
                rbtBudgetYear.Text = " �ա���֡��";
            }
            setYearType();
            getddlYear(0);
            getddlProjectsNotStrategies(ddlSearchYear.SelectedValue);
            getddlDepartment();
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
    private void setYearType()
    {
        if (Session["rbtYear"] == null)
        {
            rbtStudyYear.Checked = true;
            rbtBudgetYear.Checked = false;
        }
        else
        {
            try
            {
                if (!btc.ckIdentityName("ckBudgetYear"))
                {
                    if (Session["rbtYear"].ToString() == "0")
                    {
                        rbtStudyYear.Checked = true;
                        rbtBudgetYear.Checked = false;
                    }
                    else
                    {
                        rbtStudyYear.Checked = false;
                        rbtBudgetYear.Checked = true;
                    }
                }
                else
                {
                    if (Session["rbtYear"].ToString() == "1")
                    {
                        rbtStudyYear.Checked = true;
                        rbtBudgetYear.Checked = false;
                    }
                    else
                    {
                        rbtStudyYear.Checked = false;
                        rbtBudgetYear.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                rbtStudyYear.Checked = true;
                rbtBudgetYear.Checked = false;
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
        if (Session["sYearCode"] == null)
        {
            btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
        }
        else
        {
            try
            {
                ddlSearchYear.SelectedValue = Session["sYearCode"].ToString();
            }
            catch (Exception ex)
            {
                btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
            }
        }
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-������-", ""));
        btc.CkAllDept(ddlSearchDept);
        if (Session["sDeptCode"] == null)
        {
            ddlSearchDept.SelectedIndex = 0;
        }
        else
        {
            try
            {
               ddlSearchDept.SelectedValue = Session["sDeptCode"].ToString();
            }
            catch (Exception ex)
            {
                ddlSearchDept.SelectedIndex = 0;
            }
        }
    }
    private void getddlProjectsNotStrategies(string StudyYear)
    {
        int mode = 0;
        if (rbtBudgetYear.Checked)
        {
            mode = 1;
        }
        DataView dv = new BTC().getddlProjectsNotStrategies(StudyYear, mode);
        if (dv.Count != 0)
        {
            ddlSearchProjects.DataSource = dv;
            ddlSearchProjects.DataTextField = "FullName";
            ddlSearchProjects.DataValueField = "ProjectsCode";
            ddlSearchProjects.DataBind();
            ddlSearchProjects.Items.Insert(0, new ListItem("-������-", ""));

            if (Session["sPrjCode"] == null)
            {
                ddlSearchProjects.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearchProjects.SelectedValue = Session["sPrjCode"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearchProjects.SelectedIndex = 0;
                }
            }
        }
        else
        {
            ddlSearchProjects.Items.Clear();
            ddlSearchProjects.Items.Insert(0, new ListItem("-������-", ""));
            ddlSearchProjects.SelectedIndex = 0;
        }
    }
    public override void DataBind()
    {
        string StrSql = "";
        if (ddlSearchDept.SelectedIndex == 0)
        {
            StrSql = " Select c.ActivityCode AcCode,'�Ԩ����' + c.ActivityName As FullName, c.Status, "
                    + " IsNull(c.TotalAmount, 0) TotalAmount1, "
                    + " IsNull(c.TotalAmount2, 0) TotalAmount2, "
                    + " (IsNull(c.TotalAmount, 0) - IsNull(c.TotalAmount2, 0)) TotalBalance, '' As DeptName, IsNull(c.ApproveFlag, 0) ApproveFlag, c.UpdateDate "
                    + " From Activity c "
                    + " Where c.DelFlag = 0 "
                    + " And c.SchoolID = '" + CurrentUser.SchoolID + "' ";

            if (rbtStudyYear.Checked)
            {
                StrSql += " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
            }
            else
            {
                StrSql += " And c.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
            }

            if (txtSearch.Text != "")
            {
                StrSql = StrSql + " And c.ActivityName Like '%" + txtSearch.Text + "%'  ";
            }
            if (ddlSearchProjects.SelectedIndex != 0)
            {
                StrSql = StrSql + " And c.ProjectsCode = '" + ddlSearchProjects.SelectedValue + "' ";
            }

            StrSql = StrSql + " Group By c.ActivityCode, c.ActivityName, c.TotalAmount, c.TotalAmount2, c.Status, c.ApproveFlag, c.UpdateDate ";
        }
        else
        {
            StrSql = " Select c.ActivityCode AcCode,'�Ԩ����' + c.ActivityName As FullName, c.Status, "
                    + " IsNull(c.TotalAmount, 0) TotalAmount1, "
                    + " IsNull(c.TotalAmount2, 0) TotalAmount2, "
                    + " (IsNull(c.TotalAmount, 0) - IsNull(c.TotalAmount2, 0)) TotalBalance, '' As DeptName, IsNull(c.ApproveFlag, 0) ApproveFlag, c.UpdateDate "
                    + " From Activity c, dtAcDept d "
                    + " Where c.DelFlag = 0 And c.ActivityCode = d.ActivityCode "
                    + " And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";

            if (rbtStudyYear.Checked)
            {
                StrSql += " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
            }
            else
            {
                StrSql += " And c.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
            }

            if (txtSearch.Text != "")
            {
                StrSql = StrSql + " And c.ActivityName Like '%" + txtSearch.Text + "%'  ";
            }
            if (ddlSearchProjects.SelectedIndex != 0)
            {
                StrSql = StrSql + " And c.ProjectsCode = '" + ddlSearchProjects.SelectedValue + "' ";
            }
        }
        DataView dv = Conn.Select(string.Format(StrSql));
        if (dv.Count != 0)
        {
            for (int j = 0; j < dv.Count; j++)
            {
                dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["AcCode"].ToString());
            }
        }

        //�礼����
        try
        {
            DataTable dt = dv.ToTable();
            TotalAmount1 = Convert.ToDecimal(dt.Compute("Sum(TotalAmount1)", dv.RowFilter));
            TotalAmount2 = Convert.ToDecimal(dt.Compute("Sum(TotalAmount2)", dv.RowFilter));
            TotalAmount3 = Convert.ToDecimal(dt.Compute("Sum(TotalBalance)", dv.RowFilter));
        }
        catch (Exception ex)
        { 
        }

            GridView1.DataSource = dv;
            lblSearchTotal.InnerText = dv.Count.ToString();
            GridView1.DataBind();
        
        getBudget();

        //----GrandTotal-----------
        if (ddlSearchDept.SelectedIndex == 0)
        {
            StrSql = " Select "
                    + " IsNull(Sum(c.TotalAmount), 0) TotalAmount1, "
                    + " IsNull(Sum(c.TotalAmount2), 0) TotalAmount2, "
                    + " (IsNull(Sum(c.TotalAmount), 0) - IsNull(Sum(c.TotalAmount2), 0)) TotalBalance "
                    + " From Activity c "
                    + " Where c.DelFlag = 0  "
                    + " And c.SchoolID = '" + CurrentUser.SchoolID + "' ";

            if (rbtStudyYear.Checked)
            {
                StrSql += " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
            }
            else
            {
                StrSql += " And c.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
            }
        }
        else
        {
            StrSql = " Select "
                    + " IsNull(Sum(c.TotalAmount), 0) TotalAmount1, "
                    + " IsNull(Sum(c.TotalAmount2), 0) TotalAmount2, "
                    + " (IsNull(Sum(c.TotalAmount), 0) - IsNull(Sum(c.TotalAmount2), 0)) TotalBalance "
                    + " From Activity c, dtAcDept d "
                    + " Where c.DelFlag = 0 And c.ActivityCode = d.ActivityCode "
                    + " And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";

            if (rbtStudyYear.Checked)
            {
                StrSql += " And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
            }
            else
            {
                StrSql += " And c.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
            }
        }

        DataView dvTotal = Conn.Select(StrSql);
        ToltalBudget.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount1"]).ToString("#,##0.00") : "0.00";
        ToltalBudget2.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount2"]).ToString("#,##0.00") : "0.00";
        TotalBalance.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalBalance"]).ToString("#,##0.00") : "0.00";

        //----EndGrandTotal----------
    }
    private void getBudget()
    {
        //�Թ����Ѻ
        string StrSql = " Select IsNull(Subsidies, 0) Subsidies, IsNull(Revenue, 0) Revenue, IsNull(Free, 0) Free, IsNull(Other, 0) Other, "
                    + " (Subsidies + Revenue + Free + Other) As TotalAmount From Income "
                    + " Where DelFlag = 0 And StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        DataView dvInBudget = Conn.Select(string.Format(StrSql));
        if (dvInBudget.Count > 0)
        {
            lblInSubsidies.Text = Convert.ToDecimal(dvInBudget[0]["Subsidies"]).ToString("#,##0.00");
            lblInRevenue.Text = Convert.ToDecimal(dvInBudget[0]["Revenue"]).ToString("#,##0.00");
            lblInFree.Text = Convert.ToDecimal(dvInBudget[0]["Free"]).ToString("#,##0.00");
            lblInOther.Text = Convert.ToDecimal(dvInBudget[0]["Other"]).ToString("#,##0.00");
            lblTotalIn.Text = Convert.ToDecimal(dvInBudget[0]["TotalAmount"]).ToString("#,##0.00");
        }

        //�Թ��¨��»�Ш�
        StrSql = " Select IsNull(Subsidies, 0) Subsidies, IsNull(Revenue, 0) Revenue, IsNull(Free, 0) Free, IsNull(Other, 0) Other, " 
                + " (Subsidies + Revenue + Free + Other) As TotalAmount From Expenses "
                + " Where DelFlag = 0 And StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        DataView dvOutBudgetRt = Conn.Select(string.Format(StrSql));
        if (dvOutBudgetRt.Count > 0)
        {
            lblOutSubsidiesRt.Text = Convert.ToDecimal(dvOutBudgetRt[0]["Subsidies"]).ToString("#,##0.00");
            lblOutRevenueRt.Text = Convert.ToDecimal(dvOutBudgetRt[0]["Revenue"]).ToString("#,##0.00");
            lblOutFreeRt.Text = Convert.ToDecimal(dvOutBudgetRt[0]["Free"]).ToString("#,##0.00");
            lblOutOtherRt.Text = Convert.ToDecimal(dvOutBudgetRt[0]["Other"]).ToString("#,##0.00");
            lblTotalOutRt.Text = Convert.ToDecimal(dvOutBudgetRt[0]["TotalAmount"]).ToString("#,##0.00");
        }

        //�Թ��¨���
        StrSql = " Select IsNull(Sum(TotalMoney),0) TotalMoney "
                    + " From Activity b, CostsDetail c, BudgetType d "
                    + " Where b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And c.BudgetTypeCode = d.BudgetTypeCode "
                    + " And d.ckType = 1 ";

        if (rbtStudyYear.Checked)
        {
            StrSql += " And b.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        }
        else
        {
            StrSql += " And b.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
        }

        DataView dvOutSubsidies = Conn.Select(string.Format(StrSql));
        if (dvOutSubsidies.Count > 0)
        {
            lblOutSubsidies.Text = Convert.ToDecimal(dvOutSubsidies[0]["TotalMoney"]).ToString("#,##0.00");
        }

        StrSql = " Select IsNull(Sum(TotalMoney),0) TotalMoney "
                    + " From Activity b, CostsDetail c, BudgetType d "
                    + " Where b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And c.BudgetTypeCode = d.BudgetTypeCode "
                    + " And d.ckType = 2 ";

        if (rbtStudyYear.Checked)
        {
            StrSql += " And b.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        }
        else
        {
            StrSql += " And b.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
        }

        DataView dvOutRevenue = Conn.Select(string.Format(StrSql));
        if (dvOutRevenue.Count > 0)
        {
            lblOutRevenue.Text = Convert.ToDecimal(dvOutRevenue[0]["TotalMoney"]).ToString("#,##0.00");
        }

        StrSql = " Select IsNull(Sum(TotalMoney),0) TotalMoney "
                    + " From Activity b, CostsDetail c, BudgetType d "
                    + " Where b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And c.BudgetTypeCode = d.BudgetTypeCode "
                    + " And d.ckType = 3 ";

        if (rbtStudyYear.Checked)
        {
            StrSql += " And b.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        }
        else
        {
            StrSql += " And b.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
        }

        DataView dvOutFree = Conn.Select(string.Format(StrSql));
        if (dvOutFree.Count > 0)
        {
            lblOutFree.Text = Convert.ToDecimal(dvOutFree[0]["TotalMoney"]).ToString("#,##0.00");
        }

        StrSql = " Select IsNull(Sum(TotalMoney),0) TotalMoney "
                    + " From Activity b, CostsDetail c, BudgetType d "
                    + " Where b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And c.BudgetTypeCode = d.BudgetTypeCode "
                    + " And d.ckType = 4 ";

        if (rbtStudyYear.Checked)
        {
            StrSql += " And b.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        }
        else
        {
            StrSql += " And b.BudgetYear = '" + ddlSearchYear.SelectedValue + "' ";
        }

        DataView dvOutOther = Conn.Select(string.Format(StrSql));
        if (dvOutOther.Count > 0)
        {
            lblOutOther.Text = Convert.ToDecimal(dvOutOther[0]["TotalMoney"]).ToString("#,##0.00");
        }
        lblTotalOut.Text = (Convert.ToDecimal(lblOutSubsidies.Text) + Convert.ToDecimal(lblOutRevenue.Text) + Convert.ToDecimal(lblOutFree.Text) + Convert.ToDecimal(lblOutOther.Text)).ToString("#,##0.00");

        lblBalanceSubsidies.Text = (Convert.ToDecimal(lblInSubsidies.Text) - Convert.ToDecimal(lblOutSubsidiesRt.Text) - Convert.ToDecimal(lblOutSubsidies.Text)).ToString("#,##0.00");
        lblBalanceRevenue.Text = (Convert.ToDecimal(lblInRevenue.Text) - Convert.ToDecimal(lblOutRevenueRt.Text) - Convert.ToDecimal(lblOutRevenue.Text)).ToString("#,##0.00");
        lblBalanceFree.Text = (Convert.ToDecimal(lblInFree.Text) - Convert.ToDecimal(lblOutFreeRt.Text) - Convert.ToDecimal(lblOutFree.Text)).ToString("#,##0.00");
        lblBalanceOther.Text = (Convert.ToDecimal(lblInOther.Text) - Convert.ToDecimal(lblOutOtherRt.Text) - Convert.ToDecimal(lblOutOther.Text)).ToString("#,##0.00");

        lblTotalBalance.Text = (Convert.ToDecimal(lblBalanceSubsidies.Text) + Convert.ToDecimal(lblBalanceRevenue.Text) + Convert.ToDecimal(lblBalanceFree.Text) + Convert.ToDecimal(lblBalanceOther.Text)).ToString("#,##0.00");

        btc.lblColor(lblBalanceSubsidies, Convert.ToDecimal(lblBalanceSubsidies.Text));
        btc.lblColor(lblBalanceRevenue, Convert.ToDecimal(lblBalanceRevenue.Text));
        btc.lblColor(lblBalanceFree, Convert.ToDecimal(lblBalanceFree.Text));
        btc.lblColor(lblBalanceOther, Convert.ToDecimal(lblBalanceOther.Text));
        btc.lblColor(lblTotalBalance, Convert.ToDecimal(lblTotalBalance.Text));
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlProjectsNotStrategies(ddlSearchYear.SelectedValue);
        Session["sYearCode"] = ddlSearchYear.SelectedValue;
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["sDeptCode"] = ddlSearchDept.SelectedValue;
        DataBind();
    }
    protected void ddlSearchProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["sPrjCode"] = ddlSearchProjects.SelectedValue;
        DataBind();
    }
    protected void rbtChanged(object sender, EventArgs e)
    {
        if (!btc.ckIdentityName("ckBudgetYear"))
        {
            if (rbtStudyYear.Checked)
            {
                Session["rbtYear"] = "0";
            }
            else
            {
                Session["rbtYear"] = "1";
            }
        }
        else
        {
            if (rbtStudyYear.Checked)
            {
                Session["rbtYear"] = "1";
            }
            else
            {
                Session["rbtYear"] = "0";
            }
        }
        DataBind();
    }
    protected string checkapprove(string id, int ckOld)
    {
        string str = "<a href=\"javascript:;\" " + btc.getLinkReportWEP("W") + " onclick=\"printRpt(4,'w','{0}', " + ckOld + ");\"><img style=\"border: 0; cursor :pointer;\" title=\"���¡��㺢�͹��ѵԡԨ���� Ẻ�͡��� Word\" src=\"../Image/WordIcon.png\"</a>"
            + "<a href=\"javascript:;\" " + btc.getLinkReportWEP("E") + " onclick=\"printRpt(4,'e','{0}', " + ckOld + ");\"><img style=\"border: 0; cursor :pointer;\" title=\"���¡��㺢�͹��ѵԡԨ���� Ẻ�͡��� Excel\" src=\"../Image/Excel.png\"</a>"
            + "<a href=\"javascript:;\" " + btc.getLinkReportWEP("P") + " onclick=\"printRpt(4,'p','{0}', " + ckOld + ");\"><img style=\"border: 0; cursor :pointer;\" title=\"���¡��㺢�͹��ѵԡԨ���� Ẻ�͡��� PDF\" src=\"../Image/PdfIcon.png\"</a>";
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            return String.Format(str, id);
        }
        else
        {
            DataView dv = Conn.Select("Select ApproveFlag From Activity Where DelFlag = 0 And ActivityCode = '" + id + "' And ApproveFlag = 1");
            if (dv.Count != 0)
            {

                return String.Format(str, id);
            }
            else
            {
                return string.Format("");
            }
        }
    }
    protected void dgSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected decimal GetTotalAmount1(decimal Budget)
    {
        //TotalAmount1 += Budget;
        return Budget;
    }
    protected decimal GetSumTotalAmount1()
    {
        return TotalAmount1;
    }
    protected decimal GetTotalAmount2(decimal Budget)
    {
        //TotalAmount2 += Budget;
        return Budget;
    }
    protected decimal GetSumTotalAmount2()
    {
        return TotalAmount2;
    }
    protected decimal GetTotalAmount3(decimal Budget)
    {
        //TotalAmount3 += Budget;
        return Budget;
    }
    protected decimal GetSumTotalAmount3()
    {
        return TotalAmount3;
    }
    protected string PjOrAcName(string id, string strName)
    {
        return String.Format("<a href=\"javascript:;\" onclick=\"newtabItem('{0}');\" title=\"��䢧�����ҳ\">{1}</a>", id, strName);
    }
    protected void btAutoRefresh_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected string spnCutBudget(string id, string UpdateTime)
    {
        try
        {
            DateTime updatedate;
            updatedate = Convert.ToDateTime(UpdateTime);

            if (updatedate.AddMinutes(5) >= DateTime.Now)
            {
                string strSql = "Select "
                    + " NewBg = (Select TotalAmount From Activity Where ActivityCode = '" + id + "'), "
                    + " OldBg = (Select Top 1 OldBudget From dtEditBudgetAc Where ActivityCode = '" + id + "' Order By UpdateDate Desc)";
                DataView dv = Conn.Select(strSql);
                if (dv.Count != 0)
                {
                    if (Convert.ToDecimal(dv[0]["NewBg"]) > Convert.ToDecimal(dv[0]["OldBg"]))
                    {
                        return "spnCutbudgetG";
                    }
                    else
                    {
                        return "spnCutbudgetR";
                    }
                }
                else
                {
                    return "spnCutbudgetG";
                }
            }
            else
            {
                return "spnCutbudgetB";
            }
        }
        catch (Exception ex)
        {
            return "spnCutbudgetB";
        }
    }
}
