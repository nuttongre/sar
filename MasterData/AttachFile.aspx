<%@ Page Language="C#" MasterPageFile="~/Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="AttachFile.aspx.cs" Inherits="AttachFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../CSS/fileupload.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function AttDel(id, obj) {
            if (confirm('��ͧ���ź��¡�ù�� ���������')) {
                $.getJSON("AttachFile.aspx?opt=delAtt&attID=" + id, function (response) {
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
                    <img id="Img1" runat="server" visible="false" />
                    <asp:Label ID="MsgHead" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="headTable">
            Ṻ����������
        </div>
        <div class="spaceDiv"></div>
        <div id="Div1" class="TableSearch">
            <div class="SearchTable">
                <div class="SearchHead">
                    <span class="spantxt2 spansize14">�������</span>
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
                        <span class="spantxt">�Ӥ��� : </span>
                    </div>
                    <div class="SearchF">
                        <asp:TextBox CssClass="txtSearch" ID="txtSearch" runat="server"></asp:TextBox><asp:Button
                            CssClass="btSearch" onmouseover="SearchBt(this,'btSearchOver');" onmouseout="SearchBt(this,'btSearch');"
                            ID="Button1" runat="server" OnClick="btSearch_Click" ToolTip="�������ͤ���" Text="  " />
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
        <div id="Div2" class="PageManageDiv">
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
                    <div class="framImg" style="width:650px; height:300px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <Control:DataGridView ID="GridView1" runat="server">
                    <Columns>
                        <Control:TemplateField HeaderText="�������">
                            <ItemTemplate>
                                <a target="_blank" href="<%# ResolveClientUrl(Eval("FilePath").ToString()) %>">
                                    <%# Eval("FileName") %>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="95%" />
                        </Control:TemplateField>
                        <Control:TemplateField HeaderText="ź">
                            <ItemTemplate>
                                <a href="javascript:deleteItem('<%#Eval("AttachCode") %>');">
                                    <img style="border: 0; cursor: pointer;" alt="ź" src="../Image/delete.gif" /></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                        </Control:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" />
                </Control:DataGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
