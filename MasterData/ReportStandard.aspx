<%@ Page Language="C#" MasterPageFile="../Master/MasterOriginal.master" AutoEventWireup="true"
    CodeFile="ReportStandard.aspx.cs" Inherits="ReportStandard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function PrintReport()
    {
        var ck;
        
        if(document.getElementById("<%=rbtreport1.ClientID%>").checked == true) 
        { ck = 1;}
        
        if(document.getElementById("<%=rbtreport2.ClientID%>").checked == true) 
        { ck = 2;}
        
        if(ck == 1)
        {
            window.open('../GtReport/Viewer.aspx?rpt=9');
        }
        if(ck == 2)
        {
            window.open('../GtReport/Viewer.aspx?rpt=10');
        }
    }
    </script>

    <table style="width: 100%;">
        <tr>
            <td colspan="2" class="headTable">
                <strong>��§ҹ</strong>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 100%">
                <table id="TableSearch" width="100%">
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btPrint" CssClass="btPrint" runat="server" ToolTip="�������§ҹ"
                                OnClick="btPrint_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <span class="spantxt">�ٻẺ��§ҹ : </span>
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtreport1" runat="server" Text="��§ҹ�ҵðҹ����֡�Ң�鹾�鹰ҹ" GroupName="rpt" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtreport2" runat="server" Text="��§ҹ��͸Ժ������дѺ�س�Ҿ�ҵðҹ����֡�Ң�鹾�鹰ҹ" GroupName="rpt" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
