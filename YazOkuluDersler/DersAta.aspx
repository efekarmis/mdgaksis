<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.master" CodeBehind="DersAta.aspx.cs" Inherits="YazOkuluDersler.DersAta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ders Seçin"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <br/>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Öğrenci ID:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <br/>
        <asp:Button ID="Button1" runat="server" Text="Ders Talep Oluştur" OnClick="ButtonTalep_Click" CssClass="btn btn-danger"></asp:Button>
    </form>
</asp:Content>

