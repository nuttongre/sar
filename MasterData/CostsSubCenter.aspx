<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="CostsSubCenter.aspx.cs" Inherits="CostsSubCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if (confirm('ต้องการลบรายการนี้ ใช่หรือไม่')) location.href = "CostsSubCenter.aspx?mode=3&id=" + id;
    }
    function AddItem()
    {
        location.href = "CostsSubCenter.aspx?mode=1";
    }
    function Ckddl() {
        if (document.getElementById("<%=ddlCostsCenter.ClientID%>").value == "") {
                document.getElementById("ErrorCostsCenter").style.display = "block";
                return false;
            }
            else {
            document.getElementById("ErrorCostsCenter").style.display = "none";
            return Cktxt();
                return true;
            }
        }
    function Cktxt()
    {
        if(document.getElementById("<%=txtCostsSubName.ClientID%>").value == "")
        {
            document.getElementById("ErrorCostsSubName").style.display = "block";
            return false;
        }
        else
        {
            document.getElementById("ErrorCostsSubName").style.display = "none";
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
                    ต้นทุนศูนย์ย่อย
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
                                            <span class="spantxt2 spansize14">ค้นหาต้นทุนศูนย์ย่อย</span>
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
                                            <span class="spantxt">ปีการศึกษา : </span>
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
                                            <span class="spantxt">ต้นทุนศูนย์ : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchCostsCenter" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearchCostsCenter_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SearchB">
                                        </td>
                                        <td class="SearchtxtF">
                                            <span class="spantxt">คำค้นหา : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:TextBox CssClass="txtSearch txt300" ID="txtSearch" runat="server"></asp:TextBox><asp:Button
                                                CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                                ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip = "คลิ๊กเพื่อค้นหา" Text="  " />
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
                                    OnClick="btAdd_Click" Text="       สร้างต้นทุนศูนย์ย่อยใหม่" ToolTip="สร้างต้นทุนศูนย์ย่อยใหม่" />
                            </td>
                            <td class="SearchtxtF">
                            </td>
                            <td class="SearchF">
                                <span class="spantxt">จำนวนที่พบ : </span><span id="lblSearchTotal" class="spantxt"
                                    style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">รายการ</span>
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
                                <span class="spantxt">ปีการศึกษา : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYearB_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">ต้นทุนศูนย์ : </span>
                            </td>
                            <td class="B_Head">
                                <asp:DropDownList ID="ddlCostsCenter" CssClass="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCostsCenter_SelectedIndexChanged"></asp:DropDownList>&nbsp;<span
                                    class="ColorRed">*</span> <span id="ErrorCostsCenter" class="ErrorMsg">กรุณาเลือกต้นทุนศูนย์</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">ชื่อต้นทุนศูนย์ย่อย : </span>
                            </td>
                            <td class="B_Head">
                                <asp:TextBox ID="txtCostsSubName" CssClass="txtBoxL txt500" runat="server"></asp:TextBox>&nbsp;<span
                                    class="ColorRed">*</span> <span id="ErrorCostsSubName" class="ErrorMsg">กรุณาป้อนต้นทุนศูนย์ย่อย</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">รายละเอียด : </span>
                            </td>
                            <td class="B_Head">
                                <asp:TextBox ID="txtCostsSubDetail" CssClass="txtBoxL txt500" TextMode="MultiLine" Height="150px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                                <span class="spantxt">ลำดับที่ : </span>
                            </td>
                            <td class="B_Head">
                                <asp:TextBox ID="txtSort" CssClass="txtBoxNum txt50" MaxLength="2" runat="server"
                                    onkeypress="return KeyNumber(event);"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                <span id="ErrorSort" class="ErrorMsg">กรุณาป้อนลำดับที่</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="F_Head">
                            </td>
                            <td class="B_Head">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       บันทึก" OnClick="btSave_Click" 
                                    OnClientClick="return Cktxt();" ToolTip="บันทึกต้นทุนศูนย์ย่อยนี้" />
                                <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       บันทึกและสร้างต้นทุนศูนย์ย่อยใหม่"
                                    OnClick="btSaveAgain_Click" OnClientClick="return Cktxt();" ToolTip="บันทึกต้นทุนศูนย์ย่อยนี้และสร้างต้นทุนศูนย์ย่อยใหม่" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text="       ไม่บันทึก" OnClick="btCancel_Click" ToolTip="ไม่บันทึกต้นทุนศูนย์ย่อยนี้" />
                            </td>
                        </tr>
                        <tr>
                <td colspan="2" style="text-align: center; width: 100%;">
                    <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                        <Columns>
                            <Control:TemplateField HeaderText="ลำดับที่">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:BoundField HeaderText="ชื่อต้นทุนศูนย์" DataField="CostsName">
                                <ItemStyle Width="35%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="ชื่อต้นทุนศูนย์ย่อย" DataField="CostsSubName">
                                <ItemStyle Width="40%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="แก้ไข">
                                <ItemTemplate>
                                    <a href="CostsSubCenter.aspx?mode=2&id=<%#Eval("CostsSubCode") %>">
                                        <img style="border: 0; cursor: pointer;" title="แก้ไข" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ลบ">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CostsSubCode") %>');">
                                        <img style="border: 0; cursor: pointer;" title="ลบ" src="../Image/delete.gif" /></a>
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
                            <Control:TemplateField HeaderText="ลำดับที่">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:BoundField HeaderText="ชื่อต้นทุนศูนย์" DataField="CostsName">
                                <ItemStyle Width="35%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="ชื่อต้นทุนศูนย์ย่อย" DataField="CostsSubName">
                                <ItemStyle Width="40%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="แก้ไข">
                                <ItemTemplate>
                                    <a href="CostsSubCenter.aspx?mode=2&id=<%#Eval("CostsSubCode") %>">
                                        <img style="border: 0; cursor: pointer;" title="แก้ไข" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ลบ">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CostsSubCode") %>');">
                                        <img style="border: 0; cursor: pointer;" title="ลบ" src="../Image/delete.gif" /></a>
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
