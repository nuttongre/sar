<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="IndicatorsEditQuality.aspx.cs" Inherits="IndicatorsEditQuality" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function ValidateFile()
    {
        var file=$get('<%=fpAttach.ClientID %>');
        if(file.value==""){alert("��س����͡���");return false;}else return true;
    }
    function ConfirmDelete(Delid)
    {
        if(confirm("����͹!!\r\n���ź�����Ź���繡��źẺ�����������ö�ӡ�Ѻ����\r\n\n��ͧ���ź��¡�÷�����͡ �� �������?\r\n[���͡ OK �����׹�ѹ���ź������ �������͡ Cancel ����¡��ԡ���ź������]"))
        {
            var _id = RequestQuery("id");
            var _acid = RequestQuery("acid");
            window.location="IndicatorsEditQuality.aspx?mode=2&id=" + _id + "&acid=" + _acid + "&delmode=3&did=" + Delid;
        }
    }
    function deleteItem(id)
    {
        if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "IndicatorsEditQuality.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "IndicatorsEditQuality.aspx?mode=1";
    }
     function Ckddl()
    {

    }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </ajax:AjaxPanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="headTable">
                �����Թ�ԧ�س�Ҿ�����Ǻ觪��
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 15px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 100%">
                <ajax:AjaxPanel ID="AjaxPanel2" runat="server">
                    <table id="TableSearch" runat="server" width="100%">
                        <tr>
                            <td colspan="3" align="center">
                                <table class="tableSearch">
                                    <tr>
                                        <td colspan="3" class="headSearch">
                                            <span class="spantxt2 spansize14">���ҵ�Ǻ觪��</span>
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
                                            <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch" Width="500px" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
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
                            <td colspan="3" align="center">
                                <span class="spantxt">�ӹǹ��辺 : </span><span id="lblSearchTotal" class="spantxt"
                                    style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">��¡��</span>
                            </td>
                        </tr>
                    </table>
                </ajax:AjaxPanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 100%; height: 5px;">
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
                            <asp:Label ID="lblStudyYear" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <span class="spantxt">�ҵðҹ : </span>
                        </td>
                        <td>
                            <asp:Label ID="lblStandard" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <span class="spantxt">��Ǻ觪�� : </span>
                        </td>
                        <td>
                            <asp:Label ID="lblIndicators" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width:100%; height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%; vertical-align:top;">
                            <span class="spantxt">��͸Ժ�� : </span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDetail" runat="server" CssClass="txtBoxNotType" ReadOnly="true" TextMode="MultiLine" Height="200" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width:100%; height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%; vertical-align: top;">
                            <span class="spantxt">����繡�þԨ�ó� : </span>
                        </td>
                        <td align="left" style="width: 80%">
                            <asp:TextBox ID="txtDevelopIssues" runat="server" CssClass="txtBoxNotType" ReadOnly="true" TextMode="MultiLine" Height="300" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <span class="spantxt">���˹ѡ��ṹ : </span>
                        </td>
                        <td>
                            <asp:Label ID="lblWeightScore" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 25%; vertical-align: top;">
                            <span class="spantxt2 spantxt5">�дѺ�س�Ҿ������ : </span>
                        </td>
                        <td>
                            <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
                                <asp:DropDownList ID="ddlCan" CssClass="spantxt2 spantxt3" Width="50" runat="server" OnSelectedIndexChanged="ddlCan_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </ajax:AjaxPanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 25%; vertical-align: top;">
                            <span class="spantxt">�Դ�� : </span>
                        </td>
                        <td>
                            <ajax:AjaxPanel ID="AjaxPanel15" runat="server">
                                <asp:Label ID="lblScore" runat="server" CssClass="spantxt2 spantxt3"></asp:Label>
                                <span class="spantxt" id="lblPC">��ṹ</span>
                            </ajax:AjaxPanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%; vertical-align: top;">
                            <span class="spantxt2 spantxt5">�ѭ��/����ʹ��� : </span>
                        </td>
                        <td>
                            <ajax:AjaxPanel ID="AjaxPanel17" runat="server">
                                <asp:TextBox CssClass="txtBox" ID="txtNote" Width="600px" TextMode="MultiLine" Height="100px"
                                    MaxLength="200" runat="server"></asp:TextBox>
                            </ajax:AjaxPanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">
                            <span class="spantxt">Ṻ��� : </span>
                        </td>
                        <td>
                            <asp:FileUpload ID="fpAttach" runat="server" /><asp:Button ID="btnAttach" Text="�������Ṻ"
                                CssClass="button_green2" runat="server" OnClientClick="return ValidateFile();"
                                OnClick="btnAttach_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">
                            <span class="spantxt" style="vertical-align: top;">���Ṻ������ : </span>
                        </td>
                        <td style="border: solid 1px #BDBDBD; min-height: 25px; vertical-align: middle; text-align: left;
                            background-color: #E0F8F7">
                            <asp:Repeater ID="rptFile" runat="server">
                            <ItemTemplate>
                                    <span class="rpt_content"><a href="#">
                                        <img style="border: none;" src="../Image/RemoveContact.png" alt="ź���Ṻ�͡" title="ź���Ṻ�͡"
                                            onclick="ConfirmDelete('<%# Eval("AttachId") %>')" /></a> <a target="_blank" href="../AttachFile/<%# Eval("AttachID")%>_<%# Eval("FileName")%>">
                                                <%# Eval("FileName").ToString() %></a> </span>
                            </ItemTemplate>
                             </asp:Repeater>                                             
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                        </td>
                        <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click "
                                    OnClientClick="return Ckddl();" ToolTip="�ѹ�֡��û����Թ�Ԩ���������Ǫ���Ѵ" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text="       ���ѹ�֡"
                                    OnClick="btCancel_Click" ToolTip="���ѹ�֡��û����Թ�Ԩ���������Ǫ���Ѵ" />
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
                <ajax:AjaxPanel ID="AjaxPanel19" runat="server">
                    <Control:DataGridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <Control:TemplateField HeaderText="���͡">
                                <ItemTemplate>
                                    <a href="IndicatorsEditQuality.aspx?mode=2&id=<%#Eval("IndicatorsCode")%>&yearid=<%#Eval("StudyYear")%>">
                                        <img style="border: 0; cursor: pointer;" title="���͡任����Թ��" src="../Image/<%# (Eval("Quality").ToString()== "0" ? "goto":"goto2")%>.png" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <%--<Control:BoundField HeaderText="�ҵðҹ" DataField="stdFullName">
                                <ItemStyle Width="65%" HorizontalAlign="Left" />
                            </Control:BoundField>--%>
                            <Control:BoundField HeaderText="��Ǻ觪��" DataField="intFullName">
                                <ItemStyle Width="55%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="���˹ѡ��ṹ" DataField="WeightScore" DataFormatString="{0:n2}">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="�Ż����Թ�����" DataField="Quality" DataFormatString="{0:n0}">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="��ṹ�����" DataField="Score" DataFormatString="{0:n2}">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="IndicatorsEditQuality.aspx?mode=2&id=<%#Eval("IndicatorsCode")%>&yearid=<%#Eval("StudyYear")%>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź" Visible="false">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("IndicatorsCode") %>');">
                                        <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" />
                    </Control:DataGridView>
                </ajax:AjaxPanel>
            </td>
        </tr>
    </table>
</asp:Content>
