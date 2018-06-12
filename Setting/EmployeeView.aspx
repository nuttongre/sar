<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="EmployeeView.aspx.cs" Inherits="EmployeeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "EmployeeView.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "EmployeeView.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('ต้องการลบรายการนี้ ใช่หรือไม่')) location.href = "EmployeeView.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "EmployeeView.aspx";
        }
        function printRpt(mode, type, id) {
            var DeptID = $get("<%=ddlSearchDept.ClientID%>").value;
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&deptid=" + DeptID + "&rpttype=" + type);
        }

        function HideItem(id, status) {

            var str = "ปิด";
            if (status == 1) {
                str = "เปิด";
            }
            if (confirm('ต้องการ' + str + 'การใช้งานผู้ใช้รายนี้ ใช่หรือไม่')) location.href = "EmployeeView.aspx?mode=5&id=" + id + "&sts=" + status;
        }
        function resetItem(id) {
            if (confirm('ต้องการ Reset Password รายการนี้ ใช่หรือไม่')) location.href = "EmployeeView.aspx?mode=4&id=" + id;
        }
        function Cktxt(m) {
            var ck = 0;
            var txtUserName = $get("<%=txtUserName.ClientID%>");
            var ErrorUserName = $get("ErrorUserName");
            var txtName = $get("<%=txtName.ClientID%>");
            var ErrorName = $get("ErrorName");
            var txtEmail = $get("<%=txtEmail.ClientID%>");
            var ErrorEmail = $get("ErrorEmail");
            var txtTel = $get("<%=txtTel.ClientID%>");
            var ErrorTel = $get("ErrorTel");
            var ddlSchool = $get("<%=ddlSchool.ClientID%>");
            var ErrorSchool = $get("ErrorSchool");
            var ddlDept = $get("<%=ddlDept.ClientID%>");
            var ErrorDepartment = $get("ErrorDepartment");
            var ddlUserRole = $get("<%=ddlUserRole.ClientID%>");
            var ErrorUserRole = $get("ErrorUserRole");
            var txtPwd = $get("<%=txtPwd.ClientID%>");
            var ErrorPassword = $get("ErrorPassword");
            var txtConfirmPwd = $get("<%=txtConfirmPwd.ClientID%>");
            var ErrorConfirmPwd = $get("ErrorConfirmPwd");

            if (Request("mode") == "1") {
                ck += ckTxtNull(m, txtConfirmPwd, ErrorConfirmPwd);
                ck += ckTxtNull(m, txtPwd, ErrorPassword);

                if ($get("<%=txtPwd.ClientID%>").value != $get("<%=txtConfirmPwd.ClientID%>").value) {
                    $get("ErrorPasswordAndConfirm").style.display = "block";
                    ck += 1;
                }
                else {
                    $get("ErrorPasswordAndConfirm").style.display = "none";
                }
            }
            ck += ckDdlNull(m, ddlUserRole, ErrorUserRole);
            ck += ckDdlNull(m, ddlDept, ErrorDepartment);
            ck += ckDdlNull(m, ddlSchool, ErrorSchool);
            ck += ckTxtNull(m, txtTel, ErrorTel);
            ck += ckTxtNull(m, txtEmail, ErrorEmail);
            ck += ckTxtNull(m, txtName, ErrorName);          
            ck += ckTxtNull(m, txtUserName, ErrorUserName);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function ImgWait() {
            if ($get("<%=txtUserName.ClientID%>").value != "") {
                $get("ImgWait").style.display = "block";
                setTimeout("$get('ImgWait').style.display = 'none'", 500)
                setTimeout("$get('<%=lblErrorUserName.ClientID%>').style.display = 'block'", 500);
            }
            else {
                $get("<%=lblErrorUserName.ClientID%>").style.display = "none";
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
                    ผู้ใช้งานระบบ
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">ค้นหาผู้ใช้งานระบบ</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">กลุ่มผู้ใช้ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" Width="350" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">หน่วยงาน : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" Width="350" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged">
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
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="คลิ๊กเพื่อค้นหา" Text=" " />
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">พิมพ์รายงาน : </span>
                                    </div>
                                    <div class="SearchF">
                                        <a href="javascript:;" onclick="printRpt(28,'w');">
                                            <img style="border: 0; cursor: pointer; vertical-align: top;" width="50px;" height="50px;" title="เรียกดูรายงาน แบบเอกสาร Word" src="../Image/icon/WordIcon.png" /></a>
                                        <a href="javascript:;" onclick="printRpt(28,'p');">
                                            <img style="border: 0; cursor: pointer; vertical-align: top;" width="45px;" height="45px;" title="เรียกดูรายงาน แบบเอกสาร PDF" src="../Image/icon/PdfIcon.png" /></a>
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="spaceDiv"></div>
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" OnClientClick="AddItem();" Text="       เพิ่มผู้ใช้งานระบบใหม่"
                                    ToolTip="เพิ่มผู้ใช้งานระบบใหม่" />
                            </div>
                            <div style="float: right; padding-right: 20px;">
                                <div style="float: right">
                                    |&nbsp;<a id="spnDownLoadFile" runat="server" title="Download File Excel ต้นฉบับ" href="../Doc/ImportEmpData.xls"><img src="../Image/dwnExcel.png" style="border: none; width: 25px; height: 24px;" />
                                        Download</a>&nbsp;|&nbsp;<a id="spnImportExcel" href="InputUsers.aspx" runat="server" title="นำเข้าข้อมูลผู้ใช้ระบบจาก Excel"><img src="../Image/excelicon.png" style="border: none; width: 25px; height: 24px;" />
                                            นำเข้าข้อมูลผู้ใช้</a>&nbsp;|
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:BoundField DataField="UserName" HeaderText="UserName">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="ชื่อ">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("EmpID") %>');">
                                                <%#Eval("EmpName") %></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </Control:TemplateField>
                                    <Control:BoundField DataField="DeptName" HeaderText="หน่วยงาน" SortExpression="DeptName">
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:BoundField DataField="UserRoleName" HeaderText="กลุ่มผู้ใช้" SortExpression="DeptName">
                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="แก้ไข">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("EmpID") %>');">
                                                <img style="border: 0; cursor: pointer;" title="แก้ไข" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="สถานะ">
                                        <ItemTemplate>
                                            <%# linkHide(Eval("EmpID").ToString(), Convert.ToBoolean(Eval("HideFlag")))%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ลบ">
                                        <ItemTemplate>
                                            <%# linkDel(Eval("EmpID").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="Reset Pwd">
                                        <ItemTemplate>
                                            <a href="javascript:resetItem('<%#Eval("EmpID") %>');">
                                                <img style="border: 0; cursor: pointer;" title="Reset Password" src="../Image/reset.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" />
                            </Control:DataGridView>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                    <asp:View ID="edit" runat="server">
                        <div id="table1" class="PageManageDiv">
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ชื่อผู้ใช้ :</span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt150" ID="txtUserName" runat="server" MaxLength="20"></asp:TextBox><img id="ImgWait" alt="" src="../Image/waitiCon.gif" style="display: none; float: left;" />
                                    <span class="ColorRed">*</span> <span id="ErrorUserName" class="ErrorMsg">กรุณาป้อนชื่อผู้ใช้</span>
                                    <asp:Label ID="lblErrorUserName" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="btCkEmp" runat="server" CssClass="btNone" OnClick="btCkEmp_Click" />
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ชื่อ - นามสกุล :</span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt200" ID="txtName" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorName" class="ErrorMsg">กรุณาป้อน ชื่อ-นามสกุล</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">E-mail :</span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt200" ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorEmail" class="ErrorMsg">กรุณาป้อน E-mail</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">เบอร์โทรศัพท์ :</span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt100" ID="txtTel" runat="server" MaxLength="50"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorTel" class="ErrorMsg">กรุณาป้อนเบอร์โทรศัพท์</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">โรงเรียน :</span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlSchool" runat="server" Width="300">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorSchool" class="ErrorMsg">กรุณาเลือกโรงเรียน</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">หน่วยงานหลัก :</span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlDept" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorDepartment" class="ErrorMsg">กรุณาเลือกหน่วยงาน</span>
                                </div>
                            </div>
                            <div class="inputrowH" style="height: auto;">
                                <div class="divF_Head">
                                    <span class="spantxt">หน่วยงานอื่นๆ (ถ้ามี) :</span>
                                </div>
                                <div class="divB_Head" style="height: auto;">
                                    <asp:CheckBoxList ID="cblDept" runat="server"></asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="inputrowH" style="height: auto;">
                                <div class="divF_Head">
                                    <span class="spantxt">กลุ่มผู้ใช้ :</span>
                                </div>
                                <div class="divB_Head" style="height: auto;">
                                    <asp:DropDownList CssClass="ddl" ID="ddlUserRole" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="ddlUserRole_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorUserRole" class="ErrorMsg">กรุณาเลือกกลุ่มผู้ใช้</span>
                                </div>
                            </div>
                            <div id="tblEditPwd" style="float: left; width: 100%;" runat="server">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">รหัสผ่าน :</span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:TextBox CssClass="txtBoxL txt150" ID="txtPwd" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                                        <span class="ColorRed">*</span> <span id="ErrorPassword" class="ErrorMsg">กรุณาป้อนรหัสผ่าน</span>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">ยืนยันรหัสผ่าน :</span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:TextBox CssClass="txtBoxL txt150" ID="txtConfirmPwd" runat="server" TextMode="Password"
                                            MaxLength="20"></asp:TextBox>
                                        <span class="ColorRed">*</span> <span id="ErrorConfirmPwd" class="ErrorMsg">กรุณาใส่รหัสผ่านอีกครั้ง</span>
                                        <span id="ErrorPasswordAndConfirm" class="ErrorMsg">ยืนยันรหัสผ่านไม่ถูกต้อง</span>
                                    </div>
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
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       บันทึก"
                                            OnClick="btSave_Click" OnClientClick="return Cktxt(1);" ToolTip="บันทึกผู้ใช้งานระบบนี้" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ไม่บันทึก" title="ไม่บันทึกผู้ใช้งานระบบนี้" onclick="Cancel();" />
                                    </div>
                                </div>
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
