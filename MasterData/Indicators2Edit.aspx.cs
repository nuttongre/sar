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
using System.IO;

public partial class Indicators2Edit : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    
    public static DataView dv;
    public static string path = ConfigurationManager.AppSettings["FilePath"].ToString();

    protected override void OnInit(EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["opt"])) AjaxOption(Request.QueryString["opt"]);
        base.OnInit(e);
    }
    private void AjaxOption(string opt)
    {
        object data = "";
        switch (opt)
        {
            case "delete":
                break;
            case "delAtt":
                data = DeleteItems(Request.QueryString["attID"]);
                break;
        }

        Response.Write(data.ToString());
        Response.End();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            if (!string.IsNullOrEmpty(Request["Cr"]))
            {
                btc.Msg_Head(Img1, MsgHead, true, Request["ckmode"], Convert.ToInt32(Request["Cr"]));
            }

            //เช็คปีงบประมาณ
            btc.ckBudgetYear(lblSearchYear, lblYear);
            if (btc.ckIdentityName("ckBudgetYear"))
            {
                lblYear2.InnerText = "ปีการศึกษา : ";
            }

            getddlYear(0);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
            getddlActivity(0, ddlSearch.SelectedValue);
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("IndDept").ToString(), Cookie.GetValue2("IndEmp").ToString());

            string mode = Request["mode"];
            string delmode = Request.QueryString["delmode"];
            int ij = string.IsNullOrEmpty(Request.QueryString["i"])? 0 : Convert.ToInt32(Request.QueryString["i"]);
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        GetData(Request["id"]);
                        GetDataAttach(Request["id"]);
                        EnableTxt(false);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "CkPercent(0);", true);
                        break;
                    case "3":
                        break;
                    case "4":
                        btc.Msg_Head(Img1, MsgHead, true, "2", ij);
                        break;
                }
            }
            else
            {
                DataBind();
            }
            if (!String.IsNullOrEmpty(delmode))
            {
                //DeleteItems();
                //DataBind2();
            }
            
        }
        ddlStrategies.Attributes.Add("onchange", "Cktxt(0);");
        ddlProjects.Attributes.Add("onchange", "Cktxt(0);");
        ddlActivity.Attributes.Add("onchange", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
        txtWeight.Attributes.Add("onchange", "CkWeight();");
        txtCan.Attributes.Add("onchange", "CkNum(); CkPercent(0);");
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
    private void getddlStrategies(int mode, string StudyYear)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, StudyYear);
            if (Session["Ind2Strategies"] == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Session["Ind2Strategies"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch2.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlStrategies(1, ddlStrategies, StudyYear);
        }
    }
    private void getddlProjects(int mode, string StudyYear, string StrategiesCode)
    {
        if (mode == 0)
        {
            if (string.IsNullOrEmpty(StrategiesCode))
            {
                Session.Remove("Ind2Projects");
            }
            btc.getddlProjects(0, ddlSearch, StudyYear, StrategiesCode);
            if (Session["Ind2Projects"] == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch.SelectedValue = Session["Ind2Projects"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlProjects(1, ddlProjects, StudyYear, StrategiesCode);
        }
    }
    private void getddlActivity(int mode, string ProjectsCode)
    {
        if (mode == 0)
        {
            btc.getddlActivityIsApprove(0, ddlSearch1, ProjectsCode);
            if (Session["Ind2Activity"] == null)
            {
                ddlSearch1.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch1.SelectedValue = Session["Ind2Activity"].ToString();
                }
                catch (Exception ex)
                {
                    ddlSearch1.SelectedIndex = 0;
                }
            }
        }

        if (mode == 1)
        {
            btc.getddlActivityIsApprove(1, ddlActivity, ProjectsCode);
        }
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    private void getddlEmpByDept(int mode, string DeptCode)
    {
        if (mode == 0)
        {
            btc.getddlEmpByDept(0, ddlSearchEmp, DeptCode);
        }
    }
    private void getActivityDetail(string ActivityCode)
    {
        DataView dv = btc.getActivityDetail(ActivityCode);
        if (dv.Count != 0)
        {
            txtActivityDetail.Text = dv[0]["ActivityDetail"].ToString();
            lblType.Text = dv[0]["CostsType"].ToString();
            lblTerm.Text = dv[0]["Term"].ToString() + "/" + dv[0]["YearB"].ToString();
            lblBudgetYear.Text = dv[0]["BudgetYear"].ToString();
            txtTotalAmount.Text = Convert.ToDouble(dv[0]["TotalAmount"]).ToString("#,##0.00");
            lblDepartment.Text = dv[0]["DeptName"].ToString();
            lblEmpName.Text = dv[0]["EmpName"].ToString();
            txtSDay.Text = Convert.ToDateTime(dv[0]["SDate"]).ToShortDateString();
            txtEDay.Text = Convert.ToDateTime(dv[0]["EDate"]).ToShortDateString();
        }
        else
        {
            txtActivityDetail.Text = "";
            lblType.Text = "";
            lblTerm.Text = "";
            lblBudgetYear.Text = "";
            txtTotalAmount.Text = "0.00";
            lblDepartment.Text = "";
            lblEmpName.Text = "";
            txtSDay.Text = "";
            txtEDay.Text = "";
        }
    }
    public override void DataBind()
    {
        string StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                    + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, "
                    + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort "
                    + " From Projects a, Activity b, Indicators2 c Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode " 
                    + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' "
                    + " And b.ApproveFlag = 1 And c.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                    + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, "
                    + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort "
                    + " From Projects a, Activity b, Indicators2 c, dtAcDept d "
                    + " Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And b.ActivityCode = d.ActivityCode " 
                    + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And b.ApproveFlag = 1 And d.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                        + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, "
                        + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort "
                        + " From Projects a, Activity b, Indicators2 c, dtAcEmp d "
                        + " Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And b.ActivityCode = d.ActivityCode "
                        + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And b.ApproveFlag = 1 And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = "Select a.ProjectsName FullName, b.ActivityCode, b.ActivityName, "
                        + " c.Indicators2Code, c.IndicatorsName2 IndicatorsName2, c.Weight, "
                        + " WeightType = Case c.WeightType When 0 Then '%' When 1 Then 'จำนวน' End, c.OffAll, c.OffThat, c.Sort "
                        + " From Projects a, Activity b, Indicators2 c, dtAcEmp d, dtAcDept e "
                        + " Where c.DelFlag = 0 And a.ProjectsCode = b.ProjectsCode And b.ActivityCode = d.ActivityCode And b.ActivityCode = e.ActivityCode "
                        + " And b.ActivityCode = c.ActivityCode And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And c.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And b.ApproveFlag = 1 And d.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And e.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.ProjectsCode = '" + ddlSearch.SelectedValue + "' ";
        }
        if (ddlSearch1.SelectedIndex != 0)
        {
            StrSql = StrSql + " And b.ActivityCode = '" + ddlSearch1.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And c.IndicatorsName2 Like '%" + txtSearch.Text + "%' ";
        }
        
        DataView dv = Conn.Select(string.Format(StrSql + " Order By a.Sort Desc, b.Sort Desc, c.Sort Desc" ));

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;

        DataView dv = Conn.Select(string.Format("Select a.ProjectsCode, a.StudyYear, a.StrategiesCode, b.ActivityCode, b.Note, c.IndicatorsName2, c.Weight, c.WeightType, c.OffAll, c.OffThat, c.APercent, c.CkCriterion, c.Sort From Projects a, Activity b, Indicators2 c Where a.ProjectsCode = b.ProjectsCode And b.ActivityCode = c.ActivityCode And c.Indicators2Code = '" + id + "' "));

        if (dv.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            getddlStrategies(1, ddlYearB.SelectedValue);
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
            ddlProjects.SelectedValue = dv[0]["ProjectsCode"].ToString();
            getddlActivity(1, ddlProjects.SelectedValue);
            ddlActivity.SelectedValue = dv[0]["ActivityCode"].ToString();
            getActivityDetail(ddlActivity.SelectedValue);
            txtNote.Text = dv[0]["Note"].ToString();
            txtIndicators2Edit.Text = dv[0]["IndicatorsName2"].ToString();
            txtWeight.Text = Convert.ToInt32(dv[0]["Weight"]).ToString("#,##0");
            lblWeightType.Text = (Convert.ToInt32(dv[0]["WeightType"]) == 0) ? "%" : "";
            txtAll.Text = string.IsNullOrEmpty(dv[0]["OffAll"].ToString()) ? "0" : Convert.ToInt32(dv[0]["OffAll"]).ToString("#,##0");
            txtCan.Text = string.IsNullOrEmpty(dv[0]["OffThat"].ToString()) ? "0" : Convert.ToInt32(dv[0]["OffThat"]).ToString("#,##0");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "CkPercent(0);", true);
            txtSort.Text = dv[0]["Sort"].ToString();

            if (txtAll.Text == "5")
            {
                RateDetail.Visible = true;
            }
            //DataBind2();
        }
    }
    private void ClearAll()
    {
        txtIndicators2Edit.Text = "";
        txtWeight.Text = "50";
        txtSearch.Text = "";
        txtSort.Text = "";
        txtNote.Text = "";
    }
    private void EnableTxt(Boolean Enables)
    {
        ddlYearB.Enabled = Enables;
        ddlStrategies.Enabled = Enables;
        ddlProjects.Enabled = Enables;
        ddlActivity.Enabled = Enables;
        txtSort.Enabled = Enables;
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        Int32 CkCriterion = 1;
        if (Convert.ToDouble(txtPercent.Text) < 50)
        {
            CkCriterion = 0;
        }
        
        Int32 i = 0; 
        if (Request["mode"] == "2")
        {
            Conn.Update("Activity", "Where ActivityCode = '" + Request["acid"] + "' ", "Note", txtNote.Text);
            i = Conn.Update("Indicators2", "Where Indicators2Code = '" + Request["id"] + "' ", "OffThat, APerCent, CkCriterion", Convert.ToDouble(txtCan.Text), Convert.ToDouble(txtPercent.Text), CkCriterion);
            Conn.Update("Evaluation", "Where Indicators2Code = '" + Request["id"] + "' ", "OffAll, OffThat, APerCent, CkCriterion, UpdateUser, UpdateDate", Convert.ToDouble(txtAll.Text), Convert.ToDouble(txtCan.Text), Convert.ToDouble(txtPercent.Text), CkCriterion, CurrentUser.ID, DateTime.Now);

            //btc.ChangeDefault("Activity", "ActivityCode", ddlActivity.SelectedValue);
            //btc.Msg_Head(Img1, MsgHead, true, "2", i);
        }
        Response.Redirect("Indicators2Edit.aspx?ckmode=2&Cr=" + i);    
        //Response.Redirect("Indicators2Edit.aspx?mode=4&i="+ i);
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlStrategies(1, ddlYearB.SelectedValue);
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void ddlStrategies_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void ddlProjects_OnSelectedChanged(object sender, EventArgs e)
    {
        getddlActivity(1, ddlProjects.SelectedValue);
        getActivityDetail(ddlActivity.SelectedValue);
    }
    protected void ddlActivity_OnSelectedChanged(object sender, EventArgs e)
    {
        getActivityDetail(ddlActivity.SelectedValue);
        btc.GenSort(txtSort, "Indicators2", " And ActivityCode = '" + ddlActivity.SelectedValue + "'");
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Ind2Strategies"] = ddlSearch2.SelectedValue;
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        getddlActivity(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Ind2Projects"] = ddlSearch.SelectedValue;
        ddlSearch2.SelectedValue = btc.getStrategiesCodeOfProj(ddlSearch.SelectedValue);
        Session["Ind2Strategies"] = ddlSearch2.SelectedValue;
        getddlActivity(0, ddlSearch.SelectedValue);
        DataBind();
    }
    protected void ddlSearch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Ind2Activity"] = ddlSearch1.SelectedValue;
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("IndEmp", ddlSearchEmp.SelectedValue);
        DataBind();
    }
    //public void DataBind2()
    //{   
    //    DataView dv = new DataView();
    //    string strSql = "Select AttachID, Indicators2Code, SubString(FileName,38,len(FileName)) FileName, ContentType, DelFlag From AttachFile Where DelFlag = 0 AND Indicators2Code='{0}'";
    //    dv = Conn.Select(string.Format(strSql, Request.QueryString["id"]));
    //    rptFile.DataSource = dv;
    //    rptFile.DataBind();
    //}
    //private void DeleteItems()
    //{   
    //    Conn.Update("AttachFile", string.Format("WHERE AttachId='{0}'", Request.QueryString["did"]), "DelFlag", 1);
    //}
    //protected void btnAttach_Click(object sender, EventArgs e)
    //{
        
    //    if (fpAttach.HasFile)
    //    {
    //        string NewID = Guid.NewGuid().ToString();
    //        int rowsEffect = Conn.AddNew("AttachFile", "AttachId,Indicators2Code,FileName,ContentType,DelFlag", NewID, Request.QueryString["id"], NewID + "_" + fpAttach.FileName, fpAttach.PostedFile.ContentType, 0);
    //        //DataRow[] drSub = dv.Table.Select(string.Format("JobId='{0}'", Request.QueryString["i"]));
    //        //DataRow[] drMain = dv.Table.Select(string.Format("JobId='{0}'", drSub[0]["ParentId"].ToString()));
    //        fpAttach.SaveAs(Server.MapPath(path + NewID + "_" + Path.GetFileName(fpAttach.PostedFile.FileName)));
    //        DataBind2();
    //        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "CkPercent(0);", true);
    //    }
    //}
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "OffThat").ToString() != "0")
            {
                e.Row.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                e.Row.Font.Bold = true;
            }

            if (!(e.Row.RowType == DataControlRowType.Header))
            {
                //e.Row.Attributes.Add("onclick", "javascript:window.location='Indicators2Edit.aspx?mode=2&id=" + DataBinder.Eval(e.Row.DataItem, "Indicators2Code") + "&acid=" + DataBinder.Eval(e.Row.DataItem, "ActivityCode") + "'");
            }
        }
    }
    protected string AttachShow(string id)
    {
        string strLink = "";
        DataView dv = Conn.Select("Select Count(ItemId) CountAtt From Multimedia Where ReferID = '" + id + "'");
        if (dv.Count != 0)
        {
            if (Convert.ToInt16(dv[0]["CountAtt"]) > 0)
            {
                strLink = "<a href=\"javascript:;\" onclick=\"AttachShow('" + id + "');\">"
                         + "<img style=\"border: 0; cursor: pointer;\" title=\"แสดงไฟล์แนบ\" src=\"../Image/AttachIcon.png\" /></a>";
            }
        }
        return strLink;
    }
    private int DeleteItems(string id)
    {
        int i = 0;
        i = Conn.Delete("Multimedia", string.Format("WHERE ItemID='{0}'", id));
        return i;
    }
    private void GetDataAttach(string refid)
    {
        object id;
        if (string.IsNullOrEmpty(refid)) id = DBNull.Value; else id = refid;

        DataView dv = Conn.Call("getAttachFile", "ReferID", id);
        rptAttach.DataSource = dv;
        rptAttach.DataBind();
    }
    protected void btnAttach_Click(object sender, EventArgs e)
    {
        if (fpAttach.HasFile)
        {
            if (btc.ckAttachFileGetExtensionError(fpAttach))
            {
                string NewID = Guid.NewGuid().ToString();
                int rowsEffect = Conn.AddNew("Multimedia", "ItemID,TypeID,Title,FileUrl,FileSize,FileType,ReferID,MediaYear,CreateUser,CreateDate,UpdateUser,UpdateDate,Source,Shared,Enabled,Flag",
                    NewID, cbDuo.Checked, fpAttach.FileName, "", fpAttach.PostedFile.ContentLength, fpAttach.PostedFile.ContentType, Request["id"], DateTime.Now.Year, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now, 1, 0, 1, 0);
                btc.UploadFileAttach(fpAttach, NewID, btc.getAttachType(fpAttach.PostedFile.ContentType, Convert.ToInt32(cbDuo.Checked)));

                if (cbDuo.Checked && fpAttach.FileName.ToString().Substring(fpAttach.FileName.ToString().IndexOf('.')).Contains(".zip"))
                {
                    UnZipFiles(fpAttach, NewID);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "alert('ไม่รองรับไฟล์นี้');", true);
            }
            cbDuo.Checked = false;
            GetDataAttach(Request["id"]);
        }
    }
    protected string getImgAttatch(object ItemID, object title, object filetype, object TypeId)
    {
        string link = btc.getImageAttachFileType(btc.getAttachType(filetype.ToString(), Convert.ToInt32(TypeId)), ItemID, title);
        return string.Format(link);
    }
    private void UnZipFiles(FileUpload fpAttach, string NewID)
    {
        string path = ConfigurationManager.AppSettings["FilePath"].ToString();

        string UlFileName = "";
        string[] filetype = fpAttach.FileName.Split('.');

        ICSharpCode.SharpZipLib.Zip.FastZip myZip = new ICSharpCode.SharpZipLib.Zip.FastZip();

        UlFileName = path + NewID + "." + filetype[1].ToString();
        Page sv = new Page();
        myZip.ExtractZip(sv.Server.MapPath(UlFileName), sv.Server.MapPath("~/Temp/" + NewID), "");
    }
}
