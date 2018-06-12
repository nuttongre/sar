<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="SarManage.aspx.cs" Inherits="SarManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ckPassword() {
            if (document.getElementById("<%=txtPassword.ClientID%>").value == "") {
                document.getElementById("ErrorPassword").style.display = "block";
                return false;
            }
            else {
                document.getElementById("ErrorPassword").style.display = "none";
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
                    ��駤������Ѻ���˹�ҷ��
                </div>
                <div class="spaceDiv"></div>
                <div id="table" class="PageManageDiv">
                    <div class="inputrowH">
                        <div class="divF_Head">
                            <span class="spantxt">��͹���ʼ�ҹ����Ѻ���˹�ҷ�� : </span>
                        </div>
                        <div class="divB_Head">
                            <asp:TextBox CssClass="logintxt" TextMode="Password" ID="txtPassword"
                                runat="server"></asp:TextBox>&nbsp;
                            <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       ��ŧ" OnClick="btSave_Click" OnClientClick="return ckPassword();" ToolTip="��ŧ" />
                            <span id="ErrorPassword" class="ErrorMsg">��سһ�͹���ʼ�ҹ</span>
                            <asp:Label ID="lblCkPassword" ForeColor="Red" runat="server" Visible="false" Text="���ʼ�ҹ���١��ͧ (˹�ҹ������੾�����˹�ҷ����ҹ��)"></asp:Label>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="spaceDiv"></div>
                    <div id="tblMenu" class="SearchTable" runat="server" visible="false">
                        <div class="SearchHead">
                            <span class="spantxt2 spansize14">���٨Ѵ��õ�ҧ �</span>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link4" runat="server">
                                    <img src="../Image/MenuStlye/S0.png" style="border: none; padding-right: 5px;" />
                                    ��駤���к�</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link1" runat="server">
                                    <img src="../Image/MenuStlye/S4.png" style="border: none; padding-right: 5px;" />
                                    ��ª����ç���¹���Դ����к�</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link2" runat="server">
                                    <img src="../Image/MenuStlye/S7.png" style="border: none; padding-right: 5px;" />
                                    ͹��ѵԡԨ����������</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link5" runat="server">
                                    <img src="../Image/MenuStlye/S2.png" style="border: none; padding-right: 5px;" />
                                    ź��¡�÷�����</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link6" runat="server">
                                    <img src="../Image/MenuStlye/delete.gif" style="border: none; padding-right: 5px;" />
                                    ź��ª��ͼ����ҹ</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link7" runat="server">
                                    <img src="../Image/MenuStlye/S001.png" style="border: none; padding-right: 5px;" />
                                    ź��Ǫ���Ѵ����ҧ</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link3" runat="server">
                                    <img src="../Image/MenuStlye/S07.gif" style="border: none; padding-right: 5px;" />
                                    �Ѵ����Է�������</a>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <a id="Link8" runat="server">
                                    <img src="../Image/MenuStlye/S09.png" style="border: none; padding-right: 5px;" />
                                    �����ʼ�ҹ</a>
                            </div>
                        </div>
                        <div class="spaceDiv"></div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
