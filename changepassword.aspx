<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">

    <link id="Link1" runat="server" rel="shortcut icon" href="images/favicon.jpeg" type="image/x-icon" />
<link id="Link2" runat="server" rel="icon" href="images/favicon.jpeg" type="image/ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Change Password:</title>

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
 <form id="form1" runat="server" class="form-signin" role="form">

<h4 class="form-signin-heading">Change Password</h4>

<div class="form-group">
<label for="ddlUserId">Username</label> 
<asp:DropDownList ID="ddlUserId" runat="server" class="form-control">
                    </asp:DropDownList>
                    
<!--select class="form-control">
  <option>1</option>
  <option>2</option>
  <option>3</option>
  <option>4</option>
  <option>5</option>
</select-->
                    
                    
</div>
<div class="form-group">        
<label for="txtNewPassword">New Password</label>             
<asp:TextBox ID="txtNewPassword" runat="server" type="password" class="form-control" placeholder="Password" required></asp:TextBox>


</div>
<div class="form-group">
<label for="txtConfirmNewPassword">Confirm New Password:</label>  
 <asp:TextBox ID="txtConfirmNewPassword" runat="server" type="password" class="form-control" placeholder="Password" required></asp:TextBox>
               
</div>
<div class="form-group">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-lg btn-primary btn-block" type="submit" />
</div>

<div class="form-group">
<asp:Label ID="lblErrMsg" runat="server" CssClass="bg-warning"></asp:Label>
</div>


</form>
    
  </div>  
    
</body>
</html>
