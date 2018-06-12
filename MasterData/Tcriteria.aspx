<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Tcriteria.aspx.cs" Inherits="Tcriteria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "Tcriteria.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "Tcriteria.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Tcriteria.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "Tcriteria.aspx";
        }
        function printRpt(mode, type, id) {
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type);
        }
        function Cktxt(m) {
            var ck = 0;
            var ddlScoreGroup = $get("<%=ddlScoreGroup.ClientID%>");
            var ErrorScoreGroup = $get("ErrorScoreGroup");
            var txtDetail = $get("<%=txtDetail.ClientID%>");
            var ErrorDetail = $get("ErrorDetail");
            var txtCriterion = $get("<%=txtCriterion.ClientID%>");
            var ErrorCriterion = $get("ErrorCriterion");
            var txtTranslation = $get("<%=txtTranslation.ClientID%>");
            var ErrorTranslation = $get("ErrorTranslation");
            var txtMin = $get("<%=txtMin.ClientID%>");
            var ErrorMin = $get("ErrorMin");
            var txtMax = $get("<%=txtMax.ClientID%>");
            var ErrorMax = $get("ErrorMax");

            ck += ckTxtNull(m, txtMax, ErrorMax);
            ck += ckTxtNull(m, txtMin, ErrorMin);
            ck += ckTxtNull(m, txtTranslation, ErrorTranslation);
            ck += ckTxtNull(m, txtCriterion, ErrorCriterion);
            ck += ckTxtNull(m, txtDetail, ErrorDetail);
            ck += ckDdlNull(m, ddlScoreGroup, ErrorScoreGroup);

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
                    ���ҧ��ºࡳ���û����Թ�дѺ�س�Ҿ
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">����ࡳ���û����Թ�дѺ�س�Ҿ</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">��Ǵ���˹ѡ��ṹ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
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
                                <asp:Button ID="btAdd" CssClass="btAdd" runat="server" Text="       ���ҧࡳ���û����Թ����"
                                    OnClientClick="AddItem();" ToolTip="���ҧࡳ���û����Թ����" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server">
                                <Columns>
                                    <Control:TemplateField HeaderText="��ǧ��ṹ���ӹǳ��">
                                        <ItemTemplate>
                                            <%# checkedit(Eval("TcriteriaID").ToString(), Eval("Detail").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="35%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="��ṹ�����ش" DataField="TMin" DataFormatString="{0:N2}">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="��ṹ�ҡ�ش" DataField="TMax" DataFormatString="{0:N2}">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="���˹ѡ��ṹ" DataField="Tcriteria">
                                        <ItemStyle Width="10%" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�дѺ�س�Ҿ" DataField="Criterion">
                                        <ItemStyle Width="10%" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�Ťس�Ҿ" DataField="Translation">
                                        <ItemStyle Width="15%" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("TcriteriaID") %>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <%# checkdel(Eval("CreateUser").ToString(), Eval("TcriteriaID").ToString()) %>
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
                                    <span class="spantxt">��Ǵ���˹ѡ��ṹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlScoreGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlScoreGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorScoreGroup"
                                        class="ErrorMsg">��س����͡��Ǵ���˹ѡ��ṹ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���˹ѡ��ṹ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblTcriteria" runat="server" Text="0" ForeColor="Black"></asp:Label>
                                    <span class="spantxt">��ṹ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��ǧ��ṹ���ӹǳ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt250" ID="txtDetail" MaxLength="20" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorDetail" class="ErrorMsg">��سһ�͹��ǧ��ṹ���ӹǳ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�дѺ�س�Ҿ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum txt50" ID="txtCriterion" onkeypress="return KeyNumber(event);"
                                        MaxLength="1" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorCriterion" class="ErrorMsg">��سһ�͹�дѺ�س�Ҿ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Ťس�Ҿ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt250" ID="txtTranslation" MaxLength="20" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorTranslation" class="ErrorMsg">��سһ�͹�Ťس�Ҿ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��ṹ����ش : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum txt50" ID="txtMin" MaxLength="6" onkeypress="return KeyNumber(event);"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorMin" class="ErrorMsg">��سһ�͹��ṹ����ش</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��ṹ�٧�ش : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum txt50" ID="txtMax" MaxLength="6" onkeypress="return KeyNumber(event);"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorMax" class="ErrorMsg">��سһ�͹��ṹ�٧�ش</span>
                                </div>
                            </div>
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
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡"
                                            OnClick="btSave_Click " OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡ࡳ���û����Թ���" />
                                        <asp:Button ID="btSaveAgain" CssClass="btYesToo" runat="server" Text="       �ѹ�֡������ҧࡳ���û����Թ����"
                                            OnClick="btSaveAgain_Click" OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡������ҧࡳ���û����Թ����" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡�ѹ��Ԩ���" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridView2" runat="server">
                                    <Columns>
                                        <Control:BoundField HeaderText="��ǧ��ṹ���ӹǳ��" DataField="Detail">
                                            <ItemStyle Width="35%" HorizontalAlign="Left" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="��ṹ�����ش" DataField="TMin" DataFormatString="{0:N2}">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="��ṹ�ҡ�ش" DataField="TMax" DataFormatString="{0:N2}">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="���˹ѡ��ṹ" DataField="Tcriteria">
                                            <ItemStyle Width="10%" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="�дѺ�س�Ҿ" DataField="Criterion">
                                            <ItemStyle Width="10%" />
                                        </Control:BoundField>
                                        <Control:BoundField HeaderText="�Ťس�Ҿ" DataField="Translation">
                                            <ItemStyle Width="15%" />
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
