<%@ Page Title="Öğrenci Listesi" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciListesi.aspx.cs" Inherits="YazOkulu.OgrenciListesi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Öğrenci Yönetimi</h2>
        <p class="text-muted">Sistemdeki öğrencileri görüntüleyin, güncelleyin veya silin.</p>
        <hr />

        <asp:PlaceHolder ID="phMesaj" runat="server" Visible="false">
            <div id="divMesaj" class="alert alert-dismissible" runat="server" role="alert">
                 <button type="button" class="close" data-dismiss="alert" aria-label="Kapat"><span aria-hidden="true">×</span></button>
                <asp:Literal ID="litMesaj" runat="server"></asp:Literal>
            </div>
        </asp:PlaceHolder>

        <div class="mb-3 text-end">
            <asp:HyperLink ID="hlOgrenciEkle" runat="server" NavigateUrl="~/OgrenciKayit.aspx" CssClass="btn btn-success">
                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> Yeni Öğrenci Ekle
            </asp:HyperLink>
        </div>

        <div class="table-container">
            <table class="table table-bordered table-hover align-middle">
                <thead class="table-light">
                    <tr>
                        <th style="width: 8%; text-align: center;">ID</th>
                        <th style="width: 8%;">Fotoğraf</th>
                        <th>Ad</th>
                        <th>Soyad</th>
                        <th>Numara</th>
                        <th style="text-align: right;">Bakiye</th>
                        <th style="width: 15%; text-align: center;">İşlemler</th>
                    </tr>
                </thead>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <HeaderTemplate>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Eval("ID") %></td>
                            <td class="text-center">
                                <asp:Image ID="imgOgrenci" runat="server"
                                    ImageUrl='<%# ResolveUrl( Eval("FOTOGRAF") == null || string.IsNullOrEmpty(Eval("FOTOGRAF").ToString()) ? "~/OgrenciFotograflari/default.png" : "~/OgrenciFotograflari/" + Eval("FOTOGRAF")) %>'
                                    AlternateText='<%# Eval("AD") + " " + Eval("SOYAD") %>'
                                    CssClass="img-thumbnail rounded-circle"
                                    style="height: 50px; width: 50px; object-fit: cover; border-radius: 50%; padding: 0 !important; border: 1px solid var(--app-navbar-secondarycolor);" />
                            </td>
                            <td><%# Eval("AD") %></td>
                            <td><%# Eval("SOYAD") %></td>
                            <td><%# Eval("NUMARA") %></td>
                            <td style="text-align: right;"><%# Eval("BAKIYE", "{0:C2}") %></td>
                            <td class="text-center">
                                <asp:HyperLink NavigateUrl='<%# "~/OgrenciGuncelle.aspx?OGRID=" + Eval("ID") %>' ID="HyperLink2" CssClass="btn btn-info btn-sm me-2" ToolTip="Öğrenciyi Güncelle" runat="server">
                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Güncelle
                                </asp:HyperLink>
                                <asp:LinkButton ID="btnSil" runat="server" CssClass="btn btn-danger btn-sm"
                                    CommandName="Sil" CommandArgument='<%# Eval("ID") %>'
                                    OnClientClick="return confirm('Bu öğrenciyi silmek istediğinizden emin misiniz? Bu işlem geri alınamaz!');"
                                    ToolTip="Öğrenciyi Sil">
                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Sil
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        <% if (Repeater1.Items.Count == 0) { %>
                        <tfoot>
                            <tr>
                                <td colspan="7" class="text-center text-muted p-4">Sistemde kayıtlı öğrenci bulunmamaktadır.</td>
                            </tr>
                        </tfoot>
                         <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>