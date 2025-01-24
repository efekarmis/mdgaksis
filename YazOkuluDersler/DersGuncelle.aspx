<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.master" CodeBehind="DersGuncelle.aspx.cs" Inherits="YazOkuluDersler.DersGuncelle" %>

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
               <asp:Label for="DrpOgretmenList" runat="server" class="form-label" Text="Derse Bağlı Öğretmen:"></asp:Label>
               <asp:DropDownList ID="DrpOgretmenList" runat="server" CssClass="form-control">
                   <asp:ListItem Text="10" Value="10"></asp:ListItem>
                   <asp:ListItem Text="15" Value="15"></asp:ListItem>
                   <asp:ListItem Text="20" Value="20"></asp:ListItem>
                   <asp:ListItem Text="25" Value="25"></asp:ListItem>
                   <asp:ListItem Text="30" Value="30"></asp:ListItem>
               </asp:DropDownList>
            </div>
            <br/>
        </div>
        <div class="text-center">
            <asp:Button ID="Button1" runat="server" Text="Güncelle" OnClick="ButtonG_Click" CssClass="btn btn-primary w-100"/>
        </div>
    </form>
</asp:Content>
