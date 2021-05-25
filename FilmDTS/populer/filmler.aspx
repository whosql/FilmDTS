<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="filmler.aspx.cs" Inherits="FilmDTS.populer.filmler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
        <div class="col-9" style="height: 100%; min-height: 1000px;">
                <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Popüler Filmler</h1>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">
            
            <asp:GridView ID="Filmler1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                                        DataNavigateUrlFormatString="/film/{0}"
                                        DataTextField="Ad" HeaderText="Filmin Adı"/>
                    <asp:BoundField DataField="IzlenmeSayisi" HeaderText="İzlenme Sayısı"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>



</asp:Content>
