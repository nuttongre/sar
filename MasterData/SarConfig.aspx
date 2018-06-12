<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="SarConfig.aspx.cs" Inherits="SarConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript">
        function Cancel() {
            location.href = "SarManage.aspx";
        }
        function Cktxt(m) {
            var ck = 0;
            var txtUpdateLink = $get("<%=txtUpdateLink.ClientID%>");
            var ErrorUpdateLink = $get("ErrorUpdateLink");
            var txtLogSarLink = $get("<%=txtLogSarLink.ClientID%>");
            var ErrorLogSarLink = $get("ErrorLogSarLink");
            var txtForwardMail = $get("<%=txtForwardMail.ClientID%>");
            var ErrorForwardMail = $get("ErrorForwardMail");

            ck += ckTxtNull(m, txtForwardMail, ErrorForwardMail);
            ck += ckTxtNull(m, txtLogSarLink, ErrorLogSarLink);
            ck += ckTxtNull(m, txtUpdateLink, ErrorUpdateLink);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
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
                    ตั้งค่าระบบ
                </div>
                <div class="spaceDiv"></div>
                <div style="width: 100%;">
                    <div class="TableSearch" style="width: 500px; margin: 0 auto;">
                        <div class="SearchTable" style="float: left; border: solid 1px black; width: 630px;">
                            <div class="SearchHead">
                                <span class="spantxt2 spansize14">โหมดต่าง ๆ</span>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดโรงเรียนอนุบาล : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlAnuban" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rbtlAnuban_SelectedIndexChanged">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="DivAnubanMaster" class="inputrowH" runat="server" visible="false">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ใส่ข้อมูลพื้นฐาน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <span class="spantxt">ปีการศึกษา </span>
                                    <asp:DropDownList CssClass="ddl" ID="ddlYear" runat="server">
                                    </asp:DropDownList>
                                    <asp:Button ID="btCkYear" CssClass="btYes" runat="server" Text="       บันทึกด้าน/มาตรฐาน/ตัวชี้วัด" OnClick="btCkYear_Click" />
                                </div>
                                <div style="padding-left: 200px;">
                                    <asp:Label ID="lblAnuban" runat="server" ForeColor="Gray" Font-Size="12px" Text="***จะบันทึกได้ก็ต่อเมื่อปีที่เลือกนั้นไม่มีข้อมูลอยู่"></asp:Label>
                                </div>
                                <div class="spaceDiv"></div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดกลุ่มจุฬาภรณ์ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlJulaporn" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rbtlJulaporn_SelectedIndexChanged">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดสำหรับ Sale : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlSale" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                    </div>
                    <div style="float: left; width: 47%; padding-left: 20px;">
                        <div class="SearchTable" style="border: solid 1px black; width: 610px;">
                            <div class="SearchHead">
                                <span class="spantxt2 spansize14">การตั้งค่า</span>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ประเภทปี : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlYearType" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ปีการศึกษา" Value="0"></asp:ListItem>
                                        <asp:ListItem Text=" ปีงบประมาณ" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">รูปแบบรายงาน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:CheckBox ID="cbReportW" runat="server" Text=" Word" />
                                    <asp:CheckBox ID="cbReportE" runat="server" Text=" Excel" />
                                    <asp:CheckBox ID="cbReportP" runat="server" Text=" PDF" />
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ใบคำขอโครงการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:CheckBoxList ID="cblReportProject" runat="server" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="cblReportProject_SelectedIndexChanged"></asp:CheckBoxList>
                                    <asp:Label ID="lblReportProject" runat="server" ForeColor="Gray" Font-Size="12px" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ใบคำขอกิจกรรม : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:CheckBoxList ID="cblReportActivity" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">เอกลักษณ์โรงเรียน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlIdentity1" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox CssClass="txtBoxL txt300" TextMode="MultiLine" Height="50px" ID="txtIdentity"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed"></span>
                                    <span id="ErrorIdentity" class="ErrorMsg">กรุณาป้อนเอกลักษณ์โรงเรียน</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">อัตลักษณ์โรงเรียน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlIdentity2" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox CssClass="txtBoxL txt300" TextMode="MultiLine" Height="50px" ID="txtIdentity2"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed"></span>
                                    <span id="ErrorIdentity2" class="ErrorMsg">กรุณาป้อนอัตลักษณ์โรงเรียน</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">แผนพัฒนาระยะที่ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlPlanNo" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox CssClass="txtBoxNum txt50" MaxLength="3" ID="txtPlanNo" onkeypress="return KeyNumber(event);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">นโยบาย : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlPolicy" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">พระราชบัญญัติการศึกษา : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStatute" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">รายงานปัญหาโครงการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlResponseProblems" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ทรัพยากรที่ต้องการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlResource" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">กิจกรรมหลัก : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlMainActivity" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">เป้าหมายการพัฒนาคุณภาพด้าน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlTargetMode" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">แผนงาน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlPlan" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ลักษณะงาน / โครงการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlProjectDes" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดหลายกลยุทธ์ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStrategies" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดความเสี่ยงในกิจกรรม : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlRisk" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดหน่วยงานในโครงการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlDeptProjects" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ติดตามสถานะกิจกรรม : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlActivityStatus" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Gray" Font-Size="12px" Text="แสดง % การทำงานในแต่ละกิจกรรม"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ประเมินผลกิจกรรมแบบตาราง : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlAcAssessment" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดงบประมาณแยกเทอม : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtBudgetTerm" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมดสวนกุหลาบ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlSuan" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">โหมด 4 มาตรฐานใหม่ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlStandardNew" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">แสดงข้อมูลแบบ Full Text : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlFullText" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rbtlFullText_SelectedIndexChanged">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="lblExFullText" runat="server" ForeColor="Gray" Font-Size="12px" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt" style="font-size:15px;">ไม่แสดงรายเซ็นต์ผู้อนุมัติโครงการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlMgSignature" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">แบบประเมิน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlEstimate" runat="server" RepeatColumns="2" Enabled="false">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label1" runat="server" ForeColor="Gray" Font-Size="12px" Text="Coming Soon"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ต้นทุนศูนย์ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left">
                                        <asp:RadioButtonList ID="rbtlCostsCenter" runat="server" Enabled="false" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Gray" Font-Size="12px" Text="Coming Soon"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">Update Link : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:TextBox ID="txtUpdateLink" CssClass="txtBoxL" Width="300" TextMode="MultiLine" Height="50" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorUpdateLink" class="ErrorMsg">กรุณาป้อน Update Link</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">LogSar Link : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:TextBox ID="txtLogSarLink" CssClass="txtBoxL" Width="300" TextMode="MultiLine" Height="50" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorLogSarLink" class="ErrorMsg">กรุณาป้อน LogSar Link</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">Forward Mail : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:TextBox ID="txtForwardMail" CssClass="txtBoxL" Width="300" TextMode="MultiLine" Height="50" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorForwardMail" class="ErrorMsg">กรุณาป้อน Forward Mail</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ตำแหน่งผู้รับผิดชอบกิจกรรม : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:TextBox ID="txtPositionResponsible" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ตำแหน่งหัวหน้างาน/กลุ่มสาระฯ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:TextBox ID="txtPositionHeadGroupSara" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ตำแหน่งหัวหน้ากลุ่มงาน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:TextBox ID="txtPositionHeadGroup" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                    </div>
                    <div style="float: right; width: 47%; padding-right: 20px;">
                        <div class="SearchTable" style="border: solid 1px black; width: 610px;">
                            <div class="SearchHead">
                                <span class="spantxt2 spansize14">การเชื่อมโยง</span>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">มาตรฐานการศึกษาชาติ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStandardNation" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">มาตรฐานตามกฎกระทรวง : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStandardMinistry" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">มาตรฐานใหม่ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlNewStandard" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">วัตถุประสงค์เชิงกลยุทธ์ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStrategicObjectives" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">กลยุทธ์ สพฐ. : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStrategySPT" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span id="lblStrategySPM" class="spantxt" runat="server">กลยุทธ์ สพม. : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left;">
                                        <asp:RadioButtonList ID="rbtlStrategySPM" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="lblStrategySPMP" runat="server" ForeColor="Gray" Font-Size="12px" Text="สำนักงานเขตพื้นที่การศึกษามัธยมศึกษา"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">มาตรฐาน สพม. : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStandardSPM" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ตัวบ่งชี้ สมศ. : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlIndicatorSMS" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ยุทธศาสตร์ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStrategic" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">เกณฑ์รางวัลคุณภาพ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlQA" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">เกณฑ์ รร.มาตรฐานสากล : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlWorldClass" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">เกณฑ์ รร.รางวัลพระราชทาน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlRoyalAward" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ค่านิยม 12 ประการ : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left;">
                                        <asp:RadioButtonList ID="rbtlPopularity" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label7" runat="server" ForeColor="Gray" Font-Size="12px" Text="ของ คสช."></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">นโยบายนายกรัฐมนตรี : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left;">
                                        <asp:RadioButtonList ID="rbtlPrimePolicy" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label6" runat="server" ForeColor="Gray" Font-Size="12px" Text="โหมด กศน."></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">นโยบายรัฐมนตรีว่าการกระทรวง : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left;">
                                        <asp:RadioButtonList ID="rbtlPoliciesMinister" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label5" runat="server" ForeColor="Gray" Font-Size="12px" Text="โหมด กศน."></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">นโยบายการศึกษานอกระบบ กศน. : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <div style="float: left;">
                                        <asp:RadioButtonList ID="rbtlPolicyKSN" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div style="float: left; padding-left: 20px; padding-top: 4px; width: 500px;">
                                        <asp:Label ID="Label4" runat="server" ForeColor="Gray" Font-Size="12px" Text="โหมด กศน."></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">กลยุทธ์ระดับองค์กร : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlCorporateStrategy" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">กลยุทธ์ระดับแผนงาน : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlStrategicPlan" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">การศึกษาตามแนวทาง ศต.ที่ 21 : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlEducation21" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">หลักปรัชญาเศรษฐกิจพอเพียง : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlSuffice" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head" style="width: 250px;">
                                    <span class="spantxt">ประเมินคุณภาพภายนอกรอบ 4 : </span>
                                </div>
                                <div class="divB_Head" style="width: 350px;">
                                    <asp:RadioButtonList ID="rbtlFourthAssessment" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" ไม่ใช้งาน" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                    </div>
                    <div id="table1" class="PageManageDiv">
                        <div class="TableSearch" style="width: 500px; margin: 0 auto;">
                            <div class="SearchTable" style="float: left; border: solid 1px black; width: 630px;">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">ปรับปรุงเกณฑ์</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrowH">
                                    <div class="divF_Head" style="width: 250px;">
                                        <span class="spantxt">ปรับปรุงเกณฑ์ : </span>
                                    </div>
                                    <div class="divB_Head" style="width: 350px;">
                                        <asp:RadioButtonList ID="rbtlckRate" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rbtlckRate_SelectedIndexChanged">
                                            <asp:ListItem Text=" ใช้งาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text=" ไม่ใช้งาน" Value="0" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <asp:Panel ID="PanelRate" runat="server" Enabled="false">
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width: 250px;">
                                            <span class="spantxt">ปรับปรุงเกณฑ์ตามปี : </span>
                                        </div>
                                        <div class="divB_Head" style="width: 350px;">
                                            <asp:DropDownList CssClass="ddl" ID="ddlYearS" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width: 250px;">
                                            <span class="spantxt">ตารางเทียบเกณฑ์การประเมิน : </span>
                                        </div>
                                        <div class="divB_Head" style="width: 350px;">
                                            <asp:RadioButtonList ID="rbtlRate" runat="server">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="spaceDiv"></div>
                                <div class="centerDiv">
                                    <asp:Label ID="lblCreate" runat="server" CssClass="spantxt4"></asp:Label>
                                </div>
                                <div class="centerDiv">
                                    <asp:Label ID="lblUpdate" runat="server" CssClass="spantxt4"></asp:Label>
                                </div>
                                <div class="spaceDiv"></div>
                            </div>
                        </div>
                        <div class="centerDiv">
                            <div class="classButton">
                                <div class="classBtSave">
                                    <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       บันทึก" OnClientClick="return Cktxt(1);" OnClick="btSave_Click" ToolTip="บันทึกการตั้งค่านี้" />
                                </div>
                                <div class="classBtCancel">
                                    <input type="button" class="btNo" value="      ออก" title="ออกไปเมนูหลัก" onclick="Cancel();" />
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
