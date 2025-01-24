<%@ Page Title="Title" Language="C#" MasterPageFile="MasterPage.master" CodeBehind="KontenjanListesi.aspx.cs" Inherits="YazOkuluDersler.KontenjanListesi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <table class="table table-bordered table-hover">
            <tr>
                <th>Ders Id</th>
                <th>Ders Adı</th>
                <th>Minimum Kontenjan</th>
                <th>Maximum Kontenjan</th>
                <th>İşlemler</th>
            </tr>
            <tbody>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                      <tr>
                          <td><%#Eval("ID")%></td>
                          <td><%#Eval("DERSAD")%></td>
                          <td><%#Eval("MIN")%></td>
                          <td><%#Eval("MAX")%></td>
                          <td>
                              <asp:HyperLink NavigateUrl='<%# "~/KontenjanSil.aspx?ID=" + Eval("ID")%>' ID="HyperLink1" CssClass="btn btn-danger " runat="server">Sil</asp:HyperLink>
                              <asp:HyperLink NavigateUrl='<%# "~/KontenjanGuncelle.aspx?ID=" + Eval("ID")%>' ID="HyperLink2" CssClass="btn btn-info" runat="server">Güncelle</asp:HyperLink>
                          </td>
                      </tr>  
                    </ItemTemplate> 
                </asp:Repeater>
            </tbody>
        </table>
</asp:Content>