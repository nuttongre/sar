<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Viewer.aspx.cs" Inherits="Report_Viewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>�͡��§ҹ...</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
                AutoDataBind="true" DisplayGroupTree="False" EnableDrillDown="False"
                PrintMode="ActiveX" EnableTheming="true" HasCrystalLogo="True" />
        </div>
    </form>
</body>
</html>
