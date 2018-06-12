<%@ Page Language="C#" MasterPageFile="~/Master/MasterNotLogin.master" AutoEventWireup="true"
    CodeFile="LogSchoolSar.aspx.cs" Inherits="LogSchoolSar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "LogSchoolSar.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "LogSchoolSar.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "LogSchoolSar.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "LogSchoolSar.aspx";
        }
        function printRpt(mode, type, id) {
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type);
        }
        function exit() {
            location.href = "SarManage.aspx";
        }
        function Cktxt(m) {
            var ck = 0;
            var txtVersion = $get("<%=txtVersion.ClientID%>");
            var ErrorVersion = $get("ErrorVersion");
            var txtVirtualName = $get("<%=txtVirtualName.ClientID%>");
            var ErrorVirtualName = $get("ErrorVirtualName");
            var txtSchoolName = $get("<%=txtSchoolName.ClientID%>");
            var ErrorSchoolName = $get("ErrorSchoolName");
            var txtIPaddress = $get("<%=txtIPaddress.ClientID%>");
            var ErrorIPaddress = $get("ErrorIPaddress");

            ck += ckTxtNull(m, txtIPaddress, ErrorIPaddress);
            ck += ckTxtNull(m, txtSchoolName, ErrorSchoolName);
            ck += ckTxtNull(m, txtVirtualName, ErrorVirtualName);
            ck += ckTxtNull(m, txtVersion, ErrorVersion);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function getPopUpBackUp(IpAddress, VirtualName, id) {
            if (IpAddress != "" && VirtualName != "") {
                if (confirm('��ͧ������ͧ�������ç���¹��� ���������')) {
                    var strUrl = 'http://' + IpAddress + '/' + VirtualName + '/Setting/BackUpAuto.aspx?id=' + id + '&txtid=' + document.getElementById('<%=txtID.ClientID %>').id;

                    //alert(strUrl);
                    window.open(strUrl, "���ͧ������", " scrollbars=1 resizable=1 status=1", true);
                    //dialogBox.show("../Setting/BackUpAuto.aspx", '���ͧ������', '1000', '600', '#000', true);
                    //dialogBox.show('http://' + IpAddress + '/' + VirtualName + '/Setting/BackUpAuto.aspx', '���ͧ������', '1000', '600', '#000', true);
                }
            }
            else {
                alert("�������ö���ͧ��������!!  \n �ô��Ǩ�ͺ IPaddress ���� VirtualName");
            }
        }
        function getPopUpRenew(IpAddress, VirtualName, id, ExpDate, ckExpDate) {
            if (IpAddress != "" && VirtualName != "") {
                if (confirm('��ͧ����Դ / �Դ Website �ͧ�ç���¹��� ���������')) {
                    var strUrl = 'http://' + IpAddress + '/' + VirtualName + '/Setting/RenewContact.aspx?id=' + id + '&txtid=' + document.getElementById('<%=txtID.ClientID %>').id + '&expdate=' + ExpDate + '&ckExpDate=' + ckExpDate;

                    //alert(strUrl);
                    window.open(strUrl, "����ѭ����� Server", " scrollbars=1 resizable=1 status=1", true);
                    //dialogBox.show("../Setting/BackUpAuto.aspx", '���ͧ������', '1000', '600', '#000', true);
                    //dialogBox.show('http://' + IpAddress + '/' + VirtualName + '/Setting/BackUpAuto.aspx', '���ͧ������', '1000', '600', '#000', true);
                }
            }
            else {
                alert("�������ö���ͧ��������!!  \n �ô��Ǩ�ͺ IPaddress ���� VirtualName");
            }
        }
        function UpdatebackUp(id) {
            location.href = "LogSchoolSar.aspx?mode=9&id=" + id;
        }
        function ckddlDate(mode) {
            if (mode == 1) {
                var d2 = document.getElementById("<%=ddlSDay.ClientID%>").value;
                var m2 = document.getElementById("<%=ddlSMonth.ClientID%>").value;
                var y2 = document.getElementById("<%=ddlSYear.ClientID%>").value;
                ckDatetimeDDL(d2, m2, y2, document.getElementById("<%=ddlSDay.ClientID%>"));

                var d = document.getElementById("<%=ddlSDay.ClientID%>").value;
                var m = document.getElementById("<%=ddlSMonth.ClientID%>").value;
                var y = document.getElementById("<%=ddlSYear.ClientID%>").value;
                document.getElementById("<%=txtSDay.ClientID%>").value = d + '/' + m + '/' + y;
            }
            if (mode == 2) {
                var d2 = document.getElementById("<%=ddlEDay.ClientID%>").value;
                var m2 = document.getElementById("<%=ddlEMonth.ClientID%>").value;
                var y2 = document.getElementById("<%=ddlEYear.ClientID%>").value;
                ckDatetimeDDL(d2, m2, y2, document.getElementById("<%=ddlEDay.ClientID%>"));

                var d = document.getElementById("<%=ddlEDay.ClientID%>").value;
                var m = document.getElementById("<%=ddlEMonth.ClientID%>").value;
                var y = document.getElementById("<%=ddlEYear.ClientID%>").value;
                document.getElementById("<%=txtEDay.ClientID%>").value = d + '/' + m + '/' + y;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="AutoComplete.asmx" />
        </Services>
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageDiv">
                <div class="warningDiv">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="headTable">
                    �ʴ���¡���ç���¹���Դ��������
                </div>
                <div class="spaceDiv"></div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="view" runat="server">
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">����</span>
                                </div>
                                <div class="spaceDiv"></div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�ѧ��Ѵ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch" runat="server" Width="320px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">Version : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch2" runat="server" Width="320px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">Server : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearch3" runat="server" Width="320px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">ʶҹ� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList CssClass="ddlSearch" ID="ddlSearchStatus" runat="server" Width="320px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">੾������ѭ����� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:CheckBox ID="cbRent" runat="server" Text=" ���͡���ʹ�੾���ç���¹�������ѭ�����" ForeColor="Green" AutoPostBack="true" OnCheckedChanged="cbRent_CheckedChanged" />
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
                                <div class="clear"></div>
                                <div class="spaceDiv"></div>
                                <div class="SearchTotal">
                                    <span class="spantxt">�ӹǹ��辺 : </span><span id="lblSearchTotal" class="spantxt"
                                        style="color: Black;" runat="server"></span>&nbsp;<span class="spantxt">��¡��</span>
                                    <asp:Button ID="btLoadPage" CssClass="btNone" OnClick="btLoadPage_Click" runat="server" />
                                    <asp:TextBox ID="txtID" CssClass="txtBoxnone" Width="0px" runat="server"></asp:TextBox>
                                </div>
                                <div class="spaceDiv"></div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="btAddDiv">
                                <input type="button" class="btAdd" onclick="AddItem();" value="      ������¡������" title="������¡������" />
                            </div>
                            <div style="float: right; padding-right: 20px;">
                                <input type="button" class="btNo" value="      �͡" title="�͡�������ѡ" onclick="exit();" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ʶҹ�">
                                        <ItemTemplate>
                                                <%# getStatus(Eval("LogStatus").ToString(), Eval("ckExpDate"), Eval("IPaddress").ToString(), Eval("VirtualName").ToString(), Eval("LogID").ToString(), Eval("eDate")) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="Version" DataField="Version">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���� Virtual">
                                        <ItemTemplate>
                                            <%# getVirtualName(Eval("VirtualName").ToString(), Eval("IPaddress").ToString()) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="11%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�����ç���¹">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("LogID") %>');">
                                                <%# Eval("SchoolName").ToString() %></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="�ѧ��Ѵ" DataField="ProvinceName">
                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="IP Address" DataField="IPaddress">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="���Դ���" DataField="ConnectName">
                                        <ItemStyle Width="7%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="������" DataField="Tel">
                                        <ItemStyle Width="7%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�����" DataField="SaleName">
                                        <ItemStyle Width="7%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="�����ѭ��">
                                        <ItemTemplate>
                                            <%# string.IsNullOrEmpty(Eval("sDate").ToString()) ? "" : Convert.ToDateTime(Eval("sDate")).ToString("dd/MM/yyyy") %> - <%# string.IsNullOrEmpty(Eval("eDate").ToString()) ? "" :  Convert.ToDateTime(Eval("eDate")).ToString("dd/MM/yyyy") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="������">
                                        <ItemTemplate>
                                            <%# getRent(Eval("LogStatus").ToString(), Eval("rent").ToString()) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="BackUp">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="getPopUpBackUp('<%#Eval("IPaddress").ToString() %>','<%#Eval("VirtualName").ToString() %>','<%#Eval("LogID") %>'); UpdatebackUp('<%#Eval("LogID") %>');">
                                                <%# getImgDatabase(Convert.ToDateTime(Eval("UpdateDate"))) %></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="�ѹ���" DataField="UpdateDate" DataFormatString="{0:d}">
                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("LogID") %>');">
                                                <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
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
                                    <span class="spantxt">Version : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtVersion" CssClass="txtBoxL txt100" MaxLength="8" runat="server"
                                        onkeypress="return KeyNumber(event);"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                    <span id="ErrorVersion" class="ErrorMsg">��سһ�͹ Version</span>
                                    <cc1:AutoCompleteExtender runat="server" ID="autoComplete" TargetControlID="txtVersion"
                                        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetVersion" MinimumPrefixLength="1"
                                        CompletionInterval="5" CompletionSetCount="20" EnableCaching="true">
                                    </cc1:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���� Virtual : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtVirtualName" CssClass="txtBoxL txt400" MaxLength="100" runat="server"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                    <span id="ErrorVirtualName" class="ErrorMsg">��سһ�͹���� Virtual</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtSchoolName" CssClass="txtBoxL txt400" MaxLength="100" runat="server"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                    <span id="ErrorSchoolName" class="ErrorMsg">��سһ�͹�����ç���¹</span>
                                    <cc1:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtSchoolName"
                                        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetSchoolName" MinimumPrefixLength="1"
                                        CompletionInterval="5" CompletionSetCount="20" EnableCaching="true">
                                    </cc1:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѧ��Ѵ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlProvince" Width="400px" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">IP Address/���� Server : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtIPaddress" CssClass="txtBoxL txt400" MaxLength="50" runat="server"></asp:TextBox>&nbsp;<span class="ColorRed">*</span>
                                    <span id="ErrorIPaddress" class="ErrorMsg">��سһ�͹ IP Address</span>
                                    <cc1:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtIPaddress"
                                        ServicePath="~/AutoComplete.asmx" ServiceMethod="GetIPaddress" MinimumPrefixLength="1"
                                        CompletionInterval="5" CompletionSetCount="20" EnableCaching="true">
                                    </cc1:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ͼ��Դ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtConnectName" CssClass="txtBoxL txt400" MaxLength="100" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtTel" CssClass="txtBoxL txt400" MaxLength="50" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ͼ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtSaleName" CssClass="txtBoxL txt400" MaxLength="100" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ʶҹ� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatColumns="3"></asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������/��͹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtRent" CssClass="txtBoxNum txt100" MaxLength="7" onkeypress="return KeyNumber(event);" runat="server" Text="0"></asp:TextBox>
                                    <span class="spantxt"> �ҷ </span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ���������� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlSDay" CssClass="ddl" runat="server">
                                    </asp:DropDownList>
                                    /
                                <asp:DropDownList ID="ddlSMonth" CssClass="ddl" runat="server">
                                </asp:DropDownList>
                                    /
                                <asp:DropDownList ID="ddlSYear" CssClass="ddl" runat="server">
                                </asp:DropDownList>
                                    <asp:TextBox ID="txtSDay" CssClass="txtBoxnone" onkeypress="return false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ�������ش : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlEDay" CssClass="ddl" runat="server">
                                    </asp:DropDownList>
                                    /
                                <asp:DropDownList ID="ddlEMonth" CssClass="ddl" runat="server">
                                </asp:DropDownList>
                                    /
                                <asp:DropDownList ID="ddlEYear" CssClass="ddl" runat="server">
                                </asp:DropDownList>
                                    <asp:TextBox ID="txtEDay" CssClass="txtBoxnone" onkeypress="return false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Դ / �Դ Website : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:RadioButtonList ID="rbtlRenew" runat="server" RepeatColumns="2"></asp:RadioButtonList>
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
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡" onclick="Cancel();" />
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
