using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Star.Security.Cryptography;

public partial class SPWD : System.Web.UI.Page
{
    Connection Conn = new Connection();
    BTC btc = new BTC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

        txtUserName.Attributes.Add("onkeyup", "Cktxt(0);");
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        string strSql = @" Select Pwd From Employee Where UserName = '{0}' ";
        DataView dv = Conn.Select(string.Format(strSql, txtUserName.Text));
        if (dv.Count > 0)
        {
            DivPwd.Visible = true;
            txtPassword.Text = Text.Decrypt(dv[0]["Pwd"].ToString());
            lblNotUser.Visible = false;
        }
        else
        {
            DivPwd.Visible = false;
            lblNotUser.Visible = true;
        }
    }
    private void ClearAll()
    {
        DivPwd.Visible = false;
        txtUserName.Text = "";
        txtPassword.Text = "";
        lblNotUser.Visible = false;
    }
}
