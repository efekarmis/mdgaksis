<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.master" CodeBehind="OgrenciGuncelle.aspx.cs" Inherits="YazOkuluDersler.OgrenciGuncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" runat="server" class="container mt-5 p-4 border rounded bg-light">
        <div class="form-group">
            <div class="mb-3">
                <asp:Label for="TxtAd" runat="server" class="form-label" Text="Öğrenci Adı:"></asp:Label>
                <asp:TextBox ID="TxtAd" runat="server" CssClass="form-control" placeholder="Adınızı giriniz"></asp:TextBox>
            </div>
            <br/>
            <div class="mb-3">
                <asp:Label for="TxtSoyad" runat="server" class="form-label" Text="Öğrenci Soyadı:"></asp:Label>
                <asp:TextBox ID="TxtSoyad" runat="server" CssClass="form-control" placeholder="Soyadınızı giriniz"></asp:TextBox>
            </div>
            <br/>
            <div class="mb-3">
                <asp:Label for="TxtNumara" runat="server" class="form-label" Text="Öğrenci Numarası:"></asp:Label>
                <asp:TextBox ID="TxtNumara" runat="server" CssClass="form-control" placeholder="Numaranızı giriniz"></asp:TextBox>
            </div>
            <br/>
            <div class="mb-3">
                <asp:Label for="TxtSifre" runat="server" class="form-label" Text="Öğrenci Şifresi:"></asp:Label>
                <asp:TextBox ID="TxtSifre" runat="server" CssClass="form-control" placeholder="Şifrenizi giriniz"></asp:TextBox>
            </div>
            <br/>
            <div class="mb-3">
                <asp:Label for="TxtFoto" runat="server" class="form-label" Text="Öğrenci Fotoğrafı:"></asp:Label>
                <asp:TextBox ID="TxtFoto" runat="server" CssClass="form-control" placeholder="Fotoğraf yolunu giriniz"></asp:TextBox>
            </div>
        </div>
        <div class="text-center">
            <asp:Button ID="Button1" runat="server" Text="Güncelle" OnClick="ButtonG_Click" CssClass="btn btn-primary w-100"/>
        </div>
    </form>

</asp:Content>