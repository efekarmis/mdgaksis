<%@ Page Title="Ders Detayı" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DersDetay.aspx.cs" Inherits="YazOkulu.DersDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <asp:Panel ID="pnlHata" runat="server" Visible="false">
            <div class="alert alert-danger text-center">
                <asp:Literal ID="litHata" runat="server"></asp:Literal>
            </div>
            <div class="text-center mt-2">
                <asp:HyperLink ID="hlHataGeri" runat="server" NavigateUrl="~/DersTalep.aspx" CssClass="btn btn-secondary">
                     <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"></span> Geri Dön
                </asp:HyperLink>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlDersBilgi" runat="server" Visible="false">
            <div class="card card-common mb-4">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center flex-wrap mb-2">
                        <h2 id="hDersAdi" runat="server" class="card-title mb-0">Ders Adı</h2>
                        <div>
                            <span class="badge bg-info me-1 fs-6" title="Minimum Kontenjan">Min:
                                <asp:Literal ID="litMinKontenjan" runat="server">0</asp:Literal></span>
                            <span class="badge bg-info me-1 fs-6" title="Maksimum Kontenjan">Max:
                                <asp:Literal ID="litMaxKontenjan" runat="server">0</asp:Literal></span>
                            <span class="badge bg-success fs-6" title="Ders Ücreti">Ücret:
                                <asp:Literal ID="litUcret" runat="server">0 TL</asp:Literal></span>
                        </div>
                    </div>
                    <hr class="mt-1 mb-3" />

                    <div class="d-flex align-items-center mb-3">
                        <asp:Image ID="imgOgretmenFoto" runat="server"
                            CssClass="rounded-circle me-3 shadow-sm"
                            Style="width: 60px; height: 60px; object-fit: cover; border: 2px solid #eee;"
                            Visible="false" />
                        <div>
                            <span class="text-muted d-block mb-1" style="font-size: 0.85em;">Öğretmen</span>
                            <h5 class="mb-0">
                                <asp:HyperLink ID="hlOgretmen" runat="server" ToolTip="Öğretmen detaylarını gör" Visible="false">
                                    <asp:Literal ID="litOgretmen" runat="server"></asp:Literal>
                                </asp:HyperLink>
                                <span id="spanOgretmenAtanmamis" runat="server" class="text-muted fst-italic" visible="false">Atanmamış</span>
                            </h5>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-common">
                <div class="card-header">
                    <h4 class="mb-0">Ders Açıklaması</h4>
                </div>
                <div class="card-body">
                    <p id="pAciklama" runat="server" class="card-text">
                        <asp:Literal ID="litAciklama" runat="server" Text="<span class='text-muted fst-italic'>Bu ders için bir açıklama girilmemiştir.</span>"></asp:Literal>
                    </p>
                </div>
            </div>

            <div class="text-center mt-4 mb-4">
                <asp:HyperLink ID="hlGeri" runat="server" NavigateUrl="~/DersTalep.aspx" CssClass="btn btn-secondary">
         <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"></span> Geri Dön
                </asp:HyperLink>
            </div>

        </asp:Panel>
    </div>
</asp:Content>
