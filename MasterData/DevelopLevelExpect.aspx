<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="DevelopLevelExpect.aspx.cs" Inherits="DevelopLevelExpect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "DevelopLevelExpect.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "DevelopLevelExpect.aspx?mode=1";
    }
    function Ckddl()
    {
        if(document.getElementById("<%=ddlStandard.ClientID%>").selectedIndex == 0)
        {
            document.getElementById("ErrorStandard").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorStandard").style.display = "none";
            return CkIndicators();
            return true;
        }
    }
    function CkIndicators()
    {
        if(document.getElementById("<%=ddlIndicators.ClientID%>").selectedIndex == 0)
        {
            document.getElementById("ErrorIndicators").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorIndicators").style.display = "none";
            return true;
        }
    }
    </script>

    <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <asp:Image id="Img1" runat="server" visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headTable">
                    �дѺ��þѲ�ҷ��Ҵ��ѧ
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="TableSearch" runat="server" width="100%">
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">�ա���֡�� : </span>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList CssClass="ddl" ID="ddlSearchYear" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">�ҵðҹ : </span>
                            </td>
                            <td colspan="2" class="SearchF">
                                <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="600px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">�Ӥ��� : </span>
                            </td>
                            <td class="SearchF">
                                <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                    CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');" ID="btSearch" runat="server" OnClick="btSearch_Click" />
                            </td>
                            <td class="SearchB">
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server"
                                    OnClientClick="AddItem();" OnClick="btAdd_Click" />
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
                <td colspan="2" style="width: 100%">
                    <table id="table1" runat="server">
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ա���֡�� : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlYearS" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYearS_OnSelectedChanged" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ҵðҹ : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlStandard" Width="600px" AutoPostBack="true" OnSelectedIndexChanged="ddlStandard_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span>
                                <span id="ErrorStandard" class="ErrorMsg">���͡�ҵðҹ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">��Ǻ觪�� : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlIndicators" Width="600px" AutoPostBack="true" OnSelectedIndexChanged="ddlIndicators_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span>
                                <span id="ErrorIndicators" class="ErrorMsg">���͡��Ǻ觪��</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align:top;">
                                <span class="spantxt">�дѺ��þѲ�ҷ��Ҵ��ѧ : </span>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbtlDevelopLevel" CellSpacing="15" runat="server">
                                </asp:RadioButtonList>
                                <span class="ColorRed">*</span>
                                <span id="ErrorDevelopLevel" class="ErrorMsg">���͡�дѺ��þѲ��</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server"
                                    OnClick="btSave_Click " OnClientClick="return Ckddl();" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server"
                                    OnClick="btCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; width: 100%;">
                    <Control:DataGridView ID="GridView1" runat="server">
                        <Columns>
                            <Control:BoundField HeaderText="��Ǻ觪��" DataField="IndicatorsName">
                                <ItemStyle Width="35%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="�дѺ���" DataField="LevelScore">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="�дѺ���շ������" DataField="LevelScore2">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="��͸Ժ���дѺ��þѲ��" DataField="LevelDetail">
                                <ItemStyle Width="85%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="DevelopLevelExpect.aspx?mode=2&id=<%#Eval("IndicatorsCode") %>">
                                        <img style="border: 0; cursor: pointer;" alt="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("IndicatorsCode") %>');">
                                        <img style="border: 0; cursor: pointer;" alt="ź" src="../Image/delete.gif" /></a>
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
