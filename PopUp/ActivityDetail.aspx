<%@ Page Language="C#" MasterPageFile="~/Master/MasterPopUp.master" AutoEventWireup="true"
    CodeFile="ActivityDetail.aspx.cs" Inherits="ActivityDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../CSS/fileupload.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/lightbox.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/lightbox-plus-jquery.min.js" type="text/javascript"></script>
    <script src="../scripts/Page.js" type="text/javascript"></script>

    <script type="text/javascript">
        function AddItem() {
            location.href="ActivityDetail.aspx?mode=1&acid=" + request("acid");
        }
        function EditItem(id, acid) {
            location.href = "ActivityDetail.aspx?mode=2&id=" + id + "&acid=" + acid;
        }
        function deleteItem(id) {
            if(confirm('��ͧ���ź��¡�ù�� ���������')) location.href="ActivityDetail.aspx?mode=3&id=" + id + "&acid=" + request("acid");
        }
        function Cancel() {
            location.href="ActivityDetail.aspx?acid=" + request("acid");
        }
        function printRpt(mode, type, id) {
            window.open("../GtReport/Viewer.aspx?rpt=" + mode + "&id=" + id + "&rpttype=" + type);
        }
        function Cktxt(m) {
            var ck = 0;
        
            var txtActivityDetail = $get("<%=txtActivityDetail.ClientID%>");
            var ErrorActivityDetail = $get("ErrorActivityDetail");
            var txtWithdrow = $get("<%=txtWithdrow.ClientID%>");
            var ErrorWithdrow = $get("ErrorWithdrow");         
            

            ck += ckTxtNull(m, txtWithdrow, ErrorWithdrow);
            ck += ckTxtNull(m, txtActivityDetail, ErrorActivityDetail);


            if (ck > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function ckddlDate()
        {
            var d2 = $get("<%=ddlSDay.ClientID%>").value;
            var m2 = $get("<%=ddlSMonth.ClientID%>").value;
            var y2 = $get("<%=ddlSYear.ClientID%>").value;
            ckDatetimeDDL(d2, m2, y2, $get("<%=ddlSDay.ClientID%>"));

            var d = $get("<%=ddlSDay.ClientID%>").value;
            var m = $get("<%=ddlSMonth.ClientID%>").value;
            var y = $get("<%=ddlSYear.ClientID%>").value;
            $get("<%=txtSDay.ClientID%>").value = d + '/' + m + '/' + y;
        }
        function ckddlDate2()
        {
            var d2 = $get("<%=ddlSDay2.ClientID%>").value;
            var m2 = $get("<%=ddlSMonth2.ClientID%>").value;
            var y2 = $get("<%=ddlSYear2.ClientID%>").value;
            ckDatetimeDDL(d2, m2, y2, $get("<%=ddlSDay2.ClientID%>"));

            var d = $get("<%=ddlSDay2.ClientID%>").value;
            var m = $get("<%=ddlSMonth2.ClientID%>").value;
            var y = $get("<%=ddlSYear2.ClientID%>").value;
            $get("<%=txtSDay2.ClientID%>").value = d + '/' + m + '/' + y;
        }
        function SumCost(inx)
        {
            var txtDD = $get("txtDD"+inx).value.replace(/,/g, '');
            var txtC = $get("txtC"+inx).value.replace(/,/g, '');
            $get("txtT"+inx).value = (parseFloat(txtDD) * parseFloat(txtC)).comma();
        }
        function SumTotal() 
        {
            var Total = 0;
            var TotalDiff = 0;
            var grid=$get("<%=GridViewBudget.ClientID %>");
            //alert(grid.rows.length - 1);
            for(var i=0;i<grid.rows.length - 2;i++)
            {
                var txtT = $get("txtT"+i).value.replace(/,/g, '');
                                                                        
                Total = Total + parseFloat(txtT);
                    
            }
            $get("txtTotalT").innerHTML = Total.comma();
            $get("txtTotalT2").value = Total.comma();         
        }
        function enabledtxt(id)
        {
            $get("txtT"+id).disabled = true;
            $get("txtDD"+id).disabled = true;
        }
        function AttDel(id, obj) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) {
                $.getJSON("ActivityDetail.aspx?opt=delAtt&attID=" + id, function (response) {
                    if ($(obj).parent().length == 1) {
                        $(obj).parent().parent().closest("div").remove();
                    }
                });
            }
        }
        function ValidateAttachFile()
        {
            var attName = $get("<%=fpAttach.ClientID%>").value;
            if(attName == "")
            {
                alert("��س����͡���");
                return false;
            }
            else
            {
                return true;
            }
        }
        function AttachShow(id) {
            dialogBox.show('../PopUp/PopUpAttachFile.aspx?id=' + id, '�ʴ��͡������Ṻ', '650', '400', '#000', true);
        }

        $(document).on('click', '[data-toggle="lightbox"]', function(event) {
            event.preventDefault();
            $(this).ekkoLightbox();
        });

        function EnableStatus(ID) {
            var IsUse = 0;
            var ckWait = 0;
            if ($('#img' + ID).attr('src') == '../Image/no.png') {
                IsUse = 1;
            }
            else {
                IsUse = 0;
            }

            $.ajax({
                type: "POST",
                url: page.getWebMethodUrl("EnableStatus"),
                cache: false,
                data: "{'id':'" + ID + "','IsUse':'" + IsUse + "'}",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (IsUse == 0) {
                        $('#img' + ID).attr('src', '../Image/no.png');
                        $('#img' + ID).attr('title', '�ѧ����׹�ѹ��');
                    } else {
                        $('#img' + ID).attr('src', '../Image/ok.png');
                        $('#img' + ID).attr('title', '�׹�ѹ������');
                    }
                }
            });
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
                <div class="headTable">
                    <asp:Label ID="lalHead" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="spaceDiv"></div>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="Div1" class="TableSearch">
                            <div class="SearchTable">
                                <div class="SearchHead">
                                    <span class="spantxt2 spansize14">������¡��</span>
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
                                <div class="inputrow" style="height: auto; ">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">���͡Ԩ���� : </span>
                                    </div>
                                    <div class="SearchF" style="height: auto;">
                                        <asp:Label ID="lblActivityName" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Large"></asp:Label>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">������ҳ�������� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:Label ID="lblOBudget" runat="server" Text="0.00" CssClass="spantxt2"></asp:Label><span class="spantxt"> �ҷ</span>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">������ҳ������ : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:Label ID="lblNBudget" runat="server" Text="0.00" CssClass="spantxt2"></asp:Label><span class="spantxt"> �ҷ</span>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">��������� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:Label ID="lblBalance" runat="server" Text="0.00" CssClass="spantxt2"></asp:Label><span class="spantxt"> �ҷ</span>
                                    </div>
                                </div>
                                <div class="inputrow">
                                    <div class="SearchtxtF">
                                        <span class="spantxt">ʶҹС�÷ӡԨ���� : </span>
                                    </div>
                                    <div class="SearchF">
                                        <asp:Label ID="lblActivityStatus" runat="server" Text="0" CssClass="spantxt2"></asp:Label>
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
                                <input type="button" class="btAdd" onclick="AddItem();" value="      ���ҧ��¡������" title="���ҧ��¡������" />
                            </div>
                            <div class="spaceDiv"></div>
                        </div>
                        <div class="clear"></div>
                        <div class="spaceDiv"></div>
                        <div class="gridViewDiv">
                            <Control:DataGridView ID="GridView1" runat="server" ShowFooter="true" PageSize="50">
                                <Columns>
                                    <Control:TemplateField HeaderText="�ѹ���">
                                        <ItemTemplate>
                                            <%#DateFormat (Eval("TDay"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���駷��">
                                        <ItemTemplate>
                                            <%#(Eval("Sort"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="�Դ���" SortExpression="ActivityStatus">
                                        <ItemTemplate>
                                            <%# getActivityStatus(Eval("ActivityDetailStatus").ToString()) %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="13pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��������´��÷ӡԨ����">
                                        <ItemTemplate>
                                            <%#ActivityDetailName(Eval("ActivityDetail"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            ��� :
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="������ҳ">
                                        <ItemTemplate>
                                            <%# GetTotalAmount(Convert.ToDecimal(Eval("NAmount").ToString())).ToString("N2")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <FooterTemplate>
                                            <%# GetSumTotalAmount().ToString("N2")%>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="��Ǩ�ͺ">
                                        <ItemTemplate>
                                            <%# getConfrim(Eval("ActivityDetailCode"), Eval("Confirm"))%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���Ṻ">
                                        <ItemTemplate>
                                            <%# AttachShow(Eval("ActivityDetailCode").ToString())%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺤Ӣ�">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(16,'w','<%#Eval("ActivityDetailCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺺ѹ�֡��ͤ��� Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(16,'e','<%#Eval("ActivityDetailCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺺ѹ�֡��ͤ��� Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(16,'p','<%#Eval("ActivityDetailCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺺ѹ�֡��ͤ��� Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="㺻�˹��">
                                        <ItemTemplate>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("W") %> onclick="printRpt(49,'w','<%#Eval("ActivityDetailCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺢�͹حҵ���Թ�ҹ Ẻ�͡��� Word" src="../Image/WordIcon.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("E") %> onclick="printRpt(49,'e','<%#Eval("ActivityDetailCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺢�͹حҵ���Թ�ҹ Ẻ�͡��� Excel" src="../Image/Excel.png" /></a>
                                            <a href="javascript:;" <%# new BTC().getLinkReportWEP("P") %> onclick="printRpt(49,'p','<%#Eval("ActivityDetailCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���¡��㺢�͹حҵ���Թ�ҹ Ẻ�͡��� PDF" src="../Image/PdfIcon.png" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="���">
                                        <ItemTemplate>
                                            <a href="javascript:;" onclick="EditItem('<%#Eval("ActivityDetailCode") %>','<%#Eval("ActivityCode")%>');">
                                                <img style="border: 0; cursor: pointer;" title="���" src="../Image/edit.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                    </Control:TemplateField>
                                    <Control:TemplateField HeaderText="ź">
                                        <ItemTemplate>
                                            <a href="javascript:deleteItem('<%#Eval("ActivityDetailCode") %>');">
                                                <img style="border: 0; cursor: pointer;" title="ź" src="../Image/delete.gif" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                            BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
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
                <div id="table1" class="PageManageDiv">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ����ʹ� (㺻�˹��) : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlSDay2" CssClass="ddl" runat="server">
                                    </asp:DropDownList>
                                    /
                                <asp:DropDownList ID="ddlSMonth2" CssClass="ddl" runat="server">
                                </asp:DropDownList>
                                    /
                                <asp:DropDownList ID="ddlSYear2" CssClass="ddl" runat="server">
                                </asp:DropDownList>
                                    <asp:TextBox ID="txtSDay2" CssClass="txtBoxnone" onkeypress="return false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ѹ����ԡ (㺤Ӣ�) : </span>
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
                                    <asp:HiddenField ID="hdfID" runat="server" />
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt spantxt2 spansize14">�ç��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblProjects" CssClass="spantxt3 spantxt2 spansize14" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="txtActivity" CssClass="spantxt6 spantxt2" runat="server"></asp:Label>
                                    <asp:Label ID="txtActivityCode" Font-Size="0" ForeColor="White" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">�ԡ���¤��駷�� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtWithdrow" CssClass="txtBoxNoBorder spantxt3 spantxt2" onkeypress="return KeyNumber(event);" MaxLength="2" runat="server"></asp:TextBox><span class="ColorRed">*</span> <span id="ErrorWithdrow" class="ErrorMsg">��سһ�͹���駷���ԡ</span>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtObjective" runat="server" TextMode="MultiLine" MaxLength="200"
                                        Height="50px" Width="600px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">��������´ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox CssClass="txtBoxL" ID="txtActivityDetail" runat="server" TextMode="MultiLine"
                                        Height="150px" Width="600px"></asp:TextBox>
                                    <span class="ColorRed">*</span> <span id="ErrorActivityDetail" class="ErrorMsg">��سһ�͹��������´��÷ӡԨ����</span>
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
                            <div id="DivActivityStatus" class="inputrowH" runat="server" visible="false">
                                <div class="divF_Head">
                                    <span class="spantxt">ʶҹС�÷ӡԨ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlActivityStatus" CssClass="ddl" runat="server"></asp:DropDownList>
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
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="spaceDiv"></div>
                            <div id="Div3" class="TableSearch" style="width:96%;padding:5px 20px;">
                                <div class="SearchTable" style="text-align: center; width:100%;">
                                    <div class="SearchHead HeadCenter">
                                        <span class="spantxt2">��¡�ç�����ҳ</span>
                                    </div>
                                    <div class="spaceDiv"></div>
                                    <asp:HiddenField ID="hdfckItemID" runat="server" />
                                    <div class="gridViewDiv">
                                        <Control:DataGridView ID="GridViewBudget" runat="server" Width="100%" AutoGenerateColumns="False"
                                            PageSize="300" ShowFooter="true" OnRowDataBound="GridViewBudget_RowDataBound">
                                            <Columns>
                                                <Control:TemplateField HeaderText="���">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="��¡��">
                                                    <ItemTemplate>
                                                        <%#Eval("EntryCostsName")%>
                                                        <input id="txtEntry<%#Container.DataItemIndex %>" name="txtEntry<%#Container.DataItemIndex %>"
                                                            type="hidden" value="<%#Eval("ItemID") %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="17%" />
                                                    <FooterTemplate>
                                                        ��� :
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="�������">
                                                    <ItemTemplate>
                                                        <%# GetTotalMoney(Convert.ToDecimal(Eval("TotalMoney").ToString())).ToString("N2")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    <FooterTemplate>
                                                        <%# GetSumTotalMoney().ToString("N2")%>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="��������">
                                                    <ItemTemplate>
                                                        <%# GetTotalOldMoney(Convert.ToDecimal(Eval("OldTotalAmount").ToString())).ToString("N2")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    <FooterTemplate>
                                                        <%# GetSumTotalOldMoney().ToString("N2")%>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="˹��·����">
                                                    <ItemTemplate>
                                                        <span class="spantxt2"><%# Convert.ToInt32(Eval("TotalD")).ToString("#,##0") %> <%# Eval("TotalP") %></span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="�ӹǹ">
                                                    <ItemTemplate>
                                                        <input id="txtDD<%#Container.DataItemIndex %>" name="txtDD<%#Container.DataItemIndex %>"
                                                            type="text" class="txtBoxNum txt50" value="<%#Eval("TotalDD") %>" onkeyup="SumCost(<%#Container.DataItemIndex %>); SumTotal();"
                                                            onchange="txtComma(txtDD<%#Container.DataItemIndex%>);" onkeypress="return KeyNumber(event);"
                                                            onclick="SelecttxtAll(txtDD<%#Container.DataItemIndex%>);" onblur="txtZero(txtDD<%#Container.DataItemIndex%>,0); SumCost(<%#Container.DataItemIndex %>); SumTotal();" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="�Ҥ�/˹���">
                                                    <ItemTemplate>
                                                        <input id="txtC<%#Container.DataItemIndex %>" name="txtC<%#Container.DataItemIndex %>"
                                                            type="text" class="txtBoxNum txt100" value="<%# Convert.ToDecimal(Eval("TotalG")).ToString("#,##0.00") %>" onkeyup="SumCost(<%#Container.DataItemIndex %>); SumTotal();"
                                                            onchange="txtComma(txtC<%#Container.DataItemIndex%>);" onkeypress="return KeyNumber(event);"
                                                            onclick="SelecttxtAll(txtC<%#Container.DataItemIndex%>);" onblur="txtZero(txtC<%#Container.DataItemIndex%>,0); SumCost(<%#Container.DataItemIndex %>); SumTotal();" />
                                                        <%--<span id="txtC<%#Container.DataItemIndex %>" class="spantxt2"><%# Convert.ToDecimal(Eval("TotalG")).ToString("#,##0.00") %></span>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:TemplateField HeaderText="�������ԡ">
                                                    <ItemTemplate>
                                                        <input id="txtT<%#Container.DataItemIndex %>" name="txtT<%#Container.DataItemIndex %>"
                                                            type="text" class="txtBoxNum txt100" value="<%#Eval("TotalMoney2Ag") %>" onkeyup="SumTotal();"
                                                            onchange="txtComma(txtT<%#Container.DataItemIndex%>);" onkeypress="return KeyNumber(event);"
                                                            onclick="SelecttxtAll(txtT<%#Container.DataItemIndex%>);" onblur="txtZero(txtT<%#Container.DataItemIndex%>,0); SumTotal();" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    <FooterTemplate>
                                                        <span id="txtTotalT"></span>
                                                        <input id="txtTotalT2" name="txtTotalT2" type="hidden" />
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:TemplateField>
                                                <Control:BoundField DataField="BudgetTypeName" HeaderText="�������ͧ�Թ" SortExpression="">
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12pt"
                                                        BackColor="#cecece" BorderStyle="Solid" BorderColor="gray" BorderWidth="2" />
                                                </Control:BoundField>
                                            </Columns>
                                        </Control:DataGridView>
                                        <div class="clear"></div>
                                        <div class="spaceDiv"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ʶҹ�����Թ��� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblPlace" runat="server" CssClass="spantxt3"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">˹��§ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblDept" runat="server" CssClass="spantxt3"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">����Ѻ�Դ�ͺ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:Label ID="lblEmp" runat="server" CssClass="spantxt3"></asp:Label>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ŧ���ͼ���Ѻ�Դ�ͺ�Ԩ���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlResponsibleName" CssClass="ddl" runat="server" Width="198"></asp:DropDownList>
                                    <asp:TextBox ID="txtResponsibleName" CssClass="txtBoxL" runat="server" Visible="false"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionResponsible" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                    <a class="example-image-link" href="../Image/ReportRequisitionExp1.jpg" data-lightbox="example-1" title="�ٵ�����ҧ���˹����§ҹ">
                                        <img class="example-image" src="../Image/MenuStlye/S1.png" alt="image-1" /></a>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ŧ�������˹�ҷ���ʴ� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtSuppliesName" CssClass="txtBoxL" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionSupplies" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ŧ�������˹�ҡ�����ҹ : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtHeadGroupName" CssClass="txtBoxL" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionHeadGroup" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                    <a class="example-image-link" href="../Image/ReportRequisitionExp2.jpg" data-lightbox="example-2" title="�ٵ�����ҧ���˹����§ҹ">
                                        <img class="example-image" src="../Image/MenuStlye/S1.png" alt="image-1" /></a>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ŧ�������˹�ҧҹ/���������� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtHeadGroupSaraName" CssClass="txtBoxL" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionHeadGroupSara" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                    <a class="example-image-link" href="../Image/ReportRequisitionExp3.jpg" data-lightbox="example-3" title="�ٵ�����ҧ���˹����§ҹ">
                                        <img class="example-image" src="../Image/MenuStlye/S1.png" alt="image-1" /></a>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <span class="spantxt">ŧ�����ͧ����ӹ�¡�á���� : </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:TextBox ID="txtUnderManagerName" CssClass="txtBoxL" runat="server"></asp:TextBox>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:TextBox ID="txtPositionUnderManagerName" CssClass="txtBoxL" Width="300" runat="server"></asp:TextBox>
                                    <a class="example-image-link" href="../Image/ReportRequisition2Exp1.jpg" data-lightbox="example-4" title="�ٵ�����ҧ���˹����§ҹ">
                                        <img class="example-image" src="../Image/MenuStlye/S1.png" alt="image-1" /></a>
                                </div>
                            </div>
                            <div class="inputrowH">
                                <div class="divF_Head">
                                    <asp:TextBox ID="txtKeyWordDeputyDirector" Width="250px" MaxLength="100" CssClass="txtBoxlblnone" runat="server" AutoPostBack="true" OnTextChanged="txtKeyWordDeputyDirector_TextChanged"></asp:TextBox>
                                    <span class="spantxt">: </span>
                                </div>
                                <div class="divB_Head">
                                    <asp:DropDownList ID="ddlDeputyDirectorName" runat="server" CssClass="ddl" Width="195" AutoPostBack="true" OnSelectedIndexChanged="ddlDeputyDirectorName_SelectedIndexChanged"></asp:DropDownList>
                                    <span class="spantxt">���˹� : </span>
                                    <asp:Label ID="lblPositionDeputyDirector" runat="server" Text="�ͧ����ӹ�¡���ç���¹"></asp:Label>
                                    <%--��������ç���¹ʵ����������·��--%>
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
                                        <input type="button" class="btNo" value="       ���ѹ�֡" onclick="Cancel();" title="���ѹ�֡" />
                                    </div>
                                </div>
                                <div class="spaceDiv"></div>
                                <asp:Label ID="lblConfirm" ForeColor="Red" Visible="false" runat="server" Text="*** ���ͧ�ҡ�ա�õ�Ǩ�ͺ�ʹ�Թ����� ��Ҩзӡ��������Դ��ͽ����Ἱ�ҹ ***"></asp:Label>
                            </div>
                            <div class="clear"></div>
                            <div class="spaceDiv"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
