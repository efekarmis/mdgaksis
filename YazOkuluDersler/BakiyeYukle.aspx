<%@ Page Title="Bakiye Yükle" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BakiyeYukle.aspx.cs" Inherits="YazOkulu.BakiyeYukle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid">
        <div class="login-card" style="max-width: 600px; margin: 30px auto; text-align: left;">
            <h2>Bakiye Yükle</h2>
            <hr />

            <div class="mb-3">
                <h4>Mevcut Bakiyeniz:
                    <asp:Label ID="lblMevcutBakiye" runat="server" Text="0,00 TL" Font-Bold="true"></asp:Label></h4>
            </div>

            <asp:Panel ID="pnlBakiyeYukle" runat="server">
                <div class="form-group">
                    <label for="txtYuklenecekMiktar">Yüklenecek Miktar (TL):</label>
                    <asp:TextBox ID="txtYuklenecekMiktar" runat="server" CssClass="form-control" TextMode="Number" placeholder="Örn: 50,00"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtYuklenecekMiktar"
                        ErrorMessage="Yüklenecek miktar alanı boş bırakılamaz."
                        Display="Dynamic" CssClass="text-danger" Font-Size="Small">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ControlToValidate="txtYuklenecekMiktar" Operator="DataTypeCheck" Type="Double"
                        ErrorMessage="Lütfen geçerli bir sayısal değer girin (örn: 50 veya 50.5)."
                        Display="Dynamic" CssClass="text-danger" Font-Size="Small">*</asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                        ControlToValidate="txtYuklenecekMiktar" Operator="GreaterThan" Type="Double" ValueToCompare="0"
                        ErrorMessage="Yüklenecek miktar 0'dan büyük olmalıdır."
                        Display="Dynamic" CssClass="text-danger" Font-Size="Small">*</asp:CompareValidator>
                </div>

                <div class="text-center mt-4">
                    <asp:Button ID="btnBakiyeYukle" runat="server" Text="Bakiyeyi Yükle" CssClass="btn btn-success" OnClick="btnBakiyeYukle_Click" />
                </div>
            </asp:Panel>

            <div class="mt-4 text-center">
                <asp:Label ID="lblMesaj" runat="server" EnableViewState="false" CssClass=""></asp:Label>
            </div>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger mt-3" HeaderText="Lütfen formdaki hataları düzeltin:" />

        </div>
    </div>
</asp:Content>
