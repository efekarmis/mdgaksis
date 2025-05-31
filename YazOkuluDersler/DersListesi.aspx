<%@ Page Title="Ders Yönetimi" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DersListesi.aspx.cs" Inherits="YazOkuluDersler.DersListesi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Ders Yönetimi</h2>
        <p class="text-muted">Mevcut dersleri görüntüleyin, yeni ders ekleyin veya mevcutları düzenleyin.</p>
        <hr />

        <asp:PlaceHolder ID="phMesaj" runat="server" Visible="false">
            <div id="divMesaj" class="alert alert-dismissible" runat="server" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Kapat"><span aria-hidden="true">×</span></button>
                <asp:Literal ID="litMesaj" runat="server"></asp:Literal>
            </div>
        </asp:PlaceHolder>

        <div class="mb-3 text-end">
            <asp:HyperLink ID="hlDersEkle" runat="server" NavigateUrl="~/DersKayit.aspx" CssClass="btn btn-success">
                     <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> Yeni Ders Ekle
            </asp:HyperLink>
        </div>

        <div class="table-container">
            <table class="table table-bordered table-hover align-middle">
                <thead class="table-light">
                    <tr>
                        <th style="width: 8%; text-align: center;">ID</th>
                        <th>Ders Adı</th>
                        <th>Atanan Öğretmen</th>
                        <th style="width: 8%; text-align: center;" title="Minimum Kontenjan">Min K.</th>
                        <th style="width: 8%; text-align: center;" title="Maksimum Kontenjan">Max K.</th>
                        <th style="width: 10%; text-align: right;">Ücret (TL)</th>
                        <th style="width: 15%; text-align: center;">İşlemler</th>
                    </tr>
                </thead>
                <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_ItemCommand">
                    <HeaderTemplate>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Eval("ID") %></td>
                            <td>
                                <asp:HyperLink runat="server" NavigateUrl='<%# "~/DersDetay.aspx?ID=" + Eval("ID") %>' Text='<%# Eval("DERSAD") %>' ToolTip='<%# Eval("DERSAD") + " dersinin detaylarını gör..." %>'></asp:HyperLink>
                            </td>
                            <td><%# Eval("Ogretmen") != null ? Eval("Ogretmen.OGRTADSOYAD") : "<span class='text-muted fst-italic'>Atanmamış</span>" %></td>
                            <td class="text-center"><%# Eval("MIN") %></td>
                            <td class="text-center"><%# Eval("MAX") %></td>
                            <td style="text-align: right;"><%# Eval("DERSUCRET", "{0:N2}") %></td>
                            <td class="text-center">
                                <asp:HyperLink NavigateUrl='<%# "~/DersGuncelle.aspx?ID=" + Eval("ID") %>' ID="HyperLink2" CssClass="btn btn-info btn-sm me-2" ToolTip="Dersi Güncelle" runat="server">
                                      <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Güncelle
                                </asp:HyperLink>
                                <asp:LinkButton ID="btnSil" runat="server" CssClass="btn btn-danger btn-sm"
                                    CommandName="Sil" CommandArgument='<%# Eval("ID") %>'
                                    OnClientClick="return confirm('Bu dersi silmek istediğinizden emin misiniz? İlgili başvurular ve kayıtlar da silinebilir!');"
                                    ToolTip="Dersi Sil">
                                     <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Sil
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                            <% if (Repeater.Items.Count == 0)
                                { %>
                        <tfoot>
                            <tr>
                                <td colspan="7" class="text-center text-muted p-4">Sistemde kayıtlı ders bulunmamaktadır.</td>
                            </tr>
                        </tfoot>
                        <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
