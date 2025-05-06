<%@ Page Title="Profilim" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgretmenProfil.aspx.cs" Inherits="YazOkulu.OgretmenProfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="container-fluid">
            <div class="profile-view-card card-common">
                <h2>Öğretmen Profil Bilgileri</h2>
                <hr />
                <div class="profile-layout">
                    <div class="profile-photo-area">
                        <asp:Image ID="imgProfilFoto" runat="server" Width="180px" Height="180px"
                                   CssClass="profile-view-photo"
                                   ImageUrl="~/OgretmenFotograflari/default_teacher.png"
                                   AlternateText="Profil Fotoğrafı" />
                    </div>
                    <div class="profile-details-area">
                        <table class="table profile-info-table">
                             <tr>
                                <td class="profile-label">Ad Soyad:</td>
                                <td class="profile-value"><asp:Label ID="lblAdSoyad" runat="server" Text="-"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="profile-label">Öğretmen ID:</td>
                                <td class="profile-value"><asp:Label ID="lblOgretmenID" runat="server" Text="-"></asp:Label></td>
                            </tr>
                             <tr>
                                <td class="profile-label">Şifre:</td>
                                <td class="profile-value">********</td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="profile-actions">
                    <asp:Button ID="btnDuzenle" runat="server" Text="Profili Düzenle" CssClass="btn btn-primary" OnClick="btnDuzenle_Click" />
                    <asp:HyperLink ID="hlAnaSayfa" runat="server" NavigateUrl="~/OgretmenAnaSayfa.aspx" Text="Ana Sayfaya Dön" CssClass="btn btn-secondary" />
                </div>
            </div>
        </div>
</asp:Content>