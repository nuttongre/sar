<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="ClearIndicatorsError.aspx.cs" Inherits="ClearIndicatorsError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Literal ID="script" runat="server" />
    <script type="text/javascript">
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "ClearIndicatorsError.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "SarManage.aspx";
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
                    ź��Ǫ���Ѵ����ҧ
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
                                        <span class="spantxt">��͹���ʵ�Ǫ���Ѵ : </span>
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
                                <div class="centerDiv">
                                    <input type="button" class="btNo" value="      �͡" title="�͡�������ѡ" onclick="Cancel();" />
                                </div>
                                <div class="spaceDiv"></div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server">
                                <Columns>
                                    <Control:TemplateField HeaderText="�ӴѺ���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="����">
                                        <ItemTemplate>
                                            <%#Eval("EvaluationCode") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���͵�Ǫ���Ѵ">
                                        <ItemTemplate>
                                            <%#Eval("IndicatorsName2") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                    </Control:TemplateField>
                                     <Control:TemplateField HeaderText="���͵�Ǻ觪��">
                                        <ItemTemplate>
                                            <%#Eval("IndicatorsName") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" />
                                    </Control:TemplateField>
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
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
