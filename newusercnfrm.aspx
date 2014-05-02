<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="newusercnfrm.aspx.cs" Inherits="newusercnfrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="divNewUser" runat="server">
    New user
    <asp:Label ID="lblUserName" runat="server"></asp:Label>
&nbsp; added succefully&nbsp;
    <asp:LinkButton ID="lnkMoveBack" runat="server" OnClick="lnkMoveBack_Click">Add More User</asp:LinkButton>
        </div>
    <br />
     <div id="divEditUser" runat="server">
         User ID &nbsp; <asp:Label ID="lblUserID" runat="server"></asp:Label>&nbsp;  Edited Successfully.
         <asp:LinkButton ID="lnkMoveToEdit" runat="server" OnClick="lnkMoveToEdit_Click">Edit More User</asp:LinkButton>
         </div>
</asp:Content>

