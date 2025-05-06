<%@ Page Title="Yeni Öğrenci Kaydı" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciKayit.aspx.cs" Inherits="YazOkulu.OgrenciKayit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Yeni Öğrenci Kaydı</h2>
        <p class="text-muted">Sisteme yeni bir öğrenci kaydedin.</p>
        <hr />

        <div class="card card-common" style="max-width: 700px; margin: 20px auto;">
            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Label for="TextAd" runat="server" CssClass="form-label fw-semibold" Text="Öğrenci Adı:"></asp:Label>
                        <asp:TextBox ID="TextAd" runat="server" CssClass="form-control" placeholder="Öğrencinin adı" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextAd" ErrorMessage="Ad alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-6">
                        <asp:Label for="TxtSoyad" runat="server" CssClass="form-label fw-semibold" Text="Öğrenci Soyadı:"></asp:Label>
                        <asp:TextBox ID="TxtSoyad" runat="server" CssClass="form-control" placeholder="Öğrencinin soyadı" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtSoyad" ErrorMessage="Soyad alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="mb-3">
                    <asp:Label for="TxtNumara" runat="server" CssClass="form-label fw-semibold" Text="Öğrenci Numarası:"></asp:Label>
                    <asp:TextBox ID="TxtNumara" runat="server" CssClass="form-control" placeholder="Okul numarası" required="required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtNumara" ErrorMessage="Numara alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu</asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label for="TxtSifre" runat="server" CssClass="form-label fw-semibold" Text="Öğrenci Şifresi:"></asp:Label>
                    <asp:TextBox ID="TxtSifre" runat="server" CssClass="form-control" TextMode="Password" placeholder="Giriş için şifre belirleyin" required="required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtSifre" ErrorMessage="Şifre alanı boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu</asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label for="FileUploadControl" runat="server" CssClass="form-label fw-semibold" Text="Öğrenci Fotoğrafı (İsteğe Bağlı):"></asp:Label>
                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3 text-center">
                    <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
                </div>

                <div class="text-center border-top pt-3 mt-3">
                    <asp:Button ID="Button1" runat="server" Text="Öğrenciyi Kaydet" OnClick="Button1_Click" CssClass="btn btn-success me-2" />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/OgrenciListesi.aspx" CssClass="btn btn-secondary">
                             İptal / Listeye Dön
                    </asp:HyperLink>
                </div>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger mt-3" HeaderText="Lütfen formdaki hataları düzeltin:" />

            </div>
        </div>
    </div>
</asp:Content>
