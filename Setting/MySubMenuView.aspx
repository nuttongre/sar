<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="MySubMenuView.aspx.cs" Inherits="MySubMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
       if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "MySubMenuView.aspx?mode=3&id=" + id;
    }
    function AddItem()
    {
        location.href = "MySubMenuView.aspx?mode=1";
    }
    function Cktxt()
    {
        if(document.getElementById("<%=txtMySubMenuName.ClientID%>").value == "")
        {
            document.getElementById("ErrorMySubMenuName").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorMySubMenuName").style.display = "none";
            return CkMyProject();
            return true;
        }
    }
    function CkMyProject()
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
            return CkLinkName();
            return true;
        }
    }
    function CkLinkName()
    {
        if(document.getElementById("<%=txtLinkName.ClientID%>").value == "")
        {
            document.getElementById("ErrorLinkName").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorLinkName").style.display = "none";
            return CkSort();
            return true;
        }
    }
    function CkSort()
    {
        if(document.getElementById("<%=txtSort.ClientID%>").value == "")
        {
            document.getElementById("ErrorSort").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorSort").style.display = "none";
            return true;
        }
    }
    </script>

    <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headTable">
                    <strong>��������</strong>
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
                                            <span class="spantxt2 spansize14">������������</span>
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
                                            <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" Width="350"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
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
                                            <asp:DropDownList ID="ddlSearch1" CssClass="ddlSearch" runat="server" Width="350"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSearch1_SelectedIndexChanged">
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
                                                CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                                ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" />
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" OnClientClick="AddItem();" Text="       ����������������"
                                    OnClick="btAdd_Click" ToolTip="����������������" />
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
                                <span class="spantxt">������������ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtMySubMenuName" runat="server" Width="300px"></asp:TextBox>
                                <span id="ErrorMySubMenuName" class="ErrorMsg">��سһ�͹������������</span>
                            </td>
                        </tr>
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
                                <span class="spantxt">����˹���������� : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtLinkName" runat="server" Width="300px"></asp:TextBox>
                                <span id="ErrorLinkName" class="ErrorMsg">��سһ�͹����˹����������</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ӴѺ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);"
                                    Width="50px"></asp:TextBox>
                                <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡"
                                    OnClick="btSave_Click " OnClientClick="return Cktxt();" ToolTip="�ѹ�֡�������¹��" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text="       ���ѹ�֡"
                                    OnClick="btCancel_Click" ToolTip="���ѹ�֡�������¹��" />
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
                <td colspan="2" style="text-align: center; width: 100%;">
                    <Control:DataGridView ID="GridView1" runat="server">
                        <Columns>
                            <Control:TemplateField HeaderText="�ӴѺ���">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            </Control:TemplateField>
                            <Control:BoundField HeaderText="������������" DataField="MySubMenuName">
                                <ItemStyle Width="60%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="����������ѡ" DataField="MyMainMenuName">
                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="�ʴ�" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img style="cursor: pointer" title="<%# Convert.ToBoolean(Eval("Status")) ? "�ʴ�" : "����ʴ�" %>"
                                        src="../image/<%# Convert.ToBoolean(Eval("Status")) ? "ok" : "no" %>.png" border="0"
                                        onclick="javascript:Enabled('<%# Eval("MySubMenuID") %>','<%# Eval("Status") %>','1')" />
                                </ItemTemplate>
                                <ItemStyle Width="12%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="MySubMenuView.aspx?mode=2&id=<%#Eval("MySubMenuID") %>">
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
