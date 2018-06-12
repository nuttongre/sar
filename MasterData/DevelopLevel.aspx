<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="DevelopLevel.aspx.cs" Inherits="DevelopLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "DevelopLevel.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "DevelopLevel.aspx?mode=1";
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
            return Cktxt();
            return true;
        }
    }
    function Cktxt()
    {
        if(document.getElementById("<%=txtLevelScore.ClientID%>").value == "")
        {
            document.getElementById("ErrorLevelScore").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorLevelScore").style.display = "none";
            return CkWeight();
            return true;
        }
    }
     function CkWeight()
    {
        if(document.getElementById("<%=txtWeight.ClientID%>").value == "")
        {
            document.getElementById("ErrorWeight").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorWeight").style.display = "none";
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
                    �дѺ��þѲ��
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
                                            <span class="spantxt2 spansize14">�����дѺ��þѲ��</span>
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
                                            <span class="spantxt">�ҵðҹ : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="500px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <span class="spantxt">��Ǻ觪�� : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList ID="ddlSearch1" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearch1_SelectedIndexChanged" Width="500px">
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       ���ҧ�дѺ��þѲ������"
                                    OnClientClick="AddItem();" OnClick="btAdd_Click" ToolTip="���ҧ�дѺ��þѲ������" />
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
                <td colspan="2" style="width: 100%; height: 5px">
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
                                <asp:DropDownList CssClass="ddl" ID="ddlYearB" AutoPostBack="true" OnSelectedIndexChanged="ddlYearB_OnSelectedChanged"
                                    runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="Span1" class="ErrorMsg">���͡�ҵðҹ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ҵðҹ : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlStandard" Width="500px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlStandard_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="ErrorStandard" class="ErrorMsg">���͡�ҵðҹ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">��Ǻ觪�� : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlIndicators" Width="500px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlIndicators_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="ErrorIndicators" class="ErrorMsg">���͡��Ǻ觪��</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">��ǧ��鹷�� : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtLevelScore" Width="50px" onkeypress="return KeyNumber(event);"
                                    runat="server"></asp:TextBox>
                                <span class="ColorRed">*</span> <span id="ErrorLevelScore" class="ErrorMsg">��سһ�͹��ǧ���</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">��ҹ��˹ѡ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtWeight" Width="50px" onkeypress="return KeyNumber(event);"
                                    runat="server"></asp:TextBox>
                                <span class="spantxt">%</span> <span class="ColorRed">*</span> <span id="ErrorWeight"
                                    class="ErrorMsg">��سһ�͹��ҹ��˹ѡ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top;">
                                <span class="spantxt">��͸Ժ���дѺ��þѲ�� : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtLevelDetail" Width="500px" Height="100px" TextMode="MultiLine"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ӴѺ��� : </span>
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);"
                                    Width="50px"></asp:TextBox>
                                <span class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click "
                                    OnClientClick="return Ckddl();" ToolTip="�ѹ�֡�дѺ��þѲ�ҹ��" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text="       ���ѹ�֡"
                                    OnClick="btCancel_Click" ToolTip="���ѹ�֡�дѺ��þѲ�ҹ��" />
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
                            <Control:BoundField HeaderText="�дѺ���" DataField="LevelScore">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="��͸Ժ���дѺ��þѲ��" DataField="LevelDetail">
                                <ItemStyle Width="85%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="DevelopLevel.aspx?mode=2&id=<%#Eval("DevelopLevelCode") %>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("DevelopLevelCode") %>');">
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
