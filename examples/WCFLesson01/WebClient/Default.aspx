<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="msg"></asp:TextBox>
        <asp:Button runat="server" ID="button" Text="Send" OnClick="button_Click" />
        <br />
        <asp:Label runat="server" ID="answer" />
    </div>
    </form>
</body>
</html>
