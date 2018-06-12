<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="MR_School.aspx.cs" Inherits="MR_School" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AddItem() {
            location.href = "MR_School.aspx?mode=1";
        }
        function EditItem(id) {
            location.href = "MR_School.aspx?mode=2&id=" + id;
        }
        function deleteItem(id) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) location.href = "MR_School.aspx?mode=3&id=" + id;
        }
        function Cancel() {
            location.href = "MR_School.aspx";
        }
        function printRpt(mode, type, id) {
            var yearb = document.getElementById("<%=ddlYearS.ClientID%>").value;
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&yearB=" + yearb + "&rpttype=" + type);
        }
        function deleteImg() {
            if (confirm('��ͧ���ź�Ҿ��� ���������')) {
                return true;
            }
            else {
                return false;
            }
        }
        function ckUpload() {
            var fiUpload = document.getElementById("<%=fiUpload.ClientID%>").value;
            if (fiUpload == "") {
                alert("��س����͡���");
                return;
            }
        }
        function Cktxt(m) {
            var ck = 0;
            var txtSchoolID = $get("<%=txtSchoolID.ClientID%>");
            var ErrorSchoolID = $get("ErrorSchoolID");
            var txtSchoolName = $get("<%=txtSchoolName.ClientID%>");
            var ErrorSchoolName = $get("ErrorSchoolName");
            var txtAddress = $get("<%=txtAddress.ClientID%>");
            var ErrorAddress = $get("ErrorAddress");
            var ddlProvince = $get("<%=ddlProvince.ClientID%>");
            var ErrorProvince = $get("ErrorProvince");
            var txtTel = $get("<%=txtTel.ClientID%>");
            var ErrorTel = $get("ErrorTel");
            var txtAreaOfStudy = $get("<%=txtAreaOfStudy.ClientID%>");
            var ErrorAreaOfStudy = $get("ErrorAreaOfStudy");
            var txtIdentity = $get("<%=txtIdentity.ClientID%>");
            var ErrorIdentity = $get("ErrorIdentity");
            var txtIdentity2 = $get("<%=txtIdentity2.ClientID%>");
            var ErrorIdentity2 = $get("ErrorIdentity2");
            var txtVision = $get("<%=txtVision.ClientID%>");
            var ErrorVision = $get("ErrorVision");
            var txtManagerName = $get("<%=txtManagerName.ClientID%>");
            var ErrorManagerName = $get("ErrorManagerName");

            ck += ckTxtNull(m, txtManagerName, ErrorManagerName);
            ck += ckTxtNull(m, txtVision, ErrorVision);
            ck += ckTxtNull(m, txtIdentity2, ErrorIdentity2);
            ck += ckTxtNull(m, txtIdentity, ErrorIdentity);
            ck += ckTxtNull(m, txtAreaOfStudy, ErrorAreaOfStudy);
            ck += ckTxtNull(m, txtTel, ErrorTel);
            ck += ckDdlNull(m, ddlProvince, ErrorProvince);
            ck += ckTxtNull(m, txtAddress, ErrorAddress);
            ck += ckTxtNull(m, txtSchoolName, ErrorSchoolName);
            ck += ckTxtNull(m, txtSchoolID, ErrorSchoolID);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
     <%--   function CkComma() {
            var txtArea = document.getElementById("<%=txtArea.ClientID%>");
            document.getElementById("<%=txtArea.ClientID%>").value = (txtArea.value == "") ? "0" : txtArea.value.comma();
        }--%>

        function ckddlDate() {
            var d2 = $get("<%=ddlSDay.ClientID%>").value;
            var m2 = $get("<%=ddlSMonth.ClientID%>").value;
            var y2 = $get("<%=ddlSYear.ClientID%>").value;
            ckDatetimeDDL(d2, m2, y2, $get("<%=ddlSDay.ClientID%>"));

            var d = $get("<%=ddlSDay.ClientID%>").value;
            var m = $get("<%=ddlSMonth.ClientID%>").value;
            var y = $get("<%=ddlSYear.ClientID%>").value;
            $get("<%=txtDateBegin.ClientID%>").value = d + '/' + m + '/' + y;
        }

        function ckAddDeputyDirector(m) {
            var ck = 0;
            var txtDeputyDirectorName = $get("<%=txtDeputyDirectorName.ClientID%>");
            var txtPositionDeputyDirector = $get("<%=txtPositionDeputyDirector.ClientID%>");
            var ErrorDeputyDirector = $get("ErrorDeputyDirector");

            ck += ckTxtNull(m, txtDeputyDirectorName, ErrorDeputyDirector);
            ck += ckTxtNull(m, txtPositionDeputyDirector, ErrorDeputyDirector);

            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function editmode(id) {
            var DeputyDirectorName = $get("spnDeputyDirectorName" + id).innerHTML;
            var DeputyDirectorPositon = $get("spnDeputyDirectorPosition" + id).innerHTML;
            $get("<%=txtid.ClientID%>").value = id;
            $get("<%=txtDeputyDirectorName.ClientID%>").value = DeputyDirectorName;
            $get("<%=txtPositionDeputyDirector.ClientID%>").value = DeputyDirectorPositon;
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
            �������ç���¹
        </div>
        <div class="spaceDiv"></div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">�����ç���¹</span>
                                </div>
                                <div class="spaceDiv"></div>
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
                            <%--<div class="btAddDiv">
                        <input type="button" class="btAdd" onclick="AddItem();" value="      �����������ç���¹����" title="�����������ç���¹����" />
                    </div>--%>
                            <div class="spaceDiv"></div>
                        </div>

                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server">
                                <Columns>
                                    <Control:BoundField HeaderText="�����ç���¹" DataField="SchoolNo">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="�����ç���¹">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("SchoolID") %>');"><%# Eval("SchoolName")%></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    </Control:TemplateField>
                                    <Control:BoundField HeaderText="Tel" DataField="Tel">
                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="�ѹ��͵��" DataField="BirthDate" DataFormatString="{0:d}">
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:BoundField HeaderText="����ӹ�¡���ç���¹" DataField="ManagerName">
                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                    </Control:BoundField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("SchoolID") %>');">
                                                <img style="border: 0; cursor: pointer;" alt="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </Control:TemplateField>
                                    <%--<Control:TemplateField HeaderText="ź">
                                <ItemTemplate>
                                    <a href="javascript:deleteItem('<%#Eval("SchoolID") %>');">
                                        <img style="border: 0; cursor: pointer;" alt="ź" src="../Image/delete.gif" /></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                            </Control:TemplateField>--%>
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
                    <div class="inputrowH">
                        <div class="divF_Head">
                            <span class="spantxt">�ٻ�����ç���¹</span>
                        </div>
                        <div class="divB_Head">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; width: 100px; height: 75px;">
                                        <div style="float: right;">
                                            <asp:ImageButton ID="ImgBt" ImageUrl="../Image/delete.gif" runat="server" ToolTip="ź�Ҿ���" OnClick="DeleteImg" OnClientClick="return deleteImg();" />
                                        </div>
                                        <div style="float: left; border: dotted 2px gray; float: left; width: 71px; height: 71px; padding: 3px 3px;">
                                            <asp:Image ID="imgPicture" Width="71" Height="71" runat="server" />
                                        </div>
                                    </div>
                                    <div style="float: left; padding-left: 100px;">
                                        <asp:Literal ID="linkReport" runat="server"></asp:Literal>
                                    </div>
                                    <div class="clear"></div>
                                    <div style="float: left; color: gray; margin-left: 90px; font-size: 5px;">��Ҵ����ٻ����й� 70 x 70 pixel</div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="inputrowH">
                        <div class="divF_Head"></div>
                        <div class="divB_Head">
                            <asp:FileUpload ID="fiUpload" runat="server" CssClass="txtBox"></asp:FileUpload>
                            <input id="btnUpload" type="button" class="btUpload" onserverclick="btnUpload_OnClick" onclick="ckUpload();" value="      Upload" runat="server" />

                        </div>
                    </div>
                    <div class="spaceDiv"></div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt100" ID="txtSchoolID" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorSchoolID" class="ErrorMsg">��سһ�͹�����ç���¹</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtSchoolName" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorSchoolName" class="ErrorMsg">��سһ�͹�����ç���¹</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">������� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtAddress" runat="server" TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorAddress" class="ErrorMsg">��سһ�͹�������</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѧ��Ѵ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlProvince" CssClass="ddl" runat="server">
                                    </asp:DropDownList>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorProvince" class="ErrorMsg">��س����͡�ѧ��Ѵ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�������Ѿ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txtShort" ID="txtTel" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorTel" class="ErrorMsg">��سһ�͹�������Ѿ��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����� (Fax) : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txtShort" ID="txtFax" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ͷ������� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtArea"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�дѺ����֡�ҷ���Դ�͹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="cbPrimary" runat="server" Text=" ��ж��֡��" />
                                    </div>
                                    <div style="float: left;">
                                        <asp:CheckBox ID="cbSecondary" runat="server" Text=" �Ѹ���֡�ҵ͹��" />
                                    </div>
                                    <div style="float: left;">
                                        <asp:CheckBox ID="cbHighSchool" runat="server" Text=" �Ѹ���֡�ҵ͹����" />
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��鹷�����֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtAreaOfStudy" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorAreaOfStudy" class="ErrorMsg">��سһ�͹��鹷�����֡��</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ����͵�� : </span>
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
                                    <asp:TextBox ID="txtDateBegin" CssClass="txtBoxnone" onkeypress="return false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ջ�Ш��ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtSchoolColor" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ѫ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtPhilosophy" runat="server" TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��Ծ���/�Ӣ�ѭ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtSlogan" runat="server" TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��º�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtPolicy" runat="server" TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����ѵ��ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" ID="txtHistory" runat="server" TextMode="MultiLine"
                                        Height="200px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ա���֡�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList CssClass="ddl" ID="ddlYearS" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYearS_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�͡�ѡɳ��ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" TextMode="MultiLine" Height="50px" ID="txtIdentity"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span><span id="ErrorIdentity" class="ErrorMsg">��سһ�͹�͡�ѡɳ��ç���¹</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѵ�ѡɳ��ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" TextMode="MultiLine" Height="50px" ID="txtIdentity2"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span><span id="ErrorIdentity2" class="ErrorMsg">��سһ�͹�ѵ�ѡɳ��ç���¹</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����·�ȹ� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txt400" TextMode="MultiLine" Height="50px" ID="txtVision"
                                        runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorVision" class="ErrorMsg">��سһ�͹����·�ȹ�</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ��Ԩ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Repeater ID="rptMission" runat="server">
                                        <ItemTemplate>
                                            <span class="spantxt3">
                                                <%#Eval("MissionName")%></span>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��һ��ʧ�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Repeater ID="rptGoals" runat="server">
                                        <ItemTemplate>
                                            <span class="spantxt3">
                                                <%#Eval("GoalsName")%></span>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ط�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Repeater ID="rptStrategies" runat="server">
                                        <ItemTemplate>
                                            <span class="spantxt3">
                                                <%#Eval("StrategiesName")%></span>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���ͼ���ӹ�¡���ç���¹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtManagerName" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionManagerName" CssClass="txtBoxL txtShort" TextMode="MultiLine" Height="30" Width="300" runat="server"></asp:TextBox>
                                    <span class="ColorRed">*</span>&nbsp;<span id="ErrorManagerName" class="ErrorMsg">��سһ�͹���ͼ���ӹ�¡���ç���¹</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ͧ��.����� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtUnderManagerName" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionUnderManagerName" CssClass="txtBoxL txtShort" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�����ͧ��.����������çҹ������ҳ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtUnderBudgetName" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionManagerBudgetName" CssClass="txtBoxL txtShort" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�������˹�ҧҹ��º�����Ἱ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtManagerPlanName" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionPlanName" CssClass="txtBoxL txtShort" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�������˹�ҧҹ��ʴ� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtManagerSuppliesName" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionSuppliesName" CssClass="txtBoxL txtShort" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�������˹�ҧҹ����Թ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtManagerMoneyName" CssClass="txtBoxL txtShort" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionMoneyName" CssClass="txtBoxL txtShort" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <asp:TextBox ID="txtKeyWordGroupLeaderPlan" Width="300px" MaxLength="100" CssClass="txtBoxlblnone" runat="server" AutoPostBack="true" OnTextChanged="txtKeyWordGroupLeaderPlan_TextChanged"></asp:TextBox>
                                    <span class="spantxt">: </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txtShort" ID="txtGroupLeaderPlanName" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox CssClass="txtBoxL txtShort" ID="txtPositionGroupLeaderPlan" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <asp:TextBox CssClass="txtBoxnone" ID="txtid" runat="server" Width="0px"></asp:TextBox>
                                    <asp:TextBox ID="txtKeyWordDeputyDirector" Width="250px" MaxLength="100" CssClass="txtBoxlblnone" runat="server" AutoPostBack="true" OnTextChanged="txtKeyWordDeputyDirector_TextChanged"></asp:TextBox>
                                    <span class="spantxt">: </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL txtShort" ID="txtDeputyDirectorName" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox CssClass="txtBoxL txtShort" ID="txtPositionDeputyDirector" Width="300" runat="server" Text="�ͧ����ӹ�¡���ç���¹"></asp:TextBox>
                                    &nbsp;&nbsp;<asp:Button ID="btaddDeputyDirector" CssClass="btAdd" runat="server" OnClientClick="return ckAddDeputyDirector(1);"
                                        OnClick="btaddDeputyDirector_Click" Text="      ������¡��" ToolTip="������¡������" />
                                    <span id="ErrorDeputyDirector" class="ErrorMsg">��سһ�͹������е��˹�</span>
                                </div>
                            </div>
                            <div style="padding-left: 20px; padding-bottom: 5px;">
                                <asp:Button ID="btDelDeputyDirector" CssClass="btDelete" runat="server" Visible="false"
                                    OnClick="btDelDeputyDirector_Click" />
                            </div>
                            <div class="gridViewDiv">
                                <Control:DataGridView ID="GridViewDeputyDirector" runat="server" AutoGenerateCheckList="true"
                                    Width="100%" AutoGenerateColumns="False" PageSize="50">
                                    <Columns>
                                        <Control:TemplateField HeaderText="�����ͧ����ӹ�¡���ç���¹">
                                            <ItemTemplate>
                                                <a href="javascript:;" onclick="editmode('<%#Container.DataItemIndex%>');">
                                                    <span id="spnDeputyDirectorName<%#Container.DataItemIndex %>"><%#Eval("DeputyDirectorName").ToString()%></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="45%" />
                                        </Control:TemplateField>
                                        <Control:TemplateField HeaderText="���˹�">
                                            <ItemTemplate>
                                                <span id="spnDeputyDirectorPosition<%#Container.DataItemIndex %>"><%#Eval("DeputyDirectorPosition").ToString()%></span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="45%" />
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
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                            <div class="centerDiv">
                                <div class="classButton">
                                    <div class="classBtSave">
                                        <asp:Button ID="btSave" CssClass="btYes" runat="server" Text="       �ѹ�֡" OnClick="btSave_Click"
                                            OnClientClick="return Cktxt(1);" ToolTip="�ѹ�֡�������ç���¹���" />
                                    </div>
                                    <div class="classBtCancel">
                                        <input type="button" class="btNo" value="      ���ѹ�֡" title="���ѹ�֡�������ç���¹���" onclick="Cancel();" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="clear"></div>
                    <div class="spaceDiv"></div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
