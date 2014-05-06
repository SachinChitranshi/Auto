<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="createuser.aspx.cs" Inherits="createuser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h3>Create New User</h3>


  <div class="container">   
   
    
    <div class="form-group">
    <label for="inputSelect" class="col-sm-2 control-label">User Type</label>
     <div class="col-sm-6">
<asp:DropDownList ID="ddlistUsertype" runat="server" class="form-control">
                </asp:DropDownList>
     </div>
  </div>
  
  
    
  <div class="form-group">
    <label for="inputUserID" class="col-sm-2 control-label">User ID :</label>
    <div class="col-sm-6">
     
      
      <asp:TextBox ID="txtUserId" runat="server" type="text" class="form-control" placeholder="UserID"></asp:TextBox>
      
    </div>
  </div>
  
  <div class="form-group">
    <label for="txtpassword" class="col-sm-2 control-label">Password :</label>
    <div class="col-sm-6">
      
      
      <asp:TextBox ID="txtpassword" runat="server" MaxLength="50" TextMode="Password" type="password" class="form-control" placeholder="Password"></asp:TextBox>
      
      
    </div>
  </div>
  
  <div class="form-group">
  <label for="txtconfirmpassword" class="col-sm-2 control-label">Confirm Password :</label>
  <div class="col-sm-6">
 
  
  <asp:TextBox ID="txtconfirmpassword" runat="server" MaxLength="50" TextMode="Password" type="password" class="form-control" placeholder="Confirm Password"></asp:TextBox>
  
  
  </div>
  </div>
  
  <div class="form-group">
  <label for="txtmobileno" class="col-sm-2 control-label">Mobile No. :</label>
  <div class="col-sm-6">
  
  
  <asp:TextBox ID="txtmobileno" runat="server" type="number" class="form-control" placeholder="Mobile No." />
  
  </div>
  </div>
  
   <div class="form-group">
  <label for="txtfirstname" class="col-sm-2 control-label">First Name :</label>
  <div class="col-sm-6">

  
  <asp:TextBox ID="txtfirstname" runat="server" MaxLength="25"  type="text" class="form-control" placeholder="First Name"></asp:TextBox>
  
  </div>
  </div>
  
  <div class="form-group">
  <label for="txtlastname" class="col-sm-2 control-label">Last Name :</label>
  <div class="col-sm-6">

  
  <asp:TextBox ID="txtlastname" runat="server" MaxLength="25" type="text" class="form-control" placeholder="Last Name"></asp:TextBox>
  
  
  </div>
  </div>
  
  <div class="form-group">
  <label for="txtDOJ" class="col-sm-2 control-label">Date of Birth :</label>
  <div class="col-sm-6">
  
  
  <asp:TextBox ID="txtDOJ" runat="server" MaxLength="25" type="number" class="form-control" placeholder="Date of Birth"></asp:TextBox>
                <cc1:CalendarExtender ID="ceFrom" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDOJ">
                </cc1:CalendarExtender>
                
  
  </div>
  </div>
  
  
   <div class="form-group">
  <label for="txtemail" class="col-sm-2 control-label">Email ID :</label>
  <div class="col-sm-6">
  
  
  <asp:TextBox ID="txtemail" runat="server" type="email" class="form-control" placeholder="Email ID"></asp:TextBox>
  
  
  </div>
  </div>
  
  
  <div class="form-group">
  <label for="cbMobileLogin1" class="col-sm-2 control-label">Mobile Login :</label>
  <div class="col-sm-10">
  
  <asp:CheckBox ID="cbMobileLogin" runat="server" Checked="true" type="checkbox" />
  
  </div>
  </div>
  
  
<div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
      <button type="submit" class="btn btn-default">Create</button>
    </div>
</div>
  
  
<div class="form-group">
<div class="col-sm-10">
<asp:Label ID="lblErrMsg" runat="server" Visible="False"></asp:Label>
</div>
</div>
  
  
</div>
</asp:Content>

