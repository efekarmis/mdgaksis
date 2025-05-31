<%@ Page Title="Öğretmen Detay" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OgretmenDetay.aspx.cs" Inherits="YazOkulu.OgretmenDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <asp:PlaceHolder ID="phHata" runat="server" Visible="false">
            <div class="alert alert-danger">
                <asp:Literal ID="litHata" runat="server"></asp:Literal>
            </div>
            <p><a href="javascript:history.back()" class="btn btn-secondary">Geri Dön</a></p>
        </asp:PlaceHolder>

        <asp:Panel ID="pnlDetay" runat="server">
            <div class="row">
                <div class="col-md-3 text-center">
                    <asp:Image ID="imgOgretmenFoto" runat="server"
                        CssClass="img-thumbnail rounded-circle"
                        style="max-width: 180px; max-height: 180px; object-fit: cover; margin-bottom: 15px;"
                        AlternateText="Öğretmen Fotoğrafı" />
                </div>
                <div class="col-md-9">
                    <h2>
                        <asp:Literal ID="litOgretmenAdSoyad" runat="server"></asp:Literal></h2>
                    <hr />
                    <h4>Verdiği Dersler:</h4>

                    <asp:Repeater ID="rptVerilenDersler" runat="server">
                        <HeaderTemplate>
                            <ul class="list-group">
                        </HeaderTemplate>
                        <itemtemplate>
                            <li class="list-group-item">
                                <asp:HyperLink runat="server"
                                    NavigateUrl='<%# "~/DersDetay.aspx?ID=" + Eval("ID") %>'
                                    Text='<%# Eval("DERSAD") %>'
                                    ToolTip='<%# Eval("DERSAD") + " dersinin detaylarını gör..." %>'>
                                </asp:HyperLink>
                                <span class="badge bg-secondary ms-2" style="font-size: 0.8em; vertical-align: middle;">Min: <%# Eval("MIN") %> / Max: <%# Eval("MAX") %> - Ücret: <%# Eval("DERSUCRET", "{0:C0}") %>
                                </span>
                            </li>
                        </itemtemplate>
                        <footertemplate>
                            </ul>
                                <% if (rptVerilenDersler.Items.Count == 0)
                                    { %>
                            <p class="text-muted mt-3">Bu öğretmenin verdiği ders bulunmamaktadır.</p>
                            <% } %>
                        </footertemplate>
                    </asp:Repeater>

                </div>
            </div>
            <div class="mt-4">
                <a href="javascript:history.back()" class="btn btn-secondary">Geri Dön</a>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
