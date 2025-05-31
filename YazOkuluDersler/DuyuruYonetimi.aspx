<%@ Page Title="Duyuru Yönetimi" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DuyuruYonetimi.aspx.cs" Inherits="YazOkulu.DuyuruYonetimi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Duyuru Yönetimi</h2>
        <p class="text-muted">Mevcut duyuruları görüntüleyin, yeni duyuru ekleyin veya mevcutları yönetin.</p>
        <hr />

        <asp:PlaceHolder ID="phMesaj" runat="server" Visible="false">
            <div id="divMesaj" class="alert alert-dismissible" runat="server" role="alert">
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                <asp:Literal ID="litMesaj" runat="server"></asp:Literal>
            </div>
        </asp:PlaceHolder>

        <div class="mb-3 text-end">
            <asp:HyperLink ID="hlDuyuruEkle" runat="server" NavigateUrl="~/DuyuruEkle.aspx" CssClass="btn btn-success">
                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> Yeni Duyuru Ekle
            </asp:HyperLink>
        </div>

        <div class="card card-common shadow-sm">
            <div class="card-header">
                <h4 class="mb-0"><span class="glyphicon glyphicon-list-alt me-2"></span>Mevcut Duyurular</h4>
            </div>
            <div class="card-body">
                <div class="table-container">
                    <table class="table table-striped table-hover align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Başlık</th>
                                <th style="width: 15%;">Hedef Kitle</th>
                                <th style="width: 10%;">Önem</th>
                                <th style="width: 20%;">Yayın Tarihi</th>
                                <th style="width: 15%; text-align: center;">İşlemler</th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="rptTumDuyurular" runat="server" OnItemCommand="rptTumDuyurular_ItemCommand">
                            <HeaderTemplate>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Baslik") %></td>
                                    <td><%# GetHedefKitleText(Eval("HedefKitle")) %></td>
                                    <td>
                                        <%# (short)Eval("OnemDerecesi") == 1 ? "<span class='badge rounded-pill bg-danger'>Önemli</span>" : "<span class='badge rounded-pill bg-secondary'>Normal</span>" %>
                                    </td>
                                    <td><%# Eval("YayinTarihi", "{0:dd.MM.yyyy HH:mm}") %></td>
                                    <td class="text-center">
                                        <asp:HyperLink ID="hlGuncelle" runat="server" CssClass="btn btn-info btn-sm me-2"
                                            NavigateUrl='<%# "~/DuyuruGuncelle.aspx?DuyuruID=" + Eval("DuyuruID") %>'
                                            ToolTip="Duyuruyu Güncelle">
                                                 <span class="glyphicon glyphicon-pencil"></span> Güncelle
                                        </asp:HyperLink>
                                        <asp:LinkButton ID="btnSil" runat="server" CssClass="btn btn-danger btn-sm"
                                            CommandName="Sil" CommandArgument='<%# Eval("DuyuruID") %>'
                                            OnClientClick="return confirm('Bu duyuruyu kalıcı olarak silmek istediğinizden emin misiniz?');"
                                            ToolTip="Duyuruyu Sil">
                                                <span class="glyphicon glyphicon-trash"></span> Sil
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                    <% if (rptTumDuyurular.Items.Count == 0)
                                        { %>
                                <tfoot>
                                    <tr>
                                        <td colspan="5" class="text-center text-muted p-3">Gösterilecek duyuru yok.</td>
                                    </tr>
                                </tfoot>
                                <% } %>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
