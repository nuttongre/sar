<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="CostsCenter.aspx.cs" Inherits="CostsCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href="CostsCenter.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href="CostsCenter.aspx?mode=1";
    }
    function Cktxt()
    {
        if(document.getElementById("<%=txtCostsName.ClientID%>").value == "")
        {
            document.getElementById("ErrorCostsName").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorCostsName").style.display = "none";
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
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headTable">
                    �鹷ع�ٹ��
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
                                            <span class="spantxt2 spansize14">���ҵ鹷ع�ٹ��</span>
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
                                            <span class="spantxt">�ա���֡�� : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchYear" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged">
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
                                            <asp:TextBox CssClass="txtSearch txt300" ID="txtSearch" runat="server"></asp:TextBox><asp:Button
                                                CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                                ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip = "�������ͤ���" Text="  " />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <asp:Label ID="lblCopy" CssClass="spantxt" runat="server" Text="�Ѵ�͡�ҡ�� : "></asp:Label>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList CssClass="ddlSearch" ID="ddlOldYear" runat="server">
                                            </asp:DropDownList>
                                            <asp:Button ID="btCopy" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btCopy_Click" 
                                                ToolTip="�ѹ�֡��äѴ�͡" />
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" OnClientClick="AddItem();"
                                    OnClick="btAdd_Click" Text="       ���ҧ�鹷ع�ٹ������" ToolTip="���ҧ�鹷ع�ٹ������" />
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
                <td colspan="2" style="width: 100%;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="table1" runat="server" width="100%">
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ա���֡�� : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYearB_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">���͵鹷ع�ٹ�� : </span>
                            </td>
                            <td class="B_Head">
                                <asp:TextBox ID="txtCostsName" CssClass="txtBoxL txt500" runat="server"></asp:TextBox>&nbsp;<span
                                    class="ColorRed">*</span> <span id="ErrorCostsName" class="ErrorMsg">��سһ�͹�鹷ع�ٹ��</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">��������´ : </span>
                            </td>
                            <td class="B_Head">
                                <asp:TextBox ID="txtCostsDetail" CssClass="txtBoxL txt500" TextMode="MultiLine" Height="150px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">�ӴѺ��� : </span>
                            </td>
                            <td class="B_Head">
                                <asp:TextBox ID="txtSort" CssClass="txtBoxNum txt50" MaxLength="2" runat="server"
                                    onkeypress="return KeyNumber(event);"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                            </td>
                            <td class="B_Head">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click" 
                                    OnClientClick="return Cktxt();" ToolTip="�ѹ�֡�鹷ع�ٹ����" />
                                <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧ�鹷ع�ٹ������"
                                    OnClick="btSaveAgain_Click" OnClientClick="return Cktxt();" ToolTip="�ѹ�֡�鹷ع�ٹ����������ҧ�鹷ع�ٹ������" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text="       ���ѹ�֡" OnClick="btCancel_Click" ToolTip="���ѹ�֡�鹷ع�ٹ����" />
                            </td>
                        </tr>
                        <tr>
                <td colspan="2" style="text-align: center; width: 100%;">
                    <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                        <Columns>
                            <Control:BoundField HeaderText="�ӴѺ���" DataField="Sort">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="���͵鹷ع�ٹ��" DataField="CostsName">
                                <ItemStyle Width="80%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="CostsCenter.aspx?mode=2&id=<%#Eval("CostsCode") %>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CostsCode") %>');">
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
                            <Control:BoundField HeaderText="�ӴѺ���" DataField="Sort">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="���͵鹷ع�ٹ��" DataField="CostsName">
                                <ItemStyle Width="80%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="CostsCenter.aspx?mode=2&id=<%#Eval("CostsCode") %>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CostsCode") %>');">
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
