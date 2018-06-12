<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="UserRoleView.aspx.cs" Inherits="UserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
       if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "UserRoleView.aspx?mode=3&id=" + id;
    }
    function AddItem()
    {
        location.href = "UserRoleView.aspx?mode=1";
    }
    function Cktxt()
    {
        if(document.getElementById("<%=txtUserRole.ClientID%>").value == "")
        {
            document.getElementById("ErrorUserRole").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorUserRole").style.display = "none";
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
                    <asp:Image id="Img1" runat="server" visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headTable">
                    <strong>����������ҹ�к�</strong>
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
                                            <span class="spantxt2 spansize14">���ҡ���������ҹ�к�</span>
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       ���ҧ����������ҹ����"
                                    OnClientClick="AddItem();" OnClick="btAdd_Click" ToolTip="���ҧ����������ҹ�к�����" />
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
                    <table id="table1" runat="server" width="100%">
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">���͡��������� : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtUserRole" runat="server" Width="300px"></asp:TextBox>
                                <span id="ErrorUserRole" class="ErrorMsg">��سһ�͹���͡���������</span>
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
                                <span class="spantxt">��˹��Է��� : </span>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbAdmin" runat="server" Text="�Է���������к�" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td>
                                <asp:CheckBox ID="cbAllDept" runat="server" Text="�Է�����繷ء˹��§ҹ" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td>
                                <asp:CheckBox ID="cbDept" runat="server" Text="�Է���������˹��§ҹ" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td>
                                <asp:CheckBox ID="cbManager" runat="server" Text="�Է����������" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td>
                                <asp:CheckBox ID="cbHeadDept" runat="server" Text="�Է������˹��˹��§ҹ" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡"
                                    OnClick="btSave_Click " OnClientClick="return Cktxt();" ToolTip="�ѹ�֡����������ҹ�к����" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text ="       ���ѹ�֡"
                                    OnClick="btCancel_Click" ToolTip="���ѹ�֡����������ҹ�к����" />
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
                            <Control:BoundField DataField="UserRoleName" HeaderText="���͡���������">
                                <ItemStyle Width="80%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="UserRoleView.aspx?mode=2&id=<%#Eval("UserRoleID") %>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("UserRoleID") %>');">
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
