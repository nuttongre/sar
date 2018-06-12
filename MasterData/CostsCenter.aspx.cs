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

public partial class CostsCenter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            ClearAll();
            btc.txtEnable(table1, TableSearch, GridView1, false);
            getddlYear();
            btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "CostsCenter", ddlSearchYear.SelectedValue);
            btc.CkAdmission(GridView1, btAdd, null);
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.GenSort(txtSort, "CostsCenter", " And StudyYear = '" + ddlYearB.SelectedValue + "' ");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt();", true);
                        break;
                    case "2":
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.btEnable(btSaveAgain, false);
                        GetData(Request["id"]);
                        break;
                    case "3":
                        btc.txtEnable(table1, TableSearch, GridView1, false);
                        Delete(Request["id"]);
                        btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "CostsCenter", ddlSearchYear.SelectedValue);
                        break;
                }
            }
            DataBind();
        }
        txtCostsName.Attributes.Add("onkeyup", "Cktxt();");
        txtSort.Attributes.Add("onkeyup", "CkSort();");
    }
    private void getddlYear()
    {
        BTC btc = new BTC();
        btc.getdllStudyYear(ddlSearchYear);
        btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");

        btc.getdllStudyYear(ddlYearB);
        btc.getDefault(ddlYearB, "StudyYear", "StudyYear");

        btc.getdllStudyYearForCopy(ddlOldYear, ddlSearchYear.SelectedValue);
    }
    public override void DataBind()
    {
        string StrSql = " Select CostsCode, CostsName, Sort "
                    + " From CostsCenter "
                    + " Where DelFlag = 0 And StudyYear = '" + ddlSearchYear.SelectedValue + "' ";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And CostsName Like '%" + txtSearch.Text + "%' Or Sort Like '%" + txtSearch.Text + "%'  ";
        }
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format(StrSql + " Order By Sort Asc "));

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
        dv = Conn.Select(string.Format("Select * From CostsCenter Where CostsCode = '" + id + "'"));

        if (dv.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            txtCostsName.Text = dv[0]["CostsName"].ToString();
            txtCostsDetail.Text = dv[0]["CostsDetail"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
        }
    }
    private void ClearAll()
    {
        BTC btc = new BTC();
        txtCostsName.Text = "";
        txtCostsDetail.Text = "";
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
            Int32 i = Conn.AddNew("CostsCenter", "CostsCode, StudyYear, CostsName, CostsDetail, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlYearB.SelectedValue, txtCostsName.Text, txtCostsDetail.Text, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);           

            if (CkAgain == "N")
            {
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                btc.txtEnable(table1, TableSearch, GridView1, false);
            }
            if (CkAgain == "Y")
            {
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                btc.GenSort(txtSort, "CostsCenter", " And StudyYear = '" + ddlYearB.SelectedValue + "' ");
                GridView2.Visible = true;
            }
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("CostsCenter", "Where CostsCode = '" + Request["id"] + "' ", "StudyYear, CostsName, CostsDetail, Sort, UpdateUser, UpdateDate", ddlYearB.SelectedValue, txtCostsName.Text, txtCostsDetail.Text, txtSort.Text, CurrentUser.ID, DateTime.Now);
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
        DataView dv = Conn.Select(string.Format("Select CostsCode From dtCostsAct Where CostsCode = '" + id + "' "));
        if (dv.Count > 0)
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            Int32 i = Conn.Update("CostsCenter", "Where CostsCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        }
        ClearAll();
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "CostsCenter", ddlSearchYear.SelectedValue);
        btc.getdllStudyYearForCopy(ddlOldYear, ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlYearB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        btc.GenSort(txtSort, "CostsCenter", " And StudyYear = '" + ddlYearB.SelectedValue + "'");
    }
    protected void btCopy_Click(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (btc.CkDataDuplicate(ddlSearchYear.SelectedValue, "CostsCenter"))
        {
            btc.Msg_Head(Img1, MsgHead, true, "7", 0);
            return;
        }
        Connection Conn = new Connection();
        string strSql = " Select CostsCode, StudyYear, CostsName, CostsDetail, Sort From CostsCenter Where DelFlag = 0 And StudyYear = '" + ddlOldYear.SelectedValue + "' Order By Sort ";
        DataView dvCostsCenter = Conn.Select(strSql);

        if (dvCostsCenter.Count != 0)
        {
            for (int i = 0; i < dvCostsCenter.Count; i++)
            {
                string NewID = Guid.NewGuid().ToString();
                Int32 x = Conn.AddNew("CostsCenter", "CostsCode, StudyYear, CostsName, CostsDetail, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlSearchYear.SelectedValue, dvCostsCenter[i]["CostsName"].ToString(), dvCostsCenter[i]["CostsDetail"].ToString(), dvCostsCenter[i]["Sort"].ToString(), 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
            }
            btc.Msg_Head(Img1, MsgHead, true, "1", 1);
            btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "CostsCenter", ddlSearchYear.SelectedValue);
            DataBind();
        }
        else
        {
            btc.Msg_Head(Img1, MsgHead, true, "6", 0);
        }
    }
}
