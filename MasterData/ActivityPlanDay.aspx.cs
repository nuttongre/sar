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

public partial class ActivityPlanDay : System.Web.UI.Page
{
    BTC btc = new BTC();

    protected override void OnPreInit(EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, null);

            btc.LinkReport(linkReport);
            getddlYear();
            getddlMonth();
            getddlDepartment();
            btc.CkGroup(ddlSearchDept, null, Cookie.GetValue2("ClDept").ToString(), "");
            string mode = Request["ck"];
            if (!String.IsNullOrEmpty(mode))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "AlertActivity();", true);
            }
        }
    }
    private void getddlYear()
    {
        btc.getdllStudyYear(ddlYearB);
        btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
    }
    private void getddlMonth()
    {
        btc.getddlMonth2(ddlMonth);
        ddlMonth.Items.Insert(0, new ListItem("-ทั้งหมด-", "00"));
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("ClDept", ddlSearchDept.SelectedValue);
    }
}
