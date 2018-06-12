<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Strategies.aspx.cs" Inherits="Strategies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Strategies.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Strategies.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Strategies.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "Strategies.aspx";
        }
        function printRpt(mode, type, id) {
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type);
        }
        function Cktxt(m) {
            var ck = 0;
            var txtStrategies = $get("<%=txtStrategies.ClientID%>");
            var ErrorStrategies = $get("ErrorStrategies");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckTxtNull(m, txtStrategies, ErrorStrategies);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function ckAddIndicators(m) {
            var ck = 0;
            var txtIndicators2 = $get("<%=txtIndicators2.ClientID%>");
            var txtSortIndicators = $get("<%=txtSortIndicators.ClientID%>");
            var ErrorIndicators = $get("ErrorIndicators");

            ck += ckTxtNull(m, txtSortIndicators, ErrorIndicators);
            ck += ckTxtNull(m, txtIndicators2, ErrorIndicators);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function editmode(id) {
            var OperationName = $get("spnOperation" + id).innerHTML;
            var SortIndicators = $get("spnSortIndicators" + id).innerHTML;
            $get("<%=txtid.ClientID%>").value = id;
            $get("<%=txtSortIndicators.ClientID%>").value = SortIndicators;
            $get("<%=txtIndicators2.ClientID%>").value = OperationName;
        }

        function ckAddGoals(m) {
            var ck = 0;
            var txtGoals = $get("<%=txtGoals.ClientID%>");
            var txtSortGoals = $get("<%=txtSortGoals.ClientID%>");
            var ErrorGoals = $get("ErrorGoals");

            ck += ckTxtNull(m, txtSortGoals, ErrorGoals);
            ck += ckTxtNull(m, txtGoals, ErrorGoals);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function editmodeGoals(id) {
            var GoalsName = $get("spnGoals" + id).innerHTML;
            var SortGoals = $get("spnSortGoals" + id).innerHTML;
            $get("<%=txtGid.ClientID%>").value = id;
            $get("<%=txtSortGoals.ClientID%>").value = SortGoals;
            $get("<%=txtGoals.ClientID%>").value = GoalsName;
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
                    �Ѵ�ӡ��ط��
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">���ҡ��ط��</span>
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
                                        <span class="spantxt">�Ӥ��� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���"
                                            Text="  " />
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <asp:Label ID="lblCopy" CssClass="spantxt" runat="server" Text="�Ѵ�͡�ҡ�� : "></asp:Label>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlOldYear" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btCopy" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btCopy_Click"
                                            ToolTip="�ѹ�֡��äѴ�͡" />
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" OnClientClick="AddItem();"
                                    Text="       ���ҧ���ط������" ToolTip="���ҧ���ط������" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server">
                                <Columns>
                                    <Control:BoundField HeaderText="�ӴѺ���" DataField="Sort" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���͡��ط��">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("StrategiesCode").ToString(), Eval("StrategiesName").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="������ҳ" DataField="TotalAmount" DataFormatString="{0:n2}">
                                        <ItemStyle Width="15%" HorizontalAlign="Right" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("StrategiesCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("StrategiesCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�ӵ��">
                                        <ItemTemplate>
                                            <a href="Projects.aspx?mode=1&id=<%#Eval("StrategiesCode") %>">
                                                <img style="border: 0; cursor: pointer;" title="���ҧ�ç��������������ط����"
                                                    src="../Image/goto.png" /></a>
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
                        <div id="Div2" class="PageManageDiv">
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
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum" ID="txtSort" MaxLength="2" runat="server" onkeypress="return KeyNumber(event);"
                                        Width="50px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtStrategies" CssClass="txtBoxL txt550" MaxLength="200" runat="server" Width="700"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorStrategies" class="ErrorMsg">��سһ�͹���ط��</span>
                                </div>
                            </div>
                            <div class="spaceDiv"></div>
                            <div id="div3" class="TableSearch" runat="server" style="width:100%;">
                                <div class="SearchTable">
                                    <div class="SearchHead">
                                        <span class="spantxt2 spansize14">��Ǫ���Ѵ (KPIs)</span>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">��Ǫ���Ѵ��� :</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxNum" ID="txtSortIndicators" runat="server" Width="30px" onclick="SelecttxtAll(this);" 
                                                onblur="txtZero(this,0);" Text="1" onkeypress="return KeyNumber(event);" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">��Ǫ���Ѵ (KPIs) :</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtIndicators2" runat="server" Width="500px"></asp:TextBox>
                                            <asp:TextBox CssClass="txtBoxnone" ID="txtid" runat="server" Width="0px"></asp:TextBox>
                                            &nbsp;&nbsp;<asp:Button ID="btaddIndicators" CssClass="btAdd" runat="server" OnClientClick="return ckAddIndicators(1);"
                                                OnClick="btaddIndicators_Click" Text="      ������¡��" ToolTip="������¡������" />
                                            <span id="ErrorIndicators" class="ErrorMsg">��سһ�͹��Ǫ���Ѵ���������</span>
                                        </div>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div style="padding-left: 20px; padding-bottom: 5px;">
                                        <asp:Button ID="btDelIndicators" CssClass="btDelete" runat="server" Visible="false"
                                            OnClick="btDelIndicators_Click" />
                                    </div>
                                    <div class="gridViewDiv">
                                        <Control:DataGridView ID="GridViewIndicators" runat="server" AutoGenerateCheckList="true"
                                            Width="100%" AutoGenerateColumns="False" PageSize="100">
                                            <Columns>
                                                <Control:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                            K<span id="spnSortIndicators<%#Container.DataItemIndex %>"><%#Eval("Sort").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="��Ǫ���Ѵ���������">
                                                    <ItemTemplate>
                                                        <a href="javascript:;" onclick="editmode('<%#Container.DataItemIndex%>');">
                                                            <span id="spnOperation<%#Container.DataItemIndex %>"><%#Eval("IndicatorsName").ToString()%></span></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                        <a href="javascript:;" onclick="editmode('<%#Container.DataItemIndex%>');">
                                                            <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </Control:TemplateField>
                                            </Columns>
                                        </Control:DataGridView>
                                    </div>
                                </div>
                            </div>
                            <div id="divSuan" class="TableSearch" runat="server" style="width:100%;">
                                <div class="SearchTable">
                                    <div class="SearchHead">
                                        <span class="spantxt2 spansize14">��һ��ʧ�� (Goals)</span>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">��һ��ʧ���� :</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxNum" ID="txtSortGoals" runat="server" Width="30px" onclick="SelecttxtAll(this);" 
                                                onblur="txtZero(this,0);" Text="1" onkeypress="return KeyNumber(event);" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="inputrowH">
                                        <div class="divF_Head">
                                            <span class="spantxt">��һ��ʧ�� (Goals) :</span>
                                        </div>
                                        <div class="divB_Head">
                                            <asp:TextBox CssClass="txtBoxL" ID="txtGoals" runat="server" Width="500px"></asp:TextBox>
                                            <asp:TextBox CssClass="txtBoxnone" ID="txtGid" runat="server" Width="0px"></asp:TextBox>
                                            &nbsp;&nbsp;<asp:Button ID="btAddGoals" CssClass="btAdd" runat="server" OnClientClick="return ckAddGoals(1);"
                                                OnClick="btAddGoals_Click" Text="      ������¡��" ToolTip="������¡������" />
                                            <span id="ErrorGoals" class="ErrorMsg">��سһ�͹��һ��ʧ��</span>
                                        </div>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <div style="padding-left: 20px; padding-bottom: 5px;">
                                        <asp:Button ID="btDelGoals" CssClass="btDelete" runat="server" Visible="false"
                                            OnClick="btDelGoals_Click" />
                                    </div>
                                    <div class="gridViewDiv">
                                        <Control:DataGridView ID="gridViewGoals" runat="server" AutoGenerateCheckList="true"
                                            Width="100%" AutoGenerateColumns="False" PageSize="100">
                                            <Columns>
                                                <Control:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                            G<span id="spnSortGoals<%#Container.DataItemIndex %>"><%#Eval("Sort").ToString()%></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="��һ��ʧ�� (Goals)">
                                                    <ItemTemplate>
                                                        <a href="javascript:;" onclick="editmodeGoals('<%#Container.DataItemIndex%>');">
                                                            <span id="spnGoals<%#Container.DataItemIndex %>"><%#Eval("GoalsName").ToString()%></span></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                        <a href="javascript:;" onclick="editmodeGoals('<%#Container.DataItemIndex%>');">
                                                            <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </Control:TemplateField>
                                            </Columns>
                                        </Control:DataGridView>
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
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click"
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡���ط����" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧ���ط������"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡���ط����������ҧ���ط������" />
                                        <asp:Button ID="btSaveAndGo" CssClass="btYesAndGo" runat="server" Text="       �ѹ�֡������ҧ�ç��õ��"
                                            OnClick="btSaveAndGo_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡���ط����������ҧ�ç��õ��" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡���ط����" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server" Visible="false">
                                    <Columns>
                                        <Control:BoundField HeaderText="�ӴѺ���" DataField="Sort" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="���͡��ط��" DataField="StrategiesName" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="80%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="������ҳ" DataField="TotalAmount" DataFormatString="{0:n2}">
                                            <ItemStyle Width="15%" HorizontalAlign="Right" />
                                        </Control:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </Control:DataGridView>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
