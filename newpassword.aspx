<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newpassword.aspx.cs" Inherits="newpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div align="right">
        <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click">Logout</asp:LinkButton> 
    </div>

    <table>
        <tr>
            <td>Old Password</td>
            <td>
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                *</td>
        </tr>
        <tr>
            <td>New Password</td>
            <td>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                *</td>
        </tr>
        <tr>
            <td>Confirm New Password</td>
            <td>
                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                *</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblErrMsg" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>

    </form>
    </body>
</html>
