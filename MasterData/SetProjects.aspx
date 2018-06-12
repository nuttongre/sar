<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="SetProjects.aspx.cs" Inherits="SetProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function Cancel() {
            location.href = "SetProjects.aspx";
        }
        function Cktxt(m) {
            var ck = 0;
            var ddlNewStrategies = $get("<%=ddlNewStrategies.ClientID%>");
            var ErrorNewStrategies = $get("ErrorNewStrategies");
 
            if(<%=GridView1.ClientID%>.GetSelectedItems() == "")
            {
                alert("��س����͡��¡��");
                ck += 1
            }

            ck += ckDdlNull(m, ddlNewStrategies, ErrorNewStrategies);

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
                    �Ѵ�͡�ç��ûշ���ҹ��
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">�ջѨ�غѹ</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrowH">
                                    <div class="SearchtxtF">
                                        <span id="lblYear" runat="server" class="spantxt">�ա���֡�� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:Label ID="txtNewYear" Font-Bold="true" Font-Size="12pt" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="inputrowH">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">���ط�� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlNewStrategies" CssClass="ddlSearch" Width="500" runat="server">
                                        </asp:DropDownList>
                                        <span class="ColorRed">*</span><span id="ErrorNewStrategies" class="ErrorMsg">��س����͡���ط��</span>
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="spaceDiv"></div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div id="Div2" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">�Ѵ�͡�ҡ</span>
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
                                        <span class="spantxt">�Ӥ��� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server" Width="322px"></asp:TextBox><asp:Button
                                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                                            ID="btSearch" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" />
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">���͡��äѴ�͡ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:CheckBox ID="cbProject" runat="server" Text=" <span class='spantxt'>�ç��� </span>" Checked="true" Enabled="false"/><asp:CheckBox ID="cbActivity" runat="server" Text=" <span class='spantxt'>�Ԩ���� </span>" /><asp:CheckBox ID="cbIndicator2" runat="server" AutoPostBack="true" OnCheckedChanged="cbIndicator2_CheckedChanged" Text=" <span class='spantxt'>��Ǫ���Ѵ </span>" />
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
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="centerDiv">
                            <div class="classButton">
                                <div class="classBtSave">
                                    <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �Ѵ�͡" OnClick="btSave_Click"
                                        OnClientClick="return Cktxt(1);" ToolTip="�Ѵ�͡�ç�������ҹ��" />
                                </div>
                                <div class="classBtCancel">
                                    <input type="button" class="btNo" value="      ���Ѵ�͡" title="���Ѵ�͡" onclick="Cancel();" />
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" AutoGenerateCheckList="true" ShowFooter="true">
                                <Columns>
                                    <Control:TemplateField HeaderText="�ӴѺ���">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="���ط��" DataField="StrategiesName">
                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�����ç���" DataField="ProjectsName">
                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="������ҳ">
                                        <ItemTemplate>
                                            <%# GetBudget(decimal.Parse(Eval("TotalAmount").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        <FooterTemplate>
                                            <%# GetTotalBudget().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
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
