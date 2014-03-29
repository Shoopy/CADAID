<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<%@ Register Src="ThreadControl.ascx" TagName="ThreadControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divBody">
        <asp:Panel ID="PanelThreads" runat="server">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
