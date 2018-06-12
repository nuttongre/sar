<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="GraphCountActivityByStd.aspx.cs" Inherits="GraphCountActivityByStd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function printRpt(mode, type) {
            var yearb = $get("<%=ddlYearB.ClientID%>").value;
            var graphtype = $get("<%=ddlType.ClientID%>").selectedIndex;

            window.open('../GtReport/Viewer.aspx?rpt=42&yearB=' + yearb + '&rpttype=' + type + '&graphtype=' + graphtype);
        }
    </script>

    <script src="../scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="../scripts/FusionCharts.js" type="text/javascript"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageDiv">
                <div class="headTable">
                    กราฟจำนวนกิจกรรมตามมาตรฐาน
                </div>
                <div class="spaceDiv"></div>
                <div id="Div1" class="TableSearch">
                    <div class="SearchTable">
                        <div class="SearchHead">
                            <span class="spantxt2 spansize14">เงื่อนไขการแสดงผล</span>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">ประเภทกราฟ : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlType" CssClass="ddlSearch" Width="150px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span id="lblSearchYear" runat="server" class="spantxt">ปีการศึกษา : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlYearB" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYearB_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow" style="display:none;">
                            <div class="SearchtxtF">
                                <span class="spantxt">พิมพ์กราฟ : </span>
                            </div>
                            <div class="SearchF">
                                <asp:Literal ID="linkReport" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="spaceDiv"></div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="spaceDiv"></div>
                <div class="spaceDiv"></div>
                <div class="centerDiv">
                    <div id="divGraph1" class="divGraph" style="border: solid 5px #949494; height: 500px; width: 98%;">
                        <div class="spaceDiv"></div>
                        <div class="divSpntxtHeadGraph">
                            <img src="../Image/Icon/IconGraph.png" />
                            กราฟจำนวนกิจกรรมตามมาตรฐาน
                        </div>
                        <div>
                            <div id="graphPnl1" runat="server"></div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="spaceDiv"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
