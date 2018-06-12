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
using System.Text;
using System.IO;

public partial class PageDefault : System.Web.UI.Page
{
    BTC btc = new BTC();
    Connection Conn = new Connection();

    protected override void OnPreInit(EventArgs e)
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 2)
        {
            Response.Redirect("ManegerView.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Cookie.SetValue2("ckActivityStatus", btc.ckIdentityName("ckActivityStatus")); //�������Դ����ҹ

        if (!IsPostBack)
        {
            ckAlertRenew();
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "AlertActivity();", true);
            getSchoolDetail();
            CkAdmin();
            LoadComment();
            LoadImageSlider();
            getddlDepartment();
            getddlEmpByDept(0, ddlSearchDept.SelectedValue);
            btc.CkGroup(ddlSearchDept, ddlSearchEmp, Cookie.GetValue2("DfDept").ToString(), Cookie.GetValue2("DfEmp").ToString());
            getrbtlStatus();
            DataBind();  
        }
    }
    private void getddlDepartment()
    {
        btc.getddlDepartment(ddlSearchDept);
        ddlSearchDept.Items.Insert(0, new ListItem("-������-", ""));
        btc.CkAllDept(ddlSearchDept);
    }
    private void getddlEmpByDept(int mode, string DeptCode)
    {
        if (mode == 0)
        {
            btc.getddlEmpByDept(0, ddlSearchEmp, DeptCode);
        }
    }
    private void getrbtlStatus()
    {
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Name");
        dt1.Columns.Add("Data", typeof(Int32));

        rbtlStatus.Items.Clear();
        int[] sts = { 0, 0, 0, 0, 0, 0 };
        string[] stsName = { "�ʹ��Թ���", "���ѧ���Թ���", "��¡�˹����", "�������", "�����¡�˹�" };
        for (int i = 0; i < 6; i++)
        {
            sts[i] = btc.getCountActivity(1, i, ddlSearchDept.SelectedValue, ddlSearchEmp.SelectedValue, "0", "", "", "0");

            DataRow dr;
            if (i < 5)
            {
                dr = dt1.NewRow();
                dr["Name"] = stsName[i];
                dr["Data"] = Convert.ToInt32(sts[i]);
                dt1.Rows.Add(dr);
            }
        }

        DataView dvStatus = dt1.DefaultView;
        if (dvStatus.Count > 0)
        {
            ReportGraph("column2d", dvStatus, 1);
            ReportGraph("Pie2D", dvStatus, 2);
        }
        else
        {
            dvStatus = Conn.Select("Select '' As Name, 0 As Data From Config");
            ReportGraph("column2d", dvStatus, 1);
            ReportGraph("Pie2D", dvStatus, 2);
        }

        rbtlStatus.Items.Insert(0, new ListItem(" <img style=\"border:none;\" src=\"../Image/00.png\" width=\"25px\" height=\"25px\"/> �ʹ��Թ��� &nbsp;(" + sts[0] + ")&nbsp;", "0"));
        rbtlStatus.Items.Insert(1, new ListItem(" <img style=\"border:none;\" src=\"../Image/02.png\" width=\"25px\" height=\"25px\"/> ��¡�˹���� &nbsp;(" + sts[2] + ")&nbsp;", "2"));
        rbtlStatus.Items.Insert(2, new ListItem(" <img style=\"border:none;\" src=\"../Image/01.png\" width=\"25px\" height=\"25px\"/> ���ѧ���Թ��� &nbsp;(" + sts[1] + ")&nbsp;", "1"));
        rbtlStatus.Items.Insert(3, new ListItem(" <img style=\"border:none;\" src=\"../Image/03.png\" width=\"25px\" height=\"25px\"/> ���Թ���������� &nbsp;(" + sts[3] + ")&nbsp;", "3"));
        rbtlStatus.Items.Insert(4, new ListItem(" <img style=\"border:none;\" src=\"../Image/04.png\" width=\"25px\" height=\"25px\"/> �����¡�˹���� &nbsp;(" + sts[4] + ")&nbsp;", "4"));
        rbtlStatus.Items.Insert(5, new ListItem(" <img style=\"border:none;\" src=\"../Image/09.png\" width=\"25px\" height=\"25px\"/> �ءʶҹ�(" + sts[5] + ")&nbsp; ", "5"));

        if (Session["sts"] == null)
        {
            rbtlStatus.SelectedIndex = 2;
        }
        else
        {
            rbtlStatus.SelectedValue = Session["sts"].ToString();
        }
    }
    public override void DataBind()
    {
        DataView dv1 = Conn.Select("Select StudyYear From StudyYear Where DelFlag = 0 And Df = 1");
        string studyYear = (DateTime.Now.Year + 543).ToString();

        if (dv1.Count != 0)
        {
            studyYear = dv1[0]["StudyYear"].ToString();
        }

        string StrSql = "";

        StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.Status, "
                    + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, "
                    + " b.SDate, b.EDate, 0.0 As TotalBalance, '' DeptName, '' EmpName, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                    + " From Projects a, Activity b "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 "
                    + " And a.StudyYear = '" + studyYear + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' ";

        if (ddlSearchDept.SelectedIndex != 0)
        {
            StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.Status, "
                + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, "
                + " b.SDate, b.EDate, 0.0 As TotalBalance, '' DeptName, '' EmpName, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                + " From Projects a, Activity b, dtAcDept c "
                + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                + " And a.StudyYear = '" + studyYear + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                + " And c.DeptCode = '" + ddlSearchDept.SelectedValue + "'";
        }
        if (ddlSearchEmp.SelectedIndex != 0)
        {
            if (ddlSearchDept.SelectedIndex == 0)
            {
                StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.Status, "
                    + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, "
                    + " b.SDate, b.EDate, 0.0 As TotalBalance, '' DeptName, '' EmpName, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                    + " From Projects a, Activity b, dtAcEmp c "
                    + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 And b.ActivityCode = c.ActivityCode "
                    + " And a.StudyYear = '" + studyYear + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                    + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "'";
            }
            else
            {
                StrSql = " Select a.ProjectsCode, a.ProjectsName, b.ActivityCode, b.ActivityName, b.Status, "
                        + " IsNull(b.TotalAmount, 0) TotalAmount, IsNull(b.TotalAmount2, 0) TotalAmount2, "
                        + " b.SDate, b.EDate, 0.0 As TotalBalance, '' DeptName, '' EmpName, IsNull(b.ActivityStatus, 0) As ActivityStatus "
                        + " From Projects a, Activity b, dtAcEmp c, dtAcDept d "
                        + " Where a.ProjectsCode = b.ProjectsCode And b.DelFlag = 0 "
                        + " And b.ActivityCode = c.ActivityCode And b.ActivityCode = d.ActivityCode "
                        + " And a.StudyYear = '" + studyYear + "' And b.SchoolID = '" + CurrentUser.SchoolID + "' "
                        + " And c.EmpCode = '" + ddlSearchEmp.SelectedValue + "' And d.DeptCode = '" + ddlSearchDept.SelectedValue + "' ";
            }
        }
        if (rbtlStatus.SelectedIndex != 5)
        {
            StrSql = StrSql + " And b.Status = '" + rbtlStatus.SelectedValue + "'";
        }

        DataView dv = Conn.Select(string.Format(StrSql + " Order By b.Sort "));

        for (int j = 0; j < dv.Count; j++)
        {
            dv[j]["DeptName"] = btc.getAcDeptName(dv[j]["ActivityCode"].ToString());
            dv[j]["EmpName"] = btc.getAcEmpName(dv[j]["ActivityCode"].ToString());

            if (btc.getNTotalAmount(dv[j]["ActivityCode"].ToString()) != 0)
            {
                dv[j]["TotalAmount2"] = btc.getNTotalAmount(dv[j]["ActivityCode"].ToString());
            }
            dv[j]["TotalBalance"] = (Convert.ToDecimal(dv[j]["TotalAmount"]) - Convert.ToDecimal(dv[j]["TotalAmount2"])).ToString();
        }

        GridView1.DataSource = dv;
        lblSearchTotal.InnerHtml = dv.Count.ToString();
        GridView1.DataBind();
    }
    protected string DateFormat(object startDate, object endDate)
    {
        return Convert.ToDateTime(startDate).ToString("dd/MM/yy") + " - " + Convert.ToDateTime(endDate).ToString("dd/MM/yy");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.ForeColor = btc.getColorRowsGrid(DataBinder.Eval(e.Row.DataItem, "status").ToString());
        }
    }
    protected void ddlSearchDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("DfDept", ddlSearchDept.SelectedValue);
        Session["sts"] = rbtlStatus.SelectedValue;
        getddlEmpByDept(0, ddlSearchDept.SelectedValue);
        getrbtlStatus();
        DataBind();
    }
    protected void ddlSearchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cookie.SetValue2("DfEmp", ddlSearchEmp.SelectedValue);
        Session["sts"] = rbtlStatus.SelectedValue;
        getrbtlStatus();
        DataBind();
    }
    protected void rbtlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["sts"] = rbtlStatus.SelectedValue;
        DataBind();
    }
    decimal TotalAmount1;
    decimal TotalAmount2;
    decimal TotalAmount3;
    public decimal GetTotalAmount1(decimal Budget)
    {
        TotalAmount1 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount1()
    {
        return TotalAmount1;
    }
    public decimal GetTotalAmount2(decimal Budget)
    {
        TotalAmount2 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount2()
    {
        return TotalAmount2;
    }
    public decimal GetTotalAmount3(decimal Budget)
    {
        TotalAmount3 += Budget;
        return Budget;
    }
    public decimal GetSumTotalAmount3()
    {
        return TotalAmount3;
    }
    protected string getActivityStatus(string ActivityStatus)
    {
        return btc.getSpanColorStatus(Convert.ToBoolean(Cookie.GetValue2("ckActivityStatus")), ActivityStatus);
    }
    void ReportGraph(string ChartName, DataView dv, int mode)
    {
        string chartData = "";
        if (mode == 1)
        {
            Suffix = "numberSuffix=''";
            chartData = GenerateChart(ChartName, dv, "", null, false, " ", "");
            graphPnl1.InnerHtml = Graph.FusionCharts.RenderChartHTML(this.ResolveUrl(string.Format("~/Charts/{0}.swf", ChartName)), "", chartData, "AA", "500", "270", false);
        }
        else
        {
            if (mode == 2)
            {
                Suffix = "numberSuffix=''";
                chartData = GenerateChart(ChartName, dv, "", null, false, " ", "");
                graphPnl2.InnerHtml = Graph.FusionCharts.RenderChartHTML(this.ResolveUrl(string.Format("~/Charts/{0}.swf", ChartName)), "", chartData, "BB", "500", "270", false);
            }

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
            if (!string.IsNullOrEmpty(column))//gen xml Ẻ����� 0 record
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
            else//gen xml Ẻ record 2 �����
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

            if (compare)//gen xml Ẻ ���º��º
            {
                chartData.Append("<categories font='Arial' fontSize='11' fontColor='000000'>");

                //-------========
                int i = 0;
                if (!string.IsNullOrEmpty(column))//gen xml Ẻ����� 0 record
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
                else//gen xml Ẻ record
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
                if (!string.IsNullOrEmpty(column))//gen xml Ẻ����� 0 record
                {
                    foreach (string col in column.Split(','))
                    {
                        chartData.Append(string.Format("<set label='{0}' value='{1}' color='{2}' />", col, dv[0][col], GetColor(color, i)));
                        i++;
                    }
                }
                else//gen xml Ẻ record 2 �����
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
    private void getSchoolDetail()
    {
        string StrSql = "Select * From MR_School Where DelFlag = 0 And SchoolID = '" + CurrentUser.SchoolID + "' ";
        DataView dv = Conn.Select(string.Format(StrSql));

        if (dv.Count > 0)
        {
            Repeater1.DataSource = dv;
            Repeater1.DataBind();

            Repeater2.DataSource = dv;
            Repeater2.DataBind();
        }
    }
    private void LoadComment()
    {
        DataView dv = Conn.Select("Select Announce From MR_School");
        if (dv.Count != 0)
        {
            lblComment.InnerText = dv[0]["Announce"].ToString();
            txtComment.Text = dv[0]["Announce"].ToString();
        }
        if (lblComment.InnerText == "")
        {
            Image1.Visible = false;
        }
        else
        {
            Image1.Visible = true;
        }
        //lblComment.ForeColor = System.Drawing.Color.FromArgb(234, 0, 129);
    }
    private void CkAdmin()
    {
        if (btc.ckGetAdmission(CurrentUser.UserRoleID) == 1)
        {
            lbtUpdateComment.Visible = true;
        }
        else
        {
            lbtUpdateComment.Visible = false;
        }
    }
    private void txtEnabled(Boolean Enabled)
    {
        txtComment.Visible = Enabled;
        btSave.Visible = Enabled;
        btCancel.Visible = Enabled;
        lblComment.Visible = !Enabled;
        lbtUpdateComment.Visible = !Enabled;
    }
    protected void lbtUpdateComment_OnClick(object sender, EventArgs e)
    {
        txtEnabled(true);
    }
    protected void btSave_OnClick(object sender, EventArgs e)
    {
        Int32 i = Conn.Update("MR_School", "Where SchoolID = '" + CurrentUser.SchoolID + "'", "Announce", txtComment.Text);
        txtEnabled(false);
        LoadComment();
    }
    protected void btCancel_OnClick(object sender, EventArgs e)
    {
        txtEnabled(false);
    }
    private void ckAlertRenew()
    {
        string strSql = "Select DATEDIFF(day, GETDATE(), ExpDate) BalanceDay From MR_School";
        DataView dvExpDate = Conn.Select(string.Format(strSql));
        if (dvExpDate.Count != 0)
        {
            if (!string.IsNullOrEmpty(dvExpDate[0]["BalanceDay"].ToString()))
            {
                if ((Convert.ToInt32(dvExpDate[0]["BalanceDay"]) <= 30) && (Convert.ToInt32(dvExpDate[0]["BalanceDay"]) >= 0))
                {
                    spnAlertRenew.InnerHtml = "*** �ѭ����� Server �������ա " + dvExpDate[0]["BalanceDay"].ToString() + " �ѹ ***";
                }
            }
        }
    }
    private void LoadImageSlider()
    {
        string strSql = "Select ItemID, Title From Multimedia Where ReferID = '99999999-AAAA-BBBB-CCCC-999999999999' ";
        DataView dv = Conn.Select(string.Format(strSql));
        if (dv.Count != 0)
        {
            rptImageSlider.DataSource = dv;
            rptImageSlider.DataBind();
        }
    }
    protected string genImage(object ItemID, object Title)
    {
        return ItemID.ToString() + Path.GetExtension(Title.ToString());
    }
    protected string genStr(object Title)
    {
        return Path.GetFileNameWithoutExtension(Title.ToString()).ToString();
    }
}
