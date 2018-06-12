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
using System.Text;

public partial class Strategies : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    DataView dvIndicators;
    DataView dvGoals;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            if (!string.IsNullOrEmpty(Request["Cr"]))
            {
                btc.Msg_Head(Img1, MsgHead, true, Request["ckmode"], Convert.ToInt32(Request["Cr"]));
            }

            //àªç¤»Õ§º»ÃÐÁÒ³
            btc.ckBudgetYear(lblSearchYear, lblYear);

            getddlYear(0);
            btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "Strategies", ddlSearchYear.SelectedValue);
            ckAdmission(GridView1, btAdd);
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        ClearAll();
                        getddlYear(1);
                        btc.GenSort(txtSort, "Strategies", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        ClearAll();
                        getddlYear(1);
                        btc.btEnable(btSaveAgain, false);
                        btc.btEnable(btSaveAndGo, false);
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
        txtStrategies.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
        //txtIndicators2.Attributes.Add("onkeyup", "ckAddIndicators();");
    }
    private void getddlYear(int mode)
    {
        if (mode == 0)
        {
            btc.getdllStudyYear(ddlSearchYear);
            btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
            btc.getdllStudyYearForCopy(ddlOldYear, ddlSearchYear.SelectedValue);
        }

        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearS);
            btc.getDefault(ddlYearS, "StudyYear", "StudyYear");
        }
    } 
    private void ckAdmission(Star.Web.UI.Controls.DataGridView GridView1, Button btAdd)
    {
        DataView dv = btc.getAdmission(CurrentUser.UserRoleID);
        if (dv.Count != 0)
        {
            if (Convert.ToInt32(dv[0]["IsAdmin"]) == 1)
            {
            }
            else
            {
                if (btAdd != null)
                {
                    btAdd.Visible = false;
                }
                if (GridView1 != null)
                {
                    GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                    GridView1.Columns[GridView1.Columns.Count - 3].Visible = false;
                }
            }

        }
    }
    public override void DataBind()
    {
        string StrSql = " Select a.StrategiesCode, a.StudyYear, a.StrategiesName, a.Df, "
                + " a.Sort, IsNull(Sum(c.TotalAmount), 0) TotalAmount "
                + " From Strategies a Left Join Projects b On a.StrategiesCode = b.StrategiesCode And b.DelFlag = 0 "
                + " Left Join Activity c On b.ProjectsCode = c.ProjectsCode And c.DelFlag = 0 "
                + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' "
                + " And a.SchoolID = '" + CurrentUser.SchoolID + "' ";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And a.StrategiesName Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Group By a.StrategiesCode, a.StudyYear, a.StrategiesName, a.Df, a.Sort Order By a.Sort Desc"));
        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select * From Strategies Where StrategiesCode = '" + id + "'"));

        if (dv.Count != 0)
        {
            ddlYearS.SelectedValue = dv[0]["StudyYear"].ToString();
            txtStrategies.Text = dv[0]["StrategiesName"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Strategies", "StrategiesCode", id);

            string strSql = " Select StrategiesCode, RecNum As id, IndicatorsName, IsNull(Sort, 0) Sort "
                + " From StrategiesIndicators "
                + " Where StrategiesCode = '{0}' ";
            dvIndicators = Conn.Select(string.Format(strSql + " Order By Sort, RecNum ", id));

            if (dvIndicators.Count != 0)
            {
                btDelIndicators.Visible = true;
                if (Session["IndicatorsSuccess"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("id");
                    dt1.Columns.Add("Sort");
                    dt1.Columns.Add("IndicatorsName");

                    DataRow dr;
                    for (int i = 0; i < dvIndicators.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["id"] = dvIndicators[i]["id"].ToString();
                        dr["Sort"] = dvIndicators[i]["Sort"].ToString();
                        dr["IndicatorsName"] = dvIndicators[i]["IndicatorsName"].ToString();
                        dt1.Rows.Add(dr);
                    }
                    dvIndicators = dt1.DefaultView;
                    Session["IndicatorsSuccess"] = dt1;
                }
                GridViewIndicators.Visible = true;
                GridViewIndicators.DataSource = dvIndicators;
                GridViewIndicators.CheckListDataField = "id";
                GridViewIndicators.DataBind();
            }
            else
            {
                btDelIndicators.Visible = false;
                GridViewIndicators.Visible = false;
            }

            strSql = " Select StrategiesCode, RecNum As id, GoalsName, IsNull(Sort, 0) Sort "
                + " From StrategiesGoals "
                + " Where StrategiesCode = '{0}' ";
            dvGoals = Conn.Select(string.Format(strSql + " Order By Sort, RecNum ", id));

            if (dvGoals.Count != 0)
            {
                btDelGoals.Visible = true;
                if (Session["Goals"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("id");
                    dt1.Columns.Add("Sort");
                    dt1.Columns.Add("GoalsName");

                    DataRow dr;
                    for (int i = 0; i < dvGoals.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["id"] = dvGoals[i]["id"].ToString();
                        dr["Sort"] = dvGoals[i]["Sort"].ToString();
                        dr["GoalsName"] = dvGoals[i]["GoalsName"].ToString();
                        dt1.Rows.Add(dr);
                    }
                    dvGoals = dt1.DefaultView;
                    Session["Goals"] = dt1;
                }
                gridViewGoals.Visible = true;
                gridViewGoals.DataSource = dvGoals;
                gridViewGoals.CheckListDataField = "id";
                gridViewGoals.DataBind();
            }
            else
            {
                btDelGoals.Visible = false;
                gridViewGoals.Visible = false;
            }
        }
    }
    private void ClearAll()
    {
        Session.Remove("IndicatorsSuccess");
        Session.Remove("Goals");

        txtStrategies.Text = "";
        txtSearch.Text = "";
        GridViewIndicators.DataSource = null;
        GridViewIndicators.DataBind();
        btDelIndicators.Visible = false;
        GridViewIndicators.Visible = false;
        btc.GenSortNotDelFlag(txtSortIndicators, "StrategiesIndicators", "StrategiesCode = '" + Request.QueryString["id"] + "'");

        gridViewGoals.DataSource = null;
        gridViewGoals.DataBind();
        btDelGoals.Visible = false;
        gridViewGoals.Visible = false;
        btc.GenSortNotDelFlag(txtSortGoals, "StrategiesGoals", "StrategiesCode = '" + Request.QueryString["id"] + "'");
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            Int32 i = Conn.AddNew("Strategies", "StrategiesCode, StudyYear, StrategiesName, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", 
                NewID, ddlYearS.SelectedValue, txtStrategies.Text, txtSort.Text, 1, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

            //IndicatorsSuccess
            if (Session["IndicatorsSuccess"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["IndicatorsSuccess"];
                dvIndicators = dt1.DefaultView;
                for (int j = 0; j < dvIndicators.Count; j++)
                {
                    Conn.AddNew("StrategiesIndicators", "StrategiesCode, RecNum, IndicatorsName, Sort, ItemID", 
                        NewID, Convert.ToDecimal(dvIndicators[j]["id"]), dvIndicators[j]["IndicatorsName"].ToString(), dvIndicators[j]["Sort"].ToString(), Guid.NewGuid().ToString());
                }
            }

            //Goals
            if (Session["Goals"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Goals"];
                dvGoals = dt1.DefaultView;
                for (int j = 0; j < dvGoals.Count; j++)
                {
                    Conn.AddNew("StrategiesGoals", "StrategiesCode, RecNum, GoalsName, Sort, ItemID",
                        NewID, Convert.ToDecimal(dvGoals[j]["id"]), dvGoals[j]["GoalsName"].ToString(), dvGoals[j]["Sort"].ToString(), Guid.NewGuid().ToString());
                }
            }

            if (CkAgain == "N")
            {
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                Response.Redirect("Strategies.aspx?ckmode=1&Cr=" + i);  
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                btc.GenSort(txtSort, "Strategies", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                GridView2.Visible = true;
                DataBind();
            }
            if (CkAgain == "G")
            {
                Response.Redirect("Projects.aspx?mode=1&id=" + NewID + "&syear=" + ddlYearS.SelectedValue);
            }            
        }
        if (Request["mode"] == "2")
        {
            Int32 i = 0;
            i = Conn.Update("Strategies", "Where StrategiesCode = '" + Request["id"] + "' ", "StudyYear, StrategiesName, Sort, SchoolID, UpdateUser, UpdateDate", 
                ddlYearS.SelectedValue, txtStrategies.Text, txtSort.Text, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now);
            //IndicatorsSuccess
            if (Session["IndicatorsSuccess"] != null)
            {
                Conn.Delete("StrategiesIndicators", "Where StrategiesCode = '" + Request["id"] + "'");
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["IndicatorsSuccess"];
                dvIndicators = dt1.DefaultView;
                for (int j = 0; j < dvIndicators.Count; j++)
                {
                    Conn.AddNew("StrategiesIndicators", "StrategiesCode, RecNum, IndicatorsName, Sort, ItemID", 
                        Request["id"], Convert.ToDecimal(dvIndicators[j]["id"]), dvIndicators[j]["IndicatorsName"].ToString(), dvIndicators[j]["Sort"].ToString(), Guid.NewGuid().ToString());
                }
            }

            //Goals
            if (Session["Goals"] != null)
            {
                Conn.Delete("StrategiesGoals", "Where StrategiesCode = '" + Request["id"] + "'");
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Goals"];
                dvGoals = dt1.DefaultView;
                for (int j = 0; j < dvGoals.Count; j++)
                {
                    Conn.AddNew("StrategiesGoals", "StrategiesCode, RecNum, GoalsName, Sort, ItemID",
                        Request["id"], Convert.ToDecimal(dvGoals[j]["id"]), dvGoals[j]["GoalsName"].ToString(), dvGoals[j]["Sort"].ToString(), Guid.NewGuid().ToString());
                }
            }
            Response.Redirect("Strategies.aspx?ckmode=2&Cr=" + i); 
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
    protected void btSaveAndGo_Click(object sender, EventArgs e)
    {
        bt_Save("G");
    }
    private void Delete(string id)
    {
        Int32 i = 0;
        if (String.IsNullOrEmpty(id)) return;
        if (btc.CkUseData(id, "StrategiesCode", "Projects", "And DelFlag = 0"))
        {
            Response.Redirect("Strategies.aspx?ckmode=3&Cr=0");
        }
        else
        {
            i = Conn.Update("Strategies", "Where StrategiesCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            Conn.Delete("StrategiesIndicators", "Where StrategiesCode = '" + Request["id"] + "'");
            Conn.Delete("StrategiesGoals", "Where StrategiesCode = '" + Request["id"] + "'");
            Response.Redirect("Strategies.aspx?ckmode=3&Cr=" + i);  
        }
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "Strategies", ddlSearchYear.SelectedValue);
        btc.getdllStudyYearForCopy(ddlOldYear, ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlYearS_SelectedIndexChanged(object sender, EventArgs e)
    {
        btc.GenSort(txtSort, "Strategies", " And StudyYear = '" + ddlYearS.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
    }
    protected void btaddIndicators_Click(object sender, EventArgs e)
    {
        if (Session["IndicatorsSuccess"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("Sort");
            dt1.Columns.Add("IndicatorsName");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["Sort"] = txtSortIndicators.Text;
            dr["IndicatorsName"] = txtIndicators2.Text;
            dt1.Rows.Add(dr);

            dvIndicators = dt1.DefaultView;
            Session["IndicatorsSuccess"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["IndicatorsSuccess"];

            if (txtid.Text == "")
            {
                DataView ckdv = ((DataTable)Session["IndicatorsSuccess"]).DefaultView;
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["Sort"] = txtSortIndicators.Text;
                dr["IndicatorsName"] = txtIndicators2.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                Int32 i = Convert.ToInt32(txtid.Text);
                dt1.Rows[i]["Sort"] = txtSortIndicators.Text;
                dt1.Rows[i]["IndicatorsName"] = txtIndicators2.Text;
            }

            dvIndicators = dt1.DefaultView;
            Session["IndicatorsSuccess"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearIndicators();
        GridViewIndicators.DataSource = dvIndicators;
        GridViewIndicators.CheckListDataField = "id";
        GridViewIndicators.DataBind();
        if (dvIndicators.Count > 0)
        {
            btDelIndicators.Visible = true;
            GridViewIndicators.Visible = true;
        }
    }
    private void ClearIndicators()
    {
        txtid.Text = "";
        if (!string.IsNullOrEmpty(txtSortIndicators.Text))
        {
            txtSortIndicators.Text = (Convert.ToInt32(txtSortIndicators.Text) + 1).ToString();
        }
        txtIndicators2.Text = "";
    }
    protected void btDelIndicators_Click(object sender, EventArgs e)
    {
        if (GridViewIndicators.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["IndicatorsSuccess"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewIndicators.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvIndicators = dt1.DefaultView;
        Session["IndicatorsSuccess"] = dt1;
        GridViewIndicators.DataSource = dvIndicators;
        GridViewIndicators.DataBind();
        if (dvIndicators.Count == 0)
        {
            btDelIndicators.Visible = false;
        }
    }
    protected void btCopy_Click(object sender, EventArgs e)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        if (btc.CkDataDuplicate(ddlSearchYear.SelectedValue, "Strategies"))
        {
            btc.Msg_Head(Img1, MsgHead, true, "7", 0);
            return;
        }
        string strSql = " Select StrategiesCode, StudyYear, StrategiesName, Sort From Strategies Where DelFlag = 0 And StudyYear = '" + ddlOldYear.SelectedValue + "' Order By Sort ";
        DataView dvStrategies = Conn.Select(strSql);

        if (dvStrategies.Count != 0)
        {
            for (int i = 0; i < dvStrategies.Count; i++)
            {
                string NewID = Guid.NewGuid().ToString();
                Int32 x = Conn.AddNew("Strategies", "StrategiesCode, StudyYear, StrategiesName, Sort, Df, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate", 
                    NewID, ddlSearchYear.SelectedValue, dvStrategies[i]["StrategiesName"].ToString(), dvStrategies[i]["Sort"].ToString(), 0, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);

                strSql = "Select * From StrategiesIndicators Where StrategiesCode = '" + dvStrategies[i]["StrategiesCode"].ToString() + "'";
                DataView dvStrategiesIndicators = Conn.Select(strSql);

                if (dvStrategiesIndicators.Count != 0)
                {
                    for (int j = 0; j < dvStrategiesIndicators.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO StrategiesIndicators (StrategiesCode, RecNum, IndicatorsName, Sort, ItemID)VALUES('{0}',{1},'{2}',{3},'{4}');", 
                            NewID, Convert.ToDecimal(dvStrategiesIndicators[j]["RecNum"]), dvStrategiesIndicators[j]["IndicatorsName"].ToString(), dvStrategiesIndicators[j]["Sort"], Guid.NewGuid().ToString());

                        //Conn.AddNew("StrategiesIndicators", "StrategiesCode, RecNum, IndicatorsName, Sort, ItemID", 
                        //    NewID, Convert.ToDecimal(dvStrategiesIndicators[j]["RecNum"]), dvStrategiesIndicators[j]["IndicatorsName"].ToString(), dvStrategiesIndicators[j]["Sort"], Guid.NewGuid().ToString());
                    }
                }

                strSql = "Select * From StrategiesGoals Where StrategiesCode = '" + dvStrategies[i]["StrategiesCode"].ToString() + "'";
                DataView dvStrategiesGoals = Conn.Select(strSql);

                if (dvStrategiesGoals.Count != 0)
                {
                    for (int j = 0; j < dvStrategiesGoals.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO StrategiesGoals (StrategiesCode, RecNum, GoalsName, Sort, ItemID)VALUES('{0}',{1},'{2}',{3},'{4}');",
                            NewID, Convert.ToDecimal(dvStrategiesGoals[j]["RecNum"]), dvStrategiesGoals[j]["GoalsName"].ToString(), dvStrategiesGoals[j]["Sort"], Guid.NewGuid().ToString());

                        //Conn.AddNew("StrategiesGoals", "StrategiesCode, RecNum, GoalsName, Sort, ItemID",
                        //    NewID, Convert.ToDecimal(dvStrategiesGoals[j]["RecNum"]), dvStrategiesGoals[j]["GoalsName"].ToString(), dvStrategiesGoals[j]["Sort"], Guid.NewGuid().ToString());
                    }
                }
            }
            result = Conn.Execute(strbSql.ToString());
            btc.Msg_Head(Img1, MsgHead, true, "1", 1);
            btc.CopyEnable(lblCopy, ddlOldYear, btCopy, "Strategies", ddlSearchYear.SelectedValue);
            DataBind();
        }
        else
        {
            btc.Msg_Head(Img1, MsgHead, true, "6", 0);
        }
    }
    protected string checkedit(string id, string strName)
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            return String.Format("<a href=\"javascript:;\" onclick=\"EditItem('{0}');\">{1}</a>", id, strName);
        }
        else
        {
            return strName;
        }
    }
    protected void btAddGoals_Click(object sender, EventArgs e)
    {
        if (Session["Goals"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("Sort");
            dt1.Columns.Add("GoalsName");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["Sort"] = txtSortGoals.Text;
            dr["GoalsName"] = txtGoals.Text;
            dt1.Rows.Add(dr);

            dvGoals = dt1.DefaultView;
            Session["Goals"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Goals"];

            if (txtGid.Text == "")
            {
                DataView ckdv = ((DataTable)Session["Goals"]).DefaultView;
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["Sort"] = txtSortGoals.Text;
                dr["GoalsName"] = txtGoals.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                Int32 i = Convert.ToInt32(txtGid.Text);
                dt1.Rows[i]["Sort"] = txtSortGoals.Text;
                dt1.Rows[i]["GoalsName"] = txtGoals.Text;
            }

            dvGoals = dt1.DefaultView;
            Session["Goals"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearGoals();
        gridViewGoals.DataSource = dvGoals;
        gridViewGoals.CheckListDataField = "id";
        gridViewGoals.DataBind();
        if (dvGoals.Count > 0)
        {
            btDelGoals.Visible = true;
            gridViewGoals.Visible = true;
        }
    }
    private void ClearGoals()
    {
        txtGid.Text = "";
        if (!string.IsNullOrEmpty(txtSortGoals.Text))
        {
            txtSortGoals.Text = (Convert.ToInt32(txtSortGoals.Text) + 1).ToString();
        }
        txtGoals.Text = "";
    }
    protected void btDelGoals_Click(object sender, EventArgs e)
    {
        if (gridViewGoals.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Goals"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", gridViewGoals.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvGoals = dt1.DefaultView;
        Session["Goals"] = dt1;
        gridViewGoals.DataSource = dvGoals;
        gridViewGoals.DataBind();
        if (dvGoals.Count == 0)
        {
            btDelGoals.Visible = false;
        }
    }
}
