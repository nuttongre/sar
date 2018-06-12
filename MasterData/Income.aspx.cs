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

public partial class Income : System.Web.UI.Page
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

            getddlYear(0);
            btc.CkAdmission(GridView1, btAdd, null);
            string mode = Request["mode"];
            if (!string.IsNullOrEmpty(mode))
            {
                switch (mode.ToLower())
                {
                    case "1":
                        MultiView1.ActiveViewIndex = 1;
                        getddlYear(1);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
                        break;
                    case "2":
                        MultiView1.ActiveViewIndex = 1;
                        LinkReport(linkReport1, 20);
                        LinkReport(linkReport2, 18);
                        LinkReport(linkReport3, 19);
                        getddlYear(1);
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
    public override void DataBind()
    {
        string StrSql = " Select *, (Subsidies + Revenue + Reserve + Free + Other) As TotalAmount From Income "
                    + " Where DelFlag = 0 And StudyYear = '" + ddlSearchYear.SelectedValue + "' ";

        if (txtSearch.Text != "")
        {
            StrSql = StrSql + " And ( Subsidies Like '%" + txtSearch.Text + "%' Or Revenue Like '%" + txtSearch.Text + "%' Or Free Like '%" + txtSearch.Text + "%' Or Other Like '%" + txtSearch.Text + "%' )  ";
        }
        DataView dv = Conn.Select(string.Format(StrSql + " Order By StudyYear Asc "));
        GridView1.DataSource = dv;
        GridView1.DataBind();
        lblSearchTotal.InnerText = dv.Count.ToString();
    }
    private void GetData(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        DataView dv, dv1;
        string strSql = " Select IncomeCode, StudyYear, IsNull(Ma, 0) Ma, IsNull(Subsidies, 0) Subsidies, IsNull(MaRevenue, 0) MaRevenue, IsNull(Revenue, 0) Revenue,"
                        + " IsNull(MaFree, 0) MaFree, IsNull(Free, 0) Free, IsNull(MaReserve, 0) MaReserve, IsNull(Reserve, 0) Reserve,  "
                        + " IsNull(MaOther, 0) MaOther, IsNull(Other, 0) Other  "
                        + " From Income Where IncomeCode = '" + id + "'";
        dv = Conn.Select(string.Format(strSql));
        if (dv.Count != 0)
        {
            lblInComeCode.Text = dv[0]["IncomeCode"].ToString();
            ddlYearB.SelectedValue = dv[0]["StudyYear"].ToString();
            ddlYearB.Enabled = false;
            txtMa.Text = Convert.ToDecimal(dv[0]["Ma"]).ToString("#,##0");
            txtSubsidies.Text = (Convert.ToDecimal(dv[0]["Subsidies"]) - Convert.ToDecimal(dv[0]["Ma"])).ToString("#,##0");
            txtMaRevenue.Text = Convert.ToDecimal(dv[0]["MaRevenue"]).ToString("#,##0");
            txtRevenue.Text = (Convert.ToDecimal(dv[0]["Revenue"]) - Convert.ToDecimal(dv[0]["MaRevenue"])).ToString("#,##0");
            txtMaFree.Text = Convert.ToDecimal(dv[0]["MaFree"]).ToString("#,##0");
            txtFree.Text = (Convert.ToDecimal(dv[0]["Free"]) - Convert.ToDecimal(dv[0]["MaFree"])).ToString("#,##0");
            txtMaReserve.Text = Convert.ToDecimal(dv[0]["MaReserve"]).ToString("#,##0");
            txtReserve.Text = (Convert.ToDecimal(dv[0]["Reserve"]) - Convert.ToDecimal(dv[0]["MaReserve"])).ToString("#,##0");
            txtMaOther.Text = Convert.ToDecimal(dv[0]["MaOther"]).ToString("#,##0");
            txtOther.Text = (Convert.ToDecimal(dv[0]["Other"]) - Convert.ToDecimal(dv[0]["MaOther"])).ToString("#,##0");
        }
        strSql = " Select IsNull(A1No, 0) A1No, IsNull(A1Value, 0) A1Value, IsNull(A2No, 0) A2No, IsNull(A2Value, 0) A2Value, IsNull(A3No, 0) A3No, IsNull(A3Value, 0) A3Value, "
            + " IsNull(P1No, 0) P1No, IsNull(P1Value, 0) P1Value, IsNull(P2No, 0) P2No, IsNull(P2Value, 0) P2Value, IsNull(P3No, 0) P3No, IsNull(P3Value, 0) P3Value, "
            + " IsNull(P4No, 0) P4No, IsNull(P4Value, 0) P4Value, IsNull(P5No, 0) P5No, IsNull(P5Value, 0) P5Value, IsNull(P6No, 0) P6No, IsNull(P6Value, 0) P6Value, "
            + " IsNull(M1No, 0) M1No, IsNull(M1Value, 0) M1Value, IsNull(M2No, 0) M2No, IsNull(M2Value, 0) M2Value, IsNull(M3No, 0) M3No, IsNull(M3Value, 0) M3Value, "
            + " IsNull(M4No, 0) M4No, IsNull(M4Value, 0) M4Value, IsNull(M5No, 0) M5No, IsNull(M5Value, 0) M5Value, IsNull(M6No, 0) M6No, IsNull(M6Value, 0) M6Value, "
            + " IsNull(ClassA1, 0) ClassA1, IsNull(ClassA2, 0) ClassA2, IsNull(ClassA3, 0) ClassA3, "
            + " IsNull(ClassP1, 0) ClassP1, IsNull(ClassP2, 0) ClassP2, IsNull(ClassP3, 0) ClassP3, IsNull(ClassP4, 0) ClassP4, IsNull(ClassP5, 0) ClassP5, IsNull(ClassP6, 0) ClassP6, "
            + " IsNull(ClassM1, 0) ClassM1, IsNull(ClassM2, 0) ClassM2, IsNull(ClassM3, 0) ClassM3, IsNull(ClassM4, 0) ClassM4, IsNull(ClassM5, 0) ClassM5, IsNull(ClassM6, 0) ClassM6 "
            + " From IncomeDetail Where IncomeCode = '{0}'";
        dv1 = Conn.Select(string.Format(strSql, id));
        if (dv1.Count != 0)
        {
            txtA1.Value = Convert.ToDecimal(dv1[0]["A1No"]).ToString("#,##0");
            txtAb1.Value = Convert.ToDecimal(dv1[0]["A1Value"]).ToString("#,##0");
            txtA2.Value = Convert.ToDecimal(dv1[0]["A2No"]).ToString("#,##0");
            txtAb2.Value = Convert.ToDecimal(dv1[0]["A2Value"]).ToString("#,##0");
            txtA3.Value = Convert.ToDecimal(dv1[0]["A3No"]).ToString("#,##0");
            txtAb3.Value = Convert.ToDecimal(dv1[0]["A3Value"]).ToString("#,##0");

            txtClassA1.Value = Convert.ToDecimal(dv1[0]["ClassA1"]).ToString("#,##0");
            txtClassA2.Value = Convert.ToDecimal(dv1[0]["ClassA2"]).ToString("#,##0");
            txtClassA3.Value = Convert.ToDecimal(dv1[0]["ClassA3"]).ToString("#,##0");

            txtP1.Value = Convert.ToDecimal(dv1[0]["P1No"]).ToString("#,##0");
            txtPb1.Value = Convert.ToDecimal(dv1[0]["P1Value"]).ToString("#,##0");
            txtP2.Value = Convert.ToDecimal(dv1[0]["P2No"]).ToString("#,##0");
            txtPb2.Value = Convert.ToDecimal(dv1[0]["P2Value"]).ToString("#,##0");
            txtP3.Value = Convert.ToDecimal(dv1[0]["P3No"]).ToString("#,##0");
            txtPb3.Value = Convert.ToDecimal(dv1[0]["P3Value"]).ToString("#,##0");
            txtP4.Value = Convert.ToDecimal(dv1[0]["P4No"]).ToString("#,##0");
            txtPb4.Value = Convert.ToDecimal(dv1[0]["P4Value"]).ToString("#,##0");
            txtP5.Value = Convert.ToDecimal(dv1[0]["P5No"]).ToString("#,##0");
            txtPb5.Value = Convert.ToDecimal(dv1[0]["P5Value"]).ToString("#,##0");
            txtP6.Value = Convert.ToDecimal(dv1[0]["P6No"]).ToString("#,##0");
            txtPb6.Value = Convert.ToDecimal(dv1[0]["P6Value"]).ToString("#,##0");

            txtClassP1.Value = Convert.ToDecimal(dv1[0]["ClassP1"]).ToString("#,##0");
            txtClassP2.Value = Convert.ToDecimal(dv1[0]["ClassP2"]).ToString("#,##0");
            txtClassP3.Value = Convert.ToDecimal(dv1[0]["ClassP3"]).ToString("#,##0");
            txtClassP4.Value = Convert.ToDecimal(dv1[0]["ClassP4"]).ToString("#,##0");
            txtClassP5.Value = Convert.ToDecimal(dv1[0]["ClassP5"]).ToString("#,##0");
            txtClassP6.Value = Convert.ToDecimal(dv1[0]["ClassP6"]).ToString("#,##0");

            txtM1.Value = Convert.ToDecimal(dv1[0]["M1No"]).ToString("#,##0");
            txtMb1.Value = Convert.ToDecimal(dv1[0]["M1Value"]).ToString("#,##0");
            txtM2.Value = Convert.ToDecimal(dv1[0]["M2No"]).ToString("#,##0");
            txtMb2.Value = Convert.ToDecimal(dv1[0]["M2Value"]).ToString("#,##0");
            txtM3.Value = Convert.ToDecimal(dv1[0]["M3No"]).ToString("#,##0");
            txtMb3.Value = Convert.ToDecimal(dv1[0]["M3Value"]).ToString("#,##0");
            txtM4.Value = Convert.ToDecimal(dv1[0]["M4No"]).ToString("#,##0");
            txtMb4.Value = Convert.ToDecimal(dv1[0]["M4Value"]).ToString("#,##0");
            txtM5.Value = Convert.ToDecimal(dv1[0]["M5No"]).ToString("#,##0");
            txtMb5.Value = Convert.ToDecimal(dv1[0]["M5Value"]).ToString("#,##0");
            txtM6.Value = Convert.ToDecimal(dv1[0]["M6No"]).ToString("#,##0");
            txtMb6.Value = Convert.ToDecimal(dv1[0]["M6Value"]).ToString("#,##0");

            txtClassM1.Value = Convert.ToDecimal(dv1[0]["ClassM1"]).ToString("#,##0");
            txtClassM2.Value = Convert.ToDecimal(dv1[0]["ClassM2"]).ToString("#,##0");
            txtClassM3.Value = Convert.ToDecimal(dv1[0]["ClassM3"]).ToString("#,##0");
            txtClassM4.Value = Convert.ToDecimal(dv1[0]["ClassM4"]).ToString("#,##0");
            txtClassM5.Value = Convert.ToDecimal(dv1[0]["ClassM5"]).ToString("#,##0");
            txtClassM6.Value = Convert.ToDecimal(dv1[0]["ClassM6"]).ToString("#,##0");
        }
        btc.getCreateUpdateUser(lblCreate, lblUpdate, "InCome", "IncomeCode", id);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
    }
    protected void btSearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        Int32 i = 0;
        if ((btc.CkUseData(ddlYearB.SelectedValue, "StudyYear", "Income", "And DelFlag = 0")) && Request["mode"] != "2")
        {
            btc.Msg_Head(Img1, MsgHead, true, "7", 0);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SumTotal();", true);
            return;
        }
        if (String.IsNullOrEmpty(Request["mode"]) || Request["mode"] == "1")
        {           
            string NewID = Guid.NewGuid().ToString();
            i = Conn.AddNew("Income", "IncomeCode, StudyYear, Ma, Subsidies, MaRevenue, Revenue, MaFree, Free, MaReserve, Reserve, MaOther, Other, DelFlag, CreateUser, CreateDate, UpdateUser, UpdateDate", NewID, ddlYearB.SelectedValue, Convert.ToDecimal(txtMa.Text), Convert.ToDecimal(txtMa.Text) + Convert.ToDecimal(txtSubsidies.Text), Convert.ToDecimal(txtMaRevenue.Text), Convert.ToDecimal(txtMaRevenue.Text) + Convert.ToDecimal(txtRevenue.Text), Convert.ToDecimal(txtMaFree.Text), Convert.ToDecimal(txtMaFree.Text) + Convert.ToDecimal(txtFree.Text), Convert.ToDecimal(txtMaReserve.Text), Convert.ToDecimal(txtMaReserve.Text) + Convert.ToDecimal(txtReserve.Text), Convert.ToDecimal(txtMaOther.Text), Convert.ToDecimal(txtMaOther.Text) + Convert.ToDecimal(txtOther.Text), 0, CurrentUser.ID, DateTime.Now, CurrentUser.ID, DateTime.Now);
            Conn.AddNew("IncomeDetail", "IncomeCode, StudyYear, A1No, A1Value, A2No, A2Value, A3No, A3Value, P1No, P1Value, P2No, P2Value, P3No, P3Value, P4No, P4Value, P5No, P5Value, P6No, P6Value, M1No, M1Value, M2No, M2Value, M3No, M3Value, M4No, M4Value, M5No, M5Value, M6No, M6Value, ClassA1, ClassA2, ClassA3, ClassP1, ClassP2, ClassP3, ClassP4, ClassP5, ClassP6, ClassM1, ClassM2, ClassM3, ClassM4, ClassM5, ClassM6", NewID, ddlYearB.SelectedValue, Convert.ToDecimal(txtA1.Value), Convert.ToDecimal(txtAb1.Value), Convert.ToDecimal(txtA2.Value), Convert.ToDecimal(txtAb2.Value), Convert.ToDecimal(txtA3.Value), Convert.ToDecimal(txtAb3.Value), Convert.ToDecimal(txtP1.Value), Convert.ToDecimal(txtPb1.Value), Convert.ToDecimal(txtP2.Value), Convert.ToDecimal(txtPb2.Value), Convert.ToDecimal(txtP3.Value), Convert.ToDecimal(txtPb3.Value), Convert.ToDecimal(txtP4.Value), Convert.ToDecimal(txtPb4.Value), Convert.ToDecimal(txtP5.Value), Convert.ToDecimal(txtPb5.Value), Convert.ToDecimal(txtP6.Value), Convert.ToDecimal(txtPb6.Value), Convert.ToDecimal(txtM1.Value), Convert.ToDecimal(txtMb1.Value), Convert.ToDecimal(txtM2.Value), Convert.ToDecimal(txtMb2.Value), Convert.ToDecimal(txtM3.Value), Convert.ToDecimal(txtMb3.Value), Convert.ToDecimal(txtM4.Value), Convert.ToDecimal(txtMb4.Value), Convert.ToDecimal(txtM5.Value), Convert.ToDecimal(txtMb5.Value), Convert.ToDecimal(txtM6.Value), Convert.ToDecimal(txtMb6.Value), Convert.ToDecimal(txtClassA1.Value), Convert.ToDecimal(txtClassA2.Value), Convert.ToDecimal(txtClassA3.Value), Convert.ToDecimal(txtClassP1.Value), Convert.ToDecimal(txtClassP2.Value), Convert.ToDecimal(txtClassP3.Value), Convert.ToDecimal(txtClassP4.Value), Convert.ToDecimal(txtClassP5.Value), Convert.ToDecimal(txtClassP6.Value), Convert.ToDecimal(txtClassM1.Value), Convert.ToDecimal(txtClassM2.Value), Convert.ToDecimal(txtClassM3.Value), Convert.ToDecimal(txtClassM4.Value), Convert.ToDecimal(txtClassM5.Value), Convert.ToDecimal(txtClassM6.Value));
            Response.Redirect("Income.aspx?ckmode=1&Cr=" + i);      
        }
        if (Request["mode"] == "2")
        {
            i = Conn.Update("Income", "Where IncomeCode = '" + Request["id"] + "' ", "Ma, Subsidies, MaRevenue, Revenue, MaFree, Free, MaReserve, Reserve, MaOther, Other, UpdateUser, UpdateDate", Convert.ToDecimal(txtMa.Text), Convert.ToDecimal(txtMa.Text) + Convert.ToDecimal(txtSubsidies.Text), Convert.ToDecimal(txtMaRevenue.Text), Convert.ToDecimal(txtMaRevenue.Text) + Convert.ToDecimal(txtRevenue.Text), Convert.ToDecimal(txtMaFree.Text), Convert.ToDecimal(txtMaFree.Text) + Convert.ToDecimal(txtFree.Text), Convert.ToDecimal(txtMaReserve.Text), Convert.ToDecimal(txtMaReserve.Text) + Convert.ToDecimal(txtReserve.Text), Convert.ToDecimal(txtMaOther.Text), Convert.ToDecimal(txtMaOther.Text) + Convert.ToDecimal(txtOther.Text), CurrentUser.ID, DateTime.Now);
            Conn.Update("IncomeDetail", "Where IncomeCode = '" + Request["id"] + "' ", "A1No, A1Value, A2No, A2Value, A3No, A3Value, P1No, P1Value, P2No, P2Value, P3No, P3Value, P4No, P4Value, P5No, P5Value, P6No, P6Value, M1No, M1Value, M2No, M2Value, M3No, M3Value, M4No, M4Value, M5No, M5Value, M6No, M6Value, ClassA1, ClassA2, ClassA3, ClassP1, ClassP2, ClassP3, ClassP4, ClassP5, ClassP6, ClassM1, ClassM2, ClassM3, ClassM4, ClassM5, ClassM6", Convert.ToDecimal(txtA1.Value), Convert.ToDecimal(txtAb1.Value), Convert.ToDecimal(txtA2.Value), Convert.ToDecimal(txtAb2.Value), Convert.ToDecimal(txtA3.Value), Convert.ToDecimal(txtAb3.Value), Convert.ToDecimal(txtP1.Value), Convert.ToDecimal(txtPb1.Value), Convert.ToDecimal(txtP2.Value), Convert.ToDecimal(txtPb2.Value), Convert.ToDecimal(txtP3.Value), Convert.ToDecimal(txtPb3.Value), Convert.ToDecimal(txtP4.Value), Convert.ToDecimal(txtPb4.Value), Convert.ToDecimal(txtP5.Value), Convert.ToDecimal(txtPb5.Value), Convert.ToDecimal(txtP6.Value), Convert.ToDecimal(txtPb6.Value), Convert.ToDecimal(txtM1.Value), Convert.ToDecimal(txtMb1.Value), Convert.ToDecimal(txtM2.Value), Convert.ToDecimal(txtMb2.Value), Convert.ToDecimal(txtM3.Value), Convert.ToDecimal(txtMb3.Value), Convert.ToDecimal(txtM4.Value), Convert.ToDecimal(txtMb4.Value), Convert.ToDecimal(txtM5.Value), Convert.ToDecimal(txtMb5.Value), Convert.ToDecimal(txtM6.Value), Convert.ToDecimal(txtMb6.Value), Convert.ToDecimal(txtClassA1.Value), Convert.ToDecimal(txtClassA2.Value), Convert.ToDecimal(txtClassA3.Value), Convert.ToDecimal(txtClassP1.Value), Convert.ToDecimal(txtClassP2.Value), Convert.ToDecimal(txtClassP3.Value), Convert.ToDecimal(txtClassP4.Value), Convert.ToDecimal(txtClassP5.Value), Convert.ToDecimal(txtClassP6.Value), Convert.ToDecimal(txtClassM1.Value), Convert.ToDecimal(txtClassM2.Value), Convert.ToDecimal(txtClassM3.Value), Convert.ToDecimal(txtClassM4.Value), Convert.ToDecimal(txtClassM5.Value), Convert.ToDecimal(txtClassM6.Value));
            Response.Redirect("Income.aspx?ckmode=2&Cr=" + i);   
        }
    }
    private void Delete(string id)
    {
        if (String.IsNullOrEmpty(id)) return;
        Int32 i = Conn.Update("Income", "Where IncomeCode = '" + id + "' ", "DelFlag, UpdateUser, UpdateDate", 1, CurrentUser.ID, DateTime.Now);
        Response.Redirect("Income.aspx?ckmode=3&Cr=" + i);   
    }
    protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void LinkReport(Literal ltr, int num)
    {
        string link = " <a href=\"javascript:;\" " + btc.getLinkReportWEP("W") + "  onclick=\"printRpt(" + num + ",'w', '');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"50px;\" height=\"50px;\" title=\"เรียกดูรายงาน แบบเอกสาร Word\" src=\"../Image/icon/WordIcon.png\" /></a> "
                    + " <a href=\"javascript:;\" " + btc.getLinkReportWEP("E") + " onclick=\"printRpt(" + num + ",'e', '');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"45px;\" height=\"45px;\" title=\"เรียกดูรายงาน แบบเอกสาร Excel\" src=\"../Image/icon/ExcelIcon.png\" /></a> "
                    + " <a href=\"javascript:;\" " + btc.getLinkReportWEP("P") + " onclick=\"printRpt(" + num + ",'p', '');\"> "
                    + " <img style=\"border: 0; cursor: pointer; vertical-align: top;\" width=\"45px;\" height=\"45px;\" title=\"เรียกดูรายงาน แบบเอกสาร PDF\" src=\"../Image/icon/PdfIcon.png\" /></a> ";
        ltr.Text = link;
    }
}
