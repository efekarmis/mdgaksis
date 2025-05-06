<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YazOkulu.Login" %>

<!DOCTYPE html>
<html lang="tr">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Yaz Okulu - Giriş</title>
    <link rel="stylesheet" type="text/css" href="login-style.css" />
</head>
<body class="login-page-body">
    <form id="form1" runat="server">

        <div class="container" id="container">

            <div class="form-container sign-up-container">
                <h1>Öğretmen Girişi</h1>
                <asp:TextBox ID="txtOgretmenId" runat="server" placeholder="Id" CssClass="login-input"></asp:TextBox>
                <asp:TextBox ID="txtOgretmenSifre" runat="server" placeholder="Şifre" TextMode="Password" CssClass="login-input"></asp:TextBox>
                <asp:Literal ID="litOgretmenError" runat="server" EnableViewState="false"></asp:Literal>
                <asp:Button ID="btnOgretmenGiris" runat="server" Text="Öğretmen Giriş" OnClick="btnOgretmenGiris_Click" CssClass="login-button" />
            </div>

            <div class="form-container sign-in-container">
                <h1>Öğrenci Girişi</h1>
                <asp:TextBox ID="txtOgrenciId" runat="server" placeholder="Id" CssClass="login-input" type="text"></asp:TextBox>
                <asp:TextBox ID="txtOgrenciSifre" runat="server" placeholder="Şifre" TextMode="Password" CssClass="login-input"></asp:TextBox>
                <asp:Literal ID="litOgrenciError" runat="server" EnableViewState="false"></asp:Literal>
                <asp:Button ID="btnOgrenciGiris" runat="server" Text="Öğrenci Giriş" OnClick="btnOgrenciGiris_Click" CssClass="login-button" />
            </div>

            <div class="overlay-container">
                <div class="overlay">
                    <div class="overlay-panel overlay-left">
                        <h1>Öğrenci Girişi</h1>
                        <p>Buradan Öğrenci Girişine Geçiş Yapabilirsiniz!</p>
                        <button type="button" class="ghost" id="ogrenciGirisiGecis">Geçiş Yap</button>
                    </div>
                    <div class="overlay-panel overlay-right">
                        <h1>Öğretmen Girişi</h1>
                        <p>Buradan Öğretmen Girişine Geçiş Yapabilirsiniz!</p>
                        <button type="button" class="ghost" id="ogretmenGirisiGecis">Geçiş Yap</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hfPanelState" runat="server" Value="sign-in" />

    </form>

    <script src="Scripts/login-animation.js"></script>

    <script type="text/javascript">
        var hfPanelStateClientID = '<%= hfPanelState.ClientID %>';
    </script>

</body>
</html>
