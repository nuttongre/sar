<%@ Page Language="C#" MasterPageFile="~/Master/MasterPopUp.master" AutoEventWireup="true"
    CodeFile="PopUpActivityPresent.aspx.cs" Inherits="PopUpActivityPresent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" class="PageManageDiv">
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt spantxt2 spansize14">�ç��� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblProjects" CssClass="spantxt3 spantxt2 spansize14" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�Ԩ���� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="txtActivity" CssClass="spantxt6 spantxt2" runat="server"></asp:Label>
                        <asp:Label ID="txtActivityCode" Font-Size="0" ForeColor="White" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�ѹ��Ԩ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblMission" CssClass="spantxt4" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��һ��ʧ�� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblGoals" CssClass="spantxt4" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">���ط�� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblStrategies" CssClass="spantxt6" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��.����֡�Ҫҵ� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblStandardNationName" CssClass="spantxt3" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��.�������з�ǧ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblStandardMinistryName" CssClass="spantxt3" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�ҵðҹ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblStandard" CssClass="spantxt3" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��Ǻ觪�� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblIndicators" CssClass="spantxt3" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��ѡ�������˵ؼ� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="lblProjectsDetail" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�ѵ�ػ��ʧ�� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="lblPurpose" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">������� : </span>
                    </div>
                    <div class="divB_Head">                        
                    </div>
                </div>
                <div class="inputrow">
                    <div class="divF_Head">
                        <span class="spantxt">�ԧ����ҳ </span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="lblTarget" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�ԧ�س�Ҿ</span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="lblTarget2" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��������´�Ԩ���� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="txtActivityDetail" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��õԴ��������Թ�� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="txtEvaluation" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>                        
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��Ǻ觪����������</span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="txtEvaIndicators" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�Ըա�û����Թ</span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="txtEvaAssessment" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">����ͧ��ͷ����</span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="txtEvaTool" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�ŷ��Ҵ��Ҩ����Ѻ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:TextBox ID="txtExpected" TextMode="MultiLine" ReadOnly="true" Width="660px" Height="100" CssClass="txtBoxNotType spantxt4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�Ҥ���¹��� : </span>
                    </div>
                    <div class="divB_Head">
                        <div style="float: left;">
                            <asp:Label ID="lblTerm" CssClass="spantxt3" runat="server"></asp:Label>
                        </div>
                        <div style="float: left; padding:0px 10px;"><span class="spantxt">�ա���֡�� : </span></div>
                        <div style="float: left;">
                            <asp:Label ID="lblYearB" CssClass="spantxt3" runat="server"></asp:Label>
                        </div>
                        <div style="float: left; padding:0px 10px;"><span class="spantxt">�է�����ҳ : </span></div>
                        <div style="float: left;">
                            <asp:Label ID="lblBudgetYear" CssClass="spantxt3" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">�ѹ���������Ԩ���� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="txtSDay" CssClass="spantxt4" runat="server"></asp:Label>
                        <span class="spantxt">�ѹ�������ش�Ԩ���� : </span>
                        <asp:Label ID="txtEDay" CssClass="spantxt4" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">ʶҹ�����Թ��� : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblPlace" runat="server" CssClass="spantxt3"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblEmpName" runat="server" CssClass="spantxt6"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">˹��§ҹ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblDepartment" CssClass="spantxt6" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">������ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="lblType" CssClass="spantxt3" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">������ҳ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Label ID="txtTotalAmount" CssClass="spantxt5" runat="server"></asp:Label>&nbsp;<span
                            class="spantxt3">�ҷ</span>
                    </div>
                </div>
                <div class="inputrowH">
                    <div class="divF_Head">
                        <span class="spantxt">��Ǫ���Ѵ : </span>
                    </div>
                    <div class="divB_Head">
                        <asp:Repeater ID="rptIncicators2" runat="server">
                            <ItemTemplate>
                                <div style="float: left; width: 100%;">
                                    <span class="spantxt3"><%#Container.ItemIndex + 1 %>.<%#Eval("IndicatorsName2")%></span>
                                </div>
                                <div class="inputrowH">
                                    <div style="float: left;">
                                        <span class="spantxt4" style="padding: 0 0 0 20px;">����Ѵ :<%#Eval("txtWeightType")%></span>
                                    </div>
                                    <div style="float: left; padding: 0 0 0 20px;">
                                        <span class="spantxt">:- </span>
                                    </div>
                                    <div style="float: left;padding:0px 10px;">
                                        <span class="spantxt4">�ҡ������ : <%#Convert.ToDecimal(Eval("OffAll")).ToString("#,##0.00")%></span>
                                    </div>
                                    <div style="float: left;padding:0px 10px;">
                                        <span class="spantxt4">ࡳ�����ҹ : <%#Convert.ToDecimal(Eval("Weight")).ToString("#,##0.00")%>&nbsp;<%#(Eval("txtWeightType").ToString() == "%") ? "%" : ""%>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="spaceDiv"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
