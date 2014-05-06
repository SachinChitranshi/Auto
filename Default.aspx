<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<!DOCTYPE html>
<html lang="en">
  <head>

    <link id="Link1" runat="server" rel="shortcut icon" href="images/favicon.jpeg" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="images/favicon.jpeg" type="image/ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Login:</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="assets/ico/favicon.ico">

    
    <link href="css/bootstrap.min.css" rel="stylesheet">

   
    <link href="css/signin.css" rel="stylesheet">
    
     <link href="css/style.css" rel="stylesheet">
    

    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body>
 
    
    
<div class="container"> 
        <form id="Form1" runat="server" class="form-signin" role="form">
        <h4 class="form-signin-heading">Please sign in for Vehicle Service</h4>
		
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
       
       <asp:TextBox ID="txtLogin" type="text" runat="server" class="form-control" placeholder="Username" 
       required autofocus></asp:TextBox>
		
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" type="password" class="form-control" 
        placeholder="Password" required></asp:TextBox>    

		<asp:LinkButton ID="lnkForgotPassword" runat="server" OnClick="lnkForgotPassword_Click">Forgot Password
        </asp:LinkButton>
        
        <asp:Button ID="Button" runat="server" Text="Login" OnClick="btnSubmit_Click" 
        class="btn btn-lg btn-primary btn-block" type="submit"/>
        
      </form>
</div>
		<script src="js/bootstrap.min.js"></script>
</body>
</html>
