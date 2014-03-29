<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SubmitQuestion.aspx.cs" Inherits="SubmitQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>טופס הכנסת שאלה</h1>
    כותרת שאלה:
    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    <br />
    נושא השאלה:
    <asp:DropDownList ID="ddlSubject" runat="server">
    </asp:DropDownList>
&nbsp;או הכנס משלך:
    <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
    <br />
    בחר תגיות:
    <br />
    <asp:ListBox ID="lstTags" runat="server" Height="85px" Width="145px" 
        SelectionMode="Multiple">
    </asp:ListBox>
    &nbsp;או הכנס משלך:
    <asp:TextBox ID="txtNewTag" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnInsertTag" runat="server" Text="הוסף" 
        onclick="btnInsertTag_Click" />
    <br />
    תוכן השאלה:<br />
    <asp:TextBox ID="txtContent" runat="server" Rows="5" TextMode="MultiLine" 
        Width="419px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="שלח" 
        onclick="btnSubmit_Click" />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>

