<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Expenses.aspx.cs" Inherits="Expenses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Expenses.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Expenses.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Expenses.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "Expenses.aspx";
        }
        function printRpt(mode, type, id) {
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type);
        }
        function SumTotal() {
            var Subsidies = $get("<%=txtSubsidies.ClientID%>").value.replace(/,/g, '');
            var Revenue = $get("<%=txtRevenue.ClientID%>").value.replace(/,/g, '');
            var Free = $get("<%=txtFree.ClientID%>").value.replace(/,/g, '');
            var Other = $get("<%=txtOther.ClientID%>").value.replace(/,/g, '');
            $get("lblTotalAmount").innerHTML = (parseFloat(Subsidies) + parseFloat(Revenue) + parseFloat(Free) + parseFloat(Other)).comma();
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
                    �Թ��¨��»�ШӢͧʶҹ�֡��
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">�����Թ��¨��»�ШӢͧʶҹ�֡��</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�է�����ҳ : </span>
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
                                        <asp:TextBox CssClass="txtSearch txt300" ID="txtSearch" runat="server"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���"
                                            Text="  " />
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
                                    Text="       ���ҧ��¡������" ToolTip="���ҧ��¡������" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server">
                                <Columns>
                                    <Control:BoundField HeaderText="�է�����ҳ" DataField="StudyYear">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="���ش˹ع" DataField="Subsidies" DataFormatString="{0:n2}">
                                        <ItemStyle Width="17%" HorizontalAlign="Right" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�����ʶҹ�֡��" DataField="Revenue" DataFormatString="{0:n2}">
                                        <ItemStyle Width="17%" HorizontalAlign="Right" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="���¹��� 15 ��" DataField="Free" DataFormatString="{0:n2}">
                                        <ItemStyle Width="17%" HorizontalAlign="Right" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�Թ��� �" DataField="Other" DataFormatString="{0:n2}">
                                        <ItemStyle Width="17%" HorizontalAlign="Right" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�����¨���" DataField="TotalAmount" DataFormatString="{0:n2}">
                                        <ItemStyle Width="17%" HorizontalAlign="Right" ForeColor="Green" Font-Bold="true" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="��§ҹ">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="printRpt(35,'w','<%#Eval("ExpensesCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="��§ҹ�Թ����Ѻ�ͧʶҹ�֡�� Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" onclick="printRpt(35,'p','<%#Eval("ExpensesCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="��§ҹ�Թ����Ѻ�ͧʶҹ�֡�� Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("ExpensesCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("ExpensesCode") %>');">
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
                                    <span class="spantxt">�է�����ҳ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblExpensesCode" runat="server" Text="Label" ForeColor="White" Font-Size="0"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ش˹ع : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtSubsidies" CssClass="txtBoxNum txt150" MaxLength="9" onkeyup="SumTotal();"
                                        onchange="txtComma(this);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0); SumTotal();"
                                        Text="0" onkeypress="return KeyNumber(event);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ʶҹ�֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtRevenue" CssClass="txtBoxNum txt150" MaxLength="9" onkeyup="SumTotal();"
                                        onchange="txtComma(this);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0); SumTotal();"
                                        Text="0" onkeypress="return KeyNumber(event);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Թ���¹��� 15 �� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtFree" CssClass="txtBoxNum txt150" MaxLength="9" onkeyup="SumTotal();"
                                        onchange="txtComma(this);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0); SumTotal();"
                                        Text="0" onkeypress="return KeyNumber(event);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Թ��� � : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtOther" CssClass="txtBoxNum txt150" MaxLength="9" onkeyup="SumTotal();"
                                        onchange="txtComma(this);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0); SumTotal();"
                                        Text="0" onkeypress="return KeyNumber(event);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ʹ�����¨��·����� : </span>
                                </div>
                                <div class="divB_Head">
                                    <span id="lblTotalAmount" class="spantxt2"></span>&nbsp;<span class="spantxt">�ҷ</span>
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
                                            ToolTip="�ѹ�֡��¡�ù��" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡��¡�ù��" onclick="Cancel();" />
                                    </div>
                                </div>
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
