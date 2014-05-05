<%@ Page Language="C#" AutoEventWireup="true" CodeFile="smsmaster.aspx.cs" Inherits="smsmaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
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
        <asp:GridView ID="gvActiveSMS" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvActiveSMS_RowCommand" OnRowDataBound="gvActiveSMS_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SMSText" HeaderText="SMS" />
                 <asp:TemplateField HeaderText="Active SMS">
          <ItemTemplate>
                   <asp:LinkButton ID="lnkActivated" runat="server" CommandArgument='<%# Eval("incrid") %>'
                     CommandName="Active">
                                 Click to deactivate</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>                
            </Columns>
        </asp:GridView>
        <br /><br />
        Deactive SMS
        <asp:GridView ID="gvDeactiveSMS" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvDeactiveSMS_RowCommand" OnRowDataBound="gvDeactiveSMS_RowDataBound">
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
    </form>
</body>
</html>
