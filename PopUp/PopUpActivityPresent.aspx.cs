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

public partial class PopUpActivityPresent : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData(Request["id"]);

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
        }
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        string strSql = "Select a.StrategiesName, b.ProjectsCode, b.ProjectsName, b.MissionCode, b.GoalsCode, c.* "
                    + " From Strategies a, Projects b, Activity c "
                    + " Where c.ActivityCode = '" + id + "' And a.StrategiesCode = b.StrategiesCode And b.ProjectsCode = c.ProjectsCode ";
        DataView dv = Conn.Select(string.Format(strSql));

        if (dv.Count != 0)
        {
            lblYearB.Text = dv[0]["StudyYear"].ToString();
            lblStrategies.Text = dv[0]["StrategiesName"].ToString();
            lblProjects.Text = dv[0]["ProjectsName"].ToString();
            lblPurpose.Text = dv[0]["Purpose"].ToString();
            lblTarget.Text = dv[0]["Target"].ToString();
            lblTarget2.Text = dv[0]["Target2"].ToString();
            lblProjectsDetail.Text = dv[0]["ActivityDetail"].ToString();
            txtActivityCode.Text = dv[0]["ActivityCode"].ToString();
            txtActivity.Text = dv[0]["ActivityName"].ToString();
            getActivityDetail(dv[0]["ActivityCode"].ToString());
            txtEvaluation.Text = dv[0]["Evaluation"].ToString();
            txtEvaIndicators.Text = dv[0]["EvaIndicators"].ToString();
            txtEvaAssessment.Text = dv[0]["EvaAssessment"].ToString();
            txtEvaTool.Text = dv[0]["EvaTool"].ToString();
            txtExpected.Text = dv[0]["Expected"].ToString();
            lblMission.Text = btc.getMissionName(dv[0]["MissionCode"].ToString());
            lblGoals.Text = btc.getGoalsName(dv[0]["GoalsCode"].ToString());
            lblStandardNationName.Text = btc.getStandardNationName(false, dv[0]["ProjectsCode"].ToString());
            lblStandardMinistryName.Text = btc.getStandardMinistryName(false, dv[0]["ProjectsCode"].ToString());
            lblStandard.Text = btc.getStandardName(false, id);
            lblIndicators.Text = btc.getIndicatorsName(false, id);

            strSql = "Select *, txtWeightType = Case WeightType When 0 Then '%' When 1 Then '�ӹǹ' End, txtCkCriterion = Case CkCriterion When 0 Then '����ҹ' When 1 Then '��ҹ' End From Indicators2 Where DelFlag = 0 And ActivityCode = '" + id + "'";
            DataView dv1 = Conn.Select(strSql);
            rptIncicators2.DataSource = dv1;
            rptIncicators2.DataBind();
        }
    }
    private void getActivityDetail(string ActivityCode)
    {    
        DataView dv = btc.getActivityDetailFinished(ActivityCode);
        if (dv.Count != 0)
        {
            txtActivityDetail.Text = dv[0]["ActivityDetail"].ToString();
            lblPlace.Text = dv[0]["Place"].ToString();
            lblType.Text = dv[0]["CostsType"].ToString();
            lblTerm.Text = dv[0]["Term"].ToString() + "/" + dv[0]["YearB"].ToString();
            lblBudgetYear.Text = dv[0]["BudgetYear"].ToString();
            txtTotalAmount.Text = Convert.ToDecimal(dv[0]["TotalAmount"]).ToString("#,##0.00");       
            lblDepartment.Text = dv[0]["DeptName"].ToString();
            lblEmpName.Text = dv[0]["EmpName"].ToString();
            txtSDay.Text = Convert.ToDateTime(dv[0]["SDate"]).ToShortDateString();
            txtEDay.Text = Convert.ToDateTime(dv[0]["EDate"]).ToShortDateString();
        }
    }
}
