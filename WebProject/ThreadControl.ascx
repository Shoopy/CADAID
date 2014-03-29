<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ThreadControl.ascx.cs" Inherits="ThreadControl" %>

<div dir="rtl">
    <asp:Panel ID="PanelThread" runat="server" style="width:900px;">
        <asp:ImageButton ID="voteUp" runat="server" ImageUrl="pictures/voteUp.png" 
            AlternateText="VoteUp" class="voteButton" style="right:0; top:0; margin:auto;" onclick="voteUp_Click" />
        <asp:Label ID="lblThreadRank" runat="server" 
            style="right: 0px; vertical-align:middle; margin:auto;"></asp:Label>
        <asp:ImageButton ID="voteDown" runat="server" ImageUrl="pictures/voteDown.png" 
            AlternateText="VoteDown" class="voteButton" style="right:0px; bottom:0px; margin:auto;"
            onclick="voteDown_Click" />
        <asp:Label ID="lblThreadTitle" runat="server" style="right: 100px; margin: auto; position: relative;"></asp:Label>
        <asp:Label ID="lblThreadDate" runat="server" style="left: 0px; margin:auto; position: fixed;"></asp:Label>
        <asp:Label ID="lblThreadNickName" runat="server" style="left: 100px; margin:auto; position: fixed;"></asp:Label>
        <br />
        <asp:TextBox ID="txtContent" runat="server" BorderStyle="None" BorderWidth="0px" 
            ReadOnly="True" Rows="6" TextMode="MultiLine" 
            style="right: 100px; position: relative; top: 0px;" ></asp:TextBox>
    </asp:Panel>
</div>

