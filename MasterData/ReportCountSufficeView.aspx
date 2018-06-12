﻿<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" CodeFile="ReportCountSufficeView.aspx.cs"
    Inherits="ReportCountSufficeView" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/CountAct.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function popup(id, syear) {
            dialogBox.show('../PopUp/PopUpCountProjectsByKSN.aspx?mode=P7&id=' + id + '&syear=' + syear, 'รายชื่อโครงการ', '1000', '500', '#000', true, true);
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageDiv">
                <div class="warningDiv">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="headTable">
                    จำนวนโครงการรายตัวบ่งชี้เศรษฐกิจพอเพียง
                </div>
                <div class="spaceDiv"></div>
                <div id="Div1" class="TableSearch">
                    <div class="SearchTable">
                        <div class="SearchHead">
                            <span class="spantxt2 spansize14">เงื่อนไขการค้นหา</span>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span id="lblSearchYear" runat="server" class="spantxt">ปีการศึกษา : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList CssClass="ddlSearch" ID="ddlYearB" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYearB_OnSelectedChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="spaceDiv"></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="spaceDiv"></div>
                <div style="float: right;">
                    <span style="font-weight: bold; font-size: 12pt; color: Black;">จำนวนโครงการ</span>
                </div>
                <div class="spaceDiv"></div>
                <div id="actoutside">
                    <ul class="ulStandard">
                        <asp:Repeater ID="rptMain" runat="server" OnItemDataBound="rptMain_ItemDataBound">
                            <ItemTemplate>
                                <li style="list-style-image: url(../Image/icon/icon1.png)"><span class="spantxtStandardAct" title="<%#Eval("SufficeSideName") %>">
                                    <%#Eval("FullName") %></span>
                                    <ul class="ulIndicators">
                                        <asp:Repeater ID="rptSub" runat="server">
                                            <ItemTemplate>
                                                <li style="list-style-image: url(../Image/MenuStlye/S09.png)"><span class="spantxtStandardAct" style="font-weight:normal;" title="<%#Eval("SufficeStandardName") %>">
                                                    <%# Eval("FullSubName") %></span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                    <br />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>