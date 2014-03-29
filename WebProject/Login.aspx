<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Login ID="Login1" runat="server" LoginButtonText="התחבר" 
        PasswordLabelText="סיסמה:" TitleText="כניסת משתמשים" 
        UserNameLabelText="שם משתמש:" DisplayRememberMe="False" 
    DestinationPageUrl="~/HomePage.aspx" onauthenticate="Login1_Authenticate">
        <TitleTextStyle Font-Underline="True" Font-Size="Medium" />
    </asp:Login>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    
    </asp:Content>
