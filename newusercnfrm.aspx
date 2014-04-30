<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="newusercnfrm.aspx.cs" Inherits="newusercnfrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    New user
    <asp:Label ID="lblUserName" runat="server"></asp:Label>
&nbsp; added succefully&nbsp;
    <asp:LinkButton ID="lnkMoveBack" runat="server" OnClick="lnkMoveBack_Click">Add More User</asp:LinkButton>
</asp:Content>

