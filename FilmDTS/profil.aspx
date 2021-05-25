<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="profil.aspx.cs" Inherits="FilmDTS.profil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">

    <div class="col-10" style="height: 100%; min-height: 1000px;">
        <div class="row" style="border-bottom: 15px solid #1976D2; background: #4d42b8;">
            <div class="col-4"></div>
            <div class="col-4" style="margin-top: 20px; margin-bottom: 10px;">
                <img class="" style="width: 100%; margin-bottom: 10px;" runat="server" id="profilFotografi">
                <h3 runat="server" id="adSoyad" class="text-center" style="color: white;">Test Test</h3>
                <div runat="server" id="areaArkadasEkleButonu">
                                    <asp:Button runat="server" class="btn btn-primary" Style="width: 100%; margin-bottom: 10px; background: #1976D2;" ID="btnArkadasEkle" Text="Arkadaş Ekle" OnClick="btnArkadasEkle_Click"></asp:Button>
                </div>
            </div>
            <div class="col-4"></div>
        </div>
        <div>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>İncelemeleri</h4>
            </div>
            <asp:GridView ID="Incelemeler1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                    <asp:BoundField DataField="Inceleme" HeaderText="İnceleme" />
                </Columns>
            </asp:GridView>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>Alıntıladıkları</h4>
            </div>
            <asp:GridView ID="Alintilar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Film Adı" />
                    <asp:BoundField DataField="Cumle" HeaderText="Cümle" />
                </Columns>
            </asp:GridView>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>İzlediği</h4>
            </div>
            <asp:GridView ID="Izlenenler1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Film Adı" />
                </Columns>
            </asp:GridView>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>Verdiği Puanlar</h4>
            </div>
            <asp:GridView ID="Puanlar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="FilmID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                        DataNavigateUrlFormatString="/film/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                    <asp:BoundField DataField="Puan" HeaderText="Puan" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
