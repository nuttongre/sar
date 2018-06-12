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

public partial class Department : System.Web.UI.Page
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
            btc.CkAdmission(GridView1, btAdd, null);
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        btc.GenSort(txtSort, "Department", " ");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
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
        txtDepartment.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
    }
    public override void DataBind()
    {
        string StrSql = " Select DeptCode, DeptName, Sort "
                    + " From Department "
                    + " Where DelFlag = 0 ";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And DeptName Like '%" + txtSearch.Text + "%' Or Sort Like '%" + txtSearch.Text + "%'  ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Order By Sort Asc "));

        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblSearchTotal.InnerText = dv.Count.ToString();

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select * From Department Where DeptCode = '" + id + "'"));

        if (dv.Count != 0)
        {
            txtDepartment.Text = dv[0]["DeptName"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Department", "DeptCode", id);
        }
    }
    private void ClearAll()
    {
        txtDepartment.Text = "";
        txtSearch.Text = "";
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        Int32 i = 0;
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            i = Conn.AddNew("Department", "DeptCode, DeptName, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, txtDepartment.Text, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

            if (CkAgain == "N")
            {
                Response.Redirect("Department.aspx?ckmode=1&Cr=" + i);
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                btc.GenSort(txtSort, "Department", " ");
                GridView2.Visible = true;
                DataBind();
            }
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Department", "Where DeptCode = '" + Request["id"] + "' ", "DeptName, Sort, UpdateUser, UpdateDate", txtDepartment.Text, txtSort.Text, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Department.aspx?ckmode=2&Cr=" + i);
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
        if (btc.CkUseData(id, "DeptCode", "EmpDept", ""))
        {
            Response.Redirect("Department.aspx?ckmode=3&Cr=0");
        }
        else
        {
            Int32 i = Conn.Update("Department", "Where DeptCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Department.aspx?ckmode=3&Cr=" + i);
        }
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlYearB_SelectedIndexChanged(object sender, EventArgs e)
    {
        btc.GenSort(txtSort, "Department", " ");
    }
}
