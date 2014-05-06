<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="Editdetail.aspx.cs" Inherits="Editdetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
    
    <div class="container">  
    
    <div class="form-group">
    <label for="ddlistUsertype" class="col-sm-2 control-label">User Type</label>
     <div class="col-sm-6">

                
                <asp:DropDownList ID="ddlistUsertype" runat="server"  class="form-control">
                </asp:DropDownList>
     </div>
  </div>
  <br/><br/>
  
    
  <div class="form-group">
    <label for="lblUerID" class="col-sm-2 control-label">User ID :</label>
    <div class="col-sm-6">
      <asp:Label ID="lblUerID" runat="server" type="text" class="form-control" placeholder="UserID"></asp:Label>
    </div>
  </div>
  <br/><br/>

    <div class="form-group">
  <label for="txtmobileno" class="col-sm-2 control-label">Mobile No. :</label>
  <div class="col-sm-6">

  <asp:TextBox ID="txtmobileno" runat="server" MaxLength="15" type="number" class="form-control" placeholder="Mobile No."></asp:TextBox>
  
  </div>
  </div>
  <br/><br/>

   <div class="form-group">
  <label for="txtfirstname" class="col-sm-2 control-label">First Name :</label>
  <div class="col-sm-6">

  <asp:TextBox ID="txtfirstname" runat="server" MaxLength="25" type="text" class="form-control" placeholder="First Name"></asp:TextBox>
  
  </div>
  </div>
  <br/><br/>
  <div class="form-group">
  <label for="txtlastname" class="col-sm-2 control-label">Last Name :</label>
  <div class="col-sm-6">
  <asp:TextBox ID="txtlastname" runat="server" MaxLength="25" type="text" class="form-control" placeholder="Last Name"></asp:TextBox>
  
  </div>
  </div>
  <br/><br/>

    <div class="form-group">
  <label for="txtDOJ" class="col-sm-2 control-label">Date of Birth :</label>
  <div class="col-sm-6">

   <asp:TextBox ID="txtDOJ" runat="server" MaxLength="25" type="number" class="form-control" placeholder="Date of Birth"></asp:TextBox>

                <cc1:CalendarExtender ID="ceFrom" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDOJ">
                </cc1:CalendarExtender>
  </div>
  </div>
  
  <br/><br/>

 <div class="form-group">
  <label for="txtemail" class="col-sm-2 control-label">Email ID :</label>
  <div class="col-sm-6">
  <asp:TextBox ID="txtemail" runat="server" type="email" class="form-control" placeholder="Email ID"></asp:TextBox>
  </div>
  </div>
  
        <br/><br/>

 <div class="form-group">
  <label for="cbMobileLogin" class="col-sm-2 control-label">Mobile Login :</label>
  <div class="col-sm-6">
  <asp:CheckBox ID="cbMobileLogin" runat="server" Checked="true" type="checkbox" />
  
  </div>
  </div>


        <br/><br/>
  
  
<div class="form-group">
  <div class="col-sm-offset-2 col-sm-6">
   
      <div class="col-sm-6">
        <asp:Button ID="btncreate" runat="server" Text="Edit" OnClick="btncreate_Click" Checked="true"  type="submit" class="btn btn-default"/>
 
</div>
    </div>
</div>
  
  <br/><br/>

<div class="form-group">
<div class="col-sm-10">
<asp:Label ID="lblErrMsg" runat="server" Visible="False"></asp:Label>
</div>
</div>
  
  
</div>
    

    
    
</asp:Content>

