<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="CutBudget.aspx.cs" Inherits="CutBudget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../scripts/jquery-latest.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function chkMonitor() {
            timedRefresh(10000);
        }
        function timedRefresh(timeoutPeriod) {
            setTimeout("location.reload(true);", timeoutPeriod);
        }
        function spnYear(txt) {
            if (ck == 0) {
                document.getElementById("lblYear").innerHTML = "�ա���֡�� : ";
            }
            else {
                document.getElementById("lblYear").innerHTML = "�է�����ҳ : ";
            }
        }

        //window.onload = setupRefresh;

        function setupRefresh() {
            document.getElementById('__AjaxCall_Wait').style.display = 'none';
            setTimeout("loadPage();", 10000);
        }
        function printRpt(mode, type, id, ckOld) {
            var ckSing = $get("<%=cbNotSign.ClientID%>").checked;
            var ckLogo = $get("<%=cbNotLogo.ClientID%>").checked;
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type + "&ckSign=" + ckSing + "&ckOld=" + ckOld + "&ckLogo=" + ckLogo);
        }
        function newtabItem(id)
        {
            window.open('Activity.aspx?mode=2&id=' + id);
        }
    </script>

  <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
            <div id="pageDiv">
                <div class="warningDiv">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="headTable">
                    �Ѵ������ҳ
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">�����ç��� / �Ԩ����</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�������� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:RadioButton ID="rbtStudyYear" CssClass="spantxt2" Text=" �ա���֡��" GroupName="rbt" Checked="true" runat="server" AutoPostBack="true" OnCheckedChanged="rbtChanged" />
                                        <asp:RadioButton ID="rbtBudgetYear" CssClass="spantxt2" Text=" �է�����ҳ" GroupName="rbt" runat="server" AutoPostBack="true" OnCheckedChanged="rbtChanged" />
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span id="lblYear" class="spantxt">�� : </span>
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
                                        <asp:DropDownList ID="ddlSearchProjects" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchProjects_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">˹��§ҹ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�Ӥ��� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" />
                                        <asp:Button ID="btAutoRefresh" CssClass="btNone" OnClick="btAutoRefresh_Click" runat="server" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="spaceDiv"></div>
                                <div class="SearchTotal">
                                    <span class="spantxt">�ӹǹ��辺 : </span><span id="lblSearchTotal" class="spantxt"
                                        style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">��¡��</span>
                                </div>
                                <div class="spaceDiv"></div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <table width="100%" style="border: solid 2px gray;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="cutbudget cutbudgetBg" style="background: url(../Image/MenuStlye/mainbk.png) repeat-x left top; background-color: #313638;">
                                        <span class="spantxt2">������������ҳ</span>
                                    </td>
                                    <td class="cutbudget cutbudgetBg" style="background: url(../Image/MenuStlye/mainbk.png) repeat-x left top; background-color: #313638;">
                                        <span class="spantxt2">�Թ����Ѻ (�ҷ)</span>
                                    </td>
                                    <td class="cutbudget cutbudgetBg" style="background: url(../Image/MenuStlye/mainbk.png) repeat-x left top; background-color: #313638;">
                                        <span class="spantxt2">��¨��»�Ш� (�ҷ)</span>
                                    </td>
                                    <td class="cutbudget cutbudgetBg" style="background: url(../Image/MenuStlye/mainbk.png) repeat-x left top; background-color: #313638;">
                                        <span class="spantxt2">�Թ��¨��� (�ҷ)</span>
                                    </td>
                                    <td class="cutbudget cutbudgetBg" style="background: url(../Image/MenuStlye/mainbk.png) repeat-x left top; background-color: #313638;">
                                        <span class="spantxt2">������� (�ҷ)</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cutbudget">
                                        <span class="spantxt">�Թ�ش˹ع</span>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblInSubsidies" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutSubsidiesRt" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutSubsidies" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblBalanceSubsidies" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cutbudget">
                                        <span class="spantxt">�����ʶҹ�֡��</span>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblInRevenue" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutRevenueRt" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutRevenue" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblBalanceRevenue" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cutbudget">
                                        <span class="spantxt">���¹��� 15 ��</span>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblInFree" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutFreeRt" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutFree" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblBalanceFree" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cutbudget">
                                        <span class="spantxt">��� �</span>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblInOther" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutOtherRt" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblOutOther" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2">
                                        <asp:Label ID="lblBalanceOther" CssClass="spantxt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cutbudget cutbudgetBg">
                                        <span class="spantxt2">���</span>
                                    </td>
                                    <td class="cutbudget2 cutbudgetBg">
                                        <asp:Label ID="lblTotalIn" CssClass="spantxt2" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2 cutbudgetBg">
                                        <asp:Label ID="lblTotalOutRt" CssClass="spantxt2" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2 cutbudgetBg">
                                        <asp:Label ID="lblTotalOut" CssClass="spantxt2" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="cutbudget2 cutbudgetBg">
                                        <asp:Label ID="lblTotalBalance" CssClass="spantxt2" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="ckSign">
                                <asp:CheckBox ID="cbNotSign" runat="server" Text=" ���͡�����§ҹ�������ǹ�繪���"/>
                                <asp:CheckBox ID="cbNotLogo" runat="server" Text=" ���͡�����§ҹ����ʴ�����"/>
                            </div>
                        </div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" ShowFooter="true" OnRowDataBound="dgSummary_RowDataBound" PageSize="40">
                                <Columns>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="͹��ѵ�" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img title="<%# (Convert.ToInt32(Eval("ApproveFlag"))== 0?"�ѧ���͹��ѵ�":"͹��ѵ�����") %>"
                                                src='../Image/<%# (Convert.ToInt32(Eval("ApproveFlag"))== 0?"no":"ok") %>.png' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺤Ӣ�">
                                        <ItemTemplate>
                                            <%# checkapprove(Eval("AcCode").ToString(), 1)%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ʶҹ�" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img title="<%# (Eval("status").ToString()=="0"?"�ʹ��Թ���":(Eval("status").ToString()=="1"?"���ѧ���Թ���":(Eval("status").ToString()=="2"?"��¡�˹����":(Eval("status").ToString()=="3"?"���Թ�����������":"���֧��˹�")))) %>"
                                                src='../Image/<%# (Eval("status").ToString()=="0"?"00":(Eval("status").ToString()=="1"?"01":(Eval("status").ToString()=="2"?"02":(Eval("status").ToString()=="3"?"03":"04")))) %>.png' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���͡Ԩ����">
                                        <ItemTemplate>
                                            <%# PjOrAcName(Eval("AcCode").ToString(), Eval("FullName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="27%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="˹��§ҹ">
                                        <ItemTemplate>
                                            <%# Eval("DeptName").ToString()%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        <FooterTemplate>
                                            ��� :
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <span class="<%# spnCutBudget(Eval("AcCode").ToString(), Eval("UpdateDate").ToString()) %>">
                                                <%# GetTotalAmount1(Convert.ToDecimal(Eval("TotalAmount1").ToString())).ToString("N2")%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount1().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��������">
                                        <ItemTemplate>
                                            <%# GetTotalAmount2(Convert.ToDecimal(Eval("TotalAmount2").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount2().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���������">
                                        <ItemTemplate>
                                            <%# GetTotalAmount3(Convert.ToDecimal(Eval("TotalBalance").ToString())).ToString("N2")%> 
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount3().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" />
                            </Control:DataGridView>
                            <div id="divTotal" runat="server" class="footerTotal">
                                ���������ҳ����������� : <span id="ToltalBudget" class="lblTotal1" runat="server"></span>�ҷ <span style="padding-left: 25px;">���������ҳ���ԧ������ : </span><span id="ToltalBudget2" class="lblTotal1" runat="server"></span> �ҷ <span style="padding-left: 25px;">�����������ͷ����� : </span><span id="TotalBalance" class="lblTotal3" runat="server"></span> �ҷ
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
      </ajax:AjaxPanel>
</asp:Content>
