<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div>
    
        <strong>Change Password<br />
        <br />
        </strong>
        <table class="auto-style1">
            <tr>
                <td>UserName:</td>
                <td>
                    <asp:DropDownList ID="ddlUserId" runat="server" Width="192px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>New Password:</td>
                <td>
                    <asp:TextBox ID="txtNewPassword" runat="server" Width="192px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Confirm New Password:</td>
                <td>
                    <asp:TextBox ID="txtConfirmNewPassword" runat="server" Width="192px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
