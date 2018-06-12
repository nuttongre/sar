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

public partial class CostsSubCenter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            ClearAll();
            btc.txtEnable(table1, TableSearch, GridView1, false);
            getddlYear();
            getddlCostsCenter();
            btc.CkAdmission(GridView1, btAdd, null);
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.GenSort(txtSort, "CostsSubCenter", " And CostsCode = '" + ddlCostsCenter.SelectedValue + "' ");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Ckddl();", true);
                        break;
                    case "2":
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.btEnable(btSaveAgain, false);
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
        ddlCostsCenter.Attributes.Add("onchange", "Ckddl();");
        txtCostsSubName.Attributes.Add("onkeyup", "Cktxt();");
        txtSort.Attributes.Add("onkeyup", "CkSort();");
    }
    private void getddlYear()
    {
        BTC btc = new BTC();
        btc.getdllStudyYear(ddlSearchYear);
        btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");

        btc.getdllStudyYear(ddlYearB);
        btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
    }
    private void getddlCostsCenter()
    {
        BTC btc = new BTC();
        btc.getddlCostsCenter(0, ddlSearchCostsCenter, ddlSearchYear.SelectedValue);

        btc.getddlCostsCenter(1, ddlCostsCenter, ddlYearB.SelectedValue);
    }
    public override void DataBind()
    {
        string StrSql = " Select b.CostsSubCode, Cast(a.Sort As nVarChar) + '.' + Cast(b.Sort As nVarChar) + ' ' + b.CostsSubName As CostsSubName, "
                    + " b.Sort, Cast(a.Sort As nVarChar) + '. ' + a.CostsName As CostsName "
                    + " From CostsCenter a, CostsSubCenter b "
                    + " Where a.CostsCode = b.CostsCode And a.DelFlag = 0 And b.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";

        if (ddlSearchCostsCenter.SelectedIndex != 0)
        { 
            StrSql = StrSql + " And b.CostsCode = '" + ddlSearchCostsCenter.SelectedValue + "'";
        }

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And (b.CostsSubName Like '%" + txtSearch.Text + "%' Or b.Sort Like '%" + txtSearch.Text + "%')  ";
        }
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format(StrSql + " Order By a.Sort Asc, b.Sort Asc "));

        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblSearchTotal.InnerText = dv.Count.ToString();

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format("Select b.*, a.StudyYear From CostsCenter a, CostsSubCenter b Where a.CostsCode = b.CostsCode And b.CostsSubCode = '" + id + "'"));

        if (dv.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            ddlCostsCenter.SelectedValue = dv[0]["CostsCode"].ToString();
            txtCostsSubName.Text = dv[0]["CostsSubName"].ToString();
            txtCostsSubDetail.Text = dv[0]["CostsSubDetail"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
        }
    }
    private void ClearAll()
    {
        BTC btc = new BTC();
        ddlCostsCenter.SelectedIndex = 0;
        txtCostsSubName.Text = "";
        txtCostsSubDetail.Text = "";
        //txtSort.Text = "";
        txtSearch.Text = "";
        //btc.txtEnable(table1, TableSearch, GridView1, false);
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.txtEnable(table1, TableSearch, GridView1, true);
    }
    private void bt_Save(string CkAgain)
    {
        Connection Conn = new Connection();
        BTC btc = new BTC();
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            Int32 i = Conn.AddNew("CostsSubCenter", "CostsSubCode, CostsCode, CostsSubName, CostsSubDetail, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlCostsCenter.SelectedValue, txtCostsSubName.Text, txtCostsSubDetail.Text, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);           

            if (CkAgain == "N")
            {
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                btc.txtEnable(table1, TableSearch, GridView1, false);
            }
            if (CkAgain == "Y")
            {
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                btc.GenSort(txtSort, "CostsSubCenter", " And CostsCode = '" + ddlCostsCenter.SelectedValue + "' ");
                GridView2.Visible = true;
            }
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("CostsSubCenter", "Where CostsSubCode = '" + Request["id"] + "' ", "CostsCode, CostsSubName, CostsSubDetail, Sort, UpdateUser, UpdateDate", ddlCostsCenter.SelectedValue, txtCostsSubName.Text, txtCostsSubDetail.Text, txtSort.Text, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "2", i);
            btc.txtEnable(table1, TableSearch, GridView1, false);
        }
        ClearAll();
        DataBind();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        bt_Save("N");
    }
    protected void btSaveAgain_Click(object sender, EventArgs e)
    {
        bt_Save("Y");
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        ClearAll();
        btc.txtEnable(table1, TableSearch, GridView1, false);
        DataBind();
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        Connection Conn = new Connection();
        BTC btc = new BTC();
        //DataView dv = Conn.Select(string.Format("Select CostsCode From dtCostsAct Where CostsCode = '" + id + "' "));
        //if (dv.Count > 0)
        //{
        //    btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        //}
        //else
        //{
            Int32 i = Conn.Update("CostsSubCenter", "Where CostsSubCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        //}
        ClearAll();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearchCostsCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlYearB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.GenSort(txtSort, "CostsSubCenter", " And CostsCode = '" + ddlCostsCenter.SelectedValue + "' ");
    }
    protected void ddlCostsCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.GenSort(txtSort, "CostsSubCenter", " And CostsCode = '" + ddlCostsCenter.SelectedValue + "' ");

    }
}
