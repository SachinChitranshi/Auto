<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="activeusers.aspx.cs" Inherits="activeusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <asp:Label ID="lblErrMsg" runat="server" Visible="false"></asp:Label>
    </p>
    <p>
        <strong>Active Users</strong></p>
   
        <asp:GridView ID="gvActivatedUsers" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true">
        </asp:GridView>
  <br />
    <div align="right">
        <asp:Button ID="btnActivatedExcel" runat="server" Text="Excel" /></div>
    <p>
        <asp:Label ID="lblDeactivatedUsers" runat="server" style="font-weight: 700" Text="Deactivated Users"></asp:Label>
    </p>
    <asp:GridView ID="gvDeActivatedUsers" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true">
    </asp:GridView>
     <br />
    <div align="right">
        <asp:Button ID="btnDeactivatedExcel" runat="server" Text="Excel" /></div>
</asp:Content>

