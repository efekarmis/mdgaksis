<%@ Page Title="Ders Mesajları" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgretmenMesajlasma.aspx.cs" Inherits="YazOkulu.OgretmenMesajlasma" %>

<%-- Inherits ve CodeBehind'ı kontrol edin --%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%-- Ana Mesajlaşma Konteyneri (İki Sütunlu) --%>
    <%-- MasterPage'de tek form olduğunu varsayıyoruz, burada form EKLENMİYOR --%>
    <div class="mesajlasma-container shadow-sm border rounded">

        <%-- Sol Sütun: Ders Listesi --%>
        <div class="ders-listesi-panel">
            <div class="ders-liste-baslik p-3 border-bottom">
                <h5 class="mb-0 fw-semibold"><span class="glyphicon glyphicon-list-alt me-2"></span>Derslerim</h5>
            </div>
            <div class="ders-liste-icerik" style="overflow-y: auto;">
                <asp:Repeater ID="rptDersListesiSolMenu" runat="server" OnItemCommand="rptDersListesiSolMenu_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDersSec" runat="server"
                            CssClass='<%# "ders-list-item p-3 border-bottom " + (Eval("ID").ToString() == hfSeciliDersId.Value ? "active" : "") %>'
                            CommandName="DersSec" CommandArgument='<%# Eval("ID") %>'
                            ToolTip='<%# Eval("DERSAD") + " mesajlarını gör" %>'
                            CausesValidation="false"> <%-- Validatorları tetiklemez --%>
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1">
                                    <h6 class="mb-0 fw-semibold ders-list-ad"><%# Eval("DERSAD") %></h6>
                                </div>
                            </div>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <% if (rptDersListesiSolMenu.Items.Count == 0)
                            { %>
                        <div class="p-3 text-center text-muted">Yönettiğiniz ders bulunmamaktadır.</div>
                        <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <%-- ders-listesi-panel sonu --%>

        <%-- Sağ Sütun: Mesajlaşma Alanı --%>
        <div class="mesajlasma-alani">

            <%-- Mesaj Alanı (Hata/Başarı - Sağ panelin en üstüne taşıdık) --%>
            <asp:PlaceHolder ID="phMesaj" runat="server" Visible="false">
                <div id="divMesaj" class="alert alert-dismissible m-3" runat="server" role="alert" style="border-radius: 0.3rem;">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Kapat" style="position: absolute; top: 0.5rem; right: 0.75rem; padding: 0.5rem;"><span aria-hidden="true">×</span></button>
                    <asp:Literal ID="litMesaj" runat="server"></asp:Literal>
                </div>
            </asp:PlaceHolder>


            <%-- Ders Seçilmediğinde Gösterilecek Panel --%>
            <asp:Panel ID="pnlDersSecimiBekleniyor" runat="server" CssClass="mesaj-alani-bos d-flex flex-column justify-content-center align-items-center h-100 text-center p-5" Visible="true">
                <span class="glyphicon glyphicon-comments display-1 text-muted mb-4" style="opacity: 0.2;"></span>
                <h4 class="text-muted fw-light">Lütfen soldaki menüden<br>
                    bir ders seçiniz.</h4>
            </asp:Panel>

            <%-- Ders Seçildiğinde Gösterilecek Panel --%>
            <asp:Panel ID="pnlMesajlasmaAlaniSag" runat="server" CssClass="mesaj-alani-dolu d-flex flex-column h-100" Visible="false">
                <%-- Üst Başlık Alanı --%>
                <div class="mesaj-baslik p-3 border-bottom d-flex align-items-center bg-white shadow-sm">
                    <h5 class="mb-0 fw-semibold flex-grow-1">
                        <asp:Literal ID="litSeciliDersAdi" runat="server">Ders Adı</asp:Literal></h5>
                </div>

                <%-- Mesaj Akışı --%>
                <div id="mesajAkisiContainer" clientidmode="Static" class="mesaj-akisi-container flex-grow-1 p-3" style="overflow-y: scroll;">
                    <asp:Repeater ID="rptMesajAkisi" runat="server">
                        <ItemTemplate>
                            <div class='message-item mb-3 <%# Convert.ToInt32(Eval("GonderenKullaniciTipi")) == 1 ? "message-sent" : "message-received" %>'>
                                <asp:Image ID="imgGonderenFoto" runat="server"
                                    CssClass="message-sender-pic me-2 flex-shrink-0"
                                    ImageUrl='<%# ResolveUrl(GetGonderenFotoUrl(Eval("GonderenKullaniciID"), Eval("GonderenKullaniciTipi"))) %>'
                                    AlternateText='<%# GetGonderenAdi(Eval("GonderenKullaniciID"), Eval("GonderenKullaniciTipi")) %>' />

                                <div class="message-bubble flex-grow-1">
                                    <div class="message-sender-info">
                                        <strong class="message-sender">
                                            <%# GetGonderenAdi(Eval("GonderenKullaniciID"), Eval("GonderenKullaniciTipi")) %>
                                            <%# Convert.ToInt32(Eval("GonderenKullaniciTipi")) == 1 ? " <span class='glyphicon glyphicon-education text-primary' title='Öğretmen'></span>" : "" %>
                                        </strong>
                                        <span class="message-time text-muted ms-2">
                                            <%# Eval("GondermeTarihi", "{0:dd.MM.yy HH:mm}") %>
                                        </span>
                                    </div>
                                    <div class="message-content mt-1">
                                        <%# FormatMesajIcerik(Eval("MesajIcerik")) %>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- Mesaj Yok Paneli --%>
                    <asp:Panel ID="pnlMesajYok" runat="server" CssClass="text-center text-muted py-5 d-flex flex-column align-items-center justify-content-center h-100" Visible="false">
                        <span class="glyphicon glyphicon-inbox fs-2 mb-2" style="opacity: 0.4;"></span>
                        <span>Bu sohbette henüz mesaj yok.<br />
                            İlk mesajı siz gönderin!</span>
                    </asp:Panel>
                </div>

                <%-- Yeni Mesaj Gönderme Alanı (Panel içine alındı) --%>
                <div class="mesaj-gonderme-alani p-3 border-top bg-light">
                    <%-- DefaultButton özelliği eklendi --%>
                    <asp:Panel ID="pnlGonder" runat="server" DefaultButton="btnMesajGonder">
                        <div class="input-group">
                            <asp:TextBox ID="txtMesajIcerik" runat="server" CssClass="form-control form-control-lg rounded-pill me-2"
                                TextMode="SingleLine" placeholder="Mesajınızı yazın..." ValidationGroup="MesajGonderGrubu"
                                Style="border-right: none;"></asp:TextBox>
                            <asp:LinkButton ID="btnMesajGonder" runat="server" CssClass="btn btn-primary rounded-circle px-3"
                                OnClick="btnMesajGonder_Click" ToolTip="Gönder" ValidationGroup="MesajGonderGrubu">
                                <span class="glyphicon glyphicon-send"></span>
                            </asp:LinkButton>
                        </div>
                        <asp:RequiredFieldValidator ID="reqMesaj" runat="server" ControlToValidate="txtMesajIcerik" Display="None" ValidationGroup="MesajGonderGrubu"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mt-2 small" DisplayMode="List" HeaderText="" ValidationGroup="MesajGonderGrubu" />
                    </asp:Panel>
                    <%-- pnlGonder sonu --%>
                </div>
            </asp:Panel>
            <%-- pnlMesajlasmaAlaniSag sonu --%>
        </div>
        <%-- mesajlasma-alani sonu --%>

        <%-- Seçili Ders ID'sini saklamak için HiddenField --%>
        <asp:HiddenField ID="hfSeciliDersId" runat="server" Value="0" />

    </div>
    <%-- mesajlasma-container sonu --%>


    <%-- Özel Mesajlaşma Stilleri (styles.css'e taşınmalı) --%>
    

    <%-- Scroll Scripti --%>
    <script type="text/javascript">
        function scrollToBottom(containerId) {
            var div = document.getElementById(containerId);
            if (div) {
                // Scroll yapmadan önce kısa bir gecikme vermek render sorunlarını çözebilir
                setTimeout(function () { div.scrollTop = div.scrollHeight; }, 50);
            }
        }
        document.addEventListener('DOMContentLoaded', function () {
            var seciliDersIdField = document.getElementById('<%= hfSeciliDersId.ClientID %>');
            if (seciliDersIdField && seciliDersIdField.value && seciliDersIdField.value !== "0") {
                // Sayfa yüklendiğinde scroll et
                scrollToBottom('mesajAkisiContainer');
            }
        });
        // AJAX kullanıyorsanız (UpdatePanel vb.), bu kısım gereklidir.
        if (typeof (Sys) !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            // AJAX isteği bittikten sonra scroll et
            prm.add_endRequest(function (s, e) {
                setTimeout(function () { scrollToBottom('mesajAkisiContainer'); }, 150); // AJAX sonrası biraz daha gecikme
            });
        }
    </script>
</asp:Content>
