<%@ Page Title="Manuel Ders Başvurusu" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DersAta.aspx.cs" Inherits="YazOkulu.DersAta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <h2>Manuel Ders Başvurusu Oluştur</h2>
        <p class="text-muted">Öğretmen/Yönetici olarak bir öğrenci adına ders başvurusu oluşturun.</p>
        <hr />

        <div class="card card-common" style="max-width: 700px; margin: 20px auto;">
            <div class="card-body">

                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" Text="Ders Seçin:" CssClass="form-label fw-semibold"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidatorDers" runat="server"
                        ControlToValidate="DropDownList1"
                        Operator="NotEqual" ValueToCompare="0"
                        ErrorMessage="Lütfen bir ders seçiniz."
                        Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Ders seçimi zorunludur.</asp:CompareValidator>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label2" runat="server" Text="Öğrenci ID:" CssClass="form-label fw-semibold"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Başvuru yapılacak öğrencinin ID'si" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOgrId" runat="server"
                        ControlToValidate="TextBox1"
                        ErrorMessage="Öğrenci ID alanı boş bırakılamaz."
                        Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Öğrenci ID zorunludur.</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorOgrId" runat="server"
                        ControlToValidate="TextBox1" Operator="DataTypeCheck" Type="Integer"
                        ErrorMessage="Lütfen geçerli bir sayısal Öğrenci ID girin."
                        Display="Dynamic" CssClass="text-danger mt-1" Font-Size="Small">* Geçerli bir ID girin.</asp:CompareValidator>
                </div>

                <div class="mb-3 text-center">
                    <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
                </div>

                <div class="text-center mt-4">
                    <asp:Button ID="Button1" runat="server" Text="Başvuru Oluştur" OnClick="ButtonTalep_Click" CssClass="btn btn-primary" />
                </div>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger mt-3" HeaderText="Lütfen formdaki hataları düzeltin:" ShowSummary="true" />

            </div>
        </div>

    </div>
</asp:Content>
