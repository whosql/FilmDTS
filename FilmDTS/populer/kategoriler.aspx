<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="kategoriler.aspx.cs" Inherits="FilmDTS.populer.kategoriler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
        <div class="col-9" style="height: 100%; min-height: 1000px;">
                <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Popüler Kategoriler</h1>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">
            
            <asp:GridView ID="Kategoriler1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KategoriID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KategoriID"
                                        DataNavigateUrlFormatString="/kategori/{0}"
                                        DataTextField="KategoriAdi" HeaderText="Kategori Adı"/>
                    <asp:BoundField DataField="KategoriIzlenme" HeaderText="Toplam İzlenme"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>