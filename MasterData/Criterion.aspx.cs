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

public partial class Criterion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            ClearAll();
            getddlStandard();
            getddlIndicators(ddlSearch.SelectedValue);
            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        getddlIndicators(ddlIndicators.SelectedValue);
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.GenSort(txtSort, "Criterion", " And IndicatorsCode = '" + ddlIndicators.SelectedValue + "'");
                        break;
                    case "2":
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
        txtCriterion.Attributes.Add("onchange", "CkCriterion();");
        txtSort.Attributes.Add("onchange", "CkSort();");
    }
    private void getddlStandard()
    {
        DataView dv;
        dv = new BTC().getddlStandard();
        if (dv.Count != 0)
        {
            ddlStandard.DataSource = dv;
            ddlStandard.DataTextField = "FullName";
            ddlStandard.DataValueField = "StandardCode";
            ddlStandard.DataBind();
            ddlStandard.Items.Insert(0, new ListItem("-เลือก-", ""));
            ddlStandard.SelectedIndex = 0;

            ddlSearch.DataSource = dv;
            ddlSearch.DataTextField = "FullName";
            ddlSearch.DataValueField = "StandardCode";
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch.SelectedIndex = 1;
        }
    }
    private void getddlIndicators(string id)
    {
        DataView dv;
        dv = new BTC().getddlIndicators(id);
        if (dv.Count != 0)
        {
            ddlIndicators.DataSource = dv;
            ddlIndicators.DataTextField = "FullName";
            ddlIndicators.DataValueField = "IndicatorsCode";
            ddlIndicators.DataBind();
            ddlIndicators.Items.Insert(0, new ListItem("-เลือก-", ""));
            ddlIndicators.SelectedIndex = 0;
            ddlIndicators.Enabled = true;

            ddlSearch1.DataSource = dv;
            ddlSearch1.DataTextField = "FullName";
            ddlSearch1.DataValueField = "IndicatorsCode";
            ddlSearch1.DataBind();
            ddlSearch1.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch1.SelectedIndex = 1;
            ddlSearch1.Enabled = true;
        }
        else
        {
            ddlIndicators.Items.Insert(0, new ListItem("-เลือก-", ""));
            ddlIndicators.SelectedIndex = 0;
            ddlIndicators.Enabled = false;

            ddlSearch1.Items.Insert(0, new ListItem("-เลือก-", ""));
            ddlSearch1.SelectedIndex = 0;
            ddlSearch1.Enabled = false;
        }
    }
    public override void DataBind()
    {
        string StrSql = "Select Cast(a.Sort As nVarChar) + ' - ' + a.StandardName FullName, b.IndicatorsCode, b.IndicatorsName, c.CriterionCode, c.LevelScore, c.LevelDetail, c.CriterionName, c.Sort From Standard a, Indicators b, Criterion c Where b.DelFlag = '0' And a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode ";
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
        GridView1.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format("Select a.StandardCode, b.IndicatorsCode, c.LevelScore, c.LevelDetail, c.CriterionName, c.Sort From Standard a, Indicators b, Criterion c Where a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode And c.CriterionCode = '" + id + "' "));

        if (dv.Table.Rows.Count != 0)
        {
            ddlStandard.SelectedValue = dv.Table.Rows[0]["StandardCode"].ToString();
            getddlIndicators(ddlStandard.SelectedValue);
            ddlIndicators.SelectedValue = dv.Table.Rows[0]["IndicatorsCode"].ToString();
            txtLevelScore.Text = dv.Table.Rows[0]["LevelScore"].ToString();
            txtLevelDetail.Text = dv.Table.Rows[0]["LevelDetail"].ToString();
            txtCriterion.Text = dv.Table.Rows[0]["CriterionName"].ToString();
            txtSort.Text = dv.Table.Rows[0]["Sort"].ToString();
        }
    }
    private void ClearAll()
    {
        BTC btc = new BTC();
        txtLevelScore.Text = "";
        txtLevelDetail.Text = "";
        txtCriterion.Text = "";
        txtSearch.Text = "";
        txtSort.Text = "";
        ddlStandard.SelectedIndex = 0;
        ddlIndicators.SelectedIndex = 0;
        ddlSearch.SelectedIndex = 1;
        getddlIndicators(ddlSearch.SelectedValue);
        ddlSearch1.SelectedIndex = 1;
        btc.txtEnable(table1, TableSearch, GridView1, false);
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlIndicators(ddlSearch.SelectedValue);
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
            Int32 i = Conn.AddNew("Criterion", "CriterionCode, IndicatorsCode, LevelScore, LevelDetail, CriterionName, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlIndicators.SelectedValue, txtLevelScore.Text, txtLevelDetail.Text, txtCriterion.Text, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "1", i);
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("Criterion", "Where CriterionCode = '" + Request["id"] + "' ", "IndicatorsCode, LevelScore, LevelDetail, CriterionName, Sort, UpdateUser, UpdateDate", ddlIndicators.SelectedValue, txtLevelScore.Text, txtLevelDetail.Text, txtCriterion.Text, txtSort.Text, CurrentUser.ID, DateTime.Now);
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
        DataView dv = Conn.Select(string.Format("Select CriterionCode From Develop Where CriterionCode = '" + id + "' And DelFlag = '0' "));
        if (dv.Table.Rows.Count > 0)
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            Int32 i = Conn.Update("Criterion", "Where CriterionCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        }  
        ClearAll();
    }
    protected void ddlStandard_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlIndicators(ddlStandard.SelectedValue);
    }
    protected void ddlIndicators_OnSelectedChanged(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.GenSort(txtSort, "Criterion", " And IndicatorsCode = '" + ddlIndicators.SelectedValue + "'");
    }
}
