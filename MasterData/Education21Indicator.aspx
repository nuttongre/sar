<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Education21Indicator.aspx.cs" Inherits="Education21Indicator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Education21Indicator.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Education21Indicator.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Education21Indicator.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "Education21Indicator.aspx";
        }
        function printRpt(mode, type) {
            var yearb = document.getElementById("<%=ddlSearchYear.ClientID%>").value;
            window.open("../GtReport/Viewer.aspx?rpt=25&yearB=" + yearb + "&rpttype=" + type);
        }
        function Cktxt(m) {
            var ck = 0;
            var txtEducation21Indicator = $get("<%=txtEducation21Indicator.ClientID%>");
            var ErrorEducation21Indicator = $get("ErrorEducation21Indicator");
            var ddlEducation21Standard = $get("<%=ddlEducation21Standard.ClientID%>");
            var ErrorEducation21Standard = $get("ErrorEducation21Standard");
            var ddlEducation21Side = $get("<%=ddlEducation21Side.ClientID%>");
            var ErrorEducation21Side = $get("ErrorEducation21Side");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckTxtNull(m, txtEducation21Indicator, ErrorEducation21Indicator);
            ck += ckDdlNull(m, ddlEducation21Standard, ErrorEducation21Standard);
            ck += ckDdlNull(m, ddlEducation21Side, ErrorEducation21Side);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function editmode(id) {
            var hdfCriterionID = $get("hdfCriterionID" + id).value;
            var spnCSort = $get("spnCSort" + id).innerHTML;
            var spnCDetail = $get("spnCDetail" + id).innerHTML;
            var spnCScore = $get("spnCScore" + id).innerHTML;

            $get("<%=txtid.ClientID%>").value = id;
            $get("<%=hdfEducation21CriterionID.ClientID%>").value = Trim(hdfCriterionID);
            $get("<%=ddlCriterionSort.ClientID%>").value = Trim(spnCSort);
            $get("<%=txtcDetail.ClientID%>").value = Trim(spnCDetail);
            $get("<%=txtcScore.ClientID%>").value = Trim(spnCScore);
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
                    ��Ǫ���Ѵ�س�Ҿ��þѲ�� / ��Ǫ���Ѵ�س�Ҿ��������
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">���ҵ�Ǫ���Ѵ�س�Ҿ��þѲ�� / ��Ǫ���Ѵ�س�Ҿ��������</span>
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
                                        <span id="Span1" runat="server" class="spantxt">��������û����Թ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchEvaluateType" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchEvaluateType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">��ҹ��þѲ�ҡ�èѴ����֡�� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="330">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�ҵðҹ�س�Ҿ��þѲ�� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchStandard" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchStandard_SelectedIndexChanged" Width="330">
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
                                <%--<div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�������§ҹ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:Literal ID="linkReport" runat="server"></asp:Literal>
                                    </div>
                                </div>--%>
                                <div class="spaceDiv"></div>
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       ���ҧ��Ǫ���Ѵ�س�Ҿ��þѲ������" OnClientClick="AddItem();"
                                    ToolTip="���ҧ��Ǫ���Ѵ�س�Ҿ��þѲ������" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" PageSize="30">
                                <Columns>
                                    <Control:BoundField HeaderText="��Ǫ���Ѵ�س�Ҿ��þѲ�ҷ��" DataField="Sort1">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="��Ǫ���Ѵ�س�Ҿ��þѲ��">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("Education21IndicatorID").ToString(), Eval("Education21IndicatorName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="85%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("Education21IndicatorID") %>');">
                                                <img alt="" style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("Education21IndicatorID") %>');">
                                                <img alt="" style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
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
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearB" Width="70px" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearB_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ǫ���Ѵ�س�Ҿ��þѲ�ҷ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtSort" CssClass="txtBoxNum txt50" Width="70px" MaxLength="2" runat="server" onkeypress="return KeyNumber(event);"></asp:TextBox>&nbsp;<span
                                        class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">��سһ�͹��Ǫ���Ѵ�س�Ҿ��þѲ�ҷ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="Span2" runat="server" class="spantxt">��������û����Թ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlEvaluateType" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlEvaluateType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��ҹ��þѲ�ҡ�èѴ����֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlEducation21Side" CssClass="ddl txt300" runat="server" OnSelectedIndexChanged="ddlEducation21Side_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorEducation21Side" class="ErrorMsg">���͡��ҹ��þѲ�ҡ�èѴ����֡��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ҵðҹ�س�Ҿ��þѲ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlEducation21Standard" CssClass="ddl txt300" runat="server" OnSelectedIndexChanged="ddlEducation21Standard_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorEducation21Standard" class="ErrorMsg">���͡�ҵðҹ�س�Ҿ��þѲ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ǫ���Ѵ�س�Ҿ��þѲ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtEducation21Indicator" CssClass="txtBoxL txt400" MaxLength="200" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>&nbsp;<span
                                        class="ColorRed">*</span> <span id="ErrorEducation21Indicator" class="ErrorMsg">��سһ�͹��Ǫ���Ѵ�س�Ҿ��þѲ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��������´ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtDetail" CssClass="txtBoxL txt400" runat="server"
                                        TextMode="MultiLine" Height="200"></asp:TextBox>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                            <div id="Div2" class="TableSearch">
                                <div class="SearchTable" style="width: 90%; height: auto;">
                                    <div class="SearchHead HeadCenter">
                                        <span class="spantxt2">�дѺࡳ��س�Ҿ</span>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">�дѺ�س�Ҿ��� : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:DropDownList ID="ddlCriterionSort" CssClass="ddl" Width="50" runat="server"></asp:DropDownList>
                                            <span class="ColorRed">*</span> 
                                            <asp:TextBox CssClass="txtBoxnone" ID="txtid" runat="server" Width="0px"></asp:TextBox>
                                            <asp:HiddenField ID="hdfEducation21CriterionID" runat="server" />
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">��͸Ժ�� : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox ID="txtcDetail" CssClass="txtBoxL" Width="450" TextMode="MultiLine" Height="50" runat="server"></asp:TextBox>
                                            <span class="ColorRed">*</span>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">��ṹ : </span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox ID="txtcScore" CssClass="txtBoxNum" Width="50" MaxLength="3" onkeypress="return KeyNumber(event);" runat="server"></asp:TextBox>
                                            <span class="ColorRed">*</span> &nbsp;&nbsp;<asp:Button ID="btaddCriterion" CssClass="btAdd" runat="server" OnClientClick="return ckAddIndicators();"
                                        OnClick="btaddCriterion_Click" Text="      ������¡��" ToolTip="������¡������" />
                                            <span id="ErrorCriterion" class="ErrorMsg">��سһ�͹��¡�����ú</span>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                        </div>
                                        <div class="divB_Head">
                                            <asp:Label ID="lblckCritetion" runat="server" Text="��¡�ë��" ForeColor="Red"
                                                Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="spaceDiv"></div>
                                    <div style="padding: 0px 10px;">
                                        <asp:Button ID="btDelCriterion" CssClass="btDelete" runat="server" Visible="false" OnClick="btDelCriterion_Click" />
                                    </div>
                                    <div class="gridViewDiv">
                                        <Control:DataGridView ID="GridViewCriterion" runat="server" AutoGenerateCheckList="true"
                                            Width="100%" AutoGenerateColumns="False" PageSize="20">
                                            <Columns>
                                                <Control:TemplateField HeaderText="�дѺࡳ����">
                                                    <ItemTemplate>
                                                        <span id="spnCSort<%#Container.DataItemIndex %>" onclick="editmode('<%#Container.DataItemIndex %>');" style="cursor:pointer;">
                                                        <%# Eval("Sort").ToString() %></span>
                                                        <input id="hdfCriterionID<%#Container.DataItemIndex %>" type="hidden" value="<%# Eval("Education21CriterionID") %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="��͸Ժ��">
                                                    <ItemTemplate>
                                                        <span id="spnCDetail<%#Container.DataItemIndex %>" onclick="editmode('<%#Container.DataItemIndex %>');" style="cursor:pointer;">
                                                            <%# Eval("Detail").ToString() %></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="��ṹ">
                                                    <ItemTemplate>
                                                        <span id="spnCScore<%#Container.DataItemIndex %>">
                                                        <%# Eval("Score").ToString() %></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                </Control:TemplateField>
                                            </Columns>
                                        </Control:DataGridView>
                                    </div>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
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
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǫ���Ѵ�س�Ҿ��þѲ�ҹ��" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧ��Ǫ���Ѵ�س�Ҿ��þѲ������"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��Ǫ���Ѵ�س�Ҿ��þѲ�ҹ��������ҧ��Ǫ���Ѵ�س�Ҿ��þѲ������" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                                    <Columns>
                                        <Control:BoundField HeaderText="��Ǫ���Ѵ�س�Ҿ��þѲ�ҷ��" DataField="Sort1">
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="��Ǫ���Ѵ�س�Ҿ��þѲ��" DataField="Education21IndicatorName">
                                            <ItemStyle Width="95%" HorizontalAlign="Left" />
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
