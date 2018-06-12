<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Criterion.aspx.cs" Inherits="Criterion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function deleteItem(id)
    {
        if(confirm('ต้องการลบรายการนี้ ใช่หรือไม่')) location.href = "Criterion.aspx?mode=3&id="+id;
    }
    function AddItem()
    {
        location.href = "Criterion.aspx?mode=1";
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
                    เกณฑ์ระดับคุณภาพ
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%">
                    <table id="TableSearch" runat="server" width="100%">
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">มาตรฐาน : </span>
                            </td>
                            <td colspan="2" class="SearchF">
                                <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="600px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="SearchtxtF">
                                <span class="spantxt">ตัวบ่งชี้ : </span>
                            </td>
                            <td colspan="2" class="SearchF">
                                <asp:DropDownList ID="ddlSearch1" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearch1_SelectedIndexChanged" Width="600px">
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
                                <span class="spantxt">มาตรฐาน : </span>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="ddl" ID="ddlStandard" Width="600px" AutoPostBack="true"
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
                                <asp:DropDownList CssClass="ddl" ID="ddlIndicators" Width="600px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlIndicators_OnSelectedChanged" runat="server">
                                </asp:DropDownList>
                                <span class="ColorRed">*</span> <span id="ErrorIndicators" class="ErrorMsg">เลือกตัวบ่งชี้</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%">
                                <span class="spantxt">ร้อยละที่ผ่าน : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxNum" ID="txtLevelScore" Width="50px" onkeypress="return KeyNumber(event);"
                                    runat="server"></asp:TextBox>
                                <span class="ColorRed">*</span> <span id="ErrorLevelScore" class="ErrorMsg">กรุณาป้อนร้อยละที่ผ่าน</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top;">
                                <span class="spantxt">คำอธิบาย : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtLevelDetail" Width="600px" Height="50px" TextMode="MultiLine"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 20%; vertical-align: top;">
                                <span class="spantxt">เกณฑ์ระดับคุณภาพ : </span>
                            </td>
                            <td>
                                <asp:TextBox CssClass="txtBoxL" ID="txtCriterion" runat="server"></asp:TextBox>
                                <span id="ErrorCriterion" class="ErrorMsg">กรุณาป้อนเกณฑ์ระดับคุณภาพ</span>
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
                                <asp:Button ID="btSave" CssClass="btYes" runat="server" OnClick="btSave_Click " OnClientClick="return Ckddl();" />
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
                            <%--<Control:BoundField HeaderText="ตัวบ่งชี้" DataField="IndicatorsName">
                                <ItemStyle Width="35%" HorizontalAlign="Left" />
                            </Control:BoundField>--%>
                            <Control:BoundField HeaderText="เกณฑ์ระดับคุณภาพ" DataField="CriterionName">
                                <ItemStyle Width="10%" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="ร้อยล่ะที่ผ่าน" DataField="LevelScore">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="คำอธิบาย" DataField="LevelDetail">
                                <ItemStyle Width="70%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <%--<Control:TemplateField HeaderText="คำอธิบาย" SortExpression="LevelDetail">
                                <ItemTemplate>
                                    <div style="padding:0 0 0 <%#Eval("LevelScore") %>0px; text-align:left">
                                        <%#Eval("LevelDetail") %>
                                    </div>
                                </ItemTemplate>
                            </Control:TemplateField>--%>
                            <Control:BoundField HeaderText="ลำดับที่" DataField="Sort">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="แก้ไข">
                                <ItemTemplate>
                                    <a href="Criterion.aspx?mode=2&id=<%#Eval("CriterionCode") %>">
                                        <img style="border: 0; cursor: pointer;" alt="แก้ไข" src="../Image/edit.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="ลบ">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("CriterionCode") %>');">
                                        <img style="border: 0; cursor: pointer;" alt="ลบ" src="../Image/delete.gif" /></a>
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
