<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Indicators2.aspx.cs" Inherits="Indicators2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Indicators2.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Indicators2.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Indicators2.aspx?mode=3&id=" + id;
        }
        function gotoItem(id) {
            location.href = "Evaluation.aspx?mode=1&id=" + id;
        }
        function Cancel() {
            location.href = "Indicators2.aspx";
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
            var txtIndicators2 = $get("<%=txtIndicators2.ClientID%>");
            var ErrorIndicators2 = $get("ErrorIndicators2");
            var txtWeight = $get("<%=txtWeight.ClientID%>");
            var ErrorWeight = $get("ErrorWeight");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckTxtNull(m, txtWeight, ErrorWeight);
            ck += ckTxtNull(m, txtIndicators2, ErrorIndicators2);
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
        function CktxtAll() {
            if ($get("<%=txtAll.ClientID%>").value == "0") {
                $get("ErrorAll").style.display = "block";
                return false;
            }
            else {
                $get("ErrorAll").style.display = "none";
                return true;
            }
        }
        function CkNum() {
            var txtAll = $get("<%=txtAll.ClientID%>");
            var txtWeight = $get("<%=txtWeight.ClientID%>");

            $get("<%=txtAll.ClientID%>").value = (txtAll.value == "") ? "0" : txtAll.value.comma();
            $get("<%=txtWeight.ClientID%>").value = (txtWeight.value == "") ? "0" : txtWeight.value.comma();
        }
        function Calculate(mode) {
            var txtAll = parseFloat($get("<%=txtAll.ClientID%>").value.replace(/,/g, ''));
            var txtWeight = parseFloat($get("<%=txtWeight.ClientID%>").value.replace(/,/g, ''));
            var txtEqual = parseFloat($get("txtEqual").value.replace(/,/g, ''));

            if (txtAll == 0) {
                txtAll = 1;
            }
            if (mode == 0) {
                $get("txtEqual").value = ((txtWeight * 100) / txtAll).toFixed(2);
            }
            else {
                $get("<%=txtWeight.ClientID%>").value = ((txtEqual * txtAll) / 100).comma();
        }
    }
    function ckRate() {
        var txtAll = parseFloat($get("<%=txtAll.ClientID%>").value.replace(/,/g, ''));
            var txtWeight = parseFloat($get("<%=txtWeight.ClientID%>").value.replace(/,/g, ''));

            if (txtWeight > txtAll) {
                var str = "ࡳ�����ҹ  ������Ҷ�ҡ���Ҩӹǹ�������� !!!";
                var ratetype = $get("<%=ddlRateType.ClientID%>").selectedIndex;
                if (ratetype != 0) {
                    str = "��ṹ����ҹ  ������Ҷ�ҡ���Ҥ�ṹ����� !!!";
                }
                alert(str);
                $get("<%=txtWeight.ClientID%>").value = "0";
                Calculate(0);
                return;
            }
        }
        function ckRateType(mode) {
            var ratetype = $get("<%=ddlRateType.ClientID%>").selectedIndex;
            if (ratetype == 0) {
                $get('spnAll').innerHTML = "�ӹǹ������ : ";
                $get("<%=txtAll.ClientID%>").disabled = false;
                $get('spnWeight').innerHTML = "ࡳ�����ҹ :";

                if (mode == 0) {
                    $get("<%=txtAll.ClientID%>").value = "0";
                    $get("<%=txtWeight.ClientID%>").value = "0";
                    $get("txtEqual").value = "50";
                }
            }
            else {
                $get('spnAll').innerHTML = "��ṹ��� / �ô : ";
                $get("<%=txtAll.ClientID%>").value = "5";
                $get("<%=txtAll.ClientID%>").disabled = true;
                $get('spnWeight').innerHTML = "��ṹ / �ô����ҹ :";

                if (mode == 0) {
                    $get("<%=txtWeight.ClientID%>").value = "2";
                    Calculate(0);
                }
            }
            CktxtAll();
        }
        function ckRateDetail() {
            var ratetype = $get("<%=ddlRateType.ClientID%>").selectedIndex;
            if (ratetype != 0) {
                $get("RateDetail").style.display = "block";
            }
            else {
                $get("RateDetail").style.display = "none";
            }
        }
        function getPopUpIndicators() {
            var id = $get("<%=ddlStrategies.ClientID%>").value;
            if (id != '') {
                dialogBox.show('../PopUp/PopUpIndicators.aspx?type=r&id=' + id + '&indname=' + $get('<%=txtIndicators2.ClientID %>').id, '��Ǫ���Ѵ', '800', '500', '#000', true, 'yes');
            }
            else {
                alert("��س����͡���ط���͹���¡�٢�����");
                $get("<%=ddlStrategies.ClientID%>").focus();
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
                    ��˹���Ǫ���Ѵ
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">���ҵ�Ǫ���Ѵ</span>
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
                                <input type="button" class="btAdd" onclick="AddItem();" value="      ���ҧ��Ǫ���Ѵ����" title="���ҧ��Ǫ���Ѵ����" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="�ӴѺ���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��Ǫ���Ѵ">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("Indicators2Code")%>');"><%# Eval("IndicatorsName2") %></a>
                                        </ItemTemplate>
                                        <ItemStyle Width="53%" HorizontalAlign="Left" />
                                    </Control:TemplateField>
                                    <%--<Control:BoundField HeaderText="�����ԹẺ" DataField="RateType">
                                        <ItemStyle Width="8%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="����Ѵ" DataField="WeightType">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </Control:BoundField>--%>
                                    <Control:BoundField HeaderText="�ӹǹ������" DataField="OffAll" DataFormatString="{0:n0}">
                                        <ItemStyle Width="8%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="ࡳ�����ҹ" DataField="Weight" DataFormatString="{0:n0}">
                                        <ItemStyle Width="8%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="��º���" DataField="ckPercen">
                                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("Indicators2Code")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("Indicators2Code") %>');">
                                                <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�ӵ��">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="gotoItem('<%#Eval("Indicators2Code") %>');">
                                                <img style="border: 0; cursor: pointer;" title="������§��Ǻ觪��������Ǫ���Ѵ���" src="../Image/goto.png" /></a>
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
                                    <asp:DropDownList CssClass="ddl" ID="ddlStrategies" Width="600px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlStrategies_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStrategies" class="ErrorMsg">���͡���ط�</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ç��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlProjects" Width="600px" AutoPostBack="true"
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
                                    <asp:DropDownList CssClass="ddl" ID="ddlActivity" Width="600px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlActivity_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorActivity" class="ErrorMsg">���͡�Ԩ����</span>
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
                                    <asp:Label ID="txtTotalAmount" CssClass="spantxt2 spantxt5" runat="server"></asp:Label>&nbsp;<span
                                        class="spantxt2 spantxt3">�ҷ</span>
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
                                    <asp:TextBox CssClass="txtBoxL" ID="txtIndicators2" Width="600px" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span><img src="../image/look.png" title="���¡�ٵ�Ǫ���Ѵ���������" style="cursor: pointer; vertical-align: bottom;"
                                        onclick="getPopUpIndicators();" /><span id="ErrorIndicators2" class="ErrorMsg">��سһ�͹��Ǫ���Ѵ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ԹẺ : </span>
                                </div>
                                <div class="divB_Head">
                                    <div style="float: left">
                                        <asp:DropDownList ID="ddlRateType" CssClass="ddl" runat="server">
                                            <asp:ListItem Text="�ԧ����ҳ" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="�ԧ�س�Ҿ" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <span id="RateDetail" style="display: none; float: left; margin-left: 15px;">��ṹࡳ��  :  1 = ��Ѻ��ا, 2 = ����, 3 = ��, 4 = ���ҡ, 5 = ��������</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="spnAll" class="spantxt">�ӹǹ������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum" ID="txtAll" Width="100px" onkeypress="return KeyNumber(event);" runat="server" Text="0"
                                        onclick="SelecttxtAll(this);" onkeyup="txtZero(this,0);" onblur="txtZero(this,0);"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorAll" class="ErrorMsg">�ӹǹ������������ 0</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="spnWeight" class="spantxt">��ṹ / �ô����ҹ :</span>
                                </div>
                                <div class="divB_Head">
                                    <div style="float: left;">
                                        <span id="lblWeight1" class="spantxt2 spantxt3"></span>
                                        <asp:TextBox CssClass="txtBoxNum" ID="txtWeight" MaxLength="15" Width="100px" onkeypress="return KeyNumber(event);"
                                            runat="server" Text="50" onkeyup="Calculate(0);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0);"></asp:TextBox>
                                    </div>
                                    <div style="float: left; padding: 0 5px;"><span id="spnEqual" class="spantxt2 spantxt3">��º��� : </span></div>
                                    <div id="divEqual" style="float: left;">
                                        <input id="txtEqual" type="text" class="txtBoxNum" maxlength="8" style="width: 50px;" value="50" onkeypress="return KeyNumber(event);" onkeyup="Calculate(1);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0);" />
                                    </div>
                                    <div style="float: left; padding: 0 5px;"><span id="spnEqual2" class="spantxt2 spantxt3"></span>% </div>
                                    <div style="float: left;"><span class="ColorRed">*</span><span style="color: gray; font-size: 16px;"> �ͧࡳ���ҹ��� 50 %</span></div>
                                    <div style="float: left;"><span id="ErrorWeight" class="ErrorMsg">��سһ�͹ࡳ�����ҹ</span></div>
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
                                <asp:Label ID="lblCreate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="centerDiv">
                                <asp:Label ID="lblUpdate" runat="server" CssClass="spantxt4"></asp:Label>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <div class="classButton">
                                    <div class="classBtSave">
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click"
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǫ���Ѵ���" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧ��Ǫ���Ѵ����"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǫ���Ѵ���������ҧ��Ǫ���Ѵ����" />
                                        <asp:Button ID="btSaveAndGo" CssClass="btYesAndGo" runat="server" Text="       �ѹ�֡���������§��Ǻ觪��"
                                            OnClick="btSaveAndGo_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǫ���Ѵ������������§��Ǻ觪��" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡��Ǫ���Ѵ���" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                                    <Columns>
                                        <Control:BoundField HeaderText="��Ǫ���Ѵ" DataField="IndicatorsName2">
                                            <ItemStyle Width="70%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <%--<Control:BoundField HeaderText="����Ѵ" DataField="WeightType">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </Control:BoundField>--%>
                                        <Control:BoundField HeaderText="�ӹǹ������" DataField="OffAll" DataFormatString="{0:n0}">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="ࡳ�����ҹ" DataField="Weight" DataFormatString="{0:n0}">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="��º���" DataField="ckPercen">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
