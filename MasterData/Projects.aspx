<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Projects.aspx.cs" Inherits="Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Projects.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Projects.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Projects.aspx?mode=3&id=" + id;
        }
        function gotoItem(id) {
            location.href = "Activity.aspx?mode=1&id=" + id;
        }
        function Cancel() {
            location.href = "Projects.aspx";
        }
        function printRpt(mode, type, id) {
            var ckSing = $get("<%=cbNotSign.ClientID%>").checked;
            var ckLogo = $get("<%=cbNotLogo.ClientID%>").checked;
            var ckMg = $get("<%=cbNotManager.ClientID%>").checked;
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type + "&ckSign=" + ckSing + "&ckLogo=" + ckLogo + "&ckMg=" + ckMg);
        }
        function Cktxt(m) {
            var ck = 0;
            var ddlStrategies = $get("<%=ddlStrategies.ClientID%>");
            var ErrorStrategies = $get("ErrorStrategies");
            var txtProjectsDetail = $get("<%=txtProjectsDetail.ClientID%>");
            var ErrorProjectsDetail = $get("ErrorProjectsDetail");
            var txtProjects = $get("<%=txtProjects.ClientID%>");
            var ErrorProjects = $get("ErrorProjects");
            var txtPurpose = $get("<%=txtPurpose.ClientID%>");
            var ErrorPurpose = $get("ErrorPurpose");
            var txtPurpose2 = $get("<%=txtPurpose2.ClientID%>");
            var ErrorPurpose2 = $get("ErrorPurpose2");
            var txtTarget = $get("<%=txtTarget.ClientID%>");
            var ErrorTarget = $get("ErrorTarget");
            var txtTarget2 = $get("<%=txtTarget2.ClientID%>");
            var ErrorTarget2 = $get("ErrorTarget2");
            var txtPeriod1 = $get("<%=txtPeriod1.ClientID%>");
            var ErrorPeriod1 = $get("ErrorPeriod1");
            var txtExpected = $get("<%=txtExpected.ClientID%>");
            var ErrorExpected = $get("ErrorExpected");
            var txtResponsibleName = $get("<%=txtResponsibleName.ClientID%>");
            var txtResponsiblePosition = $get("<%=txtResponsiblePosition.ClientID%>");
            var ErrorResponsible = $get("ErrorResponsible");
            var txtProposeName = $get("<%=txtProposeName.ClientID%>");
            var txtProposePosition = $get("<%=txtProposePosition.ClientID%>");
            var ErrorPropose = $get("ErrorPropose");
            var txtApprovalName = $get("<%=txtApprovalName.ClientID%>");
            var txtApprovalPosition = $get("<%=txtApprovalPosition.ClientID%>");
            var ErrorApproval = $get("ErrorApproval");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckTxtNull(m, txtApprovalPosition, ErrorApproval);
            ck += ckTxtNull(m, txtApprovalName, ErrorApproval);
            ck += ckTxtNull(m, txtProposePosition, ErrorPropose);
            ck += ckTxtNull(m, txtProposeName, ErrorPropose);
            ck += ckTxtNull(m, txtResponsiblePosition, ErrorResponsible);
            ck += ckTxtNull(m, txtResponsibleName, ErrorResponsible);
            ck += ckTxtNull(m, txtExpected, ErrorExpected);
            ck += ckTxtNull(m, txtPeriod1, ErrorPeriod1);
            ck += ckTxtNull(m, txtTarget2, ErrorTarget2);
            ck += ckTxtNull(m, txtTarget, ErrorTarget);
            ck += ckTxtNull(m, txtPurpose2, ErrorPurpose2);
            ck += ckTxtNull(m, txtPurpose, ErrorPurpose);
            ck += ckTxtNull(m, txtProjectsDetail, ErrorProjectsDetail);
            ck += ckTxtNull(m, txtProjects, ErrorProjects);
            ck += ckDdlNull(m, ddlStrategies, ErrorStrategies);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }

        function Cktxt2(m) {
            var ck = 0;
            var ddlCreateUser = $get("<%=ddlCreateUser.ClientID%>");
            var ErrorCreateUser = $get("ErrorCreateUser");
            ck += ckDdlNull(m, ddlCreateUser, ErrorCreateUser);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function editmode(id) {
            var IndicatorsName = $get("spnIndicatorsName" + id).innerHTML;
            var MethodAss = $get("spnMethodAss" + id).innerHTML;
            var ToolsAss = $get("spnToolsAss" + id).innerHTML;

            $get("<%=txtid2.ClientID%>").value = id;
            $get("<%=txtIndicatorsName.ClientID%>").value = Trim(IndicatorsName);
            $get("<%=txtMethodAss.ClientID%>").value = Trim(MethodAss);
            $get("<%=txtToolsAss.ClientID%>").value = Trim(ToolsAss);
        }
    </script>
    <style type="text/css">
        .txtBoxlblnone2 {
        border:none 0px #FFFFFF;
        color: #424242;
        font-weight:bold;    
        font-size:medium;
        width:200px;
        text-align:right;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageDiv">
                <div class="warningDiv">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="headTable">
                    �Ѵ���ç��� / �ҹ
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">�����ç��� / �ҹ</span>
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
                                        <span class="spantxt">�Ӥ��� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" Text="  " />
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
                            <div class="btAddDiv">
                                <input type="button" class="btAdd" onclick="AddItem();" value="      ���ҧ�ç��� / �ҹ����" title="���ҧ�ç��� / �ҹ����" />
                            </div>
                            <div class="ckSign">
                                <asp:CheckBox ID="cbNotSign" runat="server" Text=" ���͡�����§ҹ�������ǹ�繪���"/> 
                                <asp:CheckBox ID="cbNotLogo" runat="server" Text=" ���͡�����§ҹ����ʴ�����"/>
                                <asp:CheckBox ID="cbNotManager" runat="server" Text=" ���͡�����§ҹ����ʴ����͹��ѵ��ç���"/>
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" ShowFooter="true" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�����ç��� / �ҹ">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("ProjectsCode").ToString(), Eval("EmpID").ToString(), Eval("ProjectsName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="27%" />
                                        <FooterTemplate>
                                            ��� :
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��������">
                                        <ItemTemplate>
                                            <%# GetSetBudget(decimal.Parse(Eval("SetBudget").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="9%" />
                                        <FooterTemplate>
                                            <%# GetTotalSetBudget().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�����ԧ">
                                        <ItemTemplate>
                                            <%# GetBudget(decimal.Parse(Eval("SetBudget").ToString()), decimal.Parse(Eval("TotalAmount").ToString()))%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="9%" />
                                        <FooterTemplate>
                                            <%# GetTotalBudget().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <%--<Control:BoundField HeaderText="��.����֡�Ҫҵ�" DataField="StandardNation">
                                        <ItemStyle Width="12%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>--%>
                                    <%--<Control:BoundField HeaderText="��.�������з�ǧ" DataField="StandardMinistry">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>--%>
                                    <Control:BoundField HeaderText="������ҧ" DataField="EmpName">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="㺷�� 1" HeaderToolTip="㺤Ӣͷ�� 1">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(12,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �ʴ��Ԩ���� Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(12,'e','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �ʴ��Ԩ���� Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(12,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �ʴ��Ԩ���� Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺷�� 2" HeaderToolTip="㺤Ӣͷ�� 2">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(24,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� ������ҵðҹ ��Ǫ���Ѵ Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(24,'e','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� ������ҵðҹ ��Ǫ���Ѵ Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(24,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� ������ҵðҹ ��Ǫ���Ѵ Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺷�� 3" HeaderToolTip="㺤Ӣͷ�� 3">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(46,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �¡��ô��Թ�ҹ PDCA Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(46,'e','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �¡��ô��Թ�ҹ PDCA Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(46,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �¡��ô��Թ�ҹ PDCA Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺷�� 4" HeaderToolTip="㺤Ӣͷ�� 4">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(50,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �¡������ Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(50,'e','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �¡������ Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(50,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �¡������ Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺷�� 5" HeaderToolTip="㺤Ӣͷ�� 5">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(47,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �աԨ������ѡ Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(47,'e','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �աԨ������ѡ Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(47,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �աԨ������ѡ Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺷�� 6" HeaderToolTip="㺤Ӣͷ�� 6">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(56,'w','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �ʴ���¡�ä������µ���Ԩ���� Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(56,'e','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �ʴ���¡�ä������µ���Ԩ���� Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(56,'p','<%#Eval("ProjectsCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺤Ӣ��ç��� �ʴ���¡�ä������µ���Ԩ���� Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("ProjectsCode").ToString(), Eval("EmpID").ToString(), "")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <%# checkdelete(Eval("ProjectsCode").ToString(), Eval("EmpID").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�ӵ��">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="gotoItem('<%#Eval("ProjectsCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="���ҧ�Ԩ��������������ç��ù��" src="../Image/goto.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" />
                            </Control:DataGridView>
                            <div class="footerTotal">
                                ���������ҳ������ : <span id="ToltalBudget" class="lblTotal1" runat="server"></span>�ҷ
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                    <asp:View ID="edit" runat="server">
                        <div id="table1" class="PageManageDiv">
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="lblYear" runat="server" class="spantxt">�ա���֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearS" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearS_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divStrategic" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategic" CssClass="spantxt" runat="server" Text="�ط���ʵ�� : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategic" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlStrategies" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStrategies_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStrategies" class="ErrorMsg">��س����͡���ط��</span>
                                </div>
                            </div>
                            <div id="divStrategies" class="inputrowH" runat="server" visible="false">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط�� (�������) : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategies" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divCorporateStrategy" class="inputrowH" runat="server" visible="false">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط���дѺͧ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlCorporateStrategy" Width="600px" runat="server" Visible="false">
                                    </asp:DropDownList>
                                    <asp:CheckBoxList ID="cblCorporateStrategy" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStrategicPlan" class="inputrowH" runat="server" visible="false">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط���дѺἹ�ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlStrategicPlan" Width="600px" runat="server" Visible="false">
                                    </asp:DropDownList>
                                    <asp:CheckBoxList ID="cblStrategicPlan" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStrategicObjectives" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategicObjectives" CssClass="spantxt" runat="server" Text="�ѵ�ػ��ʧ���ԧ���ط�� :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategicObjectives" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divTargetMode" runat="server" class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������¡�þѲ�Ҥس�Ҿ��ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblTargetMode" runat="server" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divPlan" runat="server" class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">Ἱ�ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPlan" runat="server" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divProjectDes" runat="server" class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѡɳЧҹ / �ç��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblProjectDes" runat="server" RepeatColumns ="2">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ��Ԩ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlMission" Width="600px" runat="server" Visible="false">
                                    </asp:DropDownList>
                                    <asp:CheckBoxList ID="cblMission" runat="server">
                                    </asp:CheckBoxList>
                                    <%--<span class="ColorRed">*</span>--%> <span id="ErrorMission" class="ErrorMsg">��س����͡�ѹ��Ԩ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��һ��ʧ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlGoals" Width="600px" runat="server" Visible="false">
                                    </asp:DropDownList>
                                    <asp:CheckBoxList ID="cblGoals" runat="server">
                                    </asp:CheckBoxList>
                                    <%--<span class="ColorRed">*</span>--%> <span id="ErrorGoals" class="ErrorMsg">��س����͡��һ��ʧ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ç��� / �ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProjects" runat="server" MaxLength="300" Width="600px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorProjects" class="ErrorMsg">��سһ�͹�ç���/�ҹ</span>
                                </div>
                            </div>
                            <div id="divPlanNo" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPlanNo" CssClass="spantxt" runat="server" Text="Ἱ�Ѳ�����з�� : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum txt50" ID="txtPlanNo" runat="server" onkeypress="return KeyNumber(event);" MaxLength="3" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divIdentityName2" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblIdentityName2" CssClass="spantxt" runat="server" Text="�ѵ�ѡɳ� : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtIdentityName2" runat="server" Width="600px" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divIdentityName" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblIdentityName" CssClass="spantxt" runat="server" Text="�͡�ѡɳ� : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtIdentityName" runat="server" Width="600px" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divStatute" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStatute" CssClass="spantxt" runat="server" Text="����Ҫ�ѭ�ѵԡ���֡�� �ҵ�ҷ�� : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtStatute" runat="server" Width="600px" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divPolicy" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPolicy" CssClass="spantxt" runat="server" Text="��º�� :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPolicy" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStandardNation" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStandardNation" CssClass="spantxt" runat="server" Text="�ҵðҹ����֡�Ҫҵ� : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStandardNation" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStandardMinistry" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStandardMinistry" CssClass="spantxt" runat="server" Text="�ҵðҹ�������з�ǧ : " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStandardMinistry" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divNewStandard" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblNewStandard" CssClass="spantxt" runat="server" Text="�ҵðҹ :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblNewStandard" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStrategySPT" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategySPT" CssClass="spantxt" runat="server" Text="���ط�� ʾ�. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategySPT" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStrategySPM" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStrategySPM" CssClass="spantxt" runat="server" Text="���ط�� ʾ�. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStrategySPM" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divStandardSPM" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblStandardSPM" CssClass="spantxt" runat="server" Text="�ҵðҹ ʾ�. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblStandardSPM" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divIndicatorSMS" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblIndicatorSMS" CssClass="spantxt" runat="server" Text="��Ǫ���Ѵ ���. :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblIndicatorSMS" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divPopularity" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPopularity" CssClass="spantxt" runat="server" Text="��ҹ��� 12 ��С�� :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPopularity" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divPrimePolicy" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPrimePolicy" CssClass="spantxt" runat="server" Text="��º�¹�¡�Ѱ����� :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPrimePolicy" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divPoliciesMinister" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPoliciesMinister" CssClass="spantxt" runat="server" Text="��º���Ѱ�������ҡ�á�з�ǧ :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPoliciesMinister" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divPolicyKSN" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblPolicyKSN" CssClass="spantxt" runat="server" Text="��º�¡���֡�ҹ͡�к� (�ȹ.) :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblPolicyKSN" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divWorldClass" class="inputrowH" runat="server">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��ҹ�ҵðҹ�ҡ� : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlWorldClassSide" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorldClassSide_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">�ҵðҹ�ҡ� : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlWorldClassStandard" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorldClassStandard_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                    </div>
                                    <div class="divB_Head">
                                        <asp:Button ID="btDelWorldClass" CssClass="btDelete" runat="server" Visible="false" OnClick="btDelWorldClass_Click" />
                                        <Control:DataGridView ID="gridWorldClass" runat="server" AutoGenerateCheckList="true" Width="65%">
                                            <Columns>
                                                <Control:TemplateField HeaderText="�ӴѺ���">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                </Control:TemplateField>
                                                <Control:BoundField HeaderText="��ҹ�ҵðҹ�ҡ�" DataField="WorldClassSideName">
                                                    <ItemStyle Width="45%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                                <Control:BoundField HeaderText="�ҵðҹ�ҡ�" DataField="WorldClassStandardName">
                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" />
                                        </Control:DataGridView>
                                    </div>
                                </div>
                            </div>
                            <div id="divRoyalAward" class="inputrowH" runat="server">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��ҹ�ҧ��ž���Ҫ�ҹ : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlRoyalAwardSide" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoyalAwardSide_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��Ǫ���Ѵ�ҧ��ž���Ҫ�ҹ : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlRoyalAwardIndicator" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoyalAwardIndicator_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                    </div>
                                    <div class="divB_Head">
                                        <asp:Button ID="btDelRoyalAward" CssClass="btDelete" runat="server" Visible="false" OnClick="btDelRoyalAward_Click" />
                                        <Control:DataGridView ID="gridRoyalAward" runat="server" AutoGenerateCheckList="true" Width="65%">
                                            <Columns>
                                                <Control:TemplateField HeaderText="�ӴѺ���">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                </Control:TemplateField>
                                                <Control:BoundField HeaderText="��ҹ�ҧ��ž���Ҫ�ҹ" DataField="RoyalAwardSideName">
                                                    <ItemStyle Width="45%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                                <Control:BoundField HeaderText="��Ǫ���Ѵ�ҧ��ž���Ҫ�ҹ" DataField="RoyalAwardIndicatorName">
                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" />
                                        </Control:DataGridView>
                                    </div>
                                </div>
                            </div>
                            <div id="divSuffice" class="inputrowH" runat="server">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��ҹ��Ѫ�����ɰ�Ԩ����§ : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlSufficeSide" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSufficeSide_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">ͧ���Сͺ��Ѫ�����ɰ�Ԩ����§ : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlSufficeStandard" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSufficeStandard_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��Ǻ觪���Ѫ�����ɰ�Ԩ����§ : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlSufficeIndicator" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSufficeIndicator_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                    </div>
                                    <div class="divB_Head">
                                        <asp:Button ID="btDelSuffice" CssClass="btDelete" runat="server" Visible="false" OnClick="btDelSuffice_Click" />
                                        <Control:DataGridView ID="gridSuffice" runat="server" AutoGenerateCheckList="true" Width="65%">
                                            <Columns>
                                                <Control:TemplateField HeaderText="�ӴѺ���">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                </Control:TemplateField>
                                                <Control:BoundField HeaderText="��ҹ��Ѫ�����ɰ�Ԩ����§" DataField="SufficeSideName">
                                                    <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                                <Control:BoundField HeaderText="ͧ���Сͺ��Ѫ�����ɰ�Ԩ����§" DataField="SufficeStandardName">
                                                    <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                                <Control:BoundField HeaderText="��Ǻ觪���Ѫ�����ɰ�Ԩ����§" DataField="SufficeIndicatorName">
                                                    <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" />
                                        </Control:DataGridView>
                                    </div>
                                </div>
                            </div>
                            <div id="divFourthAssessment" class="inputrowH" runat="server">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��ҹ��û����Թ��¹͡�ͺ 4 : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlFourthAssessmentSide" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFourthAssessmentSide_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��Ǻ觪���û����Թ��¹͡�ͺ 4 : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList ID="ddlFourthAssessmentIndicator" CssClass="ddl" Width="600px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFourthAssessmentIndicator_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                    </div>
                                    <div class="divB_Head">
                                        <asp:Button ID="btDelFourthAssessment" CssClass="btDelete" runat="server" Visible="false" OnClick="btDelFourthAssessment_Click" />
                                        <Control:DataGridView ID="gridFourthAssessment" runat="server" AutoGenerateCheckList="true" Width="65%">
                                            <Columns>
                                                <Control:TemplateField HeaderText="�ӴѺ���">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                </Control:TemplateField>
                                                <Control:BoundField HeaderText="��ҹ��û����Թ��¹͡�ͺ 4" DataField="FourthAssessmentSideName">
                                                    <ItemStyle Width="45%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                                <Control:BoundField HeaderText="��Ǻ觪���û����Թ��¹͡�ͺ 4" DataField="FourthAssessmentIndicatorName">
                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                </Control:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" />
                                        </Control:DataGridView>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��ѡ�������˵ؼ� : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:TextBox CssClass="txtBoxL" ID="txtProjectsDetail" runat="server" Width="600px"
                                            TextMode="MultiLine" Height="80px"></asp:TextBox>
                                        <span class="ColorRed">*</span> <span id="ErrorProjectsDetail" class="ErrorMsg">��سһ�͹��ѡ�������˵ؼ�</span>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѵ�ػ��ʧ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width: 10%;">
                                            <span class="spantxt">�ż�Ե (outputs)</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtPurpose" runat="server" Width="500px" TextMode="MultiLine"
                                                Height="80px"></asp:TextBox>
                                            <span class="ColorRed">*</span> <span id="ErrorPurpose" class="ErrorMsg">��سһ�͹�ѵ�ػ��ʧ��</span>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width: 10%;">
                                            <span class="spantxt">���Ѿ�� (outcomes)</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtPurpose2" runat="server" Width="500px" TextMode="MultiLine"
                                                Height="100px"></asp:TextBox>
                                            <span class="ColorRed">*</span> <span id="ErrorPurpose2" class="ErrorMsg">��سһ�͹�ѵ�ػ��ʧ��</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������� : </span>
                                </div>
                                <div class="divB_Head">
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width: 10%;">
                                            <span class="spantxt">�ԧ����ҳ</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtTarget" runat="server" Width="500px" TextMode="MultiLine"
                                                Height="100px"></asp:TextBox>
                                            <span class="ColorRed">*</span> <span id="ErrorTarget" class="ErrorMsg">��سһ�͹��������ԧ����ҳ</span>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width: 10%;">
                                            <span class="spantxt">�ԧ�س�Ҿ</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtTarget2" runat="server" Width="500px" TextMode="MultiLine"
                                                Height="100px"></asp:TextBox>
                                            <span class="ColorRed">*</span> <span id="ErrorTarget2" class="ErrorMsg">��سһ�͹��������ԧ�س�Ҿ</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�������Ҵ��Թ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtPeriod1" runat="server" Width="600px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorPeriod1" class="ErrorMsg">��سһ�͹��������</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��û����Թ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width:150px;">
                                            <span class="spantxt">��Ǻ觪���Ѵ��������� : </span>
                                            <asp:TextBox CssClass="txtBoxnone" ID="txtid2" runat="server" Width="0px"></asp:TextBox>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtIndicatorsName" runat="server" Width="400px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width:150px;">
                                            <span class="spantxt">�Ըա�û����Թ : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtMethodAss" runat="server" Width="400px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head" style="width:150px;">
                                            <span class="spantxt">����ͧ��ͷ���� : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtToolsAss" runat="server" Width="400px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                            <asp:Button ID="btaddAssessment" CssClass="btAdd" runat="server" OnClientClick="return ckAddAssessment();"
                                                OnClick="btaddAssessment_Click" Text="      ������¡��" ToolTip="������¡������" />
                                        </div>
                                    </div>
                                    <span id="ErrorProjectsAssessment" class="ErrorMsg">��سһ�͹��û����Թ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                </div>
                                <div class="divB_Head">
                                    <asp:Button ID="btDelAssessment" CssClass="btDelete" runat="server" Visible="false"
                                        OnClick="btDelAssessment_Click" />
                                    <Control:DataGridView ID="GridViewAssessment" runat="server" AutoGenerateCheckList="true"
                                        Width="600" AutoGenerateColumns="False" PageSize="50">
                                        <Columns>
                                            <Control:TemplateField HeaderText="��Ǻ觪���Ѵ���������">
                                                <ItemTemplate>
                                                    <span id="spnIndicatorsName<%#Container.DataItemIndex %>">
                                                        <%# Eval("IndicatorsName").ToString()%></span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="45%" />
                                            </Control:TemplateField>
                                            <Control:TemplateField HeaderText="�Ըա�û����Թ">
                                                <ItemTemplate>
                                                    <span id="spnMethodAss<%#Container.DataItemIndex %>">
                                                        <%# Eval("MethodAss").ToString()%></span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                                            </Control:TemplateField>
                                            <Control:TemplateField HeaderText="����ͧ��ͷ����">
                                                <ItemTemplate>
                                                    <span id="spnToolsAss<%#Container.DataItemIndex %>">
                                                        <%# Eval("ToolsAss").ToString()%></span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                                            </Control:TemplateField>
                                            <Control:TemplateField HeaderText="���">
                                                <ItemTemplate>
                                                    <img src="../Image/edit.gif" style="cursor: pointer;" onclick="editmode('<%#Container.DataItemIndex %>');" title="���͡�������" alt="���͡�������" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </Control:TemplateField>
                                        </Columns>
                                    </Control:DataGridView>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ŷ��Ҵ��Ҩ����Ѻ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtExpected" runat="server" Width="600px" TextMode="MultiLine"
                                        Height="80px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorExpected" class="ErrorMsg">��سһ�͹�ŷ��Ҵ��Ҩ����Ѻ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������ҳ������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum" ID="txtSetBudget" runat="server" onchange="txtComma(this);" onclick="SelecttxtAll(this);" 
                                        onblur="txtZero(this,0);" onkeypress="return KeyNumber(event);"
                                        Width="100px" Text="0"></asp:TextBox><span class="spantxt2"> �ҷ</span>
                                </div>
                            </div>
                            <div id="divResponseProblems" class="inputrowH" runat="server">
                                <div class="divF_Head">
                                    <asp:Label ID="lblResponseProblems" CssClass="spantxt" runat="server" Text="�ç��ù��ͺʹͧ�ѭ�ҷ���ҹ������ͧ :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBoxList ID="cblResponseProblems" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divDeptProjectsName" class="inputrowH" runat="server" visible="false">
                                <div class="divF_Head">
                                    <asp:Label ID="lblDeptProjectsName" CssClass="spantxt" runat="server" Text="˹��§ҹ����Ѻ�Դ�ͺ�ç��� :  " Visible="false"></asp:Label>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtDeptProjectsName" CssClass="txtBoxL" Width="600" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <%--<span class="spantxt">����Ѻ�Դ�ͺ�ç��� : </span>--%>
                                    <asp:TextBox ID="spnResponsibleName" CssClass="txtBoxlblnone2" Width="200px" AutoPostBack="true" OnTextChanged="spnResponsibleName_TextChanged" runat="server"></asp:TextBox><span class="spantxt"> : </span> 
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtResponsibleName" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtResponsiblePosition" runat="server" Width="270px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorResponsible" class="ErrorMsg">��سһ�͹�����ż���Ѻ�Դ�ͺ�ç���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����ʹ��ç��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProposeName" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtProposePosition" runat="server" Width="270px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorPropose" class="ErrorMsg">��سһ�͹�����ż���ʹ��ç���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <%--<span class="spantxt">�����繪ͺ�ç��� : </span>--%>
                                    <asp:TextBox ID="spnProjectsApprovalName" CssClass="txtBoxlblnone2" Width="200px" AutoPostBack="true" OnTextChanged="spnProjectsApprovalName_TextChanged" runat="server"></asp:TextBox><span class="spantxt"> : </span> 
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalName" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalPosition" runat="server" Width="270px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorApproval" class="ErrorMsg">��سһ�͹�����ż����繪ͺ�ç���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <%--<span class="spantxt">�����繪ͺ�ç��÷�� 2 : </span>--%>
                                    <asp:TextBox ID="spnProjectsApprovalName2" CssClass="txtBoxlblnone2" Width="200px" AutoPostBack="true" OnTextChanged="spnProjectsApprovalName2_TextChanged" runat="server"></asp:TextBox><span class="spantxt"> : </span> 
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalName2" runat="server" Width="250px"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox CssClass="txtBoxL" ID="txtApprovalPosition2" runat="server" Width="270px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ӴѺ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);"
                                        Width="50px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <asp:Label ID="lblCreate" runat="server" CssClass="spantxt4"></asp:Label>&nbsp;<asp:LinkButton ID="lbtEditCreate" runat="server" Visible="false" OnClick="lbtEditCreate_Click">���</asp:LinkButton>
                            </div>
                            <div id="divSearchCreateUser" runat="server" class="centerDiv" style="min-height:25px;" visible="false">
                                <asp:TextBox ID="txtSearchCreateUser" CssClass="txtSearch" runat="server" Width="200"></asp:TextBox><asp:Button CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearchCreateUser" runat="server" OnClick="btSearchCreateUser_Click" ToolTip="�������ͤ���" Text="  " />                                
                            </div>
                            <div id="divddlCreateUser" runat="server" class="centerDiv" style="min-height:30px;" visible="false">
                                <asp:DropDownList ID="ddlCreateUser" CssClass="ddl" runat="server"></asp:DropDownList><span id="ErrorCreateUser" class="ErrorMsg">���͡����Ѻ�Դ�ͺ�ç���</span>
                            </div>
                            <div id="divbtnEditCreate" runat="server" class="centerDiv" style="min-height:25px;" visible="false">
                                <asp:Button ID="btnEditCreate" runat="server" Text="�ѹ�֡" OnClientClick="return Cktxt2(1);" OnClick="btnEditCreate_Click" /><asp:Button ID="btnCancelCreate" runat="server" Text="¡��ԡ" OnClick="btnCancelCreate_Click" />
                            </div>
                            <div class="centerDiv">
                                <asp:Label ID="lblUpdate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <div class="classButton">
                                    <div class="classBtSave">
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click"
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡�ç���/�ҹ���" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧ�ç���/�ҹ����"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡�ç���/�ҹ���������ҧ�ç���/�ҹ����" />
                                        <asp:Button ID="btSaveAndGo" CssClass="btYesAndGo" runat="server" Text="       �ѹ�֡������ҧ�Ԩ�������"
                                            OnClick="btSaveAndGo_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡�ç���/�ҹ���������ҧ�Ԩ�������" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡�ç���/�ҹ���" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                                    <Columns>
                                        <Control:TemplateField HeaderText="�ӴѺ���">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        </Control:TemplateField>
                                        <Control:BoundField HeaderText="�����ç��� / �ҹ" DataField="ProjectsName">
                                            <ItemStyle Width="80%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="������ҳ" DataField="TotalAmount" DataFormatString="{0:n2}">
                                            <ItemStyle Width="15%" HorizontalAlign="Right" />
                                        </Control:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </Control:DataGridView>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                    </asp:View>
                </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
