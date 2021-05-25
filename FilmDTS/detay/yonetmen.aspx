<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="yonetmen.aspx.cs" Inherits="FilmDTS.detay.yonetmen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
        <div class="col-6" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Yönetmen</h1>
        </div>      
        <div class="row" style="padding: 10px; border-bottom: 15px solid #4d42b8; margin-bottom: 10px; border-top: 15px solid #4d42b8; margin-top: 10px;">
            <asp:GridView ID="Yonetmen1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="YonetmenID">
                <Columns>
                    <asp:BoundField DataField="AdSoyad" HeaderText="Yönetmenin Adı"/>
                    <asp:BoundField DataField="DogumTarihi" HeaderText="Doğum Tarihi"/>
                    <asp:BoundField DataField="DogumYeri" HeaderText="Doğum Yeri"/>
                    <asp:BoundField DataField="OlumTarihi" HeaderText="Ölüm Tarihi"/>               
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="col-4" style="background: #FFFFFF; padding-top: 10px;">
        <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
            <h4>Yönetmenin Filmleri</h4>
        </div>
        <asp:GridView ID="Filmler1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="FilmID">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="FilmID"
                    DataNavigateUrlFormatString="/film/{0}"
                    DataTextField="Ad" HeaderText="Filmin Adı" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

