<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Indicators.aspx.cs" Inherits="Indicators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Indicators.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Indicators.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Indicators.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "Indicators.aspx";
        }
        function printRpt(mode, type) {
            var yearB = document.getElementById("<%=ddlSearchYear.ClientID%>").value;
            var id = document.getElementById("<%=ddlSearch.ClientID%>").value;
            var ck;

            if (document.getElementById("<%=rbtreport1.ClientID%>").checked == true)
            { ck = 1; }
            if (document.getElementById("<%=rbtreport2.ClientID%>").checked == true)
            { ck = 2; }

            if (ck == 1) {
                window.open('../GtReport/Viewer.aspx?rpt=9&yearB=' + yearB + '&id=' + id + "&rpttype=" + type);
            }
            else {
                window.open('../GtReport/Viewer.aspx?rpt=10&yearB=' + yearB + '&id=' + id + "&rpttype=" + type);
            }
        }
        function Cktxt(m) {
            var ck = 0;
            var txtIndicators = $get("<%=txtIndicators.ClientID%>");
            var ErrorIndicators = $get("ErrorIndicators");
            var ddlStandard = $get("<%=ddlStandard.ClientID%>");
            var ErrorStandard = $get("ErrorStandard");
            var ddlScoreGroup = $get("<%=ddlScoreGroup.ClientID%>");
            var ErrorScoreGroup = $get("ErrorScoreGroup");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckDdlNull(m, ddlScoreGroup, ErrorScoreGroup);
            ck += ckTxtNull(m, txtIndicators, ErrorIndicators);
            ck += ckDdlNull(m, ddlStandard, ErrorStandard);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
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
                    <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="TableSearch" class="TableSearch">
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
                                        <span class="spantxt"><asp:Label ID="lblStandardSearch" runat="server"></asp:Label> : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" Width="500" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�Ӥ��� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:TextBox CssClass="txtSearch txt300" ID="txtSearch" runat="server"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" />
                                    </div>
                                </div>
                                <div class="inputrow" style="height: auto;">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�ٻẺ��§ҹ : </span>
                                    </div>
                                    <div class="SearchF" style="height: auto;">
                                        <div style="float: left;">
                                            <asp:RadioButton ID="rbtreport1" runat="server" Text=" ��§ҹ�ҵðҹ����֡�Ң�鹾�鹰ҹ"
                                                GroupName="rpt" Checked="true" />
                                        </div>
                                        <div style="float: left;">
                                            <asp:RadioButton ID="rbtreport2" runat="server" Text=" ��§ҹ��͸Ժ������дѺ�س�Ҿ�ҵðҹ����֡�Ң�鹾�鹰ҹ"
                                                GroupName="rpt" />
                                        </div>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                    </div>
                                    <div class="SearchF">
                                        <asp:Literal ID="linkReport" runat="server"></asp:Literal>
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       ���ҧ��Ǻ觪������" OnClientClick="AddItem();"
                                    ToolTip="���ҧ��Ǻ觪������" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" PageSize="50">
                                <Columns>
                                    <Control:TemplateField HeaderText="��Ǻ觪��">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("IndicatorsCode").ToString(), Eval("IndicatorsName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="���˹ѡ��ṹ" DataField="WeightScore">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("IndicatorsCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("IndicatorsCode") %>');">
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
                                    <span class="spantxt"><asp:Label ID="lblIndicatorNo" runat="server"></asp:Label> : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtSort" CssClass="txtBoxNum txt50" MaxLength="2" runat="server"
                                        onkeypress="return KeyNumber(event);"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                    <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="lblYear" runat="server" class="spantxt">�ա���֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearB_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt"><asp:Label ID="lblStandard" runat="server"></asp:Label> : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlStandard" CssClass="ddl txt500" AutoPostBack="true" OnSelectedIndexChanged="ddlStandard_OnSelectedChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStandard" class="ErrorMsg">��س����͡</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt"><asp:Label ID="lblIndicator" runat="server"></asp:Label> : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtIndicators" CssClass="txtBoxL txt500" MaxLength="300" runat="server"></asp:TextBox>&nbsp;<span
                                        class="ColorRed">*</span> <span id="ErrorIndicators" class="ErrorMsg">��سһ�͹</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��͸Ժ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtDetail" CssClass="txtBoxL txt550" Height="150px"
                                        TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����繡�þѲ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtDevelopIssues" CssClass="txtBoxL txt550" Height="150px"
                                        TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <%--<span class="spantxt">��Ǻ觪���ԧ�س�Ҿ : </span>--%>
                                </div>
                                <div class="divB_Head">
                                    <asp:CheckBox ID="cbQualityFlag" runat="server" Visible="false" Text=" �繵�Ǻ觪���ԧ�س�Ҿ" />
                                </div>
                            </div>
                            <div class="centerDiv" style="margin: 0px 20px;">
                                <table style="border: solid 1px black; width: 96%;">
                                    <tr>
                                        <td class="headBox" style="width: 40%; border: solid 1px black; text-align: center;">
                                            <span class="spantxt2">�Ըա�����Ǻ���������</span>
                                        </td>
                                        <td class="headBox" style="width: 50%; border: solid 1px black; text-align: center;">
                                            <span class="spantxt2">���觢�����</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 40%; text-align: left; padding: 0 0 0 10px; border: solid 1px gray;">
                                            <span class="spantxt">�. ��������ɳ� / ����ͺ��� / ����ѧࡵ�ĵԡ��� : </span>
                                        </td>
                                        <td style="width: 50%; padding: 0 0 0 0; border: solid 1px gray;">
                                            <asp:TextBox ID="txtDataSource1" CssClass="txtBoxL" Width="100%" Height="100px"
                                                TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 40%; text-align: left; padding: 0 0 0 10px; border: solid 1px gray;">
                                            <span class="spantxt">�. ��õ�Ǩ�͡��� ��ѡ�ҹ ��ͧ��� ���͢������ԧ��Шѡ�� : </span>
                                        </td>
                                        <td style="width: 50%; padding: 0 0 0 0; border: solid 1px gray;">
                                            <asp:TextBox ID="txtDataSource2" CssClass="txtBoxL" Width="100%" Height="150px"
                                                TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ǵ���˹ѡ��ṹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlScoreGroup" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlScoreGroup_SelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;<span class="ColorRed">*</span> <span id="ErrorScoreGroup" class="ErrorMsg">��س����͡��Ǵ���˹ѡ��ṹ</span>
                                    <asp:TextBox ID="txtWeightScore" CssClass="txtBoxnone" MaxLength="4" runat="server"
                                        onkeypress="return KeyNumber(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="dgCriteria" runat="server">
                                    <Columns>
                                        <Control:BoundField HeaderText="��ǧ��ṹ���ӹǳ��" DataField="Detail">
                                            <ItemStyle Width="60%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="���˹ѡ��ṹ" DataField="Tcriteria">
                                            <ItemStyle Width="10%" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="�дѺ�س�Ҿ" DataField="Criterion">
                                            <ItemStyle Width="10%" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="�Ťس�Ҿ" DataField="Translation">
                                            <ItemStyle Width="20%" />
                                        </Control:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </Control:DataGridView>
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
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǻ觪����" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧ��Ǻ觪������"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǻ觪����������ҧ��Ǻ觪������" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server" Visible="false" PageSize="20">
                                    <Columns>
                                        <Control:BoundField HeaderText="��Ǻ觪��" DataField="IndicatorsName">
                                            <ItemStyle Width="85%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="���˹ѡ��ṹ" DataField="WeightScore">
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
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
