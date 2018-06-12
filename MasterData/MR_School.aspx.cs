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
using System.Drawing;
using System.Drawing.Imaging;

public partial class MR_School : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();
    DataView dvDeputyDirector;

    string ImgName1 = "";
    string NewID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            Session.Remove("DeputyDirector");
            if (!string.IsNullOrEmpty(Request["Cr"]))
            {
                btc.Msg_Head(Img1, MsgHead, true, Request["ckmode"], Convert.ToInt32(Request["Cr"]));
            }

            string mode = Request["mode"];
            if (!String.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear();
                        getMission(ddlYearS.SelectedValue);
                        getGoals(ddlYearS.SelectedValue);
                        getStrategies(ddlYearS.SelectedValue);
                        btc.getddlDay(ddlSDay);
                        btc.getddlMonth2(ddlSMonth);
                        getddlYear(ddlSYear, 150);
                        getddlProvince();
                        getlblKeyWord();
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        LinkReport();
                        getddlYear();
                        btc.getddlDay(ddlSDay);
                        btc.getddlMonth2(ddlSMonth);
                        getddlYear(ddlSYear, 150);
                        getddlProvince();
                        getlblKeyWord();
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
        txtSchoolID.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSchoolName.Attributes.Add("onkeyup", "Cktxt(0);");
        txtAddress.Attributes.Add("onkeyup", "Cktxt(0);");
        ddlProvince.Attributes.Add("onchange", "Cktxt(0);");
        txtTel.Attributes.Add("onkeyup", "Cktxt(0);");
        txtAreaOfStudy.Attributes.Add("onkeyup", "Cktxt(0);");
        txtIdentity.Attributes.Add("onkeyup", "Cktxt(0);");
        txtIdentity2.Attributes.Add("onkeyup", "Cktxt(0);");
        txtVision.Attributes.Add("onkeyup", "Cktxt(0);");
        txtManagerName.Attributes.Add("onkeyup", "Cktxt(0);");
        //txtArea.Attributes.Add("onkeyup", "CkComma();");
        ddlSDay.Attributes.Add("onchange", "ckddlDate();");
        ddlSMonth.Attributes.Add("onchange", "ckddlDate();");
        ddlSYear.Attributes.Add("onchange", "ckddlDate();");
    }
    private void getddlYear()
    {
        btc.getdllStudyYear(ddlYearS);
        btc.getDefault(ddlYearS, "StudyYear", "StudyYear");
    }
    private void getddlYear(DropDownList ddl, int year)
    {
        for (int i = 0; i < year; i++)
        {
            ddl.Items.Insert(i, new ListItem(((DateTime.Now.Year + 543) - i).ToString(), ((DateTime.Now.Year + 543) - i).ToString()));
        }
        ddl.SelectedValue = (DateTime.Now.Year + 543).ToString();
    }
    private void getddlProvince()
    {
        DataView dv = btc.getddlProvince();
        ddlProvince.DataSource = dv;
        ddlProvince.DataTextField = "ProvinceName";
        ddlProvince.DataValueField = "ProvinceID";
        ddlProvince.DataBind();
        ddlProvince.Items.Insert(0, new ListItem("-���͡-", ""));
        ddlProvince.SelectedIndex = 0;
    }
    private void getMission(string StudyYear)
    {
        DataView dv = Conn.Select("Select Cast(Sort As nVarChar) + '. ' + MissionName MissionName From Mission Where DelFlag = 0 And StudyYear = '" + StudyYear + "' Order By Sort");
        rptMission.DataSource = dv;
        rptMission.DataBind();
    }
    private void getGoals(string StudyYear)
    {
        DataView dv = Conn.Select("Select Cast(Sort As nVarChar) + '. ' + GoalsName GoalsName From Goals Where DelFlag = 0 And StudyYear = '" + StudyYear + "' Order By Sort");
        rptGoals.DataSource = dv;
        rptGoals.DataBind();
    }
    private void getStrategies(string StudyYear)
    {
        DataView dv = Conn.Select("Select '���ط���� ' + Cast(Sort As nVarChar) + '.  ' + StrategiesName StrategiesName From Strategies Where DelFlag = 0 And StudyYear = '" + StudyYear + "' And SchoolID = '" + CurrentUser.SchoolID + "' Order By Sort");
        rptStrategies.DataSource = dv;
        rptStrategies.DataBind();
    }
    private void getlblKeyWord()
    {
        string[] lblKeyWord = btc.getKeyWordResponsibleActivity().Split(',');
        txtKeyWordDeputyDirector.Text = lblKeyWord[4];
        txtKeyWordGroupLeaderPlan.Text = lblKeyWord[5];
    }
   public override void DataBind()
    {
        String StrSql = "Select SchoolID, SchoolNo, SchoolName, Tel, BirthDate, ManagerName From MR_School Where DelFlag = 0 ";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And SchoolName Like '%" + txtSearch.Text + "%' ";
        }

        DataView dv = Conn.Select(string.Format(StrSql));
        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    private void GetData(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select * From MR_School Where SchoolID = '" + id + "'"));

        if (dv.Count != 0)
        {
            txtSchoolID.Text = dv[0]["SchoolNo"].ToString();
            txtSchoolName.Text = dv[0]["SchoolName"].ToString();
            txtAddress.Text = dv[0]["Address"].ToString();
            ddlProvince.SelectedValue = dv[0]["ProvinceID"].ToString();
            txtTel.Text = dv[0]["Tel"].ToString();
            txtFax.Text = dv[0]["Fax"].ToString();
            txtArea.Text = dv[0]["strTotalArea"].ToString();
   
            cbPrimary.Checked = Convert.ToBoolean(dv[0]["IsPrimaryEdu"]);
            cbSecondary.Checked = Convert.ToBoolean(dv[0]["IsSecondary"]); 
            cbHighSchool.Checked = Convert.ToBoolean(dv[0]["IsHighSc"]);

            txtAreaOfStudy.Text = dv[0]["AreaStudyID"].ToString();
            txtDateBegin.Text = Convert.ToDateTime(dv[0]["BirthDate"]).ToShortDateString(); 
            ddlSDay.SelectedValue = Convert.ToDateTime(dv[0]["BirthDate"]).Day.ToString("00");
            ddlSMonth.SelectedValue = Convert.ToDateTime(dv[0]["BirthDate"]).Month.ToString("00");
            ddlSYear.SelectedValue = (Convert.ToDateTime(dv[0]["BirthDate"]).Year + 543).ToString();
            txtSchoolColor.Text = dv[0]["SchoolColor"].ToString();
            txtPhilosophy.Text = dv[0]["Philosophy"].ToString();
            txtSlogan.Text = dv[0]["Slogan"].ToString();
            txtPolicy.Text = dv[0]["Policy"].ToString();
            txtHistory.Text = dv[0]["History"].ToString();

            txtManagerPlanName.Text = dv[0]["ManagerPlanName"].ToString();
            txtPositionPlanName.Text = dv[0]["PositionPlanName"].ToString();
            txtManagerSuppliesName.Text = dv[0]["ManagerSuppliesName"].ToString();
            txtPositionSuppliesName.Text = dv[0]["PositionSuppliesName"].ToString();
            txtManagerMoneyName.Text = dv[0]["ManagerMoneyName"].ToString();
            txtPositionMoneyName.Text = dv[0]["PositionMoneyName"].ToString();
            txtUnderBudgetName.Text = dv[0]["UnderBudgetName"].ToString();
            txtPositionManagerBudgetName.Text = dv[0]["PositionManagerBudgetName"].ToString();
            txtUnderManagerName.Text = dv[0]["UnderManagerName"].ToString();
            txtPositionUnderManagerName.Text = dv[0]["PositionUnderManagerName"].ToString();
            //txtDeputyDirectorName.Text = dv[0]["DeputyDirectorName"].ToString();
            //txtPositionDeputyDirector.Text = dv[0]["PositionDeputyDirector"].ToString();
            txtGroupLeaderPlanName.Text = dv[0]["GroupLeaderPlanName"].ToString();
            txtPositionGroupLeaderPlan.Text = dv[0]["PositionGroupLeaderPlan"].ToString();

            string strSql = " Select DeputyDirectorID, DeputyDirectorName, DeputyDirectorPosition, Sort As id "
                + " From DeputyDirector "
                + " Where SchoolID = '{0}' ";
            dvDeputyDirector = Conn.Select(string.Format(strSql + " Order By Sort ", id));

            if (dvDeputyDirector.Count != 0)
            {
                btDelDeputyDirector.Visible = true;
                if (Session["DeputyDirector"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("id");
                    dt1.Columns.Add("DeputyDirectorName");
                    dt1.Columns.Add("DeputyDirectorPosition");

                    DataRow dr;
                    for (int i = 0; i < dvDeputyDirector.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["id"] = dvDeputyDirector[i]["id"].ToString();
                        dr["DeputyDirectorName"] = dvDeputyDirector[i]["DeputyDirectorName"].ToString();
                        dr["DeputyDirectorPosition"] = dvDeputyDirector[i]["DeputyDirectorPosition"].ToString();
                        dt1.Rows.Add(dr);
                    }
                    dvDeputyDirector = dt1.DefaultView;
                    Session["DeputyDirector"] = dt1;
                }
                GridViewDeputyDirector.Visible = true;
                GridViewDeputyDirector.DataSource = dvDeputyDirector;
                GridViewDeputyDirector.CheckListDataField = "id";
                GridViewDeputyDirector.DataBind();
            }
            else
            {
                btDelDeputyDirector.Visible = false;
                GridViewDeputyDirector.Visible = false;
            }
            
            //ddlYearS.SelectedValue = dv[0]["StudyYear"].ToString();
            getMission(ddlYearS.SelectedValue);
            getGoals(ddlYearS.SelectedValue);
            getStrategies(ddlYearS.SelectedValue);

            txtIdentity.Text = dv[0]["IdentityName"].ToString();
            txtIdentity2.Text = dv[0]["IdentityName2"].ToString();
            txtVision.Text = dv[0]["Vision"].ToString();
            txtManagerName.Text = dv[0]["ManagerName"].ToString();
            txtPositionManagerName.Text = dv[0]["PositionManagerName"].ToString();

            if (!string.IsNullOrEmpty(dv[0]["LogoPath"].ToString()))
            {
                imgPicture.ImageUrl = dv[0]["LogoPath"].ToString();
                ImgBt.Visible = true;
            }
            else
            {
                imgPicture.ImageUrl = "../Image/Menu9.png";
                ImgBt.Visible = false;
            }

            if (btc.ckGetAdmission(CurrentUser.UserRoleID) != 1)
            {
                btSave.Visible = false;
                fiUpload.Visible = false;
                btnUpload.Visible = false;
                ImgBt.Visible = false;
            }
            else
            {
                btSave.Visible = true;
                fiUpload.Visible = true;
                btnUpload.Visible = true;
            }
        }
    }
    private void ClearAll()
    {
        txtSearch.Text = "";
        ImgName1 = "";
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {     
        Int32 i = 0;
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();

            i = Conn.AddNew("MR_School", "SchoolID, SchoolNo, SchoolName, Address, ProvinceID, Tel, Fax, strTotalArea, IsPrimaryEdu, IsSecondary, IsHighSc, AreaStudyID, BirthDate, Philosophy, Slogan, Policy, History, StudyYear, IdentityName, Vision, ManagerName, PositionManagerName, SchoolColor, IdentityName2, iNameShow, iNameShow2, PlanNo, ckPlanNo, ckStrategySPT, ckStatute, ckStandardNation, ckStandardMinistry, ManagerPlanName, PositionPlanName, ManagerSuppliesName, PositionSuppliesName, ManagerMoneyName, PositionMoneyName, UnderBudgetName, PositionManagerBudgetName, UnderManagerName, PositionUnderManagerName, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate, DeputyDirectorName, PositionDeputyDirector, GroupLeaderPlanName, PositionGroupLeaderPlan", 
            NewID, txtSchoolID.Text, txtSchoolName.Text, txtAddress.Text, ddlProvince.SelectedValue, txtTel.Text, txtFax.Text, txtArea.Text, cbPrimary.Checked ? 1 : 0, cbSecondary.Checked ? 1 : 0, cbHighSchool.Checked ? 1 : 0, txtAreaOfStudy.Text, Convert.ToDateTime(txtDateBegin.Text).ToString("s"), txtPhilosophy.Text, txtSlogan.Text, txtPolicy.Text, txtHistory.Text, ddlYearS.SelectedValue, txtIdentity.Text, txtVision.Text, txtManagerName.Text, txtPositionManagerName.Text, txtSchoolColor.Text, txtIdentity2.Text, 0, 0, "11", 0, 0, 0, 0, 0, txtManagerPlanName.Text, txtPositionPlanName.Text, txtManagerSuppliesName.Text, txtPositionSuppliesName.Text, txtManagerMoneyName.Text, txtPositionMoneyName.Text, txtUnderBudgetName.Text, txtPositionManagerBudgetName.Text, txtUnderManagerName.Text, txtPositionUnderManagerName.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now, txtDeputyDirectorName.Text, txtPositionDeputyDirector.Text, txtGroupLeaderPlanName.Text, txtPositionGroupLeaderPlan.Text);

            //DeputyDirector
            if (Session["DeputyDirector"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["DeputyDirector"];
                dvDeputyDirector = dt1.DefaultView;
                for (int j = 0; j < dvDeputyDirector.Count; j++)
                {
                    Conn.AddNew("DeputyDirector", "DeputyDirectorID, DeputyDirectorName, DeputyDirectorPosition, Sort, CreateDate, CreateUser, UpdateDate, UpdateUser, SchoolID",
                        Guid.NewGuid().ToString(), dvDeputyDirector[j]["DeputyDirectorName"].ToString(), dvDeputyDirector[j]["DeputyDirectorPosition"].ToString(), dvDeputyDirector[j]["id"], DateTime.Now, CurrentUser.ID, DateTime.Now, CurrentUser.ID, CurrentUser.SchoolID);
                }
            }

            Response.Redirect("MR_School.aspx?ckmode=1&Cr=" + i);   
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("MR_School", "Where SchoolID = '" + Request["id"] + "' ", "SchoolNo, SchoolName, Address, ProvinceID, Tel, Fax, strTotalArea, IsPrimaryEdu, IsSecondary, IsHighSc, AreaStudyID, BirthDate, Philosophy, Slogan, Policy, History, StudyYear, IdentityName, Vision, ManagerName, PositionManagerName, SchoolColor, IdentityName2, ManagerPlanName, PositionPlanName, ManagerSuppliesName, PositionSuppliesName, ManagerMoneyName, PositionMoneyName, UnderBudgetName, PositionManagerBudgetName, UnderManagerName, PositionUnderManagerName, UpdateUser, UpdateDate, DeputyDirectorName, PositionDeputyDirector, GroupLeaderPlanName, PositionGroupLeaderPlan",
                txtSchoolID.Text, txtSchoolName.Text, txtAddress.Text, ddlProvince.SelectedValue, txtTel.Text, txtFax.Text, txtArea.Text, cbPrimary.Checked ? 1 : 0, cbSecondary.Checked ? 1 : 0, cbHighSchool.Checked ? 1 : 0, txtAreaOfStudy.Text, Convert.ToDateTime(txtDateBegin.Text).ToString("s"), txtPhilosophy.Text, txtSlogan.Text, txtPolicy.Text, txtHistory.Text, ddlYearS.SelectedValue, txtIdentity.Text, txtVision.Text, txtManagerName.Text, txtPositionManagerName.Text, txtSchoolColor.Text, txtIdentity2.Text, txtManagerPlanName.Text, txtPositionPlanName.Text, txtManagerSuppliesName.Text, txtPositionSuppliesName.Text, txtManagerMoneyName.Text, txtPositionMoneyName.Text, txtUnderBudgetName.Text, txtPositionManagerBudgetName.Text, txtUnderManagerName.Text, txtPositionUnderManagerName.Text, CurrentUser.ID, DateTime.Now, txtDeputyDirectorName.Text, txtPositionDeputyDirector.Text, txtGroupLeaderPlanName.Text, txtPositionGroupLeaderPlan.Text);

            //DeputyDirector
            if (Session["DeputyDirector"] != null)
            {
                Conn.Delete("DeputyDirector", "Where SchoolID = '" + Request["id"] + "'");
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["DeputyDirector"];
                dvDeputyDirector = dt1.DefaultView;
                for (int j = 0; j < dvDeputyDirector.Count; j++)
                {
                    Conn.AddNew("DeputyDirector", "DeputyDirectorID, DeputyDirectorName, DeputyDirectorPosition, Sort, CreateDate, CreateUser, UpdateDate, UpdateUser, SchoolID",
                        Guid.NewGuid().ToString(), dvDeputyDirector[j]["DeputyDirectorName"].ToString(), dvDeputyDirector[j]["DeputyDirectorPosition"].ToString(), dvDeputyDirector[j]["id"], DateTime.Now, CurrentUser.ID, DateTime.Now, CurrentUser.ID, CurrentUser.SchoolID);
                }
            }

            Response.Redirect("MR_School.aspx?ckmode=2&Cr=" + i);  
        }
    }
    private void Delete(string id)
    {
        Int32 i = 0;
        if (String.IsNullOrEmpty(id)) return;
        DataView dv = Conn.Select(string.Format("Select SchoolID From Employee Where SchoolID = '" + id + "' And DelFlag = 0 "));
        if (dv.Count > 0)
        {
            btc.Msg_Head(Img1, MsgHead, true, "3", 0);
        }
        else
        {
            i = Conn.Update("MR_School", "Where SchoolID = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
        }
        Response.Redirect("MR_School.aspx?ckmode=3&Cr=" + i);  
    }
    protected void ddlYearS_SelectedIndexChanged(object sender, EventArgs e)
    {
        getMission(ddlYearS.SelectedValue);
        getGoals(ddlYearS.SelectedValue);
        getStrategies(ddlYearS.SelectedValue);
    }
    protected void btnUpload_OnClick(object sender, EventArgs e)
    {
        if (fiUpload.HasFile)
        {

            int intWidth = 0;
            int intHeight = 0;
            string UlFileName = null;
            string NewFileName = null;
            string NewID = Guid.NewGuid().ToString();

            intWidth = 71;
            //*** Fix Width ***//
            //intHeight = 0   '*** If = 0 Auto Re-Cal Size ***//
            intHeight = 71;

            string[] filetype = fiUpload.FileName.Split('.');

            UlFileName = "../Image/Logo/" + NewID + "." + filetype[1].ToString();  //fiUpload.FileName;

            //*** Save Images ***//
            fiUpload.SaveAs(Server.MapPath(UlFileName));

            NewFileName = "../Image/Logo/Rz_" + NewID + "." + filetype[1].ToString();  //fiUpload.FileName;

            Connection Conn = new Connection();
            Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "' And DelFlag = 0 ", "LogoPath", NewFileName);

            System.Drawing.Image objGraphic = System.Drawing.Image.FromFile(Server.MapPath(UlFileName));

            Bitmap objBitmap = default(Bitmap);
            //*** Calculate Height ***//
            if (intHeight > 0)
            {
                objBitmap = new Bitmap(objGraphic, intWidth, intHeight);
            }
            else
            {
                if (objGraphic.Width > intWidth)
                {
                    double ratio = objGraphic.Height / objGraphic.Width;
                    intHeight = (int)ratio * (int)intWidth;
                    objBitmap = new Bitmap(objGraphic, intWidth, intHeight);
                }
                else
                {
                    objBitmap = new Bitmap(objGraphic);
                }
            }

            //*** Save As  ***//
            objBitmap.Save(Server.MapPath(NewFileName.ToString()), objGraphic.RawFormat);

            //*** Close ***//
            objGraphic.Dispose();

            //*** View Images ***//
            imgPicture.Visible = true;
            imgPicture.ImageUrl = NewFileName;
            MsgHead.Text = "���º���� ! <BR> <small>Upload �ٻ�������º��������</small>";
            Img1.ImageUrl = "~/Image/msg_check.gif";
            MsgHead.CssClass = "headMsg";
            MsgHead.Visible = true;
            Img1.Visible = true;
            ImgBt.Visible = true;
        }
    }
    protected void DeleteImg(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where DelFlag = 0 And SchoolID = '" + CurrentUser.SchoolID + "'", "LogoPath", "");
        imgPicture.ImageUrl = "../Image/Menu9.png";
        ImgBt.Visible = false;
    }
    protected void LinkReport()
    {
        string link = " <a href=\"javascript:;\" " + btc.getLinkReportWEP("W") + "  onclick=\"printRpt(26,'w','" + CurrentUser.SchoolID + "');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"50px;\" height=\"50px;\" title=\"���¡����§ҹ Ẻ�͡��� Word\" src=\"../Image/icon/WordIcon.png\" /></a> "
                    + " <a href=\"javascript:;\" " + btc.getLinkReportWEP("E") + " onclick=\"printRpt(26,'e','" + CurrentUser.SchoolID + "');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"45px;\" height=\"45px;\" title=\"���¡����§ҹ Ẻ�͡��� Excel\" src=\"../Image/icon/ExcelIcon.png\" /></a> "
                    + " <a href=\"javascript:;\" " + btc.getLinkReportWEP("P") + " onclick=\"printRpt(26,'p','" + CurrentUser.SchoolID + "');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"45px;\" height=\"45px;\" title=\"���¡����§ҹ Ẻ�͡��� PDF\" src=\"../Image/icon/PdfIcon.png\" /></a> ";
        linkReport.Text = link;
    }
    protected void txtKeyWordDeputyDirector_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordDeputyDirector", txtKeyWordDeputyDirector.Text);
    }
    protected void txtKeyWordGroupLeaderPlan_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordGroupLeaderPlan", txtKeyWordGroupLeaderPlan.Text);
    }
    protected void btDelDeputyDirector_Click(object sender, EventArgs e)
    {
        if (GridViewDeputyDirector.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["DeputyDirector"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewDeputyDirector.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvDeputyDirector = dt1.DefaultView;
        Session["DeputyDirector"] = dt1;
        GridViewDeputyDirector.DataSource = dvDeputyDirector;
        GridViewDeputyDirector.DataBind();
        if (dvDeputyDirector.Count == 0)
        {
            btDelDeputyDirector.Visible = false;
        }
    }
    protected void btaddDeputyDirector_Click(object sender, EventArgs e)
    {
        if (Session["DeputyDirector"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("DeputyDirectorName");
            dt1.Columns.Add("DeputyDirectorPosition");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["DeputyDirectorName"] = txtDeputyDirectorName.Text;
            dr["DeputyDirectorPosition"] = txtPositionDeputyDirector.Text;
            dt1.Rows.Add(dr);

            dvDeputyDirector = dt1.DefaultView;
            Session["DeputyDirector"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["DeputyDirector"];

            if (txtid.Text == "")
            {
                DataView ckdv = ((DataTable)Session["DeputyDirector"]).DefaultView;
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["DeputyDirectorName"] = txtDeputyDirectorName.Text;
                dr["DeputyDirectorPosition"] = txtPositionDeputyDirector.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                Int32 i = Convert.ToInt32(txtid.Text);
                dt1.Rows[i]["DeputyDirectorName"] = txtDeputyDirectorName.Text;
                dt1.Rows[i]["DeputyDirectorPosition"] = txtPositionDeputyDirector.Text;
            }

            dvDeputyDirector = dt1.DefaultView;
            Session["DeputyDirector"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearDeputyDirector();
        GridViewDeputyDirector.DataSource = dvDeputyDirector;
        GridViewDeputyDirector.CheckListDataField = "id";
        GridViewDeputyDirector.DataBind();
        if (dvDeputyDirector.Count > 0)
        {
            btDelDeputyDirector.Visible = true;
            GridViewDeputyDirector.Visible = true;
        }
    }
    private void ClearDeputyDirector()
    {
        //Session.Remove("DeputyDirector");
        txtid.Text = "";
        txtDeputyDirectorName.Text = "";
        txtPositionDeputyDirector.Text = "�ͧ����ӹ�¡���ç���¹";
    }
}
