<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Try.aspx.cs" Inherits="Try" %>

<%@ Register Src="ThreadControl.ascx" TagName="ThreadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="divBody">
        <asp:Panel ID="PanelThreads" runat="server">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
