<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="ClearAll.aspx.cs" Inherits="ClearAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ckDelAll() {
            if (confirm('��ͧ���ź��¡�÷����� ��������� ����͹ �繡��ź�����Ũ�ԧ���������������ö�ӡ�Ѻ�����ա !!!')) {
                return true;
            }
            else {
                return false;
            }
        }
        function Cancel() {
            location.href = "SarManage.aspx";
        }
    </script>
    <style type="text/css">
        .No {
            background: url(../Image/bt_No.gif) no-repeat left center;
            border: 0;
            height: 27px;
            cursor: pointer;
            font-size: large;
            color: #424242;
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
                    ź�����ŷ�����
                </div>
                <div class="spaceDiv"></div>
                <div class="centerDiv">
                    <span class="spantxt">�ա���֡�� : </span>
                    <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlYearB_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="centerDiv">
                    <asp:Button ID="btSave" CssClass="No" runat="server" Text="       ź�����ŷ�����" OnClick="btSave_Click" OnClientClick="return ckDelAll();"
                        ToolTip="ź������" />
                    <input type="button" class="btNo" value="      �͡" title="�͡�������ѡ" onclick="Cancel();" />
                </div>
                <div class="gridViewDiv">
                    <Control:DataGridView ID="GridView1" runat="server">
                        <Columns>
                            <Control:TemplateField HeaderText="�ӴѺ���">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="�ӹǹ�ç���">
                                <ItemTemplate>
                                    <%#Eval("CountProject")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="30%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="�ӹǹ�Ԩ����">
                                <ItemTemplate>
                                    <%#Eval("CountActivity")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="30%" />
                            </Control:TemplateField>
                            <Control:TemplateField HeaderText="�ӹǹ��Ǫ���Ѵ">
                                <ItemTemplate>
                                    <%#Eval("CountIndicators")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="30%" />
                            </Control:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" />
                    </Control:DataGridView>
                    <div class="clear"></div>
                    <div class="spaceDiv"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
