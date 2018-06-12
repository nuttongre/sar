<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" CodeFile="PageDefault.aspx.cs"
    Inherits="PageDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../CSS/slider.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/flexslider.css" rel="stylesheet" type="text/css" />

    <script defer src="../scripts/jquery.flexslider.js" type="text/javascript"></script>
    <script src="../scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="../scripts/FusionCharts.js" type="text/javascript"></script>

    <script type="text/javascript">
        function AlertActivity() {
            dialogBox.show('../PopUp/PopUpAlertActivity.aspx', '��ʶҹСԨ����', '1200', '500', '#000', true, 'yes');
        }
        $(window).load(function () {
            $('.flexslider').flexslider({
                animation: "slide"
            });
        });
        function PopUpUplodeImage() {
            dialogBox.show('../PopUp/PopUpUploadImageSlider.aspx', 'Ṻ����ٻ', '1000', '400', '#000', true, 'yes');
        }

        function getPopUpActivity(id) {
            dialogBox.show('../PopUp/PopUpActivityPresent.aspx?mode=2&id=' + id, '��������´�Ԩ����', '1000', '600', '#000', true);
        }
        function getPopUpActivityDetail(id) {
            dialogBox.show('../PopUp/ActivityDetail.aspx?acid=' + id, '��������´��÷ӡԨ����', '1000', '600', '#000', true);
        }
    </script>

    <%--<meta http-equiv="refresh" content="100;URL=../MasterData/ReportTreeView.aspx?ck=1" />--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="centerDiv" style="text-align: center; background-color: White; width: 99%; float: left; height: auto;">
                <div style="float: left; padding: 10px 0 0 20px;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../Image/megaphone.gif" ToolTip="��С��" Width="33px" />
                </div>
                <div style="float: left; padding: 15px 0 0 15px; background-color: White; width: 90%;">
                    <marquee><span ID="lblComment" runat="server" class="spantxthead" style="color:#41a0a0; font-size:16pt; font-weight:bold;"></span></marquee>
                </div>
                <div style="background-color: White;">
                    <asp:LinkButton ID="lbtUpdateComment" runat="server" OnClick="lbtUpdateComment_OnClick" ForeColor="Gray">��С��</asp:LinkButton>
                </div>
                <div class="spaceDiv"></div>
                <div style="float: left; width: 100%; text-align: center;">
                    <span id="spnAlertRenew" runat="server" style="color: red; font-size: 16pt; font-weight: bold;"></span>
                </div>
                <div class="centerDiv" style="background-color: White;">
                    <asp:TextBox ID="txtComment" CssClass="txtBox" Width="550" MaxLength="500" runat="server" Visible="false"></asp:TextBox>
                    <asp:Button ID="btSave" runat="server" Text="�ѹ�֡" CssClass="btYesCm" Visible="false" OnClick="btSave_OnClick" /><asp:Button ID="btCancel"
                        runat="server" Text="¡��ԡ" CssClass="btNoCm" Visible="false" OnClick="btCancel_OnClick" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="clear"></div>
    <div style="float:left; text-align: center; width: 100%; height:auto; padding: 30px 0 0 0; background-color:#f2f2f2; border-radius:10px;">
            <div class="centerDiv">
                <span style="font-size: 20pt; font-weight: bold; color: #272727;">
                    <%=ConfigurationManager.AppSettings["ProjectName"] + " V." + Star.VersionControl.Versions.ProductVersion%></span><br />
                <br />
                <span style="font-size: 18pt; font-weight: bold; color: #272727;">�Թ�յ�͹�Ѻ</span>
                <div style="padding: 30px 0 0 0;">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <img src="<%#Eval("LogoPath")%>" class="<%#(Eval("LogoPath").ToString()==""?"dpnone":"")%>" /><br />
                            <span style="font-size: 16pt; font-weight: bold; color: Navy;"><%#Eval("SchoolName")%></span><br />
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        <div class="clear"></div>
        <div class="spaceDiv"></div>
        <div class="spaceDiv"></div>
        <%--<div style="text-align: center;">
            <br />
            <img src="../Image/loading.gif" /><br />
        </div>--%>
        <%--<center>
            <div style="width: 50px; text-align: left; color: Black; font-weight: bold;">
                Loading<span id="loading"></span>
            </div>
        </center>--%>.
        <div id="divGraph1" class="divGraph" style="border: solid 5px #f9b806; width: 47%; height: 440px; background: url(../Image/OpacityBG.png) repeat bottom;">
            <div class="spaceDiv"></div>
            <div style="text-align: center; padding: 30px 0 0 0;">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <%--<img src="<%#Eval("LogoPath")%>" class="<%#(Eval("LogoPath").ToString()==""?"dpnone":"")%>" /><br />--%>
                        <span style="font-size: 16pt; font-weight: bold; color: Navy;"><%#Eval("SchoolName")%></span><br />
                        <br />
                        <span class="spantxt2">����·�ȹ�</span><br />
                        <span class="spantxt4"><%#Eval("Vision")%></span><br />
                        <br />
                        <span class="spantxt2">��Ѫ��</span><br />
                        <span class="spantxt4"><%#Eval("Philosophy")%></span><br />
                        <br />
                        <span class="spantxt2">�͡�ѡɳ�</span><br />
                        <span class="spantxt4"><%#Eval("IdentityName")%></span><br />
                        <br />
                        <span class="spantxt2">�ѵ�ѡɳ�</span><br />
                        <span class="spantxt4"><%#Eval("IdentityName2")%></span><br />
                        <br />
                        <span class="spantxt2">��Ծ��� / �Ӣ�ѭ</span><br />
                        <span class="spantxt4"><%#Eval("Slogan")%></span>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="spaceDiv"></div>
            <div class="spaceDiv"></div>
            <div style="height: 15px;">
                <a href="../MasterData/ReportTreeView.aspx?ck=1" style="font-family: Cordia New; font-size: larger; font-weight: bold; color: #272727;">��ԡ ���ʹټš�û����Թ � �Ѩ�غѹ</a>
            </div>
        </div>
        <div id="divGraph2" class="divGraph" style="border: solid 5px #21a9c1; width: 47%; height: 440px; background: url(../Image/OpacityBG.png) repeat bottom;">
            <div class="spaceDiv"></div>
            <div class="divSpntxtHeadGraph">
                <img src="../Image/Icon/IconGraph.png" />
                �ٻ�Ҿ�Ԩ����
            </div>
            <div class="divFrameGraph">
                <div id="Div1" style="width: 99%; overflow: hidden;">
                    <div class="flexslider" style="width: 500px; height: 300px;">
                        <ul class="slides">
                            <asp:Repeater ID="rptImageSlider" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <img src="../AttachFile/<%# genImage(Eval("ItemID"), Eval("Title")) %>" width="500" height="300" />
                                        <p class="flex-caption"><%# genStr(Eval("Title")) %></p>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
            <div id="divSetImg" runat="server" style="float: right; padding-top: 35px; padding-right: 10px; color: white;">
                <a href="javascript:;" onclick="PopUpUplodeImage()">
                    <img src="../image/MenuStlye/S0.png" />
                    ��Ѻ����¹�ٻ</a>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="divGraph3" class="divGraph" style="border: solid 5px #bc202d; width: 97%; height: auto; background: url(../Image/OpacityBG.png) repeat bottom;">
                    <div class="spaceDiv"></div>
                    <div class="spaceDiv"></div>
                    <div class="TableSearch">
                        <div class="SearchTable">
                            <div class="SearchHead">
                                <span class="spantxt2 spansize14">���ҡԨ����</span>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="inputrow" style="height: auto; padding-bottom: 10px;">
                                <div class="SearchtxtF" style="width: 15%;">
                                    <span class="spantxt">ʶҹ� : </span>
                                </div>
                                <div class="SearchF" style="height: auto; width: 83%;">
                                    <asp:RadioButtonList ID="rbtlStatus" runat="server" RepeatColumns="3" AutoPostBack="true"
                                        ForeColor="Navy" OnSelectedIndexChanged="rbtlStatus_OnSelectedIndexChanged">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrow">
                                <div class="SearchtxtF">
                                    <span class="spantxt">˹��§ҹ : </span>
                                </div>
                                <div class="SearchF">
                                    <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged" Width="300px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrow">
                                <div class="SearchtxtF">
                                    <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                </div>
                                <div class="SearchF">
                                    <asp:DropDownList ID="ddlSearchEmp" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSearchEmp_SelectedIndexChanged" Width="300px">
                                    </asp:DropDownList>
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
                    <div class="gridViewDiv">
                        <div style="text-align: center;">
                            <Control:DataGridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
                                Width="100%" ShowFooter="true">
                                <Columns>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ʶҹ�" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="../PopUp/ActivityDetail.aspx?acid=<%#Eval("ActivityCode")%>" target="_blank">
                                                <img style="border: none; cursor: pointer;" title="<%# (Eval("status").ToString()=="0"?"�ʹ��Թ���":(Eval("status").ToString()=="1"?"���ѧ���Թ���":(Eval("status").ToString()=="2"?"��¡�˹����":(Eval("status").ToString()=="3"?"���Թ�����������":"���֧��˹�")))) %>"
                                                    src='../Image/<%# (Eval("status").ToString()=="0"?"00":(Eval("status").ToString()=="1"?"01":(Eval("status").ToString()=="2"?"02":(Eval("status").ToString()=="3"?"03":"04")))) %>.png' /></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�Դ���" SortExpression="ActivityStatus">
                                        <ItemTemplate>
                                            <%# getActivityStatus(Eval("ActivityStatus").ToString()) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���͡Ԩ����">
                                        <ItemTemplate>
                                            <a href="../PopUp/PopUpActivityPresent.aspx?mode=2&id=<%#Eval("ActivityCode")%>"
                                                id="LinkOld" target="_blank">
                                                <%#Eval("ActivityName")%></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="23%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="˹��§ҹ" DataField="DeptName">
                                        <ItemStyle Width="17%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="��������">
                                        <ItemTemplate>
                                            <%# GetTotalAmount1(decimal.Parse(Eval("TotalAmount").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="11%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount1().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�����">
                                        <ItemTemplate>
                                            <%# GetTotalAmount2(decimal.Parse(Eval("TotalAmount2").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="11%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount2().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���������">
                                        <ItemTemplate>
                                            <%# GetTotalAmount3(decimal.Parse(Eval("TotalBalance").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="11%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount3().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�ѹ�����Թ���" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#DateFormat (Eval("SDate"),Eval("EDate"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="18%" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt" BackColor="#cecece"
                                            BorderStyle="Solid" BorderColor="Black" BorderWidth="2" />
                                    </Control:TemplateField>
                                </Columns>
                            </Control:DataGridView>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="spaceDiv"></div>
                </div>
                <div id="divGraph4" class="divGraph" style="border: solid 5px #949494; width: 47%; height: 355px;">
                    <div class="spaceDiv"></div>
                    <div class="divSpntxtHeadGraph">
                        <img src="../Image/Icon/IconGraph.png" />
                        ��ҿ�ӹǹ�Ԩ�������ʶҹе�ҧ�
                    </div>
                    <div class="divFrameGraph">
                        <div id="graphPnl1" runat="server"></div>
                    </div>
                </div>
                <div id="divGraph5" class="divGraph" style="border: solid 5px #47bb98; width: 47%; height: 355px;">
                    <div class="spaceDiv"></div>
                    <div class="divSpntxtHeadGraph">
                        <img src="../Image/Icon/IconGraph.png" />
                        ��ҿ�ӹǹ�Ԩ�������ʶҹе�ҧ�
                    </div>
                    <div class="divFrameGraph">
                        <div id="graphPnl2" runat="server"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
