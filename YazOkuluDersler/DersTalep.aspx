<%@ Page Title="Ders Talebi" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DersTalep.aspx.cs" Inherits="YazOkulu.DersTalep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Ders Seçimi ve Talep</h2>
        <p class="text-muted">Aşağıdaki listeden almak istediğiniz dersleri inceleyebilir ve başvuru yapabilirsiniz.</p>
        <hr />

        <asp:PlaceHolder ID="phMesaj" runat="server" Visible="false">
            <div id="divMesaj" class="alert" runat="server">
                <asp:Literal ID="litMesaj" runat="server"></asp:Literal>
            </div>
        </asp:PlaceHolder>

        <div class="table-responsive">
            <table class="table table-bordered table-hover align-middle">
                <thead class="table-light">
                    <tr>
                        <th>Ders Adı</th>
                        <th>Öğretmen</th>
                        <th style="text-align: right;">Ücret (TL)</th>
                        <th style="width: 20%; text-align: center;">İşlemler</th>
                    </tr>
                </thead>
                <asp:Repeater ID="rptDersListesi" runat="server" OnItemCommand="rptDersListesi_ItemCommand">
                    <HeaderTemplate>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server"
                                    NavigateUrl='<%# "~/DersDetay.aspx?ID=" + Eval("ID") %>'
                                    Text='<%# Eval("DERSAD") %>'
                                    ToolTip='<%# Eval("DERSAD") + " dersinin detaylarını gör..." %>'>
                                </asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hlOgretmenLink" runat="server"
                                    Visible='<%# Eval("Ogretmen") != null %>'
                                    NavigateUrl='<%# "~/OgretmenDetay.aspx?OgretmenID=" + Eval("OGRETMENID") %>'
                                    Text='<%# Eval("Ogretmen.OGRTADSOYAD") %>'
                                    ToolTip='<%# Eval("Ogretmen.OGRTADSOYAD") + " detaylarını gör..." %>'>
                                </asp:HyperLink>
                                <asp:Literal ID="litAtanmamis" runat="server"
                                    Visible='<%# Eval("Ogretmen") == null %>'
                                    Text="<span class='text-muted fst-italic'>Atanmamış</span>">
                                </asp:Literal>
                            </td>
                            <td style="text-align: right;"><%# Eval("DERSUCRET", "{0:C2}") %></td>
                            <td class="text-center">
                                <asp:Button ID="btnBasvur" runat="server" Text="Başvur"
                                    CssClass="btn btn-success btn-sm me-2"
                                    CommandName="Basvur" CommandArgument='<%# Eval("ID") %>'
                                    ToolTip='<%# Eval("DERSAD") + " dersine başvur" %>'
                                    CausesValidation="false" />
                                <asp:HyperLink ID="hlDetay" runat="server" Text="Detay"
                                    CssClass="btn btn-info btn-sm"
                                    NavigateUrl='<%# "~/DersDetay.aspx?ID=" + Eval("ID") %>'
                                    ToolTip='<%# Eval("DERSAD") + " dersinin detaylarını gör..." %>'>
                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span> Detay
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        <% if (rptDersListesi.Items.Count == 0)
                            { %>
                        <tfoot>
                            <tr>
                                <td colspan="4" class="text-center text-muted p-4">Talep edilebilecek ders bulunmamaktadır.</td>
                            </tr>
                        </tfoot>
                        <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
