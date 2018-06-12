<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="ClearUser.aspx.cs" Inherits="ClearUser" %>

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
                    ź��ª��ͼ����ҹ
                </div>
                <div class="spaceDiv"></div>
                <div id="Div1" class="TableSearch">
                    <div class="SearchTable">
                        <div class="SearchHead">
                            <span class="spantxt2 spansize14">ź��ª��ͼ����ҹ</span>
                        </div>
                        <div class="spaceDiv"></div>
                        <div class="SearchTotal">
                            <span class="spantxt">�ӹǹ��辺 : </span><span id="lblSearchTotal" class="spantxt"
                                style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">��¡��</span>
                        </div>
                        <div class="spaceDiv"></div>
                    </div>
                </div>
                <div class="spaceDiv"></div>
                <div class="centerDiv">
                    <asp:Button ID="btSave" CssClass="No" runat="server" Text="       ź��ª���" OnClick="btSave_Click" OnClientClick="return ckDelAll();"
                        ToolTip="ź��ª���" />
                    <input type="button" class="btNo" value="      �͡" title="�͡�������ѡ" onclick="Cancel();" />
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
                            <Control:BoundField DataField="UserName" HeaderText="UserName">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:TemplateField HeaderText="����">
                                <ItemTemplate>
                                        <%#Eval("EmpName") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </Control:TemplateField>
                            <Control:BoundField DataField="DeptName" HeaderText="˹��§ҹ" SortExpression="DeptName">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField DataField="UserRoleName" HeaderText="����������" SortExpression="DeptName">
                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </Control:BoundField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" />
                    </Control:DataGridView>
                    <div class="clear"></div>
                    <div class="spaceDiv"></div>
                </div>
                <div class="clear"></div>
                <div class="spaceDiv"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
