<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Evaluation.aspx.cs" Inherits="Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Evaluation.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Evaluation.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Evaluation.aspx?mode=3&id=" + id;
        }
        function delItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Evaluation.aspx?mode=4&id=" + id;
        }
        function Cancel() {
            location.href = "Evaluation.aspx";
        }
        function printRpt(mode, type, id) {
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type);
        }
        function Cktxt(m) {
            var ck = 0;
            var ddlStrategies = $get("<%=ddlStrategies.ClientID%>");
            var ErrorStrategies = $get("ErrorStrategies");
            var ddlProjects = $get("<%=ddlProjects.ClientID%>");
            var ErrorProjects = $get("ErrorProjects");
            var ddlActivity = $get("<%=ddlActivity.ClientID%>");
            var ErrorActivity = $get("ErrorActivity");
            var ddlIndicators2 = $get("<%=ddlIndicators2.ClientID%>");
            var ErrorIndicators2 = $get("ErrorIndicators2");
            var ddlSide = $get("<%=ddlSide.ClientID%>");
            var ErrorSide = $get("ErrorSide");
            var ddlStandard = $get("<%=ddlStandard.ClientID%>");
            var ErrorStandard = $get("ErrorStandard");
            var ddlIndicators = $get("<%=ddlIndicators.ClientID%>");
            var ErrorIndicators = $get("ErrorIndicators");

            ck += ckDdlNull(m, ddlIndicators, ErrorIndicators);
            ck += ckDdlNull(m, ddlStandard, ErrorStandard);
            ck += ckDdlNull(m, ddlSide, ErrorSide);
            ck += ckDdlNull(m, ddlIndicators2, ErrorIndicators2);
            ck += ckDdlNull(m, ddlActivity, ErrorActivity);
            ck += ckDdlNull(m, ddlProjects, ErrorProjects);
            ck += ckDdlNull(m, ddlStrategies, ErrorStrategies);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }

    function CkNum()
    {
        var txtAll=$get("<%=txtAll.ClientID%>");
       
        $get("<%=txtAll.ClientID%>").value=(txtAll.value=="")?"0":txtAll.value.comma();
    }
    function CktxtOld()
    {
      if($get("<%=ddlStandard.ClientID%>").selectedIndex == 0 || $get("<%=ddlIndicators.ClientID%>").selectedIndex == 0)
      {
          $get("LinkOld").style.display = "none";
      }
      else
      {
        $get("LinkOld").style.display = "block";
      }
    }
    function EditActivity()
    {
        var id = $get('<%=ddlActivity.ClientID %>').value;
        dialogBox.show('../PopUp/PopUpActivity.aspx?mode=2&id=' + id,'��䢡Ԩ����','1100','600','#000',true, true);
    }
        function PopUpOldProj()
    {
            var st = $get('<%=ddlStandard.ClientID%>').selectedIndex;
            var idt = $get('<%=ddlIndicators.ClientID%>').selectedIndex;
            var YearB = $get('<%=ddlYearB.ClientID%>').value;
            dialogBox.show('../PopUp/PopUpProjOld.aspx' + '?st=' + st + '&idt=' + idt + '&YearB=' + YearB, '�٢����Żշ���ҹ��', '900', '550', '#000', true,true);
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
                    <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">����<asp:Label ID="lblTitle2" runat="server"></asp:Label></span>
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
                                        <span class="spantxt">�Ԩ���� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch1" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch1_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">��Ǫ���Ѵ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch3" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch3_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt"><asp:Label ID="lblSideSearch" runat="server"></asp:Label> : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchSide" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchSide_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt"><asp:Label ID="lblStandardSearch" runat="server"></asp:Label> : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchStandard" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchStandard_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt"><asp:Label ID="lblIndicatorSearch" runat="server"></asp:Label> : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchIndicators" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchIndicators_SelectedIndexChanged" Width="500px">
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
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="btAddDiv">
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       ������§��Ǻ觪������" OnClientClick="AddItem();"
                                    ToolTip="������§��Ǻ觪������" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div style="float:left;padding:5px 20px;">
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><img src="../Image/bt_Del.png" title="ź��¡�÷�����͡" style="border:0;" /></asp:LinkButton>
                        </div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" AutoGenerateCheckList="true" OnRowDataBound="GridView1_RowDataBound" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="�ӴѺ���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ʶҹ�" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img title="<%# (Eval("status").ToString()=="0"?"�ʹ��Թ���":(Eval("status").ToString()=="1"?"���ѧ���Թ���":(Eval("status").ToString()=="2"?"��¡�˹����":(Eval("status").ToString()=="3"?"���Թ�����������":"��ش�ʹ��Թ���")))) %>"
                                                src='../Image/<%# (Eval("status").ToString()=="0"?"00":(Eval("status").ToString()=="1"?"01":(Eval("status").ToString()=="2"?"02":(Eval("status").ToString()=="3"?"03":"04")))) %>.png' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="��Ǫ���Ѵ" DataField="IndicatorsName2">
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="��ҹ">
                                        <ItemTemplate>
                                            <%#strEvaluation (Eval("Indicators2Code").ToString(), Eval("SideName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�ҵðҹ">
                                        <ItemTemplate>
                                            <%#strEvaluation (Eval("Indicators2Code").ToString(), Eval("StandardName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��Ǻ觪��">
                                        <ItemTemplate>
                                            <%#strEvaluation(Eval("Indicators2Code").ToString(), Eval("IndicatorsName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                       <Control:TemplateField HeaderText="��Ǵࡳ��">
                                        <ItemTemplate>
                                            <%#strQA("0", Eval("EvaluationCode").ToString(), Eval("ProjectsCode").ToString() , Eval("ActivityCode").ToString(), Eval("Indicators2Code").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                       <Control:TemplateField HeaderText="��Ǣ��ࡳ��">
                                        <ItemTemplate>
                                           <%#strQA("1", Eval("EvaluationCode").ToString(), Eval("ProjectsCode").ToString() , Eval("ActivityCode").ToString(), Eval("Indicators2Code").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                <%--    <Control:TemplateField HeaderText="��ǧ�ѹ���">
                                        <ItemTemplate>
                                            <%#DateFormat (Eval("SDate"),Eval("EDate"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                    </Control:TemplateField>--%>
                                    <Control:BoundField HeaderText="����Ѻ�Դ�ͺ" DataField="DeptName">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("EvaluationCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" />
                            </Control:DataGridView>
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
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearB_OnSelectedChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlStrategies" Width="650px" OnSelectedIndexChanged="ddlStrategies_OnSelectedChanged"
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStrategies" class="ErrorMsg">���͡���ط��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ç��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlProjects" Width="650px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlProjects_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorProjects" class="ErrorMsg">���͡�ç���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlActivity" Width="650px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlActivity_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorActivity" class="ErrorMsg">���͡�Ԩ����</span>
                                    <a href="javascript:;" id="A1" runat="server" visible="false" onclick="EditActivity();">��䢡Ԩ����</a>
                                    <asp:Button ID="btAc" runat="server" OnClick="btAc_OnClick" CssClass="btNone" />
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��������´�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtActivityDetail" CssClass="spantxt4" runat="server" Width="600px"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblType" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Ҥ���¹��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblTerm" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�է�����ҳ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblBudgetYear" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������ҳ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtTotalAmount" CssClass="spantxt2 spantxt5" runat="server"></asp:Label>&nbsp;<span class="spantxt2 spantxt3">�ҷ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">˹��§ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblDepartment" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblEmpName" runat="server" CssClass="spantxt2 spantxt4"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ���������Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtSDay" CssClass="spantxt2 spantxt4" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ�������ش�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtEDay" CssClass="spantxt2 spantxt4" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ǫ���Ѵ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlIndicators2" Width="650px" OnSelectedIndexChanged="ddlIndicators2_OnSelectedChanged"
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorIndicators2" class="ErrorMsg">���͡��Ǫ���Ѵ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ࡳ�����ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtWeight" CssClass="spantxt6 spantxt2" runat="server"></asp:Label>&nbsp;
                                    <span id="lblWeightType" class="spantxt" runat="server">%</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ҡ�ӹǹ������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtAll" CssClass="spantxt2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt"><asp:Label ID="lblSide" runat="server"></asp:Label> : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlSide" Font-Size="Medium" Width="650px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSide_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorSide" class="ErrorMsg">��س����͡</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt"><asp:Label ID="lblStandard" runat="server"></asp:Label> : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlStandard" Font-Size="Medium" Width="650px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlStandard_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStandard" class="ErrorMsg">��س����͡</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt"><asp:Label ID="lblIndicator" runat="server"></asp:Label> : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlIndicators" Font-Size="Medium" Width="650px"
                                        runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorIndicators" class="ErrorMsg">��س����͡</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�٢����Żշռ�ҹ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <a href="javascript:;" id="LinkOld" onclick="PopUpOldProj();">�٢����Żշ������</a>
                                </div>
                            </div>
                            <div id="divQA" runat="server" visible="false">
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��Ǵࡳ���� : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList CssClass="ddl" ID="ddlQACategory" Font-Size="Medium" Width="650px"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlQACategory_SelectedIndexChanged" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="divF_Head">
                                        <span class="spantxt">��Ǣ��ࡳ���� : </span>
                                    </div>
                                    <div class="divB_Head">
                                        <asp:DropDownList CssClass="ddl" ID="ddlQACategorySub" Font-Size="Medium" Width="650px" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <asp:Label ID="lblCreate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="centerDiv">
                                <asp:Label ID="lblUpdate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <div class="classButton">
                                    <div class="classBtSave">
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click " 
                                    OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡���������§���" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" Text="       �ѹ�֡���������§��Ǻ觪������" runat="server" OnClick="btSaveAgain_Click"
                                    OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡���������§������������§��Ǻ觪������" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡���������§���" onclick="Cancel();" />
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
                                        <Control:BoundField HeaderText="��ҹ" DataField="SideName">
                                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="�ҵðҹ" DataField="StandardName">
                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="��Ǻ觪��" DataField="IndicatorsName">
                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                       <Control:TemplateField HeaderText="��Ǵࡳ��">
                                        <ItemTemplate>
                                            <%#strQA("0",Eval("EvaluationCode").ToString(), Eval("ProjectsCode").ToString() , Eval("ActivityCode").ToString(), Eval("Indicators2Code").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                       <Control:TemplateField HeaderText="��Ǣ��ࡳ��">
                                        <ItemTemplate>
                                           <%#strQA("1",Eval("EvaluationCode").ToString(), Eval("ProjectsCode").ToString() , Eval("ActivityCode").ToString(), Eval("Indicators2Code").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </Control:DataGridView>
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
