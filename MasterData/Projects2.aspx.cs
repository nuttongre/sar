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
using System.Text;

public partial class Projects2 : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    DataView dvMoney;
    DataView dvQuarterBudget;

    decimal TotalMoney;
    decimal TotalQuarterBudget;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            if (!string.IsNullOrEmpty(Request["Cr"]))
            {
                btc.Msg_Head(Img1, MsgHead, true, Request["ckmode"], Convert.ToInt32(Request["Cr"]));
            }
            getddlYear(0);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            
            string mode = Request.QueryString["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        ClearAll();
                        getddlYear(1);
                        SetItem();
                        string gotoid = Request.QueryString["id"];
                        getddlStrategies(1, ddlYearS.SelectedValue);

                        if (!string.IsNullOrEmpty(gotoid))
                        {
                            ddlStrategies.SelectedValue = gotoid;
                        }
                        btc.GenSort(txtSort, "Projects", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        ClearAll();
                        getddlYear(1);
                        SetItem();
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
        txtActivityName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtActivitySubName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProjectsDetail.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProjects.Attributes.Add("onkeyup", "Cktxt(0);");
        txtPurpose.Attributes.Add("onkeyup", "Cktxt(0);");
        txtResponsibleName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtResponsiblePosition.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProposeName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtProposePosition.Attributes.Add("onkeyup", "Cktxt(0);");
        txtApprovalName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtApprovalPosition.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "return Cktxt(0);");    
    }
    private void SetItem()
    {
        getddlMission(ddlYearS.SelectedValue);
        getddlGoals(ddlYearS.SelectedValue);
        getcblPolicy(ddlYearS.SelectedValue);
        getcblStandardNation(ddlYearS.SelectedValue);
        getcblStandardMinistry(ddlYearS.SelectedValue);
        getcblStrategySPT(ddlYearS.SelectedValue);
        getcblStrategySPM(ddlYearS.SelectedValue);
        getcblStandardSPM(ddlYearS.SelectedValue);
        getcblStrategic(ddlYearS.SelectedValue);
        getddlQuarter();

        btc.IdentityNameEnable(lblIdentityName, txtIdentityName, "IdentityName", "iNameShow", divIdentityName);
        btc.IdentityNameEnable(lblIdentityName2, txtIdentityName2, "IdentityName2", "iNameShow2", divIdentityName2);
        btc.IdentityNameEnable(lblPlanNo, txtPlanNo, "PlanNo", "ckPlanNo", divPlanNo);
        btc.IdentityNameEnable(lblStatute, txtStatute, "", "ckStatute", divStatute);
        btc.IdentityNameCblEnable(lblStrategySPT, cblStrategySPT, "", "ckStrategySPT", divStrategySPT);
        btc.IdentityNameCblEnable(lblStrategySPM, cblStrategySPM, "", "ckStrategySPM", divStrategySPM);
        btc.IdentityNameCblEnable(lblStandardSPM, cblStandardSPM, "", "ckStandardSPM", divStandardSPM);
        btc.IdentityNameCblEnable(lblPolicy, cblPolicy, "", "ckPolicy", divPolicy);
        btc.IdentityNameCblEnable(lblStandardNation, cblStandardNation, "", "ckStandardNation", divStandardNation);
        btc.IdentityNameCblEnable(lblStandardMinistry, cblStandardMinistry, "", "ckStandardMinistry", divStandardMinistry);
        btc.IdentityNameCblEnable(lblStrategic, cblStrategic, "", "ckStrategic", divStrategic);
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
            btc.getdllStudyYear(ddlYearS);
            btc.getDefault(ddlYearS, "StudyYear", "StudyYear");
        }
    }
    private void getddlStrategies(int mode, string id)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, id);
            if (Cookie.GetValue2("PjStrategies") == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Cookie.GetValue2("PjStrategies").ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch2.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlStrategies(1, ddlStrategies, id);
        }
    }
    private void getddlMission(string StudyYear)
    {
        btc.getddlMission(ddlMission, StudyYear);
    }
    private void getddlGoals(string StudyYear)
    {
        btc.getddlGoals(ddlGoals, StudyYear);
    }
    private void getcblPolicy(string StudyYear)
    {
        btc.getcblPolicy(cblPolicy, StudyYear);
    }
    private void getcblStandardNation(string StudyYear)
    {
        btc.getcblStandardNation(cblStandardNation, StudyYear);
    }
    private void getcblStandardMinistry(string StudyYear)
    {
        btc.getcblStandardMinistry(cblStandardMinistry, StudyYear);
    }
    private void getcblStrategySPT(string StudyYear)
    {
        btc.getcblStrategySPT(cblStrategySPT, StudyYear);
    }
    private void getcblStrategySPM(string StudyYear)
    {
        btc.getcblStrategySPM(cblStrategySPM, StudyYear);
    }
    private void getcblStandardSPM(string StudyYear)
    {
        btc.getcblStandardSPM(cblStandardSPM, StudyYear);
    }
    private void getcblStrategic(string StudyYear)
    {
        btc.getcblStrategic(cblStrategic, StudyYear);
    }
    private void getddlQuarter()
    {
        btc.getddlQuarter(ddlQuarter);
    }
    public override void DataBind()
    {
        string StrSql = " Select a.ProjectsCode, a.StrategiesCode, a.StudyYear, a.ProjectsName, a.Df, d.EmpID, d.EmpName, "
                    + " a.Sort, b.Sort Sort1, b.StrategiesName, IsNull(Sum(c.TotalAmount), 0) TotalAmount "
                    + " From Projects a Left Join Strategies b On a.StrategiesCode = b.StrategiesCode "
                    + " Left Join Activity c On a.ProjectsCode = c.ProjectsCode And c.DelFlag = 0 "
                    + " Left Join Employee d On a.CreateUser = d.EmpID And d.DelFlag = 0 "
                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' "
                    + " And a.SchoolID = '" + CurrentUser.SchoolID + "' ";

        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And a.ProjectsName Like '%" + txtSearch.Text + "%' ";
        }

        DataView dv = Conn.Select(string.Format(StrSql + " Group By a.ProjectsCode, a.StrategiesCode, a.StudyYear, a.ProjectsName, a.Df, d.EmpID, d.EmpName, a.Sort, b.Sort, b.StrategiesName Order By a.Sort Desc "));

        DataTable dt = new DataTable();
        dt.Columns.Add("ProjectsCode", typeof(string));
        dt.Columns.Add("ProjectsName", typeof(string));
        dt.Columns.Add("TotalAmount", typeof(decimal));
        dt.Columns.Add("Df", typeof(Boolean));
        dt.Columns.Add("EmpID", typeof(string));
        dt.Columns.Add("EmpName", typeof(string));
        dt.Columns.Add("StandardNation", typeof(string));
        dt.Columns.Add("StandardMinistry", typeof(string));

        foreach (DataRowView dr in dv)
        {
            dt.Rows.Add(dr["ProjectsCode"], dr["ProjectsName"], dr["TotalAmount"], dr["Df"], dr["EmpID"], dr["EmpName"], "", "");
        }

        for (int i = 0; i <= dv.Count - 1; i++)
        {
            dt.Rows[i]["StandardNation"] = btc.getStandardNationName(false, dv[i]["ProjectsCode"].ToString());
            dt.Rows[i]["StandardMinistry"] = btc.getStandardMinistryName(false, dv[i]["ProjectsCode"].ToString());
        }

        GridView1.DataSource = dt;
        lblSearchTotal.InnerText = dt.Rows.Count.ToString();
        GridView1.DataBind();

        //----GrandTotal-----------
        StrSql = " Select IsNull(Sum(c.TotalAmount), 0) TotalAmount "
                    + " From Projects a Left Join Strategies b On a.StrategiesCode = b.StrategiesCode "
                    + " Left Join Activity c On a.ProjectsCode = c.ProjectsCode And c.DelFlag = 0 "
                    + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' "
                    + " And a.SchoolID = '" + CurrentUser.SchoolID + "' ";
        DataView dvTotal = Conn.Select(StrSql);
        ToltalBudget.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount"]).ToString("#,##0.00") : "0.00";
        //----EndGrandTotal-----------

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select * From Projects Where ProjectsCode = '" + id + "'"));
        DataView dv0 = Conn.Select(string.Format("Select PolicyCode From dtPolicy Where ProjectsCode = '" + id + "' "));
        DataView dv1 = Conn.Select(string.Format("Select StandardNationCode From dtStandardNation Where ProjectsCode = '" + id + "' "));
        DataView dv2 = Conn.Select(string.Format("Select StandardMinistryCode From dtStandardMinistry Where ProjectsCode = '" + id + "' "));
        DataView dv3 = Conn.Select(string.Format("Select StrategySPTCode From dtStrategySPT Where ProjectsCode = '" + id + "' "));
        DataView dv4 = Conn.Select(string.Format("Select StrategySPMCode From dtStrategySPM Where ProjectsCode = '" + id + "' "));
        DataView dv5 = Conn.Select(string.Format("Select StandardSPMCode From dtStandardSPM Where ProjectsCode = '" + id + "' "));
        DataView dv6 = Conn.Select(string.Format("Select StrategicCode From dtStrategic Where ProjectsCode = '" + id + "' "));

        if (dv.Count != 0)
        {
            ddlYearS.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlStrategies(1, ddlYearS.SelectedValue);
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            if (btc.CkUseData(id, "ProjectsCode", "Activity", " And DelFlag = 0 "))
            {
                ddlYearS.Enabled = false;
                //ddlStrategies.Enabled = false;
            }
            txtActivityName.Text = dv[0]["ActivityName"].ToString();
            txtActivitySubName.Text = dv[0]["ActivitySubName"].ToString();
            txtYearPlan4.Text = dv[0]["YearPlan4"].ToString();
            ddlMission.SelectedValue = dv[0]["MissionCode"].ToString();
            ddlGoals.SelectedValue = dv[0]["GoalsCode"].ToString();
            txtProjects.Text = dv[0]["ProjectsName"].ToString();
            txtIdentityName.Text = dv[0]["IdentityName"].ToString();
            txtIdentityName2.Text = dv[0]["IdentityName2"].ToString();
            txtStatute.Text = dv[0]["StatuteNo"].ToString();
            txtPlanNo.Text = dv[0]["PlanNo"].ToString();
            getcblStrategySPT(ddlYearS.SelectedValue);
            getcblStrategySPM(ddlYearS.SelectedValue);
            getcblStandardSPM(ddlYearS.SelectedValue);
            getcblStrategic(ddlYearS.SelectedValue);
            txtPurpose.Text = dv[0]["Purpose"].ToString();
            txtProjectsDetail.Text = dv[0]["ProjectsDetail"].ToString();
            txtResponsibleName.Text = dv[0]["ResponsibleName"].ToString();
            txtResponsiblePosition.Text = dv[0]["ResponsiblePosition"].ToString();
            txtProposeName.Text = dv[0]["ProposeName"].ToString();
            txtProposePosition.Text = dv[0]["ProposePosition"].ToString();
            txtApprovalName.Text = dv[0]["ApprovalName"].ToString();
            txtApprovalPosition.Text = dv[0]["ApprovalPosition"].ToString();
            txtApprovalName2.Text = dv[0]["ApprovalName2"].ToString();
            txtApprovalPosition2.Text = dv[0]["ApprovalPosition2"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
        }
        btc.getCreateUpdateUser(lblCreate, lblUpdate, "Projects", "ProjectsCode", id);

        if (dv0.Count != 0)
        {
            for (int i = 0; i <= cblPolicy.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv0.Count - 1; j++)
                {
                    if (cblPolicy.Items[i].Value == dv0[j]["PolicyCode"].ToString())
                    {
                        cblPolicy.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv1.Count != 0)
        {
            for (int i = 0; i <= cblStandardNation.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv1.Count - 1; j++)
                {
                    if (cblStandardNation.Items[i].Value == dv1[j]["StandardNationCode"].ToString())
                    {
                        cblStandardNation.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv2.Count != 0)
        {
            for (int i = 0; i <= cblStandardMinistry.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv2.Count - 1; j++)
                {
                    if (cblStandardMinistry.Items[i].Value == dv2[j]["StandardMinistryCode"].ToString())
                    {
                        cblStandardMinistry.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv3.Count != 0)
        {
            for (int i = 0; i <= cblStrategySPT.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv3.Count - 1; j++)
                {
                    if (cblStrategySPT.Items[i].Value == dv3[j]["StrategySPTCode"].ToString())
                    {
                        cblStrategySPT.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv4.Count != 0)
        {
            for (int i = 0; i <= cblStrategySPM.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv4.Count - 1; j++)
                {
                    if (cblStrategySPM.Items[i].Value == dv4[j]["StrategySPMCode"].ToString())
                    {
                        cblStrategySPM.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        if (dv5.Count != 0)
        {
            for (int i = 0; i <= cblStandardSPM.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv5.Count - 1; j++)
                {
                    if (cblStandardSPM.Items[i].Value == dv5[j]["StandardSPMCode"].ToString())
                    {
                        cblStandardSPM.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }
        if (dv6.Count != 0)
        {
            for (int i = 0; i <= cblStrategic.Items.Count - 1; i++)
            {
                for (int j = 0; j <= dv6.Count - 1; j++)
                {
                    if (cblStrategic.Items[i].Value == dv6[j]["StrategicCode"].ToString())
                    {
                        cblStrategic.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        string strSql = " Select * "
                + " From ProjectsMoneyDetail "
                + " Where ProjectsCode = '{0}' ";
        dvMoney = Conn.Select(string.Format(strSql + " Order By MoneyNo ", id));

        if (dvMoney.Count != 0)
        {
            btDelMoney.Visible = true;
            if (Session["Money"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("id");
                dt1.Columns.Add("MoneyNo");
                dt1.Columns.Add("MoneyDetail");
                dt1.Columns.Add("Money");

                DataRow dr;
                for (int i = 0; i < dvMoney.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["id"] = i;
                    dr["MoneyNo"] = dvMoney[i]["MoneyNo"].ToString();
                    dr["MoneyDetail"] = dvMoney[i]["MoneyDetail"].ToString();
                    dr["Money"] = dvMoney[i]["Money"].ToString();
                    dt1.Rows.Add(dr);
                }
                dvMoney = dt1.DefaultView;
                Session["Money"] = dt1;

                GridViewMoney.DataSource = dvMoney;
                GridViewMoney.CheckListDataField = "id";
                GridViewMoney.DataBind();
            }
        }

        strSql = " Select * "
                + " From ProjectsQuarterDetail "
                + " Where ProjectsCode = '{0}' ";
        dvQuarterBudget = Conn.Select(string.Format(strSql + " Order By QuarterNo, QuarterSort ", id));

        if (dvQuarterBudget.Count != 0)
        {
            btDelQuarter.Visible = true;
            if (Session["Quarter"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("id");
                dt1.Columns.Add("QuarterNo");
                dt1.Columns.Add("QuarterSort");
                dt1.Columns.Add("QuarterTime");
                dt1.Columns.Add("QuarterPlan");
                dt1.Columns.Add("QuarterTarget");
                dt1.Columns.Add("QuarterBudget");

                DataRow dr;
                for (int i = 0; i < dvQuarterBudget.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["id"] = i;
                    dr["QuarterNo"] = dvQuarterBudget[i]["QuarterNo"].ToString();
                    dr["QuarterSort"] = dvQuarterBudget[i]["QuarterSort"].ToString();
                    dr["QuarterTime"] = dvQuarterBudget[i]["QuarterTime"].ToString();
                    dr["QuarterPlan"] = dvQuarterBudget[i]["QuarterPlan"].ToString();
                    dr["QuarterTarget"] = dvQuarterBudget[i]["QuarterTarget"].ToString();
                    dr["QuarterBudget"] = dvQuarterBudget[i]["QuarterBudget"].ToString();
                    dt1.Rows.Add(dr);
                }
                dvQuarterBudget = dt1.DefaultView;
                Session["Quarter"] = dt1;

                GridViewQuarter.DataSource = dvQuarterBudget;
                GridViewQuarter.CheckListDataField = "id";
                GridViewQuarter.DataBind();
            }
        }
    }
    private void ClearAll()
    {
        Session.Remove("Money");
        GridViewMoney.DataSource = null;
        GridViewMoney.DataBind();

        Session.Remove("Quarter");
        GridViewQuarter.DataSource = null;
        GridViewQuarter.DataBind();

        txtProjects.Text = "";
        txtProjectsDetail.Text = "";
        txtPurpose.Text = "";
        txtProjectsDetail.Text = "";
        txtResponsibleName.Text = "";
        txtResponsiblePosition.Text = "";
        txtProposeName.Text = "";
        txtProposePosition.Text = "";
        txtApprovalName.Text = "";
        txtApprovalPosition.Text = "";
        txtApprovalName2.Text = "";
        txtApprovalPosition2.Text = "";
        ddlGoals.SelectedIndex = 0;
        ddlMission.SelectedIndex = 0;
        txtSearch.Text = "";
        cblStandardMinistry.ClearSelection();
        cblPolicy.ClearSelection();
        cblStandardNation.ClearSelection();
        cblStrategySPT.ClearSelection();
        cblStrategySPM.ClearSelection();
        cblStandardSPM.ClearSelection();
        cblStrategic.ClearSelection();
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            //btc.UpdateDefault("Projects");

            Int32 i = Conn.AddNew("Projects", "ProjectsCode, StrategiesCode, ActivityName, ActivitySubName, YearPlan4, MissionCode, GoalsCode, StudyYear, ProjectsName, Purpose, ProjectsDetail, ResponsibleName, ResponsiblePosition, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2, PlanNo, StatuteNo, IdentityName, IdentityName2, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlStrategies.SelectedValue, txtActivityName.Text, txtActivitySubName.Text, txtYearPlan4.Text, ddlMission.SelectedValue, ddlGoals.SelectedValue, ddlYearS.SelectedValue, txtProjects.Text, txtPurpose.Text, txtProjectsDetail.Text, txtResponsibleName.Text, txtResponsiblePosition.Text, txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, txtPlanNo.Text, txtStatute.Text, txtIdentityName.Text, txtIdentityName2.Text, txtSort.Text, 1, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

            for (int j = 0; j <= cblPolicy.Items.Count - 1; j++)
            {
                if (cblPolicy.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtPolicy (ProjectsCode, PolicyCode)VALUES('{0}','{1}');", NewID, cblPolicy.Items[j].Value);
                    //i = Conn.AddNew("dtPolicy", "ProjectsCode, PolicyCode", NewID, cblPolicy.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblStandardNation.Items.Count - 1; j++)
            {
                if (cblStandardNation.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtStandardNation (ProjectsCode, StandardNationCode)VALUES('{0}','{1}');", NewID, cblStandardNation.Items[j].Value);
                    //i = Conn.AddNew("dtStandardNation", "ProjectsCode, StandardNationCode", NewID, cblStandardNation.Items[j].Value);
                }
            }

            for (int j = 0; j <= cblStandardMinistry.Items.Count - 1; j++)
            {
                if (cblStandardMinistry.Items[j].Selected)
                {
                    strbSql.AppendFormat("INSERT INTO dtStandardMinistry (ProjectsCode, StandardMinistryCode)VALUES('{0}','{1}');", NewID, cblStandardMinistry.Items[j].Value);
                    //i = Conn.AddNew("dtStandardMinistry", "ProjectsCode, StandardMinistryCode", NewID, cblStandardMinistry.Items[j].Value);
                }
            }

            if (btc.ckIdentityName("ckStrategySPT"))
            {
                for (int j = 0; j <= cblStrategySPT.Items.Count - 1; j++)
                {
                    if (cblStrategySPT.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPT (ProjectsCode, StrategySPTCode)VALUES('{0}','{1}');", NewID, cblStrategySPT.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPT", "ProjectsCode, StrategySPTCode", NewID, cblStrategySPT.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPM"))
            {
                for (int j = 0; j <= cblStrategySPM.Items.Count - 1; j++)
                {
                    if (cblStrategySPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPM (ProjectsCode, StrategySPMCode)VALUES('{0}','{1}');", NewID, cblStrategySPM.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPM", "ProjectsCode, StrategySPMCode", NewID, cblStrategySPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardSPM"))
            {
                for (int j = 0; j <= cblStandardSPM.Items.Count - 1; j++)
                {
                    if (cblStandardSPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardSPM (ProjectsCode, StandardSPMCode)VALUES('{0}','{1}');", NewID, cblStandardSPM.Items[j].Value);
                        //i = Conn.AddNew("dtStandardSPM", "ProjectsCode, StandardSPMCode", NewID, cblStandardSPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategic"))
            {
                for (int j = 0; j <= cblStrategic.Items.Count - 1; j++)
                {
                    if (cblStrategic.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategic (ProjectsCode, StrategicCode)VALUES('{0}','{1}');", NewID, cblStrategic.Items[j].Value);
                        //i = Conn.AddNew("dtStrategic", "ProjectsCode, StrategicCode", NewID, cblStrategic.Items[j].Value);
                    }
                }
            }

            //Money
            if (Session["Money"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Money"];
                dvMoney = dt1.DefaultView;
                for (int j = 0; j < dvMoney.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ProjectsMoneyDetail (ProjectsCode, MoneyNo, MoneyDetail, Money)VALUES('{0}',{1},N'{2}',{3});", 
                        NewID, Convert.ToInt32(dvMoney[j]["MoneyNo"]), dvMoney[j]["MoneyDetail"].ToString(), Convert.ToDecimal(dvMoney[j]["Money"]));
                    //Conn.AddNew("ProjectsMoneyDetail", "ProjectsCode, MoneyNo, MoneyDetail, Money", NewID, Convert.ToInt32(dvMoney[j]["MoneyNo"]), dvMoney[j]["MoneyDetail"].ToString(), Convert.ToDecimal(dvMoney[j]["Money"]));
                }
            }

            //Quarter
            if (Session["Quarter"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Quarter"];
                dvQuarterBudget = dt1.DefaultView;
                for (int j = 0; j < dvQuarterBudget.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ProjectsQuarterDetail (ProjectsCode, QuarterNo, QuarterSort, QuarterTime, QuarterPlan, QuarterTarget, QuarterBudget)VALUES('{0}',{1},{2},N'{3}',N'{4}',N'{5}',{6});",
                        NewID, Convert.ToInt32(dvQuarterBudget[j]["QuarterNo"]), Convert.ToInt32(dvQuarterBudget[j]["QuarterSort"]), dvQuarterBudget[j]["QuarterTime"].ToString(), dvQuarterBudget[j]["QuarterPlan"].ToString(), dvQuarterBudget[j]["QuarterTarget"].ToString(), Convert.ToDecimal(dvQuarterBudget[j]["QuarterBudget"]));
                    //Conn.AddNew("ProjectsQuarterDetail", "ProjectsCode, QuarterNo, QuarterSort, QuarterTime, QuarterPlan, QuarterTarget, QuarterBudget", NewID, Convert.ToInt32(dvQuarterBudget[j]["QuarterNo"]), Convert.ToInt32(dvQuarterBudget[j]["QuarterSort"]), dvQuarterBudget[j]["QuarterTime"].ToString(), dvQuarterBudget[j]["QuarterPlan"].ToString(), dvQuarterBudget[j]["QuarterTarget"].ToString(), Convert.ToDecimal(dvQuarterBudget[j]["QuarterBudget"]));
                }
            }
            result = Conn.Execute(strbSql.ToString());

            if (CkAgain == "N")
            {
                Response.Redirect("Projects2.aspx?ckmode=1&Cr=" + i);   
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                SetItem();
                btc.GenSort(txtSort, "Projects", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                GridView2.Visible = true;
                DataBind();
            }
            if (CkAgain == "G")
            {
                Response.Redirect("Activity.aspx?mode=1&id=" + NewID + "&syear=" + ddlYearS.SelectedValue + "&stgid=" + ddlStrategies.SelectedValue);
            }     
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("Projects", "Where ProjectsCode = '" + Request["id"] + "' ", "StrategiesCode, ActivityName, ActivitySubName, YearPlan4, MissionCode, GoalsCode, StudyYear, ProjectsName, Purpose, ProjectsDetail, ResponsibleName, ResponsiblePosition, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2, PlanNo, StatuteNo, IdentityName, IdentityName2, Sort, SchoolID, UpdateUser, UpdateDate", ddlStrategies.SelectedValue, txtActivityName.Text, txtActivitySubName.Text, txtYearPlan4.Text, ddlMission.SelectedValue, ddlGoals.SelectedValue, ddlYearS.SelectedValue, txtProjects.Text, txtPurpose.Text, txtProjectsDetail.Text, txtResponsibleName.Text, txtResponsiblePosition.Text, txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, txtPlanNo.Text, txtStatute.Text, txtIdentityName.Text, txtIdentityName2.Text, txtSort.Text, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now);

            strbSql.AppendFormat("UPDATE Activity SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);
            strbSql.AppendFormat("UPDATE Indicators2 SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);
            strbSql.AppendFormat("UPDATE Evaluation SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);
            strbSql.AppendFormat("UPDATE QALink SET StrategiesCode = '{0}' Where ProjectsCode = '{1}';", ddlStrategies.SelectedValue, Request["id"]);

            if (btc.ckIdentityName("ckPolicy"))
            {
                strbSql.AppendFormat("Delete dtPolicy Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtPolicy", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblPolicy.Items.Count - 1; j++)
                {
                    if (cblPolicy.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtPolicy (ProjectsCode, PolicyCode)VALUES('{0}','{1}');", Request["id"], cblPolicy.Items[j].Value);
                        //i = Conn.AddNew("dtPolicy", "ProjectsCode, PolicyCode", Request["id"], cblPolicy.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardNation"))
            {
                strbSql.AppendFormat("Delete dtStandardNation Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStandardNation", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStandardNation.Items.Count - 1; j++)
                {
                    if (cblStandardNation.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardNation (ProjectsCode, StandardNationCode)VALUES('{0}','{1}');", Request["id"], cblStandardNation.Items[j].Value);
                        //i = Conn.AddNew("dtStandardNation", "ProjectsCode, StandardNationCode", Request["id"], cblStandardNation.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardMinistry"))
            {
                strbSql.AppendFormat("Delete dtStandardMinistry Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStandardMinistry", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStandardMinistry.Items.Count - 1; j++)
                {
                    if (cblStandardMinistry.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardMinistry (ProjectsCode, StandardMinistryCode)VALUES('{0}','{1}');", Request["id"], cblStandardMinistry.Items[j].Value);
                        //i = Conn.AddNew("dtStandardMinistry", "ProjectsCode, StandardMinistryCode", Request["id"], cblStandardMinistry.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPT"))
            {
                strbSql.AppendFormat("Delete dtStrategySPT Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStrategySPT", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStrategySPT.Items.Count - 1; j++)
                {
                    if (cblStrategySPT.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPT (ProjectsCode, StrategySPTCode)VALUES('{0}','{1}');", Request["id"], cblStrategySPT.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPT", "ProjectsCode, StrategySPTCode", Request["id"], cblStrategySPT.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategySPM"))
            {
                strbSql.AppendFormat("Delete dtStrategySPM Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStrategySPM", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStrategySPM.Items.Count - 1; j++)
                {
                    if (cblStrategySPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategySPM (ProjectsCode, StrategySPMCode)VALUES('{0}','{1}');", Request["id"], cblStrategySPM.Items[j].Value);
                        //i = Conn.AddNew("dtStrategySPM", "ProjectsCode, StrategySPMCode", Request["id"], cblStrategySPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStandardSPM"))
            {
                strbSql.AppendFormat("Delete dtStandardSPM Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStandardSPM", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStandardSPM.Items.Count - 1; j++)
                {
                    if (cblStandardSPM.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStandardSPM (ProjectsCode, StandardSPMCode)VALUES('{0}','{1}');", Request["id"], cblStandardSPM.Items[j].Value);
                        //i = Conn.AddNew("dtStandardSPM", "ProjectsCode, StandardSPMCode", Request["id"], cblStandardSPM.Items[j].Value);
                    }
                }
            }

            if (btc.ckIdentityName("ckStrategic"))
            {
                strbSql.AppendFormat("Delete dtStrategic Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("dtStrategic", "Where ProjectsCode = '" + Request["id"] + "' ");
                for (int j = 0; j <= cblStrategic.Items.Count - 1; j++)
                {
                    if (cblStrategic.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtStrategic (ProjectsCode, StrategicCode)VALUES('{0}','{1}');", Request["id"], cblStrategic.Items[j].Value);
                        //i = Conn.AddNew("dtStrategic", "ProjectsCode, StrategicCode", Request["id"], cblStrategic.Items[j].Value);
                    }
                }
            }

            //Money
            if (Session["Money"] != null)
            {
                strbSql.AppendFormat("Delete ProjectsMoneyDetail Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("ProjectsMoneyDetail", "Where ProjectsCode = '" + Request["id"] + "'");
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Money"];
                dvMoney = dt1.DefaultView;
                for (int j = 0; j < dvMoney.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ProjectsMoneyDetail (ProjectsCode, MoneyNo, MoneyDetail, Money)VALUES('{0}',{1},N'{2}',{3});",
                        Request["id"], Convert.ToInt32(dvMoney[j]["MoneyNo"]), dvMoney[j]["MoneyDetail"].ToString(), Convert.ToDecimal(dvMoney[j]["Money"]));
                    //Conn.AddNew("ProjectsMoneyDetail", "ProjectsCode, MoneyNo, MoneyDetail, Money", Request["id"], Convert.ToInt32(dvMoney[j]["MoneyNo"]), dvMoney[j]["MoneyDetail"].ToString(), Convert.ToDecimal(dvMoney[j]["Money"]));
                }
            }

            //Quarter
            if (Session["Quarter"] != null)
            {
                strbSql.AppendFormat("Delete ProjectsQuarterDetail Where ProjectsCode = '{0}';", Request["id"]);
                //Conn.Delete("ProjectsQuarterDetail", "Where ProjectsCode = '" + Request["id"] + "'");
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Quarter"];
                dvQuarterBudget = dt1.DefaultView;
                for (int j = 0; j < dvQuarterBudget.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ProjectsQuarterDetail (ProjectsCode, QuarterNo, QuarterSort, QuarterTime, QuarterPlan, QuarterTarget, QuarterBudget)VALUES('{0}',{1},{2},N'{3}',N'{4}',N'{5}',{6});",
                        Request["id"], Convert.ToInt32(dvQuarterBudget[j]["QuarterNo"]), Convert.ToInt32(dvQuarterBudget[j]["QuarterSort"]), dvQuarterBudget[j]["QuarterTime"].ToString(), dvQuarterBudget[j]["QuarterPlan"].ToString(), dvQuarterBudget[j]["QuarterTarget"].ToString(), Convert.ToDecimal(dvQuarterBudget[j]["QuarterBudget"]));
                    //Conn.AddNew("ProjectsQuarterDetail", "ProjectsCode, QuarterNo, QuarterSort, QuarterTime, QuarterPlan, QuarterTarget, QuarterBudget", Request["id"], Convert.ToInt32(dvQuarterBudget[j]["QuarterNo"]), Convert.ToInt32(dvQuarterBudget[j]["QuarterSort"]), dvQuarterBudget[j]["QuarterTime"].ToString(), dvQuarterBudget[j]["QuarterPlan"].ToString(), dvQuarterBudget[j]["QuarterTarget"].ToString(), Convert.ToDecimal(dvQuarterBudget[j]["QuarterBudget"]));
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Response.Redirect("Projects2.aspx?ckmode=2&Cr=" + i);  
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
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        if (String.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select ProjectsCode From Activity Where ProjectsCode = '" + id + "' And DelFlag = '0' "));
        if (dv.Count > 0)
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            Int32 i = Conn.Update("Projects", "Where ProjectsCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);

            strbSql.AppendFormat("Delete dtPolicy Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStandardNation Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStandardMinistry Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategySPT Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategySPM Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStandardSPM Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtStrategic Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete ProjectsMoneyDetail Where ProjectsCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete ProjectsQuarterDetail Where ProjectsCode = '{0}';", Request["id"]);

            //Conn.Delete("dtPolicy", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtStandardNation", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtStandardMinistry", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtStrategySPT", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtStrategySPM", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtStandardSPM", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtStrategic", "Where ProjectsCode = '" + Request["id"] + "' ");
            //Conn.Delete("ProjectsMoneyDetail", "Where ProjectsCode = '" + Request["id"] + "'");
            //Conn.Delete("ProjectsQuarterDetail", "Where ProjectsCode = '" + Request["id"] + "'");

            result = Conn.Execute(strbSql.ToString());
            Response.Redirect("Projects2.aspx?ckmode=3&Cr=" + i);
        }
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlYearS_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlMission(ddlYearS.SelectedValue);
        getddlGoals(ddlYearS.SelectedValue);
        getcblPolicy(ddlYearS.SelectedValue);
        getcblStandardNation(ddlYearS.SelectedValue);
        getcblStandardMinistry(ddlYearS.SelectedValue);
        getddlStrategies(1, ddlYearS.SelectedValue);
        btc.GenSort(txtSort, "Projects", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("PjStrategies", ddlSearch2.SelectedValue);
        DataBind();
    }
    decimal TotalAmount;
    public decimal GetBudget(decimal Budget)
    {
        TotalAmount += Budget;
        return Budget;
    }
    public decimal GetTotalBudget()
    {
        return TotalAmount;
    }
    protected void btaddMoney_Click(object sender, EventArgs e)
    {
        if (Session["Money"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("MoneyNo");
            dt1.Columns.Add("MoneyDetail");
            dt1.Columns.Add("Money");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["MoneyNo"] = txtMoneyNo.Text;
            dr["MoneyDetail"] = txtMoneyDetail.Text;
            dr["Money"] = txtMoney.Text;
            dt1.Rows.Add(dr);

            dvMoney = dt1.DefaultView;
            Session["Money"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Money"];

            if (txtidMoney.Text == "")
            {
                DataView ckdv = ((DataTable)Session["Money"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int i = 0; i < ckdv.Count; i++)
                    {
                        if (ckdv[i]["MoneyNo"].ToString() == txtMoneyNo.Text)
                        {
                           lblckMoney.Visible = true;
                            return;
                        }
                    }
                }
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["MoneyNo"] = txtMoneyNo.Text;
                dr["MoneyDetail"] = txtMoneyDetail.Text;
                dr["Money"] = txtMoney.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                DataView ckdv = ((DataTable)Session["Money"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    //for (int j = 0; j < ckdv.Count; j++)
                    //{
                    //    if (ckdv[j]["id"].ToString() != txtidMoney.Text)
                    //    {
                    //        lblckMoney.Visible = true;
                    //        return;
                    //    }
                    //}
                }
                Int32 i = Convert.ToInt32(txtidMoney.Text);
                dt1.Rows[i]["MoneyNo"] = txtMoneyNo.Text;
                dt1.Rows[i]["MoneyDetail"] = txtMoneyDetail.Text;
                dt1.Rows[i]["Money"] = txtMoney.Text;
            }
            lblckMoney.Visible = false;

            dvMoney = dt1.DefaultView;
            Session["Money"] = dt1;
        }
        dvMoney.Sort = "MoneyNo";
        ClearMoney();
        GridViewMoney.DataSource = dvMoney;
        GridViewMoney.CheckListDataField = "id";
        GridViewMoney.DataBind();
        if (dvMoney.Count > 0)
        {
            btDelMoney.Visible = true;
        }
    }
    private void ClearMoney()
    {
        txtidMoney.Text = "";
        txtMoneyNo.Text = "0";
        txtMoneyDetail.Text = "";
        txtMoney.Text = "0";
    }
    protected void btDelMoney_Click(object sender, EventArgs e)
    {
        if (GridViewMoney.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Money"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewMoney.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvMoney = dt1.DefaultView;
        Session["Money"] = dt1;
        GridViewMoney.DataSource = dvMoney;
        GridViewMoney.DataBind();
        if (dvMoney.Count == 0)
        {
            btDelMoney.Visible = false;
        }
    }
    public decimal GetMoney(decimal Money)
    {
        TotalMoney += Money;
        return Money;
    }
    public decimal GetTotalMoney()
    {
        return TotalMoney;
    }
    protected void btaddQuarter_Click(object sender, EventArgs e)
    {
        if (Session["Quarter"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("QuarterNo");
            dt1.Columns.Add("QuarterSort");
            dt1.Columns.Add("QuarterTime");
            dt1.Columns.Add("QuarterPlan");
            dt1.Columns.Add("QuarterTarget");
            dt1.Columns.Add("QuarterBudget");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["QuarterNo"] = ddlQuarter.SelectedValue;
            dr["QuarterSort"] = txtQuarterSort.Text;
            dr["QuarterTime"] = txtQuarterTime.Text;
            dr["QuarterPlan"] = txtQuarterPlan.Text;
            dr["QuarterTarget"] = txtQuarterTarget.Text;
            dr["QuarterBudget"] = txtQuarterBudget.Text;
            dt1.Rows.Add(dr);

            dvQuarterBudget = dt1.DefaultView;
            Session["Quarter"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Quarter"];

            if (txtidQuarter.Text == "")
            {
                DataView ckdv = ((DataTable)Session["Quarter"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int i = 0; i < ckdv.Count; i++)
                    {
                        if ((ckdv[i]["QuarterNo"].ToString() == ddlQuarter.SelectedValue) && (ckdv[i]["QuarterSort"].ToString() == txtQuarterSort.Text))
                        {
                            lblckQuarter.Visible = true;
                            return;
                        }
                    }
                }
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["QuarterNo"] = ddlQuarter.SelectedValue;
                dr["QuarterSort"] = txtQuarterSort.Text;
                dr["QuarterTime"] = txtQuarterTime.Text;
                dr["QuarterPlan"] = txtQuarterPlan.Text;
                dr["QuarterTarget"] = txtQuarterTarget.Text;
                dr["QuarterBudget"] = txtQuarterBudget.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                DataView ckdv = ((DataTable)Session["Quarter"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    //for (int j = 0; j < ckdv.Count; j++)
                    //{
                    //    if (ckdv[j]["id"].ToString() != txtidMoney.Text)
                    //    {
                    //        lblckMoney.Visible = true;
                    //        return;
                    //    }
                    //}
                }
                Int32 i = Convert.ToInt32(txtidQuarter.Text);
                dt1.Rows[i]["QuarterNo"] = ddlQuarter.SelectedValue;
                dt1.Rows[i]["QuarterSort"] = txtQuarterSort.Text;
                dt1.Rows[i]["QuarterTime"] = txtQuarterTime.Text;
                dt1.Rows[i]["QuarterPlan"] = txtQuarterPlan.Text;
                dt1.Rows[i]["QuarterTarget"] = txtQuarterTarget.Text;
                dt1.Rows[i]["QuarterBudget"] = txtQuarterBudget.Text;
            }
            lblckQuarter.Visible = false;

            dvQuarterBudget = dt1.DefaultView;
            Session["Quarter"] = dt1;
        }
        dvQuarterBudget.Sort = "QuarterNo, QuarterSort";
        ClearQuarter();
        GridViewQuarter.DataSource = dvQuarterBudget;
        GridViewQuarter.CheckListDataField = "id";
        GridViewQuarter.DataBind();
        if (dvQuarterBudget.Count > 0)
        {
            btDelQuarter.Visible = true;
        }
    }
    private void ClearQuarter()
    {
        //ddlQuarter.SelectedIndex = 0;
        txtidQuarter.Text = "";
        txtQuarterSort.Text = "0";
        txtQuarterTime.Text = "";
        txtQuarterPlan.Text = "";
        txtQuarterTarget.Text = "";
        txtQuarterBudget.Text = "0";
    }
    protected void btDelQuarter_Click(object sender, EventArgs e)
    {
        if (GridViewQuarter.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Quarter"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewQuarter.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvQuarterBudget = dt1.DefaultView;
        Session["Quarter"] = dt1;
        GridViewQuarter.DataSource = dvQuarterBudget;
        GridViewQuarter.DataBind();
        if (dvQuarterBudget.Count == 0)
        {
            btDelQuarter.Visible = false;
        }
    }
    protected string checkedit(string id, string EmpID, string strName)
    {
        if (EmpID == CurrentUser.ID || btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            if (!string.IsNullOrEmpty(strName))
            {
                return String.Format("<a href=\"javascript:;\" onclick=\"EditItem('{0}');\">{1}</a>", id, strName);
            }
            else
            {
                return String.Format("<a href=\"javascript:;\" onclick=\"EditItem('{0}');\"><img style=\"border: 0; cursor: pointer;\" title=\"á¡éä¢\" src=\"../Image/edit.gif\" /></a>", id);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(strName))
            {
                return strName;
            }
            else
            {
                return "";
            }
        }
    }
    protected string checkdelete(string id, string EmpID)
    {
        if (EmpID == CurrentUser.ID || btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            return String.Format("<a href=\"javascript:deleteItem('{0}');\"><img style=\"border: 0; cursor: pointer;\" title=\"Åº\" src=\"../Image/delete.gif\" /></a>", id);
        }
        else
        {
            return string.Format("");
        }
    }
    public decimal GetQuarterBudget(decimal Budget)
    {
        TotalQuarterBudget += Budget;
        return Budget;
    }
    public decimal GetTotalQuarterBudget()
    {
        return TotalQuarterBudget;
    }
}
