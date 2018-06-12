<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Projects2.aspx.cs" Inherits="Projects2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Projects2.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Projects2.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('ต้องการลบรายการนี้ ใช่หรือไม่')) location.href = "Projects2.aspx?mode=3&id=" + id;
        }
        function gotoItem(id) {
            location.href = "Activity.aspx?mode=1&id=" + id;
        }
        function Cancel() {
            location.href = "Projects2.aspx";
        }
        function printRpt(mode, type, id) {
            var ckSing = $get("<%=cbNotSign.ClientID%>").checked;
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type + "&ckSign=" + ckSing);
        }
        function Cktxt(m) {
            var ck = 0;
            var ddlStrategies = $get("<%=ddlStrategies.ClientID%>");
            var ErrorStrategies = $get("ErrorStrategies");
            var txtActivityName = $get("<%=txtActivityName.ClientID%>");
            var ErrorActivityName = $get("ErrorActivityName");
            var txtActivitySubName = $get("<%=txtActivitySubName.ClientID%>");
            var ErrorActivitySubName = $get("ErrorActivitySubName");
            var txtProjectsDetail = $get("<%=txtProjectsDetail.ClientID%>");
            var ErrorProjectsDetail = $get("ErrorProjectsDetail");
            var txtProjects = $get("<%=txtProjects.ClientID%>");
            var ErrorProjects = $get("ErrorProjects");
            var txtPurpose = $get("<%=txtPurpose.ClientID%>");
            var ErrorPurpose = $get("ErrorPurpose");

            var txtResponsibleName = $get("<%=txtResponsibleName.ClientID%>");
            var txtResponsiblePosition = $get("<%=txtResponsiblePosition.ClientID%>");
            var ErrorResponsible = $get("ErrorResponsible");
            var txtProposeName = $get("<%=txtProposeName.ClientID%>");
            var txtProposePosition = $get("<%=txtProposePosition.ClientID%>");
            var ErrorPropose = $get("ErrorPropose");
            var txtApprovalName = $get("<%=txtApprovalName.ClientID%>");
            var txtApprovalPosition = $get("<%=txtApprovalPosition.ClientID%>");
            var ErrorApproval = $get("ErrorApproval");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckTxtNull(m, txtApprovalPosition, ErrorApproval);
            ck += ckTxtNull(m, txtApprovalName, ErrorApproval);
            ck += ckTxtNull(m, txtProposePosition, ErrorPropose);
            ck += ckTxtNull(m, txtProposeName, ErrorPropose);
            ck += ckTxtNull(m, txtResponsiblePosition, ErrorResponsible);
            ck += ckTxtNull(m, txtResponsibleName, ErrorResponsible);
            ck += ckTxtNull(m, txtPurpose, ErrorPurpose);
            ck += ckTxtNull(m, txtProjectsDetail, ErrorProjectsDetail);
            ck += ckTxtNull(m, txtProjects, ErrorProjects);
            ck += ckTxtNull(m, txtActivitySubName, ErrorActivitySubName);
            ck += ckTxtNull(m, txtActivityName, ErrorActivityName);
            ck += ckDdlNull(m, ddlStrategies, ErrorStrategies);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function ckAddMoney() {
            var ck = 0;
            if ($get("<%=txtMoneyDetail.ClientID%>").value == "") {
                ck += 1;
            }

            if ($get("<%=txtMoney.ClientID%>").value == "") {
                ck += 1;
            }

            if (ck > 0) {
                $get("ErrorMoney").style.display = "block";
                return false;
            }
            else {
                $get("ErrorMoney").style.display = "none";
                return true;
            }
        }
        function Moneyeditmode(id) {
            var MoneyNo = $get("spnMoneyNo" + id).innerHTML;
            var MoneyDetail = $get("spnMoneyDetail" + id).innerHTML;
            var Money = $get("spnMoney" + id).innerHTML;

            $get("<%=txtidMoney.ClientID%>").value = Trim(id);
        $get("<%=txtMoneyNo.ClientID%>").value = Trim(MoneyNo);
        $get("<%=txtMoneyDetail.ClientID%>").value = Trim(MoneyDetail);
        $get("<%=txtMoney.ClientID%>").value = Trim(Money);
    }
    function ckAddQuarter() {
        var ck = 0;
        if ($get("<%=txtQuarterSort.ClientID%>").value == "") {
            ck += 1;
        }

        if ($get("<%=txtQuarterTime.ClientID%>").value == "") {
            ck += 1;
        }

        if ($get("<%=txtQuarterPlan.ClientID%>").value == "") {
            ck += 1;
        }

        if ($get("<%=txtQuarterTarget.ClientID%>").value == "") {
            ck += 1;
        }

        if ($get("<%=txtQuarterBudget.ClientID%>").value == "") {
            ck += 1;
        }

        if (ck > 0) {
            $get("ErrorQuarter").style.display = "block";
            return false;
        }
        else {
            $get("ErrorQuarter").style.display = "none";
            return true;
        }
    }
    function Quartereditmode(id) {
        var QuarterNo = $get("spnQuarterNo" + id).innerHTML;
        var QuarterSort = $get("spnQuarterSort" + id).innerHTML;
        var QuarterTime = $get("spnQuarterTime" + id).innerHTML;
        var QuarterPlan = $get("spnQuarterPlan" + id).innerHTML;
        var QuarterTarget = $get("spnQuarterTarget" + id).innerHTML;
        var QuarterBudget = $get("spnQuarterBudget" + id).innerHTML;

        $get("<%=txtidQuarter.ClientID%>").value = Trim(id);
            $get("<%=ddlQuarter.ClientID%>").value = Trim(QuarterNo);
            $get("<%=txtQuarterSort.ClientID%>").value = Trim(QuarterSort);
            $get("<%=txtQuarterTime.ClientID%>").value = Trim(QuarterTime);
            $get("<%=txtQuarterPlan.ClientID%>").value = Trim(QuarterPlan);
            $get("<%=txtQuarterTarget.ClientID%>").value = Trim(QuarterTarget);
            $get("<%=txtQuarterBudget.ClientID%>").value = Trim(QuarterBudget);
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageDiv">
                <div class="warningDiv">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="headTable">
                    จัดทำโครงการ / งาน
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">ค้นหาโครงการ / งาน</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span id="lblYear" class="spantxt">ปีการศึกษา : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchYear" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">กลยุทธ์ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch2" Width="500px" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSearch2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">คำค้นหา : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="คลิ๊กเพื่อค้นหา" Text="  " />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="spaceDiv"></div>
                                <div class="SearchTotal">
                                    <span class="spantxt">จำนวนที่พบ : </span><span id="lblSearchTotal" class="spantxt"
                                        style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">รายการ</span>
                                </div>
                                <div class="spaceDiv"></div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="btAddDiv">
                                <input type="button" class="btAdd" onclick="AddItem();" value="      สร้างโครงการ / งานใหม่" title="สร้างโครงการ / งานใหม่" />
                            </div>
                            <div class="ckSign">
                                <asp:CheckBox ID="cbNotSign" runat="server" Text=" เลือกให้รายงานไม่มีส่วนเซ็นชื่อ"/>
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" ShowFooter="true">
                                <Columns>
                                    <Control:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ชื่อโครงการ / งาน">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("ProjectsCode").ToString(), Eval("EmpID").ToString(), Eval("ProjectsName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                                        <FooterTemplate>
                                            รวม :
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="งบประมาณ">
                                        <ItemTemplate>
                                            <%# GetBudget(decimal.Parse(Eval("TotalAmount").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <FooterTemplate>
                                            <%# GetTotalBudget().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="มฐ.การศึกษาชาติ" DataField="StandardNation">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="มฐ.ตามกฎกระทรวง" DataField="StandardMinistry">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="ผู้สร้างโครงการ" DataField="EmpName">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="ใบคำขอ">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="printRpt(39,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="เรียกดูใบคำขอโครงการรูปแบบที่ 1 แบบเอกสาร Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" onclick="printRpt(39,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="เรียกดูใบคำขอโครงการรูปแบบที่ 1 แบบเอกสาร PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="แก้ไข">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("ProjectsCode").ToString(), Eval("EmpID").ToString(), "")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ลบ">
                                        <ItemTemplate>
                                            <%# checkdelete(Eval("ProjectsCode").ToString(), Eval("EmpID").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ทำต่อ">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="gotoItem('<%#Eval("ProjectsCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="สร้างกิจกรรมใหม่ภายใต้โครงการนี้" src="../Image/goto.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" />
                            </Control:DataGridView>
                            <div class="footerTotal">
                                รวมงบประมาณทั้งหมด : <span id="ToltalBudget" class="lblTotal1" runat="server"></span>บาท
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                    <asp:View ID="edit" runat="server">
                        <div id="table1" class="PageManageDiv">
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ปีการศึกษา : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearS" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearS_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">แผนปฏิบัติงาน 4 ปี ของปี : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtYearPlan4" runat="server" Width="100px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">กลยุทธ์ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlStrategies" Width="600px" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStrategies" class="ErrorMsg">กรุณาเลือกกลยุทธ์</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ชื่อกิจกรรมหลัก : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtActivityName" runat="server" Width="600px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorActivityName" class="ErrorMsg">กรุณาป้อนชื่อกิจกรรม</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ชื่อกิจกรรมย่อย : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtActivitySubName" runat="server" Width="600px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorActivitySubName" class="ErrorMsg">กรุณาป้อนชื่อกิจกรรมย่อย</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">โครงการ / งาน : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProjects" runat="server" Width="600px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorProjects" class="ErrorMsg">กรุณาป้อนโครงการ/งาน</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">พันธกิจ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlMission" Width="600px" runat="server">
                                    </asp:DropDownList>
                                    <%--<span class="ColorRed">*</span>--%> <span id="ErrorMission" class="ErrorMsg">กรุณาเลือกพันธกิจ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">เป้าประสงค์ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlGoals" Width="600px" runat="server">
                                    </asp:DropDownList>
                                    <%--<span class="ColorRed">*</span>--%> <span id="ErrorGoals" class="ErrorMsg">กรุณาเลือกเป้าประสงค์</span>
                                </div>
                            </div>
                            <div id="divPlanNo" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPlanNo" CssClass="spantxt" runat="server" Text="แผนพัฒนาระยะที่ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum txt50" ID="txtPlanNo" runat="server" onkeypress="return KeyNumber(event);" MaxLength="3" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divIdentityName2" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblIdentityName2" CssClass="spantxt" runat="server" Text="อัตลักษณ์ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtIdentityName2" runat="server" Width="600px" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divIdentityName" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblIdentityName" CssClass="spantxt" runat="server" Text="เอกลักษณ์ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtIdentityName" runat="server" Width="600px" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divStatute" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStatute" CssClass="spantxt" runat="server" Text="พระราชบัญญัติการศึกษา มาตราที่ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtStatute" runat="server" Width="600px" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divStrategic" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategic" CssClass="spantxt" runat="server" Text="ยุทธศาสตร์ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategic" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divPolicy" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPolicy" CssClass="spantxt" runat="server" Text="นโยบาย :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPolicy" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStandardNation" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStandardNation" CssClass="spantxt" runat="server" Text="มาตรฐานการศึกษาชาติ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStandardNation" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStandardMinistry" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStandardMinistry" CssClass="spantxt" runat="server" Text="มาตรฐานตามกฎกระทรวง : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStandardMinistry" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStrategySPT" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategySPT" CssClass="spantxt" runat="server" Text="กลยุทธ์ สพฐ. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategySPT" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStrategySPM" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategySPM" CssClass="spantxt" runat="server" Text="กลยุทธ์ สพม. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategySPM" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStandardSPM" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStandardSPM" CssClass="spantxt" runat="server" Text="มาตรฐาน สพม. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStandardSPM" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div id="Div2" class="TableSearch">
                                <div class="SearchTable" style="width: 90%; height: auto;">
                                    <div class="SearchHead HeadCenter">
                                        <span class="spantxt2">งบประมาณที่ได้รับการจัดสรร</span>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">ลำดับที่ : </span>
                                            <asp:TextBox CssClass="txtBoxnone" ID="txtidMoney" runat="server" Width="0px"></asp:TextBox>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxNum" ID="txtMoneyNo"
                                                onkeypress="return KeyNumber(event);" runat="server" Text="0" onchange="txtComma(this);"
                                                onclick="SelecttxtAll(this);" onblur="txtZero(this,0);" Width="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">รายการ : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtMoneyDetail" runat="server" Width="400px"
                                                TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">จำนวนเงิน : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxNum" ID="txtMoney"
                                                onkeypress="return KeyNumber(event);" runat="server" Text="0" onchange="txtComma(this);"
                                                onclick="SelecttxtAll(this);" onblur="txtZero(this,0);" Width="150px"></asp:TextBox>
                                            <span class="spantxt">บาท</span>
                                            <asp:Button ID="btaddMoney" CssClass="btAdd" runat="server" OnClientClick="return ckAddMoney();"
                                                OnClick="btaddMoney_Click" Text="      เพิ่มรายการ" ToolTip="เพิ่มรายการใหม่" />
                                            <span id="ErrorMoney" class="ErrorMsg">กรุณาป้อนข้อมูลให้ครบ</span>
                                            <asp:Label ID="lblckMoney" runat="server" Text="รายการซ้ำ" ForeColor="Red"
                                                Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Button ID="btDelMoney" CssClass="btDelete" runat="server" Visible="false" OnClick="btDelMoney_Click" />
                                    </div>
                                    <div class="centerDiv">
                                        <Control:DataGridView ID="GridViewMoney" runat="server" AutoGenerateCheckList="true"
                                            Width="100%" AutoGenerateColumns="False" PageSize="20" ShowFooter="true">
                                            <Columns>
                                                <Control:TemplateField HeaderText="ที่">
                                                    <ItemTemplate>
                                                        <span id="spnMoneyNo<%#Container.DataItemIndex %>">
                                                            <%# Eval("MoneyNo").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="รายการ">
                                                    <ItemTemplate>
                                                        <span id="spnMoneyDetail<%#Container.DataItemIndex %>">
                                                            <%# Eval("MoneyDetail").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="60%" />
                                                    <FooterTemplate>
                                                        งบประมาณที่ได้รับการจัดสรร :
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="งบประมาณ">
                                                    <ItemTemplate>
                                                        <span id="spnMoney<%#Container.DataItemIndex %>">
                                                            <%# GetMoney(decimal.Parse(Eval("Money").ToString())).ToString("N2")%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="30%" />
                                                    <FooterTemplate>
                                                        <%# GetTotalMoney().ToString("N2")%>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="แก้ไข">
                                                    <ItemTemplate>
                                                        <img src="../Image/edit.gif" style="cursor: pointer;" onclick="Moneyeditmode('<%#Container.DataItemIndex %>');" title="เลือกเพื่อแก้ไข" alt="เลือกเพื่อแก้ไข" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                            </Columns>
                                        </Control:DataGridView>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="spaceDiv"></div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ภาระกิจ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProjectsDetail" runat="server" Width="600px"
                                        TextMode="MultiLine" Height="150px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorProjectsDetail" class="ErrorMsg">กรุณาป้อนภาระกิจ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ตัวชี้วัดความสำเร็จ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtPurpose" runat="server" Width="600px" TextMode="MultiLine"
                                        Height="150px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorPurpose" class="ErrorMsg">กรุณาป้อนตัวชี้วัดความสำเร็จ</span>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div id="Div3" class="TableSearch">
                                <div class="SearchTable" style="width: 90%; height: auto;">
                                    <div class="SearchHead HeadCenter">
                                        <span class="spantxt2">แผนการดำเนินงานแต่ละไตรมาส</span>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">ไตรมาสที่ : </span>
                                            <asp:TextBox CssClass="txtBoxnone" ID="txtidQuarter" runat="server" Width="0px"></asp:TextBox>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:DropDownList ID="ddlQuarter" CssClass="ddl" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">ลำดับที่ : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxNum" ID="txtQuarterSort"
                                                onkeypress="return KeyNumber(event);" runat="server" Text="0" onchange="txtComma(this);"
                                                onclick="SelecttxtAll(this);" onblur="txtZero(this,0);" Width="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">ช่วงวันที่ : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtQuarterTime" runat="server" Width="400px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">แผนปฏิบัติงาน : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtQuarterPlan" runat="server" Width="400px"
                                                TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">เป้าหมาย : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtQuarterTarget" runat="server" Width="400px"
                                                TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">งบประมาณ : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxNum" ID="txtQuarterBudget"
                                                onkeypress="return KeyNumber(event);" runat="server" Text="0" onchange="txtComma(this);"
                                                onclick="SelecttxtAll(this);" onblur="txtZero(this,0);" Width="150px"></asp:TextBox>
                                            <span class="spantxt">บาท</span>
                                            <asp:Button ID="btaddQuarter" CssClass="btAdd" runat="server" OnClientClick="return ckAddQuarter();"
                                                OnClick="btaddQuarter_Click" Text="      เพิ่มรายการ" ToolTip="เพิ่มรายการใหม่" />
                                            <span id="ErrorQuarter" class="ErrorMsg">กรุณาป้อนข้อมูลให้ครบ</span>
                                            <asp:Label ID="lblckQuarter" runat="server" Text="รายการซ้ำ" ForeColor="Red"
                                                Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Button ID="btDelQuarter" CssClass="btDelete" runat="server" Visible="false"
                                            OnClick="btDelQuarter_Click" />
                                    </div>
                                    <div class="centerDiv">
                                        <Control:DataGridView ID="GridViewQuarter" runat="server" AutoGenerateCheckList="true"
                                            Width="100%" AutoGenerateColumns="False" PageSize="20" ShowFooter="true">
                                            <Columns>
                                                <Control:TemplateField HeaderText="ไตรมาส">
                                                    <ItemTemplate>
                                                        <span id="spnQuarterNo<%#Container.DataItemIndex %>">
                                                            <%# Eval("QuarterNo").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="ที่">
                                                    <ItemTemplate>
                                                        <span id="spnQuarterSort<%#Container.DataItemIndex %>">
                                                            <%# Eval("QuarterSort").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" Width="5%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="ช่วงวันที่">
                                                    <ItemTemplate>
                                                        <span id="spnQuarterTime<%#Container.DataItemIndex %>">
                                                            <%# Eval("QuarterTime").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="แผนปฏิบัติงาน">
                                                    <ItemTemplate>
                                                        <span id="spnQuarterPlan<%#Container.DataItemIndex %>">
                                                            <%# Eval("QuarterPlan").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="เป้าหมาย">
                                                    <ItemTemplate>
                                                        <span id="spnQuarterTarget<%#Container.DataItemIndex %>">
                                                            <%# Eval("QuarterTarget").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                    <FooterTemplate>
                                                        งบประมาณรวม :
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="งบประมาณ">
                                                    <ItemTemplate>
                                                        <span id="spnQuarterBudget<%#Container.DataItemIndex %>">
                                                            <%# GetQuarterBudget(decimal.Parse(Eval("QuarterBudget").ToString())).ToString("N2")%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                    <FooterTemplate>
                                                        <%# GetTotalQuarterBudget().ToString("N2")%>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="แก้ไข">
                                                    <ItemTemplate>
                                                        <img src="../Image/edit.gif" style="cursor: pointer;" onclick="Quartereditmode('<%#Container.DataItemIndex %>');" title="เลือกเพื่อแก้ไข" alt="เลือกเพื่อแก้ไข" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                                        BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                            </Columns>
                                        </Control:DataGridView>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="spaceDiv"></div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ผู้รับผิดชอบโครงการ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtResponsibleName" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">ตำแหน่ง : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtResponsiblePosition" runat="server" Width="270px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorResponsible" class="ErrorMsg">กรุณาป้อนข้อมูลผู้รับผิดชอบโครงการ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ผู้เสนอโครงการ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProposeName" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">ตำแหน่ง : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProposePosition" runat="server" Width="270px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorPropose" class="ErrorMsg">กรุณาป้อนข้อมูลผู้เสนอโครงการ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ผู้เห็นชอบโครงการ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalName" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">ตำแหน่ง : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalPosition" runat="server" Width="270px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorApproval" class="ErrorMsg">กรุณาป้อนข้อมูลผู้เห็นชอบโครงการ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ผู้เห็นชอบโครงการที่ 2 : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalName2" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">ตำแหน่ง : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalPosition2" runat="server" Width="270px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ลำดับที่ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);"
                                        Width="50px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">กรุณาป้อนลำดับที่</span>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <asp:Label ID="lblCreate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="centerDiv">
                                <asp:Label ID="lblUpdate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <div class="classButton">
                                    <div class="classBtSave">
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       บันทึก" OnClick="btSave_Click"
                                            OnClientClick="return Cktxt(1);" ToolTip="บันทึกโครงการ/งานนี้" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       บันทึกและสร้างโครงการ/งานใหม่"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="บันทึกโครงการ/งานนี้และสร้างโครงการ/งานใหม่" />
                                        <asp:Button ID="btSaveAndGo" CssClass="btYesAndGo" runat="server" Text="       บันทึกและสร้างกิจกรรมต่อ"
                                            OnClick="btSaveAndGo_Click" OnClientClick="return Cktxt(1);" ToolTip="บันทึกโครงการ/งานนี้และสร้างกิจกรรมต่อ" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ไม่บันทึก" title="ไม่บันทึกโครงการ/งานนี้" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                                    <Columns>
                                        <Control:TemplateField HeaderText="ลำดับที่">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        </Control:TemplateField>
                                        <Control:BoundField HeaderText="ชื่อโครงการ / งาน" DataField="ProjectsName">
                                            <ItemStyle Width="80%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="งบประมาณ" DataField="TotalAmount" DataFormatString="{0:n2}">
                                            <ItemStyle Width="15%" HorizontalAlign="Right" />
                                        </Control:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </Control:DataGridView>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
