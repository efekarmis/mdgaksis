<%@ Page Title="Öğretmen Ana Sayfası" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgretmenAnaSayfa.aspx.cs" Inherits="YazOkulu.OgretmenAnaSayfa" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="teacherDashboard" class="dashboard-container">

        <div id="profileCard" class="profile-card" runat="server">
            <asp:Image ID="imgOgretmenFoto" runat="server"
                CssClass="profile-photo"
                Width="120px"
                Height="120px"
                AlternateText="Öğretmen Fotoğrafı"
                ImageUrl="~/OgretmenFotograflari/default_teacher.png" />

            <div id="profileDetails" class="profile-text-details" runat="server">
            </div>
        </div>

        <div class="card card-common mb-4 shadow-sm">
            <div class="card-header bg-light border-bottom">
                <h4 class="mb-0 fw-semibold">
                    <span class="glyphicon glyphicon-bullhorn text-primary me-2"></span>Güncel Duyurular
                </h4>
            </div>
            <div class="card-body announcement-list-body">
                <asp:Repeater ID="rptDuyurular" runat="server">
                    <ItemTemplate>
                        <div class='duyuru-item <%# (Convert.ToInt16(Eval("OnemDerecesi")) == 1) ? "duyuru-onemli" : "" %>'
                            style="padding: 1rem 1.25rem; border-bottom: 1px solid #eee;">

                            <div class="d-flex justify-content-between align-items-start mb-1">
                                <h5 class="mb-0 duyuru-baslik"><%# Eval("Baslik") %></h5>
                            </div>

                            <div class="d-flex align-items-center duyuru-meta mb-2">
                                <span class="glyphicon glyphicon-time me-1" style="font-size: 0.9em;"></span>
                                <span style="font-size: 0.85em;"><%# Eval("YayinTarihi", "{0:dd.MM.yyyy HH:mm}") %></span>
                            </div>

                            <div class="duyuru-icerik" style="font-size: 0.95em; line-height: 1.6;">
                                <%# LimitContent(Eval("Icerik"), 200) %>
                                <%# (Eval("Icerik").ToString().Length > 200) ? "<a href='#' class='read-more-link small'>(Devamı...)</a>" : "" %>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <% if (rptDuyurular.Items.Count == 0)
                            { %>
                        <div class="text-center text-muted p-4 duyuru-yok">
                            <span class="glyphicon glyphicon-info-sign d-block mb-2" style="font-size: 1.5rem; opacity: 0.6;"></span>
                            Gösterilecek güncel duyuru bulunmamaktadır.
                        </div>
                        <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>


        <div id="dashboardActions" class="dashboard-actions">
            <h3>Hızlı İşlemler</h3>
            <div class="dashboard-buttons">
                <a href="DersListesi.aspx" class="dashboard-button">Ders Listesi</a>
                <a href="OgretmenDerslerim.aspx" class="dashboard-button">Derslerim</a>
                <a href="DersKayit.aspx" class="dashboard-button">Ders Kaydı</a>
                <a href="DersAta.aspx" class="dashboard-button">Ders Atama</a>
                <a href="OgrenciListesi.aspx" class="dashboard-button">Öğrenci Listesi</a>
                <a href="BasvuruListesi.aspx" class="dashboard-button">Başvuru Listesi</a>
                <a href="OgretmenProfil.aspx" class="dashboard-button btn-secondary">Profilim</a>
            </div>
        </div>
    </div>
</asp:Content>
