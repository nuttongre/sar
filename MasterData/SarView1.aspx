<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" CodeFile="SarView1.aspx.cs"
    Inherits="SarView1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SarView1()
        {
            var yearid = document.getElementById("<%=ddlYearB.ClientID%>").value;
            var deptid = document.getElementById("<%=ddlSearchDept.ClientID%>").value;
            var empid = document.getElementById("<%=ddlSearchEmp.ClientID%>").value;
            location.href="SarView1.aspx?year="+ yearid + '&deptid=' + deptid + '&empid=' + empid;
        }
        function GotoStandard(id)
        {
            var yearid = document.getElementById("<%=ddlYearB.ClientID%>").value;
            var ckview = document.getElementById("<%=rbtlView1.ClientID%>").checked ? 0 : 1;
            var deptid = document.getElementById("<%=ddlSearchDept.ClientID%>").value;
            var empid = document.getElementById("<%=ddlSearchEmp.ClientID%>").value;
            location.href="SarView2.aspx?year="+ yearid + "&sdid=" + id + "&ckview=" + ckview + '&deptid=' + deptid + '&empid=' + empid;
        }
    </script>

    <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
        <div>
            <asp:Image ID="Img1" runat="server" Visible="false" />
        </div>
        <div>
            <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="headTable">
            <p>
                ��§ҹ����Ѻ��������</p>
        </div>
        <br />
        <div>
            |&nbsp;<span class="spnSvHeadAc">����ء��ҹ</span>&nbsp;|
        </div>
        <br />
        <div style="text-align: center; min-height: 30px;">
            <table width="100%">
                <tr>
                    <td align="center">
                        <table class="tableSearch">
                            <tr>
                                <td colspan="3" class="headSearch">
                                    <span class="spantxt2 spansize14">���͹䢡�����¡��</span>
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
                                    <span class="spantxt">�ա���֡�� : </span>
                                </td>
                                <td class="SearchF">
                                    <asp:DropDownList CssClass="ddlSearch spansize14" ID="ddlYearB" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearB_OnSelectedChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="SearchB">
                                </td>
                                <td class="SearchtxtF">
                                    <span class="spantxt">˹��§ҹ : </span>
                                </td>
                                <td class="SearchF">
                                    <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged" Width="350px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="SearchB">
                                </td>
                                <td class="SearchtxtF">
                                    <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                </td>
                                <td class="SearchF">
                                    <asp:DropDownList ID="ddlSearchEmp" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSearchEmp_SelectedIndexChanged" Width="350px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center; width: 100%; visibility: hidden;">
            <span class="spantxt spansize14">����ͧ : </span>
            <asp:RadioButton ID="rbtlView1" CssClass="spansize14" GroupName="v" runat="server"
                Text="�����" AutoPostBack="true" OnCheckedChanged="rbtView_OnSelectedChanged" />&nbsp;&nbsp;
            <asp:RadioButton ID="rbtlView2" CssClass="spansize14" GroupName="v" runat="server"
                Text="ࡳ���ü�ҹ" Checked="true" AutoPostBack="true" OnCheckedChanged="rbtView_OnSelectedChanged" />
        </div>
        <br />
        <div style="float: right;">
            <span style="color: #424242; font-weight: bold; font-size: 14pt; padding: 0 70px 0 0;">
                ��ṹ���</span> <span style="color: #424242; font-weight: bold; font-size: 14pt;
                    padding: 0 60px 0 0;">��ṹ�����</span> <span style="color: #424242; font-weight: bold;
                        font-size: 14pt; padding: 0 40px 0 0;">�ô</span> <span style="color: #424242; font-weight: bold;
                            font-size: 14pt;">�дѺ�س�Ҿ</span>
            <br />
            <br />
        </div>
        <p class="clear">
        </p>
        <div style="width: 100%; padding: 0 0 0 5px;">
            <asp:Label ID="lblSummarize" runat="server"></asp:Label>
        </div>
        <p class="clear">
        </p>
        <div style="padding: 0 0 0 40px; vertical-align: bottom;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <a style="cursor: pointer;" onclick="GotoStandard('<%#Eval("SideCode")%>');">
                        <%#Eval("FullName")%></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="float: right; padding: 50px 0 0 0;">
            <span>***</span> <span style="color: #0B610B; font-weight: bold;">������ - ��ҹ</span>,
            <span style="color: #DF0101; font-weight: bold;">��ᴧ - ����ҹ</span>
        </div>
    </ajax:AjaxPanel>
</asp:Content>
