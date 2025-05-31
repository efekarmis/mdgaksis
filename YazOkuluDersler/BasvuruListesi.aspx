<%@ Page Title="Bekleyen Başvurular" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BasvuruListesi.aspx.cs" Inherits="YazOkulu.BasvuruListesi" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Bekleyen Ders Başvuruları</h2>
        <p class="text-muted">Öğrencilerin ders taleplerini onaylayın veya reddedin.</p>
        <hr />

        <div class="table-container">
            <table class="table table-bordered table-hover align-middle">
                <thead class="table-light">
                    <tr>
                        <th style="width: 10%;">Başvuru Id</th>
                        <th>Öğrenci Adı Soyadı</th>
                        <th>Talep Edilen Ders</th>
                        <th style="width: 20%; text-align: center;">İşlemler</th>
                    </tr>
                </thead>
                <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_Command">
                    <HeaderTemplate>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Eval("BASVURUID") %></td>
                            <td><%# Eval("BASVURUOGRADSOYAD") %></td>
                            <td><%# Eval("BASVURUDERSAD") %></td>
                            <td class="text-center">
                                <asp:Button ID="btnOnayla" runat="server" Text="Onayla" CssClass="btn btn-success btn-sm me-2" ToolTip="Başvuruyu onayla ve öğrenciyi derse kaydet" CommandName="Onayla" CommandArgument='<%# Eval("BASVURUID") %>' CausesValidation="false" />
                                <asp:Button ID="btnRed" runat="server" Text="Reddet" CssClass="btn btn-danger btn-sm" ToolTip="Başvuruyu reddet" CommandName="Red" CommandArgument='<%# Eval("BASVURUID") %>' CausesValidation="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        <% if (Repeater.Items.Count == 0)
                            { %>
                        <tfoot>
                            <tr>
                                <td colspan="4" class="text-center text-muted p-4">Bekleyen başvuru bulunmamaktadır.</td>
                            </tr>
                        </tfoot>
                        <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>

        <div class="mt-3 text-center">
            <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
        </div>

    </div>
</asp:Content>
