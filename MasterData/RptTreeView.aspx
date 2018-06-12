<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" CodeFile="RptTreeView.aspx.cs"
    Inherits="RptTreeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function printRpt(mode, type) {
        var yearb = document.getElementById("<%=ddlYearB.ClientID%>").value;
        var deptid = document.getElementById("<%=ddlSearchDept.ClientID%>").value;
        var empid = document.getElementById("<%=ddlSearchEmp.ClientID%>").value;
        var ck;

        if (document.getElementById("<%=rbtreport1.ClientID%>").checked == true)
        { ck = 1; }
        if (document.getElementById("<%=rbtreport2.ClientID%>").checked == true)
        { ck = 2; }

        window.open('../GtReport/Viewer.aspx?rpt=' + mode + '&rpttype=' + type + '&yearB=' + yearb + '&deptid=' + deptid + '&empid=' + empid + '&ck=' + ck);
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
                    ��§ҹ�š�û����Թ � �Ѩ�غѹ
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
                                <span class="spantxt">˹��§ҹ : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged" Width="350px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                            </div>
                            <div class="SearchF">
                                <asp:DropDownList ID="ddlSearchEmp" CssClass="ddlSearch" runat="server" Width="350px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span class="spantxt">�ٻẺ��§ҹ : </span>
                            </div>
                            <div class="SearchF">
                                <div style="float: left;">
                                    <asp:RadioButton ID="rbtreport1" runat="server" Text=" Ẻ��§ҹ�š�û����Թ"
                                        GroupName="rpt" Checked="true" />
                                </div>
                                <div style="float: left;">
                                    <asp:RadioButton ID="rbtreport2" runat="server" Text=" Ẻ��§ҹ��ػ�š�û����Թ��ô��Թ�ҹ��ṡ����ҵðҹ��е�Ǫ���Ѵ"
                                        GroupName="rpt" />
                                </div>
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
