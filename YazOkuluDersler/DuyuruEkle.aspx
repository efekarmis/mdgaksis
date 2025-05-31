<%@ Page Title="Yeni Duyuru Ekle" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DuyuruEkle.aspx.cs" Inherits="YazOkulu.DuyuruEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Yeni Duyuru Ekle</h2>
        <p class="text-muted">Sisteme yeni bir duyuru ekleyin.</p>
        <hr />

        <div class="card card-common" style="max-width: 700px; margin: 20px auto;">
            <div class="card-body">
                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtBaslik" runat="server" CssClass="form-label fw-semibold">Başlık:</asp:Label>
                    <asp:TextBox ID="txtBaslik" runat="server" CssClass="form-control" MaxLength="200" placeholder="Duyurunun başlığını girin..." required="required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqBaslik" runat="server" ControlToValidate="txtBaslik" ErrorMessage="Başlık boş olamaz." Display="Dynamic" CssClass="invalid-feedback">* Başlık zorunludur.</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtIcerik" runat="server" CssClass="form-label fw-semibold">İçerik:</asp:Label>
                    <asp:TextBox ID="txtIcerik" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Duyurunun detaylarını yazın..." required="required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqIcerik" runat="server" ControlToValidate="txtIcerik" ErrorMessage="İçerik boş olamaz." Display="Dynamic" CssClass="invalid-feedback">* İçerik zorunludur.</asp:RequiredFieldValidator>
                </div>
                <div class="row mb-3 align-items-center">
                    <div class="col-md-6">
                        <asp:Label AssociatedControlID="ddlHedefKitle" runat="server" CssClass="form-label fw-semibold">Hedef Kitle:</asp:Label>
                        <asp:DropDownList ID="ddlHedefKitle" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Öğrenci" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Öğretmen" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Herkes" Value="2" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <asp:Label runat="server" CssClass="form-label fw-semibold d-block mb-2">Önem Derecesi:</asp:Label>
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rbNormal" runat="server" Text="" GroupName="OnemDerecesi" Checked="true" CssClass="form-check-input" />
                            <label class="form-check-label" for="<%= rbNormal.ClientID %>">Normal</label>
                        </div>
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rbOnemli" runat="server" Text="" GroupName="OnemDerecesi" CssClass="form-check-input" />
                            <label class="form-check-label" for="<%= rbOnemli.ClientID %>">Önemli</label>
                        </div>
                    </div>
                </div>

                <div class="mb-3 text-center">
                    <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
                </div>

                <div class="text-end border-top pt-3 mt-2">
                    <asp:Button ID="btnDuyuruEkle" runat="server" Text="Duyuruyu Yayınla" CssClass="btn btn-primary me-2" OnClick="btnDuyuruEkle_Click" />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DuyuruYonetimi.aspx" CssClass="btn btn-secondary">
                            İptal / Geri Dön
                    </asp:HyperLink>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger mt-3" HeaderText="Lütfen formdaki hataları düzeltin:" />
            </div>
        </div>
    </div>
</asp:Content>
