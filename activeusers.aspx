<%@ Page Title="" Language="C#" MasterPageFile="~/manageuser.master" AutoEventWireup="true" CodeFile="activeusers.aspx.cs" Inherits="activeusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <asp:Label ID="lblErrMsg" runat="server" Visible="false"></asp:Label>
    </p>
    <p>
        <strong>Active Users</strong></p>
   
        <asp:GridView ID="gvActivatedUsers" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="gvActivatedUsers_RowCommand" OnRowDataBound="gvActivatedUsers_RowDataBound">
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                <asp:BoundField DataField="LoginName" HeaderText="Login Name" />
                <asp:BoundField DataField="FirstName" HeaderText="EmailId" />
                <asp:BoundField DataField="FirstName" HeaderText="MobileNo" />
                 <asp:TemplateField HeaderText="Activated Users">
          <ItemTemplate>
                   <asp:LinkButton ID="lnkActivated" runat="server" CommandArgument='<%# Eval("incrid") %>'
                     CommandName="Active">
                                 Click to deactivate</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                <asp:TemplateField HeaderText="View Detail">
        <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl= '<%# "Editdetail.aspx?uid=" +Eval("loginname")+ "&slno=" +Eval("incrid") %>'
        Text="View detail">
        </asp:HyperLink>
       </ItemTemplate>
    </asp:TemplateField>
            </Columns>
        </asp:GridView>
  <br />
    <div align="right">
        </div>
    <p>
        <asp:Label ID="lblDeactivatedUsers" runat="server" style="font-weight: 700" Text="Deactivated Users"></asp:Label>
    </p>
    <asp:GridView ID="gvDeActivatedUsers" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true" AutoGenerateColumns="False" OnRowCommand="gvDeActivatedUsers_RowCommand" OnRowDataBound="gvDeActivatedUsers_RowDataBound">
         <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                <asp:BoundField DataField="LoginName" HeaderText="Login Name" />
                <asp:BoundField DataField="FirstName" HeaderText="EmailId" />
                <asp:BoundField DataField="FirstName" HeaderText="MobileNo" />
             <asp:TemplateField HeaderText="Activated Users">
          <ItemTemplate>
                   <asp:LinkButton ID="lnkDeActivated" runat="server" CommandArgument='<%# Eval("incrid") %>'
                     CommandName="DeActive">
                                 Click to activate</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
              <asp:TemplateField HeaderText="View Detail">
        <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl= '<%# "Editdetail.aspx?uid=" +Eval("loginname")+ "&slno=" +Eval("incrid") %>'
        Text="View detail">
        </asp:HyperLink>
       </ItemTemplate>
    </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <br />
    <div align="right">
        </div>
</asp:Content>

