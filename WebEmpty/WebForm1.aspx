<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebEmpty.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        #title1 {
            text-align : center;
        }
        body {
            background-image: url(https://www.searchtechnologies.com/images/logos/logo-SearchTechnologies-PNG.png);
            background-repeat: no-repeat;
            background-position: right top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1 id="title1">Welcome to SearchTheNews</h1>
        <p id ="keyword1">Masukkan keyword :&nbsp;  
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p id ="algo1">Algoritma :
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem>Boyer Moore</asp:ListItem>
                <asp:ListItem>Knuth Morris Pratt</asp:ListItem>
                <asp:ListItem>Regular Expression</asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Cari Berita" />
        </p>
        <p>
            Hasil Pencarian :
        </p>

        <div runat="server" id="myDiv" />
        


    </form>
    <p>
        &nbsp;</p>
</body>
</html>
