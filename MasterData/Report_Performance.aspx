<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Report_Performance.aspx.cs" Inherits="Report_Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function printRpt(mode, type) {
            var yearb = document.getElementById("<%=ddlYearB.ClientID%>").value;
            window.open('../GtReport/Viewer.aspx?rpt=' + mode + '&rpttype=' + type + '&bgyear=' + yearb);
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
                    Ẻ��§ҹ�š�û�Ժѵԧҹ
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
                                <span id="lblSearchYear" runat="server" class="spantxt">�ա���֡�� : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList CssClass="ddlSearch" ID="ddlYearB" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
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
