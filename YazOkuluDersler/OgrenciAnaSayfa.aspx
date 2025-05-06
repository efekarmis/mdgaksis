<%@ Page Title="Öğrenci Ana Sayfası" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciAnaSayfa.aspx.cs" Inherits="YazOkulu.OgrenciAnaSayfa" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="studentDashboard" class="dashboard-container">
        <div id="profileCardActions" class="col-md-5">
            <div id="profileCard" class="profile-card" runat="server">
                <asp:Image ID="imgOgrenciFoto" runat="server"
                           CssClass="profile-photo"
                           Width="120px"
                           Height="120px"
                           AlternateText="Öğrenci Fotoğrafı"
                           ImageUrl="~/OgrenciFotograflari/default.png" />

                <div id="profileDetails" class="profile-text-details" runat="server">
                </div>
            </div>
            <div id="dashboardActions" class="dashboard-actions">
                <div class="dashboard-buttons">
                    <a href="DersTalep.aspx" class="dashboard-button">Ders Talep</a>
                    <a href="OgrenciDerslerim.aspx" class="dashboard-button">Derslerim</a>
                    <a href="BakiyeYukle.aspx" class="dashboard-button">Bakiye Yükle</a>
                    <a href="OgrenciProfil.aspx" class="dashboard-button">Profilim</a>
                    <asp:HyperLink ID="hlProfilDuzenle" runat="server" CssClass="dashboard-button btn-secondary">Profili Düzenle</asp:HyperLink>
                </div>
            </div>
        </div>
        

        <div class="card card-common col-md-7 shadow-sm">
            <div class="card-header bg-light border-bottom">
                <h4 class="mb-0 fw-semibold">
                    <span style="color: var(--app-navbar-secondarycolor)" class="glyphicon glyphicon-bullhorn text-primary mr-2"></span>
                    Güncel Duyurular
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

        
    </div>
 
</asp:Content>
