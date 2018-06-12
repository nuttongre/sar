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

public partial class DevelopLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            ClearAll();
            getddlYear(0);
            getddlStandard(0);
            getddlIndicators(0, ddlSearch.SelectedValue);
            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        getddlYear(1);
                        getddlStandard(1);
                        ddlStandard.SelectedIndex = 0;
                        getddlIndicators(1, ddlStandard.SelectedValue);
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.GenSort(txtSort, "DevelopLevel", " And IndicatorsCode = '" + ddlIndicators.SelectedValue + "'");
                        btc.GenSort(txtLevelScore, "DevelopLevel", " And IndicatorsCode = '" + ddlIndicators.SelectedValue + "'");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Ckddl();", true);
                        break;
                    case "2":
                        getddlYear(1);
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        GetData(Request["id"]);
                        break;
                    case "3":
                        btc.txtEnable(table1, TableSearch, GridView1, false);
                        Delete(Request["id"]);
                        break;
                }
            }
            DataBind();
        }
        ddlStandard.Attributes.Add("onchange", "Ckddl();");
        ddlIndicators.Attributes.Add("onchange", "CkIndicators();");
        txtLevelScore.Attributes.Add("onchange", "Cktxt();");
        txtWeight.Attributes.Add("onchange", "CkWeight();");
        txtSort.Attributes.Add("onchange", "CkSort();");
    }
    private void getddlStandard(int mode)
    {
        BTC btc = new BTC();
        if (mode == 0)
        {
            btc.getddlStandard(0, ddlSearch, ddlSearchYear.SelectedValue);
        }

        if (mode == 1)
        {
            btc.getddlStandard(1, ddlStandard, ddlYearB.SelectedValue);
        }
    }
    private void getddlIndicators(int mode, string StandardCode)
    {
        BTC btc = new BTC();
        if (mode == 0)
        {
            btc.getddlIndicators(0, ddlSearch1, StandardCode);
        }

        if (mode == 1)
        {
            btc.getddlIndicators(1, ddlIndicators, StandardCode);
        }
    }
    private void getddlYear(int mode)
    {
        BTC btc = new BTC();
        if (mode == 0)
        {
            btc.getdllStudyYear(ddlSearchYear);
            btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
        }

        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
        }
    }
    public override void DataBind()
    {
        string StrSql = " Select Cast(a.Sort As nVarChar) + ' - ' + a.StandardName FullName, b.IndicatorsCode, b.IndicatorsName, "
                        + " c.DevelopLevelCode, c.LevelScore, c.LevelDetail, c.Sort From Standard a, Indicators b, DevelopLevel c "
                        + " Where c.DelFlag = 0 And a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode And c.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StandardCode = '" + ddlSearch.SelectedValue + "' ";
        }
        if (ddlSearch1.SelectedIndex != 0)
        {
            StrSql = StrSql + " And b.IndicatorsCode = '" + ddlSearch1.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And c.LevelDetail Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format(StrSql + " Order By a.Sort, b.Sort, c.Sort Desc" ));

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format("Select a.StandardCode, b.IndicatorsCode, c.StudyYear, c.LevelScore, c.Weight, c.LevelDetail, c.Sort From Standard a, Indicators b, DevelopLevel c Where a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode And c.DevelopLevelCode = '" + id + "' "));

        if (dv.Count != 0)
        {
            getddlYear(1);
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlStandard(1);
            ddlStandard.SelectedValue = dv[0]["StandardCode"].ToString();
            getddlIndicators(1, ddlStandard.SelectedValue);
            ddlIndicators.SelectedValue = dv[0]["IndicatorsCode"].ToString();
            txtLevelScore.Text = dv[0]["LevelScore"].ToString();
            txtWeight.Text = dv[0]["Weight"].ToString();
            txtLevelDetail.Text = dv[0]["LevelDetail"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
        }
    }
    private void ClearAll()
    {
        BTC btc = new BTC();
        txtLevelScore.Text = "";
        txtWeight.Text = "";
        txtLevelDetail.Text = "";
        txtSearch.Text = "";
        txtSort.Text = "";
        ddlStandard.SelectedIndex = 0;
        ddlIndicators.SelectedIndex = 0;
        ddlSearch.SelectedIndex = 0;
        ddlSearch1.SelectedIndex = 0;
        btc.txtEnable(table1, TableSearch, GridView1, false);
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStandard(0);
        getddlIndicators(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlIndicators(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.txtEnable(table1, TableSearch, GridView1, true);
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        Connection Conn = new Connection();
        BTC btc = new BTC();
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            Int32 i = Conn.AddNew("DevelopLevel", "StudyYear, DevelopLevelCode, IndicatorsCode, LevelScore, Weight, LevelDetail, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", ddlYearB.SelectedValue, NewID, ddlIndicators.SelectedValue, txtLevelScore.Text, txtWeight.Text, txtLevelDetail.Text, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "1", i);
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("DevelopLevel", "Where DevelopLevelCode = '" + Request["id"] + "' ", " StudyYear, IndicatorsCode, LevelScore, Weight, LevelDetail, Sort, UpdateUser, UpdateDate", ddlYearB.SelectedValue, ddlIndicators.SelectedValue, txtLevelScore.Text, txtWeight.Text, txtLevelDetail.Text, txtSort.Text, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "2", i);
        }
        ClearAll();
        DataBind();
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        DataBind();
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        Connection Conn = new Connection();
        BTC btc = new BTC();
        DataView dv = Conn.Select(string.Format("Select DevelopLevelCode From DevelopLevelExpect Where DevelopLevelCode = '" + id + "' "));
        if (dv.Table.Rows.Count > 0)
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            Int32 i = Conn.Update("DevelopLevel", "Where DevelopLevelCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        }  
        ClearAll();
    }
    protected void ddlStandard_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlIndicators(1, ddlStandard.SelectedValue);
    }
    protected void ddlIndicators_OnSelectedChanged(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.GenSort(txtSort, "DevelopLevel", " And IndicatorsCode = '" + ddlIndicators.SelectedValue + "'");
        btc.GenSort(txtLevelScore, "DevelopLevel", " And IndicatorsCode = '" + ddlIndicators.SelectedValue + "'");
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlStandard(1);
        getddlIndicators(1, ddlStandard.SelectedValue);
    }
}
