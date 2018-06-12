<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="ReportBudgetNotCloseByBudgetType.aspx.cs" Inherits="ReportBudgetNotCloseByBudgetType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function printRpt(mode, type) {
            var yearb = document.getElementById("<%=ddlSearchYear.ClientID%>").value;
            var pjid = document.getElementById("<%=ddlSearch.ClientID%>").value;
            var deptid = document.getElementById("<%=ddlSearchDept.ClientID%>").value;
            var rpt = $get("<%=ddlReportType.ClientID%>").value;
            var yearmode = 0;
            var ck;

            if ($get("<%=rbtBudgetYear.ClientID%>").checked == true) {
                yearmode = 1;
            }
            window.open('../GtReport/Viewer.aspx?rpt=' + rpt + '&yearB=' + yearb + '&id=' + pjid + '&deptid=' + deptid + '&rpttype=' + type + '&yearmode=' + yearmode);
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
                    ��§ҹ��ػ����駺����ҳ
                </div>
                <div class="spaceDiv"></div>
                <div id="Div1" class="TableSearch">
                    <div class="SearchTable">
                        <div class="SearchHead">
                            <span class="spantxt2 spansize14">���͹䢡�����¡��</span>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">�������� : </span>
                            </div>
                            <div class="SearchF">
                                <asp:RadioButton ID="rbtStudyYear" CssClass="spantxt2" Text=" �ա���֡��" GroupName="rbt" Checked="true" runat="server" AutoPostBack="true" OnCheckedChanged="rbtYearType" />
                                <asp:RadioButton ID="rbtBudgetYear" CssClass="spantxt2" Text=" �է�����ҳ" GroupName="rbt" runat="server" AutoPostBack="true" OnCheckedChanged="rbtYearType" /> 
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span id="lblYear" runat="server" class="spantxt">�ա���֡�� : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchYear" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">�ç��� : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" Width="500px">
                                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">˹��§ҹ : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" runat="server" Width="500px">
                                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">��������§ҹ : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlReportType" CssClass="ddlSearch" runat="server" Width="500px">
                                            </asp:DropDownList>
                            </div>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                            </div>
                            <div class="SearchF">
                                <asp:Literal ID="linkReport" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="spaceDiv"></div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>             
</asp:Content>
