<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="MyAdmissionView.aspx.cs" Inherits="MyAdmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
       if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "MyAdmissionView.aspx?mode=3&id=" + id;
    }
    function AddItem()
    {
        location.href = "MyAdmissionView.aspx?mode=1";
    }
    function Cktxt()
    {
       if(document.getElementById("<%=ddlMyProject.ClientID%>").selectedIndex == 0)
        {
            document.getElementById("ErrorMyProject").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorMyProject").style.display = "none";
            return CkMyMainMenu();
            return true;
        }
    }
     function CkMyMainMenu()
    {
       if(document.getElementById("<%=ddlMyMainMenu.ClientID%>").selectedIndex == 0)
        {
            document.getElementById("ErrorMyMainMenu").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorMyMainMenu").style.display = "none";
            return CkMySubMenu();
            return true;
        }
    }
    function CkMySubMenu()
    {
        if(document.getElementById("<%=ddlMySubMenu.ClientID%>").selectedIndex == "")
        {
            document.getElementById("ErrorMySubMenu").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorMySubMenu").style.display = "none";
            return true;
        }
    }
    </script>

    <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Image id="Img1" runat="server" visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headTable">
                    <strong>�Է�������Ҷ֧����</strong>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="TableSearch" runat="server" width="100%">
                        <tr>
                            <td colspan="3" align="center">
                                <table class="tableSearch">
                                    <tr>
                                        <td colspan="3" class="headSearch">
                                            <span class="spantxt2 spansize14">�����Է�������Ҷ֧����</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 10px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <span class="spantxt">�к��ҹ : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true" Width="350"
                                                OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <span class="spantxt">������ѡ : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList ID="ddlSearch1" CssClass="ddlSearch" runat="server" AutoPostBack="true" Width="350"
                                                OnSelectedIndexChanged="ddlSearch1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <span class="spantxt">�Ӥ��� : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                                CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');" ID="btSearch" 
                                                runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 10px;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px;">
                            </td>
                        </tr>
                        <tr>
                            <td class="SearchB">
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       �����Է�������Ҷ֧��������"
                                    OnClientClick="AddItem();" OnClick="btAdd_Click" ToolTip="�����Է�������Ҷ֧��������" />
                            </td>
                            <td class="SearchtxtF">
                            </td>
                            <td class="SearchF">
                                 <span class="spantxt">�ӹǹ��辺 : </span><span id="lblSearchTotal" class="spantxt"
                                    style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">��¡��</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="table1" runat="server" style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">���͡�к��ҹ : </span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMyProject" CssClass="ddl" runat="server" AutoPostBack="true" Width="300"
                                    OnSelectedIndexChanged="ddlMyProject_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span id="ErrorMyProject" class="ErrorMsg">��س����͡�к��ҹ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">���͡������ѡ : </span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMyMainMenu" CssClass="ddl" runat="server" AutoPostBack="true" Width="300"
                                    OnSelectedIndexChanged="ddlMyMainMenu_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span id="ErrorMyMainMenu" class="ErrorMsg">��س����͡������ѡ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">���͡�������� : </span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMySubMenu" CssClass="ddl" runat="server" Width="300">
                                </asp:DropDownList>
                                <span id="ErrorMySubMenu" class="ErrorMsg">��س����͡��������</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%" valign="top">
                                <span class="spantxt">���������� : </span>
                            </td>
                            <td style="padding: 0 0 0 4px;">
                                <asp:CheckBox ID="cbCkAll" runat="server" Text="���͡������" OnCheckedChanged="cbCkAll_CheckedChanged"
                                    AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%" valign="top">
                            </td>
                            <td>
                                <asp:CheckBoxList ID="cblUserRole" runat="server">
                                </asp:CheckBoxList>
                                <asp:Label ID="lblErrorUserRole" runat="server" Text="��س����͡����������" Visible="false"
                                    Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡"
                                    OnClick="btSave_Click " OnClientClick="return Cktxt();" ToolTip="�ѹ�֡�Է�������Ҷ֧���ٹ��" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text ="       ���ѹ�֡"
                                    OnClick="btCancel_Click" ToolTip="���ѹ�֡�Է�������Ҷ֧���ٹ��" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%; height: 5px">
                </td>
            </tr>
            <tr>
                <td colspan ="2" style="padding:0 0 0 10px;">
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><img src="../Image/delete2.gif" title="ź��¡�÷�����͡" style="border:0;" /> ź��¡��</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; width: 100%;">
                    <Control:DataGridView ID="GridView1" runat="server" AutoGenerateCheckList="true">
                        <Columns>
                            <Control:TemplateField HeaderText="�ӴѺ���">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:BoundField HeaderText="������������" DataField="MySubMenuName">
                                <ItemStyle Width="45%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="�����������Է���" DataField="MyUserRoleName">
                                <ItemStyle Width="40%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="MyAdmissionView.aspx?mode=2&id=<%#Eval("MySubMenuID") %>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("MySubMenuID") %>');">
                                        <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" />
                    </Control:DataGridView>
                </td>
            </tr>
        </table>
    </ajax:AjaxPanel>
</asp:Content>
