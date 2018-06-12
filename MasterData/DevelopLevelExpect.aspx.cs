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

public partial class DevelopLevelExpect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            ClearAll();
            getddlStandard();
            getddlIndicators(ddlSearch.SelectedValue, 0, "0");
            getddlYear();
            getddlDevelopLevel(ddlIndicators.SelectedValue);
            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        getddlIndicators(ddlIndicators.SelectedValue, 1, ddlYearS.SelectedValue);
                        btc.txtEnable(table1, TableSearch, GridView1, true);
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
    }
    private void getddlYear()
    {
        for (int i = 0; i < 5; i++)
        {
            ddlYearS.Items.Insert(i, new ListItem(((DateTime.Now.Year + 543) - 2 + i).ToString(), ((DateTime.Now.Year + 543) - 2 + i).ToString()));
            ddlSearchYear.Items.Insert(i, new ListItem(((DateTime.Now.Year + 543) - 2 + i).ToString(), ((DateTime.Now.Year + 543) - 2 + i).ToString()));
        }
        ddlYearS.SelectedValue = (DateTime.Now.Year + 543).ToString();
        ddlSearchYear.SelectedValue = (DateTime.Now.Year + 543).ToString();
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
            ddlIndicators.SelectedIndex = 0;

            ddlSearch.DataSource = dv;
            ddlSearch.DataTextField = "FullName";
            ddlSearch.DataValueField = "StandardCode";
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch.SelectedIndex = 1;

            rbtlDevelopLevel.Items.Clear();
            rbtlDevelopLevel.Items.Insert(0, new ListItem("ยังไม่มีรายการ", "9"));
            rbtlDevelopLevel.SelectedIndex = 0;
        }
    }
    private void getddlIndicators(string id ,Int32 ck, string YearB)
    {
        DataView dv;
        dv = new BTC().getddlIndicatorsNotIn(id, ck, YearB);
        if (dv.Count != 0)
        {
            ddlIndicators.DataSource = dv;
            ddlIndicators.DataTextField = "FullName";
            ddlIndicators.DataValueField = "IndicatorsCode";
            ddlIndicators.DataBind();
            ddlIndicators.Items.Insert(0, new ListItem("-เลือก-", ""));
            ddlIndicators.SelectedIndex = 0;
            ddlIndicators.Enabled = true;
        }
        else
        {
            ddlIndicators.Items.Insert(0, new ListItem("-เลือก-", ""));
            ddlIndicators.SelectedIndex = 0;
            ddlIndicators.Enabled = false;

            
        }
        rbtlDevelopLevel.Items.Clear();
        rbtlDevelopLevel.Items.Insert(0, new ListItem("ยังไม่มีรายการ", "9"));
        rbtlDevelopLevel.SelectedIndex = 0;
    }
    private void getddlDevelopLevel(string id)
    {
        DataView dv;
        dv = new BTC().getddlDevelopLevel(id);
        if (dv.Count != 0)
        {
            rbtlDevelopLevel.DataSource = dv;
            rbtlDevelopLevel.DataTextField = "FullName";
            rbtlDevelopLevel.DataValueField = "DevelopLevelCode";
            rbtlDevelopLevel.DataBind();
            rbtlDevelopLevel.SelectedIndex = 0;
        }
        else
        {
            rbtlDevelopLevel.Items.Clear();
            rbtlDevelopLevel.Items.Insert(0, new ListItem("ยังไม่มีรายการ", "9"));
            rbtlDevelopLevel.SelectedIndex = 0;
        }
    }
    public override void DataBind()
    {
        string StrSql = "Select Cast(a.Sort As nVarChar) + ' - ' + a.StandardName FullName, b.IndicatorsCode, 0 As LevelScore2, "
                    + " Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + b.IndicatorsName IndicatorsName, d.DevelopLevelCode, c.LevelScore, c.LevelDetail, c.Sort "
                    + " From Standard a, Indicators b, DevelopLevel c, DevelopLevelExpect d "
                    + " Where b.DelFlag = '0' "
                    + " And a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode "
                    + " And c.IndicatorsCode = d.IndicatorsCode And c.DevelopLevelCode = d.DevelopLevelCode And d.StudyYear = '" + ddlSearchYear.SelectedValue + "' And d.SchoolID = '" + CurrentUser.SchoolID + "' ";

        string StrSql2 = "Select Cast(a.Sort As nVarChar) + ' - ' + a.StandardName FullName, b.IndicatorsCode, "
                    + " Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' - ' + b.IndicatorsName IndicatorsName, d.DevelopLevelCode, c.LevelScore, c.LevelDetail, c.Sort "
                    + " From Standard a, Indicators b, DevelopLevel c, DevelopLevelExpect d "
                    + " Where b.DelFlag = '0' "
                    + " And a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode "
                    + " And c.IndicatorsCode = d.IndicatorsCode And c.DevelopLevelCode = d.DevelopLevelCode And d.StudyYear = '" + (Convert.ToInt32(ddlSearchYear.SelectedValue) - 1) + "' And d.SchoolID = '" + CurrentUser.SchoolID + "' ";

        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StandardCode = '" + ddlSearch.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And c.LevelDetail Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv, dv2;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format(StrSql + " Order By a.Sort, b.Sort" ));
        dv2 = Conn.Select(string.Format(StrSql2 + " Order By a.Sort, b.Sort" ));

        for (int i=0; i < dv.Count; i++)
        {
            for (int j = 0; j < dv2.Count; j++)
            {
                if (dv[i]["IndicatorsCode"].ToString() == dv2[j]["indicatorsCode"].ToString())
                {
                    dv[i]["LevelScore2"] = dv2[j]["LevelScore"].ToString();
                }
            }
        }

        GridView1.DataSource = dv;
        GridView1.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format("Select a.StandardCode, b.IndicatorsCode, c.DevelopLevelCode, c.StudyYear From Standard a, Indicators b, DevelopLevelExpect c Where a.StandardCode = b.StandardCode And b.IndicatorsCode = c.IndicatorsCode And c.IndicatorsCode = '" + id + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "));

        if (dv.Count != 0)
        {
            ddlYearS.SelectedValue = dv.Table.Rows[0]["StudyYear"].ToString();
            ddlStandard.SelectedValue = dv.Table.Rows[0]["StandardCode"].ToString();
            getddlIndicators(ddlStandard.SelectedValue, 0, "0");
            ddlIndicators.SelectedValue = dv.Table.Rows[0]["IndicatorsCode"].ToString();
            getddlDevelopLevel(ddlIndicators.SelectedValue);
            rbtlDevelopLevel.SelectedValue = dv.Table.Rows[0]["DevelopLevelCode"].ToString();
        }
    }
    private void ClearAll()
    {
        BTC btc = new BTC();      
        txtSearch.Text = "";
        ddlStandard.SelectedIndex = 0;
        ddlIndicators.SelectedIndex = 0;
        ddlSearch.SelectedIndex = 1;
        getddlIndicators(ddlSearch.SelectedValue, 0, "0");
        btc.txtEnable(table1, TableSearch, GridView1, false);
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlIndicators(ddlSearch.SelectedValue, 0, "0");
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
        if (rbtlDevelopLevel.SelectedValue == "9")
        {
            btc.Msg_Head(Img1, MsgHead, true, "1", 0);
            return;
        }
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            Int32 i = Conn.AddNew("DevelopLevelExpect", "IndicatorsCode, DevelopLevelCode, StudyYear, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", ddlIndicators.SelectedValue, rbtlDevelopLevel.SelectedValue, ddlYearS.SelectedValue, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "1", i);
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("DevelopLevelExpect", "Where IndicatorsCode = '" + Request["id"] + "' And SchoolID = '" + CurrentUser.SchoolID + "' ", "DevelopLevelCode, StudyYear, UpdateUser, UpdateDate", rbtlDevelopLevel.SelectedValue, ddlYearS.SelectedValue, CurrentUser.ID, DateTime.Now);
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
        //DataView dv = Conn.Select(string.Format("Select DevelopLevelCode From Develop Where DevelopLevelCode = '" + id + "' And DelFlag = '0' "));
        //if (dv.Table.Rows.Count > 0)
        //{
        //    btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        //}
        //else
        //{
        Int32 i = Conn.Delete("DevelopLevelExpect", "Where IndicatorsCode = '" + id + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        //}  
        ClearAll();
    }
    protected void ddlStandard_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlIndicators(ddlStandard.SelectedValue, 1, ddlYearS.SelectedValue);
    }
    protected void ddlIndicators_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlDevelopLevel(ddlIndicators.SelectedValue);
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlYearS_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlStandard();
    }
}
