<%@ Page Title="Ders Bilgilerini Güncelle" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DersGuncelle.aspx.cs" Inherits="YazOkulu.DersGuncelle" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Ders Bilgilerini Güncelle</h2>
        <p class="text-muted">Seçili dersin bilgilerini düzenleyin.</p>
        <hr />

        <div class="card card-common" style="max-width: 700px; margin: 20px auto;">
            <div class="card-body">

                <div class="mb-3">
                    <asp:Label for="TxtDersAdi" runat="server" CssClass="form-label fw-semibold" Text="Ders Adı:"></asp:Label>
                    <asp:TextBox ID="TxtDersAdi" runat="server" CssClass="form-control" placeholder="Ders adını giriniz" required="required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtDersAdi" ErrorMessage="Ders Adı boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu Alan</asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label for="DrpOgretmenList" runat="server" CssClass="form-label fw-semibold" Text="Atanan Öğretmen:"></asp:Label>
                    <asp:DropDownList ID="DrpOgretmenList" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Öğretmen Seçiniz / Yok --" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Label for="TxtMinKontenjan" runat="server" CssClass="form-label fw-semibold" Text="Minimum Kontenjan:"></asp:Label>
                        <asp:TextBox ID="TxtMinKontenjan" runat="server" CssClass="form-control" placeholder="Min. kontenjan" TextMode="Number" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtMinKontenjan" ErrorMessage="Min Kontenjan boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtMinKontenjan" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Min Kontenjan sayı olmalıdır." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Sayı Girin</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="TxtMinKontenjan" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" ErrorMessage="Min Kontenjan 0 veya daha büyük olmalıdır." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* >= 0</asp:CompareValidator>
                    </div>
                    <div class="col-md-6">
                        <asp:Label for="TxtMaxKontenjan" runat="server" CssClass="form-label fw-semibold" Text="Maksimum Kontenjan:"></asp:Label>
                        <asp:TextBox ID="TxtMaxKontenjan" runat="server" CssClass="form-control" placeholder="Max. kontenjan" TextMode="Number" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtMaxKontenjan" ErrorMessage="Max Kontenjan boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TxtMaxKontenjan" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Max Kontenjan sayı olmalıdır." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Sayı Girin</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="TxtMaxKontenjan" ControlToCompare="TxtMinKontenjan" Operator="GreaterThanEqual" Type="Integer" ErrorMessage="Max Kontenjan, Min Kontenjan'dan küçük olamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* >= Min</asp:CompareValidator>
                    </div>
                </div>

                <div class="mb-3">
                    <asp:Label for="TxtDersUcret" runat="server" CssClass="form-label fw-semibold" Text="Ders Ücreti (TL):"></asp:Label>
                    <asp:TextBox ID="TxtDersUcret" runat="server" CssClass="form-control" placeholder="Örn: 150,00 veya 150,50 veya 0" required="required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUcret" runat="server" ControlToValidate="TxtDersUcret" ErrorMessage="Ders Ücreti boş bırakılamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Zorunlu (Ücretsizse 0 girin)</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorUcretType" runat="server" ControlToValidate="TxtDersUcret" Operator="DataTypeCheck" Type="Currency" ErrorMessage="Geçerli bir ücret formatı girin (örn: 150 veya 150,50)." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Geçerli Sayı Girin</asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidatorUcretValue" runat="server" ControlToValidate="TxtDersUcret" Operator="GreaterThanEqual" Type="Currency" ValueToCompare="0" ErrorMessage="Ders Ücreti negatif olamaz." Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* >= 0</asp:CompareValidator>
                </div>

                <div class="mb-3">
                    <asp:Label for="TxtDersAciklama" runat="server" CssClass="form-label fw-semibold" Text="Ders Açıklaması (İsteğe Bağlı):"></asp:Label>
                    <asp:TextBox ID="TxtDersAciklama" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Dersin içeriği, hedefleri vb. hakkında bilgi giriniz..."></asp:TextBox>
                </div>

                <div class="mb-3 text-center">
                    <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
                </div>

                <div class="text-center border-top pt-3 mt-3">
                    <asp:Button ID="Button1" runat="server" Text="Değişiklikleri Kaydet" OnClick="ButtonG_Click" CssClass="btn btn-primary me-2" />
                    <asp:HyperLink ID="hlGeri" runat="server" NavigateUrl="~/DersListesi.aspx" CssClass="btn btn-secondary">
                            İptal / Geri Dön
                    </asp:HyperLink>
                </div>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger mt-3" HeaderText="Lütfen formdaki hataları düzeltin:" />

                <asp:HiddenField ID="hdnDersID" runat="server" />

            </div>
        </div>
    </div>
</asp:Content>
