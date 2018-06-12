<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="CostsView.aspx.cs" Inherits="CostsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('ต้องการลบรายการนี้ ใช่หรือไม่')) location.href = "CostsView.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "Costs.aspx?mode=1";
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
                    แบบคิดค่าใช้จ่ายกิจกรรมโครงการ
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="TableSearch" runat="server" width="100%">
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">ปีการศึกษา : </span>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList CssClass="ddl" ID="ddlSearchYear" Width="70px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">กลยุทธ : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch2" Width="600px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearch2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">โครงการ : </span>
                            </td>
                            <td colspan="2" class="SearchF">
                                <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="600px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">คำค้นหา : </span>
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
                        <tr>
                            <td align="center" colspan="2">
                                <span class="spantxt">จำนวนที่พบ : </span><span id="lblSearchTotal" class="spantxt"
                                    style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">รายการ</span>
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
                    <Control:DataGridView ID="GridView1" runat="server" PageSize="20" >
                        <Columns>
                            <Control:BoundField HeaderText="โครงการ" DataField="ProjectsName">
                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="กิจกรรม" DataField="ActivityName">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="ประเภทเงิน" DataField="CostsType">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="ยอดเงิน" DataField="TotalAmount" DataFormatString="{0:n2}">
                                <ItemStyle Width="10%" HorizontalAlign="Right" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="ภาคเรียนที่" DataField="Term">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="แก้ไข">
                                <ItemTemplate>
                                    <a href="Costs.aspx?mode=2&id=<%#Eval("CostsCode") %>">
                                        <img style="border: 0; cursor: pointer;" alt="แก้ไข" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ลบ">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CostsCode") %>');">
                                        <img style="border: 0; cursor: pointer;" alt="ลบ" src="../Image/delete.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="พิมพ์">
                                <ItemTemplate>
                                    <a href="../GtReport/Viewer.aspx?rpt=4&id=<%#Eval("CostsCode") %>" target="_blank">
                                        <img style="border: 0; cursor :pointer;" alt="พิมพ์" src="../Image/reporticon.gif"</a>
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
