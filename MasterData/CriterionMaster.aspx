<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="CriterionMaster.aspx.cs" Inherits="CriterionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "CriterionMaster.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "CriterionMaster.aspx?mode=1";
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
            return CkCriterion();
            return true;
        }
    }
    function CkCriterion()
    {
        if(document.getElementById("<%=txtCriterion.ClientID%>").value == "")
        {
            document.getElementById("ErrorCriterion").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorCriterion").style.display = "none";
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
                    ࡳ���û����Թ�дѺ�س�Ҿ
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="TableSearch" runat="server" width="100%">
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
                                <span class="spantxt">�����з���ҹ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtLevelScore" Width="50px" onkeypress="return KeyNumber(event);"
                                    runat="server"></asp:TextBox>
                                <span class="ColorRed">*</span> <span id="ErrorLevelScore" class="ErrorMsg">��سһ�͹�����з���ҹ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top;">
                                <span class="spantxt">��͸Ժ�� : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtLevelDetail" Width="600px" Height="50px" TextMode="MultiLine"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top;">
                                <span class="spantxt">ࡳ���дѺ�س�Ҿ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtCriterion" runat="server"></asp:TextBox>
                                <span id="ErrorCriterion" class="ErrorMsg">��سһ�͹ࡳ���дѺ�س�Ҿ</span>
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
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" OnClick="btSave_Click " OnClientClick="return Cktxt();" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" OnClick="btCancel_Click" />
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
                            <%--<Control:BoundField HeaderText="��Ǻ觪��" DataField="IndicatorsName">
                                <ItemStyle Width="35%" HorizontalAlign="Left" />
                            </Control:BoundField>--%>
                            <Control:BoundField HeaderText="ࡳ���дѺ�س�Ҿ" DataField="CriterionName">
                                <ItemStyle Width="10%" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="������з���ҹ" DataField="LevelScore">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="��͸Ժ��" DataField="LevelDetail">
                                <ItemStyle Width="70%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <%--<Control:TemplateField HeaderText="��͸Ժ��" SortExpression="LevelDetail">
                                <ItemTemplate>
                                    <div style="padding:0 0 0 <%#Eval("LevelScore") %>0px; text-align:left">
                                        <%#Eval("LevelDetail") %>
                                    </div>
                                </ItemTemplate>
                            </Control:TemplateField>--%>
                            <Control:BoundField HeaderText="�ӴѺ���" DataField="Sort">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="CriterionMaster.aspx?mode=2&id=<%#Eval("CriterionCode") %>">
                                        <img style="border: 0; cursor: pointer;" alt="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CriterionCode") %>');">
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
