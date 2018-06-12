<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="DevelopLevel.aspx.cs" Inherits="DevelopLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('ต้องการลบรายการนี้ ใช่หรือไม่')) location.href = "DevelopLevel.aspx?mode=3&id="+id;
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
                    ระดับการพัฒนา
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
                                            <span class="spantxt2 spansize14">ค้นหาระดับการพัฒนา</span>
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
                                            <span class="spantxt">มาตรฐาน : </span>
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
                                            <span class="spantxt">ตัวบ่งชี้ : </span>
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
                                            <span class="spantxt">คำค้นหา : </span>
                                        </td>
                                        <td class="SearchF">
                                            <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                                CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                                ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="คลิ๊กเพื่อค้นหา" />
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       สร้างระดับการพัฒนาใหม่"
                                    OnClientClick="AddItem();" OnClick="btAdd_Click" ToolTip="สร้างระดับการพัฒนาใหม่" />
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
                <td colspan="2" style="width: 100%; height: 5px">
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
                                <asp:DropDownList CssClass="ddl" ID="ddlYearB" AutoPostBack="true" OnSelectedIndexChanged="ddlYearB_OnSelectedChanged"
                                    runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="Span1" class="ErrorMsg">เลือกมาตรฐาน</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">มาตรฐาน : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlStandard" Width="500px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlStandard_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="ErrorStandard" class="ErrorMsg">เลือกมาตรฐาน</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">ตัวบ่งชี้ : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlIndicators" Width="500px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlIndicators_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="ErrorIndicators" class="ErrorMsg">เลือกตัวบ่งชี้</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">ช่วงชั้นที่ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtLevelScore" Width="50px" onkeypress="return KeyNumber(event);"
                                    runat="server"></asp:TextBox>
                                <span class="ColorRed">*</span> <span id="ErrorLevelScore" class="ErrorMsg">กรุณาป้อนช่วงชั้น</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">ค่าน้ำหนัก : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtWeight" Width="50px" onkeypress="return KeyNumber(event);"
                                    runat="server"></asp:TextBox>
                                <span class="spantxt">%</span> <span class="ColorRed">*</span> <span id="ErrorWeight"
                                    class="ErrorMsg">กรุณาป้อนค่าน้ำหนัก</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top;">
                                <span class="spantxt">คำอธิบายระดับการพัฒนา : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtLevelDetail" Width="500px" Height="100px" TextMode="MultiLine"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">ลำดับที่ : </span>
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);"
                                    Width="50px"></asp:TextBox>
                                <span class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">กรุณาป้อนลำดับที่</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                            </td>
                            <td align="left" style="width: 80%">
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       บันทึก" OnClick="btSave_Click "
                                    OnClientClick="return Ckddl();" ToolTip="บันทึกระดับการพัฒนานี้" />
                                <asp:Button ID="btCancel" CssClass="btNo" runat="server" Text="       ไม่บันทึก"
                                    OnClick="btCancel_Click" ToolTip="ไม่บันทึกระดับการพัฒนานี้" />
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
                            <Control:BoundField HeaderText="ระดับที่" DataField="LevelScore">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="คำอธิบายระดับการพัฒนา" DataField="LevelDetail">
                                <ItemStyle Width="85%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="แก้ไข">
                                <ItemTemplate>
                                    <a href="DevelopLevel.aspx?mode=2&id=<%#Eval("DevelopLevelCode") %>">
                                        <img style="border: 0; cursor: pointer;" title="แก้ไข" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ลบ">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("DevelopLevelCode") %>');">
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
