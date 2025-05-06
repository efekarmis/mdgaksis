<%@ Page Title="Profili Düzenle" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciProfilDuzenle.aspx.cs" Inherits="YazOkulu.OgrenciProfilDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <div class="profile-edit-card card-common">
            <h2>Profili Düzenle</h2>
            <hr />

            <div class="text-center mb-3">
                <asp:Image ID="imgMevcutFoto" runat="server" Height="150px" Width="150px" CssClass="profile-edit-photo" ImageUrl="~/OgrenciFotograflari/default.png" />
            </div>
            <div class="form-group">
                <label for="FileUploadControl">Yeni Fotoğraf Yükle (Değiştirmek istemiyorsanız boş bırakın):</label>
                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:CheckBox ID="chkFotoKaldir" runat="server" Text=" Mevcut Fotoğrafı Kaldır" />
            </div>

            <hr />

            <div class="form-group">
                <label for="TxtAd">Ad:</label>
                <asp:TextBox ID="TxtAd" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtAd" ErrorMessage="Ad alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="TxtSoyad">Soyad:</label>
                <asp:TextBox ID="TxtSoyad" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtSoyad" ErrorMessage="Soyad alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="TxtNumara">Öğrenci Numarası:</label>
                <asp:TextBox ID="TxtNumara" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtNumara" ErrorMessage="Numara alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="TxtSifre">Yeni Şifre (Değiştirmek istemiyorsanız boş bırakın):</label>
                <asp:TextBox ID="TxtSifre" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="mt-4 text-center">
                <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
            </div>

            <div class="profile-actions">
                <asp:Button ID="btnKaydet" runat="server" Text="Değişiklikleri Kaydet" CssClass="btn btn-success" OnClick="btnKaydet_Click" />
                <asp:Button ID="btnIptal" runat="server" Text="İptal / Geri Dön" CssClass="btn btn-secondary" OnClick="btnIptal_Click" CausesValidation="false" />
            </div>

            <asp:HiddenField ID="hdnOgrenciID" runat="server" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger mt-3" HeaderText="Lütfen formdaki hataları düzeltin:" ShowMessageBox="true" ShowSummary="false" />

        </div>
    </div>
</asp:Content>
