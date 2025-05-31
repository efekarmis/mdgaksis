<%@ Page Title="Tüm Duyurular" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciDuyurular.aspx.cs" Inherits="YazOkulu.OgrenciDuyurular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <div class="duyuru-baslik">
            <h2><span class="glyphicon glyphicon-bullhorn text-primary me-2"></span>Tüm Duyurular</h2>
            <p class="text-muted">Geçmiş ve güncel tüm duyuruları buradan takip edebilirsiniz.</p>
            <hr />
        </div>
        <asp:PlaceHolder ID="phDuyuruYok" runat="server" Visible="false">
            <div class="alert alert-info text-center">
                <span class="glyphicon glyphicon-info-sign fs-3 d-block mb-2"></span>
                Gösterilecek duyuru bulunmamaktadır.
            </div>
        </asp:PlaceHolder>
        <div class="duyuru-item-container">
            <asp:Repeater ID="rptTumDuyurular" runat="server">
                <ItemTemplate>
                    <div class='card card-common mb-3 shadow-sm duyuru-item-detay <%# (Convert.ToInt16(Eval("OnemDerecesi")) == 1) ? "duyuru-onemli-detay" : "" %>'>
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-start mb-1">
                                    <h4 class="card-title duyuru-baslik"><%# Eval("Baslik") %></h4>
                                    <%# (Convert.ToInt16(Eval("OnemDerecesi")) == 1 ? "<span class='badge rounded-pill bg-danger ms-2'>ÖNEMLİ</span>" : "") %>
                                </div>

                                <div class="text-muted duyuru-meta mb-3 pb-2 border-bottom">
                                    <span class="glyphicon glyphicon-time me-1"></span>
                                    <span class="me-3"><%# Eval("YayinTarihi", "{0:dd.MM.yyyy HH:mm}") %></span>
                                </div>

                                <div class="duyuru-icerik-tam">
                                    <%# FormatContent(Eval("Icerik")) %>
                                </div>
                            </div>
                        </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

