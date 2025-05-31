<%@ Page Title="Atanmış Derslerim" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgretmenDerslerim.aspx.cs" Inherits="YazOkulu.OgretmenDerslerim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Atanmış Derslerim ve Kayıtlı Öğrenciler</h2>
        <hr />

        <asp:Repeater ID="rptOgretmenDersleri" runat="server" OnItemDataBound="rptOgretmenDersleri_ItemDataBound" OnItemCommand="rptOgretmenDersleri_ItemCommand">
            <HeaderTemplate>
            </HeaderTemplate>

            <ItemTemplate>
                <div class="card card-common mb-4">
                    <div class="card-header bg-light">
                        <h4 class="mb-0">
                            <asp:HyperLink runat="server"
                                NavigateUrl='<%# "~/DersDetay.aspx?ID=" + Eval("ID") %>'
                                Text='<%# Eval("DERSAD") %>'
                                ToolTip='<%# Eval("DERSAD") + " dersinin detaylarını gör..." %>'>
                            </asp:HyperLink>
                            <span class="badge bg-secondary"
                                style="font-size: 0.7em; vertical-align: middle;">Min: <%# Eval("MIN") %> / Max: <%# Eval("MAX") %>
                            </span>
                        </h4>
                        <asp:HyperLink runat="server"
                            NavigateUrl='<%# "~/DersGuncelle.aspx?ID=" + Eval("ID") %>'
                            CssClass="btn btn-outline-primary btn-sm" ToolTip="Ders Bilgilerini Güncelle">
                              <span class="glyphicon glyphicon-pencil"></span> Güncelle
                        </asp:HyperLink>
                    </div>
                    <div class="card-body">
                        <h5>Kayıtlı Öğrenciler:</h5>
                        <asp:Repeater ID="rptDersOgrencileri" runat="server">
                            <HeaderTemplate>
                                <table class="table table-sm table-striped table-hover">
                                    <thead>
                                        <tr>
                                            <th>Öğrenci No</th>
                                            <th>Adı Soyadı</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("NUMARA") %></td>
                                    <td><%# Eval("AD") %> <%# Eval("SOYAD") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                    </table>
                            </FooterTemplate>
                        </asp:Repeater>

                        <asp:PlaceHolder ID="phOgrenciYok" runat="server" Visible="false">
                            <p class="text-muted text-center mt-3">Bu derse kayıtlı öğrenci bulunmamaktadır.</p>
                        </asp:PlaceHolder>

                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                <% if (rptOgretmenDersleri.Items.Count == 0)
                    { %>
                <div class="alert alert-warning text-center">Size atanmış ders bulunmamaktadır.</div>
                <% } %>
            </FooterTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
