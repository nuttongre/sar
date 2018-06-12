<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="Indicators2Edit.aspx.cs" Inherits="Indicators2Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/fileupload.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function AddItem() {
            location.href = "Indicators2Edit.aspx?mode=1";
        }
        function EditItem(id, acid) {
            location.href = "Indicators2Edit.aspx?mode=2&id=" + id +"&acid=" + acid;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "Indicators2Edit.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "Indicators2Edit.aspx";
        }
        function Cktxt(m) {
            var ck = 0;
            var ddlStrategies = $get("<%=ddlStrategies.ClientID%>");
            var ErrorStrategies = $get("ErrorStrategies");
            var ddlProjects = $get("<%=ddlProjects.ClientID%>");
            var ErrorProjects = $get("ErrorProjects");
            var ddlActivity = $get("<%=ddlActivity.ClientID%>");
            var ErrorActivity = $get("ErrorActivity");
            var txtSort = $get("<%=txtSort.ClientID%>");
            var ErrorSort = $get("ErrorSort");

            ck += ckTxtNull(m, txtSort, ErrorSort);
            ck += ckDdlNull(m, ddlActivity, ErrorActivity);
            ck += ckDdlNull(m, ddlProjects, ErrorProjects);
            ck += ckDdlNull(m, ddlStrategies, ErrorStrategies);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        
        function CkNum() {
            var txtCan = $get("<%=txtCan.ClientID%>");

            $get("<%=txtCan.ClientID%>").value = (txtCan.value == "") ? "0" : txtCan.value.comma();
            CkPercent(0);
        }
        function CkPercent(mode) {
            var txtWeight = parseFloat($get("<%=txtWeight.ClientID%>").innerHTML.replace(/,/g, ''));
        var txtAll = parseFloat($get("<%=txtAll.ClientID%>").innerHTML.replace(/,/g, ''));
        var txtCan = parseFloat($get("<%=txtCan.ClientID%>").value.replace(/,/g, ''));

        if (txtCan > txtAll) {
            if (mode == 0) {
                alert("�ӹǹ������  �ҡ����  �ӹǹ������  �����");
            }
            $get("<%=txtCan.ClientID%>").value = 0;
            $get("<%=txtPercent.ClientID%>").value = 0;

            var txtPercent2 = parseFloat($get("<%=txtPercent.ClientID%>").value);
            if (txtPercent2 >= 50) //parseFloat(txtWeight))
            {
                $get("txtPass").style.color = "green";
                $get("txtPass").innerHTML = "��ҹࡳ��";
                $get("ImgPass").src = "../Image/ok.png";
            }
            else {
                $get("txtPass").style.color = "red";
                $get("txtPass").innerHTML = "����ҹࡳ��";
                $get("ImgPass").src = "../Image/no.png";
            }
            return;
        }
        if (txtAll == 0) {
            txtAll = 1;
        }

        if ($get("<%=lblWeightType.ClientID%>").innerHTML == "%") {
            $get("<%=txtPercent.ClientID%>").value = ((txtCan * 100) / txtAll).toFixed(2);
            $get("lblPC").innerHTML = "%";
        }
        else {
            if (txtCan >= txtWeight) {
                var OverAll = txtAll - txtWeight;
                var Over = txtCan - txtWeight;

                if (OverAll == 0) {
                    OverAll = 1;
                }
                if (txtAll == txtWeight) {
                    if (Over == 0) {
                        Over = 1;
                    }
                }

                $get("<%=txtPercent.ClientID%>").value = (50 + ((50 * Over) / OverAll)).toFixed(2);
            }
            else {
                $get("<%=txtPercent.ClientID%>").value = ((50 * txtCan) / txtWeight).toFixed(2);
            }
            $get("lblPC").innerHTML = "%";
        }

        var txtPercent = parseFloat($get("<%=txtPercent.ClientID%>").value);
        if (txtPercent >= 50) //parseFloat(txtWeight))
        {
            $get("txtPass").style.color = "green";
            $get("txtPass").innerHTML = "��ҹࡳ��";
            $get("ImgPass").src = "../Image/ok.png";
        }
        else {
            $get("txtPass").style.color = "red";
            $get("txtPass").innerHTML = "����ҹࡳ��";
            $get("ImgPass").src = "../Image/no.png";
        }
        }
        function AttachShow(id) {
            dialogBox.show('../PopUp/PopUpAttachFile.aspx?id=' + id, '�ʴ��͡������Ṻ', '650', '400', '#000', true);
        }
        function AttDel(id, obj) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) {
                $.getJSON("Indicators2Edit.aspx?opt=delAtt&attID=" + id, function (response) {
                    if ($(obj).parent().length == 1) {
                        $(obj).parent().parent().closest("div").remove();
                    }
                });
            }
        }
        function ValidateAttachFile() {
            var attName = $get("<%=fpAttach.ClientID%>").value;
            if (attName == "") {
                alert("��س����͡���");
                return false;
            }
            else {
                return true;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="pageDiv">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="warningDiv">
                    <asp:Image ID="Img1" runat="server" Visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="headTable">
            �����Թ�Ԩ���������Ǫ���Ѵ
        </div>
        <div class="spaceDiv"></div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">���ҡԨ���������Ǫ���Ѵ</span>
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
                                        <span class="spantxt">�ç��� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">�Ԩ���� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearch1" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearch1_SelectedIndexChanged" Width="500px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">˹��§ҹ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchDept" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchDept_SelectedIndexChanged" Width="350px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:DropDownList ID="ddlSearchEmp" CssClass="ddlSearch" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSearchEmp_SelectedIndexChanged" Width="250px">
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
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" PageSize="20">
                                <Columns>
                                    <Control:TemplateField HeaderText="���͡">
                                        <ItemTemplate>
                                            <a href="Indicators2Edit.aspx?mode=2&id=<%#Eval("Indicators2Code")%>&acid=<%#Eval("ActivityCode")%>">
                                                <img style="border: 0; cursor: pointer;" title="���͡任����Թ�Ԩ����" src="../Image/<%# (Eval("OffThat").ToString()== "0" ? "goto":"goto2")%>.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <%--<Control:BoundField HeaderText="�ç���" DataField="FullName">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </Control:BoundField>
                            <Control:BoundField HeaderText="�Ԩ����" DataField="ActivityName">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </Control:BoundField>--%>
                                    <Control:TemplateField HeaderText="��Ǫ���Ѵ">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("Indicators2Code")%>','<%#Eval("ActivityCode")%>');">
                                                <%#Eval("IndicatorsName2")%></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="38%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <%--<Control:BoundField HeaderText="��Ǫ���Ѵ" DataField="IndicatorsName2">
                                        <ItemStyle Width="38%" HorizontalAlign="Left" CssClass="txtpointer" />
                                    </Control:BoundField>--%>
                                    <Control:BoundField HeaderText="����Ѵ" DataField="WeightType">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�ҡ������" DataField="OffAll" DataFormatString="{0:n0}">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="ࡳ�����ҹ" DataField="Weight" DataFormatString="{0:n0}">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�ŷ�����" DataField="OffThat" DataFormatString="{0:n0}">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���Ṻ">
                                        <ItemTemplate>
                                            <%# AttachShow(Eval("Indicators2Code").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("Indicators2Code")%>','<%#Eval("ActivityCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź" Visible="false">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("Indicators2Code") %>');">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="edit" runat="server">
                <div id="Div2" class="PageManageDiv">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="lblYear" runat="server" class="spantxt">�ա���֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearB" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearB_OnSelectedChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlStrategies" Width="600px" OnSelectedIndexChanged="ddlStrategies_OnSelectedChanged"
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorStrategies" class="ErrorMsg">���͡���ط�</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ç��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlProjects" Width="600px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlProjects_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorProjects" class="ErrorMsg">���͡�ç���</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlActivity" Width="600px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlActivity_OnSelectedChanged" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span> <span id="ErrorActivity" class="ErrorMsg">���͡�Ԩ����</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��������´�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtActivityDetail" CssClass="spantxt4" runat="server" Width="600px"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblType" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Ҥ���¹��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblTerm" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span id="lblYear2" runat="server" class="spantxt">�է�����ҳ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblBudgetYear" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������ҳ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtTotalAmount" CssClass="spantxt2 spantxt5" runat="server"></asp:Label>&nbsp;<span
                                        class="spantxt2 spantxt3">�ҷ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">˹��§ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblDepartment" CssClass="spantxt2 spantxt3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblEmpName" runat="server" CssClass="spantxt2 spantxt4"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ���������Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtSDay" CssClass="spantxt2 spantxt4" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ�������ش�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtEDay" CssClass="spantxt2 spantxt4" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ǫ���Ѵ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtIndicators2Edit" CssClass="spantxt4" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ࡳ�����ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtWeight" CssClass="spantxt6 spantxt2" runat="server"></asp:Label>
                                    <asp:Label ID="lblWeightType" CssClass="spantxt2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ҡ�ӹǹ������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtAll" CssClass="spantxt2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt2 spantxt5">�ӹǹ������ : </span>
                                </div>
                                <div class="divB_Head">
                                    <div style="float: left">
                                        <asp:TextBox CssClass="txtBoxNum" ID="txtCan" MaxLength="7" Width="100px" Font-Bold="true"
                                            Font-Size="15pt" onkeypress="return KeyNumber(event);" onkeyup="CkPercent(0);" onclick="SelecttxtAll(this);" onblur="txtZero(this,0); CkPercent(1);"
                                            runat="server" Text="0"></asp:TextBox>
                                    </div>
                                    <div style="float: left;">
                                        <span id="RateDetail" runat="server" style="float: left; margin-left: 15px;" visible="false">��ṹࡳ��  :  1 = ��Ѻ��ا, 2 = ����, 3 = ��, 4 = ���ҡ, 5 = ��������</span>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Դ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtPercent" CssClass="txtBoxNotType spantxt2" Font-Size="Large" Width="60" runat="server"></asp:TextBox>
                                    <span class="spantxt" id="lblPC">%</span>
                                    <img id="ImgPass" src="../Image/go.png" />
                                    <span id="txtPass" class="spantxt"></span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt2 spantxt5">�ѭ��/����ʹ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBox" ID="txtNote" Width="600px" TextMode="MultiLine" Height="100px"
                                        MaxLength="200" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="clear"></div>
                    <div class="inputrowH">
                        <div class="divF_Head">
                            <span class="spantxt">Ṻ��� : </span>
                        </div>
                        <div class="divB_Head">
                            <img src="../Image/imgIcon.png" title="���͡���" style="float: left; padding-right: 5px; vertical-align: bottom;" />
                            <asp:FileUpload ID="fpAttach" runat="server" Width="200px" /><asp:Button ID="btnAttach" Text="    Ṻ���" CssClass="btAttachFile"
                                runat="server" OnClientClick="return ValidateAttachFile();"
                                OnClick="btnAttach_Click" />
                            <asp:CheckBox ID="cbDuo" runat="server" Text=" ����� Duo" />
                        </div>
                    </div>
                    <div class="inputrowH">
                        <div class="divF_Head">
                            <span class="spantxt">���Ṻ : </span>
                        </div>
                        <div class="divB_Head">
                            <div class="framImg">
                                <asp:Repeater ID="rptAttach" runat="server">
                                    <ItemTemplate>
                                        <div class="showSmallImg">
                                            <div style="margin: 5px auto; width: 65px; height: 50px;">
                                                <img src="../Image/btnremove.png" onclick="AttDel('<%# Eval("ItemId") %>',this);" class="btnDelImg" alt="ź�Ҿ���" title="ź�Ҿ���" />
                                                <%# getImgAttatch(Eval("ItemID"), Eval("Title"), Eval("FileType"), Eval("TypeId")) %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="spaceDiv"></div>
                    <%--<div class="inputrowH">
                        <div class="divF_Head">
                            <span class="spantxt">Ṻ��� : </span>
                        </div>
                        <div class="divB_Head">
                            <asp:FileUpload ID="fpAttach" runat="server" /><asp:Button ID="btnAttach" Text="�������Ṻ"
                                CssClass="button_green2" runat="server" OnClientClick="return ValidateFile();"
                                OnClick="btnAttach_Click" />
                        </div>
                    </div>--%>
                    <%--<div class="inputrowH">
                        <div class="divF_Head">
                            <span class="spantxt" style="vertical-align: top;">���Ṻ������ : </span>
                        </div>
                        <div class="divB_Head">
                            <asp:Repeater ID="rptFile" runat="server">
                                <ItemTemplate>
                                    <span class="rpt_content"><a href="#">
                                        <img style="border: none;" src="../Image/RemoveContact.png" alt="ź���Ṻ�͡" title="ź���Ṻ�͡"
                                            onclick="ConfirmDelete('<%# Eval("AttachId") %>')" /></a> <a target="_blank" href="../AttachFile/<%# Eval("AttachID")%>_<%# Eval("FileName")%>">
                                                <%# Eval("FileName").ToString() %></a> </span>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>--%>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ӴѺ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxNum" ID="txtSort" runat="server" onkeypress="return KeyNumber(event);"
                                        Width="50px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorSort" class="ErrorMsg">��سһ�͹�ӴѺ���</span>
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
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click "
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡��û����Թ�Ԩ���������Ǫ���Ѵ" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡��û����Թ�Ԩ���������Ǫ���Ѵ" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
