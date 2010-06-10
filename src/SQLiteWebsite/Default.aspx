<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid;">
        <asp:Button Text="English (US)" runat="server" OnClick="English_Click" />
        Will retrieve value from en-US
    </div>
    <div style="border: 1px solid;">
        <asp:Button Text="French" runat="server" OnClick="French_Click" />
        Since fr-FR is not defined, it will fallback and retrieve value from fr.
    </div>
    <div style="border: 1px solid;">
        <asp:Button ID="Button1" Text="Nepali" runat="server" OnClick="Nepali_Click" />
        Since ne-NP and ne both doesn't exisit. it will fall back to default, where the
        culture name is empty.
    </div>
    <div style="border: 1px solid;">
        Global resource example:
        <asp:Label Text='<%$ Resources:Messages, Color %>' runat="server" />
    </div>
     <div style="border: 1px solid;">
        Global resource example:
        <asp:Label ID="lblHello" meta:resourcekey="lblHello" runat="server" />
    </div>
    <p>
    </p>
    <asp:Button ID="Button2" Text="Clear Resource Cache" runat="server" OnClick="ClearResourceCache_Click" />
    </form>
</body>
</html>
