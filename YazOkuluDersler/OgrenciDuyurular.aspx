<%@ Page Title="Tüm Duyurular" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciDuyurular.aspx.cs" Inherits="YazOkulu.OgrenciDuyurular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2><span class="glyphicon glyphicon-bullhorn text-primary me-2"></span>Tüm Duyurular</h2>
        <p class="text-muted">Geçmiş ve güncel tüm duyuruları buradan takip edebilirsiniz.</p>
        <hr />

        <asp:PlaceHolder ID="phDuyuruYok" runat="server" Visible="false">
            <div class="alert alert-info text-center">
                <span class="glyphicon glyphicon-info-sign fs-3 d-block mb-2"></span>
                Gösterilecek duyuru bulunmamaktadır.
            </div>
        </asp:PlaceHolder>

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

    <style>
        .duyuru-item-detay {
            position: relative;
            border-left: 5px solid #adb5bd; /* Varsayılan gri kenar */
            transition: box-shadow 0.2s ease-in-out;
        }

            .duyuru-item-detay:hover {
                box-shadow: 0 .5rem 1rem rgba(0,0,0,.10) !important;
            }

        .duyuru-onemli-detay {
            border-left-color: #dc3545 !important; /* Önemli için kırmızı kenar */
        }

        .duyuru-meta {
            font-size: 0.9em;
        }

        .duyuru-icerik-tam {
            line-height: 1.7;
            font-size: 1rem;
        }
    </style>
</asp:Content>
