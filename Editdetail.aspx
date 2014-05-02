<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="Editdetail.aspx.cs" Inherits="Editdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Edit User Details</h3>
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
                <asp:Label ID="lblUerID" runat="server"></asp:Label>
            </td>
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
                <asp:TextBox ID="txtDOJ" runat="server" MaxLength="25"></asp:TextBox></td>
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
                <asp:Button ID="btncreate" runat="server" Text="Edit" OnClick="btncreate_Click"  />
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

