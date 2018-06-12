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
using Star.Security.Cryptography;

public partial class Report_Indicators : System.Web.UI.Page
{
    BTC btc = new BTC();

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
        if (!IsPostBack)
        {
            //�礻է�����ҳ
            btc.ckBudgetYear(lblSearchYear, null);

            btc.LinkReport(linkReport);
            getddlYear(1);
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("IndDept").ToString(), "");
        }
    }
    private void getddlYear(int mode)
    {
        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
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
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
    }
}
