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

public partial class ActivityDetail : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    decimal TotalAmount;
    decimal TotalOldAmount;
    decimal TotalMoney;
    decimal ttlMoney;
    DataView dvDeputyDirector = null;

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
            getActivityName(Request.QueryString["acid"]);
            btc.getddlDay(ddlSDay);
            btc.getddlMonth2(ddlSMonth);
            btc.getddlYear(ddlSYear, 5);
            btc.getddlDay(ddlSDay2);
            btc.getddlMonth2(ddlSMonth2);
            btc.getddlYear(ddlSYear2, 5);
            txtSDay.Text = DateTime.Now.ToShortDateString();
            txtSDay2.Text = DateTime.Now.ToShortDateString();

            Cookie.SetValue2("ckActivityStatus", btc.ckIdentityName("ckActivityStatus")); //เช็คโหมดติดตามงาน

            if (btc.CkStatusActivitySuccess(Request["acid"]))
            {
                btSave.Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
            }
            if (btc.ckGetAdmission(CurrentUser.UserRoleID) != 1)
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
            }
            string mode = Request["mode"];
            int ij = string.IsNullOrEmpty(Request.QueryString["i"]) ? 0 : Convert.ToInt32(Request.QueryString["i"]);
            btc.getddlActivityStatus(ddlActivityStatus);

            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getDataActivity(Request.QueryString["acid"]);
                        getSort(Request.QueryString["acid"]);
                        hdfID.Value = Guid.NewGuid().ToString();
                        getlblKeyWord();
                        getddlDeputyDirectorName();
                        //txtResponsibleName.Text = lblEmp.Text;
                        txtPositionResponsible.Text = "ผู้รับผิดชอบกิจกรรม";
                        txtPositionHeadGroup.Text = "หัวหน้ากลุ่มงาน";
                        txtPositionHeadGroupSara.Text = "หัวหน้างาน/กลุ่มสาระฯ";
                        txtPositionUnderManagerName.Text = "รองผู้อำนวยการกลุ่ม";
                        if (Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")))
                        {
                            DivActivityStatus.Visible = true;
                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "Cktxt(0);", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        getDataActivity(Request.QueryString["acid"]);
                        hdfID.Value = Request.QueryString["id"];
                        txtWithdrow.ReadOnly = true;
                        getlblKeyWord();
                        getddlDeputyDirectorName();
                        GetData(hdfID.Value);
                        GetDataAttach(hdfID.Value);
                        break;
                    case "3":
                        MultiView1.ActiveViewIndex = 0;
                        Delete(Request["id"]);
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
        }
        txtActivityDetail.Attributes.Add("onkeyup", "Cktxt(0);");
        txtWithdrow.Attributes.Add("onkeyup", "Cktxt(0);");
        ddlSDay.Attributes.Add("onchange", "ckddlDate();");
        ddlSMonth.Attributes.Add("onchange", "ckddlDate();");
        ddlSYear.Attributes.Add("onchange", "ckddlDate();");
        ddlSDay2.Attributes.Add("onchange", "ckddlDate2();");
        ddlSMonth2.Attributes.Add("onchange", "ckddlDate2();");
        ddlSYear2.Attributes.Add("onchange", "ckddlDate2();");
    }
    private void getddlDeputyDirectorName()
    {
        string strSql = "Select DeputyDirectorID, DeputyDirectorName, DeputyDirectorPosition, Sort From DeputyDirector Where SchoolID = '{0}'";
        dvDeputyDirector = Conn.Select(string.Format(strSql, CurrentUser.SchoolID));
        ddlDeputyDirectorName.DataSource = dvDeputyDirector;
        ddlDeputyDirectorName.DataTextField = "DeputyDirectorName";
        ddlDeputyDirectorName.DataValueField = "DeputyDirectorID";
        ddlDeputyDirectorName.DataBind();
        ddlDeputyDirectorName.Items.Insert(0, new ListItem("- เลือก -", ""));
        ddlDeputyDirectorName.SelectedIndex = 0;
    }
    private void getlblKeyWord()
    {
        string[] lblKeyWord = btc.getKeyWordResponsibleActivity().Split(',');
        txtKeyWordDeputyDirector.Text = lblKeyWord[4];
    }
    private void getSort(string id)
    {
        string strSql = "Select Count(ActivityCode) + 1 Sort From ActivityDetail Where DelFlag = 0 And ActivityCode = '" + id + "'";
        DataView dv = Conn.Select(strSql);
        txtWithdrow.Text = Convert.ToInt32(dv[0]["Sort"]).ToString("#,##0");
    }
    private void getActivityName(string id)
    {
        string strSql = " Select ActivityName, TotalAmount From Activity "
                        + " Where ActivityCode = '" + id + "' ";
        DataView dv = Conn.Select(strSql);
        lalHead.Text = "รายละเอียดการทำกิจกรรม" + dv[0]["ActivityName"].ToString();
        lblActivityName.Text = dv[0]["ActivityName"].ToString();
        txtActivityCode.Text = id;

        lblOBudget.Text = Convert.ToDecimal(dv[0]["TotalAmount"]).ToString("#,##0.00");
        SumBalance();
    }
    private void getDataActivity(string id)
    {
        string strSql = " Select a.ActivityCode, a.ActivityName, a.Place, b.ProjectsName, a.TotalAmount, "
                        + " CostsType = Case a.CostsType When 0 Then 'งานกิจกรรม' When 1 Then 'งานประจำ' End From Activity a, Projects b "
                        + " Where a.ProjectsCode = b.ProjectsCode And a.ActivityCode = '" + id + "' ";
        DataView dv = Conn.Select(strSql);    
        lblProjects.Text = dv[0]["ProjectsName"].ToString();
        txtActivity.Text = dv[0]["ActivityName"].ToString();
        lblType.Text = dv[0]["CostsType"].ToString();
        lblPlace.Text = dv[0]["Place"].ToString();
        lblDept.Text = btc.getAcDeptName(id);
        lblEmp.Text = btc.getAcEmpName(id);
        getddlResponsibleName();
        if (Request.QueryString["mode"] == "1")
        {
            getBudget(dv[0]["ActivityCode"].ToString());
        }
    }
    private void getddlResponsibleName()
    {
        if (!string.IsNullOrEmpty(lblEmp.Text))
        {
            string[] ResponsibleName = lblEmp.Text.Split(',');
            if (ResponsibleName.Length > 0)
            {
                for (int i = 0; i < ResponsibleName.Length; i++)
                {
                    ddlResponsibleName.Items.Insert(i, new ListItem(ResponsibleName[i], ResponsibleName[i]));
                }
            }
            else
            {
                ddlResponsibleName.Items.Insert(0, new ListItem("- ไม่มีผู้รับผิดชอบ -", ""));
            }
        }
        else
        {
            ddlResponsibleName.Items.Insert(0, new ListItem("- ไม่มีผู้รับผิดชอบ -", ""));
        }
    }
    public override void DataBind()
    {
        string StrSql = @" Select a.ActivityDetailCode, a.ActivityDetail, a.TDay, a.ActivityCode, a.Sort, b.TotalAmount As OTotalAmount, IsNull(a.Confirm, 0) Confirm,
                IsNull(Sum(d.TotalMoney), 0) NAmount, IsNull(b.ActivityStatus, 0) As ActivityStatus, IsNull(a.ActivityStatus, 0) As ActivityDetailStatus 
                From ActivityDetail a Left Join Activity b On a.ActivityCode = b.ActivityCode
                Left Join Projects c On b.ProjectsCode = c.ProjectsCode
                Left Join ActivityCostsDetail d On a.ActivityDetailCode = d.ActivityDetailCode
                Where a.DelFlag = 0 And b.DelFlag = 0 And c.DelFlag = 0 And a.ActivityCode = '" + Request["acid"] + "'";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And a.ActivityDetail Like '%" + txtSearch.Text + "%' ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Group By a.ActivityDetailCode, a.ActivityDetail, a.TDay, a.ActivityCode, a.Sort, b.TotalAmount, a.ActivityStatus, b.ActivityStatus, a.Confirm Order By a.Sort Desc"));
        decimal NTotalAmount = 0;
        if (dv.Count != 0)
        {
            for (int i = 0; i < dv.Count; i++)
            {
                NTotalAmount += Convert.ToDecimal(dv[i]["NAMount"]);
            }
            lblNBudget.Text = NTotalAmount.ToString("#,##0.00");
            lblActivityStatus.Text = btc.getSpanColorStatus(Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")), dv[0]["ActivityStatus"].ToString());
            SumBalance();
        }

        GridView1.DataSource = dv;
        lblSearchTotal.InnerText = dv.Count.ToString();
        GridView1.DataBind();
    }
    private void SumBalance()
    {
        lblBalance.Text = (Convert.ToDecimal(lblOBudget.Text) - Convert.ToDecimal(lblNBudget.Text)).ToString("#,##0.00");
        if (Convert.ToDecimal(lblBalance.Text) == 0)
        {
            lblBalance.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            if (Convert.ToDecimal(lblBalance.Text) > 0)
            {
                lblBalance.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblBalance.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        string strSql = @" Select AD.*, IsNull(A.ActivityStatus, 0) ActivityStatus 
                    From ActivityDetail AD, Activity A
                    Where AD.ActivityCode = A.ActivityCode And AD.ActivityDetailCode = '{0}' ";
        DataView dv = Conn.Select(string.Format(strSql, id));

        if (dv.Count != 0)
        {
            txtObjective.Text = dv[0]["Objective"].ToString();
            txtActivityDetail.Text = dv[0]["ActivityDetail"].ToString();
            txtSDay.Text = Convert.ToDateTime(dv[0]["TDay"]).ToShortDateString();
            ddlSDay.SelectedValue = Convert.ToDateTime(dv[0]["TDay"]).Day.ToString("00");
            ddlSMonth.SelectedValue = Convert.ToDateTime(dv[0]["TDay"]).Month.ToString("00");
            ddlSYear.SelectedValue = (Convert.ToDateTime(dv[0]["TDay"]).Year + 543).ToString();
            if (!string.IsNullOrEmpty(dv[0]["TDay2"].ToString()))
            {
                txtSDay2.Text = Convert.ToDateTime(dv[0]["TDay2"]).ToShortDateString();
                ddlSDay2.SelectedValue = Convert.ToDateTime(dv[0]["TDay2"]).Day.ToString("00");
                ddlSMonth2.SelectedValue = Convert.ToDateTime(dv[0]["TDay2"]).Month.ToString("00");
                ddlSYear2.SelectedValue = (Convert.ToDateTime(dv[0]["TDay2"]).Year + 543).ToString();
            }
            else {
                txtSDay2.Text = Convert.ToDateTime(dv[0]["TDay"]).ToShortDateString();
                ddlSDay2.SelectedValue = Convert.ToDateTime(dv[0]["TDay"]).Day.ToString("00");
                ddlSMonth2.SelectedValue = Convert.ToDateTime(dv[0]["TDay"]).Month.ToString("00");
                ddlSYear2.SelectedValue = (Convert.ToDateTime(dv[0]["TDay"]).Year + 543).ToString();
            }
            txtWithdrow.Text = dv[0]["Sort"].ToString();
            //txtResponsibleName.Text = dv[0]["ResponsibleName"].ToString();
            ddlResponsibleName.SelectedValue = dv[0]["ResponsibleName"].ToString();
            txtPositionResponsible.Text = dv[0]["PositionResponsible"].ToString();
            txtHeadGroupName.Text = dv[0]["HeadGroupName"].ToString();
            txtPositionHeadGroup.Text = dv[0]["PositionHeadGroup"].ToString();
            txtHeadGroupSaraName.Text = dv[0]["HeadGroupSaraName"].ToString();
            txtPositionHeadGroupSara.Text = dv[0]["PositionHeadGroupSara"].ToString();
            txtUnderManagerName.Text = dv[0]["UnderManagerName"].ToString();
            txtPositionUnderManagerName.Text = dv[0]["PositionUnderManagerName"].ToString();
            txtSuppliesName.Text = dv[0]["SuppliesName"].ToString();
            txtPositionSupplies.Text = dv[0]["PositionSupplies"].ToString();
            ddlDeputyDirectorName.SelectedValue = dv[0]["DeputyDirectorID"].ToString();
            lblPositionDeputyDirector.Text = dv[0]["DeputyDirectorPosition"].ToString();
            btc.getCreateUpdateUser(lblCreate, lblUpdate, "ActivityDetail", "ActivityDetailCode", id);
            getBudgetEdit(dv[0]["ActivityDetailCode"].ToString());

            if (Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")))
            {
                DivActivityStatus.Visible = true;
                ddlActivityStatus.SelectedValue = dv[0]["ActivityStatus"].ToString();
            }
        }
        if (Convert.ToInt32(dv[0]["Confirm"]) == 1)
        {
            if (btc.ckGetAdmission(CurrentUser.UserRoleID) != 1)
            {
                btSave.Visible = false;
            }
            lblConfirm.Visible = true;
        }
    }
    private void getBudget(string ActivityCode)
    {
        string strSQL = @" Select a.ActivityCode, a.ListName, a.ListName As EntryCostsCode, a.BudgetTypeCode, 
                a.TotalP, IsNull(a.TotalD, 0) As TotalD, Isnull(a.TotalG, 0) As TotalG, a.TotalMoney, a.TotalMoney2,  
                BudgetTypeName = Case a.BudgetTypeCode When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then a.BudgetTypeOtherName Else b.BudgetTypeName End, 
                a.ListName As EntryCostsName, ValueT = Case IsNull(a.TotalMoney2,0) When 0 Then 0 Else a.TotalMoney2 End, 0 AS TotalDD,
                '' As TotalMoney2Ag, 0.0 As OldTotalAmount, a.ItemID 
                From CostsDetail a, BudgetType b 
                Where a.BudgetTypeCode = b.BudgetTypeCode And a.ActivityCode = '{0}' ";
            DataView dv1 = Conn.Select(string.Format(strSQL + " Order By a.ListNo, b.Sort ", ActivityCode));

            if (dv1.Count != 0)
            {
                //if (string.IsNullOrEmpty(dv1[0]["ItemID"].ToString()))
                //{
                //    hdfckItemID.Value = " And a.ListName = e.EntryCostsCode ";
                //}
                //else
                //{
                //    hdfckItemID.Value = " And a.ItemID = e.ItemID ";
                //}
                for (int j = 0; j < dv1.Count; j++)
                {
                    dv1[j]["TotalMoney2Ag"] = Convert.ToDecimal(dv1[j]["ValueT"]).ToString("#,##0");

                    strSQL = " Select a.ActivityCode, a.ListName As EntryCostsCode, a.BudgetTypeCode, a.BudgetTypeOtherName, a.TotalP, a.TotalD, a.TotalG, "
                        + " a.TotalMoney, a.TotalMoney2, BudgetTypeName = Case a.BudgetTypeCode When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then a.BudgetTypeOtherName Else b.BudgetTypeName End, "
                        + " a.ListName As EntryCostsName, IsNull(Sum(e.TotalMoney),0) ValueT, '' As TotalMoney2Ag, b.Sort "
                        + " From CostsDetail a, BudgetType b, ActivityDetail d, ActivityCostsDetail e "
                        + " Where a.BudgetTypeCode = b.BudgetTypeCode And a.ActivityCode = d.ActivityCode "
                        + " And d.ActivityDetailCode = e.ActivityDetailCode And a.ItemID = e.ItemID "
                        + " And a.ActivityCode = '{0}' And a.ItemID = '{1}' "
                        + " Group By a.ActivityCode, a.EntryCostsCode, a.BudgetTypeCode, a.BudgetTypeOtherName, "
                        + " a.TotalP, a.TotalD, a.TotalG, a.TotalMoney, a.TotalMoney2, a.BudgetTypeCode, BudgetTypeName, a.ListName, b.Sort ";
                    DataView dv9 = Conn.Select(string.Format(strSQL + " Order By b.Sort ", ActivityCode, dv1[j]["ItemID"].ToString()));
                    if (dv9.Count != 0)
                    {
                         dv1[j]["OldTotalAmount"] = Convert.ToDecimal(dv9[0]["ValueT"]);
                    }
                }
            }

            GridViewBudget.DataSource = dv1;
            GridViewBudget.DataBind();
            if (dv1.Count != 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
            }
    }
    private void getBudgetEdit(string ActivityDetailCode)
    {
        TotalOldAmount = 0;
        ttlMoney = 0;

        //string strSQL = @" Select ItemID From ActivityCostsDetail Where ActivityDetailCode = '{0}' ";
        //DataView ckDv = Conn.Select(string.Format(strSQL, ActivityDetailCode));
        //if (ckDv.Count != 0)
        //{
        //    if (string.IsNullOrEmpty(ckDv[0]["ItemID"].ToString()))
        //    {
        //        hdfckItemID.Value = " And a.ListName = e.EntryCostsCode ";
        //    }
        //    else
        //    {
        //        hdfckItemID.Value = " And a.ItemID = e.ItemID ";
        //    }
        //}

        string strSQL = @" Select a.ActivityCode, a.ListName, a.ListName As EntryCostsCode, a.BudgetTypeCode, IsNull(d.Sort, 0) As AcDSort, 
                a.TotalP, a.TotalD, IsNull(e.Cost, 0) AS TotalG, a.TotalMoney, a.TotalMoney2, 
                BudgetTypeName = Case a.BudgetTypeCode When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then a.BudgetTypeOtherName Else b.BudgetTypeName End, 
                a.ListName As EntryCostsName, IsNull(e.TotalMoney,0) ValueT, IsNull(e.TotalDD, 0) TotalDD, '' TotalMoney2Ag, 0.0 As OldTotalAmount, a.ItemID 
                From CostsDetail a, BudgetType b, ActivityDetail d, ActivityCostsDetail e 
                Where a.BudgetTypeCode = b.BudgetTypeCode And a.ActivityCode = d.ActivityCode And d.ActivityDetailCode = e.ActivityDetailCode 
                And a.ItemID = e.ItemID
                And e.ActivityDetailCode = '{0}' ";
        DataView dv1 = Conn.Select(string.Format(strSQL + " Order By a.ListNo, b.Sort ", ActivityDetailCode));

        if (dv1.Count != 0)
        {
            for (int j = 0; j < dv1.Count; j++)
            {
                dv1[j]["TotalMoney2Ag"] = Convert.ToDecimal(dv1[j]["ValueT"]).ToString("#,##0");

                strSQL = @" Select a.ActivityCode, a.ListName As EntryCostsCode, a.BudgetTypeCode, a.BudgetTypeOtherName, a.TotalP, a.TotalD, IsNull(e.Cost, 0) AS TotalG, 
                        a.TotalMoney, a.TotalMoney2, BudgetTypeName = Case a.BudgetTypeCode When '88f2efd0-b802-4528-8ca8-aae8d8352649' Then a.BudgetTypeOtherName Else b.BudgetTypeName End, 
                        a.ListName As EntryCostsName, IsNull(Sum(e.TotalMoney),0) ValueT, '' As TotalMoney2Ag, b.Sort 
                        From CostsDetail a, BudgetType b, ActivityDetail d, ActivityCostsDetail e 
                        Where a.BudgetTypeCode = b.BudgetTypeCode And a.ActivityCode = d.ActivityCode 
                        And d.ActivityDetailCode = e.ActivityDetailCode And a.ItemID = e.ItemID 
                        And a.ActivityCode = '{0}' And a.ItemID = '{1}' And d.Sort < {2}  
                        Group By a.ActivityCode, a.EntryCostsCode, a.BudgetTypeCode, a.BudgetTypeOtherName, 
                        a.TotalP, a.TotalD, e.Cost, a.TotalMoney, a.TotalMoney2, a.BudgetTypeCode, BudgetTypeName, a.ListName, b.Sort ";
                DataView dv9 = Conn.Select(string.Format(strSQL + " Order By b.Sort ", dv1[0]["ActivityCode"].ToString(), dv1[j]["ItemID"].ToString(), Convert.ToInt32(dv1[j]["AcDSort"])));
                if (dv9.Count != 0)
                {
                    dv1[j]["OldTotalAmount"] = Convert.ToDecimal(dv9[0]["ValueT"]);
                }
            }
        }

        GridViewBudget.DataSource = dv1;
        GridViewBudget.DataBind();
        if (dv1.Count != 0)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
        }
    }
    public decimal GetTotalAmount(decimal Budget)
    {
        TotalAmount += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount()
    {
        return TotalAmount;
    }
    public decimal GetTotalOldMoney(decimal Budget)
    {
        TotalOldAmount += Budget;
        return Budget;
    }
    public decimal GetSumTotalOldMoney()
    {
        return TotalOldAmount;
    }
    protected string DateFormat(object startDate)
    {
        return Convert.ToDateTime(startDate).ToString("dd/MM/yyyy");
    }
    protected string ActivityDetailName(object ActivityDetail)
    {
        return (ActivityDetail.ToString().Length > 50) ? ActivityDetail.ToString().Substring(0, 50) + "..." : ActivityDetail.ToString();
    }
    private void ClearAll()
    {
        hdfID.Value = "";
        txtObjective.Text = "";
        txtActivityDetail.Text = "";
        txtSearch.Text = "";
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    private void bt_Save()
    {
        Int32 i = 0;
        int result = 0;
        StringBuilder strbSql = new StringBuilder();
        string DeputyDirectorName = "";
        string DeputyDirectorPosition = "";
        if (ddlDeputyDirectorName.SelectedIndex != 0)
        {
            DeputyDirectorName = ddlDeputyDirectorName.SelectedItem.Text;
            DeputyDirectorPosition = lblPositionDeputyDirector.Text;
        }

        if (String.IsNullOrEmpty(Request.QueryString["mode"]) || Request.QueryString["mode"] == "1")
        {
            i = Conn.AddNew("ActivityDetail", "ActivityDetailCode, TDay, TDay2, ActivityCode, Objective, ActivityDetail, ActivityStatus, Sort, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate, UnderManagerName, PositionUnderManagerName, ResponsibleName, PositionResponsible, HeadGroupName, PositionHeadGroup, HeadGroupSaraName, PositionHeadGroupSara, SuppliesName, PositionSupplies, Confirm, DeputyDirectorID, DeputyDirectorName, DeputyDirectorPosition",
                hdfID.Value, Convert.ToDateTime(txtSDay.Text).ToString("s"), Convert.ToDateTime(txtSDay2.Text).ToString("s"), txtActivityCode.Text, txtObjective.Text, txtActivityDetail.Text, ddlActivityStatus.SelectedValue, txtWithdrow.Text, 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now, txtUnderManagerName.Text, txtPositionUnderManagerName.Text, ddlResponsibleName.SelectedValue, txtPositionResponsible.Text, txtHeadGroupName.Text, txtPositionHeadGroup.Text, txtHeadGroupSaraName.Text, txtPositionHeadGroupSara.Text, txtSuppliesName.Text, txtPositionSupplies.Text, 0, ddlDeputyDirectorName.SelectedValue, DeputyDirectorName, DeputyDirectorPosition);

             if (Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus"))) // เช็คว่าเปิดโหมดติดตามกิจกรรม
             {
                 strbSql.AppendFormat("UPDATE Activity SET ActivityStatus = '{1}' Where ActivityCode = '{0}';", Request["acid"], ddlActivityStatus.SelectedValue);

                 //Conn.Update("Activity", "Where ActivityCode = '" + Request["acid"] + "'", "ActivityStatus", ddlActivityStatus.SelectedValue);
             }

            //Budget
            if (GridViewBudget.Rows.Count != 0)
            {
                for (int j = 0; j < GridViewBudget.Rows.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ActivityCostsDetail (ActivityDetailCode, ActivityCode, ItemID, TotalMoney, TotalDD, Cost)VALUES('{0}','{1}','{2}',{3},{4},{5});", 
                        hdfID.Value, txtActivityCode.Text, Request["txtEntry" + j + ""].Trim(), Convert.ToDecimal(Request["txtT" + j + ""]), Convert.ToDecimal(Request["txtDD" + j + ""]), Convert.ToDecimal(Request["txtC" + j + ""]));

                    //Conn.AddNew("ActivityCostsDetail", "ActivityDetailCode, ActivityCode, EntryCostsCode, TotalMoney, TotalDD, Cost",
                    //    hdfID.Value, txtActivityCode.Text, Request["txtEntry" + j + ""].Trim(), Convert.ToDecimal(Request["txtT" + j + ""]), Convert.ToDecimal(Request["txtDD" + j + ""]), Convert.ToDecimal(Request["txtC" + j + ""]));
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Response.Redirect("ActivityDetail.aspx?ckmode=1&Cr=" + i + "&acid=" + Request["acid"]);   
        }
        if (Request.QueryString["mode"] == "2")
        {
            i = Conn.Update("ActivityDetail", "Where ActivityDetailCode = '" + hdfID.Value + "' ", "TDay, TDay2, Objective, ActivityDetail, ActivityStatus, Sort, UpdateUser, UpdateDate, UnderManagerName, PositionUnderManagerName, ResponsibleName, PositionResponsible, HeadGroupName, PositionHeadGroup, HeadGroupSaraName, PositionHeadGroupSara, SuppliesName, PositionSupplies, DeputyDirectorID, DeputyDirectorName, DeputyDirectorPosition",
                Convert.ToDateTime(txtSDay.Text).ToString("s"), Convert.ToDateTime(txtSDay2.Text).ToString("s"), txtObjective.Text, txtActivityDetail.Text, ddlActivityStatus.SelectedValue, txtWithdrow.Text, CurrentUser.ID, DateTime.Now, txtUnderManagerName.Text, txtPositionUnderManagerName.Text, ddlResponsibleName.SelectedValue, txtPositionResponsible.Text, txtHeadGroupName.Text, txtPositionHeadGroup.Text, txtHeadGroupSaraName.Text, txtPositionHeadGroupSara.Text, txtSuppliesName.Text, txtPositionSupplies.Text, ddlDeputyDirectorName.SelectedValue, DeputyDirectorName, DeputyDirectorPosition);

             if (Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus"))) // เช็คว่าเปิดโหมดติดตามกิจกรรม
             {
                 string strSql = "Select ActivityStatus FRom Activity Where ActivityCode = '{0}'";
                 DataView dvCk = Conn.Select(string.Format(strSql, Request["acid"]));
                 if (dvCk.Count != 0)
                 {
                     if (Convert.ToInt32(dvCk[0]["ActivityStatus"].ToString()) < Convert.ToInt32(ddlActivityStatus.SelectedValue))
                     {
                         strbSql.AppendFormat("UPDATE Activity SET ActivityStatus = '{1}' Where ActivityCode = '{0}';", Request["acid"], ddlActivityStatus.SelectedValue);
                         //Conn.Update("Activity", "Where ActivityCode = '" + Request["acid"] + "'", "ActivityStatus", ddlActivityStatus.SelectedValue);
                     }
                 }
             }
            //Budget
            if (GridViewBudget.Rows.Count != 0)
            {
                Conn.Delete("ActivityCostsDetail", "Where ActivityDetailCode = '" + hdfID.Value + "'");
                for (int j = 0; j < GridViewBudget.Rows.Count; j++)
                {
                    strbSql.AppendFormat("INSERT INTO ActivityCostsDetail (ActivityDetailCode, ActivityCode, ItemID, TotalMoney, TotalDD, Cost)VALUES('{0}','{1}','{2}',{3},{4},{5});",
                        hdfID.Value, txtActivityCode.Text, Request["txtEntry" + j + ""].Trim(), Convert.ToDecimal(Request["txtT" + j + ""]), Convert.ToDecimal(Request["txtDD" + j + ""]), Convert.ToDecimal(Request["txtC" + j + ""]));

                    //Conn.AddNew("ActivityCostsDetail", "ActivityDetailCode, ActivityCode, EntryCostsCode, TotalMoney, TotalDD, Cost",
                    //    hdfID.Value, txtActivityCode.Text, Request["txtEntry" + j + ""], Convert.ToDecimal(Request["txtT" + j + ""]), Convert.ToDecimal(Request["txtDD" + j + ""]), Convert.ToDecimal(Request["txtC" + j + ""]));
                }
            }
            result = Conn.Execute(strbSql.ToString());
            Response.Redirect("ActivityDetail.aspx?ckmode=2&Cr=" + i + "&acid=" + Request["acid"]); 
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        bt_Save();
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;

        Int32 i = Conn.Update("ActivityDetail", "Where ActivityDetailCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
        Conn.Delete("ActivityCostsDetail", "Where ActivityDetailCode = '" + Request["id"] + "'");
        Response.Redirect("ActivityDetail.aspx?ckmode=3&Cr=" + i + "&acid=" + Request["acid"]); 
    }
    public decimal GetTotalMoney(decimal Budget)
    {
        ttlMoney += Budget;
        return Budget;
    }
    public decimal GetSumTotalMoney()
    {
        return ttlMoney;
    }
    protected void GridViewBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Request["mode"] == "1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalMoney")) <= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OldTotalAmount")))
                {
                    e.Row.Enabled = false;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "enabledtxt(" + e.Row.RowIndex.ToString() + ");", true);
                }
            }
        }
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
                    NewID, cbDuo.Checked, fpAttach.FileName, "", fpAttach.PostedFile.ContentLength, fpAttach.PostedFile.ContentType, hdfID.Value, DateTime.Now.Year, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now, 1, 0, 1, 0);
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
            GetDataAttach(hdfID.Value);

            if (Request["mode"] == "1")
            {
                getBudget(Request["acid"]);
            }
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
    protected string getActivityStatus(string ActivityStatus)
    {
        return btc.getSpanColorStatus(Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")), ActivityStatus);
    }
    protected string getConfrim(object ID, object status)
    {
        string Link = "";
        string ckImg = "";
        string ckTitle = "";
        if (!string.IsNullOrEmpty(status.ToString()))
        {
            if (Convert.ToInt32(status) == 0)
            {
                ckImg = "no";
                ckTitle = "ยังไม่ยืนยันงบ";
            }
            else
            {
                ckImg = "ok";
                ckTitle = "ยืนยันงบแล้ว";            
            }
        }
        string ckOnclick = " ";
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            ckOnclick = " onclick=\"EnableStatus('" + ID.ToString() + "');\" style=\"cursor:pointer;\" ";
        }
        Link = "<img id=\"img" + ID.ToString() + "\"  " + ckOnclick + " src=\"../Image/" + ckImg + ".png\" title=\"" + ckTitle + "\" class=\"tooltip\" />";
        return Link;
    }

    [System.Web.Services.WebMethod]
    public static string EnableStatus(string id, Int32 IsUse)
    {
        string result = "0";
        Connection Conn = new Connection();
        try
        {
            result = Conn.Update("ActivityDetail", "Where ActivityDetailCode = '" + id + "'", "Confirm, ConfirmUser, ConfirmDate", IsUse, CurrentUser.ID, DateTime.Now).ToString();
            return result;
        }
        catch (Exception ex)
        {
            return result;
        }
    }
    protected void txtKeyWordDeputyDirector_TextChanged(object sender, EventArgs e)
    {
        Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "KeyWordDeputyDirector", txtKeyWordDeputyDirector.Text);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
        //if (Request["mode"] == "1")
        //{
        //    getBudget(Request.QueryString["acid"]);
        //}
        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "enabledtxt(" + i + ");", true);
        //}
    }
    protected void ddlDeputyDirectorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataRow[] dr = dvDeputyDirector.Table.Select("DeputyDirectorID = '" + ddlDeputyDirectorName.SelectedValue + "'");
        ////dvDeputyDirector.RowFilter = "DeputyDirectorID = '" + ddlDeputyDirectorName.SelectedValue + "'";
        //if (dr.Length > 0)
        //{
        //    lblPositionDeputyDirector.Text = dr[0]["DeputyDirectorPosition"].ToString();
        //}
        string strSql = "Select DeputyDirectorPosition From DeputyDirector Where DeputyDirectorID = '" + ddlDeputyDirectorName.SelectedValue + "' ";
        DataView dv = Conn.Select(string.Format(strSql));
        if (dv.Count > 0)
        {
            lblPositionDeputyDirector.Text = dv[0]["DeputyDirectorPosition"].ToString();
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
        //if (Request["mode"] == "1")
        //{
        //    getBudget(Request.QueryString["acid"]);
        //}
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "enabledtxt(" + i + ");", true);
        //    //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "enabledtxt(" + i + ");", true);
        //}
    }
}
