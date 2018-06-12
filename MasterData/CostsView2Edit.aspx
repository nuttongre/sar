<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="CostsView2Edit.aspx.cs" Inherits="CostsView2Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function EditItem(id) {
            location.href = "Costs2Edit.aspx?mode=2&id=" + id;
        }
        function printRpt(mode, type, id) {
            var ckSing = $get("<%=cbNotSign.ClientID%>").checked;
            var ckLogo = $get("<%=cbNotLogo.ClientID%>").checked;
            if (mode == 31) {
                var yearB = $get("<%=ddlSearchYear.ClientID%>").value;
                window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type + "&yearB=" + yearB + "&ckLogo=" + ckLogo);
            }
            else {
                window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type + "&ckSign=" + ckSing + "&ckLogo=" + ckLogo);
            }
        }
        function getPopUp(inx, id) {
            if (inx == 1) {
                dialogBox.show('../PopUp/PopUpActivityDetail.aspx?id=' + id, '��������´�Ԩ����', '1000', '600', '#000', true, 'yes');
            }
        }
        function AttachShow(id) {
                dialogBox.show('../PopUp/PopUpAttachFile.aspx?id=' + id, '�ʴ��͡������Ṻ', '650', '400', '#000', true, 'yes');
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
                    �ѹ�֡����������¡Ԩ����
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">���ҡԨ����</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span id="lblSearchYear" runat="server" class="spantxt">�ա���֡�� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchYear" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">���ط�� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch2" Width="500px" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSearch2_SelectedIndexChanged">
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
                                            OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged" Width="350px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchEmp" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchEmp_SelectedIndexChanged" Width="250px">
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
                            <div class="ckSign">
                                <asp:CheckBox ID="cbNotSign" runat="server" Text=" ���͡�����§ҹ�������ǹ�繪���" />
                                <asp:CheckBox ID="cbNotLogo" runat="server" Text=" ���͡�����§ҹ����ʴ�����"/>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="���͡">
                                        <ItemTemplate>
                                            <a href="Costs2Edit.aspx?mode=2&id=<%#Eval("ActivityCode")%>">
                                                <img style="border: 0; cursor: pointer;" title="���͡仺ѹ�֡�������¨�ԧ" src="../Image/<%# (((Eval("Status").ToString()== "3") || (Eval("TotalAmount2").ToString()!="0")) ? "goto2":"goto")%>.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ʶҹ�" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img style="border: none; cursor: pointer;" title="<%# (Eval("status").ToString()=="0"?"�ʹ��Թ���":(Eval("status").ToString()=="1"?"���ѧ���Թ���":(Eval("status").ToString()=="2"?"��¡�˹����":(Eval("status").ToString()=="3"?"���Թ�����������":"���֧��˹�")))) %>"
                                                src='../Image/<%# (Eval("status").ToString()=="0"?"00":(Eval("status").ToString()=="1"?"01":(Eval("status").ToString()=="2"?"02":(Eval("status").ToString()=="3"?"03":"04")))) %>.png' onclick="getPopUp(1, '<%#Eval("ActivityCode")%>');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�Դ���" SortExpression="ActivityStatus">
                                        <ItemTemplate>
                                            <%# getActivityStatus(Eval("ActivityStatus").ToString()) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�Ԩ����">
                                        <ItemTemplate>
                                            <%# GetActivityName(Eval("ActivityName").ToString(), Eval("ActivityCode").ToString(), Eval("Status").ToString(), Eval("TotalAmount2").ToString()) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="25%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="�Ҥ���¹���" DataField="Term">
                                        <ItemStyle Width="7%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�������ҹ" DataField="CostsType">
                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="����������">
                                        <ItemTemplate>
                                            <%# GetBudget(decimal.Parse(Eval("TotalAmount").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <FooterTemplate>
                                            <%# GetTotalBudget().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��������ԧ">
                                        <ItemTemplate>
                                            <%# GetBudget2(decimal.Parse(Eval("TotalAmount2").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <FooterTemplate>
                                            <%# GetTotalBudget2().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���Ṻ">
                                        <ItemTemplate>
                                            <%# AttachShow(Eval("ActivityCode").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�鹷ع�ż�Ե">
                                        <ItemTemplate>
                                            <%# checkStatus(Eval("ActivityCode").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��ػ��ô��Թ�ҹ">
                                        <ItemTemplate>
                                            <%# checkResult(Eval("ActivityCode").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��§ҹ">
                                        <ItemTemplate>
                                            <%# checkRpt(Eval("ActivityCode").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("ActivityCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" />
                            </Control:DataGridView>
                            <div id="divTotal" runat="server" class="footerTotal">
                                ���������ҳ����������� : <span id="ToltalBudget" class="lblTotal1" runat="server"></span>�ҷ <span style="padding-left: 50px;">���������ҳ���ԧ������ : </span><span id="ToltalBudget2" class="lblTotal2" runat="server"></span> �ҷ
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
