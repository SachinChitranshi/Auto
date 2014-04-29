<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recover Password</title>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <strong>Recover Password<br />
        <br />
        </strong>
        <table>
            <tr>
                <td>UserId:</td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server" Width="192px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">OR</td>
            </tr>
            <tr>
                <td>EmailId:</td>
                <td>
                    <asp:TextBox ID="txtEmailId" runat="server" Width="192px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
