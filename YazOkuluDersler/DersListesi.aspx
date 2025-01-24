<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.master" CodeBehind="DersListesi.aspx.cs" Inherits="YazOkuluDersler.DersListesi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <table class="table table-bordered table-hover">
            <tr>
                <th>Ders Id</th>
                <th>Ders Ad</th>
                <th>Derse Bağlı Öğretmen</th>
                <th>İşlemler</th>
            </tr>
            <tbody>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                      <tr>
                          <td><%#Eval("ID")%></td>
                          <td><%#Eval("DERSAD")%></td>
                          <td><%#Eval("Ogretmen.OGRTADSOYAD")%></td>
                          <td>
                              <asp:HyperLink NavigateUrl='<%# "~/DersSil.aspx?ID=" + Eval("ID")%>' ID="HyperLink1" CssClass="btn btn-danger " runat="server">Sil</asp:HyperLink>
                              <asp:HyperLink NavigateUrl='<%# "~/DersGuncelle.aspx?ID=" + Eval("ID")%>' ID="HyperLink2" CssClass="btn btn-info" runat="server">Güncelle</asp:HyperLink>
                          </td>
                      </tr>  
                    </ItemTemplate> 
                </asp:Repeater>
            </tbody>
        </table>
</asp:Content>