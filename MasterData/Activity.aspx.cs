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
using Star.Web.UI.Controls;

using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Protocols;
using System.IO;
using System.Xml;
using System.Text;
using System.Globalization;

using System.Data.OleDb;
using Star.Security.Cryptography;

public partial class Activity : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    private OleDbConnection exConn;
    private DataSet ds;

    DataView dvBudget;
    DataView dvOperation2;
    DataView dvCostsCenter;
    DataView dvAssessment = null;
    DataView dvBudgetType = null;

    decimal TotalMoney = 0;
    decimal TotalBudgetTerm1 = 0;
    decimal TotalBudgetTerm2 = 0;
    decimal TotalMoneyOperation = 0;
    decimal TotalBudgetOther = 0;
    decimal TotalCostsWeight = 0;
    decimal TotalAmount = 0;

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

            Cookie.SetValue2("ckActivityStatus", btc.ckIdentityName("ckActivityStatus")); //เช็คโหมดติดตามงาน

            getddlYear(0);
            getddlSearchTerm(Convert.ToInt32(btc.ckIdentityName("ckBudgetYear")), ddlSearchTerm);
            getddlStrategies(0, ddlSearchYear.SelectedValue);
            getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);

            if (btc.ckIdentityName("ckMainActivity")) // เช็คว่าเปิดโหมดกิจกรรมหลัก
            {
                divSearchMainActivity.Visible = true;
                btc.getddlMainActivity("0",ddlSearchMainActivity, ddlSearchYear.SelectedValue);
                if (Cookie.GetValue2("MainActivity") == null)
                {
                    ddlSearchMainActivity.SelectedIndex = 0;
                }
                else
                {
                    try
                    {
                        ddlSearchMainActivity.SelectedValue = Cookie.GetValue2("MainActivity").ToString();
                    }
                    catch (Exception ex)
                    {
                        ddlSearchMainActivity.SelectedIndex = 0;
                    }
                }
            }

           ddlSearchType.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
           ddlSearchType.Items.Insert(1, new ListItem("กิจกรรมใหม่", "0"));
           ddlSearchType.Items.Insert(2, new ListItem("งานประจำ", "1"));
           ddlSearchType.Items.Insert(3, new ListItem("กิจกรรมต่อเนื่อง", "2"));
           ddlSearchType.SelectedIndex = 0;
           if (Cookie.GetValue2("AcType") == null)
           {
               ddlSearchType.SelectedIndex = 0;
           }
           else
           {
               try
               {
                   ddlSearchType.SelectedValue = Cookie.GetValue2("AcType").ToString();
               }
               catch (Exception ex)
               {
                   ddlSearchType.SelectedIndex = 0;
               }
           }

            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("AcDept").ToString(), Cookie.GetValue2("AcEmp").ToString());
            getddlSubDept(0, ddlSearchDept.SelectedValue);
            lblResponsible2.Text = btc.getEmpName(CurrentUser.ID);
            
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                if (btc.ckIdentityName("ckMainActivity")) // เช็คว่าเปิดโหมดกิจกรรมหลัก
                {
                    divMainActivity.Visible = true;
                }

                if (btc.ckIdentityName("ckRisk")) // เช็คว่าเปิดโหมดความเสี่ยง
                {
                    divRisk.Visible = true;
                }

                if (btc.ckIdentityName("ckAcAssessment")) // เช็คว่าเปิดโหมดประเมินผลกิจกรรมแบบเก่าหรือใหม่
                {
                    divAcAssessmentOld.Visible = false;
                    divAcAssessmentNew.Visible = true;
                }
                else {
                    divAcAssessmentOld.Visible = true;
                    divAcAssessmentNew.Visible = false;
                }

                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        Session.Remove("Operation2ac");
                        Session.Remove("Assessment");
                        hdfActivityCode.Value = Guid.NewGuid().ToString();
                        ClearAll();
                        getddlYear(1);
                        SetItem();
                        ckBudgetTerm();
                        string gotoid = Request.QueryString["id"];
                        if (!string.IsNullOrEmpty(gotoid))
                        {
                            DataView dvCk = Conn.Select("Select StudyYear, StrategiesCode, ProjectsCode From Projects Where ProjectsCode = '" + gotoid + "'");
                            ddlYearB.SelectedValue = dvCk[0]["StudyYear"].ToString();
                            getddlStrategies(1, ddlYearB.SelectedValue);
                            ddlStrategies.SelectedValue = dvCk[0]["StrategiesCode"].ToString();
                            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
                            ddlProjects.SelectedValue = gotoid;
                            getProjectsDetail(gotoid);
                        }
                        else
                        {
                            getddlStrategies(1, ddlYearB.SelectedValue);
                            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
                        }
                        getcblIndicators();
                        getcblGoals();
                        btc.btEnable(btEditDate, false);
                        btc.btEnable(btEditBudget, false);
                        txtYearB.Text = ddlYearB.SelectedItem.Text;
                        ddlBudgetYear.SelectedValue = ddlYearB.SelectedValue;
                        btc.getddlMainActivity("1", ddlMainActivity, ddlYearB.SelectedValue);
                        btc.GenSort(txtSort, "Activity", " And ProjectsCode = '" + ddlProjects.SelectedValue + "'");
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        Session.Remove("Operation2ac");
                        Session.Remove("Assessment");
                        ClearAll();
                        getddlYear(1);
                        //SetItem();
                        ckBudgetTerm();
                        btc.btEnable(btSaveAgain, false);
                        btc.btEnable(btSaveAndGo, false);

                        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
                        {
                            if(!btc.CkActivityApprove(Request["id"]))
                            {
                                btc.btEnable(btApproveBudget, true);
                            }
                            else
                            {
                                DataView dvApproveUser = Conn.Select("Select ApproveUser From Activity Where ActivityCode = '" + Request["id"] + "'");
                                if (!string.IsNullOrEmpty(dvApproveUser[0]["ApproveUser"].ToString()))
                                {
                                    string ApproveName = "";
                                    ApproveName = btc.getEmpName(dvApproveUser[0]["ApproveUser"].ToString());
                                    lblAlertApprove.Text = "*** กิจกรรมนี้ได้ถูกอนุมัติแล้ว โดยคุณ " + ApproveName + " ถ้าต้องการแก้ไขวันที่ดำเนินการหรือแก้ไขงบประมาณ  กรุณาติดต่อฝ่ายแผนงาน";
                                }
                                lblAlertApprove.Visible = true;
                                ImgBtUnApprove.Visible = true;
                            }
                            if (btc.CkActivityBudgetDetail(Request["id"]))
                            {
                                btEditBudget.Enabled = false;
                                btEditBudget.ToolTip = "ไม่สามารถแก้ไขงบประมาณได้เนื่องจากกมีการเบิกงบย่อยแล้ว";
                            }
                            else
                            {
                                btEditBudget.Enabled = true;
                            }
                        }
                        else
                        {
                            if (btc.CkActivityApprove(Request["id"]))
                            {
                                btc.btEnable(btEditDate, false);
                                btc.btEnable(btEditBudget, false);
                                DataView dvApproveUser = Conn.Select("Select ApproveUser From Activity Where ActivityCode = '" + Request["id"] + "'");
                                if (!string.IsNullOrEmpty(dvApproveUser[0]["ApproveUser"].ToString()))
                                {
                                    string ApproveName = "";
                                    ApproveName = btc.getEmpName(dvApproveUser[0]["ApproveUser"].ToString());
                                    lblAlertApprove.Text = "*** กิจกรรมนี้ได้ถูกอนุมัติแล้ว โดยคุณ " + ApproveName + " ถ้าต้องการแก้ไขวันที่ดำเนินการหรือแก้ไขงบประมาณ  กรุณาติดต่อฝ่ายแผนงาน";
                                }
                                lblAlertApprove.Visible = true;
                            }
                        }
                        hdfActivityCode.Value = Request.QueryString["id"];
                        GetData(Request["id"]);
                        break;
                    case "3":
                        MultiView1.ActiveViewIndex = 0;
                        Delete(Request["id"]);
                        break;
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "function loadDept(){" + Page.ClientScript.GetPostBackEventReference(btSearchDept, null, false) + ";} function loadIndicators(){" + Page.ClientScript.GetPostBackEventReference(btSearchInd, null, false) + ";}", true);
            }
            else
            {
                if (btc.ckIdentityName("ckMgSignature"))
                {
                    cbNotManager.Checked = true;
                }
                else
                {
                    cbNotManager.Checked = false;
                }
                DataBind();
            }
        }
        ddlStrategies.Attributes.Add("onchange", "Cktxt(0);");
        ddlProjects.Attributes.Add("onchange", "Cktxt(0);");
        txtActivity.Attributes.Add("onkeyup", "Cktxt(0);");
        txtActivityDetail.Attributes.Add("onkeyup", "Cktxt(0);");
        txtPurpose.Attributes.Add("onkeyup", "Cktxt(0);");
        txtTarget.Attributes.Add("onkeyup", "Cktxt(0);");
        txtTarget2.Attributes.Add("onkeyup", "Cktxt(0);");
        txtOperation2.Attributes.Add("onkeyup", "Cktxt(0);");
        txtExpected.Attributes.Add("onkeyup", "Cktxt(0);");
        txtPlace.Attributes.Add("onkeyup", "Cktxt(0);");
        txtDepartment.Attributes.Add("onkeyup", "Cktxt(0);");
        txtSort.Attributes.Add("onkeyup", "Cktxt(0);");
        txtEmp.Attributes.Add("onkeyup", "Cktxt(0);");
        txtAlertDay.Attributes.Add("onkeyup", "AlertDayNull(); CkAlertDay(0);");
        ddlSDay.Attributes.Add("onchange", "ckddlDate(1);");
        ddlSMonth.Attributes.Add("onchange", "ckddlDate(1);"); 
        ddlSYear.Attributes.Add("onchange", "ckddlDate(1);");
        ddlEDay.Attributes.Add("onchange", "ckddlDate(2);");
        ddlEMonth.Attributes.Add("onchange", "ckddlDate(2);");
        ddlEYear.Attributes.Add("onchange", "ckddlDate(2);");
        ddlRealSDay.Attributes.Add("onchange", "ckddlRealDate(1);");
        ddlRealSMonth.Attributes.Add("onchange", "ckddlRealDate(1);");
        ddlRealSYear.Attributes.Add("onchange", "ckddlRealDate(1);");
        ddlRealEDay.Attributes.Add("onchange", "ckddlRealDate(2);");
        ddlRealEMonth.Attributes.Add("onchange", "ckddlRealDate(2);");
        ddlRealEYear.Attributes.Add("onchange", "ckddlRealDate(2);");
        ddlBudgetType.Attributes.Add("onchange", "cktxtBudgetType();");
        txtListName.Attributes.Add("onkeyup", "ckAddBudget();");
        txtTotalBudgetTerm1.Attributes.Add("onkeyup", "ckBudgetTerm();");
        txtTotalBudgetTerm2.Attributes.Add("onkeyup", "ckBudgetTerm();");
    }
    private void SetItem()
    {
        rbtlYearType.SelectedValue = Convert.ToInt32(btc.ckIdentityName("ckBudgetYear")).ToString();
        ckYearType();
        getlblTarget();
        getddlEntryCosts();
        getddlBudgetType();
        btc.getrbtlType(rbtlType);
        btc.getddlTerm(Convert.ToInt32(rbtlYearType.SelectedValue), ddlTerm);
        btc.getddlActivityStatus(ddlActivityStatus);

        btc.getddlDay(ddlSDay);
        btc.getddlMonth2(ddlSMonth);
        btc.getddlYear(ddlSYear, 5);
        btc.getddlDay(ddlEDay);
        btc.getddlMonth2(ddlEMonth);
        btc.getddlYear(ddlEYear, 5);
        btc.getddlDay(ddlRealSDay);
        btc.getddlMonth2(ddlRealSMonth);
        btc.getddlYear(ddlRealSYear, 5);
        btc.getddlDay(ddlRealEDay);
        btc.getddlMonth2(ddlRealEMonth);
        btc.getddlYear(ddlRealEYear, 5);

        btc.IdentityNameEnable(lblIdentityName, txtIdentityName, "IdentityName", "iNameShow", divIdentityName);
        btc.IdentityNameEnable(lblIdentityName2, txtIdentityName2, "IdentityName2", "iNameShow2", divIdentityName2);

        if (btc.ckIdentityName("ckResource"))
        {
            DivResource.Visible = true;
        }

        if (btc.ckIdentityName("ckSuan"))
        {
            divSuan.Visible = true;
            getrbtlSuanIdentityName();
            getcblSuanIdentitySideName();
            getcblSuanIdentityName2();
            getcblSuanGood();
            divSuanProduct.Visible = true;
            divSuanTime.Visible = true;
        }
        else
        {
            divSuan.Visible = false;
            divSuanProduct.Visible = false;
            divSuanTime.Visible = false;
        }

        ckCostsCenter();
    }
    private void getrbtlSuanIdentityName()
    {
        rbtlSuanIdentityName.Items.Insert(0, new ListItem(" เป็นเลิศวิชาการ", "0"));
        rbtlSuanIdentityName.Items.Insert(1, new ListItem(" สร้างสรรค์กิจกรรม", "1"));
        rbtlSuanIdentityName.SelectedIndex = 0;
    }
    private void getcblSuanIdentitySideName()
    {
        cblSuanIdentitySideName.Items.Insert(0, new ListItem(" วิชาการ", "0"));
        cblSuanIdentitySideName.Items.Insert(1, new ListItem(" ศิลปะ", "1"));
        cblSuanIdentitySideName.Items.Insert(2, new ListItem(" ดนตรี", "2"));
        cblSuanIdentitySideName.Items.Insert(3, new ListItem(" กีฬา", "3"));
    }
    private void getcblSuanIdentityName2()
    {
        cblSuanIdentityName2.Items.Insert(0, new ListItem(" เป็นผู้นำ", "0"));
        cblSuanIdentityName2.Items.Insert(1, new ListItem(" รักเพื่อน", "1"));
        cblSuanIdentityName2.Items.Insert(2, new ListItem(" นับถือพี่", "2"));
        cblSuanIdentityName2.Items.Insert(3, new ListItem(" เคารพครู", "3"));
        cblSuanIdentityName2.Items.Insert(4, new ListItem(" กตัญญูพ่อแม่", "4"));
        cblSuanIdentityName2.Items.Insert(5, new ListItem(" ดูแลน้อง", "5"));
        //cblSuanIdentityName2.SelectedIndex = 0;
    }
    private void getcblSuanGood()
    {
        cblSuanGood.Items.Insert(0, new ListItem(" โรงเรียน", "0"));
        cblSuanGood.Items.Insert(1, new ListItem(" ชุมชน", "1"));
        cblSuanGood.Items.Insert(2, new ListItem(" องค์กรสนับสนุน", "2"));
        cblSuanGood.Items.Insert(3, new ListItem(" ประเทศ", "3"));
        cblSuanGood.Items.Insert(4, new ListItem(" โลก", "4"));
        //cblSuanGood.SelectedIndex = 0;
    }
    private void ckBudgetTerm()
    {
        string StrSql = @" Select ckBudgetTerm From MR_School Where SchoolID = '{0}' And DelFlag = 0 ";
        DataView dvCk = Conn.Select(string.Format(StrSql, CurrentUser.SchoolID));
        if (dvCk.Count != 0)
        {
            Int32 ckBudgetTerm = Convert.ToInt32(dvCk[0]["ckBudgetTerm"]);
            if (ckBudgetTerm == 0)
            {
                hdfBudgetTerm.Value = ckBudgetTerm.ToString();
                GridViewBudget.Columns[8].Visible = false;
                GridViewBudget.Columns[9].Visible = false;
                spnBudgetTerm.Visible = false;
                divBudgetTerm.Visible = false;
            }
            else {
                hdfBudgetTerm.Value = ckBudgetTerm.ToString();
                GridViewBudget.Columns[8].Visible = true;
                GridViewBudget.Columns[9].Visible = true;
                spnBudgetTerm.Visible = true;
                divBudgetTerm.Visible = true;
            }
        }
    }
    public void getddlSearchTerm(int mode, DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Insert(0, new ListItem("-ทั้งหมด-", ""));
        ddl.Items.Insert(1, new ListItem("1", "1"));
        ddl.Items.Insert(2, new ListItem("2", "2"));
        if (mode == 0)
        {
            ddl.Items.Insert(3, new ListItem("1-2", "1-2"));
        }
        if (mode == 1)
        {
            ddl.Items.Insert(3, new ListItem("2-1", "2-1"));
        }

        if (Cookie.GetValue2("AcTerm") == null)
        {
            ddl.SelectedIndex = 0;
        }
        else
        {
            try
            {
                ddl.SelectedValue = Cookie.GetValue2("AcTerm").ToString();
            }
            catch (Exception ex)
            {
                ddl.SelectedIndex = 0;
            }
        }
    }
    private void getlblTarget()
    {
        string[] lblTarget = btc.getlblTarget().Split(',');
        lblTarget1.Text = lblTarget[0];
        lblTarget2.Text = lblTarget[1];

        string[] lblKeyWord = btc.getKeyWordResponsibleActivity().Split(',');
        txtKeyWordResponsibleActivity.Text = lblKeyWord[0];
        txtKeyWordProposeActivity.Text = lblKeyWord[1];
        txtKeyWordApprovalActivity.Text = lblKeyWord[2];
        txtKeyWordApproval2Activity.Text = lblKeyWord[3];
    }
    private void ckCostsCenter()
    {
        if (!btc.ckIdentityName("ckCostsCenter"))
        {
            divCostsCenter.Visible = false;
        }
        else
        {
            divCostsCenter.Visible = true;
            getddlCostsCenter(ddlYearB.SelectedValue);
            getddlCostsSubCenter(ddlCostsCenter.SelectedValue);
        }
    }
    private void getddlEntryCosts()
    {
        ddlEntryCosts.DataSource = new BTC().getddlEntryCosts();
        ddlEntryCosts.DataTextField = "EntryCostsName";
        ddlEntryCosts.DataValueField = "EntryCostsCode";
        ddlEntryCosts.DataBind();
        ddlEntryCosts.Items.Insert(0, new ListItem("-ไม่เลือก-", ""));
    }
    private void getddlBudgetType()
    {
        ddlBudgetType.DataSource = new BTC().getddlBudgetType();
        ddlBudgetType.DataTextField = "BudgetTypeName";
        ddlBudgetType.DataValueField = "BudgetTypeCode";
        ddlBudgetType.DataBind();
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

            btc.getdllStudyYear(ddlBudgetYear);
            btc.getDefault(ddlBudgetYear, "StudyYear", "StudyYear");
        }
    }
    private void getcblIndicators()
    {
        string strSql = @" Select ItemID, ' K' + Cast(IsNull(Sort, 0) As nVarChar) + ' ' + IndicatorsName IndicatorsName From StrategiesIndicators Where StrategiesCode = '{0}' Order By Sort ";
        DataView dv = Conn.Select(string.Format(strSql, ddlStrategies.SelectedValue));
        if (dv.Count > 0)
        {
            cblIndicators.DataSource = dv;
            cblIndicators.DataTextField = "IndicatorsName";
            cblIndicators.DataValueField = "ItemID";
            cblIndicators.DataBind();
            cblIndicators.Enabled = true;
        }
        else
        {
            cblIndicators.Items.Insert(0, new ListItem("- ไม่มีข้อมูล -", ""));
            cblIndicators.Enabled = false;
        }
    }
    private void getcblGoals()
    {
        string strSql = @" Select ItemID, ' G' + Cast(IsNull(Sort, 0) As nVarChar) + ' ' + GoalsName GoalsName From StrategiesGoals Where StrategiesCode = '{0}' Order By Sort ";
        DataView dv = Conn.Select(string.Format(strSql, ddlStrategies.SelectedValue));
        if (dv.Count > 0)
        {
            cblGoals.DataSource = dv;
            cblGoals.DataTextField = "GoalsName";
            cblGoals.DataValueField = "ItemID";
            cblGoals.DataBind();
            cblGoals.Enabled = true;
        }
        else
        {
            cblGoals.Items.Insert(0, new ListItem("- ไม่มีข้อมูล -", ""));
            cblGoals.Enabled = false;
        }
    }
    private void getddlStrategies(int mode, string StudyYear)
    {
        if (mode == 0)
        {
            btc.getddlStrategies(0, ddlSearch2, StudyYear);
            if (Cookie.GetValue2("AcStrategies") == null)
            {
                ddlSearch2.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    ddlSearch2.SelectedValue = Cookie.GetValue2("AcStrategies").ToString();
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
            btc.getddlProjects(0, ddlSearch, StudyYear, StrategiesCode);
            if (Cookie.GetValue2("AcProjects") == null)
            {
                ddlSearch.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(StrategiesCode))
                    {
                        ddlSearch.SelectedValue = Cookie.GetValue2("AcProjects").ToString();
                    }
                    else
                    {
                        ddlSearch.SelectedIndex = 0;
                    }
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
    private void getddlSubDept(int mode, string DeptCode)
    {
        if (mode == 0)
        {
            btc.getddlSubDept(0, ddlSearchSubDept, DeptCode);
            if (Cookie.GetValue2("AcSubDept") == null)
            {
                ddlSearchSubDept.SelectedIndex = 0;
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(Cookie.GetValue2("AcSubDept").ToString()))
                    {
                        ddlSearchSubDept.SelectedValue = Cookie.GetValue2("AcSubDept").ToString();
                    }
                    else
                    {
                        ddlSearchSubDept.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    ddlSearchSubDept.SelectedIndex = 0;
                }
            }
        }
    }
    private void getddlCostsCenter(string StudyYear)
    {
        btc.getddlCostsCenter(ddlCostsCenter, ddlYearB.SelectedValue);
    }
    private void getddlCostsSubCenter(string CostsCode)
    {
        btc.getddlCostsSubCenter(ddlCostsSubCenter, ddlCostsCenter.SelectedValue);
    }
    public override void DataBind()
    {
        string strSql = @" Select a.ActivityCode, a.BudgetTypeCode, 
            BudgetTypeName = Case b.BudgetTypeName When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then b.BudgetTypeName Else a.BudgetTypeOtherName End, 
            Sum(a.TotalMoney) TotalMoney, b.Sort 
            From CostsDetail a, BudgetType b, Activity c 
            Where b.DelFlag = 0 And a.BudgetTypeCode = b.BudgetTypeCode 
            And a.ActivityCode = c.ActivityCode
            Group By a.ActivityCode, a.BudgetTypeCode, b.BudgetTypeName, a.BudgetTypeOtherName, b.Sort 
            Order By b.Sort ";
        dvBudgetType = Conn.Select(string.Format(strSql));

        string StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, IsNull(b.ApproveFlag, 0) ApproveFlag, "
                    + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, 0 As ConnectInd, 0 As ConnectEva, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                    + " From Projects a, Activity b "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            if (ddlSearchSubDept.SelectedIndex == 0)
            {
                StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, IsNull(b.ApproveFlag, 0) ApproveFlag, "
                        + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, 0 As ConnectInd, 0 As ConnectEva, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                        + " From Projects a, Activity b, dtAcDept c "
                        + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                        + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
            }
            else
            {
                StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, IsNull(b.ApproveFlag, 0) ApproveFlag, "
                            + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, 0 As ConnectInd, 0 As ConnectEva, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                            + " From Projects a, Activity b, dtAcDept c, dtAcSubDept d "
                            + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode "
                            + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "' And d.SubDeptCode = '" + ddlSearchSubDept.SelectedValue + "'";
            }
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, IsNull(b.ApproveFlag, 0) ApproveFlag, "
                    + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, 0 As ConnectInd, 0 As ConnectEva, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                    + " From Projects a, Activity b, dtAcEmp c "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                if (ddlSearchSubDept.SelectedIndex == 0)
                {
                    StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, IsNull(b.ApproveFlag, 0) ApproveFlag, "
                            + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, 0 As ConnectInd, 0 As ConnectEva, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                            + " From Projects a, Activity b, dtAcEmp c, dtAcDept d "
                            + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode "
                            + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                            + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
                }
                else
                {
                    StrSql = "Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.SDate , b.EDate, '' As DeptName, b.Status, b.Df, IsNull(b.ApproveFlag, 0) ApproveFlag, "
                                + " b.CostsType, IsNull(b.TotalAmount, 0) TotalAmount, IsNull((Cast(b.Term As nVarChar) + '/' + Cast(b.YearB As nVarChar)), '') Term, 0 As ConnectInd, 0 As ConnectEva, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                                + " From Projects a, Activity b, dtAcEmp c, dtAcDept d, dtAcSubDept e "
                                + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode And b.ActivityCode = e.ActivityCode "
                                + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                                + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' And e.SubDeptCode = '" + ddlSearchSubDept.SelectedValue + "'";
                }
            }            
        }
        if (ddlSearchTerm.SelectedIndex != 0)
        {
            StrSql = StrSql + " And b.Term = '" + ddlSearchTerm.SelectedValue + "'";
        }
        if (ddlSearch2.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.StrategiesCode = '" + ddlSearch2.SelectedValue + "'";
        }
        if (ddlSearch.SelectedIndex != 0)
        {
            StrSql = StrSql + " And a.ProjectsCode = '" + ddlSearch.SelectedValue + "'";
        }
        if (divSearchMainActivity.Visible == true)
        {
            if (ddlSearchMainActivity.SelectedIndex != 0)
            {
                StrSql += " And b.MainActivityID = '" + ddlSearchMainActivity.SelectedValue + "'";
            }
        }
        if (ddlSearchType.SelectedIndex != 0)
        {
            StrSql += " And b.CostsType = '" + ddlSearchType.SelectedValue + "' ";
        }
        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And b.ActivityName Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Order By b.CreateDate Desc "));

        for (int j = 0; j < dv.Count; j++)
        {
            dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["ActivityCode"].ToString());
            dv[j]["ConnectInd"] = btc.getAcIndicators2(dv[j]["ActivityCode"].ToString());
            dv[j]["ConnectEva"] = btc.getAcEvaluation(dv[j]["ActivityCode"].ToString());
        }

        //เช็คผลรวม
        try
        {
            DataTable dt = dv.ToTable();
            TotalAmount = Convert.ToDecimal(dt.Compute("Sum(TotalAmount)", dv.RowFilter));
        }
        catch(Exception ex)
        {
        }

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();

        DataView dv1 = Conn.Select(string.Format(StrSql + " And b.Status In (0,4) Order By b.CreateDate Desc "));

        for (int j = 0; j < dv1.Count; j++)
        {
            dv1[j]["DeptName"] = btc.getAcDeptName(dv1[j]["ActivityCode"].ToString());
        }

        StrSql = @" Select ckReportActivity From MR_School Where SchoolID = '{0}' And DelFlag = 0 ";
        DataView dvCkReport = Conn.Select(string.Format(StrSql, CurrentUser.SchoolID));
        //if (dvCkReport.Count != 0)
        {
            string[] ckReportActivity = dvCkReport[0]["ckReportActivity"].ToString().Split(',');

            GridView1.Columns[10].Visible = false;
            GridView1.Columns[11].Visible = false;
            GridView1.Columns[12].Visible = false;
            for (int i = 0; i < ckReportActivity.Length; i++)
            {
                if (ckReportActivity[i] == "0")
                {
                    GridView1.Columns[11].Visible = true;
                }
                if (ckReportActivity[i] == "1")
                {
                    GridView1.Columns[10].Visible = true;
                }
                if (ckReportActivity[i] == "2")
                {
                    GridView1.Columns[12].Visible = true;
                }
            }
        }

        //----GrandTotal-----------
        StrSql = "Select IsNull(Sum(b.TotalAmount), 0) TotalAmount "
                    + " From Projects a, Activity b "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";
        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = "Select IsNull(Sum(b.TotalAmount), 0) TotalAmount "
                    + " From Projects a, Activity b, dtAcDept c "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = "Select IsNull(Sum(b.TotalAmount), 0) TotalAmount "
                    + " From Projects a, Activity b, dtAcEmp c "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                    + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = "Select IsNull(Sum(b.TotalAmount), 0) TotalAmount "
                        + " From Projects a, Activity b, dtAcEmp c, dtAcDept d "
                        + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode "
                        + " And a.StudyYear = '" + ddlSearchYear.SelectedValue + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
            }
        }

        DataView dvTotal = Conn.Select(StrSql);
        ToltalBudget.InnerHtml = (dvTotal.Count != 0) ? Convert.ToDecimal(dvTotal[0]["TotalAmount"]).ToString("#,##0.00") : "0.00";
        //----EndGrandTotal-----------

        GridView3.DataSource = dv;
        GridView3.DataBind();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv;
        string strSql = " Select a.StudyYear, b.* "
                    + " From Projects a, Activity b "
                    + " Where b.ActivityCode = '{0}' And a.ProjectsCode = b.ProjectsCode ";
        dv = Conn.Select(string.Format(strSql, id));

        if (dv.Count != 0)
        {
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            btc.getddlMainActivity("1", ddlMainActivity, ddlYearB.SelectedValue);
            SetItem();
            getddlStrategies(1, ddlYearB.SelectedValue);
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            getcblIndicators();
            getcblGoals();
            if (ddlStrategies.SelectedIndex != 0)
            {
                lblStrategies.Text = ddlStrategies.SelectedItem.Text.Replace("กลยุทธ์ที่ ", "S");
            }
            getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
            ddlProjects.SelectedValue = dv[0]["ProjectsCode"].ToString();
            if (btc.CkUseData(id, "ActivityCode", "Indicators2", " And DelFlag = 0 "))
            {
                ddlYearB.Enabled = false;
                //ddlStrategies.Enabled = false;
                //ddlProjects.Enabled = false;
            }
            txtIdentityName.Text = dv[0]["IdentityName"].ToString();
            txtIdentityName2.Text = dv[0]["IdentityName2"].ToString();
            txtActivity.Text = dv[0]["ActivityName"].ToString();
            if (btc.ckIdentityName("ckSuan"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "getActivityName();", true);
                rbtlSuanIdentityName.SelectedValue = dv[0]["SuanIdentityName"].ToString();
                if (rbtlSuanIdentityName.SelectedIndex == 0)
                {
                    cblSuanIdentitySideName.Enabled = true;
                }
                else
                {
                    cblSuanIdentitySideName.Enabled = false;
                }
                //cblSuanIdentityName2.SelectedValue = dv[0]["SuanIdentityName2"].ToString();
                //cblSuanGood.SelectedValue = dv[0]["SuanGood"].ToString();
                if (!string.IsNullOrEmpty(dv[0]["SuanIdentitySideName"].ToString()))
                {
                    string[] SuanIdentitySideName = dv[0]["SuanIdentitySideName"].ToString().Split(',');
                    for (int i = 0; i <= cblSuanIdentitySideName.Items.Count - 1; i++)
                    {
                        for (int j = 0; j <= SuanIdentitySideName.Length - 1; j++)
                        {
                            if (cblSuanIdentitySideName.Items[i].Value == SuanIdentitySideName[j])
                            {
                                cblSuanIdentitySideName.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(dv[0]["SuanIdentityName2"].ToString()))
                {
                    string[] SuanIdentityName2 = dv[0]["SuanIdentityName2"].ToString().Split(',');
                    for (int i = 0; i <= cblSuanIdentityName2.Items.Count - 1; i++)
                    {
                        for (int j = 0; j <= SuanIdentityName2.Length - 1; j++)
                        {
                            if (cblSuanIdentityName2.Items[i].Value == SuanIdentityName2[j])
                            {
                                cblSuanIdentityName2.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(dv[0]["SuanGood"].ToString()))
                {
                    string[] SuanGood = dv[0]["SuanGood"].ToString().Split(',');
                    for (int i = 0; i <= cblSuanGood.Items.Count - 1; i++)
                    {
                        for (int j = 0; j <= SuanGood.Length - 1; j++)
                        {
                            if (cblSuanGood.Items[i].Value == SuanGood[j])
                            {
                                cblSuanGood.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
                DataView dv10 = Conn.Select(string.Format("Select ItemID From dtAcStrategiesIndicators Where ActivityCode = '" + id + "' "));
                DataView dv11 = Conn.Select(string.Format("Select ItemID From dtAcStrategiesGoals Where ActivityCode = '" + id + "' "));

                if (dv10.Count != 0)
                {
                    for (int i = 0; i <= cblIndicators.Items.Count - 1; i++)
                    {
                        for (int j = 0; j <= dv10.Count - 1; j++)
                        {
                            if (cblIndicators.Items[i].Value == dv10[j]["ItemID"].ToString())
                            {
                                cblIndicators.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }

                if (dv11.Count != 0)
                {
                    for (int i = 0; i <= cblGoals.Items.Count - 1; i++)
                    {
                        for (int j = 0; j <= dv11.Count - 1; j++)
                        {
                            if (cblGoals.Items[i].Value == dv11[j]["ItemID"].ToString())
                            {
                                cblGoals.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
            txtActivityDetail.Text = dv[0]["ActivityDetail"].ToString();
            txtPurpose.Text = dv[0]["Purpose"].ToString();
            txtTarget.Text = dv[0]["Target"].ToString();
            txtTarget2.Text = dv[0]["Target2"].ToString();
            txtParticipants.Text = dv[0]["Participants"].ToString();
            txtOperation1.Text = dv[0]["Operation1"].ToString();
            txtPeriod1.Text = dv[0]["Period1"].ToString();
            txtPlace1.Text = dv[0]["Place1"].ToString();
            txtEmp1.Text = dv[0]["Emp1"].ToString();
            txtPeriod2.Text = dv[0]["Period2"].ToString();
            txtPlace2.Text = dv[0]["Place2"].ToString();
            txtEmp2.Text = dv[0]["Emp2"].ToString();
            txtOperation3.Text = dv[0]["Operation3"].ToString();
            txtPeriod3.Text = dv[0]["Period3"].ToString();
            txtPlace3.Text = dv[0]["Place3"].ToString();
            txtEmp3.Text = dv[0]["Emp3"].ToString();
            txtSolutions.Text = dv[0]["Solutions"].ToString();
            txtRisk.Text = dv[0]["Risk"].ToString();
            txtProtect.Text = dv[0]["Protect"].ToString();
            txtEvaluation.Text = dv[0]["Evaluation"].ToString();
            txtEvaIndicatorsProduct.Text = dv[0]["AcProduct"].ToString();
            txtEvaIndicators.Text = dv[0]["EvaIndicators"].ToString();
            txtEvaAssessment.Text = dv[0]["EvaAssessment"].ToString();
            txtEvaTool.Text = dv[0]["EvaTool"].ToString();
            txtEvaTime.Text = dv[0]["AcTime"].ToString();
            txtExpected.Text = dv[0]["Expected"].ToString();
            txtVolumeExpect.Text = string.IsNullOrEmpty(dv[0]["VolumeExpect"].ToString()) ? "0" : Convert.ToDecimal(dv[0]["VolumeExpect"]).ToString("#,##0");
            txtResource.Text = dv[0]["Resource"].ToString();
            txtPlace.Text = dv[0]["Place"].ToString();
            rbtlType.SelectedValue = dv[0]["CostsType"].ToString();
            ddlTerm.SelectedValue = dv[0]["Term"].ToString();
            txtYearB.Text = dv[0]["YearB"].ToString();
            ddlBudgetYear.SelectedValue = dv[0]["BudgetYear"].ToString();
            txtProposeName.Text = dv[0]["ProposeName"].ToString();
            txtProposePosition.Text = dv[0]["ProposePosition"].ToString();
            txtApprovalName.Text = dv[0]["ApprovalName"].ToString();
            txtApprovalPosition.Text = dv[0]["ApprovalPosition"].ToString();
            txtApprovalName2.Text = dv[0]["ApprovalName2"].ToString();
            txtApprovalPosition2.Text = dv[0]["ApprovalPosition2"].ToString();
            txtResponsiblePosition.Text = dv[0]["ResponsiblePosition"].ToString();
            txtResponsiblePosition2.Text = dv[0]["ResponsiblePosition"].ToString();

            if (Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")))
            {
                DivActivityStatus.Visible = true;
                ddlActivityStatus.SelectedValue = dv[0]["ActivityStatus"].ToString();
            }

            strSql = " Select ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2  From Projects Where DelFlag = 0 And ProjectsCode = '" + ddlProjects.SelectedValue + "'";
            DataView dv1 = Conn.Select(strSql);
            if (dv.Count != 0)
            {
                txtProposeName.Text = string.IsNullOrEmpty(txtProposeName.Text) ? dv1[0]["ProposeName"].ToString() : dv[0]["ProposeName"].ToString();
                txtProposePosition.Text = string.IsNullOrEmpty(txtProposePosition.Text) ? dv1[0]["ProposePosition"].ToString() : dv[0]["ProposePosition"].ToString();
                txtApprovalName.Text = string.IsNullOrEmpty(txtApprovalName.Text) ? dv1[0]["ApprovalName"].ToString() : dv[0]["ApprovalName"].ToString();
                txtApprovalPosition.Text = string.IsNullOrEmpty(txtApprovalPosition.Text) ? dv1[0]["ApprovalPosition"].ToString() : dv[0]["ApprovalPosition"].ToString();
                txtApprovalName2.Text = string.IsNullOrEmpty(txtApprovalName2.Text) ? dv1[0]["ApprovalName2"].ToString() : dv[0]["ApprovalName2"].ToString();
                txtApprovalPosition2.Text = string.IsNullOrEmpty(txtApprovalPosition2.Text) ? dv1[0]["ApprovalPosition2"].ToString() : dv[0]["ApprovalPosition2"].ToString(); 
            }

            strSql = " Select ActivityCode, RecNum As id, Operation2, IsNull(Budget2, 0) Budget2, IsNull(BudgetOther, 0) BudgetOther "
                + " From ActivityOperation2 "
                + " Where ActivityCode = '{0}' ";
            dvOperation2 = Conn.Select(string.Format(strSql + " Order By RecNum ", id));

            if (dvOperation2.Count != 0)
            {
                btDelOperation2.Visible = true;
                if (Session["Operation2ac"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("id");
                    dt1.Columns.Add("Operation2");
                    dt1.Columns.Add("Budget2");
                    dt1.Columns.Add("BudgetOther");

                    DataRow dr;
                    for (int i = 0; i < dvOperation2.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["id"] = dvOperation2[i]["id"].ToString();
                        dr["Operation2"] = dvOperation2[i]["Operation2"].ToString();
                        dr["Budget2"] = dvOperation2[i]["Budget2"].ToString();
                        dr["BudgetOther"] = dvOperation2[i]["BudgetOther"].ToString();
                        dt1.Rows.Add(dr);
                    }
                    dvOperation2 = dt1.DefaultView;
                    Session["Operation2ac"] = dt1;

                    GridViewOperation2.DataSource = dvOperation2;
                    GridViewOperation2.CheckListDataField = "id";
                    GridViewOperation2.DataBind();
                }
            }

            string EmpName = "";
            string EmpCode = "";
            strSql = "Select a.*, b.EmpName From dtAcEmp a, Employee b Where b.DelFlag = 0 And a.EmpCode = b.EmpID And a.ActivityCode = '{0}'";
            DataView dvEmp = Conn.Select(string.Format(strSql, id));
            if (dvEmp.Count != 0)
            {
                for (int i = 0; i < dvEmp.Count; i++)
                {
                    EmpName += dvEmp[i]["EmpName"].ToString();
                    EmpCode += dvEmp[i]["EmpCode"].ToString();
                    if (i != dvEmp.Count - 1)
                    {
                        EmpName += ",";
                        EmpCode += ",";
                    }
                }
                txtEmp.Text = EmpName;
                userid.Value = EmpCode;
            }

            string DeptName = "";
            string DeptCode = "";
            strSql = "Select a.*, b.DeptName From dtAcDept a, Department b Where b.DelFlag = 0 And a.DeptCode = b.DeptCode And a.ActivityCode = '{0}'";
            DataView dvDept = Conn.Select(string.Format(strSql, id));
            if (dvDept.Count != 0)
            {
                for (int i = 0; i < dvDept.Count; i++)
                {
                    DeptName += dvDept[i]["DeptName"].ToString();
                    DeptCode += dvDept[i]["DeptCode"].ToString();
                    if (i != dvDept.Count - 1)
                    {
                        DeptName += ",";
                        DeptCode += ",";
                    }
                }
                txtDepartment.Text = DeptName;
                JId.Value = DeptCode;
            }

            string SubDeptName = "";
            string SubDeptCode = "";
            strSql = "Select a.*, b.SubDeptName From dtAcSubDept a, SubDepartment b Where b.DelFlag = 0 And a.SubDeptCode = b.SubDeptCode And a.ActivityCode = '{0}'";
            DataView dvSubDept = Conn.Select(string.Format(strSql, id));
            if (dvSubDept.Count != 0)
            {
                for (int i = 0; i < dvSubDept.Count; i++)
                {
                    SubDeptName += dvSubDept[i]["SubDeptName"].ToString();
                    SubDeptCode += dvSubDept[i]["SubDeptCode"].ToString();
                    if (i != dvSubDept.Count - 1)
                    {
                        SubDeptName += ",";
                        SubDeptCode += ",";
                    }
                }
                txtSubDept.Text = SubDeptName;
                SubDeptid.Value = SubDeptCode;
            }

            txtSDay.Text = Convert.ToDateTime(dv[0]["SDate"]).ToShortDateString();
            ddlSDay.SelectedValue = Convert.ToDateTime(dv[0]["SDate"]).Day.ToString("00");
            ddlSMonth.SelectedValue = Convert.ToDateTime(dv[0]["SDate"]).Month.ToString("00");
            ddlSYear.SelectedValue = (Convert.ToDateTime(dv[0]["SDate"]).Year + 543).ToString();
            ddlSDay.Enabled = false;
            ddlSMonth.Enabled = false;
            ddlSYear.Enabled = false;
            txtEDay.Text = Convert.ToDateTime(dv[0]["EDate"]).ToShortDateString();
            ddlEDay.SelectedValue = Convert.ToDateTime(dv[0]["EDate"]).Day.ToString("00");
            ddlEMonth.SelectedValue = Convert.ToDateTime(dv[0]["EDate"]).Month.ToString("00");
            ddlEYear.SelectedValue = (Convert.ToDateTime(dv[0]["EDate"]).Year + 543).ToString();
            ddlEDay.Enabled = false;
            ddlEMonth.Enabled = false;
            ddlEYear.Enabled = false;
            txtRealSDate.Text = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(dv[0]["RealSDate"]).ToShortDateString());
            ddlRealSDay.SelectedValue = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? DateTime.Now.Day.ToString("00") : Convert.ToDateTime(dv[0]["RealSDate"]).Day.ToString("00"));
            ddlRealSMonth.SelectedValue = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? DateTime.Now.Month.ToString("00") : Convert.ToDateTime(dv[0]["RealSDate"]).Month.ToString("00"));
            ddlRealSYear.SelectedValue = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? (DateTime.Now.Year + 543).ToString() : (Convert.ToDateTime(dv[0]["RealSDate"]).Year + 543).ToString());
            txtRealEDate.Text = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(dv[0]["RealEDate"]).ToShortDateString());
            ddlRealEDay.SelectedValue = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? DateTime.Now.Day.ToString("00") : Convert.ToDateTime(dv[0]["RealEDate"]).Day.ToString("00"));
            ddlRealEMonth.SelectedValue = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? DateTime.Now.Month.ToString("00") : Convert.ToDateTime(dv[0]["RealEDate"]).Month.ToString("00"));
            ddlRealEYear.SelectedValue = (string.IsNullOrEmpty(dv[0]["RealSDate"].ToString()) ? (DateTime.Now.Year + 543).ToString() : (Convert.ToDateTime(dv[0]["RealEDate"]).Year + 543).ToString());
            txtAlertDay.Text = dv[0]["AlertDay"].ToString();
            txtSort.Text = dv[0]["Sort"].ToString();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "Activity", "ActivityCode", id);
            if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
            {
                lbtEditCreate.Visible = true;
            }
            if (btc.ckIdentityName("ckMainActivity")) // เช็คว่าเปิดโหมดกิจกรรมหลัก
            {
                ddlMainActivity.SelectedValue = dv[0]["MainActivityID"].ToString();
            }
        }

        if (btc.ckIdentityName("ckCostsCenter")) // เช็คว่าเปิดโหมดต้นทุนศูนย์
        {
            strSql = " Select *, '' CostsName, '' CostsSubName "
                + " From dtAcCostsCenter "
                + " Where ActivityCode = '{0}' ";
            dvCostsCenter = Conn.Select(string.Format(strSql, id));

            if (dvCostsCenter.Count != 0)
            {
                for (int i = 0; i < dvCostsCenter.Count; i++)
                {
                    dvCostsCenter[i]["CostsName"] = new BTC().getCostsCenterName(dvCostsCenter[i]["CostsCode"].ToString());
                    dvCostsCenter[i]["CostsSubName"] = new BTC().getCostsSubCenterName(dvCostsCenter[i]["CostsSubCode"].ToString());
                }
                if (Session["CostsCenter"] == null)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("id");
                    dt1.Columns.Add("CostsCode");
                    dt1.Columns.Add("CostsName");
                    dt1.Columns.Add("CostsSubCode");
                    dt1.Columns.Add("CostsSubName");
                    dt1.Columns.Add("CostsWeight");

                    DataRow dr;
                    for (int i = 0; i < dvCostsCenter.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr["id"] = dr.Table.Rows.Count;
                        dr["CostsCode"] = dvCostsCenter[i]["CostsCode"].ToString();
                        dr["CostsName"] = dvCostsCenter[i]["CostsName"].ToString();
                        dr["CostsSubCode"] = dvCostsCenter[i]["CostsSubCode"].ToString();
                        dr["CostsSubName"] = dvCostsCenter[i]["CostsSubName"].ToString();
                        dr["CostsWeight"] = dvCostsCenter[i]["CostsWeight"].ToString();
                        dt1.Rows.Add(dr);
                    }
                    dvCostsCenter = dt1.DefaultView;
                    Session["CostsCenter"] = dt1;

                    dgCostsCenter.DataSource = dvCostsCenter;
                    dgCostsCenter.CheckListDataField = "id";
                    dgCostsCenter.DataBind();

                    if (dvCostsCenter.Count != 0)
                    {
                        btDelCostsCenter.Visible = true;
                    }
                }
            }
        }

        string StrSql = " Select ActivityCode, RecNum As id, IndicatorsName, MethodAss, ToolsAss "
                + " From ActivityAssessment "
                + " Where ActivityCode = '{0}' ";
        dvAssessment = Conn.Select(string.Format(StrSql + " Order By RecNum ", id));

        if (dvAssessment.Count != 0)
        {
            btDelAssessment.Visible = true;
            if (Session["Assessment"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("id");
                dt1.Columns.Add("IndicatorsName");
                dt1.Columns.Add("MethodAss");
                dt1.Columns.Add("ToolsAss");

                DataRow dr;
                for (int i = 0; i < dvAssessment.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["id"] = dvAssessment[i]["id"].ToString();
                    dr["IndicatorsName"] = dvAssessment[i]["IndicatorsName"].ToString();
                    dr["MethodAss"] = dvAssessment[i]["MethodAss"].ToString();
                    dr["ToolsAss"] = dvAssessment[i]["ToolsAss"].ToString();
                    dt1.Rows.Add(dr);
                }
                dvAssessment = dt1.DefaultView;
                Session["Assessment"] = dt1;

                GridViewAssessment.DataSource = dvAssessment;
                GridViewAssessment.CheckListDataField = "id";
                GridViewAssessment.DataBind();
            }
        }

        strSql = " Select a.*, a.ListNo As id, a.ListName As EntryCostsName, a.EntryCostsCode As EntryCostsCode, '' As txtCostsName, BudgetTypeName = Case a.BudgetTypeCode When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then a.BudgetTypeOtherName Else b.BudgetTypeName End "
                + " From CostsDetail a, BudgetType b "
                + " Where a.BudgetTypeCode = b.BudgetTypeCode And a.ActivityCode = '{0}' ";
        dvBudget = Conn.Select(string.Format(strSql + " Order By a.ListNo, b.Sort ", id));

        if (dvBudget.Count != 0)
        {
            for (int i = 0; i < dvBudget.Count; i++)
            {
                dvBudget[i]["txtCostsName"] = new BTC().getEntryCostsName(dvBudget[i]["EntryCostsCode"].ToString());
            }
            if (Session["budget"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("id", typeof(int));
                dt1.Columns.Add("ItemID");
                dt1.Columns.Add("EntryCostsName");
                dt1.Columns.Add("txtCostsName");
                dt1.Columns.Add("EntryCostsCode");
                dt1.Columns.Add("BudgetTypeName");
                dt1.Columns.Add("BudgetTypeCode");
                dt1.Columns.Add("TotalP");
                dt1.Columns.Add("TotalD");
                dt1.Columns.Add("TotalG");
                dt1.Columns.Add("TotalMoney", typeof(decimal));
                dt1.Columns.Add("TotalBudgetTerm1", typeof(decimal));
                dt1.Columns.Add("TotalBudgetTerm2", typeof(decimal));

                DataRow dr;
                for (int i = 0; i < dvBudget.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["id"] = (!string.IsNullOrEmpty(dvBudget[i]["id"].ToString()) ? Convert.ToInt16(dvBudget[i]["id"]) : dr.Table.Rows.Count);
                    dr["ItemID"] = dvBudget[i]["ItemID"].ToString();
                    dr["EntryCostsName"] = dvBudget[i]["EntryCostsName"].ToString();
                    dr["txtCostsName"] = dvBudget[i]["txtCostsName"].ToString();
                    dr["EntryCostsCode"] = dvBudget[i]["EntryCostsCode"].ToString();
                    dr["BudgetTypeName"] = dvBudget[i]["BudgetTypeName"].ToString();
                    dr["BudgetTypeCode"] = dvBudget[i]["BudgetTypeCode"].ToString();
                    dr["TotalP"] = dvBudget[i]["TotalP"].ToString();
                    dr["TotalD"] = dvBudget[i]["TotalD"].ToString();
                    dr["TotalG"] = dvBudget[i]["TotalG"].ToString();
                    dr["TotalMoney"] = Convert.ToDecimal(dvBudget[i]["TotalMoney"]);
                    dr["TotalBudgetTerm1"] = string.IsNullOrEmpty(dvBudget[i]["TotalBudgetTerm1"].ToString()) ? 0 : Convert.ToDecimal(dvBudget[i]["TotalBudgetTerm1"]);
                    dr["TotalBudgetTerm2"] = string.IsNullOrEmpty(dvBudget[i]["TotalBudgetTerm2"].ToString()) ? 0 : Convert.ToDecimal(dvBudget[i]["TotalBudgetTerm2"]);
                    dt1.Rows.Add(dr);
                }
                dvBudget = dt1.DefaultView;
                Session["budget"] = dt1;

                dvBudget.Sort = "id";
                GridViewBudget.DataSource = dvBudget;
                GridViewBudget.CheckListDataField = "id";
                GridViewBudget.DataBind();
                btaddBudget.Visible = false;
                btDelBudget.Visible = false;
                GridViewBudget.Columns[10].Visible = false;
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
            }
        }
        btDelBudget.Visible = false;
        btaddBudget.Visible = false;
        getLogDate(Request["id"]);
        getLogBudget(Request["id"]);
        getSummarizeBudget();
    }
    private void getProjectsDetail(string ProjectsID)
    {
        String mode = Request.QueryString["mode"];
        string strSql = " Select StrategiesCode, ProjectsDetail, Purpose, Target, Target2, Expected, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2  From Projects Where DelFlag = 0 And ProjectsCode = '" + ProjectsID + "'";
        DataView dv = Conn.Select(strSql);
        if (dv.Count != 0)
        {
            ddlStrategies.SelectedValue = dv[0]["StrategiesCode"].ToString();
            txtActivityDetail.Text = dv[0]["ProjectsDetail"].ToString();
            txtPurpose.Text = dv[0]["Purpose"].ToString();
            txtTarget.Text = dv[0]["Target"].ToString();
            txtTarget2.Text = dv[0]["Target2"].ToString();
            txtExpected.Text = dv[0]["Expected"].ToString();

            if (mode == "1")
            {
                txtProposeName.Text = dv[0]["ProposeName"].ToString();
                txtProposePosition.Text = dv[0]["ProposePosition"].ToString();
                txtApprovalName.Text = dv[0]["ApprovalName"].ToString();
                txtApprovalPosition.Text = dv[0]["ApprovalPosition"].ToString();
                txtApprovalName2.Text = dv[0]["ApprovalName2"].ToString();
                txtApprovalPosition2.Text = dv[0]["ApprovalPosition2"].ToString();

                string StrSql = " Select ProjectsCode, RecNum As id, IndicatorsName, MethodAss, ToolsAss "
                + " From ProjectsAssessment "
                + " Where ProjectsCode = '{0}' ";
                dvAssessment = Conn.Select(string.Format(StrSql + " Order By RecNum ", ProjectsID));

                if (dvAssessment.Count != 0)
                {
                    btDelAssessment.Visible = true;
                    if (Session["Assessment"] == null)
                    {
                        DataTable dt1 = new DataTable();
                        dt1.Columns.Add("id");
                        dt1.Columns.Add("IndicatorsName");
                        dt1.Columns.Add("MethodAss");
                        dt1.Columns.Add("ToolsAss");

                        DataRow dr;
                        for (int i = 0; i < dvAssessment.Count; i++)
                        {
                            dr = dt1.NewRow();
                            dr["id"] = dvAssessment[i]["id"].ToString();
                            dr["IndicatorsName"] = dvAssessment[i]["IndicatorsName"].ToString();
                            dr["MethodAss"] = dvAssessment[i]["MethodAss"].ToString();
                            dr["ToolsAss"] = dvAssessment[i]["ToolsAss"].ToString();
                            dt1.Rows.Add(dr);
                        }
                        dvAssessment = dt1.DefaultView;
                        Session["Assessment"] = dt1;

                        GridViewAssessment.DataSource = dvAssessment;
                        GridViewAssessment.CheckListDataField = "id";
                        GridViewAssessment.DataBind();
                    }
                }
                else {
                    Session.Remove("Assessment");
                    GridViewAssessment.DataSource = null;
                    GridViewAssessment.DataBind();
                }
            }
        }
        else
        {
            Session.Remove("Assessment");
            GridViewAssessment.DataSource = null;
            GridViewAssessment.DataBind();
            ddlStrategies.SelectedIndex = 0;
            txtActivityDetail.Text = "";
            txtPurpose.Text = "";
            txtTarget.Text = "";
            txtTarget2.Text = "";
            txtExpected.Text = "";
        }
    }
    private void getLogDate(string id)
    {
        DataView dv = Conn.Select("Select a.*, b.EmpName, '' As LogDateName from dtEditDateAc a, Employee b Where a.UpdateUser = b.EmpID And a.ActivityCode = '" + id + "' Order By a.UpdateDate Desc");
        if (dv.Count != 0)
        {
            for (int i = 0; i < dv.Count; i++)
            {
                dv[i]["LogDateName"] = "วันที่เริ่มกิจกรรมเดิม : " + Convert.ToDateTime(dv[i]["OldSDate"]).ToShortDateString() + ", วันที่สิ้นสุดกิจกรรมเดิม : " + Convert.ToDateTime(dv[i]["OldEDate"]).ToShortDateString() + " - ผู้แก้ไขล่าสุด : " + dv[i]["EmpName"].ToString() + " - " + Convert.ToDateTime(dv[i]["UpdateDate"]).ToShortDateString();
            }
            lblLogDate.Text = "แก้ไขวันที่ล่าสุด : ";
            rptLogEditDate.DataSource = dv;
            rptLogEditDate.DataBind();
            rptLogEditDate.Visible = true;
        }
        else
        {
            rptLogEditDate.DataSource = dv;
            rptLogEditDate.DataBind();
            rptLogEditDate.Visible = false;
        }
    }
    private void getLogBudget(string id)
    {
        DataView dv = Conn.Select("Select a.*, b.EmpName, '' As LogBudgetName from dtEditBudgetAc a, Employee b Where a.UpdateUser = b.EmpID And a.ActivityCode = '" + id + "' Order By a.UpdateDate Desc");
        if (dv.Count != 0)
        {
            for (int i = 0; i < dv.Count; i++)
            {
                dv[i]["LogBudgetName"] = "งบประมาณเดิม : " + Convert.ToDecimal(dv[i]["OldBudget"]).ToString("#,##0.00") + " - ผู้แก้ไขล่าสุด : " + dv[i]["EmpName"].ToString() + " - " + Convert.ToDateTime(dv[i]["UpdateDate"]).ToShortDateString();
            }
            lblLogBudget.Text = "แก้ไขงบประมาณล่าสุด : ";
            rptLogEditBudget.DataSource = dv;
            rptLogEditBudget.DataBind();
            rptLogEditBudget.Visible = true;
        }
        else
        {
            rptLogEditBudget.DataSource = dv;
            rptLogEditBudget.DataBind();
            rptLogEditBudget.Visible = false;
        }     
    }
    private void ClearAll()
    {
        Session.Remove("budget");
        GridViewBudget.DataSource = null;
        GridViewBudget.DataBind();

        Session.Remove("Operation2ac");
        GridViewOperation2.DataSource = null;
        GridViewOperation2.DataBind();

        Session.Remove("CostsCenter");
        dgCostsCenter.DataSource = null;
        dgCostsCenter.DataBind();

        Session.Remove("Assessment");
        GridViewAssessment.DataSource = null;
        GridViewAssessment.DataBind();

        getSummarizeBudget();

        txtActivity.Text = "";
        //txtActivityDetail.Text = "";
        //txtPurpose.Text = "";
        //txtTarget.Text = "";
        //txtTarget2.Text = "";
        txtOperation1.Text = "";
        txtPeriod1.Text = "";
        txtPlace1.Text = "";
        txtEmp1.Text = "";
        txtPeriod2.Text = "";
        txtPlace2.Text = "";
        txtEmp2.Text = "";
        txtOperation3.Text = "";
        txtPeriod3.Text = "";
        txtPlace3.Text = "";
        txtEmp3.Text = "";
        txtSolutions.Text = "";
        txtRisk.Text = "";
        txtProtect.Text = "";
        txtEvaluation.Text = "";
        txtEvaIndicators.Text = "";
        txtEvaIndicatorsProduct.Text = "";
        txtEvaAssessment.Text = "";
        txtEvaTool.Text = "";
        txtEvaTime.Text = "";
        //txtExpected.Text = "";
        txtEmp.Text = "";
        txtDepartment.Text = "";
        userid.Value = "";
        JId.Value = "";
        txtSubDept.Text = "";
        SubDeptid.Value = "";
        txtResource.Text = "";
        txtPlace.Text = "";
        rbtlType.SelectedIndex = 0;
        txtSDay.Text = DateTime.Now.ToShortDateString();
        txtEDay.Text = DateTime.Now.ToShortDateString();
        txtRealSDate.Text = DateTime.Now.ToShortDateString(); 
        txtRealEDate.Text = DateTime.Now.ToShortDateString();
        txtAlertDay.Text = "0";
        txtVolumeExpect.Text = "0";
        // txtSort.Text = "";
        txtSearch.Text = "";
        lblTotalAmount.Text = "0.00";
        txtParticipants.Text = "";
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private Boolean ckCostsWeight()
    {
        if (txtTotalCostsWeight.Text == "100")
        {
            lblCkTotalCostsWeight.Visible = false;
            return true;
        }
        else
        {
            lblCkTotalCostsWeight.Visible = true;
            return false;
        }
    }
    private void bt_Save(string CkAgain)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        if (btc.ckIdentityName("ckCostsCenter"))
        {
            if (!ckCostsWeight())
            {
                return;
            }
        }
        
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {
            string NewID = Guid.NewGuid().ToString();
            //btc.UpdateDefault("Activity");
            //Int32 i = Conn.AddNew("Activity", "ActivityCode, StrategiesCode, ProjectsCode, ActivityName, IdentityName, IdentityName2, ActivityDetail, Purpose, Target, Target2, Participants, Operation1, Period1, Place1, Emp1, Period2, Place2, Emp2, Operation3, Period3, Place3, Emp3, Solutions, Risk, Protect, Evaluation, EvaIndicators, EvaAssessment, EvaTool, Expected, VolumeExpect, Place, CostsType, TotalAmount, StudyYear, BudgetYear, Term, YearB, SDate, EDate, RealSDate, RealEDate, AlertDay, Sort, Df, Status, Resource, DeptCode, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate, ProposeName, ProposePosition, ApprovalName,  ApprovalPosition, ApprovalName2, ApprovalPosition2, ActivityStatus, MainActivityID",
            //    NewID, ddlStrategies.SelectedValue, ddlProjects.SelectedValue, txtActivity.Text, txtIdentityName.Text, txtIdentityName2.Text, txtActivityDetail.Text, txtPurpose.Text, txtTarget.Text, txtTarget2.Text, txtParticipants.Text, txtOperation1.Text, txtPeriod1.Text, txtPlace1.Text, txtEmp1.Text, txtPeriod2.Text, txtPlace2.Text, txtEmp2.Text, txtOperation3.Text, txtPeriod3.Text, txtPlace3.Text, txtEmp3.Text, txtSolutions.Text, txtRisk.Text, txtProtect.Text, txtEvaluation.Text, txtEvaIndicators.Text, txtEvaAssessment.Text, txtEvaTool.Text, txtExpected.Text, Convert.ToDecimal(txtVolumeExpect.Text), txtPlace.Text, rbtlType.SelectedValue, Convert.ToDecimal(lblTotalAmount.Text), ddlYearB.SelectedValue, ddlBudgetYear.SelectedValue, ddlTerm.SelectedValue, txtYearB.Text, Convert.ToDateTime(txtSDay.Text).ToString("s"), Convert.ToDateTime(txtEDay.Text).ToString("s"), Convert.ToDateTime(txtRealSDate.Text).ToString("s"), Convert.ToDateTime(txtRealEDate.Text).ToString("s"), txtAlertDay.Text, txtSort.Text, 1, 0, txtResource.Text, CurrentUser.DeptID, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now, txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, 0, ddlMainActivity.SelectedValue);
            strbSql.AppendFormat("INSERT INTO Activity (ActivityCode, StrategiesCode, ProjectsCode, ActivityName, IdentityName, IdentityName2, ActivityDetail, Purpose, Target, Target2, Participants, Operation1, Period1, Place1, Emp1, Period2, Place2, Emp2, Operation3, Period3, Place3, Emp3, Solutions, Risk, Protect, Evaluation, EvaIndicators, EvaAssessment, EvaTool, Expected, VolumeExpect, Place, CostsType, TotalAmount, StudyYear, BudgetYear, Term, YearB, SDate, EDate, RealSDate, RealEDate, AlertDay, Sort, Df, Status, Resource, DeptCode, DelFlag, SchoolID, CreateUser, CreateDate, UpdateUser, UpdateDate, ProposeName, ProposePosition, ApprovalName, ApprovalPosition, ApprovalName2, ApprovalPosition2, ActivityStatus, MainActivityID, ResponsiblePosition, AcProduct, AcTime)VALUES('{0}', '{1}', '{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}', N'{19}', N'{20}', N'{21}', N'{22}', N'{23}', N'{24}', N'{25}', N'{26}', N'{27}', N'{28}', N'{29}', {30}, N'{31}', '{32}', {33}, {34}, {35}, N'{36}', {37}, '{38}', '{39}', '{40}', '{41}', {42}, {43}, {44}, '{45}', N'{46}', '{47}', {48}, '{49}', '{50}', '{51}', '{52}', '{53}', N'{54}', N'{55}', N'{56}', N'{57}', N'{58}', N'{59}', {60}, '{61}', '{62}', '{63}', '{64}');",
                NewID, ddlStrategies.SelectedValue, ddlProjects.SelectedValue, txtActivity.Text, txtIdentityName.Text, txtIdentityName2.Text, txtActivityDetail.Text, txtPurpose.Text, txtTarget.Text, txtTarget2.Text, txtParticipants.Text, txtOperation1.Text, txtPeriod1.Text, txtPlace1.Text, txtEmp1.Text, txtPeriod2.Text, txtPlace2.Text, txtEmp2.Text, txtOperation3.Text, txtPeriod3.Text, txtPlace3.Text, txtEmp3.Text, txtSolutions.Text, txtRisk.Text, txtProtect.Text, txtEvaluation.Text, txtEvaIndicators.Text, txtEvaAssessment.Text, txtEvaTool.Text, txtExpected.Text, Convert.ToDecimal(txtVolumeExpect.Text), txtPlace.Text, rbtlType.SelectedValue, Convert.ToDecimal(lblTotalAmount.Text), Convert.ToInt32(ddlYearB.SelectedValue), Convert.ToInt32(ddlBudgetYear.SelectedValue), ddlTerm.SelectedValue, Convert.ToInt32(txtYearB.Text), Convert.ToDateTime(txtSDay.Text).ToString("yyyy-MM-dd", new CultureInfo("en-gb")) + " 00:00:00.000", Convert.ToDateTime(txtEDay.Text).ToString("yyyy-MM-dd", new CultureInfo("en-gb")) + " 00:00:00.000", Convert.ToDateTime(txtRealSDate.Text).ToString("yyyy-MM-dd", new CultureInfo("en-gb")) + " 00:00:00.000", Convert.ToDateTime(txtRealEDate.Text).ToString("yyyy-MM-dd", new CultureInfo("en-gb")) + " 00:00:00.000", Convert.ToInt32(txtAlertDay.Text), Convert.ToInt32(txtSort.Text), 1, '0', txtResource.Text, CurrentUser.DeptID, 0, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-gb")), CurrentUser.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-gb")), txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, 0, ddlMainActivity.SelectedValue, txtResponsiblePosition2.Text, txtEvaIndicatorsProduct.Text, txtEvaTime.Text);

            if (btc.ckIdentityName("ckSuan"))
            {
                string SuanIdentitySideName = "";
                for (int j = 0; j <= cblSuanIdentitySideName.Items.Count - 1; j++)
                {
                    if (cblSuanIdentitySideName.Items[j].Selected)
                    {
                        SuanIdentitySideName += cblSuanIdentitySideName.Items[j].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(SuanIdentitySideName))
                {
                    SuanIdentitySideName = SuanIdentitySideName.Substring(0, SuanIdentitySideName.Length - 1);
                }
                string SuanIdentityName2 = "";
                for (int j = 0; j <= cblSuanIdentityName2.Items.Count - 1; j++)
                {
                    if (cblSuanIdentityName2.Items[j].Selected)
                    {
                        SuanIdentityName2 += cblSuanIdentityName2.Items[j].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(SuanIdentityName2))
                {
                    SuanIdentityName2 = SuanIdentityName2.Substring(0, SuanIdentityName2.Length - 1);
                }
                string SuanGood = "";
                for (int j = 0; j <= cblSuanGood.Items.Count - 1; j++)
                {
                    if (cblSuanGood.Items[j].Selected)
                    {
                        SuanGood += cblSuanGood.Items[j].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(SuanGood))
                {
                    SuanGood = SuanGood.Substring(0, SuanGood.Length - 1);
                }

                for (int j = 0; j <= cblIndicators.Items.Count - 1; j++)
                {
                    if (cblIndicators.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtAcStrategiesIndicators (ActivityCode, ItemID)VALUES('{0}','{1}');", NewID, cblIndicators.Items[j].Value);
                    }
                }

                for (int j = 0; j <= cblGoals.Items.Count - 1; j++)
                {
                    if (cblGoals.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtAcStrategiesGoals (ActivityCode, ItemID)VALUES('{0}','{1}');", NewID, cblGoals.Items[j].Value);
                    }
                }

                strbSql.AppendFormat("Update Activity Set SuanIdentityName = '{1}', SuanIdentitySideName = '{2}', SuanIdentityName2 = '{3}', SuanGood = '{4}' Where ActivityCode = '{0}';", NewID, rbtlSuanIdentityName.SelectedValue, SuanIdentitySideName, SuanIdentityName2, SuanGood);
            }
            if (userid.Value != "")
            {
                string[] EmpID = userid.Value.Split(',');
                for (int j = 0; j < EmpID.Length; j++)
                {
                    strbSql.AppendFormat("INSERT INTO dtAcEmp (ActivityCode, EmpCode)VALUES('{0}','{1}');", NewID, EmpID[j]);
                    //Conn.AddNew("dtAcEmp", "ActivityCode, EmpCode", NewID, EmpID[j]);
                }
            }
            if (JId.Value != "")
            {
                string[] DeptID = JId.Value.Split(',');
                for (int j = 0; j < DeptID.Length; j++)
                {
                    strbSql.AppendFormat("INSERT INTO dtAcDept (ActivityCode, DeptCode)VALUES('{0}','{1}');", NewID, DeptID[j]);
                    //Conn.AddNew("dtAcDept", "ActivityCode, DeptCode", NewID, DeptID[j]);
                }
            }
            if (SubDeptid.Value != "")
            {
                string[] SubDeptID = SubDeptid.Value.Split(',');
                for (int j = 0; j < SubDeptID.Length; j++)
                {
                    strbSql.AppendFormat("INSERT INTO dtAcSubDept (ActivityCode, SubDeptCode)VALUES('{0}','{1}');", NewID, SubDeptID[j]);
                    //Conn.AddNew("dtAcSubDept", "ActivityCode, SubDeptCode", NewID, SubDeptID[j]);
                }
            }
            //Operation
            if (Session["Operation2ac"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Operation2ac"];
                dvOperation2 = dt1.DefaultView;
                for (int j = 0; j < dvOperation2.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ActivityOperation2 (ActivityCode, RecNum, Operation2, Budget2, BudgetOther)VALUES('{0}',{1},N'{2}',{3}, {4});",
                        NewID, Convert.ToDecimal(dvOperation2[j]["id"]), dvOperation2[j]["Operation2"].ToString(), Convert.ToDecimal(dvOperation2[j]["Budget2"]), Convert.ToDecimal(dvOperation2[j]["BudgetOther"]));
                    //Conn.AddNew("ActivityOperation2", "ActivityCode, RecNum, Operation2, Budget2", 
                    //    NewID, Convert.ToDecimal(dvOperation2[j]["id"]), dvOperation2[j]["Operation2"].ToString(), Convert.ToDecimal(dvOperation2[j]["Budget2"]));
                }
            }

            //Assessment
            if (Session["Assessment"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Assessment"];
                dvAssessment = dt1.DefaultView;
                for (int j = 0; j < dvAssessment.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ActivityAssessment (ActivityCode, RecNum, IndicatorsName, MethodAss, ToolsAss)VALUES('{0}',{1},N'{2}',N'{3}',N'{4}');",
                        NewID, Convert.ToDecimal(dvAssessment[j]["id"]), dvAssessment[j]["IndicatorsName"].ToString().Trim(), dvAssessment[j]["MethodAss"].ToString().Trim(), dvAssessment[j]["ToolsAss"].ToString().Trim());
                }
            }

            //Budget
            if (Session["budget"] != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["budget"];
                dvBudget = dt1.DefaultView;
                for (int j = 0; j < dvBudget.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO CostsDetail (ActivityCode, ListName, EntryCostsCode, BudgetTypeCode, BudgetTypeOtherName, TotalP, TotalD, TotalG, TotalMoney, TotalMoney2, ItemID, TotalBudgetTerm1, TotalBudgetTerm2, ListNo)VALUES('{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',{6},{7},{8},{9},'{10}',{11},{12},{13});",
                        NewID, dvBudget[j]["EntryCostsName"].ToString().Trim(), dvBudget[j]["EntryCostsCode"].ToString(), dvBudget[j]["BudgetTypeCode"].ToString(), dvBudget[j]["BudgetTypeName"].ToString(), dvBudget[j]["TotalP"], Convert.ToDecimal(dvBudget[j]["TotalD"]), Convert.ToDecimal(dvBudget[j]["TotalG"]), Convert.ToDecimal(dvBudget[j]["TotalMoney"]), 0, dvBudget[j]["ItemID"].ToString(), Convert.ToDecimal(dvBudget[j]["TotalBudgetTerm1"]), Convert.ToDecimal(dvBudget[j]["TotalBudgetTerm2"]), Convert.ToInt32(dvBudget[j]["id"]));
                    //Conn.AddNew("CostsDetail", "ActivityCode, ListName, EntryCostsCode, BudgetTypeCode, BudgetTypeOtherName, TotalP, TotalD, TotalG, TotalMoney, TotalMoney2", 
                    //    NewID, dvBudget[j]["EntryCostsName"].ToString(), dvBudget[j]["EntryCostsCode"].ToString(), dvBudget[j]["BudgetTypeCode"].ToString(), dvBudget[j]["BudgetTypeName"].ToString(), dvBudget[j]["TotalP"], Convert.ToDecimal(dvBudget[j]["TotalD"]), Convert.ToDecimal(dvBudget[j]["TotalG"]), Convert.ToDecimal(dvBudget[j]["TotalMoney"]), 0);
                }
            }

            if (btc.ckIdentityName("ckCostsCenter"))  // เช็คว่าเปิดโหมดต้นทุนศูนย์
            {
                //CostsCenter
                if (Session["CostsCenter"] != null)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["CostsCenter"];
                    dvCostsCenter = dt1.DefaultView;
                    for (int j = 0; j < dvCostsCenter.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtAcCostsCenter (ActivityCode, CostsCode, CostsSubCode, CostsWeight)VALUES('{0}',N'{1}',N'{2}',{3});",
                       NewID, dvCostsCenter[j]["CostsCode"].ToString(), dvCostsCenter[j]["CostsSubCode"].ToString(), Convert.ToDecimal(dvCostsCenter[j]["CostsWeight"]));
                        //Conn.AddNew("dtAcCostsCenter", "ActivityCode, CostsCode, CostsSubCode, CostsWeight", 
                        //    NewID, dvCostsCenter[j]["CostsCode"].ToString(), dvCostsCenter[j]["CostsSubCode"].ToString(), Convert.ToDecimal(dvCostsCenter[j]["CostsWeight"]));
                    }
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Session.Remove("Operation2ac");
            Session.Remove("Assessment");
            btc.UpdateStatusActivity();

            if (CkAgain == "N")
            {
                Response.Redirect("Activity.aspx?ckmode=1&Cr=" + result);   
            }
            if (CkAgain == "Y")
            {
                MultiView1.ActiveViewIndex = 1;
                btc.Msg_Head(Img1, MsgHead, true, "1", result);
                ClearAll();
                SetItem();
                getProjectsDetail(ddlProjects.SelectedValue);
                btc.GenSort(txtSort, "Activity", " And ProjectsCode = '" + ddlProjects.SelectedValue + "' And SchoolID = '" + CurrentUser.SchoolID + "' ");
                GridView3.Visible = true;
                DataBind();
            }
            if (CkAgain == "G")
            {
                Session.Remove("budget");
                Response.Redirect("Indicators2.aspx?mode=1&id=" + NewID + "&syear=" + ddlYearB.SelectedValue + "&stgid=" + ddlStrategies.SelectedValue + "&pjid=" + ddlProjects.SelectedValue);
            }
        }
        if (Request["mode"] == "2")
        {
            Int32 i = Conn.Update("Activity", "Where ActivityCode = '" + Request["id"] + "' ", "StrategiesCode, ProjectsCode, ActivityName, IdentityName, IdentityName2, ActivityDetail, Purpose, Target, Target2, Participants, Operation1, Period1, Place1, Emp1, Period2, Place2, Emp2, Operation3, Period3, Place3, Emp3, Solutions, Risk, Protect, Evaluation, EvaIndicators, EvaAssessment, EvaTool, Expected, VolumeExpect, Place, CostsType, StudyYear, BudgetYear, Term, YearB, RealSDate, RealEDate, AlertDay, Sort, Resource, SchoolID, UpdateUser, UpdateDate , ProposeName, ProposePosition, ApprovalName,  ApprovalPosition, ApprovalName2, ApprovalPosition2, MainActivityID, ResponsiblePosition, AcProduct, AcTime ",
                ddlStrategies.SelectedValue, ddlProjects.SelectedValue, txtActivity.Text, txtIdentityName.Text, txtIdentityName2.Text, txtActivityDetail.Text, txtPurpose.Text, txtTarget.Text, txtTarget2.Text, txtParticipants.Text, txtOperation1.Text, txtPeriod1.Text, txtPlace1.Text, txtEmp1.Text, txtPeriod2.Text, txtPlace2.Text, txtEmp2.Text, txtOperation3.Text, txtPeriod3.Text, txtPlace3.Text, txtEmp3.Text, txtSolutions.Text, txtRisk.Text, txtProtect.Text, txtEvaluation.Text, txtEvaIndicators.Text, txtEvaAssessment.Text, txtEvaTool.Text, txtExpected.Text, Convert.ToDecimal(txtVolumeExpect.Text), txtPlace.Text, rbtlType.SelectedValue, ddlYearB.SelectedValue, ddlBudgetYear.SelectedValue, ddlTerm.SelectedValue, txtYearB.Text, Convert.ToDateTime(txtRealSDate.Text).ToString("s"), Convert.ToDateTime(txtRealEDate.Text).ToString("s"), txtAlertDay.Text, txtSort.Text, txtResource.Text, CurrentUser.SchoolID, CurrentUser.ID, DateTime.Now, txtProposeName.Text, txtProposePosition.Text, txtApprovalName.Text, txtApprovalPosition.Text, txtApprovalName2.Text, txtApprovalPosition2.Text, ddlMainActivity.SelectedValue, txtResponsiblePosition2.Text, txtEvaIndicatorsProduct.Text, txtEvaTime.Text);

            if (btc.ckIdentityName("ckSuan"))
            {
                string SuanIdentitySideName = "";
                for (int j = 0; j <= cblSuanIdentitySideName.Items.Count - 1; j++)
                {
                    if (cblSuanIdentitySideName.Items[j].Selected)
                    {
                        SuanIdentitySideName += cblSuanIdentitySideName.Items[j].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(SuanIdentitySideName))
                {
                    SuanIdentitySideName = SuanIdentitySideName.Substring(0, SuanIdentitySideName.Length - 1);
                }
                string SuanIdentityName2 = "";
                for (int j = 0; j <= cblSuanIdentityName2.Items.Count - 1; j++)
                {
                    if (cblSuanIdentityName2.Items[j].Selected)
                    {
                        SuanIdentityName2 += cblSuanIdentityName2.Items[j].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(SuanIdentityName2))
                {
                    SuanIdentityName2 = SuanIdentityName2.Substring(0, SuanIdentityName2.Length - 1);
                }
                string SuanGood = "";
                for (int j = 0; j <= cblSuanGood.Items.Count - 1; j++)
                {
                    if (cblSuanGood.Items[j].Selected)
                    {
                        SuanGood += cblSuanGood.Items[j].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(SuanGood))
                {
                    SuanGood = SuanGood.Substring(0, SuanGood.Length - 1);
                }

                strbSql.AppendFormat("Delete dtAcStrategiesIndicators Where ActivityCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblIndicators.Items.Count - 1; j++)
                {
                    if (cblIndicators.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtAcStrategiesIndicators (ActivityCode, ItemID)VALUES('{0}','{1}');", Request["id"], cblIndicators.Items[j].Value);
                    }
                }

                strbSql.AppendFormat("Delete dtAcStrategiesGoals Where ActivityCode = '{0}';", Request["id"]);
                for (int j = 0; j <= cblGoals.Items.Count - 1; j++)
                {
                    if (cblGoals.Items[j].Selected)
                    {
                        strbSql.AppendFormat("INSERT INTO dtAcStrategiesGoals (ActivityCode, ItemID)VALUES('{0}','{1}');", Request["id"], cblGoals.Items[j].Value);
                    }
                }

                strbSql.AppendFormat("Update Activity Set SuanIdentityName = '{1}', SuanIdentitySideName = '{2}', SuanIdentityName2 = '{3}', SuanGood = '{4}' Where ActivityCode = '{0}';", Request["id"], rbtlSuanIdentityName.SelectedValue, SuanIdentitySideName, SuanIdentityName2, SuanGood);
            }

            if (Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus"))) // เช็คว่าเปิดโหมดติดตามกิจกรรม
            {
                Conn.Update("Activity", "Where ActivityCode = '" + Request["id"] + "'", "ActivityStatus", ddlActivityStatus.SelectedValue);
            }

            strbSql.AppendFormat("UPDATE Indicators2 SET StrategiesCode = '{1}', ProjectsCode = '{2}' Where ActivityCode = '{0}';", Request["id"], ddlStrategies.SelectedValue, ddlProjects.SelectedValue);
            strbSql.AppendFormat("UPDATE Evaluation SET StrategiesCode = '{1}', ProjectsCode = '{2}' Where ActivityCode = '{0}';", Request["id"], ddlStrategies.SelectedValue, ddlProjects.SelectedValue);
            strbSql.AppendFormat("UPDATE QALink SET StrategiesCode = '{1}', ProjectsCode = '{2}' Where ActivityCode = '{0}';", Request["id"], ddlStrategies.SelectedValue, ddlProjects.SelectedValue);
            //Conn.Update("Indicators2", "Where ActivityCode = '" + Request["id"] + "' ", "StrategiesCode, ProjectsCode", ddlStrategies.SelectedValue, ddlProjects.SelectedValue);
            //Conn.Update("Evaluation", "Where ActivityCode = '" + Request["id"] + "' ", "StrategiesCode, ProjectsCode", ddlStrategies.SelectedValue, ddlProjects.SelectedValue);
            //Conn.Update("QALink", "Where ActivityCode = '" + Request["id"] + "' ", "StrategiesCode, ProjectsCode", ddlStrategies.SelectedValue, ddlProjects.SelectedValue);

            strbSql.AppendFormat("Delete dtAcEmp Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcDept Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcSubDept Where ActivityCode = '{0}';", Request["id"]);
            //Conn.Delete("dtAcEmp", "Where ActivityCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtAcDept", "Where ActivityCode = '" + Request["id"] + "' ");
            //Conn.Delete("dtAcSubDept", "Where ActivityCode = '" + Request["id"] + "' ");
            if (userid.Value != "")
            {
                string[] EmpID = userid.Value.Split(',');
                for (int j = 0; j < EmpID.Length; j++)
                {
                    strbSql.AppendFormat("INSERT INTO dtAcEmp (ActivityCode, EmpCode)VALUES('{0}','{1}');", Request["id"], EmpID[j]);
                    //Conn.AddNew("dtAcEmp", "ActivityCode, EmpCode", Request["id"], EmpID[j]);
                }
            }
            if (JId.Value != "")
            {
                string[] DeptID = JId.Value.Split(',');
                for (int j = 0; j < DeptID.Length; j++)
                {
                    strbSql.AppendFormat("INSERT INTO dtAcDept (ActivityCode, DeptCode)VALUES('{0}','{1}');", Request["id"], DeptID[j]);
                    //Conn.AddNew("dtAcDept", "ActivityCode, DeptCode", Request["id"], DeptID[j]);
                }
            }
            if (SubDeptid.Value != "")
            {
                string[] SubDeptID = SubDeptid.Value.Split(',');
                for (int j = 0; j < SubDeptID.Length; j++)
                {
                    strbSql.AppendFormat("INSERT INTO dtAcSubDept (ActivityCode, SubDeptCode)VALUES('{0}','{1}');", Request["id"], SubDeptID[j]);
                    //Conn.AddNew("dtAcSubDept", "ActivityCode, SubDeptCode", Request["id"], SubDeptID[j]);
                }
            }
            //Operation
            if (Session["Operation2ac"] != null)
            {
                strbSql.AppendFormat("Delete ActivityOperation2 Where ActivityCode = '{0}';", Request["id"]);
                //Conn.Delete("ActivityOperation2", "Where ActivityCode = '" + Request["id"] + "'");
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Operation2ac"];
                dvOperation2 = dt1.DefaultView;
                for (int j = 0; j < dvOperation2.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ActivityOperation2 (ActivityCode, RecNum, Operation2, Budget2, BudgetOther)VALUES('{0}',{1},N'{2}',{3}, {4});",
                        Request["id"], Convert.ToDecimal(dvOperation2[j]["id"]), dvOperation2[j]["Operation2"].ToString(), Convert.ToDecimal(dvOperation2[j]["Budget2"]), Convert.ToDecimal(dvOperation2[j]["BudgetOther"]));
                    //Conn.AddNew("ActivityOperation2", "ActivityCode, RecNum, Operation2, Budget2", 
                    //    Request["id"], Convert.ToDecimal(dvOperation2[j]["id"]), dvOperation2[j]["Operation2"].ToString(), Convert.ToDecimal(dvOperation2[j]["Budget2"]));
                }
            }

            //Assessment
            if (Session["Assessment"] != null)
            {
                strbSql.AppendFormat("Delete ActivityAssessment Where ActivityCode = '{0}';", Request["id"]);
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)Session["Assessment"];
                dvAssessment = dt1.DefaultView;
                for (int j = 0; j < dvAssessment.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ActivityAssessment (ActivityCode, RecNum, IndicatorsName, MethodAss, ToolsAss)VALUES('{0}',{1},N'{2}',N'{3}',N'{4}');",
                        Request["id"], Convert.ToDecimal(dvAssessment[j]["id"]), dvAssessment[j]["IndicatorsName"].ToString().Trim(), dvAssessment[j]["MethodAss"].ToString().Trim(), dvAssessment[j]["ToolsAss"].ToString().Trim());
                }
            }

            if (btc.ckIdentityName("ckCostsCenter")) // เช็คว่าเปิดโหมดต้นทุนศูนย์
            {
                //CostsCenter
                if (Session["CostsCenter"] != null)
                {
                    strbSql.AppendFormat("Delete dtAcCostsCenter Where ActivityCode = '{0}';", Request["id"]);
                    //Conn.Delete("dtAcCostsCenter", "Where ActivityCode = '" + Request["id"] + "'");
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["CostsCenter"];
                    dvCostsCenter = dt1.DefaultView;
                    for (int j = 0; j < dvCostsCenter.Count; j++)
                    {
                        strbSql.AppendFormat("INSERT INTO dtAcCostsCenter (ActivityCode, CostsCode, CostsSubCode, CostsWeight)VALUES('{0}',N'{1}',N'{2}',{3});",
                            Request["id"], dvCostsCenter[j]["CostsCode"].ToString(), dvCostsCenter[j]["CostsSubCode"].ToString(), Convert.ToDecimal(dvCostsCenter[j]["CostsWeight"]));
                        //Conn.AddNew("dtAcCostsCenter", "ActivityCode, CostsCode, CostsSubCode, CostsWeight", 
                        //    Request["id"], dvCostsCenter[j]["CostsCode"].ToString(), dvCostsCenter[j]["CostsSubCode"].ToString(), Convert.ToDecimal(dvCostsCenter[j]["CostsWeight"]));
                    }
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Session.Remove("Operation2ac");
            Session.Remove("Assessment");
            btc.UpdateStatusActivity();
            Response.Redirect("Activity.aspx?ckmode=2&Cr=" + i); 
        }
    }
    protected void SearchDept_Click(object sender, EventArgs e)
    {
        DataView dv, dv1;
        if (userid.Value != "")
        {
            string[] EmpID = userid.Value.Split(',');
            string NewEmpID = "";
            for (int j = 0; j < EmpID.Length; j++)
            {
                NewEmpID += "'" + EmpID[j] + "'";
                if (j != EmpID.Length - 1)
                {
                    NewEmpID += ",";
                }
            }

            string strSql = " Select Distinct(EmpID) EmpID, EmpName From Employee "
                        + " Where DelFlag = 0 And EmpID In(" + NewEmpID + ")";
            dv = Conn.Select(strSql);
            if (dv.Count != 0)
            {
                string EmpCode = "";
                string EmpName = "";
                for (int i = 0; i < dv.Count; i++)
                {
                    EmpCode += dv[i]["EmpID"].ToString();
                    EmpName += dv[i]["EmpName"].ToString();
                    if (i != dv.Count - 1)
                    {
                        EmpCode += ",";
                        EmpName += ",";
                    }
                    txtEmp.Text = EmpName;
                    userid.Value = EmpCode;
                }
            }
        }

        if (JId.Value != "")
        {
            string[] DeptID = JId.Value.Split(',');
            string NewDeptID = "";
            for (int j = 0; j < DeptID.Length; j++)
            {
                NewDeptID += "'" + DeptID[j] + "'";
                if (j != DeptID.Length - 1)
                {
                    NewDeptID += ",";
                }
            }

            string strSql = " Select Distinct(DeptCode) DeptCode, DeptName From Department "
                        + " Where DelFlag = 0 And DeptCode In(" + NewDeptID + ")";
            dv1 = Conn.Select(strSql);
            if (dv1.Count != 0)
            {
                string DeptCode = "";
                string DeptName = "";
                for (int i = 0; i < dv1.Count; i++)
                {
                    DeptCode += dv1[i]["DeptCode"].ToString();
                    DeptName += dv1[i]["DeptName"].ToString();
                    if (i != dv1.Count - 1)
                    {
                        DeptCode += ",";
                        DeptName += ",";
                    }
                    txtDepartment.Text = DeptName;
                    JId.Value = DeptCode;
                }
            }
        }
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
    }
    protected void btSearchInd_Click(object sender, EventArgs e)
    {
        if (hdfEvaIndicators.Value != "")
        {
            string[] RefID = hdfEvaIndicators.Value.Split(',');
            string NewID = "";
            for (int j = 0; j < RefID.Length; j++)
            {
                NewID += "'" + RefID[j] + "'";
                if (j != RefID.Length - 1)
                {
                    NewID += ",";
                }
            }

            string strSql = " Select RecNum As ID, IndicatorName As Name From StrategiesIndicators "
                        + " Where StrategiesCode = '" + ddlStrategies.SelectedValue + "' And RecNum In (" + NewID + ")";

            DataView dv = Conn.Select(strSql);
            if (dv.Count != 0)
            {
                string ID = "";
                string Name = "";

                for (int i = 0; i < dv.Count; i++)
                {
                    ID += dv[i]["ID"].ToString();
                    Name += dv[i]["Name"].ToString();
                    if (i != dv.Count - 1)
                    {
                        ID += ",";
                        Name += ",";
                    }
                    txtEvaIndicators.Text = Name;
                    hdfEvaIndicators.Value = ID;
                }
            }
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
    private void enableMode(string mode, Boolean enables)
    {
        if (mode == "d")
        {
            btEditDate.Enabled = !enables;
            ddlSDay.Enabled = enables;
            ddlSMonth.Enabled = enables;
            ddlSYear.Enabled = enables;
            ddlEDay.Enabled = enables;
            ddlEMonth.Enabled = enables;
            ddlEYear.Enabled = enables;
        }
        if (mode == "b")
        {
            btEditBudget.Enabled = enables;
            btDelBudget.Visible = !enables;
            btaddBudget.Visible = !enables;
            GridViewBudget.Columns[10].Visible = !enables; 
        }         
    }
    protected void btEditDate_Click(object sender, EventArgs e)
    {
        btc.btEnable(btSave, false);
        btc.btEnable(btSaveEditDate, true);
        btc.btEnable(btSaveEditBudget, false);
        btc.btEnable(btCancelEdit2, true);
        enableMode("d", true);
    }
    protected void btEditBudget_Click(object sender, EventArgs e)
    {
        btc.btEnable(btSave, false);
        btc.btEnable(btSaveEditDate, false);
        btc.btEnable(btSaveEditBudget, true);
        btc.btEnable(btCancelEdit, true);
        enableMode("b", false);
    }
    protected void btApproveBudget_Click(object sender, EventArgs e)
    {
        Conn.Update("Activity", "Where ActivityCode = '" + Request["id"] + "'", "ApproveFlag, ApproveUser", 1, CurrentUser.ID);
        btApproveBudget.Visible = false;
        DataView dvApproveUser = Conn.Select("Select ApproveUser From Activity Where ActivityCode = '" + Request["id"] + "'");
        if (!string.IsNullOrEmpty(dvApproveUser[0]["ApproveUser"].ToString()))
        {
            string ApproveName = "";
            ApproveName = btc.getEmpName(dvApproveUser[0]["ApproveUser"].ToString());
            lblAlertApprove.Text = "*** กิจกรรมนี้ได้ถูกอนุมัติแล้ว โดยคุณ " + ApproveName + " ถ้าต้องการแก้ไขวันที่ดำเนินการหรือแก้ไขงบประมาณ  กรุณาติดต่อฝ่ายแผนงาน";
        }
        lblAlertApprove.Visible = true;
        ImgBtUnApprove.Visible = true;
    }
    protected void btSaveEditDate_Click(object sender, EventArgs e)
    {
        DataView dv = Conn.Select("Select SDate, EDate From Activity Where ActivityCode = '" + Request["id"] + "'");
        Conn.AddNew("dtEditDateAc", "ActivityCode, OldSDate, OldEdate, UpdateUser, UpdateDate", Request["id"], dv[0]["SDate"], dv[0]["EDate"], CurrentUser.ID, DateTime.Now);

        Int32 i = Conn.Update("Activity", "Where ActivityCode = '" + Request["id"] + "' ", "SDate, EDate, UpdateUser, UpdateDate", Convert.ToDateTime(txtSDay.Text).ToString("s"), Convert.ToDateTime(txtEDay.Text).ToString("s"), CurrentUser.ID, DateTime.Now);
        btc.UpdateStatusActivity();
        ClearEdit();
    }
    protected void btSaveEditBudget_Click(object sender, EventArgs e)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        DataView dv = Conn.Select("Select TotalAmount From Activity Where ActivityCode = '" + Request["id"] + "'");
        Conn.AddNew("dtEditBudgetAc", "ActivityCode, OldBudget, UpdateUser, UpdateDate", Request["id"], dv[0]["TotalAmount"], CurrentUser.ID, DateTime.Now);

        Int32 i = Conn.Update("Activity", "Where ActivityCode = '" + Request["id"] + "' ", "TotalAmount, UpdateUser, UpdateDate", Convert.ToDecimal(lblTotalAmount.Text), CurrentUser.ID, DateTime.Now);

        //Budget
        if (Session["budget"] != null)
        {
            strbSql.AppendFormat("Delete CostsDetail Where ActivityCode = '{0}';", Request["id"]);
            //Conn.Delete("CostsDetail", "Where ActivityCode = '" + Request["id"] + "'");
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["budget"];
            dvBudget = dt1.DefaultView;
            for (int j = 0; j < dvBudget.Count; j++)
            {
                strbSql.AppendFormat("INSERT INTO CostsDetail (ActivityCode, ListName, EntryCostsCode, BudgetTypeCode, BudgetTypeOtherName, TotalP, TotalD, TotalG, TotalMoney, TotalMoney2, ItemID, TotalBudgetTerm1, TotalBudgetTerm2, ListNo)VALUES('{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',{6},{7},{8},{9},'{10}',{11},{12},{13});",
                    Request["id"], dvBudget[j]["EntryCostsName"].ToString().Trim(), dvBudget[j]["EntryCostsCode"].ToString(), dvBudget[j]["BudgetTypeCode"].ToString(), dvBudget[j]["BudgetTypeName"].ToString(), dvBudget[j]["TotalP"], Convert.ToDecimal(dvBudget[j]["TotalD"]), Convert.ToDecimal(dvBudget[j]["TotalG"]), Convert.ToDecimal(dvBudget[j]["TotalMoney"]), 0, dvBudget[j]["ItemID"].ToString(), Convert.ToDecimal(dvBudget[j]["TotalBudgetTerm1"]), Convert.ToDecimal(dvBudget[j]["TotalBudgetTerm2"]), j);
                //Conn.AddNew("CostsDetail", "ActivityCode, ListName, EntryCostsCode, BudgetTypeCode, BudgetTypeOtherName, TotalP, TotalD, TotalG, TotalMoney, TotalMoney2", Request["id"], dvBudget[j]["EntryCostsName"].ToString().Trim(), dvBudget[j]["EntryCostsCode"].ToString(), dvBudget[j]["BudgetTypeCode"].ToString(), dvBudget[j]["BudgetTypeName"].ToString(), dvBudget[j]["TotalP"], Convert.ToDecimal(dvBudget[j]["TotalD"]), Convert.ToDecimal(dvBudget[j]["TotalG"]), Convert.ToDecimal(dvBudget[j]["TotalMoney"]), 0);
            }
            result = Conn.Execute(strbSql.ToString());
        }
        ClearEdit();
    }
    protected void btCancelEdit_Click(object sender, EventArgs e)
    {
        string strSql = " Select a.*, a.ListNo As id, a.ListName As EntryCostsName, a.EntryCostsCode As EntryCostsCode, '' As txtCostsName, BudgetTypeName = Case a.BudgetTypeCode When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then a.BudgetTypeOtherName Else b.BudgetTypeName End "
        + " From CostsDetail a, BudgetType b "
        + " Where a.BudgetTypeCode = b.BudgetTypeCode And a.ActivityCode = '{0}' ";
        dvBudget = Conn.Select(string.Format(strSql + " Order By a.ListNo, b.Sort ", Request.QueryString["id"]));

        Session.Remove("budget");
        if (dvBudget.Count != 0)
        {
            for (int i = 0; i < dvBudget.Count; i++)
            {
                dvBudget[i]["txtCostsName"] = new BTC().getEntryCostsName(dvBudget[i]["EntryCostsCode"].ToString());
            }
            if (Session["budget"] == null)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("id", typeof(int));
                dt1.Columns.Add("ItemID");
                dt1.Columns.Add("EntryCostsName");
                dt1.Columns.Add("txtCostsName");
                dt1.Columns.Add("EntryCostsCode");
                dt1.Columns.Add("BudgetTypeName");
                dt1.Columns.Add("BudgetTypeCode");
                dt1.Columns.Add("TotalP");
                dt1.Columns.Add("TotalD");
                dt1.Columns.Add("TotalG");
                dt1.Columns.Add("TotalMoney", typeof(decimal));
                dt1.Columns.Add("TotalBudgetTerm1", typeof(decimal));
                dt1.Columns.Add("TotalBudgetTerm2", typeof(decimal));

                DataRow dr;
                for (int i = 0; i < dvBudget.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["id"] = (!string.IsNullOrEmpty(dvBudget[i]["id"].ToString()) ? Convert.ToInt16(dvBudget[i]["id"]) : dr.Table.Rows.Count);
                    dr["ItemID"] = dvBudget[i]["ItemID"].ToString();
                    dr["EntryCostsName"] = dvBudget[i]["EntryCostsName"].ToString();
                    dr["txtCostsName"] = dvBudget[i]["txtCostsName"].ToString();
                    dr["EntryCostsCode"] = dvBudget[i]["EntryCostsCode"].ToString();
                    dr["BudgetTypeName"] = dvBudget[i]["BudgetTypeName"].ToString();
                    dr["BudgetTypeCode"] = dvBudget[i]["BudgetTypeCode"].ToString();
                    dr["TotalP"] = dvBudget[i]["TotalP"].ToString();
                    dr["TotalD"] = dvBudget[i]["TotalD"].ToString();
                    dr["TotalG"] = dvBudget[i]["TotalG"].ToString();
                    dr["TotalMoney"] = Convert.ToDecimal(dvBudget[i]["TotalMoney"]);
                    dr["TotalBudgetTerm1"] = string.IsNullOrEmpty(dvBudget[i]["TotalBudgetTerm1"].ToString()) ? 0 : Convert.ToDecimal(dvBudget[i]["TotalBudgetTerm1"]);
                    dr["TotalBudgetTerm2"] = string.IsNullOrEmpty(dvBudget[i]["TotalBudgetTerm2"].ToString()) ? 0 : Convert.ToDecimal(dvBudget[i]["TotalBudgetTerm2"]);
                    dt1.Rows.Add(dr);
                }
                dvBudget = dt1.DefaultView;
                Session["budget"] = dt1;

                dvBudget.Sort = "id";
                GridViewBudget.DataSource = dvBudget;
                GridViewBudget.CheckListDataField = "id";
                GridViewBudget.DataBind();
                btaddBudget.Visible = false;
                btDelBudget.Visible = false;
                GridViewBudget.Columns[10].Visible = false;
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
            }
        }
        getSummarizeBudget();
        ClearEdit();
    }
    private void ClearEdit()
    {
        btEditDate.Enabled = true;
        if (btc.CkActivityBudgetDetail(Request["id"]))
        {
            btEditBudget.Enabled = false;
            btEditBudget.ToolTip = "ไม่สามารถแก้ไขงบประมาณได้เนื่องจากกมีการเบิกงบย่อยแล้ว";
        }
        else
        {
            btEditBudget.Enabled = true;
        }
        GridViewBudget.Columns[10].Visible = false;
        btc.btEnable(btSave, true);
        btc.btEnable(btSaveEditDate, false);
        btc.btEnable(btSaveEditBudget, false);
        btc.btEnable(btCancelEdit, false);
        btc.btEnable(btCancelEdit2, false);

        ddlSDay.Enabled = false;
        ddlSMonth.Enabled = false;
        ddlSYear.Enabled = false;
        ddlEDay.Enabled = false;
        ddlEMonth.Enabled = false;
        ddlEYear.Enabled = false;

        btaddBudget.Visible = false;
        btDelBudget.Visible = false;

        getddlDepartment();
        getLogDate(Request["id"]);
        getLogBudget(Request["id"]);
    }
    private void Delete(string id)
    {
        int result = 0;
        StringBuilder strbSql = new StringBuilder();

        if (String.IsNullOrEmpty(id)) return;
        int i = 0;
        if (btc.CkUseData(id, "ActivityCode", "Indicators2", " And DelFlag = 0 "))
        {
            Response.Redirect("Activity.aspx?ckmode=3&Cr=" + i);
        }
        else
        {
            i = Conn.Update("Activity", "Where ActivityCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);      
            Conn.Update("ActivityDetail", "Where ActivityCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);

            strbSql.AppendFormat("Delete ActivityOperation2 Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcDept Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcEmp Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcSubDept Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtFactor Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtEditBudgetAc Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtEditDateAc Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcCostsCenter Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete CostsDetail Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcStrategiesIndicators Where ActivityCode = '{0}';", Request["id"]);
            strbSql.AppendFormat("Delete dtAcStrategiesGoals Where ActivityCode = '{0}';", Request["id"]);
            

            //Conn.Delete("ActivityOperation2", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtAcDept", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtAcEmp", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtAcSubDept", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtFactor", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtEditBudgetAc", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtEditDateAc", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("dtAcCostsCenter", "Where ActivityCode = '" + Request["id"] + "'");
            //Conn.Delete("CostsDetail", "Where ActivityCode = '" + Request["id"] + "'");

            result = Conn.Execute(strbSql.ToString());
            Response.Redirect("Activity.aspx?ckmode=3&Cr=" + i);
        }
    }
    protected void ddlProjects_OnSelectedChanged(object sender, EventArgs e)
    {
        btc.GenSort(txtSort, "Activity", " And ProjectsCode = '" + ddlProjects.SelectedValue + "'");
        getProjectsDetail(ddlProjects.SelectedValue);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlStrategies(0, ddlSearchYear.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        DataBind();
    }
    protected void ddlSearch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcStrategies", ddlSearch2.SelectedValue);
        getddlProjects(0, ddlSearchYear.SelectedValue, ddlSearch2.SelectedValue);
        DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcProjects", ddlSearch.SelectedValue);
        ddlSearch2.SelectedValue = btc.getStrategiesCodeOfProj(ddlSearch.SelectedValue);
        Cookie.SetValue2("AcStrategies", ddlSearch2.SelectedValue);
        DataBind();
    }
    protected void ddlSearchMainActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("MainActivity", ddlSearchMainActivity.SelectedValue);
        DataBind();
    }
    protected void ddlSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcType", ddlSearchType.SelectedValue);
        DataBind();
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["AcDept"] = ddlSearchDept.SelectedValue;
        Cookie.SetValue2("AcDept", ddlSearchDept.SelectedValue);
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        getddlSubDept(0, ddlSearchDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchSubDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcSubDept", ddlSearchSubDept.SelectedValue);
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["AcEmp"] = ddlSearchEmp.SelectedValue;
        Cookie.SetValue2("AcEmp", ddlSearchEmp.SelectedValue);
        DataBind();
    }
   
    protected void ddlStrategies_OnSelectedChanged(object sender, EventArgs e)
    {
        getcblIndicators();
        getcblGoals();
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        if (ddlStrategies.SelectedIndex != 0)
        {
            lblStrategies.Text = ddlStrategies.SelectedItem.Text.Replace("กลยุทธ์ที่ ", "S");
        }
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.ForeColor = btc.getColorRowsGrid(DataBinder.Eval(e.Row.DataItem, "status").ToString());
        }
    }
    protected void btaddOperation2_Click(object sender, EventArgs e)
    {
        if (Session["Operation2ac"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("Operation2");
            dt1.Columns.Add("budget2");
            dt1.Columns.Add("budgetOther");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["Operation2"] = txtOperation2.Text;
            dr["budget2"] = txtBudget2.Text;
            dr["budgetOther"] = txtBudgetOther.Text;
            dt1.Rows.Add(dr);

            dvOperation2 = dt1.DefaultView;
            Session["Operation2ac"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Operation2ac"];

            if (txtid2.Text == "")
            {
                DataView ckdv = ((DataTable)Session["Operation2ac"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int i = 0; i < ckdv.Count; i++)
                    {
                        if (ckdv[i]["Operation2"].ToString() == txtOperation2.Text)
                        {
                            lblckOperation2.Visible = true;
                            return;
                        }
                    }
                }
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["Operation2"] = txtOperation2.Text;
                dr["budget2"] = txtBudget2.Text;
                dr["budgetOther"] = txtBudgetOther.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                DataView ckdv = ((DataTable)Session["Operation2ac"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int j = 0; j < ckdv.Count; j++)
                    {
                        if ((ckdv[j]["id"].ToString() != txtid2.Text) && (ckdv[j]["Operation2"].ToString() == txtOperation2.Text))
                        {
                            lblckOperation2.Visible = true;
                            return;
                        }
                    }
                }
                Int32 i = Convert.ToInt32(txtid2.Text);
                dt1.Rows[i]["Operation2"] = txtOperation2.Text;
                dt1.Rows[i]["budget2"] = txtBudget2.Text;
                dt1.Rows[i]["budgetOther"] = txtBudgetOther.Text;
            }
            lblckOperation2.Visible = false;

            dvOperation2 = dt1.DefaultView;
            Session["Operation2ac"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearOperation2();
        GridViewOperation2.DataSource = dvOperation2;
        GridViewOperation2.CheckListDataField = "id";
        GridViewOperation2.DataBind();
        if (dvOperation2.Count > 0)
        {
            btDelOperation2.Visible = true;
        }
    }
    private void ClearOperation2()
    {
        txtid2.Text = "";
        txtOperation2.Text = "";
        txtBudget2.Text = "0";
        txtBudgetOther.Text = "0";
    }
    protected void btDelOperation2_Click(object sender, EventArgs e)
    {
        if (GridViewOperation2.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Operation2ac"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewOperation2.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvOperation2 = dt1.DefaultView;
        Session["Operation2ac"] = dt1;
        GridViewOperation2.DataSource = dvOperation2;
        GridViewOperation2.DataBind();
        if (dvOperation2.Count == 0)
        {
            btDelOperation2.Visible = false;
        }
    }
    protected void btaddBudget_Click(object sender, EventArgs e)
    {
        if (Session["budget"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id", typeof(int));
            dt1.Columns.Add("ItemID");
            dt1.Columns.Add("EntryCostsName");
            dt1.Columns.Add("txtCostsName");
            dt1.Columns.Add("EntryCostsCode");
            dt1.Columns.Add("BudgetTypeName");
            dt1.Columns.Add("BudgetTypeCode");
            dt1.Columns.Add("TotalP");
            dt1.Columns.Add("TotalD");
            dt1.Columns.Add("TotalG");
            dt1.Columns.Add("TotalMoney", typeof(decimal));
            dt1.Columns.Add("TotalBudgetTerm1", typeof(decimal));
            dt1.Columns.Add("TotalBudgetTerm2", typeof(decimal));

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["ItemID"] = Guid.NewGuid().ToString();
            dr["EntryCostsName"] = txtListName.Text;
            dr["txtCostsName"] = (ddlEntryCosts.SelectedIndex == 0) ? "" : ddlEntryCosts.SelectedItem.Text;
            dr["EntryCostsCode"] = ddlEntryCosts.SelectedValue;
            dr["BudgetTypeName"] = (ddlBudgetType.SelectedIndex != 3) ? ddlBudgetType.SelectedItem.Text.ToString() : ddlBudgetType.SelectedItem.Text.ToString() + " (" + txtBudgetType.Text + ") ";
            dr["BudgetTypeCode"] = ddlBudgetType.SelectedValue.ToString();
            dr["TotalP"] = txtP.Text;
            dr["TotalD"] = txtD.Text;
            dr["TotalG"] = txtG.Text;
            dr["TotalMoney"] = (Convert.ToDecimal(txtD.Text) * Convert.ToDecimal(txtG.Text));
            dr["TotalBudgetTerm1"] = Convert.ToDecimal(txtTotalBudgetTerm1.Text);
            dr["TotalBudgetTerm2"] = Convert.ToDecimal(txtTotalBudgetTerm2.Text);
            dt1.Rows.Add(dr);

            dvBudget = dt1.DefaultView;
            Session["budget"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["budget"];

            if (txtid.Text == "")
            {
                DataView ckdv = ((DataTable)Session["budget"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int i = 0; i < ckdv.Count; i++)
                    {
                        if ((ckdv[i]["EntryCostsName"].ToString() == txtListName.Text) && (ckdv[i]["BudgetTypeCode"].ToString() == ddlBudgetType.SelectedValue.ToString()))
                        {
                            lblckBudget.Visible = true;
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
                            return;
                        }
                    }
                }

                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["ItemID"] = Guid.NewGuid().ToString();
                dr["EntryCostsName"] = txtListName.Text;
                dr["txtCostsName"] = (ddlEntryCosts.SelectedIndex == 0) ? "" : ddlEntryCosts.SelectedItem.Text;
                dr["EntryCostsCode"] = ddlEntryCosts.SelectedValue;
                dr["BudgetTypeName"] = (ddlBudgetType.SelectedIndex != 3) ? ddlBudgetType.SelectedItem.Text.ToString() : ddlBudgetType.SelectedItem.Text.ToString() + " (" + txtBudgetType.Text + ") ";
                dr["BudgetTypeCode"] = ddlBudgetType.SelectedValue.ToString();
                dr["TotalP"] = txtP.Text;
                dr["TotalD"] = txtD.Text;
                dr["TotalG"] = txtG.Text;
                dr["TotalMoney"] = (Convert.ToDecimal(txtD.Text) * Convert.ToDecimal(txtG.Text));
                dr["TotalBudgetTerm1"] = Convert.ToDecimal(txtTotalBudgetTerm1.Text);
                dr["TotalBudgetTerm2"] = Convert.ToDecimal(txtTotalBudgetTerm2.Text);
                dt1.Rows.Add(dr);
            }
            else
            {
                DataView ckdv = ((DataTable)Session["budget"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int j = 0; j < ckdv.Count; j++)
                    {
                        if ((ckdv[j]["id"].ToString() != txtid.Text) && (ckdv[j]["EntryCostsName"].ToString() == txtListName.Text) && (ckdv[j]["BudgetTypeCode"].ToString() == ddlBudgetType.SelectedValue.ToString()))
                        {
                            lblckBudget.Visible = true;
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
                            return;
                        }
                    }
                }

                Int32 i = Convert.ToInt32(txtid.Text);
                dt1.Rows[i]["ItemID"] = hdfItemID.Value;
                dt1.Rows[i]["EntryCostsName"] = txtListName.Text;
                dt1.Rows[i]["txtCostsName"] = (ddlEntryCosts.SelectedIndex == 0) ? "" : ddlEntryCosts.SelectedItem.Text;
                dt1.Rows[i]["EntryCostsCode"] = ddlEntryCosts.SelectedValue;
                dt1.Rows[i]["BudgetTypeName"] = (ddlBudgetType.SelectedIndex != 3) ? ddlBudgetType.SelectedItem.Text.ToString() : ddlBudgetType.SelectedItem.Text.ToString() + " (" + txtBudgetType.Text + ") ";
                dt1.Rows[i]["BudgetTypeCode"] = ddlBudgetType.SelectedValue.ToString();
                dt1.Rows[i]["TotalP"] = txtP.Text;
                dt1.Rows[i]["TotalD"] = txtD.Text;
                dt1.Rows[i]["TotalG"] = txtG.Text;
                dt1.Rows[i]["TotalMoney"] = (Convert.ToDecimal(txtD.Text) * Convert.ToDecimal(txtG.Text));
                dt1.Rows[i]["TotalBudgetTerm1"] = Convert.ToDecimal(txtTotalBudgetTerm1.Text);
                dt1.Rows[i]["TotalBudgetTerm2"] = Convert.ToDecimal(txtTotalBudgetTerm2.Text);
            }
            lblckBudget.Visible = false;

            dvBudget = dt1.DefaultView;
            Session["budget"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearBudget();
        dvBudget.Sort = "id";
        GridViewBudget.DataSource = dvBudget;
        GridViewBudget.CheckListDataField = "id";
        GridViewBudget.DataBind();
        if (dvBudget.Count > 0)
        {
            btDelBudget.Visible = true;
        }
        getSummarizeBudget();
    }
    private void ClearBudget()
    {
        txtid.Text = "";
        hdfItemID.Value = "";
        txtListName.Text = "";
        ddlEntryCosts.SelectedIndex = 0;
        ddlBudgetType.SelectedIndex = 0;
        txtBudgetType.Text = "";
        txtP.Text = "";
        txtD.Text = "0";
        txtG.Text = "0";
        lblTotal.Text = "0.00";
        txtTotalBudgetTerm1.Text = "0";
        txtTotalBudgetTerm2.Text = "0";
    }
    protected void btDelBudget_Click(object sender, EventArgs e)
    {
        if (GridViewBudget.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["budget"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewBudget.SelectedItems) + ")");
        foreach (DataRow dr in dra)
        {
            dr.Delete();
            dt1.AcceptChanges();

        }
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            dt1.Rows[i]["id"] = i;
        }
        dvBudget = dt1.DefaultView;
        Session["budget"] = dt1;
        GridViewBudget.DataSource = dvBudget;
        GridViewBudget.DataBind();
        getSummarizeBudget();
        if (dvBudget.Count == 0)
        {
            btDelBudget.Visible = false;
            lblTotalAmount.Text = "0";
        }
    }
    public decimal GetTotalMoney(decimal Budget)
    {
        TotalMoney += Budget;
        return Budget;
    }
    public decimal GetSumTotalMoney()
    {
        lblTotalAmount.Text = TotalMoney.ToString("#,##0.00");
        return TotalMoney;
    }
    public decimal GetTotalBudgetTerm1(decimal Budget)
    {
        TotalBudgetTerm1 += Budget;
        return Budget;
    }
    public decimal GetSumTotalBudgetTerm1()
    {
        return TotalBudgetTerm1;
    }
    public decimal GetTotalBudgetTerm2(decimal Budget)
    {
        TotalBudgetTerm2 += Budget;
        return Budget;
    }
    public decimal GetSumTotalBudgetTerm2()
    {
        return TotalBudgetTerm2;
    }
    public decimal GetTotalMoneyOperation(decimal Budget)
    {
        TotalMoneyOperation += Budget;
        return Budget;
    }
    public decimal GetSumTotalMoneyOperation()
    {
        return TotalMoneyOperation;
    }
    public decimal GetTotalBudgetOther(decimal Budget)
    {
        TotalBudgetOther += Budget;
        return Budget;
    }
    public decimal GetSumTotalBudgetOther()
    {
        return TotalBudgetOther;
    }
    private void getSummarizeBudget()
    {
        if (Session["budget"] != null)
        {
            DataTable dt1, dt2, dt3;
            dt2 = new DataTable();
            dt1 = (DataTable)Session["budget"];
            
            dt2 = dt1.DefaultView.ToTable(true, "BudgetTypeCode", "BudgetTypeName");
            //dt2.Columns.Add("BudgetTypeName");
            dt2.Columns.Add("TotalMoney");
            
            foreach (DataRow dr in dt2.Rows)
                dr["TotalMoney"] = dt1.Compute("SUM(TotalMoney)", "BudgetTypeCode='" + dr["BudgetTypeCode"] + "'");

            if (dt2.Rows.Count != 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["BudgetTypeCode"].ToString() == "88f2efd0-b802-4528-8ca8-aae8d8352649")
                    {
                        dt2.Rows[i]["BudgetTypeName"] = "อื่น ๆ";
                    }
                }
            }
            dt3 = dt2.DefaultView.ToTable(true, "BudgetTypeCode", "BudgetTypeName", "TotalMoney");
            //Session["budget"] = dt1;
            rptBudget.DataSource = dt3;
            rptBudget.DataBind();
            dt3.Columns["BudgetTypeName"].ColumnName = "name";
            dt3.Columns["TotalMoney"].ColumnName = "data";
            dt3.AcceptChanges();
            ReportGraph("Pie2D", dt3.DefaultView, 1);
        }
        else
        {
            rptBudget.DataSource = null;
            rptBudget.DataBind();
        }
    }
    protected string checkapprove(string id, int ckOld)
    {
        string str = "<a href=\"javascript:;\" " + btc.getLinkReportWEP("W") +" onclick=\"printRpt(4,'w','{0}', " + ckOld + ");\"><img style=\"border: 0; cursor :pointer;\" title=\"เรียกดูใบขออนุมัติกิจกรรม แบบเอกสาร Word\" src=\"../Image/WordIcon.png\"</a>"
            + "<a href=\"javascript:;\" " + btc.getLinkReportWEP("E") + " onclick=\"printRpt(4,'e','{0}', " + ckOld + ");\"><img style=\"border: 0; cursor :pointer;\" title=\"เรียกดูใบขออนุมัติกิจกรรม แบบเอกสาร Excel\" src=\"../Image/Excel.png\"</a>"
            + "<a href=\"javascript:;\" " + btc.getLinkReportWEP("P") + " onclick=\"printRpt(4,'p','{0}', " + ckOld + ");\"><img style=\"border: 0; cursor :pointer;\" title=\"เรียกดูใบขออนุมัติกิจกรรม แบบเอกสาร PDF\" src=\"../Image/PdfIcon.png\"</a>";
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            return String.Format(str, id);
        }
        else
        {
            DataView dv = Conn.Select("Select ApproveFlag From Activity Where DelFlag = 0 And ActivityCode = '" + id + "' And ApproveFlag = 1");
            if (dv.Count != 0)
            {

                return String.Format(str, id);
            }
            else
            {
                return string.Format("");
            }
        }
    }
    public string GetBudget(decimal Budget, string ActivityCode)
    {
        string tooltrip = "";
        DataRow[] drBudgetType = dvBudgetType.Table.Select("ActivityCode = '" + ActivityCode + "' ");
        if (drBudgetType.Length > 0)
        {
            for (int i = 0; i < drBudgetType.Length; i++ )
                tooltrip += drBudgetType[i]["BudgetTypeName"].ToString() + " " + Convert.ToDecimal(drBudgetType[i]["TotalMoney"]).ToString("#,##0.00") + " บาท, ";
        }
        //TotalAmount += Budget;
        return "<span style=\"cursor:default;\" title=\"" + (!string.IsNullOrEmpty(tooltrip) ? tooltrip.Substring(0, tooltrip.Length - 2) : "ไม่มีงบประมาณ") + "\">" + Budget.ToString("#,##0.00") + "</span>";
    }
    public decimal GetTotalBudget()
    {
        return TotalAmount;
    }
    private void ModeCreate(Boolean enabled)
    {
        ddlCreateUser.Visible = enabled;
        lblResponsiblePosition.Visible = enabled;
        txtResponsiblePosition.Visible = enabled;
        btnEditCreate.Visible = enabled;
        btnCancelCreate.Visible = enabled;
    }
    protected void lbtEditCreate_Click(object sender, EventArgs e)
    {
        getddlEditCreate();
        ModeCreate(true);
    }
    private void getddlEditCreate()
    {
        string newuid = "";
        if (userid.Value != "")
        {
            string[] uID = userid.Value.Split(',');
            for (int j = 0; j < uID.Length; j++)
            {
                newuid += " '" + uID[j] + "' ";
                if (j != uID.Length - 1)
                {
                    newuid += ",";
                }
            }

            string strSql = "Select EmpID, EmpName From Employee Where EmpID In (" + newuid + ")";        
            DataView dv = Conn.Select(strSql);
            if (dv.Count != 0)
            {
                ddlCreateUser.DataSource = dv;
                ddlCreateUser.DataTextField = "EmpName";
                ddlCreateUser.DataValueField = "EmpID";
                ddlCreateUser.DataBind();
            }
        }
    }
    protected void btnCancelCreate_Click(object sender, EventArgs e)
    {
        ModeCreate(false);
    }
    protected void btnEditCreate_Click(object sender, EventArgs e)
    {
        Conn.Update("Activity", "Where ActivityCode = '" + Request.QueryString["id"] + "'", "CreateUser, DeptCode, ResponsiblePosition", ddlCreateUser.SelectedValue, btc.getDeptIDInActivityByUserId(ddlCreateUser.SelectedValue, Request["id"]), txtResponsiblePosition.Text);
        //GetData(Request.QueryString["id"]);
        ModeCreate(false);
    }
    protected string DateFormat(object startDate, object endDate)
    {
        return Convert.ToDateTime(startDate).ToString("dd/MM/yy") + " - " + Convert.ToDateTime(endDate).ToString("dd/MM/yy");
    }
    protected string GenConnection(int ConnectInd, int ConnectEva)
    { 
        Int32 ckConnect = 0;
        string Detail = "";
        if(ConnectInd > 0)
        {
            ckConnect += 1;
            Detail = "กำหนดตัวชี้วัดแล้ว";
        }
        else
        {
            Detail = "ยังไม่ได้กำหนดตัวชี้วัด";
        }
        if(ConnectEva > 0)
        {
            ckConnect += 1;
            if(Detail == "")
            {
                Detail = "เชื่อมโยงตัวบ่งชี้แล้ว";
            }
            else
            {
                Detail += ", เชื่อมโยงตัวบ่งชี้แล้ว";
            }
        }
        else
        {
            if(Detail == "")
            {
                Detail = "ยังไม่เชื่อมโยงตัวบ่งชี้";
            }
            else
            {
                Detail += ", ยังไม่เชื่อมโยงตัวบ่งชี้";
            }
        }

        if (ckConnect == 2)
        {
            return "<img style=\"border: none; \" title=\"กำหนดตัวชี้วัดและเชื่อมโยงตัวบ่งชี้สมบูรณ์แล้ว\" src='../Image/Connect.png' />";
        }
        else
        {
            //if (ckConnect == 1)
            //{
            //    return "<img style=\"border: none; \" title=\"" + Detail + "\" src='../Image/Connect1.png' />";
            //}
            //else
            //{
                return "<img style=\"border: none; \" title=\"" + Detail + "\" src='../Image/UnConnect.png' />";
            //}
        }       
    }
    //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    TotalAmount = 0;
    //    DataBind();
    //}
    protected void lblTarget1_TextChanged(object sender, EventArgs e)
    {
        UpdatelblTarget();
    }
    protected void lblTarget2_TextChanged(object sender, EventArgs e)
    {
        UpdatelblTarget();
    }
    private void UpdatelblTarget()
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "txtTarget1, txtTarget2", lblTarget1.Text, lblTarget2.Text);
    }
    protected void ddlCostsCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlCostsSubCenter(ddlCostsCenter.SelectedValue);
    }
    protected void btAddCostsCenter_Click(object sender, EventArgs e)
    {
        if (Session["CostsCenter"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("CostsCode");
            dt1.Columns.Add("CostsName");
            dt1.Columns.Add("CostsSubCode");
            dt1.Columns.Add("CostsSubName");
            dt1.Columns.Add("CostsWeight");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["CostsCode"] = ddlCostsCenter.SelectedValue;
            dr["CostsName"] = (ddlCostsCenter.SelectedIndex == 0) ? "" : ddlCostsCenter.SelectedItem.Text;
            dr["CostsSubCode"] = ddlCostsSubCenter.SelectedValue;
            dr["CostsSubName"] = (ddlCostsSubCenter.SelectedIndex == 0) ? "" : ddlCostsSubCenter.SelectedItem.Text;
            dr["CostsWeight"] = txtCostsWeight.Text;
            dt1.Rows.Add(dr);

            dvCostsCenter = dt1.DefaultView;
            Session["CostsCenter"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["CostsCenter"];

            //if (txtCostsCenterID.Text == "")
            //{
                DataView ckdv = ((DataTable)Session["CostsCenter"]).DefaultView;
                if (ckdv.Count != 0)
                {
                    for (int i = 0; i < ckdv.Count; i++)
                    {
                        if (ckdv[i]["CostsSubCode"].ToString() == ddlCostsSubCenter.SelectedValue)
                        {
                            lblckCostsCenter.Visible = true;
                            return;
                        }
                    }
                }

                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["CostsCode"] = ddlCostsCenter.SelectedValue;
                dr["CostsName"] = (ddlCostsCenter.SelectedIndex == 0) ? "" : ddlCostsCenter.SelectedItem.Text;
                dr["CostsSubCode"] = ddlCostsSubCenter.SelectedValue;
                dr["CostsSubName"] = (ddlCostsSubCenter.SelectedIndex == 0) ? "" : ddlCostsSubCenter.SelectedItem.Text;
                dr["CostsWeight"] = txtCostsWeight.Text;
                dt1.Rows.Add(dr);

            lblckCostsCenter.Visible = false;

            dvCostsCenter = dt1.DefaultView;
            Session["CostsCenter"] = dt1;
        }
        dvCostsCenter.Sort = "CostsSubName";
        ClearCostsCenter();
        dgCostsCenter.DataSource = dvCostsCenter;
        dgCostsCenter.CheckListDataField = "id";
        dgCostsCenter.DataBind();
        if (dvCostsCenter.Count > 0)
        {
            btDelCostsCenter.Visible = true;
        }
    }
    protected void btDelCostsCenter_Click(object sender, EventArgs e)
    {
        if (dgCostsCenter.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["CostsCenter"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", dgCostsCenter.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvCostsCenter = dt1.DefaultView;
        Session["CostsCenter"] = dt1;
        dgCostsCenter.DataSource = dvCostsCenter;
        dgCostsCenter.DataBind();
        if (dvCostsCenter.Count == 0)
        {
           btDelCostsCenter.Visible = false;
        }
    }
    private void ClearCostsCenter()
    {
        txtCostsCenterID.Text = "";
        ddlCostsCenter.SelectedIndex = 0;
        ddlCostsSubCenter.SelectedIndex = 0;
        txtCostsWeight.Text = "";
    }
    public decimal GetCostsWeight(decimal CostsWeight)
    {
        TotalCostsWeight += CostsWeight;
        return CostsWeight;
    }
    public decimal GetTotalCostsWeight()
    {
        txtTotalCostsWeight.Text = TotalCostsWeight.ToString();
        return TotalCostsWeight;
    }

    protected StringBuilder chartData;
    protected string chartType;

    void ReportGraph(string ChartName, DataView dv, int mode)
    {
        string chartData = "";
        if (mode == 1)
        {
            Suffix = "numberSuffix=''";
            chartData = GenerateChart(ChartName, dv, "", null, false, " ", "");
            graphPnl1.InnerHtml = Graph.FusionCharts.RenderChartHTML(this.ResolveUrl(string.Format("~/Charts/{0}.swf", ChartName)), "", chartData, "AA", "300", "150", false);
        }
    }

    private string DatapieChart = "";
    private Boolean QCbar = false;
    #region GenerateChart

    private string Suffix = null;
    private string MaxValue = null;
    private string xaxisname = "";
    private string yaxisname = "";
    private Boolean NameCountPerson = false;
    string rotateNames = "";
    private int showLimits = 1;
    private string showLabels = "1";
    private string showName = "1";

    private string showYAxisValues = "1";
    private string valueslabel = "";
    private string setplaceValuesInside = "0";
    private string setValueInsideLabel = "";
    private string labelDisplay = "";
    private string slantLabels = "";
    private string chartLeftMargin = "";
    private string chartRightMargin = "";
    private string maxLabelWidthPercent = "";
    private string formatNumberScale = "";
    private string GenerateChart(string ChartName, DataView dv, string column, string[] columns, bool compare, string caption, string subCaption)
    {
        string chartName = ChartName;
        if (chartName == "MSLine" || chartName == "MSColumn2D")
        {
            compare = true;
        }

        if (dv.Count.Equals(0)) return "";

        string baseFont = "Microsoft Sans Serif";
        string baseFontColor = "787878";
        string bgColor = "FFFFFF";
        string canvasBgColor = "FFFFFF";
        string outCnvBaseFont = "Tahoma";
        string outCnvBaseFontColor = "787878";
        rotateNames = "rotateNames='0'";
        string[] color = { "949494", "0cb1cd", "fe4d53", "afc000", "ff9e02", "F3EB1E", "FF7777", "FA33BB", "F3A01E", "238627", "78177E", "82B5D5", "7A4E28", "9D9F0C", "F97CC3", "A2919B", "ADFAF4" };
        int baseFontSize = 13;
        int outCnvBaseFontSize = 12;


        StringBuilder chartData = new StringBuilder();
        if (chartName == "ScrollColumn2D" || chartName == "ZoomLine")
        {
            chartData.Append(string.Format("<chart palette='2' showLimits='1' showValues='1' " + Suffix + " " + rotateNames + " caption='{0}' showvalues='0' useRoundEdges='1' legendBorderAlpha='0' subCaption='{1}' yaxisname='{2}' xaxisname='{3}' baseFontSize='{4}' outCnvBaseFontSize='{5}' baseFont='{6}' outCnvBaseFont='{7}' bgColor='{8}' canvasBgColor='{9}' showLimits='{10}' baseFontColor='{11}' outCnvBaseFontColor='{12}' shownames='{13}' labelDisplay='{14}' slantLabels='{15}' chartLeftMargin='{16}' chartRightMargin='{17}' showLabels = '{18}' maxLabelWidthPercent='{19}' formatNumberScale='{20}'>", caption, subCaption, yaxisname, xaxisname, baseFontSize, outCnvBaseFontSize, baseFont, outCnvBaseFont, bgColor, canvasBgColor, showLimits, baseFontColor, outCnvBaseFontColor, showName, labelDisplay, slantLabels, chartLeftMargin, chartRightMargin, showLabels, maxLabelWidthPercent, formatNumberScale));
            chartData.Append("<categories> ");
            if (!string.IsNullOrEmpty(column))//gen xml แบบคอลัม 0 record
            {
                foreach (string col in column.Split(','))
                {
                    chartData.Append(string.Format("<category label='{0}'  />", col));
                }
                chartData.Append("</categories>");

                chartData.Append("<dataset showValues='1'>");
                int i = 0;
                foreach (string col in column.Split(','))
                {
                    chartData.Append(string.Format("<set value='{0}' color='{1}'/>", dv[0][col], GetColor(color, i)));
                    i++;
                }
            }
            else//gen xml แบบ record 2 คอลัม
            {
                foreach (DataRowView dr in dv)
                {
                    chartData.Append(string.Format("<category label='{0}'  />", dr["Name"]));
                }
                chartData.Append("</categories>");

                chartData.Append("<dataset showValues='1'>");
                int i = 0;
                foreach (DataRowView dr in dv)
                {
                    chartData.Append(string.Format("<set value='{0}' color='{1}'/>", dr["Data"], GetColor(color, i)));
                    i++;
                }
            }
            chartData.Append("</dataset>");
            chartData.Append("</chart>");
        }
        else
        {
            chartData.Append(string.Format("<chart palette='2' decimalPrecision='0' " + Suffix + " " + MaxValue + " " + rotateNames + " showValues='1'  showPercentageValues='1' showPercentageInLabel ='1' animation='1'  caption='{0}' subCaption='{1}' yaxisname='{2}' xaxisname='{3}' baseFontSize='{4}' outCnvBaseFontSize='{5}' baseFont='{6}' outCnvBaseFont='{7}' bgColor='{8}' canvasBgColor='{9}' showLimits='{10}' baseFontColor='{11}' outCnvBaseFontColor='{12}' divLineIsDashed='1' placeValuesInside='{13}' showLabels='{14}' shownames='{15}' showYAxisValues='{16}' labelDisplay='{17}' slantLabels='{18}' maxLabelWidthPercent='{19}' formatNumberScale='{20}'>", caption, subCaption, yaxisname, xaxisname, baseFontSize, outCnvBaseFontSize, baseFont, outCnvBaseFont, bgColor, canvasBgColor, showLimits, baseFontColor, outCnvBaseFontColor, setplaceValuesInside, showLabels, showName, showYAxisValues, labelDisplay, slantLabels, maxLabelWidthPercent, formatNumberScale));

            if (compare)//gen xml แบบ เปรียบเทียบ
            {
                chartData.Append("<categories font='Arial' fontSize='11' fontColor='000000'>");

                //-------========
                int i = 0;
                if (!string.IsNullOrEmpty(column))//gen xml แบบคอลัม 0 record
                {
                    foreach (string col in column.Split(','))
                    {
                        chartData.Append(string.Format("<category name='{0}' />", col));
                    }
                    chartData.Append("</categories>");

                    foreach (string col in columns)
                    {
                        string[] value = col.Split('=');
                        chartData.Append(string.Format("<dataset seriesname='{0}' color='{1}' alpha='70'>", value[0], GetColor(color, i)));
                        string[] v = value[1].Split(',');
                        int c = 0;
                        foreach (string col1 in column.Split(','))
                        {
                            chartData.Append(string.Format("<set value='{0}' />", dv[0][v[c]]));
                            c++;
                        }
                        chartData.Append("</dataset>");
                        i++;
                    }
                }
                else//gen xml แบบ record
                {
                    foreach (DataRowView dr in dv)
                    {
                        string value = dr["Name"].ToString();
                        if (value.Length > 100)
                        {

                        }
                        chartData.Append(string.Format("<category name='{0}' />", value));
                    }
                    chartData.Append("</categories>");

                    foreach (string col in columns)
                    {
                        string[] value = col.Split('=');
                        bool IsTooltip = false;
                        if (dv.Table.Columns.Contains("Tooltip")) IsTooltip = true;
                        chartData.Append(string.Format("<dataset seriesname='{0}' color='{1}' alpha='70'>", value[0], GetColor(color, i)));
                        foreach (DataRowView dr in dv)
                        {
                            if (IsTooltip)
                                chartData.Append(string.Format("<set value='{0}' tooltext='{1}'/>", dr[value[1]], value[0] + ", " + dr["Tooltip"].ToString() + ", " + dr[value[1]]));
                            else
                                chartData.Append(string.Format("<set value='{0}'/>", dr[value[1]]));
                        }
                        chartData.Append("</dataset>");
                        i++;
                    }
                }
            }
            else
            {
                int i = 0;
                if (!string.IsNullOrEmpty(column))//gen xml แบบคอลัม 0 record
                {
                    foreach (string col in column.Split(','))
                    {
                        chartData.Append(string.Format("<set label='{0}' value='{1}' color='{2}' />", col, dv[0][col], GetColor(color, i)));
                        i++;
                    }
                }
                else//gen xml แบบ record 2 คอลัม
                {
                    if (NameCountPerson == true)
                    {
                        if (QCbar == true)
                        {
                            foreach (DataRowView dr in dv)
                            {
                                chartData.Append(string.Format("<set label='{0} {3}' value='{1}' color='{2}' />", dr["Name"], dr["Data"], GetColor(color, i), dr["DepName"]));
                                i++;
                            }
                            QCbar = false;
                            NameCountPerson = false;
                        }
                        else
                        {
                            foreach (DataRowView dr in dv)
                            {
                                chartData.Append(string.Format("<set label='{0}' value='{1}' color='{2}' />", dr["Name"], dr[DatapieChart], GetColor(color, i), valueslabel));
                                NameCountPerson = false;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRowView dr in dv)
                        {
                            chartData.Append(string.Format("<set label='{0}' value='{1}' color='{2}' />", dr["Name"].ToString(), dr["Data"], GetColor(color, i)));
                            NameCountPerson = false;
                            i++;
                        }
                    }

                }
            }
            chartData.Append("</chart >");
        }
        return chartData.ToString();
    }

    private string GetColor(string[] color, int number)
    {
        string colorCode;
        if (number < color.Length)
        {
            colorCode = color[number];
        }
        else
        {
            colorCode = color[number % color.Length];
        }
        return colorCode;
    }
    #endregion


    private void ckYearType()
    {
        if (!btc.ckIdentityName("ckBudgetYear"))  //ปีการศึกษา
        {
            lblYear2.InnerHtml = "ปีงบประมาณ : ";
            if (ddlTerm.SelectedValue == "1")
            {
                ddlBudgetYear.SelectedValue = ddlYearB.SelectedValue;
                txtYearB.Text = ddlYearB.SelectedValue;
            }
            if (ddlTerm.SelectedValue == "2")
            {
                ddlBudgetYear.SelectedValue = (Convert.ToInt32(ddlYearB.SelectedValue) + 1).ToString();
                txtYearB.Text = ddlYearB.SelectedValue;
            }
            if (ddlTerm.SelectedValue == "1-2")
            {
                ddlBudgetYear.SelectedValue = ddlYearB.SelectedValue;
                txtYearB.Text = ddlYearB.SelectedValue;
            }
        }
        else  //ปีงบประมาณ
        {
            lblYear2.InnerHtml = "ปีการศึกษา : ";
            if (ddlTerm.SelectedValue == "1")
            {
                ddlBudgetYear.SelectedValue = ddlYearB.SelectedValue;
                txtYearB.Text = ddlYearB.SelectedValue;
            }
            if (ddlTerm.SelectedValue == "2")
            {
                ddlBudgetYear.SelectedValue = (Convert.ToInt32(ddlYearB.SelectedValue) - 1).ToString();
                txtYearB.Text = (Convert.ToInt32(ddlYearB.SelectedValue) - 1).ToString();
            }
            if (ddlTerm.SelectedValue == "2-1")
            {
                ddlBudgetYear.SelectedValue = (Convert.ToInt32(ddlYearB.SelectedValue) - 1).ToString();
                txtYearB.Text = (Convert.ToInt32(ddlYearB.SelectedValue) - 1).ToString();
            }
            //getddlStrategies(1, ddlYearB.SelectedValue);
            //getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
        }
    }
    protected void rbtlYearType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtlYearType.SelectedIndex == 0)  //ปีการศึกษา
        {
            btc.getddlTerm(rbtlYearType.SelectedIndex, ddlTerm);
        }
        else  //ปีงบประมาณ
        {
            btc.getddlTerm(rbtlYearType.SelectedIndex, ddlTerm);
        }
        ckYearType();
    }
    protected void ddlYearB_OnSelectedChanged(object sender, EventArgs e)
    {
        ckYearType();
        getddlStrategies(1, ddlYearB.SelectedValue);
        getddlProjects(1, ddlYearB.SelectedValue, ddlStrategies.SelectedValue);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
    }
    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        ckYearType();
    }
    protected void ddlBudgetYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ckYearType();
    }
    protected string getActivityStatus(string ActivityStatus)
    {
        return btc.getSpanColorStatus(Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")), ActivityStatus);
    }
    protected void ddlSearchTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("AcTerm", ddlSearchTerm.SelectedValue);
        DataBind();
    }
    protected void btaddAssessment_Click(object sender, EventArgs e)
    {
        if (Session["Assessment"] == null)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("id");
            dt1.Columns.Add("IndicatorsName");
            dt1.Columns.Add("MethodAss");
            dt1.Columns.Add("ToolsAss");

            DataRow dr;
            dr = dt1.NewRow();
            dr["id"] = dr.Table.Rows.Count;
            dr["IndicatorsName"] = txtIndicatorsName.Text;
            dr["MethodAss"] = txtMethodAss.Text;
            dr["ToolsAss"] = txtToolsAss.Text;
            dt1.Rows.Add(dr);

            dvAssessment = dt1.DefaultView;
            Session["Assessment"] = dt1;
        }
        else
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["Assessment"];

            if (txtid2.Text == "")
            {
                DataView ckdv = ((DataTable)Session["Assessment"]).DefaultView;
                DataRow dr;
                dr = dt1.NewRow();
                dr["id"] = dr.Table.Rows.Count;
                dr["IndicatorsName"] = txtIndicatorsName.Text;
                dr["MethodAss"] = txtMethodAss.Text;
                dr["ToolsAss"] = txtToolsAss.Text;
                dt1.Rows.Add(dr);
            }
            else
            {
                DataView ckdv = ((DataTable)Session["Assessment"]).DefaultView;
                Int32 i = Convert.ToInt32(txtid2.Text);
                dt1.Rows[i]["IndicatorsName"] = txtIndicatorsName.Text;
                dt1.Rows[i]["MethodAss"] = txtMethodAss.Text;
                dt1.Rows[i]["ToolsAss"] = txtToolsAss.Text;
            }

            dvAssessment = dt1.DefaultView;
            Session["Assessment"] = dt1;
        }
        //dvBudget.Sort = "YearE DESC";
        ClearAssessment();
        GridViewAssessment.DataSource = dvAssessment;
        GridViewAssessment.CheckListDataField = "id";
        GridViewAssessment.DataBind();
        if (dvAssessment.Count > 0)
        {
            btDelAssessment.Visible = true;
        }
    }
    private void ClearAssessment()
    {
        txtid2.Text = "";
        txtIndicatorsName.Text = "";
        txtMethodAss.Text = "";
        txtToolsAss.Text = "";
    }
    protected void btDelAssessment_Click(object sender, EventArgs e)
    {
        if (GridViewAssessment.SelectedItems.Length == 0) return;
        DataTable dt1 = new DataTable();
        dt1 = (DataTable)Session["Assessment"];
        DataRow[] dra = dt1.Select("id in (" + string.Join(",", GridViewAssessment.SelectedItems) + ")");
        foreach (DataRow dr in dra)
            dr.Delete();
        dt1.AcceptChanges();
        dvAssessment = dt1.DefaultView;
        if (dvAssessment.Count != 0)
        {
            Session["Assessment"] = dt1;
        }
        else
        {
            Session.Remove("Assessment");
        }
        GridViewAssessment.DataSource = dvAssessment;
        GridViewAssessment.DataBind();
        if (dvAssessment.Count == 0)
        {
            btDelAssessment.Visible = false;
        }
    }
    protected void rbtlSuanIdentityName_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblSuanIdentitySideName.ClearSelection();
        if (rbtlSuanIdentityName.SelectedIndex == 0)
        {
            cblSuanIdentitySideName.Enabled = true;
        }
        else
        {
            cblSuanIdentitySideName.Enabled = false;
        }
    }
    protected void ImgBtUnApprove_Click(object sender, ImageClickEventArgs e)
    {
       Int32 result =  Conn.Update("Activity", "Where ActivityCode = '" + Request["id"] + "'", "ApproveFlag, ApproveUser", "0", DBNull.Value);
       if (result > 0)
       {
           lblAlertApprove.Visible = false;
           btApproveBudget.Visible = true;
           ImgBtUnApprove.Visible = false;
       }
    }

    protected void txtKeyWordResponsibleActivity_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordResponsibleActivity", txtKeyWordResponsibleActivity.Text);
    }
    protected void txtKeyWordProposeActivity_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordProposeActivity", txtKeyWordProposeActivity.Text);
    }
    protected void txtKeyWordApprovalActivity_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordApprovalActivity", txtKeyWordApprovalActivity.Text);
    }
    protected void txtKeyWordApproval2Activity_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordApproval2Activity", txtKeyWordApproval2Activity.Text);
    }

    protected void btUpload_Click(object sender, EventArgs e)
    {
        Upload();
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "function loadDept(){" + Page.ClientScript.GetPostBackEventReference(btSearchDept, null, false) + ";}", true);
    }
    private void Upload()
    {
        try
        {
            string path = ipFile.FileName; //Request.Files[0].FileName;
            path = string.Format("~/Uploads/{0}", Path.GetFileName(path));
            path = Server.MapPath(path);

            string[] CkSprit;
            CkSprit = Path.GetFileName(path).Split('.');
            if ((CkSprit[1] == "xls") || (CkSprit[1] == "xlsx"))
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                Request.Files[0].SaveAs(path);
                Session["ExcelName"] = Path.GetFileName(path);
                Session["budget"] = getDataByWorksheet(Session["ExcelName"].ToString(), CkSprit[1]);
                getUpload();
                getSummarizeBudget();
            }
            else
            {
                Msg_Head(true, "9", 0, "");
            }
        }
        catch (Exception ex)
        { }
    }

    private void getUpload()
    {
        GridViewBudget.DataSource = Session["budget"];
        GridViewBudget.DataBind();
        lblSearchTotal.InnerHtml = GridView1.Rows.Count.ToString();
    }
    private DataTable getDataByWorksheet(string worksSheetNames, string versionExcel)
    {
        ds = new DataSet();

        string strConn = "";
        if (versionExcel == "xls")
        {
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Uploads/") + worksSheetNames + @";Extended Properties=""Excel 8.0;""";
        }
        else
        {
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Uploads/") + worksSheetNames + @";Extended Properties=""Excel 8.0;HDR=Yes""";
        }
        try
        {
            exConn = new OleDbConnection(strConn.ToString());
            exConn.Open();
            string strSql = " Select 0 As id, '''' As ItemID, รายการ As EntryCostsName, หมวดค่าใช้จ่าย As txtCostsName, '''' As EntryCostsCode, ประเภทเงิน As BudgetTypeName, '''' As BudgetTypeCode, หน่วย As TotalP, จำนวน As TotalD, ราคาหน่วย As TotalG, 0 As TotalMoney, 0 As TotalBudgetTerm1, 0 As TotalBudgetTerm2 From [Sheet1$] ";
            OleDbDataAdapter da = new OleDbDataAdapter(strSql, exConn);
            da.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dvCkEntryCosts = Conn.Select("Select EntryCostsCode, EntryCostsName From EntryCosts Where DelFlag = 0");
                DataView dvCkBudgetType = Conn.Select("Select BudgetTypeCode, BudgetTypeName From BudgetType Where DelFlag = 0");

                for (Int32 i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow[] drEntryCosts = dvCkEntryCosts.Table.Select("EntryCostsName = '" + ds.Tables[0].Rows[i]["txtCostsName"] + "'");
                    if (drEntryCosts.Length > 0)
                    {
                        ds.Tables[0].Rows[i]["EntryCostsCode"] = drEntryCosts[0]["EntryCostsCode"].ToString();
                    }

                    DataRow[] drBudgetType = dvCkBudgetType.Table.Select("BudgetTypeName = '" + ds.Tables[0].Rows[i]["BudgetTypeName"] + "'");
                    if (drBudgetType.Length > 0)
                    {
                        ds.Tables[0].Rows[i]["BudgetTypeCode"] = drBudgetType[0]["BudgetTypeCode"].ToString();
                    }
                    ds.Tables[0].Rows[i]["id"] = i;
                    ds.Tables[0].Rows[i]["TotalMoney"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalD"]) * Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalG"]);
                    ds.Tables[0].Rows[i]["ItemID"] = Guid.NewGuid().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            MsgHead.Text = ex.ToString();
            MsgHead.Visible = true;
        }
        finally
        {
            if (exConn != null)
            {
                exConn.Close();
                exConn.Dispose();
            }
        }
        //เช็คมีข้อมูลหรือไม่
        if (ds.Tables.Count > 0)
        {
            Msg_Head(true, "1", 1, "");
            return ds.Tables[0];
        }
        else
        {
            Msg_Head(true, "1", 0, "");
            DelFile(worksSheetNames);
            return null;
        }
    }
    private void DelFile(string worksSheetNames)
    {
        //ลบไฟล์
        string x = Path.GetFileName(worksSheetNames);
        x = Server.MapPath("~/Uploads/" + x);
        if (File.Exists(x))
        {
            File.Delete(x);
        }
    }
    private void Msg_Head(Boolean Enables, String mode, Int32 i, string Msg)
    {
        if (i > 0)
        {
            switch (mode)
            {
                case "1":
                    MsgHead.Text = "เรียบร้อย ! <BR> <small>Upload File เรียบร้อยแล้ว</small>";
                    break;
                case "2":
                    MsgHead.Text = "เรียบร้อย ! <BR> <small>บันทึกข้อมูลเรียบร้อยแล้ว</small>";
                    break;
                case "3":
                    MsgHead.Text = "เรียบร้อย ! <BR> <small>ลบข้อมูลเรียบร้อยแล้ว</small>";
                    break;
                case "4":
                    MsgHead.Text = "";
                    break;
            }
            Img1.ImageUrl = "~/Image/msg_check.gif";
            MsgHead.CssClass = "headMsg";
        }
        else
        {
            switch (mode)
            {
                case "1":
                    MsgHead.Text = "ผิดพลาด ! <BR> <small>ไม่สามารถ Upload File ได้</small>";
                    break;
                case "2":
                    MsgHead.Text = "ผิดพลาด ! <BR> <small>ไม่สามารถแก้ไขข้อมูลได้</small>";
                    break;
                case "3":
                    MsgHead.Text = "ผิดพลาด ! <BR> <small>ไม่สามารถลบข้อมูลได้  เนื่องจากข้อมูลถูกใช้อยู่</small>";
                    break;
                case "4":
                    MsgHead.Text = "ผิดพลาด ! <BR> <small>ไม่สามารถเพิ่มข้อมูลได้</small>";
                    break;
                case "9":
                    MsgHead.Text = "ผิดพลาด ! <BR> <small>ต้องเป็น File Excel .xls เท่านั้น</small>";
                    break;
            }
            Img1.ImageUrl = "~/Image/msg_error.gif";
            MsgHead.CssClass = "headMsgError";
        }
        Img1.Visible = Enables;
        MsgHead.Visible = Enables;
    }

    private void ClealAll()
    {
        GridViewBudget.DataSource = null;
        GridViewBudget.DataBind();
    }
}
