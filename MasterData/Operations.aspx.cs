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

public partial class Operations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BTC btc = new BTC();
        if (!IsPostBack)
        {
            ClearAll();
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        btc.txtEnable(table1, TableSearch, GridView1, true);
                        btc.GenSort(txtSort, "Operations", "");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt();", true);
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
        txtOperations.Attributes.Add("onchange", "Cktxt();");
        txtSort.Attributes.Add("onchange", "CkSort();");
    }
    public override void DataBind()
    {
        string StrSql = "Select * From Operations Where DelFlag = 0 ";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And OperationsName Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format(StrSql + " Order By Sort "));

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv;
        Connection Conn = new Connection();
        dv = Conn.Select(string.Format("Select * From Operations Where OperationsCode = '" + id + "'"));

        if (dv.Count != 0)
        {
            txtOperations.Text = dv.Table.Rows[0]["OperationsName"].ToString();
            txtSort.Text = dv.Table.Rows[0]["Sort"].ToString();
        }
    }
    private void ClearAll()
    {
        BTC btc = new BTC();
        txtOperations.Text = "";
        txtSort.Text = "";
        txtSearch.Text = "";
        btc.txtEnable(table1, TableSearch, GridView1, false);
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
    protected void btSave_Click(object sender, EventArgs e)
    {
        Connection Conn = new Connection();
        BTC btc = new BTC();
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {           
            string NewID = Guid.NewGuid().ToString();
            Int32 i = Conn.AddNew("Operations", "OperationsCode, OperationsName, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, txtOperations.Text, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);           
            btc.Msg_Head(Img1, MsgHead, true, "1", i);
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("Operations", "Where OperationsCode = '" + Request["id"] + "' ", "OperationsName, Sort, UpdateUser, UpdateDate", txtOperations.Text, txtSort.Text, CurrentUser.ID, DateTime.Now);
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
        DataView dv = Conn.Select(string.Format("Select OperationsCode From Indicators Where OperationsCode = '" + id + "' And DelFlag = 0 "));
        if (dv.Count > 0)
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            Int32 i = Conn.Update("Operations", "Where OperationsCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            btc.Msg_Head(Img1, MsgHead, true, "3", i);
        }
        ClearAll();
    }
}