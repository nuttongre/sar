<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Tumbon.aspx.cs" Inherits="Tumbon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Tumbon.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "Tumbon.aspx?mode=1";
    }
    function Ckddl()
    {
        if(document.getElementById("<%=ddlProvince.ClientID%>").selectedIndex == 0)
        {
            document.getElementById("ErrorProvince").style.display ="block";
            return false;
        }
        else
        {
            document.getElementById("ErrorProvince").style.display = "none";
            return CkAmphur();
            return true;
        }
    }
    function CkAmphur() {
        if (document.getElementById("<%=ddlAmphur.ClientID%>").selectedIndex == 0) {
            document.getElementById("ErrorAmphur").style.display = "block";
            return false;
        }
        else {
            document.getElementById("ErrorAmphur").style.display = "none";
            return Cktxt();
            return true;
        }
    }
    function Cktxt()
    {
        if(document.getElementById("<%=txtTumbon.ClientID%>").value == "")
        {
            document.getElementById("ErrorTumbon").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorTumbon").style.display = "none";
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
                    �����ŵӺ�
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
                                            <span class="spantxt2 spansize14">���ҵӺ�</span>
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
                                            <span class="spantxt">�ѧ��Ѵ : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch" runat="server" Width="350px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <span class="spantxt">����� : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch1" runat="server" Width="350px" AutoPostBack="true"
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       �����Ӻ�����"
                                    OnClientClick="AddItem();" OnClick="btAdd_Click" ToolTip="���������ŵӺ�����" />
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
                                <span class="spantxt">�ѧ��Ѵ : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlProvince" runat="server" Width="350"
                                    OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <span id="ErrorProvince" class="ErrorMsg">���͡�ѧ��Ѵ</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">����� : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlAmphur" runat="server" Width="350"
                                    OnSelectedIndexChanged="ddlAmphur_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <span id="ErrorAmphur" class="ErrorMsg">���͡�����</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">���͵Ӻ� : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtTumbon" runat="server" Width="600px"></asp:TextBox>
                                <span id="ErrorTumbon" class="ErrorMsg">��سһ�͹���͵Ӻ�</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">�ӴѺ��� : </span>
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);" Width="50px" MaxLength="5"></asp:TextBox>
                                <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡"
                                    OnClick="btSave_Click " OnClientClick="return Ckddl();" ToolTip="�ѹ�֡�����ŵӺŹ��" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text ="       ���ѹ�֡"
                                    OnClick="btCancel_Click" ToolTip="���ѹ�֡�����ŵӺŹ��" />
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
                            <Control:TemplateField HeaderText="�ӴѺ���">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            </Control:TemplateField>
                             <Control:BoundField HeaderText="�Ӻ�" DataField="TumbonName">
                                <ItemStyle Width="80%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="���">
                                <ItemTemplate>
                                    <a href="Tumbon.aspx?mode=2&id=<%#Eval("TumbonID") %>">
                                        <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("TumbonID") %>');">
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
