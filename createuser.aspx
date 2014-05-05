<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="createuser.aspx.cs" Inherits="createuser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h3>Create New User</h3>
    <div>
    <table>
        <tr>
            <td>*Fields are mandatory</td>
        </tr>
      
        <tr>
            <td>
                </td>
            <td>
                </td>
        </tr>
      
        <tr>
            <td>
                User Type :
            <td>
                <asp:DropDownList ID="ddlistUsertype" runat="server">
                </asp:DropDownList></td>
        </tr>
      
        <tr>
            <td>
                User Id:<td>
                <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
                *</td>
        </tr>
      
        <tr>
            <td>
                Password :</td>
            <td>
                <asp:TextBox ID="txtpassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                *</td>
        </tr>
        <tr>
            <td>Confirm Password :</td>
            <td>
                <asp:TextBox ID="txtconfirmpassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                *</td>
        </tr>
        <tr>
            <td>
                Mobile No. :</td>
            <td>
                <asp:TextBox ID="txtmobileno" runat="server" MaxLength="15"></asp:TextBox>
                *</td>
            
        </tr>
        <tr>
            <td>
                First Name :</td>
            <td>
                <asp:TextBox ID="txtfirstname" runat="server" MaxLength="25"></asp:TextBox>
                *</td>
        </tr>
        
        <tr>
            <td>
                Last Name :</td>
            <td>
                <asp:TextBox ID="txtlastname" runat="server" MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                DOJ :</td>
            <td>
                <asp:TextBox ID="txtDOJ" runat="server" MaxLength="25"></asp:TextBox>
                <cc1:CalendarExtender ID="ceFrom" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDOJ">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                E-mail Id :</td>
            <td>
                <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>*</td>
        </tr>
        <tr>
            <td>
                Mobile Login :</td>
            <td>
                <asp:CheckBox ID="cbMobileLogin" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btncreate" runat="server" Text="Create" OnClick="btncreate_Click"  />
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblErrMsg" runat="server" Visible="False"></asp:Label>
                </td> 
        </tr>
    </table>
    </div>
</asp:Content>

