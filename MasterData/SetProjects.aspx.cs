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

public partial class SetProjects : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    decimal TotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;

            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);

            ClearAll();
            txtNewYear.Text = btc.getdvDefault("StudyYear", "StudyYear");
            btc.getdllStudyYearForCopy(ddlSearchYear, txtNewYear.Text);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlNewStrategies(0, txtNewYear.Text);

            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
            }
            else
            {
                DataBind();
            }
        }
        ddlNewStrategies.Attributes.Add("onchange", "Cktxt(0);");
    }
    private void getddlStrategies(int mode, string id)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, id);
        }
    }
    private void getddlNewStrategies(int mode, string id)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(1, ddlNewStrategies, id);
        }
    }
    public override void DataBind()
    {
        string StrSql = " Select a.ProjectsCode, a.StrategiesCode, a.StudyYear, a.ProjectsName, a.ProjectsDetail, "
                    + " a.Sort, b.Sort Sort1, b.StrategiesName, IsNull(Sum(c.TotalAmount), 0) TotalAmount "
                    + " From Projects a Left Join Strategies b On a.StrategiesCode = b.StrategiesCode And b.DelFlag = 0 "
                    + " Left Join Activity c On a.ProjectsCode = c.ProjectsCode And c.DelFlag = 0 "
                    + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' " // And (a.CopyFlag Is Null Or a.CopyFlag = 0) "
                    + " And a.ProjectsCode Not In (Select RefProjectsCode From Projects Where DelFlag = 0 And RefProjectsCode Is Not NULL And StudyYear = '" + txtNewYear.Text + "')  "
                    + " And a.SchoolID = '" + CurrentUser.SchoolID + "' ";

        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And a.ProjectsName Like '%" + txtSearch.Text + "%' ";
        }

        DataView dv = Conn.Select(string.Format(StrSql + " Group By a.ProjectsCode, a.StrategiesCode, a.StudyYear, a.ProjectsName, a.ProjectsDetail, a.Sort, b.Sort, b.StrategiesName Order By b.Sort, a.Sort "));

        //เช็คผลรวม
        try
        {
            DataTable dt = dv.ToTable();
            TotalAmount = Convert.ToDecimal(dt.Compute("Sum(TotalAmount)", dv.RowFilter));
        }
        catch (Exception ex)
        {
        }

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.CheckListDataField = "ProjectsCode";
        GridView1.DataBind();
    }
    private void ClearAll()
    {
        txtSearch.Text = "";        
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private Boolean CkDuplicate(string StudyYearNext)
    {
        DataView dvSide = Conn.Select("Select * From Side Where DelFlag = 0 And StudyYear = '" + StudyYearNext + "'");
        if (dvSide.Count != 0)
        {
            return true;
        }

        DataView dvStandard = Conn.Select("Select * From Standard Where DelFlag = 0 And StudyYear = '" + StudyYearNext + "'");
        if (dvStandard.Count != 0)
        {
            return true;
        }

        DataView dvIndicators = Conn.Select("Select b.* From Standard a, Indicators b Where a.StandardCode = b.StandardCode And b.DelFlag = 0 And a.StudyYear = '" + StudyYearNext + "'");
        if (dvIndicators.Count != 0)
        {
            return true;
        }
        return false;
    }
    private void bt_Save()
    {
        string strSql = "";

        if (GridView1.SelectedItems.Length == 0) return;
        for (Int32 i = 0; i <= GridView1.SelectedItems.Length - 1; i++)
        {
            
            //โครงการเดิม
            strSql = " Select * , Isnull(SetBudget, 0) Set_Budget "
                    + " From Projects "
                    + " Where DelFlag = 0 And ProjectsCode = '" + GridView1.SelectedItems[i].ToString() + "' ";
            DataView dvProjects = Conn.Select(strSql);

            if (dvProjects.Count != 0)
            {
                //Update ว่าเคยเลือกคัดลอกไปแล้ว
                //Conn.Update("Projects", "Where ProjectsCode = '" + GridView1.SelectedItems[i].ToString() + "'", "CopyFlag", 1);

                //คัดลอกโครงการ
                string NewProjectsID = Guid.NewGuid().ToString();
                if (cbProject.Checked)
                {
                    Int32 x = Conn.AddNew("Projects", "ProjectsCode, StrategiesCode, MissionCode, GoalsCode, StudyYear, ProjectsName, Purpose, Purpose2, Target, Target2, Period1, ProjectsDetail, Expected, ResponsibleName, ResponsiblePosition, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate, PlanNo, StatuteNo, IdentityName, IdentityName2, SetBudget, RefProjectsCode", NewProjectsID, ddlNewStrategies.SelectedValue, dvProjects[0]["MissionCode"].ToString(), dvProjects[0]["GoalsCode"].ToString(), txtNewYear.Text, dvProjects[0]["ProjectsName"].ToString(), dvProjects[0]["Purpose"].ToString(), dvProjects[0]["Purpose2"].ToString(), dvProjects[0]["Target"].ToString(), dvProjects[0]["Target2"].ToString(), dvProjects[0]["Period1"].ToString(), dvProjects[0]["ProjectsDetail"].ToString(), dvProjects[0]["Expected"].ToString(), dvProjects[0]["ResponsibleName"].ToString(), dvProjects[0]["ResponsiblePosition"].ToString(), dvProjects[0]["ProposeName"].ToString(), dvProjects[0]["ProposePosition"].ToString(), dvProjects[0]["ApprovalName"].ToString(), dvProjects[0]["ApprovalPosition"].ToString(), dvProjects[0]["ApprovalName2"].ToString(), dvProjects[0]["ApprovalPosition2"].ToString(), dvProjects[0]["Sort"].ToString(), 0, 0, CurrentUser.SchoolID, dvProjects[0]["CreateUser"].ToString(), DateTime.Now, CurrentUser.ID, DateTime.Now, dvProjects[0]["PlanNo"].ToString(), dvProjects[0]["StatuteNo"].ToString(), dvProjects[0]["IdentityName"].ToString(), dvProjects[0]["IdentityName2"].ToString(), Convert.ToDecimal(dvProjects[0]["Set_Budget"].ToString()), GridView1.SelectedItems[i].ToString());
                }
                ////มาตรฐานชาติ
                //strSql = " Select * From dtStandardNation "
                //        + " Where ProjectsCode = '" + GridView1.SelectedItems[i].ToString() + "' ";
                //DataView dvStandardNation = Conn.Select(strSql);

                //if (dvStandardNation.Count != 0)
                //{
                //    for (int j = 0; j <= dvStandardNation.Count; j++)
                //    {
                //        Conn.AddNew("dtStandardNation", "ProjectsCode, StandardNationCode", NewProjectsID, dvStandardNation[j]["StandardNationCode"].ToString());
                //    }
                //}
                
                ////มาตรฐานกระทรวง
                //strSql = " Select * From dtStandardMinistry "
                //        + " Where ProjectsCode = '" + GridView1.SelectedItems[i].ToString() + "' ";
                //DataView dvStandardMinistry = Conn.Select(strSql);

                //if (dvStandardMinistry.Count != 0)
                //{
                //    for (int j = 0; j <= dvStandardMinistry.Count; j++)
                //    {
                //        Conn.AddNew("dtStandardMinistry", "ProjectsCode, StandardMinistryCode", NewProjectsID, dvStandardMinistry[j]["StandardMinistryCode"].ToString());
                //    }
                //}

                ////การดำเนินงาน
                //strSql = " Select * From ProjectOperation2 "
                //        + " Where ProjectsCode = '" + GridView1.SelectedItems[i].ToString() + "' ";
                //DataView dvProjectOperation2 = Conn.Select(strSql);

                //if (dvProjectOperation2.Count != 0)
                //{
                //    for (int j = 0; j < dvProjectOperation2.Count; j++)
                //    {
                //        Conn.AddNew("ProjectOperation2", "ProjectsCode, RecNum, Operation2, Budget2", NewProjectsID, Convert.ToDecimal(dvProjectOperation2[j]["RecNum"]), dvProjectOperation2[j]["Operation2"].ToString(), Convert.ToDecimal(dvProjectOperation2[j]["Budget2"]));
                //    }
                //} 
                
                //กิจกรรมเดิม
                strSql = " Select * "
                        + " From Activity "
                        + " Where DelFlag = 0 And ProjectsCode = '" + GridView1.SelectedItems[i].ToString() + "' ";
                DataView dvActivity = Conn.Select(strSql);

                if (dvActivity.Count != 0)
                {
                    //คัดลอกกิจกรรม
                    for (int j = 0; j < dvActivity.Count; j++)
                    {
                        string NewActivityID = Guid.NewGuid().ToString();

                        if (cbActivity.Checked)
                        {
                            Int32 y = Conn.AddNew("Activity", "ActivityCode, StrategiesCode, ProjectsCode, ActivityName, ActivityDetail, Purpose, Target, Target2, Operation1, Period1, Place1, Emp1, Period2, Place2, Emp2, Operation3, Period3, Place3, Emp3, Evaluation, EvaIndicators, EvaAssessment, EvaTool, Expected, VolumeExpect, Place, CostsType, TotalAmount, StudyYear, BudgetYear, Term, YearB, SDate, EDate, AlertDay, Sort, Df, Status, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate, Solutions", NewActivityID, ddlNewStrategies.SelectedValue, NewProjectsID, dvActivity[j]["ActivityName"].ToString(), dvActivity[j]["ActivityDetail"].ToString(), dvActivity[j]["Purpose"].ToString(), dvActivity[j]["Target"].ToString(), dvActivity[j]["Target2"].ToString(), dvActivity[j]["Operation1"].ToString(), dvActivity[j]["Period1"].ToString(), dvActivity[j]["Place1"].ToString(), dvActivity[j]["Emp1"].ToString(), dvActivity[j]["Period2"].ToString(), dvActivity[j]["Place2"].ToString(), dvActivity[j]["Emp2"].ToString(), dvActivity[j]["Operation3"].ToString(), dvActivity[j]["Period3"].ToString(), dvActivity[j]["Place3"].ToString(), dvActivity[j]["Emp3"].ToString(), dvActivity[j]["Evaluation"].ToString(), dvActivity[j]["EvaIndicators"].ToString(), dvActivity[j]["EvaAssessment"].ToString(), dvActivity[j]["EvaTool"].ToString(), dvActivity[j]["Expected"].ToString(), dvActivity[j]["VolumeExpect"].ToString(), dvActivity[j]["Place"].ToString(), dvActivity[j]["CostsType"].ToString(), Convert.ToDouble(dvActivity[j]["TotalAmount"]), txtNewYear.Text, txtNewYear.Text, 1, txtNewYear.Text, Convert.ToDateTime(dvActivity[j]["SDate"]).ToString("s"), Convert.ToDateTime(dvActivity[j]["EDate"]).ToString("s"), 0, dvActivity[j]["Sort"].ToString(), 0, 0, 0, CurrentUser.SchoolID, dvActivity[j]["CreateUser"].ToString(), DateTime.Now, CurrentUser.ID, DateTime.Now, dvActivity[j]["Solutions"].ToString());

                            //งบประมาณ
                            strSql = " Select *, IsNull(TotalBudgetTerm1, 0) Total_BudgetTerm1, IsNull(TotalBudgetTerm2, 0) Total_BudgetTerm2 From CostsDetail Where ActivityCode = '" + dvActivity[j]["ActivityCode"].ToString() + "'";
                            DataView dvCostsDetail = Conn.Select(strSql);

                            if (dvCostsDetail.Count != 0)
                            {
                                for (int k = 0; k < dvCostsDetail.Count; k++)
                                {
                                    //คัดลอกงบประมาณ
                                    Conn.AddNew("CostsDetail", "ActivityCode, ListName, EntryCostsCode, BudgetTypeCode, BudgetTypeOtherName, TotalP, TotalD, TotalG, TotalMoney, TotalMoney2, TotalBudgetTerm1, TotalBudgetTerm2", NewActivityID, dvCostsDetail[k]["ListName"].ToString(), dvCostsDetail[k]["EntryCostsCode"].ToString(), dvCostsDetail[k]["BudgetTypeCode"].ToString(), dvCostsDetail[k]["BudgetTypeOtherName"].ToString(), dvCostsDetail[k]["TotalP"].ToString(), Convert.ToDouble(dvCostsDetail[k]["TotalD"]), Convert.ToDouble(dvCostsDetail[k]["TotalG"]), Convert.ToDouble(dvCostsDetail[k]["TotalMoney"]), 0, Convert.ToDouble(dvCostsDetail[k]["Total_BudgetTerm1"]), Convert.ToDouble(dvCostsDetail[k]["Total_BudgetTerm2"]));
                                }
                            }

                            //การดำเนินงาน
                            strSql = " Select * From ActivityOperation2 "
                                    + " Where ActivityCode = '" + dvActivity[j]["ActivityCode"].ToString() + "' ";
                            DataView dvActivityOperation2 = Conn.Select(strSql);

                            if (dvActivityOperation2.Count != 0)
                            {
                                for (int k = 0; k < dvActivityOperation2.Count; k++)
                                {
                                    Conn.AddNew("ActivityOperation2", "ActivityCode, RecNum, Operation2, Budget2", NewActivityID, Convert.ToDecimal(dvActivityOperation2[k]["RecNum"]), dvActivityOperation2[k]["Operation2"].ToString(), Convert.ToDecimal(dvActivityOperation2[k]["Budget2"]));
                                }
                            }

                            //หน่วยงาน
                            strSql = " Select * From dtAcDept Where ActivityCode = '" + dvActivity[j]["ActivityCode"].ToString() + "'";
                            DataView dvAcDept = Conn.Select(strSql);

                            if (dvAcDept.Count != 0)
                            {
                                for (int k = 0; k < dvAcDept.Count; k++)
                                {
                                    //คัดลอกหน่วยงาน
                                    Conn.AddNew("dtAcDept", "ActivityCode, DeptCode", NewActivityID, dvAcDept[k]["DeptCode"].ToString());
                                }
                            }

                            //ผู้รับผิดชอบ
                            strSql = " Select * From dtAcEmp Where ActivityCode = '" + dvActivity[j]["ActivityCode"].ToString() + "'";
                            DataView dvAcEmp = Conn.Select(strSql);

                            if (dvAcEmp.Count != 0)
                            {
                                for (int k = 0; k < dvAcEmp.Count; k++)
                                {
                                    //คัดลอกผู้รับผิดชอบ
                                    Conn.AddNew("dtAcEmp", "ActivityCode, EmpCode", NewActivityID, dvAcEmp[k]["EmpCode"].ToString());
                                }
                            }
                        }
                        //ตัวชี้วัดเดิม
                        strSql = " Select * "
                                + " From Indicators2 "
                                + " Where DelFlag = 0 And ActivityCode = '" + dvActivity[j]["ActivityCode"].ToString() + "' ";
                        DataView dvIndicators2 = Conn.Select(strSql);

                        if (dvIndicators2.Count != 0)
                        {
                            //คัดลอกตัวชี้วัด
                            for (int k = 0; k < dvIndicators2.Count; k++)
                            {
                                string NewIndicators2ID = Guid.NewGuid().ToString();

                                if (cbIndicator2.Checked)
                                {
                                    Int32 z = Conn.AddNew("Indicators2", "Indicators2Code, StrategiesCode, ProjectsCode, ActivityCode, IndicatorsName2, Weight, WeightType, OffAll, OffThat, APerCent, CkCriterion, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", NewIndicators2ID, ddlNewStrategies.SelectedValue, NewProjectsID, NewActivityID, dvIndicators2[k]["IndicatorsName2"].ToString(), dvIndicators2[k]["Weight"].ToString(), dvIndicators2[k]["WeightType"].ToString(), Convert.ToDouble(dvIndicators2[k]["OffAll"]), 0, 0, 0, dvIndicators2[k]["Sort"].ToString(), 1, 0, CurrentUser.SchoolID, dvIndicators2[k]["CreateUser"].ToString(), DateTime.Now, CurrentUser.ID, DateTime.Now);
                                }
                            }
                        }
                    }
                }
            }
        }
        btc.Msg_Head(Img1, MsgHead, true, "1", 1);
        DataBind();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        bt_Save();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlNewStrategies(0, txtNewYear.Text);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    public decimal GetBudget(decimal Budget)
    {
        //TotalAmount += Budget;
        return Budget;
    }
    public decimal GetTotalBudget()
    {
        return TotalAmount;
    }
    protected void cbIndicator2_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIndicator2.Checked)
        {
            cbActivity.Checked = true;
            cbActivity.Enabled = false;
        }
        else
        {
            cbActivity.Enabled = true;
        }
    }
}
