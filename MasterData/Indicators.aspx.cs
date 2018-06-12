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

public partial class Indicators : System.Web.UI.Page
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
            if (btc.ckIdentityName("ckStandardNew"))
            {
                lblTitle1.Text = "ประเด็นการพิจารณาย่อย";
                lblTitle2.Text = "ประเด็นการพิจารณาย่อย";
                lblStandardSearch.Text = "ประเด็นการพิจารณา";
                lblIndicatorNo.Text = "ประเด็นการพิจารณาย่อยที่";
                lblStandard.Text = "ประเด็นการพิจารณา";
                lblIndicator.Text = "ประเด็นการพิจารณาย่อย";
                GridView1.Columns[0].HeaderText = "ประเด็นการพิจารณาย่อย";
                GridView2.Columns[0].HeaderText = "ประเด็นการพิจารณาย่อย";
                btAdd.Text = "       สร้างประเด็นการพิจารณาย่อยใหม่";
                btAdd.ToolTip = "สร้างประเด็นการพิจารณาย่อยใหม่";
                btSave.ToolTip = "บันทึกประเด็นการพิจารณาย่อยนี้";
                btSaveAgain.Text = "       บันทึกและสร้างประเด็นการพิจารณาย่อยใหม่";
                btSaveAgain.ToolTip = "บันทึกประเด็นการพิจารณาย่อยนี้และสร้างประเด็นการพิจารณาย่อยใหม่";
            }
            else
            {
                lblTitle1.Text = "ตัวบ่งชี้";
                lblTitle2.Text = "ตัวบ่งชี้";
                lblStandardSearch.Text = "มาตรฐาน";
                lblIndicatorNo.Text = "ตัวบ่งชี้ที่";
                lblStandard.Text = "มาตรฐาน";
                lblIndicator.Text = "ตัวบ่งชี้";
                GridView1.Columns[0].HeaderText = "ตัวบ่งชี้";
                GridView2.Columns[0].HeaderText = "ตัวบ่งชี้";
                btAdd.Text = "       สร้างตัวบ่งชี้ใหม่";
                btAdd.ToolTip = "สร้างตัวบ่งชี้ใหม่";
                btSave.ToolTip = "บันทึกตัวบ่งชี้นี้";
                btSaveAgain.Text = "       บันทึกและสร้างตัวบ่งชี้ใหม่";
                btSaveAgain.ToolTip = "บันทึกตัวบ่งชี้นี้และสร้างตัวบ่งชี้ใหม่";
            }  
            btc.LinkReport(linkReport);
            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);

            getddlYear(0);
            getddlStandard(0, ddlSearchYear.SelectedValue);
            btc.CkAdmission(GridView1, btAdd, null);
            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        getddlStandard(1, ddlYearB.SelectedValue);
                        getddlScoreGroup();
                        btc.GenSort(txtSort, "Indicators", " And StandardCode = '" + ddlStandard.SelectedValue + "'");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        getddlScoreGroup();
                        btc.btEnable(btSaveAgain, false);
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
        ddlStandard.Attributes.Add("onchange", "Cktxt(0);");
        txtIndicators.Attributes.Add("onkeyup", "Cktxt(0);");
        txtWeightScore.Attributes.Add("onkeyup", "Cktxt(0);");
        ddlScoreGroup.Attributes.Add("onchange", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
    }
    private void getddlYear(int mode)
    {
        if (mode == 0)
        {
            btc.getdllStudyYear(ddlSearchYear);

            if (Session["intYearCode"] == null)
            {
                btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
            }
            else
            {
                try
                {
                    ddlSearchYear.SelectedValue = Session["intYearCode"].ToString();
                }
                catch (Exception ex)
                {
                    btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
                }
            }
        }

        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
        }
    }
    private void getddlStandard(int mode, string sYear)
    {
        if (mode == 0)
        {
            btc.getddlStandard(0, ddlSearch, sYear);
            if (Session["intStdCode"] == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch.SelectedValue = Session["intStdCode"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch.SelectedIndex = 0;
                }
            }
            
        }

        if (mode == 1)
        {
            btc.getddlStandard(1, ddlStandard, sYear);
        }
    }
    private void getddlScoreGroup()
    {
        DataView dv = btc.getScoreGroup();
        ddlScoreGroup.DataSource = dv;
        ddlScoreGroup.DataTextField = "ScoreGroupName";
        ddlScoreGroup.DataValueField = "ScoreGroupID";
        ddlScoreGroup.DataBind();
        ddlScoreGroup.Items.Insert(0, new ListItem("-เลือก-", ""));
        ddlScoreGroup.SelectedIndex = 0;
    }
    public override void DataBind()
    {
        string StrSql = "Select Cast(a.Sort As nVarChar) + ' - ' + a.StandardName FullName, b.IndicatorsCode, "
            + " Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + b.IndicatorsName IndicatorsName ,"
            + " b.WeightScore, IsNull(b.QualityFlag, 0) QualityFlag From Standard a, Indicators b "
            + " Where a.DelFlag = 0 And b.DelFlag = 0 And a.StandardCode = b.StandardCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";

        if (btc.ckIdentityName("ckStandardNew"))
        {
            StrSql = @" Select Cast(Sd.Sort As nvarchar) + '.' + Cast(a.Sort As nVarChar) + ' - ' + a.StandardName FullName, b.IndicatorsCode, 
            Cast(Sd.Sort As nvarchar) + '.' + Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + b.IndicatorsName IndicatorsName ,
            b.WeightScore, IsNull(b.QualityFlag, 0) QualityFlag From Side Sd, Standard a, Indicators b 
            Where Sd.DelFlag = 0 And a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And a.StandardCode = b.StandardCode And Sd.SideCode = a.SideCode ";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StandardCode = '" + ddlSearch.SelectedValue + "'";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And b.IndicatorsName Like '%" + txtSearch.Text + "%' Or b.Sort Like '%" + txtSearch.Text + "%' ";
        }
        if (btc.ckIdentityName("ckStandardNew"))
        {
            StrSql += " Order By Sd.Sort, a.Sort, b.Sort ";
        }
        else
        {
            StrSql += " Order By a.Sort, b.Sort ";
        }
        DataView dv = Conn.Select(string.Format(StrSql));

        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblSearchTotal.InnerText = dv.Count.ToString();

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select a.StandardCode, a.StudyYear, b.IndicatorsCode, b.ScoreGroupID, b.IndicatorsName, b.Detail, b.DevelopIssues, IsNull(b.QualityFlag, 0) QualityFlag, b.WeightScore, b.DataSource1, b.DataSource2, b.Sort From Standard a, Indicators b Where a.StandardCode = b.StandardCode And b.IndicatorsCode = '" + id + "' "));

        if (dv.Table.Rows.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlStandard(1, ddlYearB.SelectedValue);
            ddlStandard.SelectedValue = dv[0]["StandardCode"].ToString();
            txtIndicators.Text = dv[0]["IndicatorsName"].ToString();
            txtDetail.Text = dv[0]["Detail"].ToString();
            txtDevelopIssues.Text = dv[0]["DevelopIssues"].ToString();
            cbQualityFlag.Checked = Convert.ToBoolean(dv[0]["QualityFlag"]);
            txtDataSource1.Text = dv[0]["DataSource1"].ToString();
            txtDataSource2.Text = dv[0]["DataSource2"].ToString(); ;
            txtWeightScore.Text = dv[0]["WeightScore"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
            ddlScoreGroup.SelectedValue = dv[0]["ScoreGroupID"].ToString();
            getCriteria();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Indicators", "IndicatorsCode", id);
        }
    }
    private void getCriteria()
    {
        string StrSql = "Select TcriteriaID, ScoreGroupID, Tcriteria, Detail, Criterion, Translation, TMin, TMax From TCriteria Where DelFlag = 0 ";
        StrSql = StrSql + " And ScoreGroupID = '" + ddlScoreGroup.SelectedValue + "' ";

        DataView dv = Conn.Select(string.Format(StrSql + " Order By Tcriteria, Criterion"));
        dgCriteria.DataSource = dv;
        dgCriteria.DataBind();
    }
    private void ClearAll()
    {
        txtIndicators.Text = "";
        txtDetail.Text = "";
        txtDevelopIssues.Text = "";
        txtDataSource1.Text = "";
        txtDataSource2.Text = "";
        txtWeightScore.Text = "";
        cbQualityFlag.Checked = false;
        txtSearch.Text = "";
        ddlScoreGroup.SelectedIndex = 0;
        dgCriteria.DataSource = null;
        dgCriteria.DataBind();
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["intStdCode"] = ddlSearch.SelectedValue;
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        Int32 i = 0;
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            i = Conn.AddNew("Indicators", "IndicatorsCode, IndicatorsName, StandardCode, Detail, DevelopIssues, WeightScore, QualityFlag, DataSource1, DataSource2, ScoreGroupID, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate",
                NewID, txtIndicators.Text, ddlStandard.SelectedValue, txtDetail.Text, txtDevelopIssues.Text, txtWeightScore.Text, cbQualityFlag.Checked, txtDataSource1.Text, txtDataSource2.Text, ddlScoreGroup.SelectedValue, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

            if (CkAgain == "N")
            {
                Response.Redirect("Indicators.aspx?ckmode=1&Cr=" + i);  
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                btc.GenSort(txtSort, "Indicators", " And StandardCode = '" + ddlStandard.SelectedValue + "'");
                GridView2.Visible = true;
                DataBind();
            }
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Indicators", "Where IndicatorsCode = '" + Request["id"] + "' ", "IndicatorsName, StandardCode, Detail, DevelopIssues, WeightScore, QualityFlag, DataSource1, DataSource2, ScoreGroupID, Sort, UpdateUser, UpdateDate", 
                txtIndicators.Text, ddlStandard.SelectedValue, txtDetail.Text, txtDevelopIssues.Text, txtWeightScore.Text, cbQualityFlag.Checked, txtDataSource1.Text, txtDataSource2.Text, ddlScoreGroup.SelectedValue, txtSort.Text, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Indicators.aspx?ckmode=2&Cr=" + i);  
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
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        if (btc.CkUseData(id, "IndicatorsCode", "Evaluation", "And DelFlag = 0"))
        {
            Response.Redirect("Indicators.aspx?ckmode=3&Cr=0"); 
        }
        else
        {
            Int32 i = Conn.Update("Indicators", "Where IndicatorsCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Indicators.aspx?ckmode=3&Cr=" + i);    
        }  
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["intYearCode"] = ddlSearchYear.SelectedValue;
        getddlStandard(0, ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlYearB_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStandard(1, ddlYearB.SelectedValue);
        btc.GenSort(txtSort, "Indicators", " And StandardCode = '" + ddlStandard.SelectedValue + "'");
    }
    protected void ddlStandard_OnSelectedChanged(object sender, EventArgs e)
    {
        btc.GenSort(txtSort, "Indicators", " And StandardCode = '" + ddlStandard.SelectedValue + "'");
    }
    protected void EnableMode(string id, bool status)
    {
        int result = Conn.Update("Indicators", "WHERE IndicatorsCode='" + id + "'", "QualityFlag", (status ? 0 : 1));
    }
    protected void ddlScoreGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        getCriteria();
        txtWeightScore.Text = btc.getWeightScoreGroup(ddlScoreGroup.SelectedValue).ToString();
    }
    protected string checkedit(string id, string strName)
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            return String.Format("<a href=\"javascript:;\" title=\"{1}\" onclick=\"EditItem('{0}');\">{1}</a>", id, strName);
        }
        else
        {
            return strName;
        }
    }
}
