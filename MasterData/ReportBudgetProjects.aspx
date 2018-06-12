<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="ReportBudgetProjects.aspx.cs" Inherits="ReportBudgetProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function getPopUp(inx, id) {
            if (inx == 1) {
                dialogBox.show('../PopUp/PopUpActivityDetail.aspx?id=' + id, '��������´�Ԩ����', '1000', '600', '#000', true);
            }
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
                    ������ҳ��������ç���
                </div>
                <div class="spaceDiv"></div>
                <div id="Div1" class="TableSearch">
                    <div class="SearchTable">
                        <div class="SearchHead">
                            <span class="spantxt2 spansize14">�����ç��� / �Ԩ����</span>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="inputrow">
                            <div class="SearchtxtF">
                                <span id="lblYear" class="spantxt">�է�����ҳ : </span>
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
                                <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="500px">
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
                </div>
                <div class="clear"></div>
                <div class="spaceDiv"></div>
                <div class="gridViewDiv">
                    <Control:DataGridView ID="GridView1" runat="server" ShowFooter="true" OnRowDataBound="dgSummary_RowDataBound">
                        <Columns>
                            <Control:TemplateField HeaderText="�ӴѺ���">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                    BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="�����ç��� / �Ԩ����">
                                <ItemTemplate>
                                    <%# PjOrAcName(Eval("AcCode").ToString(), Eval("FullName").ToString())%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                <FooterTemplate>
                                    ��� :
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                    BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="˹��§ҹ">
                                <ItemTemplate>
                                    <%# Eval("DeptName").ToString()%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                    BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="������ҳ�����">
                                <ItemTemplate>
                                    <%# GetTotalAmount1(decimal.Parse(Eval("TotalAmount1").ToString())).ToString("N2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                <FooterTemplate>
                                    <%# GetSumTotalAmount1().ToString("N2")%>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                    BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="������ҳ������">
                                <ItemTemplate>
                                    <%# GetTotalAmount2(decimal.Parse(Eval("TotalAmount2").ToString())).ToString("N2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                                <FooterTemplate>
                                    <%# GetSumTotalAmount2().ToString("N2")%>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                    BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="������ҳ�������">
                                <ItemTemplate>
                                    <%# GetTotalAmount3(decimal.Parse(Eval("TotalBalance").ToString())).ToString("N2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
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
                        ���������ҳ����������� : <span id="ToltalBudget" class="lblTotal1" runat="server"></span>�ҷ <span style="padding-left: 50px;">���������ҳ���ԧ������ : </span><span id="ToltalBudget2" class="lblTotal2" runat="server"></span> �ҷ <span style="padding-left: 50px;">�����������ͷ����� : </span><span id="TotalBalance" class="lblTotal3" runat="server"></span> �ҷ
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
