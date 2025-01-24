<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.master" CodeBehind="KontenjanGuncelle.aspx.cs" Inherits="YazOkuluDersler.KontenjanGuncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" runat="server" class="container mt-5 p-4 border rounded bg-light">
        <div class="form-group">
            <div class="mb-3">
                <asp:Label for="TxtDersAdi" runat="server" class="form-label" Text="Ders Adı:"></asp:Label>
                <asp:TextBox ID="TxtDersAdi" runat="server" CssClass="form-control" placeholder="Ders adını giriniz"></asp:TextBox>
            </div>
            <br/>
            <div class="mb-3">
                <asp:Label for="TxtMinKontenjan" runat="server" class="form-label" Text="Minimum Kontenjan:"></asp:Label>
                <asp:TextBox ID="TxtMinKontenjan" runat="server" CssClass="form-control" placeholder="Minimum kontenjanı giriniz"></asp:TextBox>
            </div>
            <br/>
            <div class="mb-3">
                <asp:Label for="TxtMaxKontenjan" runat="server" class="form-label" Text="Maksimum Kontenjan:"></asp:Label>
                <asp:TextBox ID="TxtMaxKontenjan" runat="server" CssClass="form-control" placeholder="Maksimum kontenjanı giriniz"></asp:TextBox>
            </div>
        </div>
        <div class="text-center">
            <asp:Button ID="Button1" runat="server" Text="Güncelle" OnClick="ButtonG_Click" CssClass="btn btn-primary w-100"/>
        </div>
    </form>
</asp:Content>
