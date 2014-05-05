<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link id="Link1" runat="server" rel="shortcut icon" href="images/favicon.jpeg" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="images/favicon.jpeg" type="image/ico" />
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Login:</title>
</head>
<body>
  <form id="Form1" runat="server">
  Vehicle Service
  <br />	
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
        <br />
      <label for="usernanme">Login:</label>
      <asp:TextBox ID="txtLogin" runat="server" Width="147px"></asp:TextBox><br /><br />
      <label for="password">Password:</label>
      <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="147px"></asp:TextBox><br />
        <br />
        <asp:LinkButton ID="lnkForgotPassword" runat="server" OnClick="lnkForgotPassword_Click">Forgot Password</asp:LinkButton><br /><br />
      <label for="submit"></label>
      <asp:Button ID="Button" runat="server" Text="Login" OnClick="btnSubmit_Click" />
      <br />    
  </form>
</body>
</html>
