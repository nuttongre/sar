using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Star.Security.Cryptography;

public partial class EmployeeView : System.Web.UI.Page
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
            getddlUserRole(0);
            getddlDept(0);
            btc.CkAdmission(GridView1,btAdd, null);
            int i = btc.ckGetAdmission(CurrentUser.UserRoleID);
            if(i != 1)
            {
                spnImportExcel.Visible = false;
                spnDownLoadFile.Visible = false;
                ddlSearchDept.Enabled = false;
                ddlSearch.Enabled = false;
                GridView1.Columns[GridView1.Columns.Count - 3].Visible = false;
            }
            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getddlUserRole(1);
                        getddlDept(1);
                        getSchool();
                        tblEditPwd.Visible = true;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "function CkEmp(){" + Page.ClientScript.GetPostBackEventReference(btCkEmp, null, false) + ";}", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getddlUserRole(1);
                        getddlDept(1);
                        getSchool();
                        tblEditPwd.Visible = false;
                        GetData(Request["id"]);
                        break;
                    case "3":
                        MultiView1.ActiveViewIndex = 0;
                        Delete(Request["id"]);
                        break;
                    case "4":
                        MultiView1.ActiveViewIndex = 0;
                        ResetPwd(Request["id"]);
                        break;
                    case "5":
                        MultiView1.ActiveViewIndex = 0;
                        Hide(Request["id"]);
                        break;
                }
            }
            else
            {
                DataBind();
            }
        }
        txtUserName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtUserName.Attributes.Add("onchange", "var fnc=parent.eval('CkEmp'); if(typeof(fnc)=='function' && fnc) fnc();");
        txtPwd.Attributes.Add("onkeyup", "Cktxt(0);");
        txtConfirmPwd.Attributes.Add("onkeyup", "Cktxt(0);");
        txtName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtEmail.Attributes.Add("onkeyup", "Cktxt(0);");
        txtTel.Attributes.Add("onkeyup", "Cktxt(0);");
        ddlSchool.Attributes.Add("onchange", "Cktxt(0);");
        ddlDept.Attributes.Add("onchange", "Cktxt(0);");
        ddlUserRole.Attributes.Add("onchange", "Cktxt(0);");
    }
    private void getddlUserRole(int mode)
    {
        if (mode == 0)
        {
            ddlSearch.DataSource = btc.getddlUserRole();
            ddlSearch.DataTextField = "UserRoleName";
            ddlSearch.DataValueField = "UserRoleID";
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            if (Cookie.GetValue2("Role") == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch.SelectedValue = Cookie.GetValue2("Role");
                }
                catch (Exception ex)
                {
                    ddlSearch.SelectedIndex = 0;
                }
            }
        }
        if (mode == 1)
        {
            ddlUserRole.DataSource = btc.getddlUserRole();
            ddlUserRole.DataTextField = "UserRoleName";
            ddlUserRole.DataValueField = "UserRoleID";
            ddlUserRole.DataBind();
            ddlUserRole.Items.Insert(0, new ListItem("-เลือก-", ""));
        }
    }
    private void getddlDept(int mode)
    {
        if (mode == 0)
        {
            ddlSearchDept.DataSource = btc.getddlDepartment();
            ddlSearchDept.DataTextField = "DeptName";
            ddlSearchDept.DataValueField = "DeptCode";
            ddlSearchDept.DataBind();
            ddlSearchDept.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
            if (Cookie.GetValue2("Dept") == null)
            {
                ddlSearchDept.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearchDept.SelectedValue = Cookie.GetValue2("Dept");
                }
                catch (Exception ex)
                {
                    ddlSearchDept.SelectedIndex = 0;
                }
            }
        }
        if (mode == 1)
        {
            ddlDept.DataSource = btc.getddlDepartment();
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DeptCode";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("-เลือก-", ""));

            cblDept.DataSource = btc.getddlDepartment();
            cblDept.DataTextField = "DeptName";
            cblDept.DataValueField = "DeptCode";
            cblDept.DataBind();
        }
    }
    private void getSchool()
    {
        ddlSchool.DataSource = btc.getddlSchool();
        ddlSchool.DataTextField = "SchoolName";
        ddlSchool.DataValueField = "SchoolID";
        ddlSchool.DataBind();
        ddlSchool.Items.Insert(0, new ListItem("-เลือก-", ""));
        ddlSchool.SelectedIndex = 1;
    }
    public override void DataBind()
    {
        string strSql = " Select b.IsAdmin From EmpLoyee a, UserRole b Where a.UserRoleID = b.UserRoleID And IsAdmin = 1 And a.EmpID = '" + CurrentUser.ID + "' ";
        DataView dv2 = Conn.Select(string.Format(strSql));
        
        if (dv2.Count != 0)
        {
            strSql = " SELECT a.EmpID, a.UserName, a.Pwd, a.EmpName, '' DeptName, c.UserRoleName, HideFlag "
                    + " FROM Employee a "
                    + " Left Join UserRole c On a.UserRoleID = c.UserRoleID "
                    + " Where a.DelFlag = 0 And a.EmpID Not In ('4E975C83-6E7B-4A48-A815-F733094B1234', '4C3E218F-D513-416E-BD10-B4F161717F70') ";      
        }
        else
        {
            strSql = " SELECT a.EmpID, a.UserName, a.Pwd, a.EmpName, '' DeptName, c.UserRoleName, HideFlag "
                    + " FROM Employee a "
                    + " Left Join UserRole c On a.UserRoleID = c.UserRoleID "
                    + " Where a.DelFlag = 0 AND a.EmpID = '" + CurrentUser.ID + "' "
                    + " And a.EmpID Not In ('4E975C83-6E7B-4A48-A815-F733094B1234', '4C3E218F-D513-416E-BD10-B4F161717F70') ";
        }

        if (ddlSearchDept.SelectedIndex != 0)
        {
            if (dv2.Count != 0)
            {
                strSql = " SELECT a.EmpID, a.UserName, a.Pwd, a.EmpName, '' DeptName, c.UserRoleName, HideFlag "
                        + " FROM Employee a Left Join EmpDept b On a.EmpID = b.EmpID "
                        + " Left Join UserRole c On a.UserRoleID = c.UserRoleID "
                        + " Where a.DelFlag = 0 And a.EmpID Not In ('4E975C83-6E7B-4A48-A815-F733094B1234', '4C3E218F-D513-416E-BD10-B4F161717F70') ";
            }
            else
            {
                strSql = " SELECT a.EmpID, a.UserName, a.Pwd, a.EmpName, '' DeptName, c.UserRoleName, HideFlag "
                        + " FROM Employee a Left Join EmpDept b On a.EmpID = b.EmpID "
                        + " Left Join UserRole c On a.UserRoleID = c.UserRoleID "
                        + " Where a.DelFlag = 0 AND a.EmpID = '" + CurrentUser.ID + "' "
                        + " And a.EmpID Not In ('4E975C83-6E7B-4A48-A815-F733094B1234', '4C3E218F-D513-416E-BD10-B4F161717F70') ";
            }
            strSql = strSql + " And b.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
        }

        if (ddlSearch.SelectedIndex != 0)
        {
            strSql = strSql + " And c.UserRoleID = '" + ddlSearch.SelectedValue + "' ";
        }

        if (txtSearch.Text != "")
        {
            strSql = strSql + " And a.EmpName Like '%" + txtSearch.Text + "%' ";
        }

        DataView dv = Conn.Select(string.Format(strSql + " Order By HideFlag, a.UserName "));

        if (dv.Count != 0)
        {
            for (int i = 0; i < dv.Count; i++)
            {
                dv[i]["DeptName"] = btc.getEmpDeptName(dv[i]["EmpID"].ToString());
            }
        }

        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblSearchTotal.InnerText = dv.Count.ToString();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv, dv1;
        string strSql = "Select * From Employee Where EmpID = '" + id + "' ";
        dv = Conn.Select(strSql);

        if (dv.Count != 0)
        {
            txtUserName.Text = dv[0]["UserName"].ToString();
            txtUserName.ReadOnly = true;
            txtName.Text = dv[0]["EmpName"].ToString();
            txtEmail.Text = dv[0]["Email"].ToString();
            txtTel.Text = dv[0]["Tel"].ToString();
            ddlSchool.SelectedValue = dv[0]["SchoolID"].ToString();
            ddlDept.SelectedValue = dv[0]["DeptCode"].ToString();
            ddlUserRole.SelectedValue = dv[0]["UserRoleID"].ToString();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Employee", "EmpID", id);

            strSql = "Select DeptCode From EmpDept Where EmpID = '" + id + "' ";
            dv1 = Conn.Select(strSql);
            if (dv1.Count != 0)
            {
                for (int i = 0; i <= cblDept.Items.Count - 1; i++)
                {
                    for (int j = 0; j <= dv1.Count - 1; j++)
                    {
                        if (cblDept.Items[i].Value == dv1[j]["DeptCode"].ToString())
                        {
                            cblDept.Items[i].Selected = true;

                            if (cblDept.Items[i].Value == ddlDept.SelectedValue)
                            {
                                cblDept.Items[i].Enabled = false;
                            }
                            break;
                        }
                    }
                }
            }

            if (btc.ckGetAdmission(CurrentUser.UserRoleID) != 1)
            {
                ddlSchool.Enabled = false;
                ddlDept.Enabled = false;
                ddlUserRole.Enabled = false;
                cblDept.Enabled = false;
            }
            else
            {
                ddlSchool.Enabled = true;
                ddlDept.Enabled = true;
                ddlUserRole.Enabled = true;
                cblDept.Enabled = true;
            }

            if (ddlUserRole.SelectedIndex == 4)
            {
                cblDept.SelectedItem.Selected = false;
                cblDept.SelectedValue = ddlDept.SelectedValue;
                cblDept.Enabled = false;
            }
        }
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        if (CurrentUser.ID.ToString() == Request["id"].ToString())
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            Int32 i = 0;
            DataView dv = Conn.Select("Select EmpCode From dtAcEmp Where EmpCode = '" + Request["id"] + "'");
            if (dv.Count == 0)
            {
                i = Conn.Update("Employee", "Where EmpID = '" + Request["id"] + "' ", "DelFlag", 1);
                Conn.Delete("EmpDept", "Where EmpID = '" + Request["id"] + "' ");
            }
            Response.Redirect("EmployeeView.aspx?ckmode=3&Cr=" + i); 
        }
    }
    private void ResetPwd(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        Int32 i = Conn.Update("Employee", "Where EmpID = '" + Request["id"] + "' ", "Pwd", Text.Encrypt("password"));
        Response.Redirect("EmployeeView.aspx?ckmode=2&Cr=" + i); 
    }
    private void Hide(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        Int32 sts = 0;
        if(!string.IsNullOrEmpty(Request["sts"]))
        {
            sts = Convert.ToInt32(Request["sts"]);
            if (sts == 0)
            {
                sts = 1;
            }
            else
            {
                sts = 0;
            }
        }

        Int32 i = Conn.Update("Employee", "Where EmpID = '" + Request["id"] + "' ", "HideFlag", sts);
        Response.Redirect("EmployeeView.aspx?ckmode=2&Cr=" + i); 
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("Role", ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("Dept", ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected string linkHide(string id, Boolean HideFlag)
    {
        string ImgName = "off.png";
        string StatusName = "ปิดการใช้งานอยู่";

        if (!HideFlag)
        {
            ImgName = "OnB.png";
            StatusName = "เปิดการใช้งานอยู่";
        }

        DataView dv = Conn.Select("Select EmpID, UserName From Employee Where DelFlag = 0 And EmpID = '" + id + "'");
        if (dv.Count != 0)
        {
            if (dv[0]["UserName"].ToString() != "Admin")
            {
                return string.Format("<a href=\"javascript:HideItem('{0}', {1})\"><img style=\"border: 0; cursor: pointer;\" title=\"" + StatusName + "\" src=\"../Image/" + ImgName + "\" /></a>", id, Convert.ToInt32(HideFlag));
            }
            else
            {
                return String.Format("<img style=\"border: 0; \" title=\"" + StatusName + "\" src=\"../Image/" + ImgName + "\" />");
            }
        }
        else
        {
            return string.Format("<a href=\"javascript:HideItem('{0}', {1})\"><img style=\"border: 0; cursor: pointer;\" title=\"" + StatusName + "\" src=\"../Image/" + ImgName + "\" /></a>", id, Convert.ToInt32(HideFlag));
        }
    }
    protected string linkDel(string id)
    {
        DataView dv = Conn.Select("Select EmpID, UserName From Employee Where DelFlag = 0 And EmpID = '" + id + "'");
        if (dv.Count != 0)
        {
            if (dv[0]["UserName"].ToString() != "Admin")
            {
                return String.Format("<a href=\"javascript:deleteItem('{0}')\"><img style=\"border: 0; cursor: pointer;\" title=\"ลบ\" src=\"../Image/delete.gif\" /></a>", id);
            }
            else
            {
                return string.Format("");
            }
        }
        else
        {
            return string.Format("");
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            if (!Cktxt())
            {
                return;
            }
            string NewID = Guid.NewGuid().ToString();
            Int32 i = Conn.AddNew("Employee", "EmpID, UserName, Pwd, EmpName, Email, Tel, SchoolID, DeptCode, UserRoleID, HideFlag, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, txtUserName.Text, Text.Encrypt(txtPwd.Text), txtName.Text, txtEmail.Text, txtTel.Text, ddlSchool.SelectedValue, ddlDept.SelectedValue, ddlUserRole.SelectedValue, 0, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

            for (int j = 0; j <= cblDept.Items.Count - 1; j++)
            {
                if (cblDept.Items[j].Selected)
                {
                    Conn.AddNew("EmpDept", "EmpID, DeptCode", NewID, cblDept.Items[j].Value);
                }
            }
            Response.Redirect("EmployeeView.aspx?ckmode=1&Cr=" + i);  
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("Employee", "WHERE EmpID = '" + Request["id"] + "' ", "EmpName, Email, Tel, SchoolID, DeptCode, UserRoleID, UpdateUser, UpdateDate", txtName.Text, txtEmail.Text, txtTel.Text, ddlSchool.SelectedValue, ddlDept.SelectedValue, ddlUserRole.SelectedValue, CurrentUser.ID, DateTime.Now);

            Conn.Delete("EmpDept", "Where EmpID = '" + Request["id"] + "' ");
            for (int j = 0; j <= cblDept.Items.Count - 1; j++)
            {
                if (cblDept.Items[j].Selected)
                {
                    Conn.AddNew("EmpDept", "EmpID, DeptCode", Request["id"], cblDept.Items[j].Value);
                }
            }
            Response.Redirect("EmployeeView.aspx?ckmode=2&Cr=" + i);  
        }
    }
    private Boolean Cktxt()
    {
        string sql = string.Format("Select UserName From Employee Where UserName = '{0}' And DelFlag = 0", txtUserName.Text);
        DataView dv = Conn.Select(sql);

        if (dv.Count != 0)
        {
            return false;
        }
        return true;
    }
    protected void btCkEmp_Click(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "ImgWait();", true);
        if (!string.IsNullOrEmpty(txtUserName.Text))
        {
            string sql = string.Format("Select UserName From Employee Where UserName = '{0}' And DelFlag = 0", txtUserName.Text);
            DataView dv = Conn.Select(sql);
            if (dv.Count != 0)
            {
                lblErrorUserName.Visible = true;
                lblErrorUserName.Text = "ชื่อนี้มีผู้ใช้แล้ว";
                lblErrorUserName.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblErrorUserName.Visible = true;
                lblErrorUserName.Text = "ชื่อนี้ใช้ได้";
                lblErrorUserName.ForeColor = System.Drawing.Color.Green;
            }
        }
        else
        {
            lblErrorUserName.Visible = false;
        }
    }
    protected void ddlDept_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDept.SelectedIndex != 0)
        {
            if (Request["mode"] != "1")
            {
                cblDept.SelectedItem.Selected = false;
            }

            for (int i = 0; i < cblDept.Items.Count; i++)
            {
                cblDept.Items[i].Enabled = true;
            }
            cblDept.SelectedValue = ddlDept.SelectedValue;
            cblDept.SelectedItem.Enabled = false;
        }
        else
        {
            for (int i = 0; i < cblDept.Items.Count; i++)
            {
                cblDept.Items[i].Selected = false;
            }
        }
    }
    protected void ddlUserRole_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserRole.SelectedIndex == 4)
        {
            if (ddlDept.SelectedIndex != 0)
            {
                cblDept.SelectedItem.Selected = false;
                cblDept.SelectedValue = ddlDept.SelectedValue;
                cblDept.Enabled = false;
            }
        }
        else
        {
            cblDept.Enabled = true;
        }
    }
}
