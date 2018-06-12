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

public partial class Standard : System.Web.UI.Page
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
            if (btc.ckIdentityName("ckStandardNew"))
            {
                lblTitle1.Text = "ประเด็นการพิจารณา";
                lblTitle2.Text = "ประเด็นการพิจารณา";
                lblSideSearch.Text = "มาตรฐาน";
                lblStandardNo.Text = "ประเด็นการพิจารณาที่";
                lblSide.Text = "มาตรฐาน";
                lblStandard.Text = "ประเด็นการพิจารณา";
                GridView1.Columns[0].HeaderText = "ประเด็นที่";
                GridView1.Columns[1].HeaderText = "ชื่อประเด็นการพิจารณา";
                GridView2.Columns[0].HeaderText = "ประเด็นการพิจารณาที่";
                GridView2.Columns[1].HeaderText = "ชื่อประเด็นการพิจารณา";
                btAdd.Text = "       สร้างประเด็นการพิจารณาใหม่";
                btAdd.ToolTip = "สร้างประเด็นการพิจารณาใหม่";
                btSave.ToolTip = "บันทึกประเด็นการพิจารณานี้";
                btSaveAgain.Text = "       บันทึกและสร้างประเด็นการพิจารณาใหม่";
                btSaveAgain.ToolTip = "บันทึกประเด็นการพิจารณานี้และสร้างประเด็นการพิจารณาใหม่";
            }
            else
            {
                lblTitle1.Text = "มาตรฐาน";
                lblTitle2.Text = "มาตรฐาน";
                lblSideSearch.Text = "ด้าน";
                lblStandardNo.Text = "มาตรฐานที่";
                lblSide.Text = "ด้าน";
                lblStandard.Text = "มาตรฐาน";
                GridView1.Columns[0].HeaderText = "มาตรฐานที่";
                GridView1.Columns[1].HeaderText = "ชื่อมาตรฐาน";
                GridView2.Columns[0].HeaderText = "มาตรฐานที่";
                GridView2.Columns[1].HeaderText = "ชื่อมาตรฐาน";
                btAdd.Text = "       สร้างมาตรฐานใหม่";
                btAdd.ToolTip = "สร้างมาตรฐานใหม่";
                btSave.ToolTip = "บันทึกมาตรฐานนี้";
                btSaveAgain.Text = "       บันทึกและสร้างมาตรฐานใหม่";
                btSaveAgain.ToolTip = "บันทึกมาตรฐานนี้และสร้างมาตรฐานใหม่";
            }  

            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);

            btc.LinkReport(linkReport);

            getddlYear(0);           
            getddlSide(0, ddlSearchYear.SelectedValue);
            btc.CkAdmission(GridView1, btAdd, null);
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        getddlSide(1, ddlYearB.SelectedValue);
                        getddlScoreGroup();
                        btc.GenSort(txtSort, "Standard", " And StudyYear = '" + ddlYearB.SelectedValue + "'");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        getddlScoreGroup();
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
        txtStandard.Attributes.Add("onkeyup", "Cktxt(0);");
        ddlSide.Attributes.Add("onchange", "Cktxt(0);");
        ddlScoreGroup.Attributes.Add("onchange", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
    }
    private void getddlYear(int mode)
    {
        if (mode == 0)
        {
            btc.getdllStudyYear(ddlSearchYear);
            btc.getDefault(ddlSearchYear, "StudyYear", "StudyYear");
        }
        if (mode == 1)
        {
            btc.getdllStudyYear(ddlYearB);
            btc.getDefault(ddlYearB, "StudyYear", "StudyYear");
        }
    }
    private void getddlScoreGroup()
    {
        DataView dv = btc.getScoreGroup();
        ddlScoreGroup.DataSource = dv;
        ddlScoreGroup.DataTextField = "ScoreGroupName";
        ddlScoreGroup.DataValueField = "ScoreGroupID";
        ddlScoreGroup.DataBind();
        ddlScoreGroup.Items.Insert(0, new ListItem("-เลือก-", ""));
        ddlScoreGroup.SelectedIndex = 0;
    }
    private void getddlSide(int mode, string StudyYear)
    {
        if (mode == 0)
        {
            btc.getddlSide(0, ddlSearch, StudyYear);
        }

        if (mode == 1)
        {
            btc.getddlSide(1, ddlSide, StudyYear);
        }
    }
    public override void DataBind()
    {
        string StrSql = " Select a.StandardCode, a.StandardName, a.Sort, IsNull(b.WeightScore, 0) WeightScore "
                    + " From Standard a Left Join ScoreGroup b On a.ScoreGroupID = b.ScoreGroupID "
                    + " Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";

        if (btc.ckIdentityName("ckStandardNew"))
        {
            StrSql = @" Select a.StandardCode, a.StandardName, Cast(S.Sort As nvarchar) + '.' + Cast(a.Sort As nvarchar) As Sort, IsNull(b.WeightScore, 0) WeightScore 
            From Side S Left Join Standard a On S.SideCode = a.SideCode
            Left Join ScoreGroup b On a.ScoreGroupID = b.ScoreGroupID 
            Where a.DelFlag = 0 And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' ";
        }

        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.SideCode = '" + ddlSearch.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And (a.StandardName Like '%" + txtSearch.Text + "%' Or a.Sort Like '%" + txtSearch.Text + "%')  ";
        }

        if (btc.ckIdentityName("ckStandardNew"))
        {
            StrSql += "  Order By S.Sort, a.Sort ";
        }
        else
        {
            StrSql += " Order By a.Sort ";
        }
        DataView dv = Conn.Select(string.Format(StrSql));
        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblSearchTotal.InnerText = dv.Count.ToString();

        GridView2.DataSource = dv;
        GridView2.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select * From Standard Where StandardCode = '" + id + "'"));

        if (dv.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlSide(1, ddlYearB.SelectedValue);          
            ddlSide.SelectedValue = dv[0]["SideCode"].ToString();
            txtStandard.Text = dv[0]["StandardName"].ToString();
            txtNote.Text = dv[0]["Note"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
            ddlScoreGroup.SelectedValue = dv[0]["ScoreGroupID"].ToString();
            getCriteria();
        }
        btc.getCreateUpdateUser(lblCreate, lblUpdate, "Standard", "StandardCode", id);
    }
    private void getCriteria()
    {
        string StrSql = "Select TcriteriaID, ScoreGroupID, Tcriteria, Detail, Criterion, Translation, TMin, TMax From TCriteria Where DelFlag = 0 ";
        StrSql = StrSql + " And ScoreGroupID = '" + ddlScoreGroup.SelectedValue + "' ";

        DataView dv = Conn.Select(string.Format(StrSql + " Order By Tcriteria, Criterion"));
        dgCriteria.DataSource = dv;
        dgCriteria.DataBind();
    }
    private void ClearAll()
    {
        txtStandard.Text = "";
        ddlSide.SelectedIndex = 0;
        txtNote.Text = "";
        txtSearch.Text = "";
        ddlScoreGroup.SelectedIndex = 0;
        dgCriteria.DataSource = null;
        dgCriteria.DataBind();
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save(string CkAgain)
    {
        Int32 i = 0;
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            i = Conn.AddNew("Standard", "StandardCode, StandardName, SideCode, StudyYear, Note, ScoreGroupID, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, txtStandard.Text, ddlSide.SelectedValue, ddlYearB.SelectedValue, txtNote.Text, ddlScoreGroup.SelectedValue, txtSort.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);           

            if (CkAgain == "N")
            {
                Response.Redirect("Standard.aspx?ckmode=1&Cr=" + i);    
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", i);
                ClearAll();
                btc.GenSort(txtSort, "Standard", " And StudyYear = '" + ddlYearB.SelectedValue + "'");
                GridView2.Visible = true;
                DataBind();
            }
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Standard", "Where StandardCode = '" + Request["id"] + "' ", "StandardName, SideCode, StudyYear, Note, ScoreGroupID, Sort, UpdateUser, UpdateDate", txtStandard.Text, ddlSide.SelectedValue, ddlYearB.SelectedValue, txtNote.Text, ddlScoreGroup.SelectedValue, txtSort.Text, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Standard.aspx?ckmode=2&Cr=" + i); 
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
        if (btc.CkUseData(id, "StandardCode", "Indicators", "And DelFlag = 0"))
        {
            Response.Redirect("Standard.aspx?ckmode=3&Cr=0");    
        }
        else
        {
            Int32 i = Conn.Update("Standard", "Where StandardCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
            Response.Redirect("Standard.aspx?ckmode=3&Cr=" + i);    
        }
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlSide(0, ddlSearchYear.SelectedValue);
        DataBind();
    }
    protected void ddlYearB_SelectedIndexChanged(object sender, EventArgs e)
    {
       getddlSide(1, ddlYearB.SelectedValue);
       btc.GenSort(txtSort, "Standard", " And StudyYear = '" + ddlYearB.SelectedValue + "'");
    }
    protected void ddlScoreGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        getCriteria();
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
}
