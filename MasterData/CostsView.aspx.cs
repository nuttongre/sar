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

public partial class CostsView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            getddlYear();
            getddlStrategies(ddlSearchYear.SelectedValue);
            getddlProjects(ddlSearchYear.SelectedValue,ddlSearch2.SelectedValue);
            ClearAll();
            string mode = Request.QueryString["mode"];
            Int32 ckrec = Convert.ToInt32(Request.QueryString["ckrec"]);
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        btc.Msg_Head(Img1, MsgHead, true, mode, ckrec);
                        break;
                    case "2":
                        btc.Msg_Head(Img1, MsgHead, true, mode, ckrec);
                        break;
                    case "3":
                        //btc.txtEnable(table1, TableSearch, GridView1, false);
                        Delete(Request["id"]);
                        break;
                }
            }
            DataBind();
        }
    }
    private void getddlYear()
    {
        for (int i = 0; i < 5; i++)
        {
            ddlSearchYear.Items.Insert(i, new ListItem(((DateTime.Now.Year + 543) - 2 + i).ToString(), ((DateTime.Now.Year + 543) - 2 + i).ToString()));
        }
        ddlSearchYear.SelectedValue = (DateTime.Now.Year + 543).ToString();
    }
    private void getddlStrategies(string id)
    {
        DataView dv = new BTC().getddlStrategies(id);
        if (dv.Count != 0)
        {
            ddlSearch2.DataSource = dv;
            ddlSearch2.DataTextField = "FullName";
            ddlSearch2.DataValueField = "StrategiesCode";
            ddlSearch2.DataBind();
            ddlSearch2.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch2.SelectedIndex = 0;
            ddlSearch2.Enabled = true;
        }
        else
        {
            ddlSearch2.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch2.SelectedIndex = 0;
            ddlSearch2.Enabled = false;

            ddlSearch.Enabled = false;
            ddlSearch.SelectedIndex = 0;
        }
    }
    private void getddlProjects(string id, string stid)
    {
        DataView dv;
        dv = new BTC().getddlProjects(id, stid);

        if (dv.Count != 0)
        {
            ddlSearch.DataSource = dv;
            ddlSearch.DataTextField = "FullName";
            ddlSearch.DataValueField = "ProjectsCode";
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch.SelectedIndex = 0;
            ddlSearch.Enabled = true;
        }
        else
        {
            ddlSearch.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            ddlSearch.SelectedIndex = 0;
            ddlSearch.Enabled = false;
        }
    }
    public override void DataBind()
    {
        string StrSql = "Select a.CostsCode, a.ProjectsCode, b.ProjectsName, a.ActivityCode, c.ActivityName, "
                    + " CostsType = Case a.CostsType When 0 Then 'เงินงบประมาณ' When 1 Then 'เงินระดม' End, "
                    + " a.TotalAmount, Cast(Term As nVarChar) + '/' + Cast(YearB As nVarChar) Term "
                    + " From Costs a, Projects b, Activity c "
                    + " Where a.DelFlag = '0' And a.ProjectsCode = b.ProjectsCode And a.ActivityCode = c.ActivityCode "
                    + " And a.YearB = '" + ddlSearchYear.SelectedValue + "' And a.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.ProjectsCode = '" + ddlSearch.SelectedValue + "'";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And c.ActivityName Like '%" + txtSearch.Text + "%'";
        }
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format(StrSql + " Order By b.Sort, c.Sort " ));

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    private void ClearAll()
    {
        BTC btc = new BTC();
        ddlSearch.SelectedIndex = 0;
        //btc.txtEnable(table1, TableSearch, GridView1, false);
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        //BTC btc = new BTC();
        //btc.txtEnable(table1, TableSearch, GridView1, true);
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        Connection Conn = new Connection();
        BTC btc = new BTC();
        //DataView dv = Conn.Select(string.Format("Select EntryCostsCode From DevelopLevel Where EntryCostsCode = '" + id + "' And DelFlag = '0' "));
        //if (dv.Table.Rows.Count > 0)
        //{
        //    btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        //}
        //else
        //{
            Int32 i = Conn.Update("Costs", "Where CostsCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        //}  
        ClearAll();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlProjects(ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        DataBind();
    }
}
