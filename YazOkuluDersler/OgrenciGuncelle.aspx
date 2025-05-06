<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.Master" CodeBehind="OgrenciGuncelle.aspx.cs" Inherits="YazOkulu.OgrenciGuncelle" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="mb-3 text-center">
        <asp:Image ID="imgMevcutFoto" runat="server" Height="150px" Width="150px" CssClass="img-thumbnail" ImageUrl="~/OgrenciFotograflari/default.png" />
    </div>
    <br />

    <div class="form-group">
        <div class="mb-3">
            <asp:Label for="TxtAd" runat="server" class="form-label" Text="Öğrenci Adı:"></asp:Label>
            <asp:TextBox ID="TxtAd" runat="server" CssClass="form-control" placeholder="Adınızı giriniz"></asp:TextBox>
        </div>
        <br />
        <div class="mb-3">
            <asp:Label for="TxtSoyad" runat="server" class="form-label" Text="Öğrenci Soyadı:"></asp:Label>
            <asp:TextBox ID="TxtSoyad" runat="server" CssClass="form-control" placeholder="Soyadınızı giriniz"></asp:TextBox>
        </div>
        <br />
        <div class="mb-3">
            <asp:Label for="TxtNumara" runat="server" class="form-label" Text="Öğrenci Numarası:"></asp:Label>
            <asp:TextBox ID="TxtNumara" runat="server" CssClass="form-control" placeholder="Numaranızı giriniz"></asp:TextBox>
        </div>
        <br />
        <div class="mb-3">
            <asp:Label for="TxtSifre" runat="server" class="form-label" Text="Öğrenci Şifresi:"></asp:Label>
            <asp:TextBox ID="TxtSifre" runat="server" CssClass="form-control" placeholder="Yeni şifre (değişmeyecekse boş bırakın)"></asp:TextBox>
        </div>
        <br />
        <div class="mb-3">
            <asp:Label for="FileUploadControl" runat="server" class="form-label" Text="Yeni Fotoğraf Yükle (İsteğe Bağlı):"></asp:Label>
            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="form-control" />
        </div>
        <br />
        <div class="mb-3">
            <asp:CheckBox ID="chkFotoKaldir" runat="server" Text=" Mevcut Fotoğrafı Kaldır" />
        </div>

    </div>
    <div class="text-center">
        <asp:Button ID="Button1" runat="server" Text="Güncelle" OnClick="ButtonG_Click" CssClass="btn btn-primary w-100" />
    </div>
    <asp:HiddenField ID="hdnOgrenciID" runat="server" />
</asp:Content>
