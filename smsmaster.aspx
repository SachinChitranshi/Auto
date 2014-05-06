<%@ Page Title="" Language="C#" MasterPageFile="~/settings.master" AutoEventWireup="true" CodeFile="smsmaster.aspx.cs" Inherits="smsmaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div>
    
        <table class="table table-striped">
            <tr>
                <td>
                    <asp:FileUpload ID="fuSMS" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkSample" runat="server" OnClick="lnkSample_Click">Download sample file</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                   
                </td>
            </tr>
        </table>
    
    </div>
         <asp:Label ID="lblErrMsg" runat="server" Visible="false"></asp:Label>
        <br />
        <table>
            <tr>
                <td>SMS Text</td>
                <td>
                    <asp:TextBox ID="txtSMSText" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <br />
        Active SMS
        <asp:GridView ID="gvActiveSMS" runat="server" class="table table-striped" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvActiveSMS_RowCommand" OnRowDataBound="gvActiveSMS_RowDataBound" OnRowCancelingEdit="gvActiveSMS_RowCancelingEdit" OnRowEditing="gvActiveSMS_RowEditing" OnRowUpdating="gvActiveSMS_RowUpdating">
            <Columns>
                
                   <asp:TemplateField  HeaderText="SMS Text">
                        <EditItemTemplate>
                        <asp:TextBox ID="txtSMSText" runat="server" Text='<%# Eval("SMSText") %>'></asp:TextBox>
                        </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSMSText" runat="server" Text='<%# Eval("SMSText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                 <asp:TemplateField HeaderText="Active SMS">
          <ItemTemplate>
                   <asp:LinkButton ID="lnkActivated" runat="server" CommandArgument='<%# Eval("incrid") %>'
                     CommandName="Active">
                                 Click to deactivate</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="Edit" ShowHeader="False">
               <ItemTemplate>
  <asp:LinkButton ID="btnedit" runat="server" CommandName="Edit" Text="Edit" ></asp:LinkButton>
               </ItemTemplate>
               <EditItemTemplate>
     <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" ></asp:LinkButton>
     <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
               </EditItemTemplate>
            </asp:TemplateField>   
                
                 <asp:TemplateField  HeaderText="incrid" Visible="false">                        
                            <ItemTemplate>
                                <asp:Label ID="lblIncrId" runat="server" Text='<%# Eval("incrid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                              
            </Columns>
        </asp:GridView>
        <br /><br />
        Deactive SMS
        <asp:GridView ID="gvDeactiveSMS" runat="server" class="table table-striped" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvDeactiveSMS_RowCommand" OnRowDataBound="gvDeactiveSMS_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SMSText" HeaderText="SMS" />
                 <asp:TemplateField HeaderText="Deactive SMS">
          <ItemTemplate>
                   <asp:LinkButton ID="lnkDeactivated" runat="server" CommandArgument='<%# Eval("incrid") %>'
                     CommandName="Deactive">
                                 Click to activate</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>