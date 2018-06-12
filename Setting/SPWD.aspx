<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="SPWD.aspx.cs" Inherits="SPWD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function Cancel() {
            location.href = "../MasterData/SarManage.aspx";
        }
        function Cktxt(m) {
            var ck = 0;

            var txtUserName = $get("<%=txtUserName.ClientID%>");
            var ErrorUserName = $get("ErrorUserName");

            ck += ckTxtNull(m, txtUserName, ErrorUserName);

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
                    แสดงรหัสผ่าน
                </div>
                <div class="spaceDiv"></div>
                <div id="table1" class="PageManageDiv">
                    <div class="inputrowH">
                        <div class="divF_Head" style="width:45%;">
                            <span class="spantxt">ชื่อผู้ใช้งาน :</span>
                        </div>
                        <div class="divB_Head" style="width:45%;">
                            <asp:TextBox CssClass="txtBox txt200" ID="txtUserName" runat="server"></asp:TextBox>
                            <span class="ErrorMsg">*</span> <span id="ErrorUserName" class="ErrorMsg">กรุณาป้อนชื่อผู้ใช้งาน</span>
                        </div>
                    </div>
                    <div class="spaceDiv"></div>
                    <div class="centerDiv">
                        <div class="classButton">
                            <div class="classBtSave">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       ยืนยัน"
                                    OnClick="btSave_Click" OnClientClick="return Cktxt(1);" ToolTip="ยืนยัน" />
                            </div>
                            <div class="classBtCancel">
                                <input type="button" onclick="Cancel();" class="btNo" value="       ยกเลิก"/>
                            </div>
                        </div>
                    </div>
                    <div class="spaceDiv"></div>
                    <div id="DivPwd" class="inputrowH" runat="server" visible="false">
                        <div class="divF_Head" style="width: 45%;">
                            <span class="spantxt">รหัสผ่าน :</span>
                        </div>
                        <div class="divB_Head" style="width: 45%;">
                            <asp:TextBox CssClass="txtBox txt200" ID="txtPassword" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="centerDiv">
                        <asp:Label ID="lblNotUser" runat="server" Text="ไม่พบชื่อผู้ใช้งานนี้" ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                    <div class="clear"></div>
                    <div class="spaceDiv"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
