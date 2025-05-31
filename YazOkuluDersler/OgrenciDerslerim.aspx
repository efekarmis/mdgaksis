<%@ Page Title="Kayıtlı Derslerim" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgrenciDerslerim.aspx.cs" Inherits="YazOkulu.OgrenciDerslerim" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Onaylı Ders Listem</h2>
        <p class="text-muted">Aşağıda kayıtlı olduğunuz ve açılması kesinleşen/beklenen derslerin listesi bulunmaktadır.</p>
        <hr />

        <div class="table-container">
            <table class="table table-bordered table-hover align-middle">
                <thead class="table-light">
                    <tr>
                        <th>Ders Adı</th>
                        <th>Öğretmen</th>
                        <th style="width: 15%;">Kayıt Tarihi</th>
                        <th style="width: 15%; text-align: center;">Dersi Alan Öğrenci</th>
                        <th style="width: 30%;">Ders Durumu</th>
                    </tr>
                </thead>
                <asp:Repeater ID="RepeaterDerslerim" runat="server">
                    <HeaderTemplate>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server"
                                    NavigateUrl='<%# "~/DersDetay.aspx?ID=" + Eval("DersId") %>'
                                    Text='<%# Eval("DersAd") %>'
                                    ToolTip='<%# Eval("DersAd") + " dersinin detaylarını gör..." %>'>
                                </asp:HyperLink>
                            <td>
                                <asp:HyperLink ID="hlOgretmenLink" runat="server"
                                    Visible='<%# Eval("OgretmenID") != null && Convert.ToInt32(Eval("OgretmenID")) > 0 %>'
                                    NavigateUrl='<%# "~/OgretmenDetay.aspx?OgretmenID=" + Eval("OgretmenID") %>'
                                    Text='<%# Eval("OgretmenAdSoyad") %>'
                                    ToolTip='<%# Eval("OgretmenAdSoyad") + " detaylarını gör..." %>'>
                                </asp:HyperLink>
                                <asp:Label ID="lblOgretmenYok" runat="server"
                                    CssClass="text-muted fst-italic"
                                    Visible='<%# Eval("OgretmenID") == null || Convert.ToInt32(Eval("OgretmenID")) <= 0 %>'
                                    Text='<%# Eval("OgretmenAdSoyad") %>'>
                                </asp:Label>
                            </td>
                            <td><%# Eval("KayitTarihi", "{0:dd.MM.yyyy}") %></td>
                            <td class="text-center"><%# Eval("MevcutOgrenciSayisi") %></td>
                            <td>
                                <span class='<%# (int)Eval("MevcutOgrenciSayisi") >= (int)Eval("MinKontenjan") ? "text-success fw-semibold" : "text-danger" %>'>
                                    <%# (int)Eval("MevcutOgrenciSayisi") >= (int)Eval("MinKontenjan") ? "Ders açılacaktır." : $"Minimum kontenjan ({Eval("MinKontenjan")}) sağlanamadığı için ders açılmayacaktır." %>
                                </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        <% if (RepeaterDerslerim.Items.Count == 0)
                            { %>
                        <tfoot>
                            <tr>
                                <td colspan="5" class="text-center text-muted p-4">Kayıtlı olduğunuz onaylı ders bulunmamaktadır.</td>
                            </tr>
                        </tfoot>
                        <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
